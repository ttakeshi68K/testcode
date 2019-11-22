def simple_generator():
    yield 1
    print('first yield expression done')
    yield 2
    print('second yield expression done')
    yield 3
    print('thisrd yield expression done')

def simple_generator2():
    value = yield 1
    print('value of first yield:', value)
    value = yield 2
    print('value of second yield:', value)
    value = yield 3
    print('value of thisrd yield:', value)

# mygeniter = simple_generator()
# mygeniter = simple_generator2()

# print(next(mygeniter))
# print(next(mygeniter))

# print(mygeniter.send('hello'))

# print(type(mygeniter))

# print('__iter__' in dir(mygeniter))
# print('__nexr__' in dir(mygeniter))

# print(next(mygeniter))  # 次の値を取得
# print(mygeniter.__next__())  # 次の値を取得
# print(next(mygeniter))  # 次の値を取得
# print(mygeniter.__next__())  # 次の値を取得

def coutup_geniter(limit=5):
    counter = -1
    while True:
        counter += 1
        if counter >= limit:
            break
        yield counter

# for num in coutup_geniter(3):
#     print(num)

def hellomsg_geniter():
    namelist = ['kawasaki', 'isshiki', 'endo']
    counter = 0
    length = len(namelist)
    value = None
    while True:
        if value:  # sendメソッドで名前が送信されてきたときの処理
            namelist.append(str(value))
            length += 1
            counter = length - 1
        value = yield 'hello ' + namelist[counter]
        counter += 1
        if counter % length == 0:  # リストの末尾要素を使ったら先頭から使う
            counter = 0

mygeniter = hellomsg_geniter()
print(next(mygeniter))
print(next(mygeniter))
print(next(mygeniter))
print(next(mygeniter))
print(mygeniter.send('deep insider'))
print(next(mygeniter))
print(next(mygeniter))
print(next(mygeniter))
print(next(mygeniter))
print(next(mygeniter))