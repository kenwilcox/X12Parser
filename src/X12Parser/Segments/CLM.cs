namespace X12Parser.Segments
{
    [SegmentName("Claim Payment Information")]
    public class CLM : X12
    {
        [Segment(1, 1, 38)]
        public string ClaimSubmittersIdentifier { get; set; }
        [Segment(2, 1, 18, true)]
        public string MonetaryAmount { get; set; }
        [Segment(3, 1, 2, true)]
        public string ClaimFilingIndicatorCode { get; set; }
        [Segment(4, 1, 2, true)]
        public string NonInstitutionalClaimTypeCode { get; set; }

        [Segment(5, 1, 6)]
        public string HealthCareServiceLocationInformation { get; set; }

        [Segment(6, 1, 1, true)]
        public string YesNoConditionOrResponseCode { get; set; }
        [Segment(7, 1, 1, true)]
        public string ProviderAcceptAssignmentCode { get; set; }
        [Segment(8, 1, 1, true)]
        public string YesNoConditionOrResponseCode2 { get; set; }
        [Segment(9, 1, 1, true)]
        public string ReleaseOfInformationCode { get; set; }
        [Segment(10, 1, 1, true)]
        public string PatientSignatureSourceCode { get; set; }

        [Segment(11, 2, 14, true)]
        public string RelatedCausesInformation { get; set; }

        [Segment(12, 2, 3, true)]
        public string SpecialProgramCode { get; set; }
        [Segment(13, 1, 1, true)]
        public string YesNoConditionOrResponseCode3 { get; set; }
        [Segment(14, 1, 3, true)]
        public string LevelOfServiceCode { get; set; }
        [Segment(15, 1, 1, true)]
        public string YesNoConditionOrResponseCode4 { get; set; }
        [Segment(16, 1, 1, true)]
        public string ProviderAgreementCode { get; set; }
        [Segment(17, 1, 2, true)]
        public string ClaimStatusCode { get; set; }
        [Segment(18, 1, 1, true)]
        public string YesNoConditionOrResponseCode5 { get; set; }
        [Segment(19, 2, 2, true)]
        public string ClaimSubmissionReasonCode { get; set; }
        [Segment(20, 1, 2, true)]
        public string DelayReasonCode { get; set; }
    }
}
