from pathlib import Path

def file_copy(src, dst):
    try:
        with open(src, 'rb') as fsrc, open(dst, 'wb') as fdst:
            try:
                content = fsrc.read()
                fdst.write(content)
            except Exception as e:
                print(e)
    except OSError as e:
        print(e)

file_copy('foo.txt', 'bar.txt')
print(Path('bar.txt').read_text())
