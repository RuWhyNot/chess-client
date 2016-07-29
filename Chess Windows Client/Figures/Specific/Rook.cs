using System.Drawing;

namespace Chess_Windows_Client.Figures.Specific
{
	class Rook : FigureImpl, ChessFigure
	{
		public Rook(Player player) : base(player)
		{
		}

		public void Draw(Graphics g, Color color, PointF pos, PointF size)
		{
			g.DrawString("R", SystemFonts.DefaultFont, new SolidBrush(color), pos);
		}

		private bool IsPathFree(ref ChessGameField.Cell[,] field, Point posFrom, Point posTo)
		{
			if (posFrom.X == posTo.X)
			{
				int dir = posTo.Y - posFrom.Y > 0 ? 1 : -1;
				for (int i = posFrom.Y + dir; i != posTo.Y; i += dir)
				{
					if (field[posTo.X, i].figure != null)
					{
						return false;
					}
				}
				return true;
			}
			else if (posFrom.Y == posFrom.Y)
			{
				int dir = posTo.X - posFrom.X > 0 ? 1 : -1;
				for (int i = posFrom.X + dir; i != posTo.X; i += dir)
				{
					if (field[i, posTo.Y].figure != null)
					{
						return false;
					}
				}
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool Move(ref ChessGameField.Cell[,] field, Point posFrom, Point posTo, ChessFigure lastMovedFig)
		{
			Figure targetFig = field[posTo.X, posTo.Y].figure;
			if (targetFig == null || targetFig.GetOwner() != GetOwner())
			{
				if (IsPathFree(ref field, posFrom, posTo))
				{
					field[posTo.X, posTo.Y].figure = this;
					field[posFrom.X, posFrom.Y].figure = null;
					return true;
				}
			}

			return false;
		}
	}
}
