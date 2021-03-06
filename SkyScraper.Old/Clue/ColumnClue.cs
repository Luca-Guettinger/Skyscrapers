namespace SkyScraper.Old.Clue;

public class ColumnClue : Clue
{
    public ColumnClue(int l, int clue1, int clue2, int index) : base(l, clue1, clue2, index)
    {
    }

    public override IEnumerable<Field> GenerateFields()
    {
        var permutations = (IEnumerable<int[]>)Permutations;

        foreach (var permutation in permutations)
        {
            yield return new Field(new Column(permutation), Index);
        }
    }
}