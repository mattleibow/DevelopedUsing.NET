namespace DevelopedUsingDotNet.Games.TowerOfHanoi
{
	public class Board
	{
		public const int PegsCount = 3;

		public Board(int disks)
		{
			Peg0 = new Peg(disks, true);
			Peg1 = new Peg(disks, false);
			Peg2 = new Peg(disks, false);

			Pegs = new Peg[PegsCount] { Peg0, Peg1, Peg2 };
		}

		public Peg Peg0 { get; }
		public Peg Peg1 { get; }
		public Peg Peg2 { get; }

		public Peg[] Pegs { get; }
	}
}
