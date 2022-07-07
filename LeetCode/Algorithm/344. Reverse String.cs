// Write a function that reverses a string. The input string is given as an array of characters s.
// You must do this by modifying the input array in-place with O(1) extra memory.

public class Solution {
    public void ReverseString(char[] s) {
        var mid = s.Length / 2;
        for (var i = 0; i < mid; i++)
        {
            var temp = s[s.Length - 1 - i];
            s[s.Length - 1 - i] = s[i];
            s[i] = temp;
        }
    }
}