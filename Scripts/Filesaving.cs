using System;
using Nerd_STF.Lists;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Nerd_STF.Filesaving
{
    [Obsolete(nameof(BinaryFile) + " uses the " + nameof(BinaryFormatter) + ", which is considered dangerous. Go to 'https://aka.ms/binaryformatter/' for more information.")]
    public class BinaryFile : File<object>
    {
        public BinaryFile(string path) => Path = path;
        public BinaryFile(string path, object data)
        {
            Data = data;
            Path = path;
        }

        public static BinaryFile Load(string path)
        {
            BinaryFile file = new(path);
            FileStream stream = new(path, FileMode.Open);
            BinaryFormatter formatter = new();
            file.Data = formatter.Deserialize(stream);
            stream.Close();
            return file;
        }

        public override void Erase() => Data = null;
        public override void Load(bool erase = true)
        {
            if (erase) Erase();
            FileStream stream = new(Path, FileMode.Open);
            BinaryFormatter formatter = new();
            Data = formatter.Deserialize(stream);
            stream.Close();
        }
        public override void Save()
        {
            FileStream stream = new(Path, FileMode.Create);
            BinaryFormatter formatter = new();
            formatter.Serialize(stream, Data);
            stream.Close();
        }
    }
    public class ByteFile : File<List<byte>>
    {
        public byte this[int index]
        {
            get
            {
                if (index < 0 || index >= Data.Length) throw new ArgumentOutOfRangeException(nameof(index));
                return Data[index];
            }
            set { Data[index] = value; }
        }

        public ByteFile(string path) => Path = path;
        public ByteFile(string path, byte[] data)
        {
            Data = new List<byte>(data);
            Path = path;
        }
        public ByteFile(string path, List<byte> data)
        {
            Data = data;
            Path = path;
        }

        public static ByteFile Load(string path)
        {
            ByteFile file = new(path);
            FileStream stream = new(file.Path, FileMode.Open);
            for (long i = 0; i < stream.Length; i++) file.Data.Add((byte)stream.ReadByte());
            stream.Close();
            return file;
        }

        public override void Erase() => Data = new();
        public void Fill(int length, byte fill = 0) => Data = new List<byte>(length, fill);
        public override void Load(bool erase = true)
        {
            if (erase) Erase();
            FileStream stream = new(Path, FileMode.Open);
            for (long i = 0; i < stream.Length; i++) Data.Add((byte)stream.ReadByte());
            stream.Close();
        }
        public void Remove(int start, int amount)
        {
            List<byte> old = Data;
            Data = new List<byte>(old.Length - amount);
            for (int i = 0; i < old.Length; i++)
            {
                if (i > start && i < start + amount) i = start + amount;
                Data[i] = old[i];
            }
        }
        public override void Save()
        {
            FileStream stream = new(Path, FileMode.Create);
            foreach (byte b in Data) stream.WriteByte(b);
            stream.Close();
        }
        public void Write(byte write, bool toFile = false)
        {
            Data += write;
            if (toFile)
            {
                FileStream stream = new(Path, FileMode.Append);
                stream.WriteByte(write);
                stream.Close();
            }
        }
        public void Write(List<byte> write, bool toFile = false)
        {
            Data += write;
            if (toFile)
            {
                FileStream stream = new(Path, FileMode.Append);
                foreach (byte b in write) stream.WriteByte(b);
                stream.Close();
            }
        }
    }
    public class TextFile : File<string>
    {
        public TextFile(string path) => Path = path;
        public TextFile(string path, string data)
        {
            Data = data;
            Path = path;
        }

        public static TextFile Load(string path)
        {
            TextFile file = new(path);
            FileStream stream = new(file.Path, FileMode.Open);
            for (long i = 0; i < stream.Length; i++) file.Data += ((char)stream.ReadByte());
            stream.Close();
            return file;
        }

        public override void Erase() => Data = "";
        public override void Load(bool erase = true)
        {
            if (erase) Erase();
            FileStream stream = new(Path, FileMode.Open);
            for (long i = 0; i < stream.Length; i++) Data += (char)stream.ReadByte();
            stream.Close();
        }
        public void Remove(int start, int amount) => Data = Data.Remove(start, amount);
        public override void Save()
        {
            FileStream stream = new(Path, FileMode.Create);
            foreach (byte b in Data) stream.WriteByte(b);
            stream.Close();
        }
        public void Write(char write, bool toFile = false)
        {
            Data += write;
            if (toFile)
            {
                FileStream stream = new(Path, FileMode.Append);
                stream.WriteByte((byte)write);
                stream.Close();
            }
        }
        public void Write(string write, bool toFile = false)
        {
            Data += write;
            if (toFile)
            {
                FileStream stream = new(Path, FileMode.Append);
                foreach (byte b in write) stream.WriteByte(b);
                stream.Close();
            }
        }
    }

    public abstract class File<T>
    {
        public T Data { get; set; }
        public string Path { get; set; }

        public abstract void Erase();
        public abstract void Load(bool erase = true);
        public abstract void Save();
    }
}