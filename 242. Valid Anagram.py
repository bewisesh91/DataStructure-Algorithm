class Solution:
    def isAnagram(self, s: str, t: str) -> bool:
        string1 = list(s)
        string2 = list(t)
        
        while string1:
            char = string1.pop()
            if char in string2:
                string2.remove(char)
            else :
                return False
        
        if len(string2) != 0:
            return False
        
        return True
        