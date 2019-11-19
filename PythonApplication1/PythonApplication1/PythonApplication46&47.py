# T = (False) and "接点T" or "接点Tではない"
# print(T)

# import os
# from pathlib import Path
# import shutil

# Path('xxx.txt').write_text('xxx')
# os.chmod('xxx.txt',0o777)

from pathlib import Path

def get_content(src):
    try:
        fsrc = open(src)
    except OSError as e:
        print(e)
    else:
        try:
            content = fsrc.read()
            # raise Exception('hello from nested try statement')
            return content
        except Exception as e:
            print(e)
        finally:
            # print('closing fsrc')
            fsrc.close()

# Path('foo.txt').write_text('foo,bar,baz')

content = get_content('foo.txt')
# content = get_content('nowexist.txt')

print(content)

