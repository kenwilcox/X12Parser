using System.Collections.Generic;
using System.Linq;
using X12Parser;

namespace X12Tester
{
    [SegmentName("Provider Level Adjustment")]
    public class PLB : X12Parser.Segments.PLB
    {
        public int MaxLength => new int[6] {AdjustmentReasonCode1.Length, AdjustmentReasonCode2.Length,
                        AdjustmentReasonCode3.Length, AdjustmentReasonCode4.Length,
                        AdjustmentReasonCode5.Length, AdjustmentReasonCode6.Length }.Max();

        public IEnumerable<string> Sections => new string[6] {
            AdjustmentReasonCode1, AdjustmentReasonCode2, AdjustmentReasonCode3,
            AdjustmentReasonCode4,AdjustmentReasonCode5, AdjustmentReasonCode6
        }.Where(x => !string.IsNullOrEmpty(x));
    }
}

/*
FB - Forwarding Balance
IR - Internal Revenue Service Withholding
L6 - Interest Owed
PI - Periodic Interim Payment
WO - Overpayment Recover
    negitive (Identified), positive(Withheld)
C5 - Temporary Allowance
CS - Adjustment
72 - Authorized Return
B2 - Rebate

L3 - Penalty Sanctions
*/
