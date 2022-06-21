// You are given the root node of a binary search tree (BST) and a value to insert into the tree. 
// Return the root node of the BST after the insertion. 
// It is guaranteed that the new value does not exist in the original BST.
// Notice that there may exist multiple valid ways for the insertion, as long as the tree remains a BST after insertion. You can return any of them.

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
    public TreeNode InsertIntoBST(TreeNode root, int val) {
        if (root == null)
            return new TreeNode(val);
        
        DFS(root, val);
        
        return root;
    }
    
    private void DFS(TreeNode node, int val)
    {
        if (node.val > val)
        {
            if (node.left == null)
                node.left = new TreeNode(val);
            else
                DFS(node.left, val);
        }
        else
        {
            if (node.right == null)
                node.right = new TreeNode(val);
            else
                DFS(node.right, val);
        }
    }
}