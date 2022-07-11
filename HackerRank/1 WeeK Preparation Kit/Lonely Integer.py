def lonelyinteger(a):
    temp = {}
    
    for num in a :
        if num in temp:
            temp[num] += 1
        else :
            temp[num] = 1
    
    for key, value in temp.items():
        if value == 1:
            return key