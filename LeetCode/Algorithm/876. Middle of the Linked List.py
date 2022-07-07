# Definition for singly-linked list.
# class ListNode:
#     def __init__(self, val=0, next=None):
#         self.val = val
#         self.next = next
class Solution:
    def middleNode(self, head: Optional[ListNode]) -> Optional[ListNode]:
        count = 0
        temp = head
        
        while temp:
            count += 1
            temp = temp.next
            
        mid = count // 2 + 1
        
        while mid > 1:
            mid -=1
            head = head.next
        
        return head
        