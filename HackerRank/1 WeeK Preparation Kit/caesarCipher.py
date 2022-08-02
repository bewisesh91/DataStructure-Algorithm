def caesarCipher(s, k):
    # Write your code here
    original = list('abcdefghijklmnopqrstuvwxyz')
    convert = {}
    for i in range(len(original)):
        convert[original[i]] = original[(i + k) % 26]
    
    result = ""
    for alpha in s:
        temp = alpha.lower()
        if temp in convert:
            if temp == alpha:
                result += convert[temp]
            else:
                temp = convert[temp].upper()
                result += temp
        else:
            result += alpha
    
    return result