/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
public class Solution {
    public ListNode RemoveNthFromEnd(ListNode head, int n) {
        var p1 = head;
        var p2 = head;
        
        for (var i = 0; i < n; i ++)
            p1 = p1.next;
        
        if (p1 == null)
            return head.next;
    
        while (p1.next != null)
        {
            p1 = p1.next;
            p2 = p2.next;
        }
        
        p2.next = p2.next.next;
        
        return head;
    }
}