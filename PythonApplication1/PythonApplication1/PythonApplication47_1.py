from pathlib import Path

def get_content(src):
        try:
            with open(src) as fsrc:
                try:
                    content = fsrc.read()
                    return content
                except Exception as e:
                    print(e)
        except Exception as e:
               print(e)

content = get_content('foo.txt')
# content = get_content('nowexist.txt')

print(content)

