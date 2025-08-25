namespace X12Parser.Segments
{
    [SegmentName("Professional Service")]
    public class SV1 : X12
    {
        [Segment(1, 3, 186)]
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
        [Segment(2, 1, 18, true)]
        public string MonetaryAmount { get; set; }
        [Segment(3, 2, 2, true)]
        public string UnitOrBasisForMeasurementCode { get; set; }
        [Segment(4, 1, 15, true)]
        public string Quantity { get; set; }
        [Segment(5, 1, 2, true)]
        public string FacilityCodeValue { get; set; }
        [Segment(6, 1, 2, true)]
        public string ServiceTypeCode { get; set; }

        [Segment(7, 1, 8)]
        public string CompositeDiagnosisCodePointer { get; set; }

        //[Segment(1, 1, 2)]
        //public string DiagnosisCodePointer { get; set; }
        //[Segment(2, 1, 2, true)]
        //public string DiagnosisCodePointer2 { get; set; }
        //[Segment(3, 1, 2, true)]
        //public string DiagnosisCodePointer3 { get; set; }
        //[Segment(4, 1, 2, true)]
        //public string DiagnosisCodePointer4 { get; set; }
        [Segment(8, 1, 18, true)]
        public string MonetaryAmount2 { get; set; }
        [Segment(9, 1, 1, true)]
        public string YesNoConditionOrResponseCode { get; set; }
        [Segment(10, 1, 2, true)]
        public string MultipleProcedureCode { get; set; }
        [Segment(11, 1, 1, true)]
        public string YesNoConditionOrResponseCode2 { get; set; }
        [Segment(12, 1, 1, true)]
        public string YesNoConditionOrResponseCode3 { get; set; }
        [Segment(13, 1, 2, true)]
        public string ReviewCode { get; set; }
        [Segment(14, 1, 2, true)]
        public string NationalOrLocalAssignedReviewValue { get; set; }
        [Segment(15, 1, 1, true)]
        public string CopayStatusCode { get; set; }
        [Segment(16, 1, 1, true)]
        public string HealthCareProfessionalShortageAreaCode { get; set; }
        [Segment(17, 1, 50, true)]
        public string ReferenceIdentification { get; set; }
        [Segment(18, 3, 15, true)]
        public string PostalCode { get; set; }
        [Segment(19, 1, 18, true)]
        public string MonetaryAmount3 { get; set; }
        [Segment(20, 1, 1, true)]
        public string LevelOfCareCode { get; set; }
        [Segment(21, 1, 1, true)]
        public string ProviderAgreementCode { get; set; }
    }
}
