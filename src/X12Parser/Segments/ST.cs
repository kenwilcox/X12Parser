namespace X12Parser.Segments
{
    [SegmentName("Transaction Set Header")]
    public class ST : X12
    {
        [Segment(1, 3, 3)]
        public string TransactionSetIdentifierCode { get; set; }
        [Segment(2, 4, 9)]
        public string TransactionSetControlNumber { get; set; }
        [Segment(3, 1, 35, optional: true)]
        public string ImplementationConventionReference { get; set; }
    }
}
