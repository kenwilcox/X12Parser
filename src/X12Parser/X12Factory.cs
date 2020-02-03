using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace X12Parser
{
    public class X12Factory
    {
        private Dictionary<string, Type> _objects;
        private Dictionary<string, List<PropCache>> _properties;

        private readonly Type _type;
        public X12Factory(Type t): this(t, Assembly.GetEntryAssembly())
        {
            //_type = t;
            //var externalAssembly = Assembly.GetEntryAssembly();
            //FindClasses(externalAssembly);
        }

        public X12Factory(Type t, Assembly externalAssembly)
        {
            _type = t;
            FindClasses(externalAssembly);
        }

        public void DumpProperties()
        {
            _properties.Dump();
        }

        public List<PropCache> GetPropertiesForType(string type)
        {
            if (_properties.ContainsKey(type)) return _properties[type];
            else return new List<PropCache>();
        }

        public void FindClasses(Assembly externalAssembly)
        {
            _properties = new Dictionary<string, List<PropCache>>();

            var internalAssembly = Assembly.GetExecutingAssembly();
            //var externalAssembly = Assembly.GetEntryAssembly();

            _objects = internalAssembly.GetTypes()
                .Where(x => x.BaseType == _type)
                .ToDictionary(x => x.Name, x => x);

            if (internalAssembly != externalAssembly && externalAssembly != null)
            {
                var external = externalAssembly.GetTypes()
                    .Where(x => /*x.BaseType == _type ||*/ x.IsSubclassOf(_type))
                    .ToDictionary(x => x.Name, x => x);

                foreach (var ex in external)
                {
                    //_objects.Add(ex.Key, ex.Value);
                    _objects[ex.Key] = ex.Value;
                }
            }
        }

        public X12 GetX12Item(string data, bool dataChecks = true)
        {
            var segments = data.Split('*').ToList();
            var segment = segments.First();
            if (!_objects.ContainsKey(segment))
            {
                var generic = (X12)Activator.CreateInstance(typeof(X12));
                generic.RecordType = segment;
#if INCLUDERAW
                generic.RawValue = data;
#endif
                return generic;
            }
            var type = _objects[segment];
            var obj = (X12)Activator.CreateInstance(type);
            obj.RecordType = segment;
#if INCLUDERAW
            obj.RawValue = data;
#endif

            if (!_properties.ContainsKey(segment))
            {
                CacheProperties(segment, type);
            }

            var pc = _properties[segment];
            // blank out all optional properties, so we don't have nulls
            foreach (var p in pc.Where(x => x.Segment.Optional))
            {
                p.Property.SetValue(obj, "");
            }

            // now set them to the values we have
            for (var i = 1; i < segments.Count; i++)
            {
                //if (i > pc.Count) break;
                var prop = pc.FirstOrDefault(x => x.Segment.Order == i);
                if (prop == null) break;
                var value = segments[i];

                // Now, check what we've got, if we want to...
                if (dataChecks)
                {
                    CheckValue(value, prop.Segment, prop.Property);
                }
                prop.Property.SetValue(obj, value);
            }

            return obj;
        }

        private void CacheProperties(string segment, Type type)
        {
            var props = type.GetProperties();
            var seenOrder = new Dictionary<int, string>();
            var previousOrder = -1;
            var maxKey = 0;

            var list = new List<PropCache>();
            foreach (var prop in props)
            {
                var custom = (Segment)prop.GetCustomAttribute(typeof(Segment));
                if (custom == null) continue; // throw new Exception($"Property {prop.Name} missing Segment Attribute");
                if (seenOrder.ContainsKey(custom.Order)) throw new ArgumentException($"Segment order {custom.Order} has already been used on property {seenOrder[custom.Order]}. Doubting that it is also for {prop.Name}, should this be {++previousOrder}?");
                if (seenOrder.Any())
                {
                    maxKey = seenOrder.Max(x => x.Key);
                }
                if (maxKey < custom.Order - 1) throw new ArgumentException($"Segment order {custom.Order} on property {prop.Name} was used before segment order {custom.Order - 1}, That doesn't seem correct.");
                seenOrder.Add(custom.Order, prop.Name);
                previousOrder = custom.Order;
                var propCache = new PropCache
                {
                    Segment = custom,
                    Property = prop
                };
                list.Add(propCache);
            }
            _properties.Add(segment, list);
        }

        private void CheckValue(string value, Segment seg, PropertyInfo prop)
        {
            if (seg.Optional && string.IsNullOrEmpty(value)) return;
            if (seg.MinLength.HasValue && seg.MaxLength.HasValue && seg.MaxLength < seg.MinLength) throw new ArgumentException($"Segment {prop.ReflectedType.Name}.{prop.Name} max length of {seg.MaxLength.Value} is less than min length of {seg.MinLength.Value}");
            if (seg.MinLength.HasValue && seg.MaxLength.HasValue && seg.MinLength > seg.MaxLength) throw new ArgumentException($"Segment {prop.ReflectedType.Name}.{prop.Name} min length of {seg.MinLength.Value} is greater than min length of {seg.MaxLength.Value}");
            if (seg.MinLength.HasValue && value.Length < seg.MinLength.Value) throw new ArgumentException($"Segment {prop.ReflectedType.Name}.{prop.Name} min length is defined as {seg.MinLength.Value} but length is {value.Length}");
            if (seg.MaxLength.HasValue && value.Length > seg.MaxLength.Value) throw new ArgumentException($"Segment {prop.ReflectedType.Name}.{prop.Name} max length is defined as {seg.MaxLength.Value} but length is {value.Length}");
        }
    }
}
