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
    public IList<int> PostorderTraversal(TreeNode root) {
        List<int> result = new List<int>{};
        result = Postorder(root, result).ToList();
        return result;
    }
    
    public List<int> Postorder(TreeNode root, List<int> result)
    {
        if (root != null)
        {
            Postorder(root.left, result);
            Postorder(root.right, result);
            result.Add(root.val);
        }
        return result;
    }
}