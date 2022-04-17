using System;
using System.Collections.Generic;

public class Solution
{
    

    public IList<IList<int>> Permutation(int[] candidates, int target)
    {
        List<IList<int>> permutations = new List<IList<int>>();
        recurse(candidates.ToList(), new List<int>(), permutations);

        return permutations;
    }

    private void recurse(List<int> choices, List<int> workset, List<IList<int>> permutations)
    {
        // base-case
        if(choices.Count == 0)
        {
            permutations.Add(new List<int>(workset));
            return;
        }

        // recursive case
        for(int i = 0; i < choices.Count; ++i)
        {
            var value = choices[i];
            workset.Add(value);
            choices.RemoveAt(i);

            recurse(choices, workset, permutations);

            choices.Insert(i, value);
            workset.Remove(value);
        }
    }
}