using System;

namespace X12Parser.Segments
{
    // TODO - Not sure if these are correct, I don't have an example
    public class HI : X12
    {
        [Segment(1, 1, 30)]
        public string InsurerIdentification { get; set; }

        [Segment(2, 1, 17)]
        public string PolicyNumber { get; set; }

        [Segment(3, 1, 8)]
        public string CoverageEffectiveDate { get; set; }
        [Segment(4, 2, 8)]
        public string CoverageTerminationDate { get; set; }

        [Segment(5, 1, 6)]
        public string ServiceLineIdentifier { get; set; }

        [Segment(6, 1, 1)]
        public char SubscriberRelationshipCode { get; set; }

        [Segment(7, 1, 10)]
        public string CopayDeductibleInformation { get; set; }

        [Segment(8, 1, 1)]
        public char CoverageTypeCode { get; set; }
    }

}
