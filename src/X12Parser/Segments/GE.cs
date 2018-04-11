namespace X12Parser.Segments
{
    [SegmentName("Functional Group Trailer")]
    public class GE : X12
    {
        [Segment(1, 1, 6)]
        public string NumberOfTransactionSetsIncluded { get; set; }
        [Segment(2, 1, 9)]
        public string GroupControlNumber { get; set; }
    }
}
