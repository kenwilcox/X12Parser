namespace X12Parser.Segments
{
    [SegmentName("Payer/Payee Identification")]
    public class N1 : X12
    {
        [Segment(1, 2, 3)]
        public string EntityIdentifierCode { get; set; }
        [Segment(2, 1, 60, true)]
        public string Name { get; set; }
        [Segment(3, 1, 2, true)]
        public string IdentificationCodeQualifier { get; set; }
        [Segment(4, 2, 80, true)]
        public string IdentificationCode { get; set; }
        [Segment(5, 2, 2, true)]
        public string EntityRelationshipCode { get; set; }
        [Segment(6, 2, 3, true)]
        public string EntityIdentifierCode2 { get; set; }
    }
}
