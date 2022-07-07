// Given an integer array nums, find the contiguous subarray (containing at least one number) which has the largest sum and return its sum.

int[] nums = new int[] {-2,1,-3,4,-1,2,1,-5,4};


public class Solution {
    public int MaxSubArray(int[] nums) {
        int curr_sum = nums[0];
        int max_sum = nums[0];
        
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] > nums[i] + curr_sum)
            {
                curr_sum = nums[i];
            }
            else 
            {
                curr_sum += nums[i];
            }
            
            if (max_sum < curr_sum)
            {
                max_sum = curr_sum;
            }
            
        }
        
        return max_sum;
    }
}