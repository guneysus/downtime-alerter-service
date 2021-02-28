using System;
using System.Text.RegularExpressions;

namespace IntervalParserLib
{
    public static class IntervalParser
    {
        public static TimeSpan parseInterval(string input)
        {
            Regex rgx = new Regex(@"(?<interval>[1-9]+([0-9]+)?)+(?=(\s+)?((m(in(ute)?)?)(s?)))");
            MatchCollection m = rgx.Matches(input);

            if (m.Count == 0)
                throw new FormatException($"{input} is not a valid input for interval");

            var minutes = int.Parse(m[0].Groups["interval"].Value);
            var result = TimeSpan.FromMinutes(minutes);
            return result;
        }
    }


}
