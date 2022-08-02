int[] nums = new int[] {2,7,11,15};

int target = 9;
public class Solution {
    public int[] TwoSum(int[] nums, int target) {
        int[] array = new int[2];
        for(int i = 0; i < nums.Length; i++)
        {
            for(int j = 1 + i; j < nums.Length; j++)
            {
                if (nums[i] + nums[j] == target)
                {
                    array[0] = i;
                    array[1] = j;        
                } 
            }
        }
        return array; 
    }
}

public class Solution {
    public int[] TwoSum(int[] nums, int target) {
        var numsDict = new Dictionary<int, int>();
        var result = new List<int>();
        
        for (var i = 0; i < nums.Length; i++)
        {
            numsDict.Add(i, nums[i]);
        }
        
        for (var i = 0; i < nums.Length; i++)
        {
            if (numsDict.ContainsValue(target - nums[i]) && i != Array.IndexOf(nums, target - nums[i]))
            {
                result.Add(i);
                result.Add(Array.IndexOf(nums, target - nums[i]));
                break;
            }
        }
        return result.ToArray();
    }
}
