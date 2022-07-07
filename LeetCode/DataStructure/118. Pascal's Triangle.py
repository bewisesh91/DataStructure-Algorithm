# Given an integer numRows, return the first numRows of Pascal's triangle.

# In Pascal's triangle, each number is the sum of the two numbers directly above it as shown:

numRows = 5
# Output: [[1],[1,1],[1,2,1],[1,3,3,1],[1,4,6,4,1]]

class Solution:
    def generate(self, numRows: int) -> List[List[int]]:
        result = []
        
        for i in range(numRows):
            if i == 0:
                result.append([1])
                
            elif i == 1:
                result.append([1, 1])
            
            else :
                temp = []
                before = result[i - 1]
                
                for index in range(len(before) - 1 ):
                    add_sum = before[index] + before[index + 1]
                    temp.append(add_sum)
            
                result.append([1] + temp + [1])
        
        return result