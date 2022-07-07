// Given an array, rotate the array to the right by k steps, where k is non-negative.

public class Solution {
    public void Rotate(int[] nums, int k) {
        var n = nums.Length;
        k = k % n;
        var num = n-k;
               
        var nums1 = nums[num..];
        var nums2 = nums[..num];
            
        nums1 = nums1.Concat(nums2).ToArray();
           
        for (var i = 0; i < nums.Length; i++)
            nums[i] = nums1[i];
    }
}