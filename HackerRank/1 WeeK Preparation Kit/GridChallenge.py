def gridChallenge(grid):
    # Write your code here
    temp = []
    for info in grid:
        info = sorted(list(info))
        temp.append(info)
    result = transpose(temp)

    for case in result:
        if case != sorted(case):
            return "NO"
    return "YES"

def transpose(grid) :
    row, col = len(grid), len(grid[0])
    result = [[0 for row in range(row)] for col in range(col)]

    for i in range(row):
        for j in range(col):
            result[j][i] = grid[i][j]
    return result
    

  
grid = ['ebacd', 'fghij', 'olmkn', 'trpqs', 'xywuv']
print(gridChallenge(grid))