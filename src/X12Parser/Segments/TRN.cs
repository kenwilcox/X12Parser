namespace X12Parser.Segments
{
    [SegmentName("Reassociation Trace Number")]
    public class TRN : X12
    {
        [Segment(1, 1, 2)]
        public string TraceTypeCode { get; set; }
        [Segment(2, 1, 50)]
        public string TraceNumber { get; set; }
        [Segment(3, 10, 10, true)]
        public string PayerIdentification { get; set; }
        [Segment(4, 1, 50, true)]
        public string ReferenceIdentification { get; set; }
    }
}
