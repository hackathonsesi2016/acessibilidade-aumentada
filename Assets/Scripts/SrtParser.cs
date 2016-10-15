using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;
using System.Linq;

public static class SrtParser
{
    public const string blockRegexPattern = @"[\r\n](?=\d)";
    public const string timeRegexPattern = @"\d+\:\d+\:\d+(\,\d+)?";

    public static string[] GetSubtitleBlocks(string rawSrt)
    {
        string[] subBlocks = Regex.Split(rawSrt, blockRegexPattern);

        return subBlocks;
    }

    public static double[] GetSubtitleTimes(string subtitleBlock)
    {
        string[] regexMatches = Regex.Matches(subtitleBlock, timeRegexPattern).Cast<Match>().Select(match => match.Value).ToArray();

        double startTime, endTime;
        double[] times = new double[2];

        startTime = TimeSpan.Parse(regexMatches[0]).TotalSeconds;
        endTime = TimeSpan.Parse(regexMatches[1]).TotalSeconds;

        times[0] = startTime;
        times[1] = endTime;

        return times;
    }

    public static string GetSubtitleText(string subtitleBlock)
    {
        string text = subtitleBlock.Split('\n')[1];

        return text;
    }
}
