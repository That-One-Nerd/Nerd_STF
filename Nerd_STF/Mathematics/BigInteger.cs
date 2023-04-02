using System.Numerics;

namespace Nerd_STF.Mathematics;

public readonly struct BigInteger/* : IAbsolute<BigInteger>, IAverage<BigInteger>, IClamp<BigInteger>, IDivide<BigInteger>,
    ILerp<BigInteger, float>, IMathOperators<BigInteger>, IMax<BigInteger>, IMedian<BigInteger>, IMin<BigInteger>,
    IPresets1d<BigInteger>, IProduct<BigInteger>, ISubtract<BigInteger>, ISum<BigInteger>*/
{
    public static BigInteger One => new(false, new byte[] { 1 });
    public static BigInteger Zero => new(false, Array.Empty<byte>());

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

    private byte[] GetDataOfSize(int bytes)
    {
        byte[] data = new byte[bytes];
        Array.Copy(p_data, data, Mathf.Min(p_data.Length, bytes));
        return data;
    }
}
