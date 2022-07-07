public class Solution {
    public int SearchInsert(int[] nums, int target) {
        var left = 0;
        var right = nums.Length - 1;
        
        while (left <= right)
        {
            var mid = (left + right) / 2;
            if (nums[mid] == target)
                return mid;
            else if (nums[mid] > target)
                right = mid - 1;
            else 
                left = mid + 1;
        }
        return left;
    }
}

public class Solution {
    public int SearchInsert(int[] nums, int target) {
        var result = Array.BinarySearch(nums, target);
        
        if (result >= 0)
            return result;
        else
            return ~result;
    }
}