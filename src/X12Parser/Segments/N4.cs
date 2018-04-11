namespace X12Parser.Segments
{
    [SegmentName("Payer/Payee City, State, Zip Code")]
    public class N4 : X12
    {
        [Segment(1, 2, 30, true)]
        public string PayerCityName { get; set; }
        [Segment(2, 2, 2, true)]
        public string PayerStateOrProvinceCode { get; set; }
        [Segment(3, 3, 15, true)]
        public string PayerPostalCode { get; set; }
        [Segment(4, 2, 3, true)]
        public string PayerCountryCode { get; set; }
        [Segment(5, 1, 2, true)]
        public string PayerLocationQualifier { get; set; }
        [Segment(6, 1, 30, true)]
        public string PayerLocationIdentifier { get; set; }
        [Segment(7, 1, 3, true)]
        public string PayerCountrySubdivisionCode { get; set; }
    }
}
