public class Solution {
    public int FirstUniqChar(string s) {
        Dictionary<char, int> temp = new Dictionary<char, int>();
        
        for (int i = 0; i < s.Length; i++)
        {
            if (temp.ContainsKey(s[i]))
            {
                temp[s[i]] += 1;
            }
            else 
            {
                temp.Add(s[i], 1);
            }
        }
        
        foreach (char alpha in s)
        {
            if (temp[alpha] == 1)
            {
                return s.IndexOf(alpha);
            }
            
        }
        return -1;
    }
}