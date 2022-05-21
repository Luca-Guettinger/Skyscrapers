namespace SkyScraper.Old;

public static class Generator
{

    public static IEnumerable<int[]> GetPermutations(int[] elements)
    {
        if (elements == null) throw new ArgumentNullException(nameof(elements));

        if (elements.Length == 1)
        {
            yield return elements;
            yield break;
        }

        foreach (int element in elements)
        {
            var others = elements.Except(new[] { element }).ToArray();
            var children = GetPermutations(others).ToArray();
            foreach (var child in children)
            {
                var work = new int[elements.Length];
                work[0] = element;
                child.CopyTo(work, 1);
                yield return work;
            }
        }
    }

    private static int Fakultaet(int i)
    {
        if (i == 1)
            return 1;

        return i * Fakultaet(i - 1);
    }
}