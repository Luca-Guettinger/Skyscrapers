using System.Collections;

namespace Skyscrapers.Code.Iterators;

public class SkyScraperColumnEntryList : IEnumerable<Position>
{
    private readonly Skyscraper _skyscraper;
    private readonly int _x;
    
    public int StartClue { get; }
    public int EndClue { get; }
    
    public SkyScraperColumnEntryList(Skyscraper skyscraper, int x, int startClue, int endClue)
    {
        StartClue = startClue;
        EndClue = endClue;
        _skyscraper = skyscraper;
        _x = x;
        foreach (var position in this)
        {
            position.ColumnEntry = this;
        }
    }

    public IEnumerator<Position> GetEnumerator()
    {
        for (byte i = 0; i < _skyscraper.Size; i++)
        {
            yield return _skyscraper.Field[i, _x];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}