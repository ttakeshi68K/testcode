class MyStack:
    def __init__(self, *args):
        self.stack = []
        #for item in args:
        #    self.stack.append(item)
        self.stack.extend(args)
    def push(self, item):
        self.stack.append(item)
    def pop(self):
        # result = self.stack[-1]
        # del self.stack[-1]
        # return resul
        if len(self.stack) == 0:
            return None
        return self.stack.pop() 
    def __repr__(self):
        return 'MyStack(' + repr(self.stack)  + ')'
    def __str__(self):
        return str(self.stack)
    def __iter__(self):
        return iter(self.stack)
    def __getitem__(self, KEY):
        return self.stack[KEY]

class MyQueue:
    def __init__(self):
        self.queue = []
    def enqueue(self, item):
        self.queue.append(item)
    def dequeue(self):
        if len(self.queue) == 0:
            return None
        result = self.queue[0]
        del self.queue[0]
        return result

     
#myq = MyQueue()
#myq.enqueue(0)
#myq.enqueue(1)
#print(myq.dequeue())
#print(myq.dequeue())
#print(myq.dequeue())

mystack = MyStack(1, 2, [3, 4])

print(str(mystack))
print(repr(mystack))

for item in mystack:
    print(item)

print(mystack[0])
print(mystack[0:2])  # スライスできるか？

#print(mystack.pop())
#mystack.push(0)
#mystack.push(1)
#mystack.push(2)
print(mystack.stack)
#print(mystack.pop())
#print(mystack.pop())
#print(mystack.stack)

