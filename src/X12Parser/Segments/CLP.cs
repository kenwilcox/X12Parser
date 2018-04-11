namespace X12Parser.Segments
{
    [SegmentName("Claim Payment Information")]
    public class CLP : X12
    {
        [Segment(1, 1, 38)]
        public string ClaimSubmitterSIdentifier { get; set; }
        [Segment(2, 1, 2)]
        public string ClaimStatusCode { get; set; }
        [Segment(3, 1, 18)]
        public string Charges { get; set; }
        [Segment(4, 1, 18)]
        public string Paid { get; set; }
        [Segment(5, 1, 18, true)]
        public string DeductibleNonCovered { get; set; }
        [Segment(6, 1, 2, true)]
        public string ClaimFilingIndicatorCode { get; set; }
        [Segment(7, 1, 50, true)]
        public string ReferenceIdentification { get; set; }
        [Segment(8, 1, 2, true)]
        public string FacilityCodeValue { get; set; }
        [Segment(9, 1, 1, true)]
        public string ClaimFrequencyTypeCode { get; set; }
        [Segment(10, 1, 2, true)]
        public string PatientStatusCode { get; set; }
        [Segment(11, 1, 4, true)]
        public string DiagnosisRelatedGroupDrgCode { get; set; }
        [Segment(12, 1, 15, true)]
        public string Quantity { get; set; }
        [Segment(13, 1, 10, true)]
        public string PercentageAsDecimal { get; set; }
        [Segment(14, 1, 1, true)]
        public string YesNoConditionOrResponseCode { get; set; }
    }
}
