using System;

namespace X12Parser
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Segment : Attribute
    {
        public int? MinLength;
        public int? MaxLength;
        public int Order { get; private set; }
        public bool Optional { get; private set; }

        public Segment(int order)
        {
            Order = order;
            Optional = false;
        }
        public Segment(int order, bool optional) : this(order)
        {
            Optional = optional;
        }
        public Segment(int order, int minLength, int maxLength) : this(order)
        {
            MinLength = minLength;
            MaxLength = maxLength;
        }
        public Segment(int order, int minLength, int maxLength, bool optional) : this(order, minLength, maxLength)
        {
            Optional = optional;
        }
    }
}
