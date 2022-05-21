using System.Collections;

namespace Skyscrapers.Code.Iterators;

public class SkyScraperRowEntryList : IEnumerable<Position>
{
    private readonly Skyscraper _skyscraper;
    private readonly byte _y;
    public int StartClue { get; }
    public int EndClue { get; }

    public SkyScraperRowEntryList(Skyscraper skyscraper, byte y, int startClue, int endClue)
    {
        StartClue = startClue;
        EndClue = endClue;
        _skyscraper = skyscraper;
        _y = y;
        
        foreach (var position in this)
        {
            position.RowEntry = this;
        }
    }
    
    
    public IEnumerator<Position> GetEnumerator()
    {
        for (byte i = 0; i < _skyscraper.Size; i++)
        {
            yield return _skyscraper[_y, i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}