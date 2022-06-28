class Solution:
    def floodFill(self, image: List[List[int]], sr: int, sc: int, color: int) -> List[List[int]]:
        visited = [[0 for _ in range(len(image[0])) ] for _ in range(len(image))]
        temp = [[sr, sc]]
        visited[sr][sc] = 1
        target = image[sr][sc]
        if color == 0:
            return image
        
        image[sr][sc] = color
        
        while temp:
            r, c = temp.pop()
            
            for dir_r, dir_c in (-1, 0), (1, 0), (0, -1), (0, 1):
                new_r, new_c = r + dir_r, c + dir_c
                
                if 0 <= new_r and new_r < len(image) and 0 <= new_c and new_c < len(image[0]):
                    print(new_r, new_c)
                    if image[new_r][new_c] == target and visited[new_r][new_c] != 1:
                        temp.append([new_r, new_c])
                        image[new_r][new_c] = color
                        visited[new_r][new_c] = 1
                
        return image
        
            