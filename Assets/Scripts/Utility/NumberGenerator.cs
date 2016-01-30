using System;
using System.Security.Cryptography;

public class NumberGenerator : Singleton<NumberGenerator>
{
    private byte[] _randomBuffer;
    private int _bufferOffset;
    private RNGCryptoServiceProvider _generator;

    private NumberGenerator(int bufferSize = 32768) // must be a multiple of 4
    {
        _randomBuffer = new byte[bufferSize];
        _generator = new RNGCryptoServiceProvider();
        _bufferOffset = _randomBuffer.Length;
	}

    private void FillBuffer()
    {
        _generator.GetBytes(_randomBuffer);
        _bufferOffset = 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int Next()
    {
        if (_bufferOffset >= _randomBuffer.Length)
        {
            FillBuffer();
        }

        int val = BitConverter.ToInt32(_randomBuffer, _bufferOffset) & 0x7fffffff;
        _bufferOffset += sizeof(int);
        return val;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="maxValue"></param>
    /// <returns>A random integer value greater than or equal to zero and less than maxValue</returns>
    public int Next(int maxValue)
    {
        if (maxValue < 0)
        {
            throw new ArgumentOutOfRangeException("maxValue must be a non-negative number");
        }

        return Next() % maxValue;
    }

    public int Next(int minValue, int maxValue)
    {
        if (maxValue < minValue)
        {
            throw new ArgumentOutOfRangeException("maxValue must be greater than or equal to minValue");
        }
        int range = maxValue - minValue;
        return minValue + Next(range);
    }

    public double NextDouble()
    {
        int val = Next();
        return (double)val / int.MaxValue;
    }

    public void GetBytes(byte[] buff)
    {
        _generator.GetBytes(buff);
    }
}