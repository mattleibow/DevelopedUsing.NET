using System;
using System.Text;

namespace DevelopedUsingDotNet.Tools.Randomization
{
	public static class RandomStrings
	{
		public const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		public const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
		public const string Numeric = "0123456789";
		public const string Alphanumeric = Uppercase + Lowercase + Numeric;

		public static string NextString(string characters, int length)
		{
			if (length <= 0 || string.IsNullOrEmpty(characters))
			{
				return string.Empty;
			}

			var rnd = new Random(RandomNumbers.NextInt());

			var sb = new StringBuilder(length);
			for (int i = 0; i < length; i++)
			{
				sb.Append(characters[rnd.Next(characters.Length)]);
			}
			return sb.ToString();
		}

		public static string NextGuid(string format = null)
		{
			return Guid.NewGuid().ToString(format);
		}
	}
}
