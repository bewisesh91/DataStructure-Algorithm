class Solution:    
    def searchMatrix(self, matrix: List[List[int]], target: int) -> bool:
        def binary_search(array, target):
            array.sort() # 정렬
            left = 0
            right = len(array) - 1

            while left <= right:
                mid = (left + right) // 2 
                if array[mid] == target: 
                    return mid 
                elif array[mid] > target: 
                    right = mid - 1 
                else: 
                    left = mid + 1
                    
            return -1 
        
        for m in matrix:
            if binary_search(m, target) != -1:
                return True
        return False