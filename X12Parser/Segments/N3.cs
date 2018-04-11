namespace X12Parser.Segments
{
    [SegmentName("Payer/Payee Address")]
    public class N3 : X12
    {
        [Segment(1, 1, 55)]
        public string AddressLine1 { get; set; }
        [Segment(2, 1, 55, true)]
        public string AddressLine2 { get; set; }
    }
}
