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
    public ListNode ReverseList(ListNode head) {
        var prev = new ListNode();
        prev = prev.next;
        while (head != null)
        {
            var curr = head;
            head = head.next;
            curr.next = prev;
            prev = curr;
        }
        return prev;
    }
}