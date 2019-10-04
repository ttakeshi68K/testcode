#def get_dimension(filename):
#    f = open(filename, 'rb')
#    spec = f.read(6).decode()
#    width = int.from_bytes(f.read(2), 'little')
#    height = int.from_bytes(f.read(2), 'little')
#    f.close()
#    print(f'this file is {spec}, size: {width} x {height}')

#get_dimension('di-pybasic3801.gif')

from struct import pack, unpack, calcsize

#data = pack('b', 100)
#print(data)
#print(f"100 == b'{chr(100)}'")

#result = unpack('b', data)
#print(result)

#data = pack('f', 1.1)
#print(data)

mydata = [(1, 'FOO', 1023), (2, 'BAR', 80), (3, 'BAZ', 4000)]

#result = pack('l3sl', mydata[0][0], mydata[0][1].encode(), mydata[0][2])
#print(result)

myfile = open('mydata.data', 'wb')

for item in mydata:
    result = pack('l3sl', item[0], item[1].encode(), item[2])
    myfile.write(result)

myfile.close()

size = calcsize('l3sl')
myfile = open('mydata.data', 'rb')

content = myfile.read(size)
while content:
    restored_data = unpack('l3sl', content)
    print(restored_data)
    content = myfile.read(size)

myfile.close()
