public class Solution {
    public int[] TwoSum(int[] numbers, int target) {
        var start = 0;
        var end = numbers.Length - 1;
        
        while (start <  end)
        {
            if (numbers[start] + numbers[end] == target)
                return new int[2] {start + 1, end + 1};
            else if (numbers[start] + numbers[end] > target)
                end -= 1;
            else
                start += 1;            
        }
        return numbers;
    }
}