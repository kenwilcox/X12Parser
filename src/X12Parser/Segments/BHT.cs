namespace X12Parser.Segments
{
    public class BHT : X12
    {
        [Segment(1, 4, 4)]
        public string HierarchicalStructureCode { get; set; }
        [Segment(2, 2, 2)]
        public string TransactionSetPurposeCode { get; set; }
        [Segment(3, 1, 50, true)]
        public string ReferenceIdentification { get; set; }
        [Segment(4, 8, 8, true)]
        public string Date { get; set; }
        [Segment(5, 4, 8, true)]
        public string Time { get; set; }
        [Segment(6, 2, 2, true)]
        public string TransactionTypeCode { get; set; }

    }
}
