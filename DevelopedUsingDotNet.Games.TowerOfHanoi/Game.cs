using System;
using System.Collections.Generic;
using System.Threading;

namespace DevelopedUsingDotNet.Games.TowerOfHanoi
{
	public class Game
	{
		public Game(int disks)
		{
			if (disks < 0)
			{
				disks = 0;
			}

			Disks = disks;
			States = new List<GameState>(GetMinimumMoves(disks));
			Board = new Board(disks);
		}

		public int Disks { get; }

		public Board Board { get; }

		public List<GameState> States { get; }

		public static int GetMinimumMoves(int disks)
		{
			return (int)Math.Pow(2, disks) - 1;
		}

		public void Solve(bool saveState, CancellationToken token = default(CancellationToken))
		{
			Solve(1, saveState, token);
		}

		public void Solve(int to, bool saveState, CancellationToken token = default(CancellationToken))
		{
			MoveTower(Disks, 0, to, Board.PegsCount - to, saveState, token);
		}

		private void MoveTower(int disks, int from, int to, int with, bool saveState, CancellationToken token = default(CancellationToken))
		{
			if (disks >= 1 && !token.IsCancellationRequested)
			{
				MoveTower(disks - 1, from, with, to, saveState);
				MoveDisk(from, to, saveState);
				MoveTower(disks - 1, with, to, from, saveState);
			}
		}

		private void MoveDisk(int from, int to, bool saveState)
		{
			// make move
			var disk = Board.Pegs[from].Remove();
			Board.Pegs[to].Add(disk);

			// record state
			DetailedGameState detailed = null;
			var state = saveState ? (detailed = new DetailedGameState(Disks)) : new GameState();
			state.From = from;
			state.To = to;
			if (saveState)
			{
				Array.Copy(Board.Peg0.Disks, detailed.Peg0, Disks);
				Array.Copy(Board.Peg1.Disks, detailed.Peg1, Disks);
				Array.Copy(Board.Peg2.Disks, detailed.Peg2, Disks);
			}
			States.Add(state);
		}
	}
}
