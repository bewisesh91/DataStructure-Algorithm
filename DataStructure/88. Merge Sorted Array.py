# You are given two integer arrays nums1 and nums2, sorted in non-decreasing order, and two integers m and n, representing the number of elements in nums1 and nums2 respectively.
# Merge nums1 and nums2 into a single array sorted in non-decreasing order.
# The final sorted array should not be returned by the function, but instead be stored inside the array nums1. 
# To accommodate this, nums1 has a length of m + n, where the first m elements denote the elements that should be merged, and the last n elements are set to 0 and should be ignored. nums2 has a length of n.

nums1 = [1,2,3,0,0,0]
m = 3

nums2 = [2,5,6]
n = 3

class Solution:
    from bisect import bisect_right
    def merge(self, nums1: List[int], m: int, nums2: List[int], n: int) -> None:
        """
        Do not return anything, modify nums1 in-place instead.
        """
        while nums1 and nums1[-1] == 0:
            nums1.pop()
        
        for num in nums2:
            index = bisect_right(nums1, num)
            nums1.insert(index, num)
        
        while len(nums1) < m + n :
            nums1.append(0)