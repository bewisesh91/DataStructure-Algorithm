public class Solution {
    public bool IsValid(string s) {
        Stack<char> stack = new Stack<char>();
        Dictionary<char, char> brakets = new Dictionary<char, char>
        { 
            {'(', ')'},
            {'{', '}'}, 
            {'[', ']'}
        };
        
        foreach (char c in s)
        {
            if (brakets.ContainsKey(c))
            {
                stack.Push(brakets[c]);
            }
            else
            {
                if (stack.Count > 0 && c == stack.Peek())
                {
                    stack.Pop();
                }
                else 
                {
                    return false;
                }
            }
        }
        
        if (stack.Count > 0)
        {
            return false;    
        }
        return true;
    }
}