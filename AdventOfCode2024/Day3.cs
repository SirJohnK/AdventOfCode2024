using System.Text.RegularExpressions;

namespace AdventOfCode2024;

internal class Day3
{
    private static string Program()
    {
        return File.ReadAllText(@"..\..\..\Day3Input.txt");
    }

    public static int ExecutePart1()
    {
        var data = Program();
        var matches = Regex.Matches(data, @"mul\((?<First>\d{1,3}),(?<Second>\d{1,3})\)");
        return matches.Select(match => int.Parse(match.Groups["First"].Value) * int.Parse(match.Groups["Second"].Value)).Sum();
    }

    public static int ExecutePart2()
    {
        var sum = 0;
        var process = true;
        var data = Program();
        var matches = Regex.Matches(data, @"mul\((?<First>\d{1,3}),(?<Second>\d{1,3})\)|don't\(\)|do\(\)");
        foreach (Match match in matches)
        {
            if (process && match.Value.StartsWith("mul"))
            {
                sum += int.Parse(match.Groups["First"].Value) * int.Parse(match.Groups["Second"].Value);
            }
            else
            {
                process = match.Value.StartsWith("do") && !match.Value.StartsWith("don't");
            }
        }

        return sum;
    }
}