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
    public ListNode MiddleNode(ListNode head) {
        var count = 0;
        var temp = head;
        
        while (temp != null)
        {
            count += 1;
            temp = temp.next;
        }
        
        var mid = count / 2 + 1;
        
        while (mid > 1)
        {
            mid -= 1;
            head = head.next;
        }
        
        return head;
    }
}