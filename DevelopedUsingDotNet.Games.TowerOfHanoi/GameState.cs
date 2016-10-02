namespace DevelopedUsingDotNet.Games.TowerOfHanoi
{
	public class GameState
	{
		public int From { get; set; }
		public int To { get; set; }
	}

	public class DetailedGameState : GameState
	{
		public DetailedGameState(int disks)
		{
			Peg0 = new int[disks];
			Peg1 = new int[disks];
			Peg2 = new int[disks];
		}

		public int[] Peg0 { get; }
		public int[] Peg1 { get; }
		public int[] Peg2 { get; }
	}
}
