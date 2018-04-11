namespace X12Parser
{
    public class X12 : IX12
    {
        [Segment(0, 2, 3)]
        public string RecordType { get; set; }
#if INCLUDERAW
  public string RawValue{get;set;}
#endif
    }
}
