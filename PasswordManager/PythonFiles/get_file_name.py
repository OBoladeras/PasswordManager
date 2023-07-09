import sys
import hashlib

username = sys.argv[1]

hash_object = hashlib.md5()
hash_object.update(username.encode('utf-8'))
hashed_txt = hash_object.hexdigest()

print(f"../../Data/{hashed_txt}.txt")