import os
import sys
import base64
import hashlib
from cryptography.fernet import Fernet


username = sys.argv[1]
web = sys.argv[2]
email = sys.argv[3]

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


def decrypt_password(cipher_text, encryption_key):
    cipher_suite = Fernet(encryption_key)
    plain_text = cipher_suite.decrypt(cipher_text)

    return plain_text.decode()


encryption_key = get_encryption_key()

with open(password_file, "r") as file:
    for line in file:
        stored_site, stored_username, date, encoded_password = line.strip().split(";")
        
        if stored_site == web and stored_username == email:
            encoded_password += "=" * (4 - len(encoded_password) % 4)
            encrypted_password = base64.urlsafe_b64decode(encoded_password)
            decrypted_password = decrypt_password(
                encrypted_password, encryption_key
            )

            print(decrypted_password)
