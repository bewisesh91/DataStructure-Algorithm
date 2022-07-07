# Given two integer arrays nums1 and nums2, return an array of their intersection.
# Each element in the result must appear as many times as it shows in both arrays and you may return the result in any order.

nums1 = [1, 2, 2, 1]
nums2 = [2, 2]


class Solution:
    def intersect(self, nums1: List[int], nums2: List[int]) -> List[int]:
        result = []

        if len(nums1) > len(nums2):
            flag = True
        else:
            flag = False

        if flag:
            while nums2:
                num = nums2.pop()
                if num in nums1:
                    result.append(num)
                    nums1.remove(num)

        else:
            while nums1:
                num = nums1.pop()
                if num in nums2:
                    result.append(num)
                    nums2.remove(num)

        return result
