using System.Collections;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using Skyscrapers.Code.Iterators;

namespace Skyscrapers.Code;

public class Skyscraper
{
    internal readonly Position[,] Field;
    public SkyScraperRowEntryList[] Rows { get; private set; }
    public SkyScraperColumnEntryList[] Columns { get; private set; }
    public Position this[byte x, byte y] => Field[x, y];
    public int Size => Field.GetLength(0);

    public Skyscraper(int size, IReadOnlyList<int> clues)
    {
        if (clues.Count < size * 4)
        {
            throw new ArgumentException("Clues count is less than size of field");
        }
        
        Field = new Position[size, size];
        for (var x = 0; x < Size; x++)
        {
            for (var y = 0; y < Size; y++)
            {
                Field[x, y] = new Position();
            }
        }
        SetupRowsAndColumns(clues);
    }

    public Skyscraper(Position[,] field, IReadOnlyList<int> clues)
    {
        if (clues.Count < field.GetLength(0) * 4)
        {
            throw new ArgumentException("Clues count is less than size of field");
        }
        
        this.Field = field;
        SetupRowsAndColumns(clues);
    }

    private void SetupRowsAndColumns(IReadOnlyList<int> clues)
    {
        Rows = new SkyScraperRowEntryList[Size];
        Columns = new SkyScraperColumnEntryList[Size];

        var otherSide = (Size * 3 - 1);
        for (byte i = 0; i < Size; i++)
        {
            Rows[i] = new SkyScraperRowEntryList(this, i, clues[Size + otherSide - i], clues[Size + i]);
            Columns[i] = new SkyScraperColumnEntryList(this, i, clues[i], clues[otherSide - i]);
        }
    }

    public void ApplyAllowedOptions()
    {
        var allValues = Enumerable.Range(1, Size).ToArray();
        var permutations = allValues.GetPermutationsByVisibilityHeight().ToArray();
        
        foreach (var row in Rows)
        {
            var positions = row.ToArray();

            AllowEveryNumber(positions);
            
            for (var i = 0; i < positions.Length; i++)
            {

                var allowedNumbers = permutations.Where((tuple, i1) => tuple.visibleStart < row.StartClue).SelectMany((tuple, i1) =>
                {
                    var enumerable = tuple.permutation.Take(i + 1);
                    return enumerable;
                }).ToHashSet();

                var toRemove = allValues.Where(i1 => !allowedNumbers.Contains(i1));
                
                
                var pos = positions[i];

                pos.AllowedHeights.RemoveAll(b => toRemove.Contains(b));
            }
        }
    }

    private void AllowEveryNumber(Position[] positions)
    {
        foreach (var position in positions)
        {
            position.AllowedHeights.Clear();
            for (byte i = 0; i < Size; i++)
            {
                position.AllowedHeights.Add((byte)(i + 1));
            }
        }
    }
}