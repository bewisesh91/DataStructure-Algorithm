public class Solution {
    public bool CanConstruct(string ransomNote, string magazine) {
        List<char> charListR = new List<char>();
        List<char> charListM = new List<char>();
        
        foreach (char c in ransomNote)
        {
            charListR.Add(c);
        }
        
        foreach (char c in magazine)
        {
            charListM.Add(c);
        }
                
        
        for (int i = charListR.Count -1; i >= 0; i--)
        {
            if (charListM.Contains(charListR[i]))
            {
                charListM.Remove(charListR[i]);
            }
            else 
            {
                return false;
            }
        }
        
        return true;
    }
}