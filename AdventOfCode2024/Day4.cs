using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2024;

internal class Day4
{
    private static string[] Words()
    {
        return File.ReadAllLines(@"..\..\..\Day4Input.txt");
    }

    public static int ExecutePart1()
    {
        var words = Words();
        var wordCount = 0;
        var rowCount = words.Length;
        var columnCount = words[0].Length;

        for (int row = 0; row < rowCount; row++)
        {
            for (int column = 0; column < columnCount; column++)
            {
                //Word start?
                if (words[row][column] == 'X')
                {
                    //East
                    if (column <= columnCount - 4)
                        if (words[row].Substring(column + 1, 3) == "MAS")
                            wordCount++;

                    //West
                    if (column >= 3)
                        if (words[row].Substring(column - 3, 3) == "SAM")
                            wordCount++;

                    //North
                    if (row >= 3)
                        if (words[row - 1][column] == 'M' && words[row - 2][column] == 'A' && words[row - 3][column] == 'S')
                            wordCount++;

                    //South
                    if (row <= rowCount - 4)
                        if (words[row + 1][column] == 'M' && words[row + 2][column] == 'A' && words[row + 3][column] == 'S')
                            wordCount++;

                    //NorthEast
                    if (row >= 3 && column <= columnCount - 4)
                        if (words[row - 1][column + 1] == 'M' && words[row - 2][column + 2] == 'A' && words[row - 3][column + 3] == 'S')
                            wordCount++;

                    //NorthWest
                    if (row >= 3 && column >= 3)
                        if (words[row - 1][column - 1] == 'M' && words[row - 2][column - 2] == 'A' && words[row - 3][column - 3] == 'S')
                            wordCount++;

                    //SouthEast
                    if (row <= rowCount - 4 && column <= columnCount - 4)
                        if (words[row + 1][column + 1] == 'M' && words[row + 2][column + 2] == 'A' && words[row + 3][column + 3] == 'S')
                            wordCount++;

                    //SouthWest
                    if (row <= rowCount - 4 && column >= 3)
                        if (words[row + 1][column - 1] == 'M' && words[row + 2][column - 2] == 'A' && words[row + 3][column - 3] == 'S')
                            wordCount++;
                }
            }
        }

        return wordCount;
    }

    public static int ExecutePart2()
    {
        var words = Words();
        var xmasCount = 0;
        var rowCount = words.Length - 2;
        var columnCount = words[0].Length - 2;

        for (int row = 0; row < rowCount; row++)
        {
            for (int column = 0; column < columnCount; column++)
            {
                //X-MAS Start?
                if (words[row][column] is 'M' or 'S' || words[row][column + 2] is 'M' or 'S')
                {
                    var xmas = string.Concat(words[row][column], words[row + 1][column + 1], words[row + 2][column + 2]);
                    var xmas2 = string.Concat(words[row][column + 2], words[row + 1][column + 1], words[row + 2][column]);
                    if (xmas is "MAS" or "SAM" && xmas2 is "MAS" or "SAM")
                        xmasCount++;
                }
            }
        }

        return xmasCount;
    }
}