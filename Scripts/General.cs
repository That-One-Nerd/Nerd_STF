using System;
using System.Text;

namespace Nerd_STF
{
    public static class Hashes
    {
        public static int Default(object obj) => obj.GetHashCode(); 
        public static byte[] MD5(byte[] input) => System.Security.Cryptography.MD5.Create().ComputeHash(input);
        public static string MD5(string input)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputB = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputB);

            string s = "";
            for (int i = 0; i < hash.Length; i++) s += hash[i].ToString("X2");
            return s;
        }
        public static uint SchechterTurbulence(uint seed)
        {
            seed ^= 2747636419u;
            seed *= 2654435769u;
            seed ^= seed >> 16;
            seed *= 2654435769u;
            seed ^= seed >> 16;
            seed *= 2654435769u;

            return seed;
        }
        public static byte[] SHA1(byte[] input) => System.Security.Cryptography.SHA1.Create().ComputeHash(input);
        public static string SHA1(string input)
        {
            System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create();

            byte[] inputB = Encoding.ASCII.GetBytes(input);
            byte[] hash = sha1.ComputeHash(inputB);

            string s = "";
            for (int i = 0; i < hash.Length; i++) s += hash[i].ToString("X2");
            return s;
        }
        public static byte[] SHA256(byte[] input) => System.Security.Cryptography.SHA256.Create().ComputeHash(input);
        public static string SHA256(string input)
        {
            System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create();

            byte[] inputB = Encoding.ASCII.GetBytes(input);
            byte[] hash = sha256.ComputeHash(inputB);

            string s = "";
            for (int i = 0; i < hash.Length; i++) s += hash[i].ToString("X2");
            return s;
        }
        public static byte[] SHA384(byte[] input) => System.Security.Cryptography.SHA384.Create().ComputeHash(input);
        public static string SHA384(string input)
        {
            System.Security.Cryptography.SHA384 sha384 = System.Security.Cryptography.SHA384.Create();

            byte[] inputB = Encoding.ASCII.GetBytes(input);
            byte[] hash = sha384.ComputeHash(inputB);

            string s = "";
            for (int i = 0; i < hash.Length; i++) s += hash[i].ToString("X2");
            return s;
        }
        public static byte[] SHA512(byte[] input) => System.Security.Cryptography.SHA512.Create().ComputeHash(input);
        public static string SHA512(string input)
        {
            System.Security.Cryptography.SHA512 sha512 = System.Security.Cryptography.SHA512.Create();

            byte[] inputB = Encoding.ASCII.GetBytes(input);
            byte[] hash = sha512.ComputeHash(inputB);

            string s = "";
            for (int i = 0; i < hash.Length; i++) s += hash[i].ToString("X2");
            return s;
        }
    }

    public static class Misc
    {
        public static string PlaceMaker(int num)
        {
            return num.ToString()[^1] switch
            {
                '1' => num + "st",
                '2' => num + "nd",
                '3' => num + "rd",
                _ => num + "th",
            };
        }
    }

    public static class Stats
    {
        public static readonly string Creator = "That_One_Nerd";
        public static readonly string[] Links = new[]
        {   
            "Discord: https://discord.gg/ySXMtWDTYY/",
            "Github: https://https://github.com/that-one-nerd",
            "Itch: https://that-one-nerd.itch.io/"
        };
        public static readonly string Version = "2021.2";
    }

    [Serializable]
    public struct Optional<T>
    {
        public bool Exists => Value != null;
        public T Value { get; internal set; }

        public Optional(T input) => Value = input;

        public static explicit operator T(Optional<T> input) => input.Value;
        public static explicit operator Optional<T>(T input) => new(input);
    }
}