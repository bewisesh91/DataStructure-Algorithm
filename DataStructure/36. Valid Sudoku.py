class Solution:
    def isValidSudoku(self, board: List[List[str]]) -> bool:
        rows = [set() for i in range(9)]
        cols = [set() for i in range(9)]
        boxs = [set() for i in range(9)]
        
        for r in range(len(board)):
            for c in range(len(board[r])):
                s = board[r][c]
                if s != '.':
                    if s in rows[r] or s in cols[c] or s in boxs[r // 3 * 3 + c // 3]:
                        return False
                    rows[r].add(s)
                    cols[c].add(s)
                    boxs[r//3*3+c//3].add(s)
                    print(r//3*3+c//3)
        
        print(boxs)
        return True