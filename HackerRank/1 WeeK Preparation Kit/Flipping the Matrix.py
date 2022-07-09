def flippingMatrix(matrix):
    # Write your code here
    n = len(matrix)
    result = 0
    
    for i in range(n // 2):
        for j in range(n // 2):
            result += max(matrix[i][j], matrix[i][n-j-1], matrix[n-i-1][j], matrix[n-i-1][n-j-1])

    return result