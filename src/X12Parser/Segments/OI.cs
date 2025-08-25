namespace X12Parser.Segments
{

    public class OI : X12
    {
        [Segment(1, 1, 2, true)]
        public string ClaimFilingIndicatorCode { get; set; }
        [Segment(2, 2, 2, true)]
        public string ClaimSubmissionReasonCode { get; set; }
        [Segment(3, 1, 1, true)]
        public string YesNoConditionOrResponseCode { get; set; }
        [Segment(4, 1, 1, true)]
        public string PatientSignatureSourceCode { get; set; }
        [Segment(5, 1, 1, true)]
        public string ProviderAgreementCode { get; set; }
        [Segment(6, 1, 1, true)]
        public string ReleaseOfInformationCode { get; set; }

    }

}
