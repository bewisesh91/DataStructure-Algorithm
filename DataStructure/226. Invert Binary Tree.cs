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
    public TreeNode InvertTree(TreeNode root)
    {
        if (root == null)
            return root;
        Invert(root);
        return root;
    }
    
    public void Invert(TreeNode node)
    {
        if (node == null)
            return;
        Invert(node.left);
        Invert(node.right);
        var temp = node.left;
        node.left = node.right;
        node.right = temp;
    }
}