using UnityEngine;

using System;
using System.Security.Cryptography;

public class NumberGenerator : Singleton<NumberGenerator>
{
    private readonly int BufferSize = 32768;  // must be a multiple of 4
    private readonly byte[] RandomBuffer;
    private int BufferOffset;
    private readonly RNGCryptoServiceProvider rng;

	private NumberGenerator()
    {
        RandomBuffer = new byte[BufferSize];
        rng = new RNGCryptoServiceProvider();
        BufferOffset = RandomBuffer.Length;
	}

    private void FillBuffer()
    {
        rng.GetBytes(RandomBuffer);
        BufferOffset = 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int Next()
    {
        if (BufferOffset >= RandomBuffer.Length)
        {
            FillBuffer();
        }

        int val = BitConverter.ToInt32(RandomBuffer, BufferOffset) & 0x7fffffff;
        BufferOffset += sizeof(int);
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
        rng.GetBytes(buff);
    }
}