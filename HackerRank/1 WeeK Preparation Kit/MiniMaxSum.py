# Given a time in -hour AM/PM format, convert it to military (24-hour) time.
# Note: - 12:00:00AM on a 12-hour clock is 00:00:00 on a 24-hour clock.
# - 12:00:00PM on a 12-hour clock is 12:00:00 on a 24-hour clock.

def timeConversion(s):
    # Write your code here
    h, m, flag = s.split(':')
    if flag[2:] == "PM" and h != '12':
        h = str(int(h) + 12)
    if flag[2:] == "AM" and h == '12':
        h = "00"
    if flag[2:] == "PM" and h == '12':
        h = "12"
    
    return h + ':' + m + ':' + flag[:2]