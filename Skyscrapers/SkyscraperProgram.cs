namespace Skyscrapers;

public class SkyscraperProgram
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
        
        
        
        // Start your coding here...
        return Array.Empty<int[]>();
    }
}