namespace X12Parser.Segments
{
    [SegmentName("Subscriber Information")]
    public class SBR : X12
    {
        [Segment(1, 1, 1)]
        public string PayerResponsibilitySequenceNumberCode { get; set; }
        [Segment(2, 2, 2, true)]
        public string IndividualRelationshipCode { get; set; }
        [Segment(3, 1, 50, true)]
        public string ReferenceIdentification { get; set; }
        [Segment(4, 1, 60, true)]
        public string Name { get; set; }
        [Segment(5, 1, 3, true)]
        public string InsuranceTypeCode { get; set; }
        [Segment(6, 1, 1, true)]
        public string CoordinationOfBenefitsCode { get; set; }
        [Segment(7, 1, 1, true)]
        public string YesNoConditionOrResponseCode { get; set; }
        [Segment(8, 2, 2, true)]
        public string EmploymentStatusCode { get; set; }
        [Segment(9, 1, 2, true)]
        public string ClaimFilingIndicatorCode { get; set; }
    }
}
