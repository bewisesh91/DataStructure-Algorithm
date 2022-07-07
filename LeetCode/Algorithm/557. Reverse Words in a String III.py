class Solution:
    def reverseWords(self, s: str) -> str:
        def reverse(string):
            mid = len(string) // 2
            for i in range(mid):
                string[i], string[len(string) - 1 - i] = string[len(string) - 1 - i], string[i]
            return string
        
        result = []
        temp = s.split(" ")
        for string in temp:
            result.append("".join(reverse(list(string))))
            
        return " ".join(result)