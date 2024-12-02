namespace AdventOfCode2024;

internal class Day1
{
    private static (IEnumerable<int> Left, IEnumerable<int> Right) LocationIds()
    {
        var data = File.ReadAllLines(@"..\..\..\Day1Input.txt")
            .Select(line => line.Split("   "))
            .Select(ids => (int.Parse(ids[0]), int.Parse(ids[1])));

        return (data.Select(ids => ids.Item1), data.Select(ids => ids.Item2));
    }

    public static int ExecutePart1()
    {
        var ids = LocationIds();
        var sortedLeft = ids.Left.Order();
        var sortedRight = ids.Right.Order();

        return sortedLeft.Zip(sortedRight, (l, r) => Math.Abs(l - r)).Sum();
    }

    public static int ExecutePart2()
    {
        var ids = LocationIds();

        return ids.Left.Select(leftId => leftId * ids.Right.Count(rightId => rightId == leftId)).Sum();
    }
}