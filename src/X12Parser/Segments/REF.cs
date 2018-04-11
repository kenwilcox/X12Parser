namespace X12Parser.Segments
{
    [SegmentName("Service/Rendering Provider/Other Claim Related Identification")]
    public class REF : X12
    {
        [Segment(1, 2, 3)]
        public string ReferenceIdentificationQualifier { get; set; }
        [Segment(2, 1, 50)]
        public string ReferenceIdentification { get; set; }
        [Segment(3, 1, 80, true)]
        public string Description { get; set; }
        [Segment(4, 1, 50, true)]
        public string ReferenceIdentifier { get; set; }
        [Segment(5, 2, 3, true)]
        public string ReferenceIdentificationQualifier2 { get; set; }
        [Segment(6, 1, 50, true)]
        public string ReferenceIdentification2 { get; set; }
    }
}
