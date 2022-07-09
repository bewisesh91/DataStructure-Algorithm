# Given a square matrix, calculate the absolute difference between the sums of its diagonals.
# For example, the square matrix  is shown below:

# 1 2 3
# 4 5 6
# 9 8 9  

# reuslt is 2

def diagonalDifference(arr):
    left = 0
    right = 0

    for i in range(len(arr[0])):
        left = left + arr[i][i]
        right = right + arr[i][len(arr[0]) - i - 1]

    return abs(left - right)
