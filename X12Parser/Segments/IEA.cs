namespace X12Parser.Segments
{
    [SegmentName("Interchange Control Trailer")]
    public class IEA : X12
    {
        [Segment(1, 1, 5)]
        public string NumberOfIncludedFunctionalGroups { get; set; }
        [Segment(2, 9, 9)]
        public string InterchangeControlNumber { get; set; }
    }
}
