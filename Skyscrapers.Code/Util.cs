using System.ComponentModel;
using System.Xml.Serialization;

namespace Skyscrapers.Code;

public static class Util
{
    public static IEnumerable<int[]> GetPermutations(int[] elements)
    {
        if (elements == null) throw new ArgumentNullException(nameof(elements));

        if (elements.Length == 1)
        {
            yield return elements;
            yield break;
        }

        foreach (var element in elements)
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

    public static IEnumerable<(int visibleStart, int visibleEnd, int[] permutation)> GetPermutationsByVisibilityHeight(this int[] permutationBase)
    {
        var permutations = GetPermutations(permutationBase);
        return 
            (from permutation in permutations 
                let countVisible = CountVisible(permutation) 
                select (countVisible.Item1, countVisible.Item2, permutation)
            ).ToList();
    }
    
    public static IEnumerable<(int visibleStart, int visibleEnd, int[] permutation)> GetPermutationsFromStart(this IEnumerable<int[]> permutations, int height)
    {
        return 
            (from permutation in permutations 
                let countVisible = CountVisible(permutation) 
                select (countVisible.Item1, countVisible.Item2, permutation)
            ).Where((tuple, i) => tuple.Item1 > height).ToList();
    }
    
    public static IEnumerable<(int visibleStart, int visibleEnd, int[] permutation)> GetPermutationsFromEnd(this IEnumerable<int[]> permutations, int height)
    {
        return 
            (from permutation in permutations 
                let countVisible = CountVisible(permutation) 
                select (countVisible.Item1, countVisible.Item2, permutation)
            ).Where((tuple, i) => tuple.Item2 > height).ToList();
    }
    
    public static (int countStart, int countEnd) CountVisible(int[] skyscrapers)
    {
        var highestStart = 0;
        var visibleCountStart = 0;
        var highestEnd = 0;
        var visibleCountEnd = 0;
        for (var i = 0; i < skyscrapers.Length; i++)
        {
            var skyscraperStart = skyscrapers[i];
            var skyscraperEnd = skyscrapers[skyscrapers.Length - 1 - i];

            if (skyscraperStart > highestStart)
            {
                highestStart = skyscraperStart;
                visibleCountStart++;
            }

            if (skyscraperEnd > highestEnd)
            {
                highestEnd = skyscraperEnd;
                visibleCountEnd++;
            }
        }
        return (visibleCountStart, visibleCountEnd);
    }
}