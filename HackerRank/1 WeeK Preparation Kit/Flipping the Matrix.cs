class Result
{

    /*
     * Complete the 'flippingMatrix' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts 2D_INTEGER_ARRAY matrix as parameter.
     */

    public static int flippingMatrix(List<List<int>> matrix)
    {
        var n = matrix.Count();
        int result = 0;
        
        for (var i = 0; i < n / 2; i++)
        {
            for (var j =0; j < n / 2; j++)
            {
                result += Math.Max(Math.Max(matrix[i][j], matrix[i][n-j-1]), Math.Max(matrix[n-i-1][j],matrix[n-i-1][n-j-1]));
            }
        }
        return result;
    }
}