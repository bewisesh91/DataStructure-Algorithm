public class Solution {
    public int MaxProfit(int[] prices) {
        int result = 0;
        int minPrice = Int32.MaxValue;
        
        foreach (int price in prices)
        {
            result = Math.Max(result, price - minPrice);
            
            if (price < minPrice)
            {
                minPrice = price;
            }
        }
        return result;
        
    }
}