﻿using System;
using System.Text.RegularExpressions;

public static class IntervalParser
{
    public static TimeSpan parseInterval(string input)
    {
        Regex rgx = new Regex(@"(?<interval>[1-9]+([0-9]+)?)+(?=(\s+)?((m(in(ute)?)?)(s?)))");
        MatchCollection m = rgx.Matches(input);
        // m.Count == 1
        // m[0].Success

        if (m.Count == 0)
            throw new FormatException($"{input} is not a valid input for interval");

        var minutes = int.Parse(m[0].Groups["interval"].Value);
        var result = TimeSpan.FromMinutes(minutes);
        return result;
    }
}
