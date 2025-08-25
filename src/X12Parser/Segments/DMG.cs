namespace X12Parser.Segments
{
    [SegmentName("Demographic Information")]
    public class DMG : X12
    {
        [Segment(1, 2, 3, true)]
        public string DateTimePeriodFormatQualifier { get; set; }
        [Segment(2, 1, 35, true)]
        public string DateTimePeriod { get; set; }
        [Segment(3, 1, 1, true)]
        public string GenderCode { get; set; }
        [Segment(4, 1, 1, true)]
        public string MaritalStatusCode { get; set; }

        //[Segment(1, 1, 1, true)]
        //public string RaceOrEthnicityCode { get; set; }
        //[Segment(2, 1, 3, true)]
        //public string CodeListQualifierCode { get; set; }
        //[Segment(3, 1, 30, true)]
        //public string IndustryCode { get; set; }

        [Segment(5, 1, 34, true)]
        public string CompositeRaceOrEthnicityInformation { get; set; }

        [Segment(6, 1, 2, true)]
        public string CitizenshipStatusCode { get; set; }
        [Segment(7, 2, 3, true)]
        public string CountryCode { get; set; }
        [Segment(8, 1, 2, true)]
        public string BasisOfVerificationCode { get; set; }
        [Segment(9, 1, 15, true)]
        public string Quantity { get; set; }
        [Segment(10, 1, 3, true)]
        public string CodeListQualifierCode { get; set; }
        [Segment(11, 1, 30, true)]
        public string IndustryCode { get; set; }
    }
}
