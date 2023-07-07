import sys
import hashlib

salt = b"kb3p9sa8NSJ8783As"
username = sys.argv[1]
password = sys.argv[2]

hashedPassword = hashlib.pbkdf2_hmac("sha256", password.encode(), salt, 100000)

userValid = False
passwordValid = False

with open("../../Data/users.txt", "r") as f:
    for line in f.readlines():
        user = line.split(";")[0]
        txtPasssword = line.split(";")[1].strip()

        if user == username:
            userValid = True

            if str(txtPasssword) == str(hashedPassword):
                passwordValid = True

if userValid:
    if passwordValid:
        print("valid")
    else:
        print("error1")
else:
    print("error2")
