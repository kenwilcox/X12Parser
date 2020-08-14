namespace X12Parser.Segments
{
    [SegmentName("Provider Summary Information")]
    public class TS3 : X12
    {
        [Segment(1, 1, 50)]
        public string ReferenceIdentification { get; set; }
        [Segment(2, 1, 2)]
        public string FacilityCodeValue { get; set; }
        [Segment(3, 8, 8)]
        public string Date { get; set; }
        [Segment(4, 1, 15)]
        public string Quantity { get; set; }
        [Segment(5, 1, 18)]
        public string MonetaryAmount { get; set; }
        [Segment(6, 1, 18, true)]
        public string MonetaryAmount2 { get; set; }
        [Segment(7, 1, 18, true)]
        public string MonetaryAmount3 { get; set; }
        [Segment(8, 1, 18, true)]
        public string MonetaryAmount4 { get; set; }
        [Segment(9, 1, 18, true)]
        public string MonetaryAmount5 { get; set; }
        [Segment(10, 1, 18, true)]
        public string MonetaryAmount6 { get; set; }
        [Segment(11, 1, 18, true)]
        public string MonetaryAmount7 { get; set; }
        [Segment(12, 1, 18, true)]
        public string MonetaryAmount8 { get; set; }
        [Segment(13, 1, 18, true)]
        public string MonetaryAmount9 { get; set; }
        [Segment(14, 1, 18, true)]
        public string MonetaryAmount10 { get; set; }
        [Segment(15, 1, 18, true)]
        public string MonetaryAmount11 { get; set; }
        [Segment(16, 1, 18, true)]
        public string MonetaryAmount12 { get; set; }
        [Segment(17, 1, 18, true)]
        public string MonetaryAmount13 { get; set; }
        [Segment(18, 1, 18, true)]
        public string MonetaryAmount14 { get; set; }
        [Segment(19, 1, 18, true)]
        public string MonetaryAmount15 { get; set; }
        [Segment(20, 1, 18, true)]
        public string MonetaryAmount16 { get; set; }
        [Segment(21, 1, 18, true)]
        public string MonetaryAmount17 { get; set; }
        [Segment(22, 1, 18, true)]
        public string MonetaryAmount18 { get; set; }
        [Segment(23, 1, 15, true)]
        public string Quantity2 { get; set; }
        [Segment(24, 1, 18, true)]
        public string MonetaryAmount19 { get; set; }
    }
}
