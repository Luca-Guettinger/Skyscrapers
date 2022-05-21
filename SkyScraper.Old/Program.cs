using SkyScraper.Old.Clue;

namespace SkyScraper.Old;

public class Skyscrapers
{
    public static void Main(string[] args)
    {
    }


    public static int[][] SolvePuzzle(int[] clues)
    {
        var clueManager = new ClueManager(clues);
        var result = clueManager.CombineAllClues().ToArray();
        if (result.Length != 1)
            throw new ArgumentException("Not solvable");


        var field = result.First();
        for (int i = 0; i < 10; i++)
        {
            field = field.autoComplete();
        }
        var output = new List<int[]>();
        for (var i = 0; i < field.Size; i++)
        {
            output.Add(field.GetRow(i).Values);
        }
        return output.ToArray();
    }
}