def sub_genetator():
    value = 0
    while True:
        if value == 'stop':
            return -1
        else:
            result = sum(range(value + 1))
            value = yield result
            
def outer_geniter():
    value = yield from sub_genetator()
    yield value


mygeniter = outer_geniter()
print(next(mygeniter))  # これによりサブジェネレータのコードが実行開始

print(mygeniter.send(5))
print(mygeniter.send('stop')) 
