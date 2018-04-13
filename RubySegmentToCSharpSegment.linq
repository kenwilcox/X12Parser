<Query Kind="Program">
  <Connection>
    <ID>1a4f8074-aaf1-4e8a-a275-8148f1366393</ID>
    <Persist>true</Persist>
    <Server>mbsi-srvprod01</Server>
    <IsProduction>true</IsProduction>
    <Database>PARM</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

void Main()
{
  var file = @"C:\Ruby21\lib\ruby\gems\2.1.0\gems\hippo-0.5.5\lib\hippo\segments\HL.rb";
  var className = Path.GetFileNameWithoutExtension(file);
  var lines = File.ReadAllLines(file);
  var fields = new List<Field>();
  var field = new Field();
  foreach(var ln in lines)
  {
    var line = ln.Trim();
    if (line.StartsWith("field"))
    {
      field = new Field{Name = GetValue(line)};
    } 
    else if (line.StartsWith(":sequence")) field.Sequence = GetValue(line);
    else if (line.StartsWith(":minimum")) field.Min = GetValue(line);
    else if (line.StartsWith(":maximum")) field.Max = GetValue(line);
    else if (line.StartsWith(":required")) field.Required = GetBoolValue(line);
    if (line.Trim().StartsWith(":data_element")) fields.Add(field);
  }
  var properties = "";
  foreach(var fld in fields)
  {
    var prop = "";
    prop += fld.Required? requiredSegment : optionalSegment;
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

// Define other methods and classes here
class Field
{
 public string Name{get;set;} 
 public string Sequence {get;set;}
 public string Min {get;set;}
 public string Max {get;set;}
 public bool Required {get;set;}
}