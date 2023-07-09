import random

password = ""
characters = ("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*-+=|:?,/")

for i in range(random.randint(15, 20)):
    password += random.choice(characters)

    if random.randint(15, 20) == i:
        password += str(i)

print(password)