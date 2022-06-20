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
    public IList<int> InorderTraversal(TreeNode root) {
        List<int> result = new List<int>{};
        result = Inorder(root, result).ToList();
        return result;
    }
    
    public List<int> Inorder(TreeNode root, List<int> result)
    {
        if (root != null)
        {
            Inorder(root.left, result);
            result.Add(root.val);
            Inorder(root.right, result);
        }
        return result;
    }
}