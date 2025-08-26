using System;

namespace X12Parser.Segments
{
    [SegmentName("Health Care Information Codes")]
    public class HI : X12
    {
        [Segment(1, 2, 165, true)]
        public string HealthCareCodeInformation1 { get; set; }
        [Segment(2, 2, 165, true)]
        public string HealthCareCodeInformation2 { get; set; }
        [Segment(3, 2, 165, true)]
        public string HealthCareCodeInformation3 { get; set; }
        [Segment(4, 2, 165, true)]
        public string HealthCareCodeInformation4 { get; set; }
        [Segment(5, 2, 165, true)]
        public string HealthCareCodeInformation5 { get; set; }
        [Segment(6, 2, 165, true)]
        public string HealthCareCodeInformation6 { get; set; }
        [Segment(7, 2, 165, true)]
        public string HealthCareCodeInformation7 { get; set; }
        [Segment(8, 2, 165, true)]
        public string HealthCareCodeInformation8 { get; set; }
        [Segment(9, 2, 165, true)]
        public string HealthCareCodeInformation9 { get; set; }
        [Segment(10, 2, 165, true)]
        public string HealthCareCodeInformation10 { get; set; }
        [Segment(11, 2, 165, true)]
        public string HealthCareCodeInformation11 { get; set; }
        [Segment(12, 2, 165, true)]
        public string HealthCareCodeInformation12 { get; set; }

        // This is the partials that repeat in each part
        //  [Segment(1, 1, 30)]
        //  public string InsurerIdentification { get; set; }
        //
        //  [Segment(2, 1, 17)]
        //  public string PolicyNumber { get; set; }
        //
        //  [Segment(3, 1, 8)]
        //  public string CoverageEffectiveDate { get; set; }
        //  [Segment(4, 2, 8)]
        //  public string CoverageTerminationDate { get; set; }
        //
        //  [Segment(5, 1, 6)]
        //  public string ServiceLineIdentifier { get; set; }
        //
        //  [Segment(6, 1, 1)]
        //  public string SubscriberRelationshipCode { get; set; }
        //
        //  [Segment(7, 1, 10)]
        //  public string CopayDeductibleInformation { get; set; }
        //
        //  [Segment(8, 1, 1)]
        //  public string CoverageTypeCode { get; set; }
    }

}
