# Given an array of integers nums and an integer target, return indices of the two numbers such that they add up to target.

# You may assume that each input would have exactly one solution, and you may not use the same element twice.

# You can return the answer in any order.

nums = [2,7,11,15]

target = 9

class Solution:
    def twoSum(self, nums: List[int], target: int) -> List[int]:
        temp = {}
        for index, num in enumerate(nums):
            temp[num] = index
        for index, num in enumerate(nums):
            if target - num in temp and index != temp[target - num]:
                return [index, temp[target - num]]

class Solution:
    def twoSum(self, nums: List[int], target: int) -> List[int]:
        for i in range(len(nums)):
            for j in range(1 + i, len(nums)):
                if nums[i] + nums[j] == target:
                    return [i, j]