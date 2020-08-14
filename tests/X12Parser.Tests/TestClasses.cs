namespace X12Parser.Tests
{
    // These classes are only used for the test cases

    internal class FOO : X12
    {
        [Segment(1)]
        public string FieldOne { get; set; }
        [Segment(2, true)]
        public string OptionalOne { get; set; }
        [Segment(3, true)]
        public string OptionalTwo { get; set; }
    }

    internal class OPT : X12
    {
        [Segment(1, true)]
        public string OptionalOne { get; set; }
        [Segment(2, true)]
        public string OptionalTwo { get; set; }
        [Segment(3, true)]
        public string OptionalThree { get; set; }
        [Segment(4, true)]
        public string OptionalFour { get; set; }
        [Segment(5, true)]
        public string Here { get; set; }
    }

    internal class MIN : X12
    {
        [Segment(1, 10, 10)]
        public string Exception { get; set; }
    }

    internal class MAX : X12
    {
        [Segment(1, 1, 10)]
        public string Exception { get; set; }
    }

    internal class QTY : X12
    {
        [Segment(1, true)]
        public string City { get; set; }
        [Segment(2, true)]
        public string State { get; set; }
        [Segment(3, true)]
        public string Zip { get; set; }
    }
}
