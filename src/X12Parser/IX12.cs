namespace X12Parser
{
    public interface IX12
    {
        string RecordType { get; set; }
#if INCLUDERAW
  string RawValue {get;set;}
#endif
    }
}
