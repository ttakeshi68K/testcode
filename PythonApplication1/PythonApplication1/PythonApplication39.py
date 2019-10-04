from struct import pack, unpack, calcsize

def get_dimension_with_struct(filename):
    myfile = open(filename, 'rb')
    content = myfile.read(10)
    myfile.close()
    (spec, width, height) = unpack('6shh', content)
    print(f'this file is {spec.decode()}, size: {width} x {height}')

get_dimension_with_struct('di-pybasic3801.gif')
