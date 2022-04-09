using System.Collections.Generic;

public class Solution
{
    public int[] TwoSum(int[] nums, int target)
    {
        Dictionary<int, int> indexMap = new Dictionary<int, int>(){};
        for(int i = 0; i < nums.Length; ++i)
        {
            if(indexMap.ContainsKey(target - nums[i]))
                return new int[] { indexMap[target - nums[i]], i};
            
            if(!indexMap.ContainsKey(nums[i]))
                indexMap.Add(nums[i], i);
        }

        return new int[]{0, 0};
    }
}