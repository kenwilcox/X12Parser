using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AnesRemit.Tests
{
    public class CptTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("01400", "ANESTH KNEE JOINT SURGERY")]
        [InlineData("64447", "n block inj fem single")]
        public void TestThat_CptHoldsValues(string code, string description)
        {
            var sut = new Cpt {Code = code, Description = description};
            Assert.Equal(code, sut.Code);
            Assert.Equal(description.ToUpper(), sut.Description);
        }
    }
}
