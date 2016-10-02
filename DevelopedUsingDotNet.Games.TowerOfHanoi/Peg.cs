using System;

namespace DevelopedUsingDotNet.Games.TowerOfHanoi
{
	public class Peg
	{
		public Peg(int disks, bool populate)
		{
			if (disks < 0)
			{
				throw new ArgumentOutOfRangeException("The number of disks must be positive.");
			}

			Disks = new int[disks];
			if (populate)
			{
				for (int i = 1; i <= disks; i++)
				{
					Disks[i - 1] = i;
				}
				Top = 0;
			}
			else
			{
				Top = disks;
			}
		}

		public int[] Disks { get; }

		public int Top { get; set; }

		public void Add(int disk)
		{
			if (Top <= 0)
			{
				throw new InvalidOperationException("Cannot add a disk to a full peg.");
			}

			Disks[--Top] = disk;
		}

		public int Remove()
		{
			if (Top >= Disks.Length)
			{
				throw new InvalidOperationException("Cannot remove a disk from an empty peg.");
			}

			var disk = Disks[Top];
			Disks[Top] = 0;
			Top++;
			return disk;
		}
	}
}
