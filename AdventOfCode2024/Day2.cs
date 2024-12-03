namespace AdventOfCode2024;

internal class Day2
{
    private static IEnumerable<IEnumerable<int>> Reports()
    {
        return File.ReadAllLines(@"..\..\..\Day2Input.txt").Select(line => line.Split(" ").Select(value => int.Parse(value)));
    }

    private static bool IsReportSafe(IEnumerable<int> report, bool useProblemDampener = false)
    {
        //Attempt to solve the problem without the dampener
        var increasing = report.First() < report.Skip(1).First();
        var isSafeReport = report.SkipLast(1).Zip(report.Skip(1), (a, b) => (increasing ? a < b : a > b) && (Math.Abs(a - b) is >= 1 and <= 3)).All(isSafe => isSafe);

        //If the dampener is enabled and the report is not safe, apply dampener and try again
        if (!isSafeReport && useProblemDampener)
        {
            var removeIndex = 0;
            while (removeIndex < report.Count() && !isSafeReport)
            {
                var newReport = report.Where((value, index) => index != removeIndex);
                increasing = newReport.First() < newReport.Skip(1).First();
                isSafeReport = newReport.SkipLast(1).Zip(newReport.Skip(1), (a, b) => (increasing ? a < b : a > b) && (Math.Abs(a - b) is >= 1 and <= 3)).All(isSafe => isSafe);
                removeIndex++;
            }
        }

        //Return status if report is safe
        return isSafeReport;
    }

    public static int ExecutePart1() => Reports().Where(report => IsReportSafe(report)).Count();

    public static int ExecutePart2() => Reports().Where(report => IsReportSafe(report, true)).Count();
}