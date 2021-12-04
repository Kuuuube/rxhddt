using System;
using System.IO;
using System.Text;

namespace RXHDDT.Util
{
  internal class ReplayWriter : BinaryWriter
  {
    public ReplayWriter(Stream fileStream)
      : base(fileStream, Encoding.UTF8)
    {

    }

    public override void Write(string value)
    {
      if (value == null)
        Write((byte)0);
      else
      {
        Write((byte)11);
        base.Write(value);
      }
    }

    public override void Write(byte[] buffer)
    {
      if (buffer == null)
        Write(-1);
      else
      {
        int length = buffer.Length;
        Write(length);
        if (length <= 0)
          return;
        base.Write(buffer);
      }
    }

        public void Write(DateTime dateTime)
    {
            Random random = new Random();

            long r = random.NextLong(DateTime.MinValue.Ticks, dateTime.ToUniversalTime().Ticks);

            Write(r);
    }


        public void NormalWrite(byte[] byte_0)
    {
      base.Write(byte_0);
    }
  }
    public static class RandomExtensionMethods
    {
        /// <summary>
        /// Returns a random long from min (inclusive) to max (exclusive)
        /// </summary>
        /// <param name="random">The given random instance</param>
        /// <param name="min">The inclusive minimum bound</param>
        /// <param name="max">The exclusive maximum bound.  Must be greater than min</param>
        public static long NextLong(this Random random, long min, long max)
        {
            if (max <= min)
                throw new ArgumentOutOfRangeException("max", "max must be > min!");

            //Working with ulong so that modulo works correctly with values > long.MaxValue
            ulong uRange = (ulong)(max - min);

            //Prevent a modolo bias; see https://stackoverflow.com/a/10984975/238419
            //for more information.
            //In the worst case, the expected number of calls is 2 (though usually it's
            //much closer to 1) so this loop doesn't really hurt performance at all.
            ulong ulongRand;
            do
            {
                byte[] buf = new byte[8];
                random.NextBytes(buf);
                ulongRand = (ulong)BitConverter.ToInt64(buf, 0);
            } while (ulongRand > ulong.MaxValue - ((ulong.MaxValue % uRange) + 1) % uRange);

            return (long)(ulongRand % uRange) + min;
        }

        /// <summary>
        /// Returns a random long from 0 (inclusive) to max (exclusive)
        /// </summary>
        /// <param name="random">The given random instance</param>
        /// <param name="max">The exclusive maximum bound.  Must be greater than 0</param>
        public static long NextLong(this Random random, long max)
        {
            return random.NextLong(0, max);
        }

        /// <summary>
        /// Returns a random long over all possible values of long (except long.MaxValue, similar to
        /// random.Next())
        /// </summary>
        /// <param name="random">The given random instance</param>
        public static long NextLong(this Random random)
        {
            return random.NextLong(long.MinValue, long.MaxValue);
        }
    }
}