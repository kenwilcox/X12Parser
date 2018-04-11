namespace X12Parser.Segments
{
    [SegmentName("Header Number")]
    public class LX : X12
    {
        [Segment(1, 1, 6)]
        public string AssignedNumber { get; set; }
    }
}
