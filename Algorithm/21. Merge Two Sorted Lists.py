# Definition for singly-linked list.
# class ListNode:
#     def __init__(self, val=0, next=None):
#         self.val = val
#         self.next = next
class Solution:
    def mergeTwoLists(self, list1: Optional[ListNode], list2: Optional[ListNode]) -> Optional[ListNode]:
        real_result = result = ListNode()
        while list1 and list2:               
            if list1.val < list2.val:
                result.next = list1
                list1, result = list1.next, list1
            else:
                result.next = list2
                list2, result = list2.next, list2
                
        if list1:
            result.next = list1
        else :
            result.next = list2
            
        return real_result.next