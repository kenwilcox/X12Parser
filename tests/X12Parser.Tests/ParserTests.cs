﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace X12Parser.Tests
{
    public class ParserTests
    {
        private X12Factory _factory;
        private List<X12> _sut;
        private List<PropCache> _cache;

        //private const string _isa = @"ISA*00*AAAAAAAAAA*11*BBBBBBBBBB*22*123456789012345*33*987654321987654*CCCCCC*DDDD*^*EEEEE*FFFFFFFFF*G*T*:~";
        private const string _isaProperties = @"ISA*AA*BBBBBBBBBB*CC*DDDDDDDDDD*EE*FFFFFFFFFFFFFFF*GG*HHHHHHHHHHHHHHH*IIIIII*JJJJ*K*LLLLL*MMMMMMMMM*N*O*P~";
        private const string _optionalFoo = @"FOO*REQUIRED*~";
        private const string _onlyOptional = @"OPT*****HERE~";
        private const string _minTest = "MIN*123456789~";
        private const string _maxTest = "MAX*12345678901234567890~";
        private const string _isaWithCrLf = @"ISA*00*          *00*          *ZZ*610442         *ZZ*0FB            *190326*2111*|*00
001*
000000002*0*P*>~";

        public ParserTests()
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

        [Fact]
        public void TestThat_OptionalFields_WorkAsExpected()
        {
            var sut = Parser.ParseText(_optionalFoo, _factory);
            var foo = sut.FirstOrDefault() as FOO;
            Assert.Equal("FOO", foo.RecordType);
            Assert.Equal("REQUIRED", foo.FieldOne);
            Assert.Equal("", foo.OptionalOne);
            Assert.Equal("", foo.OptionalTwo);
        }

        [Fact]
        public void TestThat_OnlyOptionalFields_WorkAsExpected()
        {
            var sut = Parser.ParseText(_onlyOptional, _factory);
            var opt = sut.FirstOrDefault() as OPT;
            Assert.Equal("OPT", opt.RecordType);
            Assert.Equal("HERE", opt.Here);
        }

        [Fact]
        public void TestThat_MinLength_IsChecked()
        {
            Assert.Throws<ArgumentException>(() => Parser.ParseText(_minTest, _factory));
        }

        [Fact]
        public void TestThat_MaxLength_IsChecked()
        {
            Assert.Throws<ArgumentException>(() => Parser.ParseText(_maxTest, _factory));
        }

        [Fact]
        public void TestThat_ThisHandlesCrLf()
        {
            // The library does not handle this anymore. There were way too many unexpected
            // characters in the 835 files I've parsed.
            var sut = Parser.ParseText(_isaWithCrLf.Replace("\r\n", "").Replace("\n", ""), _factory);
            var opt = sut.FirstOrDefault() as Segments.ISA;
            Assert.Equal("ISA", opt.RecordType);
            // new line in value
            Assert.Equal("00001", opt.InterchangeControlVersionNumber);
            // new line before segment
            Assert.Equal("000000002", opt.InterchangeControlNumber);
        }
    }
}
