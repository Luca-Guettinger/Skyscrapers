namespace SkyScraper.Old;

public abstract class Line
{
    private readonly int[] _cells;

    public int Length => _cells.Length;

    public int[] Values
    {
        get
        {
            var arr = new int[Length];
            _cells.CopyTo(arr, 0);
            return arr;
        }
    }

    protected Line(int[] line)
    {
        _cells = line;
    }

    public int this[int x] => _cells[x];

    public bool ContainsDuplicates()
    {
        return ContainsDuplicates(_cells);
    }

    public static bool ContainsDuplicates(int[] cells)
    {
        var used = new bool[cells.Length + 1];

        foreach (var cell in cells)
        {
            if (cell != 0 && used[cell])
                return true;

            used[cell] = true;
        }
        return false;
    }
}