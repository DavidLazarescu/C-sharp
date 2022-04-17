using System;
using System.Linq;
using System.Collections.Generic;

public class Solution
{
    private int _target;
    private int[] _candidates;
    private List<IList<int>> _result = new List<IList<int>>();


    public IList<IList<int>> CombinationSum(int[] candidates, int target)
    {
        _target = target;
        _candidates = candidates;
        
        recurse(new List<int>());

        return _result;
    }

    private void recurse(List<int> list)
    {
        // Base case
        if(sumOfList(list) > _target)
            return;

        if(sumOfList(list) == _target)
        {
            if(!resultContains(list))
                _result.Add(new List<int>(list));
            return;
        }


        // Recursive case
        for(int i = 0; i < _candidates.Length; ++i)
        {
            list.Add(_candidates[i]);
            recurse(list);
            list.Remove(_candidates[i]);
        }
    }

    private int sumOfList(List<int> list)
    {
        int sum = 0;
        foreach(int item in list)
        {
            sum += item;
        }

        return sum;
    }

    private bool resultContains(List<int> list)
    {
        foreach(var subList in _result)
        {
            bool contains = true;
            foreach(int item in list)
            {
                if(!subList.Contains(item) || subList.Where(elem => elem == item).Count() != list.Where(elem => elem == item).Count())
                {
                    contains = false;
                    break;
                }
            }
            if(contains == true)
                return true;
        }

        return false;
    }
}