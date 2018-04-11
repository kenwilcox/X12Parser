using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using X12Parser.Segments;
using Xunit;

namespace X12Parser.Tests
{
    public class ISATests
    {
        private X12Factory _factory;
        private List<X12> _sut;
        private List<PropCache> _cache;

        //private const string _isa = @"ISA*00*AAAAAAAAAA*11*BBBBBBBBBB*22*123456789012345*33*987654321987654*CCCCCC*DDDD*^*EEEEE*FFFFFFFFF*G*T*:~";
        private const string _isaProperties = @"ISA*AA*BBBBBBBBBB*CC*DDDDDDDDDD*EE*FFFFFFFFFFFFFFF*GG*HHHHHHHHHHHHHHH*IIIIII*JJJJ*K*LLLLL*MMMMMMMMM*N*O*P~";

        public ISATests()
        {
            // This is only necessary in unit tests because the test runners are typically unmanaged code
            // which makes the call to GetEntryAssembly return null, this way we are handing
            // "ourselves" to the factory instead of the factory trying to find us.
            _factory = new X12Factory(typeof(X12), Assembly.GetExecutingAssembly());
            _sut = Parser.ParseText(_isaProperties, _factory);
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
                var c = Convert.ToChar(startAt);
                var test = c.ToString().PadLeft(info.Segment.MaxLength.Value, c);
                Assert.Equal(test, info.Property.GetValue(sut));
                startAt++;
            }
            // position length -1 should be :
        }
    }
}
