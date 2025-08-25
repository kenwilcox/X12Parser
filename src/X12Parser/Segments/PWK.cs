namespace X12Parser.Segments
{
    [SegmentName("Paperwork")]
    public class PWK : X12
    {
        [Segment(1, 2, 2)]
        public string ReportTypeCode { get; set; }
        [Segment(2, 1, 2, true)]
        public string ReportTransmissionCode { get; set; }
        [Segment(3, 1, 2, true)]
        public string ReportCopiesNeeded { get; set; }
        [Segment(4, 2, 3, true)]
        public string EntityIdentifierCode { get; set; }
        [Segment(5, 1, 2, true)]
        public string IdentificationCodeQualifier { get; set; }
        [Segment(6, 2, 80, true)]
        public string IdentificationCode { get; set; }
        [Segment(7, 1, 80, true)]
        public string Description { get; set; }

        [Segment(8, 1, 10, true)]
        public string ActionsIndicated { get; set; }

        //[Segment(1, 1, 2)]
        //public string PaperworkReportActionCode { get; set; }
        //[Segment(2, 1, 2, true)]
        //public string PaperworkReportActionCode2 { get; set; }
        //[Segment(3, 1, 2, true)]
        //public string PaperworkReportActionCode3 { get; set; }
        //[Segment(4, 1, 2, true)]
        //public string PaperworkReportActionCode4 { get; set; }
        //[Segment(5, 1, 2, true)]
        //public string PaperworkReportActionCode5 { get; set; }

        [Segment(9, 1, 2, true)]
        public string RequestCategoryCode { get; set; }
    }
}
