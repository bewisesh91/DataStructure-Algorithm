class Result
{

    /*
     * Complete the 'diagonalDifference' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts 2D_INTEGER_ARRAY arr as parameter.
     */

    public static int diagonalDifference(List<List<int>> arr)
    {
        var left = 0;
        var right = 0;
        
        for (var i = 0; i < arr[0].Count(); i++)
        {
            left += arr[i][i];
            right += arr[i][arr[0].Count() - 1 - i];
        }
        
        return Math.Abs(left - right);
    }

}