class Solution:
    def canConstruct(self, ransomNote: str, magazine: str) -> bool:
        ransomNote = list(ransomNote)
        magazine = list(magazine)
        
        while ransomNote:
            char = ransomNote.pop()
            if char in magazine:
                magazine.remove(char)
            else :
                return False
        
        return True