namespace SkyScraper.Old.Clue;

public class ClueManager
{
    private readonly int _oneSide;
    public static int[] Array;
    private readonly int[] _clues;
    private bool _needAutoComplete = false;

    public ClueManager(int[] clues)
    {
        this._clues = clues;
        _oneSide = _clues.Length / 4;
        Array = new int[_oneSide];
        for (var i = 0; i < _oneSide; i++)
        {
            Array[i] = i + 1;
        }
    }

    public Clue[] GetClues()
    {
        var columnClueTop = _clues.ToList().GetRange(0, _oneSide);
        var rowClueRight = _clues.ToList().GetRange(_oneSide, _oneSide);
        var columnClueBottom = _clues.ToList().GetRange(2 * _oneSide, _oneSide).ToArray().Reverse().ToArray();
        var rowClueLeft = _clues.ToList().GetRange(3 * _oneSide, _oneSide).ToArray().Reverse().ToArray();

        var clueList = new List<Clue>();
        for (var i = 0; i < _oneSide; i++)
        {
            if (columnClueTop[i] != 0 || columnClueBottom[i] != 0)
                clueList.Add(new ColumnClue(_oneSide, columnClueTop[i], columnClueBottom[i], i));
            if (rowClueLeft[i] != 0 || rowClueRight[i] != 0)
                clueList.Add(new RowClue(_oneSide, rowClueLeft[i], rowClueRight[i], i));
        }
        var cClues = 0;
        var rClues = 0;
        foreach (var clue in clueList)
        {
            if (clue is RowClue)
                rClues++;
            if (clue is ColumnClue)
                cClues++;
        }
        if (cClues < _oneSide && rClues < _oneSide)
            _needAutoComplete = true;
        return clueList.OrderBy(o => o.Permutations.Length).ToArray();
        //OrderBy(o => o.Permutations.Length).ToArray();
    }
    public IEnumerable<Field> CombineClues(IEnumerable<Field> fields1, IEnumerable<Field> fields2)
    {
        var valid = 0;
        var invalid = 0;
        Field[] fields2AsArray = fields2.ToArray();
        foreach (var field in fields1)
        {
            foreach (var field1 in fields2AsArray)
            {
                var r = field + field1;

                if (r == Field.InvalidField)
                {
                    invalid++;
                    continue;
                }
                valid++;
                if (_needAutoComplete)
                    r = r.autoComplete();
                yield return r;
            }
        }
    }

    /// <summary>
    /// At least 2 Clues in 1 Field needed.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Field> CombineAllClues()
    {
        var clueList = GetClues();

        var output = CombineClues(clueList[0].GenerateFields(), clueList[1].GenerateFields());
        for (var i = 2; i < clueList.Length; i++)
        {
            output = CombineClues(output, clueList[i].GenerateFields());
        }
        //_needAutoComplete = false;
        return output;
    }
}