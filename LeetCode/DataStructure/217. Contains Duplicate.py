# Given an integer array nums, return true if any value appears at least twice in the array, and return false if every element is distinct.

nums = [1,2,3,1]

class Solution:
    def containsDuplicate(self, nums: List[int]) -> bool:
        if len(nums) != len(set(nums)):
            return True
        return False

