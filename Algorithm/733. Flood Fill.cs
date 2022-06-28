public class Solution {
    public int[][] FloodFill(int[][] image, int sr, int sc, int newColor) {
        DFS(image, sr, sc, newColor, image[sr][sc]);
        return image;
    }
    
    public void DFS(int[][] image, int sr, int sc, int newColor,int originalColor){
        if (sr < 0 || sc < 0 || sr >= image.Length || sc >= image[0].Length || newColor == originalColor) 
            return;
        if (image[sr][sc] != originalColor) 
            return;
        
        image[sr][sc] = newColor;
        DFS(image, sr + 1, sc, newColor, originalColor);
        DFS(image, sr - 1, sc, newColor, originalColor);
        DFS(image, sr, sc + 1, newColor, originalColor);
        DFS(image, sr, sc - 1, newColor, originalColor);
    }
    
}