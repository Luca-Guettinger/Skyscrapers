using Skyscrapers.Code.Iterators;

namespace Skyscrapers.Code;

public class Position
{
    public SkyScraperColumnEntryList ColumnEntry { get; internal set; }
    public SkyScraperRowEntryList RowEntry { get; internal  set; }
    public List<byte> AllowedHeights { get; }
    
    public Position(params byte[] values)
    {
        AllowedHeights = new List<byte>(values);
    }

}