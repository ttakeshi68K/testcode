myfile = open('myfile.txt', 'w')  # テキストファイルを書き込み用にオープン
myfile.write('あ')  # 文字列「'あ'」を書き込み
myfile.close()

myfile = open('myfile.txt', 'rb')  # テキストファイルをバイナリモードで読み込み用にオープン
content = myfile.read()
print(content)
myfile.close()