namespace X12Parser
{
    public interface IX12
    {
        string RecordType { get; set; }
        int SegmentIndex { get; set; }
#if INCLUDERAW
  string RawValue {get;set;}
#endif
    }
}
