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
    public ListNode MergeTwoLists(ListNode list1, ListNode list2) {
        var result = new ListNode();
        var realResult = result;
        
        while (list1 != null && list2 != null)
        {
            if (list1.val < list2.val)
            {
                result.next = list1;
                var temp = list1;
                list1 = list1.next;
                result = temp;
            }
            else
            {
                var temp = list2;
                result.next = list2;
                list2 = list2.next;
                result = temp;
            }
        }
        if (list1 != null)
            result.next = list1;
        else
            result.next = list2;
        
        return realResult.next;
    }
}