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