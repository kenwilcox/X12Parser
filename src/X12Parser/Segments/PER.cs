namespace X12Parser.Segments
{
    [SegmentName("Payer Contact Information")]
    public class PER : X12
    {
        [Segment(1, 2, 2)]
        public string ContactFunctionCode { get; set; }
        [Segment(2, 1, 60, true)]
        public string PayerContactName { get; set; }
        [Segment(3, 2, 2, true)]
        public string PayerCommunicationNumberQualifier { get; set; }
        [Segment(4, 1, 256, true)]
        public string PayerContactCommunicationNumber { get; set; }
        [Segment(5, 2, 2, true)]
        public string PayerCommunicationNumberQualifier2 { get; set; }
        [Segment(6, 1, 256, true)]
        public string PayerContactCommunicationNumber2 { get; set; }
        [Segment(7, 2, 2, true)]
        public string PayerCommunicationNumberQualifier3 { get; set; }
        [Segment(8, 1, 256, true)]
        public string PayerContactCommunicationNumber3 { get; set; }
        [Segment(9, 1, 20, true)]
        public string ContactInquiryReference { get; set; }
    }
}
