using Microsoft.AspNetCore.Mvc;
using DevelopedUsingDotNet.Tools.Randomization;

namespace DevelopedUsingDotNet.Controllers
{
	[Route("api/randomization")]
	public class RindomizationController : Controller
	{
		[HttpGet("number/integer")]
		public int GetInteger()
		{
			return RandomNumbers.NextInt();
		}

		[HttpGet("number/integer/{max}")]
		public int GetInteger(int max)
		{
			return RandomNumbers.NextInt(0, max);
		}

		[HttpGet("number/integer/{min}/{max}")]
		public int GetInteger(int min, int max)
		{
			return RandomNumbers.NextInt(min, max);
		}

		[HttpGet("number/decimal")]
		public double GetDecimal()
		{
			return RandomNumbers.NextDouble();
		}

		[HttpGet("number/decimal/{max}")]
		public double GetDecimal(double max)
		{
			return RandomNumbers.NextDouble(0, max);
		}

		[HttpGet("number/decimal/{min}/{max}")]
		public double GetDecimal(double min, double max)
		{
			return RandomNumbers.NextDouble(min, max);
		}

		[HttpGet("string/lowercase/{length}")]
		public string GetLowercaseString(int length)
		{
			return RandomStrings.NextString(RandomStrings.Lowercase, length);
		}

		[HttpGet("string/uppercase/{length}")]
		public string GetUppercaseString(int length)
		{
			return RandomStrings.NextString(RandomStrings.Uppercase, length);
		}

		[HttpGet("string/alphanumeric/{length}")]
		public string GetAlphanumericString(int length)
		{
			return RandomStrings.NextString(RandomStrings.Alphanumeric, length);
		}

		[HttpGet("string/characters/{length}/{characters}")]
		public string GetString(int length, string characters)
		{
			return RandomStrings.NextString(characters, length);
		}

		[HttpGet("guid/{*format}")]
		public string GetGuid(string format)
		{
			return RandomStrings.NextGuid(format);
		}

		[HttpGet("boolean/{*format}")]
		public string GetBoolean(BooleanFormat format)
		{
			return RandomNumbers.NextBoolean(format);
		}

		[HttpGet("coin")]
		public string GetCoin()
		{
			return RandomNumbers.NextBoolean("heads", "tails");
		}

		[HttpGet("dice")]
		public string GetDice()
		{
			return RandomStrings.NextString("123456", 1);
		}
	}
}
