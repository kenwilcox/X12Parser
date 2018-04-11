using X12Parser;

namespace X12Tester
{
    [SegmentName("Date or Time Period")]
    public class DTP : X12
    {
        [Segment(1, 3, 3)]
        public string DateTimeQualifier { get; set; }
        [Segment(2, 2, 3)]
        public string DateTimePeriodFormatQualifier { get; set; }
        [Segment(3, 1, 35)]
        public string DateTimePeriod { get; set; }
    }
}
