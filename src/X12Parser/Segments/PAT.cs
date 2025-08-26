namespace X12Parser.Segments
{
    [SegmentName("Patient Information")]
    public class PAT : X12
    {
        [Segment(1, 2, 2, true)]
        public string IndividualRelationshipCode { get; set; }
        [Segment(2, 1, 1, true)]
        public string PatientLocationCode { get; set; }
        [Segment(3, 2, 2, true)]
        public string EmploymentStatusCode { get; set; }
        [Segment(4, 1, 1, true)]
        public string StudentStatusCode { get; set; }
        [Segment(5, 2, 3, true)]
        public string DateTimePeriodFormatQualifier { get; set; }
        [Segment(6, 1, 35, true)]
        public string DateTimePeriod { get; set; }
        [Segment(7, 2, 2, true)]
        public string UnitOrBasisForMeasurementCode { get; set; }
        [Segment(8, 1, 10, true)]
        public string Weight { get; set; }
        [Segment(9, 1, 1, true)]
        public string YesNoConditionOrResponseCode { get; set; }
    }
}
