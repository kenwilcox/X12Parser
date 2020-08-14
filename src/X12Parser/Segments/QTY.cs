namespace X12Parser.Segments
{
    [SegmentName("Supplemental Quantity")]
    public class QTY : X12
    {
        [Segment(1, 2, 2)]
        public string QuantityQualifier { get; set; }
        [Segment(2, 1, 15, true)]
        public string Quantity { get; set; }
    }
}
