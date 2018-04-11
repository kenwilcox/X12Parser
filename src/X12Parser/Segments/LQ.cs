namespace X12Parser.Segments
{
    [SegmentName("Health Care Remark Codes")]
    public class LQ : X12
    {
        [Segment(1, 1, 3, true)]
        public string CodeListQualifierCode { get; set; }
        [Segment(2, 1, 30, true)]
        public string IndustryCode { get; set; }
    }
}
