using System.Collections;

namespace Skyscrapers;

public class SkyscraperSolver
{
    
    public static int[][] SolvePuzzle(int[] clues)
    {
        if (clues.Length % 4 != 0)
        {
            throw new InvalidOperationException("clues must be a multiple of 4");
        }
        
        var fieldSize = clues.Length / 4;
        
        if (fieldSize > byte.MaxValue)
        {
            throw new InvalidOperationException("field size must be less than " + byte.MaxValue);
        }

        var field = new byte[fieldSize][];

        foreach (var row in field)
        {
            for (var x = 0; x < field.Length; x++)
            {
                row[x] = 0;
            }
            row[0] = 1;
        }
        
        

        return Array.Empty<int[]>();
    }
}