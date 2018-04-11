namespace X12Parser.Segments
{
    [SegmentName("Functional Group Header")]
    public class GS : X12
    {
        [Segment(1, 2, 2)]
        public string FunctionalIdentifierCode { get; set; }
        [Segment(2, 2, 15)]
        public string ApplicationSendersCode { get; set; }
        [Segment(3, 2, 15)]
        public string ApplicationReceiversCode { get; set; }
        [Segment(4, 8, 8)]
        public string Date { get; set; }
        [Segment(5, 4, 8)]
        public string Time { get; set; }
        [Segment(6, 1, 9)]
        public string GroupControlNumber { get; set; }
        [Segment(7, 1, 2)]
        public string ResponsibleAgencyCode { get; set; }
        [Segment(8, 1, 12)]
        public string VersionReleaseIndustryIdentifierCode { get; set; }
    }
}
