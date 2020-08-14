namespace X12Parser.Segments
{
    [SegmentName("Outpatient Adjudication Information")]
    public class MOA : X12
    {
        [Segment(1, 1, 10, true)]
        public string PercentageAsDecimal { get; set; }
        [Segment(2, 1, 18, true)]
        public string MonetaryAmount { get; set; }
        [Segment(3, 1, 50, true)]
        public string ReferenceIdentification { get; set; }
        [Segment(4, 1, 50, true)]
        public string ReferenceIdentification2 { get; set; }
        [Segment(5, 1, 50, true)]
        public string ReferenceIdentification3 { get; set; }
        [Segment(6, 1, 50, true)]
        public string ReferenceIdentification4 { get; set; }
        [Segment(7, 1, 50, true)]
        public string ReferenceIdentification5 { get; set; }
        [Segment(8, 1, 18, true)]
        public string MonetaryAmount2 { get; set; }
        [Segment(9, 1, 18, true)]
        public string MonetaryAmount3 { get; set; }
    }
}
