using System;
using System.Threading;

namespace DevelopedUsingDotNet.Tools.Randomization
{
	public static class RandomNumbers
	{
		private static ThreadLocal<Random> randoms;

		static RandomNumbers()
		{
			randoms = new ThreadLocal<Random>(() => new Random());
		}

		public static double NextDouble()
		{
			return randoms.Value.NextDouble();
		}

		public static double NextDouble(double min, double max)
		{
			if (min > max)
			{
				var temp = min;
				min = max;
				max = temp;
			}

			var val = NextDouble();

			if (val >= min && val <= max)
				return val;

			return min + (val * (max - min));
		}

		public static int NextInt()
		{
			return randoms.Value.Next();
		}

		public static int NextInt(int min, int max)
		{
			if (min > max)
			{
				var temp = min;
				min = max;
				max = temp;
			}

			// we want inclusive
			return randoms.Value.Next(min, max + 1);
		}

		public static string NextBoolean(BooleanFormat format)
		{
			switch (format)
			{
				case BooleanFormat.TrueFalse:
					return NextBoolean("true", "false");
				case BooleanFormat.YesNo:
					return NextBoolean("yes", "no");
				case BooleanFormat.OneZero:
					return NextBoolean("1", "0");
			}
			throw new ArgumentOutOfRangeException(nameof(format));
		}

		public static string NextBoolean(string t, string f)
		{
			return randoms.Value.Next(2) == 1 ? t : f;
		}
	}
}
