public class Solution {
    public bool SearchMatrix(int[][] matrix, int target) {
        foreach (int[] m in matrix)
        {
            List<int> newM = m.ToList();
            if (newM.BinarySearch(target) >= 0)
                return true;
        }
        return false;
    }
}