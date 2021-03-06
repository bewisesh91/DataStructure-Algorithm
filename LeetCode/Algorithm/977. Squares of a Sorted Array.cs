public class Solution {
    public int[] SortedSquares(int[] nums) {
        return nums
            .Select(num => num * num)
            .OrderBy(num => num)
            .ToArray();
    }
}