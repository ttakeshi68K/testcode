from pathlib import Path
import os

# mypath = Path('.')
# print(type(mypath))

# mypath = Path('foo')
# newpath = mypath / 'bar' / Path('baz')
# print(newpath)

from pathlib import PurePath

# abspath = PurePath('c:/foo/bar/baz.txt')  # 絶対パス
# relpath = PurePath('xxx/yyy/zzz.txt')  # 相対パス
# print('type:', type(abspath))  # 実行する環境によって変化する
# print('parts:', abspath.parts)  # 構成要素に分割
# print('root:', abspath.root)  # ルートを表示
# print('root:', relpath.root)  # ルートを表示（相対パスの場合）
# print('parent:', abspath.parent)  # 上位パスの表示
# print('name:', relpath.name)  # ファイル名
# print('name:', os.path.basename('xxx/yyy/zzz.txt'))  # os.pathモジュールを使用
# print('suffix:', abspath.suffix)  # 拡張子
# print('stem:', abspath.stem)  # 拡張子以外のファイル名
# print('stem + suffix:', abspath.stem + abspath.suffix)  # ファイル名全体
# print('uri:', abspath.as_uri())  # URI形式（Windowsでは例外が発生するので注意）
# print('is absolute:', relpath.is_absolute())  # 相対パスを「絶対パスか」調べる
# print('joinpath:', Path('foo/bar').joinpath('baz', 'qux'))  # パスを結合
# print('with name:', relpath.with_name('qux.txt')) # ファイル名を置き換え

# print('cwd:', Path.cwd())

mypath = Path('.')

for path in mypath.iterdir():
    print('path.name:', path.name)

print('list comprehension',[path for path in mypath.iterdir()])
