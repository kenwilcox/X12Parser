namespace X12Parser.Segments
{
    [SegmentName("Interchange Control Header")]
    public class ISA : X12
    {
        [Segment(1, 2, 2)]
        public string AuthorizationInformationQualifier { get; set; }
        [Segment(2, 10, 10)]
        public string AuthorizationInformation { get; set; }
        [Segment(3, 2, 2)]
        public string SecurityInformationQualifier { get; set; }
        [Segment(4, 10, 10)]
        public string SecurityInformation { get; set; }
        [Segment(5, 2, 2)]
        public string InterchangeSenderIdQualifier { get; set; }
        [Segment(6, 15, 15)]
        public string InterchangeSenderId { get; set; }
        [Segment(7, 2, 2)]
        public string InterchangeReceiverIdQualifier { get; set; }
        [Segment(8, 15, 15)]
        public string InterchangeReceiverId { get; set; }
        [Segment(9, 6, 6)]
        public string InterchangeDate { get; set; }
        [Segment(10, 4, 4)]
        public string InterchangeTime { get; set; }
        [Segment(11, 1, 1)]
        public string InterchangeControlStandardsId { get; set; }
        [Segment(12, 5, 5)]
        public string InterchangeControlVersionNumber { get; set; }
        [Segment(13, 9, 9)]
        public string InterchangeControlNumber { get; set; }
        [Segment(14, 1, 1)]
        public string AcknowledgmentRequested { get; set; }
        [Segment(15, 1, 1)]
        public string TestIndicator { get; set; }
        [Segment(16, 1, 1)]
        public string ComponentElementSeparator { get; set; }
    }
}
