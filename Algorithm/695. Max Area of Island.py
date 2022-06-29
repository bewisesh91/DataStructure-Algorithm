class Solution:
    def maxAreaOfIsland(self, grid: List[List[int]]) -> int:
        # 일단 0, 0 부터 시작, 맥스 값 0으로 설정
        # 1을 만나면 거기서 상, 하, 좌, 우 탐색하면서 1의 합 계산
        # 맥스 값 갱신
        
        m, n = len(grid), len(grid[0])

        def dfs(i, j):
            if 0 <= i < m and 0 <= j < n and grid[i][j]:
                grid[i][j] = 0
                return 1 + dfs(i - 1, j) + dfs(i, j + 1) + dfs(i + 1, j) + dfs(i, j - 1)
            return 0

        areas = [dfs(i, j) for i in range(m) for j in range(n) if grid[i][j]]
        return max(areas) if areas else 0
                    
                        
        
        
        
        
        