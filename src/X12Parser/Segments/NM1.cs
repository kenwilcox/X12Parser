namespace X12Parser.Segments
{
    [SegmentName("Patient/Service Provider Name")]
    public class NM1 : X12
    {
        [Segment(1, 2, 3)]
        public string EntityIdentifierCode { get; set; }
        [Segment(2, 1, 1)]
        public string EntityTypeQualifier { get; set; }
        [Segment(3, 1, 60, true)]
        public string NameLastOrOrganizationName { get; set; }
        [Segment(4, 1, 35, true)]
        public string NameFirst { get; set; }
        [Segment(5, 1, 25, true)]
        public string NameMiddle { get; set; }
        [Segment(6, 1, 10, true)]
        public string NamePrefix { get; set; }
        [Segment(7, 1, 10, true)]
        public string NameSuffix { get; set; }
        [Segment(8, 1, 2, true)]
        public string IdentificationCodeQualifier { get; set; }
        [Segment(9, true)]//2, 80, true)]
        public string IdentificationCode { get; set; }
        [Segment(10, true)]//2, 2, true)]
        public string EntityRelationshipCode { get; set; }
        [Segment(11, 2, 3, true)]
        public string EntityIdentifierCode2 { get; set; }
        [Segment(12, 1, 60, true)]
        public string NameLastOrOrganizationName2 { get; set; }
    }
}
