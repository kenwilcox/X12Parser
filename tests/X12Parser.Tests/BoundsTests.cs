using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Xunit;

namespace X12Parser.Tests
{
    public class BoundsTests
    {
        private readonly X12Factory _factory;

        public BoundsTests()
        {
            _factory = new X12Factory(typeof(X12), Assembly.GetExecutingAssembly());
        }

        [Fact]
        public void TestThat_WeCheckPropertyBounds()
        {
            var data = "QTY*SOMETOWN*SOMESTATE*99999***";
            Assert.Throws<FormatException>(() => Parser.ParseText(data, _factory, boundsChecks: true));
        }

        [Fact]
        public void TestThat_WeCanIgnorePropertyBounds()
        {
            var data = "QTY*SOMETOWN*SOMESTATE*99999***";
            var items = Parser.ParseText(data, _factory);
            var sut = items.FirstOrDefault(x => x is QTY) as QTY;
            Assert.Equal("SOMETOWN", sut.City);
            Assert.Equal("SOMESTATE", sut.State);
            Assert.Equal("99999", sut.Zip);
        }
    }
}
