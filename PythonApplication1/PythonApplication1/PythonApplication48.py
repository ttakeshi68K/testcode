# r = range((3))
# print(type(r))

# print('__iter__' in dir(r))  # Trueならrangeオブジェクトは__iter__メソッドを持つ
# print('__next__' in dir(r))  # Trueならrangeオブジェクトは__next__メソッドを持つ

# range_iter = r.__iter__()  # イテレータを取り出す
# range_iter2 = iter(r)  # イテレータを取り出す

# print(type(range_iter))
# print(type(range_iter2))

# print('__iter__' in dir(range_iter))  # Trueならrangeオブジェクトは__iter__メソッドを持つ
# print('__next__' in dir(range_iter))  # Trueならrangeオブジェクトは__next__メソッドを持つ

# print(range_iter.__next__())  # range_iterから次の値を取得
# print(range_iter.__next__())  # range_iterから次の値を取得
# print(next(range_iter2))  # range_iter2から次の値を取得
# print(next(range_iter2))  # range_iter2から次の値を取得

# print(next(range_iter))  # range(3)で作成したので、要素はここで尽きる
# # print(next(range_iter))  # これ以降は例外が発生する

class CountUpIterator:
    def __init__(self, limit=5):
        # 上限とカウンターの初期化処理
        self.limit = limit
        self.counter = -1
    def __iter__(self):
        # 自分自身を呼び出し側に戻す
        print('__iter__ method called')
        return self
    def __next__(self):
        # 上限を超えるまでカウントアップして、その値を戻す
        print('__next__ method called')
        self.counter += 1
        if self.counter >= self.limit:
            raise StopIteration()
        return self.counter

# countup_iter = CountUpIterator(3)
# countup_iter2 = iter(countup_iter)
# print(countup_iter is countup_iter2)  # True

# print(next(countup_iter))
# print(countup_iter2.__next__())
# print(next(countup_iter2))
# print(countup_iter.__next__())

from random import randint

class RandomIntIterator:
    def __init__(self, limit=5):
        # 上限とカウンターの初期化処理
        self.limit = limit
        self.counter = -1
    def __iter__(self):
        # 自分自身を呼び出し側に戻す
        return self
    def __next__(self):
        self.counter += 1
        if self.counter >= self.limit:
            raise StopIteration()
        return randint(0, 100)

mylist = list(RandomIntIterator()) 
print(mylist)

for num in RandomIntIterator(3):
    print(num)


