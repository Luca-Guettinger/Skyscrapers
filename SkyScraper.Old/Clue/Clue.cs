namespace SkyScraper.Old.Clue;

public abstract class Clue
{
    protected int Index;
    public int[][] Permutations { get; }

    protected Clue(int l, int leftTopClue, int rightBottomClue, int index)
    {
        var leftTopClue1 = leftTopClue;
        var rightBottomClue1 = rightBottomClue;
        this.Index = index;

        var array = new int[l];
        for (var i = 0; i < l; i++)
        {
            array[i] = i + 1;
        }

        this.Permutations = Generator.GetPermutations(array)
            .Where(arr => (leftTopClue1 == 0 || leftTopClue1 == VisibleCounter.CountVisible(arr, Direction.FromLeftToRight)))
            .Where(arr => (rightBottomClue1 == 0 || rightBottomClue1 == VisibleCounter.CountVisible(arr, Direction.FromRightToLeft)))
            .ToArray();
    }

    public abstract IEnumerable<Field> GenerateFields();
}