# Definition for a binary tree node.
# class TreeNode:
#     def __init__(self, val=0, left=None, right=None):
#         self.val = val
#         self.left = left
#         self.right = right
class Solution:
    def hasPathSum(self, root: Optional[TreeNode], targetSum: int) -> bool:        
        if root is None:
            return False
        
        def hasPathSumHelp(node, running_sum, sum):
            
            running_sum += node.val

            if node.left is None and node.right is None:
                if running_sum == sum:
                    return True
                else:
                    return False
        
            if node.left:
                if hasPathSumHelp(node.left, running_sum, sum):
                    return True
                
            if node.right:
                if hasPathSumHelp(node.right, running_sum, sum):
                    return True
            
        if hasPathSumHelp(root, 0, targetSum):
            return True
        
        return False