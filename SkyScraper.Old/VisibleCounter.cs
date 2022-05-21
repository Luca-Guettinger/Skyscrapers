using System.ComponentModel;

namespace SkyScraper.Old;

public static class VisibleCounter
{
    public static int CountVisible(int[] skyscrapers, Direction direction)
    {
        var highest = 0;
        var visibleCount = 0;

        IEnumerable<int> scraper;

        switch (direction)
        {
            case Direction.FromLeftToRight:
                scraper = skyscrapers;
                break;
            case Direction.FromRightToLeft:
                scraper = skyscrapers.Reverse();
                break;
            default:
                throw new InvalidEnumArgumentException();
        }

        foreach (var skyscraper in scraper)
        {
            if (skyscraper <= highest) continue;

            highest = skyscraper;
            visibleCount++;
        }
        return visibleCount;
    }
}