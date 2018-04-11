namespace X12Parser.Segments
{
    [SegmentName("Service Date")]
    public class DTM : X12
    {
        [Segment(1, 3, 3)]
        public string DateTimeQualifier { get; set; }
        [Segment(2, 8, 8, true)]
        public string Date { get; set; }
        [Segment(3, 4, 8, true)]
        public string Time { get; set; }
        [Segment(4, 2, 2, true)]
        public string TimeCode { get; set; }
        [Segment(5, 2, 3, true)]
        public string DateTimePeriodFormatQualifier { get; set; }
        [Segment(6, 1, 35, true)]
        public string DateTimePeriod { get; set; }
    }
}
