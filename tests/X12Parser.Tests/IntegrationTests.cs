using System.Reflection;
using Xunit;

namespace X12Parser.Tests
{
    public class IntegrationTests
    {
        private const string MixedFieldSeparators = "FOO*1*2*3~FOO|1|2|3~";
        private readonly X12Factory _factory;

        public IntegrationTests()
        {
            _factory = new X12Factory(typeof(X12), Assembly.GetExecutingAssembly());
        }

        [Fact]
        public void TestThat_ParserParsesLinesCorrectly()
        {
            var sut = Parser.ParseText(MixedFieldSeparators, _factory);
            Assert.Equal(2, sut.Count);

            foreach (var item in sut)
            {
                if (!(item is FOO foo)) continue;
                Assert.Equal("1", foo.FieldOne);
                Assert.Equal("2", foo.OptionalOne);
                Assert.Equal("3", foo.OptionalTwo);
            }
        }
    }
}