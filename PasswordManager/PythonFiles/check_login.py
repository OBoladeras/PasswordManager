import sys
import hashlib


def hash_password(password):
    salt = b"kb3p9sa8NSJ8783As"
    hashed_password = hashlib.pbkdf2_hmac("sha256", password.encode(), salt, 100000)


username = sys.argv[1]
password = sys.argv[2]

if username == "Oriol" and password == "123":
    print(True)