namespace X12Parser.Segments
{
    [SegmentName("Transaction Set Trailer")]
    public class SE : X12
    {
        [Segment(1, 1, 10)]
        public string NumberOfIncludedSegments { get; set; }
        [Segment(2, 4, 9)]
        public string TransactionSetControlNumber { get; set; }
    }
}
