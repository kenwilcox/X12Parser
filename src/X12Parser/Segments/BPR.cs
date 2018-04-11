namespace X12Parser.Segments
{
    [SegmentName("Financial Information")]
    public class BPR : X12
    {
        [Segment(1, 1, 2)]
        public string TransactionHandlingCode { get; set; }
        [Segment(2, 1, 18)]
        public string TotalPaymentAmount { get; set; }
        [Segment(3, 1, 1)]
        public string CreditDebitFlagCode { get; set; }
        [Segment(4, 3, 3)]
        public string PaymentMethodCode { get; set; }
        [Segment(5, 1, 10, true)]
        public string PaymentFormatCode { get; set; }
        [Segment(6, 2, 2, true)]
        public string DfiIdQualifier { get; set; }
        [Segment(7, 3, 12, true)]
        public string SenderDfiId { get; set; }
        [Segment(8, 1, 3, true)]
        public string SenderAccountNumberQualifier { get; set; }
        [Segment(9, 1, 35, true)]
        public string SenderAccountNumber { get; set; }
        [Segment(10, 10, 10, true)]
        public string PayerIdentifier { get; set; }
        [Segment(11, 9, 9, true)]
        public string OriginatingCompanySupplementalCode { get; set; }
        [Segment(12, 2, 2, true)]
        public string DfiIdQualifier2 { get; set; }
        [Segment(13, 3, 12, true)]
        public string ReceiverProviderBankId { get; set; }
        [Segment(14, 1, 3, true)]
        public string ReceiverProviderAccountNumberQualifier { get; set; }
        [Segment(15, 1, 35, true)]
        public string ReceiverProviderAccountNumber { get; set; }
        [Segment(16, 8, 8, true)]
        public string CheckEffectiveDate { get; set; }
        [Segment(17, 1, 3, true)]
        public string BusinessFunctionCode { get; set; }
        [Segment(18, 2, 2, true)]
        public string DfiIdQualifier3 { get; set; }
        [Segment(19, 3, 12, true)]
        public string DfiId { get; set; }
        [Segment(20, 1, 3, true)]
        public string AccountNumberQualifier { get; set; }
        [Segment(21, 1, 35, true)]
        public string AccountNumber { get; set; }
    }
}
