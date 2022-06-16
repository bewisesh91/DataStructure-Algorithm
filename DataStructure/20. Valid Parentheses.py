from collections import deque
class Solution:
    def isValid(self, s: str) -> bool:
        stack = deque()
        brakets={'(' : ')', '{' : '}', '[' : ']'}

        for braket in s:
            if braket in brakets:
                stack.append(brakets[braket])
            else:
                if stack and braket == stack[-1]:
                    stack.pop()
                else:
                    return False
        
        if stack:
            return False
        
        return True
