# Version 2021.0:
    The Original Release of the Library.
    Includes:
        * Nerd_STF:
            * public static class Hashes
            * Nerd_STF.Interfaces:
                * public interface INegatives<T>
            * Nerd_STF.Lists:
                * public class List<T>
                * public class Matrix<T>
            * Nerd_STF.Mathematics:
                * public struct Angle
                * public struct Color
                * public struct ColorByte
                * public static class Math
                * public struct Percent
                * public struct Vector
                * public struct Vector2
                * public struct Vector3
                * public struct Vector4
            * public class Miscellaneous
            * public struct Optional<T>
            * public static class Stats
            
# Version 2021.1:
    This update is mainly centered around files and filesaving.
    * Nerd_STF
        + Nerd_STF.Filesaving
            + File<T>
            + BinaryFile
            + ByteFile
            + TextFile
        - public class Miscellaneous
        
# Version 2021.2:
    This update is centered around lists.
        * Nerd_STF:
            * public static class Hashes:
                + public static string SHA1(string)
                + public static string SHA256(string)
                + public static string SHA384(string)
                + public static string SHA512(string)
                + public static byte[] MD5(byte[])
                + public static byte[] SHA1(byte[])
                + public static byte[] SHA256(byte[])
                + public static byte[] SHA384(byte[])
                + public static byte[] SHA512(byte[])
                = Made `public static string MD5(string)` include more of my own scripting
            * Nerd_STF.File:
                = Nerd_STF.Filesaving: Moved to Nerd_STF.File.Saving
                * Nerd_STF.File.Saving:
                    + public class BinaryFile<T>
                    * public class ByteFile:
                        + public override bool TryLoad(out File<List<byte>>)
                        = Made `public static ByteFile Load(string)` load files faster
                        = Made `public override void Load(bool)` load files faster
                        = Made `public override void Save()` save files faster
                        = Made `public override void Write(byte, bool)` save files faster
                        = Made `public override void Write(List<byte>, bool)` save files faster
                    * public abstract class File<T>:
                        + public bool Exists;
                        + public abstract bool TryLoad(out File<T>)
                    * public class TextFile:
                        + public override bool TryLoad(out File<string>)
                        = Made `public static TextFile Load(string)` load files faster
                        = Made `public override void Load(bool)` load files faster
                        = Made `public override Save()` save files faster
                        = Made `public override void Write(char, bool)` save files faster
                        = Made `public override void Write(string, bool)` save files faster
            - Nerd_STF.Interfaces:
                = Moved `public interface INegatives<T>` to `Nerd_STF.Mathematics.Interfaces`
            * Nerd_STF.Lists:
                + public class ReadOnlyList<T>
                = public class List<T>: Completely reworked everything in `List<T>`
                    + public int this[T] { get; set; }
                    + public List(IEnumerable)
                    + public bool IsEmpty { get; }
                    + public bool IsNull { get; }
                    + public bool IsNullOrEmpty { get; }
                    + public bool Contains(Predicate<T>)
                    + public int Count (Predicate<T>)
                    + public void AddRange(IEnumerable)
                    + public bool Any(Predicate<T>)
                    + public bool Any(T)
                    + public T Find(Predicate<T>)
                    + public T Find(Predicate<T>, int)
                    + public T Find(Predicate<T>, int, int)
                    + public T FindOrDefault(Predicate<T>)
                    + public T FindOrDefault(T)
                    + public T FindOrDefault(Predicate<T>, int)
                    + public T FindOrDefault(T, int)
                    + public T FindOrDefault(Predicate<T>, int, int)
                    + public T FindOrDefault(T, int, int)
                    + public List<T> FindAll(Predicate<T>)
                    + public List<T> FindAll(Predicate<T>, int)
                    + public List<T> FindAll(Predicate<T>, int, int)
                    + public List<T> FindLast(Predicate<T>)
                    + public List<T> FindLast(Predicate<T>, int)
                    + public List<T> FindLast(Predicate<T>, int, int)		
                    + public T FindLastOrDefault(Predicate<T>)
                    + public T FindLastOrDefault(T)
                    + public T FindLastOrDefault(Predicate<T>, int)
                    + public T FindLastOrDefault(T, int)
                    + public T FindLastOrDefault(Predicate<T>, int, int)
                    + public T FindLastOrDefault(T, int, int)
                    + public int FindIndex(Predicate<T>)
                    + public int FindIndex(Predicate<T>, int)
                    + public int FindIndex(Predicate<T>, int, int)
                    + public List<int> FindAllIndex(Predicate<T>)
                    + public List<int> FindAllIndex(Predicate<T>, int)
                    + public List<int> FindAllIndex(Predicate<T>, int, int)
                    + public int FindLastIndex(Predicate<T>)
                    + public int FindLastIndex(Predicate<T>, int)
                    + public int FindLastIndex(Predicate<T>, int, int)
                    + public bool MatchesAll(Predicate<T>)
                    + public void Remove(Predicate<T>)
                    + public void RemoveAll(Predicate<T>)
                    + public void RemoveAll(T)
                    + public void RemoveLast(Predicate<T>)
                    + public void RemoveLast(T)
                    + IEnumerator<T> IEnumerable<T>.GetEnumerator()
                    + public void Randomize()
                    + public List<int> FindAllIndex()
                    + public void Shuffle()
                    + public List<int> FindAllIndex(int)
                    + public List<int> FindAllIndex(int, int)
                    + public ReadOnlyList<T> ToReadOnly()
                    = Made `public List<T> Duplicate()` a readonly variable and better (`public List<T> Duplicate { get; }`)
                    = Renamed the internal array to `array,` as opposed to `list`
                    = Renamed `public void Add(T[])` to `public void AddRange(T[])`
                    = Renamed `public void Add(List<T>)` to `public void AddRange(List<T>)`
                    = Renamed `public bool Compare(T)` to `public bool Contains(T)`
                    = Renamed `public void Remove(int, bool)` to `public void Remove(int)`
                    = Renamed `public void SetAll(T)` to `public void Fill(T)`
                    = Made `public string ToString(bool)` count up from zero instead of one when the bool is set to true.
                    = Renamed `public ReadOnlyCollection<T> AsReadOnly()` to `public ReadOnlyCollection<T> ToSystemReadOnly()`
                    - public bool Check(int)
                    - public void Convert(T)
                    - public void Convert(T[])
                    - public void Convert(List<T>)
                    - public T Get(int)
                    - public void Get(int, out T)
                    - public T[] GetAll()
                    - public void Set(int, T)
                    - public void Set(T[])
                    - public void Set(List<T>)
                    - public static List<T> AllDefault(int)
                    - public static List<T> operator +(T[], List<T>)
                    - public static List<T> operator +(List<T>, int)
                    - public static List<T> operator +(T, List<T>)
                    - public static List<T> operator *(List<T>, int)
                    - public static explicit operator T[](List<T>)
                    - public static explicit operator List<T>(T[])
                = Marked `public class Nerd_STF.Lists.Matrix<T>` as deprecated. This class will be removed or heavily modified in a future release. Also removed all instances of removed List<T> methods and replaced them with work-arounds.
            * Nerd_STF.Mathematics:
                = Marked `public struct Angle` as serializable
                = Marked `public struct Color` as serializable
                = Marked `public struct ColorByte` as serializable
                = Marked `public struct Percent` as serializable
                = Marked `public struct Vector` as serializable
                = Marked `public struct Vector2` as serializable
                = Marked `public struct Vector3` as serializable
                = Marked `public struct Vector4` as serializable
            + public static class Misc
                + public static string PlaceMaker(int)
