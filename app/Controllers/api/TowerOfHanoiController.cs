using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using DevelopedUsingDotNet.Games.TowerOfHanoi;

namespace DevelopedUsingDotNet.Controllers
{
	[Route("api/tower-of-hanoi")]
	public class TowerOfHanoiController : Controller
	{
		private const int TimeOut = 10 * 1000;
		private const int MaxDisks = 15;
		private const int MaxStates = 10;

		[HttpGet("moves/{disks}")]
		public int GetMoves(int disks)
		{
			return Game.GetMinimumMoves(disks);
		}

		[HttpGet("solve/{disks}")]
		public async Task<Solution> Solve(int disks)
		{
			if (disks > MaxDisks)
			{
				throw new ArgumentOutOfRangeException(nameof(disks));
			}

			var cts = new CancellationTokenSource();
			using (new Timer(state => cts.Cancel(), null, TimeOut, Timeout.Infinite))
			{
				return await Task.Run(() =>
				{
					var game = new Game(disks);
					var sw = new Stopwatch();
					sw.Start();
					game.Solve(disks <= MaxStates, cts.Token);
					sw.Stop();
					cts.Token.ThrowIfCancellationRequested();
					return new Solution
					{
						Disks = disks,
						Moves = game.States.Count,
						States = game.States.ToArray(),
						Duration = sw.ElapsedMilliseconds
					};
				});
			}
		}
	}

	public class Solution
	{
		public int Disks { get; set; }

		public int Moves { get; set; }

		public long Duration { get; set; }

		public GameState[] States { get; set; }
	}
}
