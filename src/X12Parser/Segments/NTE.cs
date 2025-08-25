namespace X12Parser.Segments
{
    [SegmentName("Note or Special Instruction")]
    public class NTE : X12
    {
        [Segment(1, 3, 3, true)]
        public string NoteReferenceCode { get; set; }
        [Segment(2, 1, 80)]
        public string Description { get; set; }
    }
}
