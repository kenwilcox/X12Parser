namespace X12Parser.Segments
{
    [SegmentName("Provider Information")]
    public class PRV : X12
    {
        [Segment(1, 1, 3)]
        public string ProviderCode { get; set; }
        [Segment(2, 2, 3, true)]
        public string ReferenceIdentificationQualifier { get; set; }
        [Segment(3, 1, 50, true)]
        public string ReferenceIdentification { get; set; }
        [Segment(4, 2, 2, true)]
        public string StateOrProvinceCode { get; set; }
        //[Segment(1, 1, 3)]
        //public string ProviderSpecialtyCode { get; set; }
        //[Segment(2, 2, 2, true)]
        //public string AgencyQualifierCode { get; set; }
        //[Segment(3, 1, 1, true)]
        //public string YesNoConditionOrResponseCode { get; set; }

        [Segment(5, 1, 6, true)]
        public string ProviderSpecialtyInformation { get; set; }

        [Segment(6, 3, 3, true)]
        public string ProviderOrganizationCode { get; set; }
    }
}
