public class Solution {
    public string ReverseWords(string s) {
        return string.Join(" ",
                s.Split(' ')
                .Select(str => new String(str.Reverse().ToArray())));
    }
}