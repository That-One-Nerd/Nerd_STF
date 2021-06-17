using System;
using System.Text;
using Nerd_STF.Lists;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Nerd_STF.File.Saving
{
    [Obsolete(nameof(BinaryFile) + " uses the " + nameof(BinaryFormatter) + ", which is considered dangerous. Go to 'https://aka.ms/binaryformatter/' for more information.")]
    [Serializable]
    public class BinaryFile
    {
        public object Data { get; set; }
        public string Path { get; set; }

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

        public void Erase() => Data = null;
        public void Load()
        {
            FileStream stream = new(Path, FileMode.Open);
            BinaryFormatter formatter = new();
            Data = formatter.Deserialize(stream);
            stream.Close();
        }
        public void Save()
        {
            FileStream stream = new(Path, FileMode.Create);
            BinaryFormatter formatter = new();
            formatter.Serialize(stream, Data);
            stream.Close();
        }
    }
    [Obsolete(nameof(BinaryFile) + " uses the " + nameof(BinaryFormatter) + ", which is considered dangerous. Go to 'https://aka.ms/binaryformatter/' for more information.")]
    [Serializable]
    public class BinaryFile<T>
    {
        public T Data { get; set; }
        public string Path { get; set; }

        public BinaryFile(string path) => Path = path;
        public BinaryFile(string path, T data)
        {
            Data = data;
            Path = path;
        }

        public static BinaryFile<T> Load(string path)
        {
            BinaryFile<T> file = new(path);
            FileStream stream = new(path, FileMode.Open);
            BinaryFormatter formatter = new();
            file.Data = (T)formatter.Deserialize(stream);
            stream.Close();
            return file;
        }

        public void Erase() => Data = default;
        public void Load()
        {
            FileStream stream = new(Path, FileMode.Open);
            BinaryFormatter formatter = new();
            Data = (T)formatter.Deserialize(stream);
            stream.Close();
        }
        public void Save()
        {
            FileStream stream = new(Path, FileMode.Create);
            BinaryFormatter formatter = new();
            formatter.Serialize(stream, Data);
            stream.Close();
        }
    }
    [Serializable]
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
        public ByteFile(string path, params byte[] data)
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
            byte[] b = new byte[stream.Length];
            while (stream.Read(b, 0, b.Length) > 0) ;
            file.Data = new(b);
            stream.Close();
            return file;
        }

        public override void Erase() => Data = new();
        public void Fill(int length, byte fill = 0) => Data = new List<byte>(length, fill);
        public override void Load(bool erase = true)
        {
            if (erase) Erase();
            FileStream stream = new(Path, FileMode.Open);
            byte[] b = new byte[stream.Length];
            while (stream.Read(b, 0, b.Length) > 0) ;
            Data.AddRange(b);
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
            stream.Write(Data.ToArray(), 0, Data.Length);
            stream.Close();
        }
        public override bool TryLoad(out File<List<byte>> file)
        {
            bool success = false;
            try
            {
                file = new ByteFile(Path);
                FileStream stream = new(file.Path, FileMode.Open);
                byte[] b = new byte[stream.Length];
                while (stream.Read(b, 0, b.Length) > 0) ;
                file.Data.AddRange(b);
                stream.Close();
                success = true;
            }
            catch { file = null; }

            return success;
        }
        public void Write(byte write, bool toFile = false)
        {
            Data += write;
            if (toFile) Save();
        }
        public override void Write(List<byte> write, bool toFile = false)
        {
            Data += write;
            if (toFile) Save();
        }
    }
    [Serializable]
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
            byte[] b = new byte[stream.Length];
            while (stream.Read(b, 0, b.Length) > 0) ;
            file.Data += Encoding.Default.GetString(b);
            stream.Close();
            return file;
        }

        public override void Erase() => Data = "";
        public override void Load(bool erase = true)
        {
            if (erase) Erase();
            FileStream stream = new(Path, FileMode.Open);
            byte[] b = new byte[stream.Length];
            while (stream.Read(b, 0, b.Length) > 0) ;
            Data += Encoding.Default.GetString(b);
            stream.Close();
        }
        public void Remove(int start, int amount) => Data = Data.Remove(start, amount);
        public override void Save()
        {
            FileStream stream = new(Path, FileMode.Create);
            byte[] b = Encoding.Default.GetBytes(Data);
            stream.Write(b, 0, b.Length);
            stream.Close();
        }
        public override bool TryLoad(out File<string> file)
        {
            bool success = false;
            try
            {
                file = new TextFile(Path);
                FileStream stream = new(file.Path, FileMode.Open);
                byte[] b = new byte[stream.Length];
                while (stream.Read(b, 0, b.Length) > 0) ;
                file.Data += Encoding.Default.GetString(b);
                stream.Close();
                success = true;
            }
            catch { file = null; }
            
            return success;
        }
        public void Write(char write, bool toFile = false)
        {
            Data += write;
            if (toFile) Save();
        }
        public override void Write(string write, bool toFile = false)
        {
            Data += write;
            if (toFile) Save();
        }
    }

    [Serializable]
    public abstract class File<T>
    {
        public T Data { get; set; }
        public bool Exists => System.IO.File.Exists(Path);
        public string Path { get; set; }

        public abstract void Erase();
        public abstract void Load(bool erase = true);
        public abstract void Save();
        public abstract bool TryLoad(out File<T> file);
        public abstract void Write(T write, bool toFile = false);
    }
}