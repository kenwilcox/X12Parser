using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace X12Parser.Tests
{
    public class ParserTests
    {
        private readonly X12Factory _factory;
        private readonly List<X12> _sut;
        private readonly List<PropCache> _cache;

        // ReSharper disable CommentTypo
        //private const string _isa = @"ISA*00*AAAAAAAAAA*11*BBBBBBBBBB*22*123456789012345*33*987654321987654*CCCCCC*DDDD*^*EEEEE*FFFFFFFFF*G*T*:~";
        private const string IsaProperties = @"ISA*AA*BBBBBBBBBB*CC*DDDDDDDDDD*EE*FFFFFFFFFFFFFFF*GG*HHHHHHHHHHHHHHH*IIIIII*JJJJ*K*LLLLL*MMMMMMMMM*N*O*P~";
        private const string OptionalFoo = @"FOO*REQUIRED*~";
        private const string OnlyOptional = @"OPT*****HERE~";
        private const string MinTest = "MIN*123456789~";
        private const string MaxTest = "MAX*12345678901234567890~";
        private const string IsaWithCrLf = @"ISA*00*          *00*          *ZZ*610442         *ZZ*0FB            *190326*2111*^*00
001*
000000002*0*P*>~";
        private const string BhtLine = @"BHT*0019*00*144*20250219*020022*CH~";
        private const string ClmLine = @"CLM*XXX201640759-9*965***23:B:1*Y*A*Y*Y~";
        private const string DmgLine = @"DMG*D8*19771028*M~";
        
        private const string HiLineShort = @"HI*ABK:R55~";
        private const string HiLineLong = @"HI*ABK:E8342*ABF:M25512*ABF:M25511*ABF:M25561*ABF:M25551*ABF:G8929~";

        private const string HlLineShort = @"HL*1**20*1~";
        private const string HlLineLong = @"HL*10*1*22*0~";

        private const string NteLineShort = @"NTE*ADD*CORRECTED DX~";
        private const string NteLineLong = @"NTE*ADD*CORRECTED CLAIM PRIMARY EOB ATTACHED~";

        private const string OiLine = @"OI***Y*P**Y~";

        private const string PatLineShort = @"PAT*01~";
        private const string PatLineLong = @"PAT*****D8*20250121~";

        private const string PrvLine = @"PRV*PE*PXC*QQQP00000X~";
        private const string PwkLine = @"PWK*OZ*BM***AC*2023232BF3050~";
        
        private const string SbrShortLine = @"SBR*P********CI";
        private const string SbrLongLine = @"SBR*P*18**BLUE CROSS BLUE SHIELD HEALTHY*****MC~";

        private const string Sv1ShortLine = @"SV1*HC:99239*682*UN*1***1~";
        private const string Sv1LongLine = @"SV1*HC:99285:25:GC:FS*1286*UN*1***1:2:3:4**Y~";

        private const string SvdShortLine = @"SVD*PAPER*0*HC:99285:GC**1~";
        private const string SvdLongLine = @"SVD*GEB00074P*204.83*HC:99284**1~";

        public ParserTests()
        {
            // This is only necessary in unit tests because the test runners are typically unmanaged code
            // which makes the call to GetEntryAssembly return null, this way we are handing
            // "ourselves" to the factory instead of the factory trying to find us.
            _factory = new X12Factory(typeof(X12), Assembly.GetExecutingAssembly());
            _sut = Parser.ParseText(IsaProperties, _factory);
            _cache = _factory.GetPropertiesForType("ISA");
        }

        [Fact]
        public void TestThat_ISA_ParsesASingleLine()
        {
            Assert.Single(_sut);
        }

        [Fact]
        public void TestThat_ISA_IsParsedProperly()
        {
            // position 0 should be ISA
            var sut = _sut.FirstOrDefault();
            Assert.NotNull(sut);

            Assert.Equal("ISA", sut.RecordType);
            var startAt = 65;
            for(var i = 1; i < _cache.Count; i++)
            {
                var info = _cache.FirstOrDefault(x => x.Segment.Order == i);
                Assert.NotNull(info);
                Assert.NotNull(info.Segment);
                Assert.NotNull(info.Segment.MaxLength);
                var c = Convert.ToChar(startAt);
                var test = c.ToString().PadLeft(info.Segment.MaxLength.Value, c);
                Assert.Equal(test, info.Property.GetValue(sut));
                startAt++;
            }
            // position length -1 should be :
        }

        [Fact]
        public void TestThat_OptionalFields_WorkAsExpected()
        {
            var sut = Parser.ParseText(OptionalFoo, _factory);
            var foo = sut.FirstOrDefault() as FOO;
            Assert.NotNull(foo);
            Assert.Equal("FOO", foo.RecordType);
            Assert.Equal("REQUIRED", foo.FieldOne);
            Assert.Equal("", foo.OptionalOne);
            Assert.Equal("", foo.OptionalTwo);
        }

        [Fact]
        public void TestThat_OnlyOptionalFields_WorkAsExpected()
        {
            var sut = Parser.ParseText(OnlyOptional, _factory);
            var opt = sut.FirstOrDefault() as OPT;
            Assert.NotNull(opt);
            Assert.Equal("OPT", opt.RecordType);
            Assert.Equal("HERE", opt.Here);
        }

        [Fact]
        public void TestThat_MinLength_IsChecked()
        {
            Assert.Throws<ArgumentException>(() => Parser.ParseText(MinTest, _factory));
        }

        [Fact]
        public void TestThat_MaxLength_IsChecked()
        {
            Assert.Throws<ArgumentException>(() => Parser.ParseText(MaxTest, _factory));
        }

        [Fact]
        public void TestThat_ThisHandlesCrLf()
        {
            // The library does not handle this anymore. There were way too many unexpected
            // characters in the 835 files I've parsed.
            var sut = Parser.ParseText(IsaWithCrLf.Replace("\r\n", "").Replace("\n", ""), _factory);
            var opt = sut.FirstOrDefault() as Segments.ISA;
            Assert.NotNull(opt);
            Assert.Equal("ISA", opt.RecordType);
            // new line in value
            Assert.Equal("00001", opt.InterchangeControlVersionNumber);
            // new line before segment
            Assert.Equal("000000002", opt.InterchangeControlNumber);
        }

        [Fact]
        public void TestThat_BHTParses()
        {
            var sut = Parser.ParseText(BhtLine, _factory);
            var opt = sut.FirstOrDefault() as Segments.BHT;
            Assert.NotNull(opt);

            Assert.Equal("BHT", opt.RecordType);
            Assert.Equal("0019", opt.HierarchicalStructureCode);
            Assert.Equal("00", opt.TransactionSetPurposeCode);
            Assert.Equal("144", opt.ReferenceIdentification);
            Assert.Equal("20250219", opt.Date);
            Assert.Equal("020022", opt.Time);
            Assert.Equal("CH", opt.TransactionTypeCode);
        }

        [Fact]
        public void TestThat_ClmParses()
        {
            var sut = Parser.ParseText(ClmLine, _factory);
            var opt = sut.FirstOrDefault() as Segments.CLM;
            Assert.NotNull(opt);
            
            Assert.Equal("CLM", opt.RecordType);
            Assert.Equal("XXX201640759-9", opt.ClaimSubmittersIdentifier);
            Assert.Equal("23:B:1", opt.HealthCareServiceLocationInformation);
        }

        [Fact]
        public void TestThat_DmgParses()
        {
            var sut = Parser.ParseText(DmgLine, _factory);
            var opt = sut.FirstOrDefault() as Segments.DMG;
            Assert.NotNull(opt);

            Assert.Equal("DMG", opt.RecordType);
            Assert.Equal("D8", opt.DateTimePeriodFormatQualifier);
            Assert.Equal("19771028", opt.DateTimePeriod);
            Assert.Equal("M", opt.GenderCode);

            // I don't have any other DMG data with optional fields...
        }

        [Fact]
        public void TestThat_HiShortParses()
        {
            var sut = Parser.ParseText(HiLineShort, _factory);
            var opt = sut.FirstOrDefault() as Segments.HI;
            Assert.NotNull(opt);

            Assert.Equal("HI", opt.RecordType);
            Assert.Equal("ABK:R55", opt.HealthCareCodeInformation1);
        }

        [Fact]
        public void TestThat_HiLongParses()
        {
            var sut = Parser.ParseText(HiLineLong, _factory);
            var opt = sut.FirstOrDefault() as Segments.HI;
            Assert.NotNull(opt);
            
            Assert.Equal("HI", opt.RecordType);
            Assert.Equal("ABK:E8342", opt.HealthCareCodeInformation1);
            Assert.Equal("ABF:M25512", opt.HealthCareCodeInformation2);
            Assert.Equal("ABF:M25511", opt.HealthCareCodeInformation3);
            Assert.Equal("ABF:M25561", opt.HealthCareCodeInformation4);
            Assert.Equal("ABF:M25551", opt.HealthCareCodeInformation5);
            Assert.Equal("ABF:G8929", opt.HealthCareCodeInformation6);
        }

        [Fact]
        public void TestThat_HlShortParses()
        {
            var sut = Parser.ParseText(HlLineShort, _factory);
            var opt = sut.FirstOrDefault() as Segments.HL;
            Assert.NotNull(opt);

            Assert.Equal("HL", opt.RecordType);
            Assert.Equal("1", opt.HierarchicalIdNumber);
            Assert.Equal("", opt.HierarchicalParentIdNumber);
            Assert.Equal("20", opt.HierarchicalLevelCode);
            Assert.Equal("1", opt.HierarchicalChildCode);
        }

        [Fact]
        public void TestThat_HlLongParses()
        {
            var sut = Parser.ParseText(HlLineLong, _factory);
            var opt = sut.FirstOrDefault() as Segments.HL;
            Assert.NotNull(opt);

            Assert.Equal("HL", opt.RecordType);
            Assert.Equal("10", opt.HierarchicalIdNumber);
            Assert.Equal("1", opt.HierarchicalParentIdNumber);
            Assert.Equal("22", opt.HierarchicalLevelCode);
            Assert.Equal("0", opt.HierarchicalChildCode);
        }

        [Fact]
        public void TestThat_NteShortParses()
        {
            var sut = Parser.ParseText(NteLineShort, _factory);
            var opt = sut.FirstOrDefault() as Segments.NTE;
            Assert.NotNull(opt);

            Assert.Equal("NTE", opt.RecordType);
            Assert.Equal("ADD", opt.NoteReferenceCode);
            Assert.Equal("CORRECTED DX", opt.Description);
        }

        [Fact]
        public void TestThat_NteLongParses()
        {
            var sut = Parser.ParseText(NteLineLong, _factory);
            var opt = sut.FirstOrDefault() as Segments.NTE;
            Assert.NotNull(opt);

            Assert.Equal("NTE", opt.RecordType);
            Assert.Equal("ADD", opt.NoteReferenceCode);
            Assert.Equal("CORRECTED CLAIM PRIMARY EOB ATTACHED", opt.Description);
        }

        [Fact]
        public void TestThat_OiParses()
        {
            var sut = Parser.ParseText(OiLine, _factory);
            var opt = sut.FirstOrDefault() as Segments.OI;
            Assert.NotNull(opt);

            Assert.Equal("OI", opt.RecordType);
            Assert.Equal("", opt.ClaimFilingIndicatorCode);
            Assert.Equal("", opt.ClaimSubmissionReasonCode);
            Assert.Equal("Y", opt.YesNoConditionOrResponseCode);
            Assert.Equal("P", opt.PatientSignatureSourceCode);
            Assert.Equal("", opt.ProviderAgreementCode);
            Assert.Equal("Y", opt.ReleaseOfInformationCode);
        }

        [Fact]
        public void TestThat_PatShortParses()
        {
            var sut = Parser.ParseText(PatLineShort, _factory);
            var opt = sut.FirstOrDefault() as Segments.PAT;
            Assert.NotNull(opt);

            Assert.Equal("PAT", opt.RecordType);
            Assert.Equal("01", opt.IndividualRelationshipCode);
        }

        [Fact]
        public void TestThat_PatLongParses()
        {
            var sut = Parser.ParseText(PatLineLong, _factory);
            var opt = sut.FirstOrDefault() as Segments.PAT;
            Assert.NotNull(opt);

            Assert.Equal("PAT", opt.RecordType);
            Assert.Equal("", opt.IndividualRelationshipCode);
            Assert.Equal("", opt.PatientLocationCode);
            Assert.Equal("", opt.EmploymentStatusCode);
            Assert.Equal("", opt.StudentStatusCode);
            Assert.Equal("D8", opt.DateTimePeriodFormatQualifier);
            Assert.Equal("20250121", opt.DateTimePeriod);
        }

        [Fact]
        public void TestThat_PrvParses()
        {
            var sut = Parser.ParseText(PrvLine, _factory);
            var opt = sut.FirstOrDefault() as Segments.PRV;
            Assert.NotNull(opt);
            
            Assert.Equal("PRV", opt.RecordType);
            Assert.Equal("PE", opt.ProviderCode);
            Assert.Equal("PXC", opt.ReferenceIdentificationQualifier);
            Assert.Equal("QQQP00000X", opt.ReferenceIdentification);
            Assert.Equal("", opt.StateOrProvinceCode);
            Assert.Equal("", opt.ProviderSpecialtyInformation);
            Assert.Equal("", opt.ProviderOrganizationCode);
        }

        [Fact]
        public void TestThat_PwkParses()
        {
            var sut = Parser.ParseText(PwkLine, _factory);
            var opt = sut.FirstOrDefault() as Segments.PWK;
            Assert.NotNull(opt);
            
            Assert.Equal("PWK", opt.RecordType);
            Assert.Equal("OZ", opt.ReportTypeCode);
            Assert.Equal("BM", opt.ReportTransmissionCode);
            Assert.Equal("", opt.ReportCopiesNeeded);
            Assert.Equal("", opt.EntityIdentifierCode);
            Assert.Equal("AC", opt.IdentificationCodeQualifier);
            Assert.Equal("2023232BF3050", opt.IdentificationCode);
            Assert.Equal("", opt.Description);
            Assert.Equal("", opt.ActionsIndicated);
            Assert.Equal("", opt.RequestCategoryCode);
        }

        [Fact]
        public void TestThat_SbrShortParses()
        {
            var sut = Parser.ParseText(SbrShortLine, _factory);
            var opt = sut.FirstOrDefault() as Segments.SBR;
            Assert.NotNull(opt);
            
            Assert.Equal("SBR", opt.RecordType);
            Assert.Equal("P", opt.PayerResponsibilitySequenceNumberCode);
            Assert.Equal("", opt.IndividualRelationshipCode);
            Assert.Equal("", opt.ReferenceIdentification);
            Assert.Equal("", opt.Name);
            Assert.Equal("", opt.InsuranceTypeCode);
            Assert.Equal("", opt.CoordinationOfBenefitsCode);
            Assert.Equal("", opt.YesNoConditionOrResponseCode);
            Assert.Equal("", opt.EmploymentStatusCode);
            Assert.Equal("CI", opt.ClaimFilingIndicatorCode);
        }

        [Fact]
        public void TestThat_SbrLongParses()
        {
            var sut = Parser.ParseText(SbrLongLine, _factory);
            var opt = sut.FirstOrDefault() as Segments.SBR;
            Assert.NotNull(opt);

            Assert.Equal("SBR", opt.RecordType);
            Assert.Equal("P", opt.PayerResponsibilitySequenceNumberCode);
            Assert.Equal("18", opt.IndividualRelationshipCode);
            Assert.Equal("", opt.ReferenceIdentification);
            Assert.Equal("BLUE CROSS BLUE SHIELD HEALTHY", opt.Name);
            Assert.Equal("", opt.InsuranceTypeCode);
            Assert.Equal("", opt.CoordinationOfBenefitsCode);
            Assert.Equal("", opt.YesNoConditionOrResponseCode);
            Assert.Equal("", opt.EmploymentStatusCode);
            Assert.Equal("MC", opt.ClaimFilingIndicatorCode);
        }

        [Fact]
        public void TestThat_Sv1ShortParses()
        {
            var sut = Parser.ParseText(Sv1ShortLine, _factory);
            var opt = sut.FirstOrDefault() as Segments.SV1;
            Assert.NotNull(opt);

            Assert.Equal("SV1", opt.RecordType);
            Assert.Equal("HC:99239", opt.CompositeMedicalProcedureIdentifier);
            Assert.Equal("682", opt.MonetaryAmount);
            Assert.Equal("UN", opt.UnitOrBasisForMeasurementCode);
            Assert.Equal("1", opt.Quantity);
            Assert.Equal("", opt.FacilityCodeValue);
            Assert.Equal("", opt.ServiceTypeCode);
            Assert.Equal("1", opt.CompositeDiagnosisCodePointer);
            Assert.Equal("", opt.MonetaryAmount2);
            Assert.Equal("", opt.YesNoConditionOrResponseCode);
            Assert.Equal("", opt.MultipleProcedureCode);
            Assert.Equal("", opt.YesNoConditionOrResponseCode2);
            Assert.Equal("", opt.YesNoConditionOrResponseCode3);
            Assert.Equal("", opt.ReviewCode);
            Assert.Equal("", opt.NationalOrLocalAssignedReviewValue);
            Assert.Equal("", opt.CopayStatusCode);
            Assert.Equal("", opt.HealthCareProfessionalShortageAreaCode);
            Assert.Equal("", opt.ReferenceIdentification);
            Assert.Equal("", opt.PostalCode);
            Assert.Equal("", opt.MonetaryAmount3);
            Assert.Equal("", opt.LevelOfCareCode);
            Assert.Equal("", opt.ProviderAgreementCode);
        }

        [Fact]
        public void TestThat_Sv1LongParses()
        {
            var sut = Parser.ParseText(Sv1LongLine, _factory);
            var opt = sut.FirstOrDefault() as Segments.SV1;
            Assert.NotNull(opt);

            Assert.Equal("SV1", opt.RecordType);
            Assert.Equal("HC:99285:25:GC:FS", opt.CompositeMedicalProcedureIdentifier);
            Assert.Equal("1286", opt.MonetaryAmount);
            Assert.Equal("UN", opt.UnitOrBasisForMeasurementCode);
            Assert.Equal("1", opt.Quantity);
            Assert.Equal("", opt.FacilityCodeValue);
            Assert.Equal("", opt.ServiceTypeCode);
            Assert.Equal("1:2:3:4", opt.CompositeDiagnosisCodePointer);
            Assert.Equal("", opt.MonetaryAmount2);
            Assert.Equal("Y", opt.YesNoConditionOrResponseCode);
            Assert.Equal("", opt.MultipleProcedureCode);
            Assert.Equal("", opt.YesNoConditionOrResponseCode2);
            Assert.Equal("", opt.YesNoConditionOrResponseCode3);
            Assert.Equal("", opt.ReviewCode);
            Assert.Equal("", opt.NationalOrLocalAssignedReviewValue);
            Assert.Equal("", opt.CopayStatusCode);
            Assert.Equal("", opt.HealthCareProfessionalShortageAreaCode);
            Assert.Equal("", opt.ReferenceIdentification);
            Assert.Equal("", opt.PostalCode);
            Assert.Equal("", opt.MonetaryAmount3);
            Assert.Equal("", opt.LevelOfCareCode);
            Assert.Equal("", opt.ProviderAgreementCode);
        }

        [Fact]
        public void TestThat_SvdShortParses()
        {
            var sut = Parser.ParseText(SvdShortLine, _factory);
            var opt = sut.FirstOrDefault() as Segments.SVD;
            Assert.NotNull(opt);

            //SVD*PAPER*0*HC:99285:GC**1~
            Assert.Equal("SVD", opt.RecordType);
            Assert.Equal("PAPER", opt.IdentificationCode);
            Assert.Equal("0", opt.MonetaryAmount);
            Assert.Equal("HC:99285:GC", opt.CompositeMedicalProcedureIdentifier);
            Assert.Equal("", opt.ProductServiceId);
            Assert.Equal("1", opt.Quantity);
            Assert.Equal("", opt.AssignedNumber);
        }

        [Fact]
        public void TestThat_SvdLongParses()
        {
            var sut = Parser.ParseText(SvdLongLine, _factory);
            var opt = sut.FirstOrDefault() as Segments.SVD;
            Assert.NotNull(opt);

            //SVD*GEB00074P*204.83*HC:99284**1~
            Assert.Equal("SVD", opt.RecordType);
            Assert.Equal("GEB00074P", opt.IdentificationCode);
            Assert.Equal("204.83", opt.MonetaryAmount);
            Assert.Equal("HC:99284", opt.CompositeMedicalProcedureIdentifier);
            Assert.Equal("", opt.ProductServiceId);
            Assert.Equal("1", opt.Quantity);
            Assert.Equal("", opt.AssignedNumber);
        }
    }
}
