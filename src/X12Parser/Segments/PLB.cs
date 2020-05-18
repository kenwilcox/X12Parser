using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X12Parser.Segments
{
    [SegmentName("Provider Level Adjustment")]
    public class PLB : X12
    {
        [Segment(1, 1, 50)]
        public string ReferenceIdentification { get; set; }
        [Segment(2, 8, 8)]
        public string Date { get; set; }
        [Segment(3, 1, 52, true)]
        public string AdjustmentReasonCode1 { get; set; }
        [Segment(4, 1, 18, true)]
        public string MonetaryAmount1 { get; set; }
        [Segment(5, 1, 52, true)]
        public string AdjustmentReasonCode2 { get; set; }
        [Segment(6, 1, 18, true)]
        public string MonetaryAmount2 { get; set; }
        [Segment(7, 1, 52, true)]
        public string AdjustmentReasonCode3 { get; set; }
        [Segment(8, 1, 18, true)]
        public string MonetaryAmount3 { get; set; }
        [Segment(9, 1, 52, true)]
        public string AdjustmentReasonCode4 { get; set; }
        [Segment(10, 1, 18, true)]
        public string MonetaryAmount4 { get; set; }
        [Segment(11, 1, 52, true)]
        public string AdjustmentReasonCode5 { get; set; }
        [Segment(12, 1, 18, true)]
        public string MonetaryAmount5 { get; set; }
        [Segment(13, 1, 52, true)]
        public string AdjustmentReasonCode6 { get; set; }
        [Segment(14, 1, 18, true)]
        public string MonetaryAmount6 { get; set; }
    }
}
