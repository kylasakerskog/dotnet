from struct import unpack
import sys
import os
# Read fixed number of bytes
def read_bytes(f, l):
    bytes = f.read(l)
    if len(bytes) != l:
           raise Exception("Not enough bytes in stream")
    return bytes

# Unpack a 4-byte network byte order integer
def read_int(f):
    return unpack("!i", read_bytes(f, 4))[0]
   # Read a single byte

def read_byte(f):
    return ord(read_bytes(f, 1))

filename = sys.argv[1]
file_size = os.path.getsize(filename)
f = open(filename, "rb")
magic_value = read_bytes(f, 4)
if magic_value != b'BINX':
    f.seek(0)
else:
    print("Magic: "+ str(magic_value))


# Keep reading until we run out of file
while f.tell() < file_size:
    length = read_int(f)
    unk1 = read_int(f)
    unk2 = read_byte(f)
    data = read_bytes(f, length - 1)
    print("Unk2: %d, Data: %s"
          % (unk2, data))
