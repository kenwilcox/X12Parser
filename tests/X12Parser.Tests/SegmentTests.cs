using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace X12Parser.Tests
{
    public class SegmentTests
    {
        private readonly List<X12> _sut;

        public SegmentTests()
        {
            var factory = new X12Factory(typeof(X12), Assembly.GetExecutingAssembly());
            var data = "FOO*~NoF**~FOO*~NoF~";
            _sut = Parser.ParseText(data, factory);
        }

        [Fact]
        public void TestThat_SegmentCount_IsExpected()
        {
            // We should have four segments
            Assert.Equal(4, _sut.Count);
        }

        [Fact]
        public void TestThat_RecordTypes_AreCorrect()
        {
            // Record Types should be the segment name
            Assert.Equal("FOO", _sut[0].RecordType);
            Assert.Equal("NoF", _sut[1].RecordType);
            Assert.Equal("FOO", _sut[2].RecordType);
            Assert.Equal("NoF", _sut[3].RecordType);
        }

        [Fact]
        public void TestThat_ActualObjects_AreCorrect()
        {
            // Actual object could be different, NF currently isn't defined
            Assert.Equal(typeof(FOO), _sut[0].GetType());
            Assert.Equal(typeof(X12), _sut[1].GetType());
            Assert.Equal(typeof(FOO), _sut[2].GetType());
            Assert.Equal(typeof(X12), _sut[3].GetType());
        }
    }
}
