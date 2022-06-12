// Given two integer arrays nums1 and nums2, return an array of their intersection.
// Each element in the result must appear as many times as it shows in both arrays and you may return the result in any order.

int[] nums1 = new int[] {1, 2, 2, 1};
int[] nums2 = new int[] {2, 2};

public class Solution {
    public int[] Intersect(int[] nums1, int[] nums2) {
        var result = new List<int>();
        var nums2List = nums2.ToList();
        
        for (int i = 0; i < nums1.Length; i++)
        {
            foreach (int num in nums2List)
            {
                if (nums1[i] == num)
                {
                    result.Add(num);
                    nums2List.Remove(num);
                    break;
                }
            }
        }
        return result.ToArray();
    }
}