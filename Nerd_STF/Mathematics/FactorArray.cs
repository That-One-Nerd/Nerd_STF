using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nerd_STF.Mathematics
{
    public class FactorArray : IEnumerable<int>, IEquatable<FactorArray>
    {
        public int Distinct => factorDict.Count;
        public int Count
        {
            get
            {
                int sum = 0;
                foreach (KeyValuePair<int, int> factor in factorDict)
                    sum += factor.Value;
                return sum;
            }
        }

        private readonly SortedDictionary<int, int> factorDict;

        internal FactorArray(IEnumerable<int> factors)
        {
            factorDict = new SortedDictionary<int, int>();
            foreach (int f in factors)
            {
                if (factorDict.TryGetValue(f, out int count)) factorDict[f] = count + 1;
                else factorDict.Add(f, 1);
            }
        }

        public IEnumerable<int> EnumerateFactors()
        {
            foreach (KeyValuePair<int, int> f in factorDict)
            {
                for (int i = 0; i < f.Value; i++) yield return f.Key;
            }
        }
        public IEnumerable<KeyValuePair<int, int>> GetFactors() => factorDict;
        public IEnumerable<int> GetDistinctFactors() => factorDict.Keys;
        public int GetMultiplicity(int factor)
        {
            if (factorDict.TryGetValue(factor, out int mult)) return mult;
            else return 0;
        }

        public int[] ToArray() => ToList().ToArray(); // Not weird syntax at all, wdym?
        public List<int> ToList()
        {
            List<int> total = new List<int>();
            foreach (KeyValuePair<int, int> f in factorDict)
            {
                for (int i = 0; i < f.Value; i++) total.Add(f.Key);
            }
            return total;
        }
        public Fill<int> ToFill()
        {
            int[] @copy = ToArray();
            return i => @copy[i];
        }

        public static FactorArray GetPrimeFactors(int num) => MathE.PrimeFactors(num);

        public IEnumerator<int> GetEnumerator() => EnumerateFactors().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

#if CS8_OR_GREATER
        public bool Equals(FactorArray? other)
#else
        public bool Equals(FactorArray other)
#endif
        {
            if (other is null) return false;
            else if (factorDict.Count != other.factorDict.Count) return false;
            else foreach (int key in factorDict.Keys)
            {
                if (!other.factorDict.TryGetValue(key, out int otherFactors)) return false;
                else if (factorDict[key] != otherFactors) return false;
            }
            return true;
        }
#if CS8_OR_GREATER
        public override bool Equals(object? obj)
#else
        public override bool Equals(object obj)
#endif
        {
            if (obj is FactorArray other) return Equals(other);
            else return false;
        }
        public override int GetHashCode() => factorDict.GetHashCode();
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            int count = 0;
            foreach (KeyValuePair<int, int> f in factorDict)
            {
                if (count > 0) result.Append(" * ");

                result.Append(f.Key);
                if (f.Value > 1) result.Append($"^{f.Value}");
                count++;
            }
            return result.ToString();
        }
    }
}
