namespace X12Parser.Segments
{
    [SegmentName("Claim/Service Adjustment")]
    public class CAS : X12
    {
        [Segment(1, 1, 2)]
        public string ClaimAdjustmentGroupCode { get; set; }
        [Segment(2, 1, 5)]
        public string FirstAdjustmentReasonCode { get; set; }
        [Segment(3, 1, 18)]
        public string FirstAdjustmentAmount { get; set; }
        [Segment(4, 1, 15, true)]
        public string FirstAdjustmentQuantity { get; set; }
        [Segment(5, 1, 5, true)]
        public string SecondAdjustmentReasonCode { get; set; }
        [Segment(6, 1, 18, true)]
        public string SecondAdjustmentAmount { get; set; }
        [Segment(7, 1, 15, true)]
        public string SecondAdjustmentQuantity { get; set; }
        [Segment(8, 1, 5, true)]
        public string ThridAdjustmentReasonCode { get; set; }
        [Segment(9, 1, 18, true)]
        public string ThirdAdjustmentAmount { get; set; }
        [Segment(10, 1, 15, true)]
        public string ThirdAdjustmentQuantity { get; set; }
        [Segment(11, 1, 5, true)]
        public string FourthAdjustmentReasonCode { get; set; }
        [Segment(12, 1, 18, true)]
        public string FourthAdjustmentAmount { get; set; }
        [Segment(13, 1, 15, true)]
        public string FourthAdjustmentQuantity { get; set; }
        [Segment(14, 1, 5, true)]
        public string FifthAdjustmentReasonCode { get; set; }
        [Segment(15, 1, 18, true)]
        public string FifthAdjustmentAmount { get; set; }
        [Segment(16, 1, 15, true)]
        public string FifthAdjustmentQuantity { get; set; }
        [Segment(17, 1, 5, true)]
        public string SixthAdjustmentReasonCode { get; set; }
        [Segment(18, 1, 18, true)]
        public string SixthAdjustmentAmount { get; set; }
        [Segment(19, 1, 15, true)]
        public string SixthAdjustmentQuantity { get; set; }
    }
}
