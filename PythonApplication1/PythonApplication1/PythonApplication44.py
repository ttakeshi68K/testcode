import os

# print(os.getcwd())

print('using os.listdir')
for name in os.listdir():
    if os.path.isfile(name):
        print(f'file: {name}')
    else:
        print(f'dir:  {name}')

print('using os.scanfir')
for entry in os.scandir():
    if entry.is_file():
        print(f'file: {entry.name}')
    else:
        print(f'dir:  {entry.name}')


