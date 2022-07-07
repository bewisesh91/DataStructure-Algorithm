public class Solution {
    public void MoveZeroes(int[] nums) {
        var slow = 0;
        var fast = 0;
        while (fast < nums.Length)
        {
            if (nums[slow] == 0 && nums[fast] != 0)
            {
                var temp = nums[fast];
                nums[fast] = nums[slow];
                nums[slow] = temp;
            }
                
            if (nums[slow] != 0)
                slow += 1;
            fast += 1;
        }
    }
}