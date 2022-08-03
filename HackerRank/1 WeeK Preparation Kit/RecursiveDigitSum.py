def superDigit(n, k):
    # Write your code here
    if int(n) < 10 :
        return int(n)
    temp = []
    if k != 1:
        for char in n :
            temp.append(int(char))
        return superDigit(str(sum(temp)*k), 1)
    else:
        for char in n :
                temp.append(int(char))
        return superDigit(str(sum(temp)), 1)