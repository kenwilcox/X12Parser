namespace X12Parser.Segments
{
    [SegmentName("Remittance Delivery Method")]
    public class RDM : X12
    {
        [Segment(1, 1, 2)]
        public string ReportTransmissionCode { get; set; }
        [Segment(2, 1, 60, true)]
        public string Name { get; set; }
        [Segment(3, 1, 256, true)]
        public string CommunicationNumber { get; set; }
    }
}
