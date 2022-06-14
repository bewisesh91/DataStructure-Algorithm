class Solution:
    def firstUniqChar(self, s: str) -> int:
        temp = {}   
        for string in s :
            if string not in temp:
                temp[string] = 1
            else :
                temp[string] += 1
        
        for char in temp:
            if temp[char] == 1:
                return s.index(char)
        return -1
        