import os
import sys
from datetime import date
import base64
import hashlib
from cryptography.fernet import Fernet


username = sys.argv[1]
web = sys.argv[2]
email = sys.argv[3]
password = sys.argv[4]

password_file = f"../../Data/{username}_data.txt"



def encrypt4password(username):
    hash_object = hashlib.md5()
    hash_object.update(username.encode('utf-8'))
    hashed_txt = hash_object.hexdigest()

    return hashed_txt


def get_encryption_key():
    key_file = f"../../Data/{encrypt4password(username)}.txt"

    if os.path.exists(key_file):
        with open(key_file, "rb") as file:
            encryption_key = file.read()
    else:
        encryption_key = Fernet.generate_key()
        with open(key_file, "wb") as file:
            file.write(encryption_key)

    return encryption_key


def encrypt_password(password, encryption_key):
    cipher_suite = Fernet(encryption_key)
    cipher_text = cipher_suite.encrypt(password.encode())

    return cipher_text


def save_password(site, username, password):
    encryption_key = get_encryption_key()
    currDate = date.today()

    encrypted_password = encrypt_password(password, encryption_key)
    encoded_password = base64.urlsafe_b64encode(encrypted_password).decode()
    with open(password_file, "a") as file:
        file.write(f"{site};{username};{currDate};{encoded_password}\n")

try:
    save_password(web, email, password)
    print("valid")
except:
    print("error")