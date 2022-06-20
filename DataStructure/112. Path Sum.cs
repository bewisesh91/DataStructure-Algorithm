/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Solution {
    public bool HasPathSum(TreeNode root, int targetSum) {
        if (root == null)
            return false;
        
        if (HasPathSumHelp(root, 0, targetSum))
            return true;
            
        return false;
    }
    
    public bool HasPathSumHelp(TreeNode node, int curSum, int targetSum)
    {
        curSum += node.val;
        if (node.left == null && node.right == null)
        {
            if (curSum == targetSum)
                return true;
            else
                return false;
        }
        
        if (node.left != null)
        {
            if (HasPathSumHelp(node.left, curSum, targetSum))
                return true;
        }
        
        if (node.right != null)
        {
            if (HasPathSumHelp(node.right, curSum, targetSum))
                return true;
        }
        
        return false;
    }
}