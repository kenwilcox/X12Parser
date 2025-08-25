X12Parser
=========

X12Parser is a simple parser for X12 format EDI documents. It was not created with any specific format in mind. It's not ready for prime time yet, but it does work. YMMV.

```
PM> Install-Package X12Parser.Core
```

Usage
-----
```csharp
var segments = Parser.Parse(<filename>);
```

segments will then be either a known list of parsed objects or a "dummy" X12 object that just tells you what segment name it is. The debug build is set up to include the RawValue of the segment line. This is to aid in what is missing.

You do not need to modify the project to include missing segments. The X12Tester project shows some examples of Segments that are defined in that project, but are still used in the X12Parser. There isn't a specific DI container used, it does everything internally. It does look in the X12Parser project as well as your own project for any classes with an eventual base class of X12. Your classes will take precedence over internal classes, so you can change things up as you see fit.

The internal classes so far are those in the 835 files I've seen. The test project includes one for a 277 file. The point being the parser shouldn't know or care about all types that will occur, it can be easily extended.

Factory
-------
Internally the Parser creates a X12Factory object that, when created, looks for all X12 classes inside the X12Parser project as well as the calling project. These are all cached so it doesn't have to look them up every time. However, this is done each time you call the Parse method. If you have several X12/EDI documents that need to be parsed, you can save some time upfront by creating the factory yourself and passing it into the Parse method like so

```csharp
var factory = new X12Factory(typeof(X12));
var segments = Parser.Parse(<filename>, factory);
```

In a directory of 363 edi documents doing this saved about 10 seconds on my machine.

Segments
--------
Segment definitions are loosely based off the [Hippo](https://rubygems.org/gems/hippo) Ruby Gem.

An X12 document is based on several segments. They're usually preceded with a tilde (~) and have either two or three character identifier. X12Parser currently uses that identifier to map, by name, to a class that has that segment defined. So if the Parser finds a segment that is like so:
```
~ISA*...
```
It will look for a corresponding class named ISA that eventually has a base class of X12. If one is found the properties and attributes are then loaded up and cached for the next time it is seen. The class should look like this:

```csharp
public class ISA : X12
{
    [Segment(1, 2, 2)]
    public string AuthorizationInformationQualifier { get; set; }
    [Segment(2, 10, 10)]
    public string AuthorizationInformation { get; set; }
}
```
The segment attribute defines the order (position), minimum expected length, maximum expected length and if the segment is optional. All parameters are optional except for the order (position). If you define a minimum length then you need to include a maximum length. Always start a position with 1 since the X12 base class automatically defines segment 0 as the RecordType, which will be the same value as the class name. This was done because every definition I found listed the segments starting with 1, and I wanted to avoid [off by one issues](https://en.wikipedia.org/wiki/Off-by-one_error).

In order to assist with potential errors the Parser will check your segments for you. It can't tell if you named them correctly, but it does let you know if a segment order has already been used and offers hints as to your error.

### Examples ###
```csharp
public class ISA : X12
{
    [Segment(1, 2, 2)]
    public string AuthorizationInformationQualifier { get; set; }
    [Segment(1, 2, 2)]
    public string AuthorizationInformation { get; set; }
    ...
}
```
In this example the Segment attribute was a copy and paste. When you run the program you will get the following exception:
```
System.ArgumentException: Segment order 1 has already been used on property AuthorizationInformationQualifier. Doubting that it is also for AuthorizationInformation, should this be 2?
```

```csharp
public class ISA : X12
{
    [Segment(1, 2, 2)]
    public string AuthorizationInformationQualifier { get; set; }
    [Segment(2, 10, 10)]
    public string AuthorizationInformation { get; set; }
    [Segment(3, 2, 2)]
    public string SecurityInformationQualifier { get; set; }
    [Segment(2, 10, 10)]
    public string SecurityInformation { get; set; }
    [Segment(5, 2, 2)]
    public string InterchangeSenderIdQualifier { get; set; }
    ...
{
```
This time Segment order 2 was copied for SecurityInformation, but the position wasn't updated, the following exception will be thrown
```
System.ArgumentException: Segment order 2 has already been used on property AuthorizationInformation. Doubting that it is also for SecurityInformation, should this be 4?
```
It's noticed that segment order 3 was already defined, so it lets you know it might be four, which would be correct in this case.

```csharp
public class ISA : X12
{
    [Segment(2, 2, 2)]
    public string AuthorizationInformationQualifier { get; set; }
    ...
{
```
In this instance we started our definitions, but we didn't start at 1, so the following exception is raised:
```
System.ArgumentException: Segment order 2 on property AuthorizationInformationQualifier was used before segment order 1, That doesn't seem correct.
```
Note, that segments don't have to be defined in order, but you do have to specify a preceding order before the order you're currently using. Meaning, you can't define an order or position of 10 without having 1-9 also defined.

#### Length ####

```csharp
public class ISA : X12
{
    [Segment(2, 12, 2)]
    public string AuthorizationInformationQualifier { get; set; }
    ...
{
```
If you put the min and max length in the wrong order you will get the following exception:
```
System.ArgumentException: Segment AuthorizationInformationQualifier max length of 2 is less than min length of 12
```

If you defined a min and max length and the data received isn't the length expected you will get the following exception:
```
System.ArgumentException: Segment AuthorizationInformationQualifier min length is defined as 2 but length is 0
```

If you don't care about the length of the data, or the provider doesn't follow what you expect you can exclude the min and max lengths like so
```csharp
public class ISA : X12
{
    [Segment(1)]
    public string AuthorizationInformationQualifier { get; set; }
    ...
{
```
Doing so will exclude checking the length of the data parsed.

#### Optionals ####

If the field is optional you can define it like so
```csharp
public class ISA : X12
{
    [Segment(1, 2, 2, true)]
    public string AuthorizationInformationQualifier { get; set; }
    ...
{
```
or like so, if you don't want the length checked
```csharp
public class ISA : X12
{
    [Segment(1, true)]
    public string AuthorizationInformationQualifier { get; set; }
    ...
{
```

All fields marked optional will be set to an empty string, so there should be no nulls.

#### Additional Properties ####

If you would like to add some additional properties to the object, either a calculated property or something else you can, in your own project extend an existing class like so

```csharp
public class ISA : X12Parser.Segments.ISA
{
    // I want to know when this was parsed.
    public string ParseDate => System.DateTime.Now.ToString();
}
```

```csharp
public class GE : X12Parser.Segments.GE
{
    // This will add two additional properties that are "calculated"
    public string FooBar => NumberOfTransactionSetsIncluded + ":" + GroupControlNumber;
    public string BarBaz => GroupControlNumber + ":" + GroupControlNumber;
}
```

Or, if you don't care to use the field names I've defined, you can replace it with your own.
```csharp
public class IEA : X12
{
    [Segment(1, 1, 5)]
    public string Uno { get; set; }
    [Segment(2, 9, 9)]
    public string Zwei { get; set; }
}
```

If there is a segment found, and it's not defined by any class you will get a "generic" X12 object that will either look like this:
```
*** ISA  ***
RecordType = ISA
```
or like this if the INCLUDERAW conditional compilation symbol is defined
```
*** ISA  ***
RecordType = ISA
RawValue = ISA*00*          *00*          *ZZ*1223485049     *ZZ*MEDAMERICA     *180222*1200*^*00501*498317667*0*P*:

```

Note, this symbol is defined in a debug build of the X12Parser project already.

I hope this helps. The Doc folder contains other docs I was using, other sources are listed below.

Sources: 
 - https://docs.oracle.com/cd/E19398-01/820-1275/agdaw/index.html


 Version History
----------------
- 0.8.7 Version 0.8.6 used System.Linq. I thought this was causing an issue with System.Data, but it was just Visual Studio no longer working. Diff in PR #14

- 0.8.6 X12 Base class now has Segment Index, so you know the position of the segment in the document, each segment in just incremented, starting at 0. Diff in PR #13

- 0.8.5 Parser now uses * and | as segment separators

- 0.8.4 Added a `boundsCheck` option. If `true` It will check to make sure your fields in the definition fit within the fields in the data. This is so if your segment definition only has 3 fields, but the data has more than that, this will throw a `FormatException` explaining that we expected 3 fields, but got 7 (for example).
    - Added MIA, MOA, QTY, RDM, TS2, and TS3 segments