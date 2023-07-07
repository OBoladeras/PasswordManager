import os
import sys

print(os.getcwd())

valid = True
username = sys.argv[1]

try:
    with open("../../Data/users.txt", "r") as f:
        for line in f.readlines():
            if username in line:
                valid = False
except:
    pass

if valid:
    print("valid")
