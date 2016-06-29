using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;
using LuaInterface;

namespace MyFrameWork {
    public class ByteBuffer {
        MemoryStream stream = null;
        BinaryWriter writer = null;
        BinaryReader reader = null;

        public ByteBuffer() {
            stream = new MemoryStream();
            writer = new BinaryWriter(stream);
        }

        public ByteBuffer(byte[] data) {
            if (data != null) {
                stream = new MemoryStream(data);
                reader = new BinaryReader(stream);
            } else {
                stream = new MemoryStream();
                writer = new BinaryWriter(stream);
            }
        }

        public void Close() {
            if (writer != null) writer.Close();
            if (reader != null) reader.Close();

            stream.Close();
            writer = null;
            reader = null;
            stream = null;
        }

        public void WriteByte(byte v) {
            writer.Write(v);
        }

        public void WriteInt(int v) {
            writer.Write((int)v);
        }

        public void WriteShort(ushort v) {
            byte[] temp = BitConverter.GetBytes(v);
            Array.Reverse(temp);
            writer.Write(BitConverter.ToInt16(temp, 0));
        }

        public void WriteLong(long v) {
            writer.Write((long)v);
        }

        public void WriteFloat(float v) {
            byte[] temp = BitConverter.GetBytes(v);
            Array.Reverse(temp);
            writer.Write(BitConverter.ToSingle(temp, 0));
        }

        public void WriteDouble(double v) {
            byte[] temp = BitConverter.GetBytes(v);
            Array.Reverse(temp);
            writer.Write(BitConverter.ToDouble(temp, 0));
        }

        public void WriteString(string v) {
            byte[] bytes = Encoding.UTF8.GetBytes(v);
            byte[] temp = BitConverter.GetBytes(bytes.Length);
            Array.Reverse(temp);
            writer.Write(BitConverter.ToInt16(temp, 0));
            writer.Write(bytes);
        }

        public void WriteBytes(byte[] v) {
//             byte[] temp = BitConverter.GetBytes(v.Length);
//             Array.Reverse(temp);
//             writer.Write(BitConverter.ToInt32(temp, 0));
            writer.Write(v);
        }

        public void WriteBuffer(LuaByteBuffer strBuffer) {
           WriteBytes(strBuffer.buffer);
        }

        public byte ReadByte() {
            return reader.ReadByte();
        }

        public int ReadInt() {
            byte[] temp = BitConverter.GetBytes(reader.ReadInt32());
            Array.Reverse(temp);
            return (int)BitConverter.ToInt32(temp,0);
        }

        public ushort ReadShort()
        {
            byte[] temp = BitConverter.GetBytes(reader.ReadInt16());
            Array.Reverse(temp);
            return (ushort)BitConverter.ToInt16(temp, 0);
        }

        public long ReadLong() {
            return (long)reader.ReadInt64();
        }

        public float ReadFloat() {
            byte[] temp = BitConverter.GetBytes(reader.ReadSingle());
            Array.Reverse(temp);
            return BitConverter.ToSingle(temp, 0);
        }

        public double ReadDouble() {
            byte[] temp = BitConverter.GetBytes(reader.ReadDouble());
            Array.Reverse(temp);
            return BitConverter.ToDouble(temp, 0);
        }

        public string ReadString() {
            ushort len = ReadShort();
            byte[] buffer = new byte[len];
            buffer = reader.ReadBytes(len);
            return Encoding.UTF8.GetString(buffer);
        }

        public byte[] ReadBytes()
        {
            byte[] final = reader.ReadBytes(ReadInt());
            return final;
        }

        public LuaByteBuffer ReadBuffer() {
            byte[] bytes = ReadBytes();
            return new LuaByteBuffer(bytes);
        }

        public byte[] ToBytes() {
            writer.Flush();
            return stream.ToArray();
        }

        public void Flush() {
            writer.Flush();
        }
    }
}