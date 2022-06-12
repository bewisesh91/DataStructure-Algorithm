# You are given an array prices where prices[i] is the price of a given stock on the ith day.

# You want to maximize your profit by choosing a single day to buy one stock and choosing a different day in the future to sell that stock.

# Return the maximum profit you can achieve from this transaction. If you cannot achieve any profit, return 0.

prices = [7, 1, 5, 3, 6, 4]


class Solution:
    def maxProfit(self, prices: List[int]) -> int:
        answer = 0
        min_price = sys.maxsize

        for price in prices:
            answer = max(answer, price - min_price)

            if price < min_price:
                min_price = price
        return answer
