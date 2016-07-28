using System;
using System.Drawing;

namespace Chess_Windows_Client
{
	public class ChessGameField
	{
		public const int FIELD_SIZE = 8;

		private Cell[,] Field = new Cell[FIELD_SIZE, FIELD_SIZE];

		public struct Cell
		{
			public Player player;
			public ChessFigure figure;
		}

		public enum Player
		{
			White,
			Black
		}

		public ChessGameField()
		{
			ForEachCell((pos, cell) => { Field[pos.X, pos.Y] = new Cell(); return true; });

			for (int i = 0; i < FIELD_SIZE; ++i)
			{
				Field[i, 1].figure = new Pawn();
				Field[i, 1].player = Player.Black;

				Field[i, 6].figure = new Pawn();
				Field[i, 6].player = Player.White;
			}
		}

		public static Player GetOpponent(Player player)
		{
			return (player == Player.Black) ? Player.White : Player.Black;
		}

		public Cell GetCell(Point pos)
		{
			return Field[pos.X, pos.Y];
		}

		public void ForEachCell(Func<Point, Cell, bool> func)
		{
			for (int i = 0; i < FIELD_SIZE; ++i)
			{
				for (int j = 0; j < FIELD_SIZE; ++j)
				{
					Point point = new Point(i, j);
                    if (!func(point, GetCell(point)))
					{
						return;
					}
				}
			}
		}

		public bool MakeMove(Player player, Point from, Point to)
		{
			return false;
		}
	}
}
