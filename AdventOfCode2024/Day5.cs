using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2024;

internal class Day5
{
    private static (Dictionary<int, List<int>> Rules, List<List<int>> Updates) Manual()
    {
        var data = File.ReadAllLines(@"..\..\..\Day5Input.txt");

        var ruleData = data.TakeWhile(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => line.Split("|"))
            .Select(ids => (int.Parse(ids[0]), int.Parse(ids[1])));
        var rules = new Dictionary<int, List<int>>();
        foreach (var (x, y) in ruleData)
        {
            if (rules.ContainsKey(x))
                rules[x].Add(y);
            else
                rules[x] = new List<int> { y };
        }

        var updates = data.SkipWhile(line => !string.IsNullOrWhiteSpace(line))
            .Skip(1).Select(line => line.Split(","))
            .Select(pages => pages.Select(page => int.Parse(page)).ToList()).ToList();

        return (rules, updates);
    }

    private static int SumMiddles(IEnumerable<List<int>> updates) => updates.Sum(update => update[update.Count / 2]);

    private static IEnumerable<List<int>> GetUpdates(bool correctOrder = true)
    {
        var manual = Manual();
        return manual.Updates.Where(update =>
        {
            var index = 0;
            var correct = true;
            while (correct && index < update.Count)
            {
                var before = update.Take(index);
                var rules = manual.Rules[update[index]];
                correct = !before.Intersect(rules).Any();
                index++;
            }
            return correctOrder ? correct : !correct;
        });
    }

    public static int ExecutePart1()
    {
        var tmp = GetUpdates();
        return SumMiddles(GetUpdates());
    }

    public static int ExecutePart2()
    {
        var rules = Manual().Rules;
        var incorrect = GetUpdates(false).ToList();
        foreach (var update in incorrect)
        {
            var index = 0;
            while (index < update.Count)
            {
                var before = update.Take(index);
                var pageRules = rules[update[index]].Intersect(update);
                var incorrectPages = before.Intersect(pageRules).ToList();
                if (incorrectPages.Any())
                {
                    foreach (var page in incorrectPages)
                    {
                        update.Remove(page);
                        update.Insert(index, page);
                    }
                }
                index++;
            }
        }

        return SumMiddles(incorrect);
    }
}