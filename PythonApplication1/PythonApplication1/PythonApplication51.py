def mydecorator(func):
    def inner_func(*args):
        print(f'before execute {func.__name__}')
        result = func(*args)
        print(f'after  execute {func.__name__}')
        return result
    return inner_func

@mydecorator
def myfunc(msg):
    print(msg)

# mylen = mydecorator(len)
# print(mylen('string'))

# myfunc = mydecorator(myfunc)  # @mydecoratorでmyfuncが修飾されているため不要となる
# myfunc('Hello World')

def decorator1(func):
    def inner_func(*args):
        print('decorator1')
        result = func(*args)
    return inner_func

def decorator2(func):
    def inner_func(*args):
        print('decorator2')
        result = func(*args)
    return inner_func

# @decorator2
@decorator1
@decorator2
# @decorator1
def myfunc2(msg):
    print(msg)

# myfunc2('hello world')

def yourfunc(msg):
    print(msg)

# tmp1 = decorator2(yourfunc)
# tmp2 = decorator1(tmp1)

# tmp3 = decorator1(yourfunc)
# tmp4 = decorator2(tmp3)

# print('--- calling tmp2 ---')
# tmp2('hello world')
# print('--- calling tmp4 ---')
# tmp4('hello world')

def f1(msg='foo'):
    def inner_func(func):
        def most_innner_func(*arg):
            print(f'msg: {msg}')
            print('most_innner_func')
            result = func(*arg)
        return most_innner_func
    return inner_func

@f1('bar')
def some_func(msg):
    print(msg)

# some_func('Hello World')


def other_func(msg):
    print(msg)

# other_func = (f1('bazz'))(other_func)
# other_func = f1('bazz')(other_func)
# other_func('Hello World')

# other_func2 = f1('foobazz')
# other_func3 = other_func2(other_func)
# other_func3('Hello World3')

def f2(func):
    def inner_func(*args):
        print('decorator f2')
        result = func(*args)
        return result
    return inner_func


f0 = f1('arg')  # デコレーターを取得

# @f1('arg')
@f0
@f2
def func():
    print('func')

# func()

def func2():
    print('func2')

func2 = f1('arg')(f2(func2))

print('--- calling func ---')
func()
print('--- calling func2 ---')
func2()