<Query Kind="Program" />

void Main()
{
  var file = @"C:\Users\wilcoxk\Documents\GitHub\hippo\lib\hippo\segments\CUR.rb";
  var className = Path.GetFileNameWithoutExtension(file);
  var lines = File.ReadAllLines(file);
  var fields = new List<Field>();
  var field = new Field();
  foreach (var ln in lines)
  {
    var line = ln.Trim();
    if (line.StartsWith("field"))
    {
      field = new Field { Name = GetFieldName(line) };
    }
    else if (line.StartsWith(":sequence")) field.Sequence = GetValue(line);
    else if (line.StartsWith(":minimum")) field.Min = GetValue(line);
    else if (line.StartsWith(":maximum")) field.Max = GetValue(line);
    else if (line.StartsWith(":required")) field.Required = GetBoolValue(line);
    if (line.Trim().StartsWith(":data_element")) fields.Add(field);
  }
  var properties = "";
  foreach (var fld in fields)
  {
    var prop = "";
    prop += fld.Required ? requiredSegment : optionalSegment;
    prop += Environment.NewLine + property + Environment.NewLine;
    prop = prop
      .Replace("%SEQUENCE%", fld.Sequence)
      .Replace("%MIN%", fld.Min)
      .Replace("%MAX%", fld.Max)
      .Replace("%NAME%", fld.Name);
    properties += prop;
  }
  Console.WriteLine(template.Replace("%CLASSNAME%", className).Replace("%PROPERTIES%", properties));
}

const string template = @"
public class %CLASSNAME% : X12
{
%PROPERTIES%
}
";

const string requiredSegment = @"  [Segment(%SEQUENCE%, %MIN%, %MAX%)]";
const string optionalSegment = @"  [Segment(%SEQUENCE%, %MIN%, %MAX%, true)]";
const string property = @"  public string %NAME% { get; set; }";

bool GetBoolValue(string line)
{
  var value = Boolean.Parse(GetValue(line));
  return value;
}

string GetValue(string line)
{
  return line.Split('>')[1].Replace("'", "").Replace(",", "").Trim();
}

Dictionary<string, int> _used = new Dictionary<string, int>();

string GetFieldName(string line)
{
  var name = GetValue(line);

  if (_used.ContainsKey(name))
  {
    var amount = _used[name];
    amount++;    
    _used[name] = amount;
    name = $"{name}{amount}";
  }
  else
  {
    _used.Add(name, 1);
  }

  return name;
}

// Define other methods and classes here
class Field
{
  public string Name { get; set; }
  public string Sequence { get; set; }
  public string Min { get; set; }
  public string Max { get; set; }
  public bool Required { get; set; }
}