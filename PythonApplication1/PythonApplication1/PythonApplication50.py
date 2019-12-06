def sample_geniter():
    counter = 0
    while True:
        try:
            yield counter
            counter += 1
        except TypeError as e:
            break
        except Exception as e:
            print(e)
            counter += 1
        finally:
            print('finally')


mygeniter = sample_geniter()
# print(next(mygeniter))
# print(mygeniter.throw(NameError, 'name error'))
# print(mygeniter.throw(TypeError, 'type error'))

next(mygeniter)
# del mygeniter  # 変数mygeniterを削除すると、自動的にcloseメソッドが呼び出される
# print('hello')
mygeniter = sample_geniter()

#   ジェネレータ式

gen_expr = (x for x in range(4))

for num in gen_expr:
    print(num)


# yield form式

def sample_geniter3():
    yield from [1, 2, 3, 4]
    print('finished')

for num in sample_geniter3():
    print(num)

print('hello')

# 上記をwhileで書き直したモノ例外発生する StopIteration

def sample_geniter4():
    yield from [1, 2, 3, 4]
    print('finished')

mygeniter = sample_geniter4()
num = next(mygeniter)
while True:
    print(num)
    num = next(mygeniter)



