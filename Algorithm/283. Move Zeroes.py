class Solution:
    def moveZeroes(self, nums: List[int]) -> None:
        """
        Do not return anything, modify nums in-place instead.
        """
        
        count = len(nums)
        index = 0
        while count > 0 :
            count -= 1
            if nums[index] == 0:
                nums.append(nums.pop(index))
            else :
                index += 1