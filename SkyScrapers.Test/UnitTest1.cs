using NUnit.Framework;

namespace SkyScrapers.Test;

public class Tests
{
    [Test]
    public void SolvePuzzle1()
    {
        var clues = new[]{ 3, 2, 2, 3, 2, 1,
            1, 2, 3, 3, 2, 2,
            5, 1, 2, 2, 4, 3,
            3, 2, 1, 2, 2, 4};

        var expected = new[]{new []{ 2, 1, 4, 3, 5, 6}, 
            new []{ 1, 6, 3, 2, 4, 5}, 
            new []{ 4, 3, 6, 5, 1, 2}, 
            new []{ 6, 5, 2, 1, 3, 4}, 
            new []{ 5, 4, 1, 6, 2, 3}, 
            new []{ 3, 2, 5, 4, 6, 1 }};

        
        var actual = Skyscrapers.Code.Skyscrapers.SolvePuzzle(clues);
        CollectionAssert.AreEqual(expected, actual);
    }
}