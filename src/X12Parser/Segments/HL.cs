namespace X12Parser.Segments
{
    [SegmentName("Hierarchical Level")]
    public class HL : X12
    {
        [Segment(1, 1, 12)]
        public string HierarchicalIdNumber { get; set; }
        [Segment(2, 1, 12, true)]
        public string HierarchicalParentIdNumber { get; set; }
        [Segment(3, 1, 2)]
        public string HierarchicalLevelCode { get; set; }
        [Segment(4, 1, 1, true)]
        public string HierarchicalChildCode { get; set; }
    }
}
