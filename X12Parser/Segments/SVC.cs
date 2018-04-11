namespace X12Parser.Segments
{
    [SegmentName("Service Payment Information")]
    public class SVC : X12
    {
        [Segment(1)]//, 2, 2)]
        public string ProductServiceIdQualifier { get; set; }
        [Segment(2)]//, 1, 48)]
        public string ProductServiceId { get; set; }
        [Segment(3, true)]//, 2, 2, true)]
        public string ProcedureModifier { get; set; }
        [Segment(4, true)]//, 2, 2, true)]
        public string ProcedureModifier2 { get; set; }
        [Segment(5, true)]//, 2, 2, true)]
        public string ProcedureModifier3 { get; set; }
        [Segment(6, true)]//, 2, 2, true)]
        public string ProcedureModifier4 { get; set; }
        [Segment(7, true)]//, 1, 80, true)]
        public string Description { get; set; }
        //[Segment(8, 1, 48, true)]
        //public string ProductServiceId { get; set; }
    }
}
