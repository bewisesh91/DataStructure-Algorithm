public class Solution {
    public int[][] MatrixReshape(int[][] mat, int r, int c) {
        List<int> numsList = new List<int>();
        
        foreach (var matrix in mat)
        {
            foreach (var num in matrix)
            {
                numsList.Add(num);
            }
        }
        
        List<int[]> result = new List<int[]>();
        
        for (int i = 0; i < r; i++)
        {
            List<int> temp = new List<int>();
            for (int j = 0; j < c; j++)
            {
                if (numsList.Count !=0)
                {
                    temp.Add(numsList[0]);
                    numsList.Remove(numsList[0]);
                }
                else 
                {
                    return mat;
                }
            }
            result.Add(temp.ToArray());
        }
        
        if (numsList.Count != 0)
        {
            return mat;
        }
        else 
        {
            return result.ToArray();
        }
    }
}