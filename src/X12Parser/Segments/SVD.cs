namespace X12Parser.Segments
{
    [SegmentName("Service Line Adjudication")]
    public class SVD : X12
    {
        [Segment(1, 2, 80)]
        public string IdentificationCode { get; set; }
        [Segment(2, 1, 18)]
        public string MonetaryAmount { get; set; }

        [Segment(3, 3, 196)]
        public string CompositeMedicalProcedureIdentifier { get; set; }

        //[Segment(1, 2, 2)]
        //public string ProductServiceIdQualifier { get; set; }
        //[Segment(2, 1, 48)]
        //public string ProductServiceId { get; set; }
        //[Segment(3, 2, 2, true)]
        //public string ProcedureModifier { get; set; }
        //[Segment(4, 2, 2, true)]
        //public string ProcedureModifier2 { get; set; }
        //[Segment(5, 2, 2, true)]
        //public string ProcedureModifier3 { get; set; }
        //[Segment(6, 2, 2, true)]
        //public string ProcedureModifier4 { get; set; }
        //[Segment(7, 1, 80, true)]
        //public string Description { get; set; }
        //[Segment(8, 1, 48, true)]
        //public string ProductServiceId2 { get; set; }

        [Segment(4, 1, 48, true)]
        public string ProductServiceId { get; set; }
        [Segment(5, 1, 15, true)]
        public string Quantity { get; set; }
        [Segment(6, 1, 6, true)]
        public string AssignedNumber { get; set; }
    }
}
