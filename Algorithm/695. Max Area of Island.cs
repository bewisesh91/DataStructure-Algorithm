public class Solution {
    public int MaxAreaOfIsland(int[][] grid) {
        var maxRow = grid.Count();
        var maxCol = grid[0].Length;
        List<int> result = new List<int> {};
        
        for (var i = 0; i < maxRow; i++)
            for (var j = 0; j < maxCol; j++)
                if (grid[i][j] != 0)
                    result.Add(DFS(maxRow, maxCol, i, j, grid));
        
        if (result.Count() != 0)
            return result.Max();
        else
            return 0;
    }
    
    public int DFS(int maxRow, int maxCol, int curRow, int curCol, int[][] grid) {
        if (0 <= curRow && curRow < maxRow && 0 <= curCol && curCol < maxCol && grid[curRow][curCol] != 0)
        {
            grid[curRow][curCol] = 0;
            return 1 + DFS(maxRow, maxCol, curRow - 1, curCol, grid) + DFS(maxRow, maxCol, curRow, curCol + 1, grid) + DFS(maxRow, maxCol, curRow + 1, curCol, grid) + DFS(maxRow, maxCol, curRow, curCol - 1, grid);
        }
        return 0;
    }
}