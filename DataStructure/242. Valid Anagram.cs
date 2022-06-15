public class Solution {
    public bool IsAnagram(string s, string t) {
        if (s.Length != t.Length) 
        {
            return false;
        }
        
        var tempDict = new Dictionary<char, int>();
        
        for (int i = 0; i < t.Length; i++)
        {
            if (tempDict.ContainsKey(t[i]))
            {
                tempDict[t[i]] += 1;
            }
            else 
            {
                tempDict[t[i]] = 1;
            }
        }
        
        for (int i = 0; i < s.Length; i++)
        {
            if (tempDict.ContainsKey(s[i]))
            {
                tempDict[s[i]] -= 1;
            }
            else 
            {
                tempDict[s[i]] = 1;
            }
        }
        
        foreach(char Key in tempDict.Keys) 
        {
            if (tempDict[Key] != 0)
                {
                    return false;
                }; 
		}
        
        return true;

    }
}