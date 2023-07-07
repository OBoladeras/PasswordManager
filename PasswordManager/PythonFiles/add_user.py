import sys
import hashlib

username = sys.argv[1]
password = sys.argv[2]

salt = b"kb3p9sa8NSJ8783As"
hashed_password = hashlib.pbkdf2_hmac("sha256", password.encode(), salt, 100000)

line = f"{username};{hashed_password}\n"

with open("../../Data/users.txt", "a") as f:
    f.write(line)