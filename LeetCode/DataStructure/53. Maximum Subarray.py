# Given an integer array nums, find the contiguous subarray (containing at least one number) which has the largest sum and return its sum.

nums = [-2,1,-3,4,-1,2,1,-5,4]

class Solution:
    def maxSubArray(self, nums: List[int]) -> int:
        curr_sum, max_sum = nums[0], nums[0]
        for num in nums[1:]:
            if num > num + curr_sum:
                curr_sum = num
            else:
                curr_sum += num
            max_sum = max(curr_sum, max_sum)
        return max_sum

# Using DP
class Solution:
    def maxSubArray(self, nums: List[int]) -> int:
        DP = nums
        for i in range(1, len(nums)):
            DP[i] = max(DP[i-1] + nums[i], DP[i])
        print(DP)
        return max(DP)