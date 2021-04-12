using System;
using System.Linq;

namespace Nerd_STF
{
    public static class Hashes
    {
        public static int Default(object obj)
        {
            return obj.GetHashCode();
        }
        public static string MD5(string input)
        {
            var md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputB = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputB);

            var builder = new System.Text.StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("X2"));
            }
            return builder.ToString();
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
    }

    public static class Miscellaneous
    {
        public static int SyllableCount(string input)
        {
            // Starter code by KeithS on StackExchange.

            input = input.ToLower().Trim();
            bool lastWasVowel = false;
            char[] vowels = new[] { 'a', 'e', 'i', 'o', 'u', 'y' };
            int count = 0;

            foreach (var c in input)
            {
                if (vowels.Contains(c))
                {
                    if (!lastWasVowel) count++;
                    lastWasVowel = true;
                }
                else lastWasVowel = false;
            }

            if ((input.EndsWith("e") || input.EndsWith("es") || input.EndsWith("ed")) && !input.EndsWith("le")) count--;

            return count;
        }
    }

    public static class Stats
    {
        public static readonly string Creator = "That_One_Nerd"; 
        public static readonly string[] Links = new[]
        {   "Discord: https://discord.gg/ySXMtWDTYY/",
            "Github: https://https://github.com/that-one-nerd",
            "Itch: https://that-one-nerd.itch.io/" 
        };
        public static readonly string Version = "2021.0";
    }

    public struct Optional<T>
    {
        public bool Exists
        {
            get
            {
                return Value != null;
            }
        }
        public T Value { get; internal set; }

        public Optional(T input)
        {
            Value = input;
        }

        public static explicit operator T(Optional<T> input)
        {
            return input.Value;
        }
        public static explicit operator Optional<T>(T input)
        {
            return new Optional<T>(input);
        }
    }
}