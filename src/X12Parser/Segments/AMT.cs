namespace X12Parser.Segments
{
    [SegmentName("Amounts")]
    public class AMT : X12
    {
        [Segment(1, 1, 3)]
        public string AmountQualifierCode { get; set; }
        [Segment(2, 1, 18)]
        public string MonetaryAmount { get; set; }
        [Segment(3, 1, 1, true)]
        public string CreditDebitFlagCode { get; set; }
    }
}
