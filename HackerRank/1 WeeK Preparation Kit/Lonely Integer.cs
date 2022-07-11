class Result
{

    /*
     * Complete the 'lonelyinteger' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts INTEGER_ARRAY a as parameter.
     */

    public static int lonelyinteger(List<int> a)
    {
        foreach (var num in a)
        {
            if (a.Where(n => n.Equals(num)).Count() == 1)
            {
                return num;
            }
        }
        return -1;
    }
}