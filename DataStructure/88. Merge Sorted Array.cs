public class Solution {
        public void Merge(int[] nums1, int m, int[] nums2, int n) {
        int i = m - 1;
        int j = n - 1;
        int k = m + n - 1;
        
        while(i >= 0 && j >= 0){
            if(nums1[i] > nums2[j]){
                nums1[k] = nums1[i];
                i--;
            }else{
                nums1[k] = nums2[j];
                j--;
            }
            k--;
        }

        while(j >= 0){
            nums1[k] = nums2[j];
            k--;
            j--;
        }
    }
}

public class Solution {
    public void Merge(int[] nums1, int m, int[] nums2, int n) {
          for(var i = 0; i < nums2.Length; i++){
            nums1[nums1.Length-1-i] = nums2[i];
          }
          Array.Sort(nums1);
        }
}