public class Solution {
    public bool IsValidSudoku(char[][] board) {
        List<HashSet<int>> rows = new List<HashSet<int>>();
        List<HashSet<int>> cols = new List<HashSet<int>>();
        List<HashSet<int>> boxes = new List<HashSet<int>>();
        
        for (int i = 0; i < board.GetLength(0); i++)
        {
            rows.Add(new HashSet<int>());
            cols.Add(new HashSet<int>());
            boxes.Add(new HashSet<int>());
        }
        
        for (int r = 0; r < rows.Count(); r++)
        {
            for (int c = 0; c < cols.Count(); c++)
            {
                char s = board[r][c];
                if (s != '.')
                {
                    if (rows[r].Contains(s) || cols[c].Contains(s) || boxes[r / 3 * 3 + c / 3].Contains(s))
                    {
                        return false;
                    }
                    rows[r].Add(s);
                    cols[c].Add(s);
                    boxes[r / 3 * 3 + c / 3].Add(s);
                }
            }
        }        
        return true;
    }
}