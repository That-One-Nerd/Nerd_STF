namespace Nerd_STF.Mathematics;

public readonly struct BigInteger/* : IAbsolute<BigInteger>, IAverage<BigInteger>, IClamp<BigInteger>,
    IComparable<BigInteger>, IComparable<byte>, IComparable<sbyte>, IComparable<short>, IComparable<ushort>,
    IComparable<int>, IComparable<uint>, IComparable<long>, IComparable<ulong>, IComparable<Half>,
    IComparable<float>, IComparable<double>, IComparable<decimal>, IDivide<BigInteger>, IEquatable<BigInteger>,
    IEquatable<byte>, IEquatable<sbyte>, IEquatable<short>, IEquatable<ushort>, IEquatable<int>, IEquatable<uint>,
    IEquatable<long>, IEquatable<ulong>, IEquatable<Half>, IEquatable<float>, IEquatable<double>,
    IEquatable<decimal>, ILerp<BigInteger, float>, IMathOperators<BigInteger>, IMax<BigInteger>,
    IMedian<BigInteger>, IMin<BigInteger>, IPresets1d<BigInteger>, IProduct<BigInteger>, ISubtract<BigInteger>,
    ISum<BigInteger>*/
{
    public static BigInteger One => new(false, new byte[] { 1 });
    public static BigInteger Zero => new(false, Array.Empty<byte>());

    public bool IsNegative => p_sign;
    public bool IsPositive => !p_sign;

    public int Length => p_data.Length;
    public int Sign => p_sign ? -1 : 1;

    private readonly byte[] p_data;
    private readonly bool p_sign;

    private BigInteger(bool sign, byte[] data)
    {
        p_data = data;
        p_sign = sign;
    }
    public BigInteger(byte val)
    {
        p_sign = false;
        p_data = new byte[] { val };
    }
    public BigInteger(sbyte val)
    {
        p_sign = Math.Sign(val) < 0;
        p_data = new byte[] { (byte)Math.Abs(val) };
    }
    public BigInteger(ushort val)
    {
        p_sign = false;
        p_data = BitConverter.GetBytes(val);
    }
    public BigInteger(short val)
    {
        p_sign = Math.Sign(val) < 0;
        p_data = BitConverter.GetBytes(Math.Abs(val));
    }
    public BigInteger(uint val)
    {
        p_sign = false;
        p_data = BitConverter.GetBytes(val);
    }
    public BigInteger(int val)
    {
        p_sign = Math.Sign(val) < 0;
        p_data = BitConverter.GetBytes(Math.Abs(val));
    }
    public BigInteger(ulong val)
    {
        p_sign = false;
        p_data = BitConverter.GetBytes(val);
    }
    public BigInteger(long val)
    {
        p_sign = Math.Sign(val) < 0;
        p_data = BitConverter.GetBytes(Math.Abs(val));
    }

    public byte[] GetAbsoluteData() => p_data;

    public sbyte ToInt8()
    {
        byte[] data = GetDataOfSize(sizeof(sbyte));
        sbyte absNum = (sbyte)data[0];
        return p_sign ? (sbyte)-absNum : absNum;
    }
    public byte ToUInt8() => (byte)ToInt8();
    public short ToInt16()
    {
        byte[] data = GetDataOfSize(sizeof(short));
        short absNum = BitConverter.ToInt16(data);
        return p_sign ? (short)-absNum : absNum;
    }
    public ushort ToUInt16() => (ushort)ToInt16();
    public int ToInt32()
    {
        byte[] data = GetDataOfSize(sizeof(int));
        int absNum = BitConverter.ToInt32(data);
        return p_sign ? -absNum : absNum;
    }
    public uint ToUInt32() => (uint)ToInt32();
    public long ToInt64()
    {
        byte[] data = GetDataOfSize(sizeof(long));
        long absNum = BitConverter.ToInt64(data);
        return p_sign ? -absNum : absNum;
    }
    public ulong ToUInt64() => (ulong)ToInt64();

    public int CompareTo(byte other) => CompareTo((BigInteger)other);
    public int CompareTo(sbyte other) => CompareTo((BigInteger)other);
    public int CompareTo(ushort other) => CompareTo((BigInteger)other);
    public int CompareTo(short other) => CompareTo((BigInteger)other);
    public int CompareTo(uint other) => CompareTo((BigInteger)other);
    public int CompareTo(int other) => CompareTo((BigInteger)other);
    public int CompareTo(ulong other) => CompareTo((BigInteger)other);
    public int CompareTo(long other) => CompareTo((BigInteger)other);
    public int CompareTo(Half other) => CompareTo((BigInteger)other);
    public int CompareTo(float other) => CompareTo((BigInteger)other);
    public int CompareTo(double other) => CompareTo((BigInteger)other);
    public int CompareTo(decimal other) => CompareTo((BigInteger)other);
    public int CompareTo(BigInteger other)
    {
        if (p_sign ^ other.p_sign) return p_sign ? -1 : 1;

        int compare = p_data.Length.CompareTo(other.p_data.Length);
        if (compare != 0) return compare;

        for (int i = p_data.Length - 1; i >= 0; i--)
        {
            compare = p_data[i].CompareTo(other.p_data[i]);
            if (compare != 0) return compare;
        }

        return 0;
    }
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is null) return false;
        else if (obj is BigInteger val) return Equals(val);
        else if (obj is byte valUInt8) return Equals(valUInt8);
        else if (obj is sbyte valInt8) return Equals(valInt8);
        else if (obj is ushort valUInt16) return Equals(valUInt16);
        else if (obj is short valInt16) return Equals(valInt16);
        else if (obj is uint valUInt32) return Equals(valUInt32);
        else if (obj is int valInt32) return Equals(valInt32);
        else if (obj is ulong valUInt64) return Equals(valUInt64);
        else if (obj is long valInt64) return Equals(valInt64);
        else if (obj is Half valHalf) return Equals(valHalf);
        else if (obj is float valSingle) return Equals(valSingle);
        else if (obj is double valDouble) return Equals(valDouble);
        else if (obj is decimal valDecimal) return Equals(valDecimal);
        return base.Equals(obj);
    }
    public bool Equals(byte other) => Equals((BigInteger)other);
    public bool Equals(sbyte other) => Equals((BigInteger)other);
    public bool Equals(ushort other) => Equals((BigInteger)other);
    public bool Equals(short other) => Equals((BigInteger)other);
    public bool Equals(uint other) => Equals((BigInteger)other);
    public bool Equals(int other) => Equals((BigInteger)other);
    public bool Equals(ulong other) => Equals((BigInteger)other);
    public bool Equals(long other) => Equals((BigInteger)other);
    public bool Equals(Half other) => Equals((BigInteger)other);
    public bool Equals(float other) => Equals((BigInteger)other);
    public bool Equals(double other) => Equals((BigInteger)other);
    public bool Equals(decimal other) => Equals((BigInteger)other);
    public bool Equals(BigInteger other) => p_sign == other.p_sign;
    public override int GetHashCode() => base.GetHashCode();
    public override string ToString()
    {
        return ToInt64().ToString(); // This isn't good because it doesn't work for numbers larger than 2^63
    }

    private byte[] GetDataOfSize(int bytes)
    {
        byte[] data = new byte[bytes];
        Array.Copy(p_data, data, Mathf.Min(p_data.Length, bytes));
        return data;
    }

    private int CompareAbs(BigInteger other)
    {
        // TODO: replace with a slightly generalized public method.

        int compare = p_data.Length.CompareTo(other.p_data.Length);
        if (compare != 0) return compare;

        for (int i = p_data.Length - 1; i >= 0; i--)
        {
            compare = p_data[i].CompareTo(other.p_data[i]);
            if (compare != 0) return compare;
        }

        return 0;
    }
    private byte[] TrimmedData()
    {
        int start = 0;
        while (start < p_data.Length && p_data[start] == 0) start++;
        return p_data.Skip(start).ToArray();
    }

    public static BigInteger Absolute(BigInteger val) => new(false, val.p_data);
    public static BigInteger Max(params BigInteger[] vals)
    {
        if (vals.Length < 1) return Zero;
        BigInteger val = vals[0];
        foreach (BigInteger b in vals) val = b > val ? b : val;
        return val;
    }

    public static bool operator ==(BigInteger a, BigInteger b) => a.Equals(b);
    public static bool operator !=(BigInteger a, BigInteger b) => !a.Equals(b);
    public static bool operator >(BigInteger a, BigInteger b) => a.CompareTo(b) > 0;
    public static bool operator <(BigInteger a, BigInteger b) => a.CompareTo(b) < 0;
    public static bool operator >=(BigInteger a, BigInteger b) => a == b || a > b;
    public static bool operator <=(BigInteger a, BigInteger b) => a == b || a < b;

    public static implicit operator BigInteger(byte val) => new(val);
    public static implicit operator BigInteger(sbyte val) => new(val);
    public static implicit operator BigInteger(ushort val) => new(val);
    public static implicit operator BigInteger(short val) => new(val);
    public static implicit operator BigInteger(uint val) => new(val);
    public static implicit operator BigInteger(int val) => new(val);
    public static implicit operator BigInteger(ulong val) => new(val);
    public static implicit operator BigInteger(long val) => new(val);
    public static explicit operator BigInteger(Half val) => new((int)val);
    public static explicit operator BigInteger(float val) => new((int)val);
    public static explicit operator BigInteger(double val) => new((long)val);
    public static explicit operator BigInteger(decimal val) => new((long)val);
    public static explicit operator byte(BigInteger val) => val.ToUInt8();
    public static explicit operator sbyte(BigInteger val) => val.ToInt8();
    public static explicit operator ushort(BigInteger val) => val.ToUInt16();
    public static explicit operator short(BigInteger val) => val.ToInt16();
    public static explicit operator uint(BigInteger val) => val.ToUInt32();
    public static explicit operator int(BigInteger val) => val.ToInt32();
    public static explicit operator ulong(BigInteger val) => val.ToUInt64();
    public static explicit operator long(BigInteger val) => val.ToInt64();
    public static explicit operator Half(BigInteger val) => (Half)val.ToInt32();
    public static explicit operator float(BigInteger val) => val.ToInt32();
    public static explicit operator double(BigInteger val) => val.ToInt64();
    public static explicit operator decimal(BigInteger val) => val.ToInt64();

    public static BigInteger operator +(BigInteger a, BigInteger b)
    {
        // Add padding to integer data to keep lists the same size.
        int desiredLength = Mathf.Max(a.Length, b.Length) + 1;

        List<byte> dataA = new(a.p_data),
                   dataB = new(b.p_data),
                   dataC = new();

        while (dataA.Count < desiredLength) dataA.Add(0x00);
        while (dataB.Count < desiredLength) dataB.Add(0x00);

        // Add the data of A and B from left to right. Roll over remainders.
        sbyte remainder = 0;
        byte result;
        for (int i = 0; i < desiredLength - 1; i++)
        {
            short sum = (short)(dataA[i] * a.Sign + dataB[i] * b.Sign + remainder);
            remainder = (sbyte)(sum >> 8);
            result = (byte)(sum & 0xFF);
            dataC.Add(result);
        }
        dataC.Add((byte)Math.Abs(remainder)); // Add the remainder for the last addition.

        bool sign;
        if (!(a.p_sign ^ b.p_sign)) sign = a.p_sign;
        else
        {
            bool aBigger = Absolute(a) > Absolute(b);
            if (aBigger) sign = a.p_sign;
            else sign = b.p_sign;
        }

        return new(sign, dataC.ToArray());
    }
}
