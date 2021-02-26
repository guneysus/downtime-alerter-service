using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using static IntervalParser;

namespace IntervalParserLib.Tests
{
    public class IntervalParserTests
    {
        [Theory]
        [ClassData(typeof(IntervalParserValidDataGenerator))]
        public void Valid_input_tests(string input, TimeSpan expected)
        {
            Assert.Equal(expected, parseInterval(input));
        }

        [Theory]
        [ClassData(typeof(IntervalParserInvalidDataGenerator))]
        public void Invalid_input_tests(string input)
        {
            Assert.Throws<System.FormatException>(() => parseInterval(input));
        }
    }

    public class IntervalParserValidDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] {"1m", TimeSpan.FromMinutes(1)},
            new object[] {"1 m", TimeSpan.FromMinutes(1)},
            new object[] { "1 min", TimeSpan.FromMinutes(1)},
            new object[] { "1 mins", TimeSpan.FromMinutes(1)},
            new object[] { "1 minute", TimeSpan.FromMinutes(1)},
            new object[] { "1 minutes", TimeSpan.FromMinutes(1)},

            new object[] {"10m", TimeSpan.FromMinutes(10)},
            new object[] {"10 m", TimeSpan.FromMinutes(10)},
            new object[] { "10 min", TimeSpan.FromMinutes(10)},
            new object[] { "10 mins", TimeSpan.FromMinutes(10)},
            new object[] { "10 minute", TimeSpan.FromMinutes(10)},
            new object[] { "10 minutes", TimeSpan.FromMinutes(10)},

            new object[] {"100m", TimeSpan.FromMinutes(100)},
            new object[] {"100 m", TimeSpan.FromMinutes(100)},
            new object[] { "100 min", TimeSpan.FromMinutes(100)},
            new object[] { "100 mins", TimeSpan.FromMinutes(100)},
            new object[] { "100 minute", TimeSpan.FromMinutes(100)},
            new object[] { "100 minutes", TimeSpan.FromMinutes(100)},
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class IntervalParserInvalidDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] {"0m"},
            new object[] {"1"},
            new object[] {"10"},
            new object[] {"100"},
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
