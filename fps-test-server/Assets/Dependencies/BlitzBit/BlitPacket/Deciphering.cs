
using System;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BlitzBit {

    public partial class BlitPacket {

        private byte[] byteBuffer = new byte[0];

        private void CheckBuffers () {

            if (byteBuffer.Length != buffer.Count)
                byteBuffer = buffer.ToArray();
        }

        private int index;

        public string GetString () { CheckBuffers();

            int size = BitConverter.ToInt32(byteBuffer, index); index += 4;

            byte[] textBuf = new byte[size];
            Buffer.BlockCopy(byteBuffer, index, textBuf, 0, size); index += size;

            return Encoding.Unicode.GetString(textBuf);
        }

        public byte[] GetByteArray () { CheckBuffers();

            int size = BitConverter.ToInt32(byteBuffer, index); index += 4;

            byte[] outBuf = new byte[size];
            Buffer.BlockCopy(byteBuffer, index, outBuf, 0, size); index += size;

            return outBuf;
        }

        // public System.SByte GetSByte () { CheckBuffers();
        //
        //     System.SByte val = BitConverter.ToSByte(byteBuffer, index); index += 1;
        //
        //     return val;
        // }
        // public System.Byte GetByte () { CheckBuffers();
        //
        //     System.Byte val = BitConverter.ToByte(byteBuffer, index); index += 1;
        //
        //     return val;
        // }

        public System.Int16 GetInt16 () { CheckBuffers();

            System.Int16 val = BitConverter.ToInt16(byteBuffer, index); index += 2;

            return val;
        }
        public System.UInt32 GetUInt16 () { CheckBuffers();

            System.UInt32 val = BitConverter.ToUInt16(byteBuffer, index); index += 2;

            return val;
        }

        public System.Int32 GetInt32 () { CheckBuffers();

            System.Int32 val = BitConverter.ToInt32(byteBuffer, index); index += 4;

            return val;
        }
        public System.UInt32 GetUInt32 () { CheckBuffers();

            System.UInt32 val = BitConverter.ToUInt32(byteBuffer, index); index += 4;

            return val;
        }

        public System.Int64 GetInt64 () { CheckBuffers();

            System.Int64 val = BitConverter.ToInt64(byteBuffer, index); index += 8;

            return val;
        }
        public System.UInt64 GetUInt64 () { CheckBuffers();

            System.UInt64 val = BitConverter.ToUInt64(byteBuffer, index); index += 8;

            return val;
        }

        public System.Single GetSingle () { CheckBuffers();

            System.Single val = BitConverter.ToSingle(byteBuffer, index); index += 4;

            return val;
        }
        public System.Double GetDouble () { CheckBuffers();

            System.Double val = BitConverter.ToDouble(byteBuffer, index); index += 8;

            return val;
        }
        // public System.Decimal GetDecimal () { CheckBuffers();
        //
        //     System.Decimal val = BitConverter.ToDecimal(byteBuffer, index); index += 16;
        //
        //     return val;
        // }

        public object GetObject () { CheckBuffers();

            int size = BitConverter.ToInt32(byteBuffer, index); index += 4;

            byte[] objBuf = new byte[size];
            Buffer.BlockCopy(byteBuffer, index, objBuf, 0, size); index += size;

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();

            memoryStream.Write(objBuf, 0, objBuf.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return binaryFormatter.Deserialize(memoryStream);
        }
    }
}
