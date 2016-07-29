using System;
using System.Drawing;

namespace Chess_Windows_Client.Figures.Specific
{
	class Bishop : FigureImpl, ChessFigure
	{
		public Bishop(Player player) : base(player)
		{
		}

		public void Draw(Graphics g, Color color, PointF pos, PointF size)
		{
			g.DrawString("B", SystemFonts.DefaultFont, new SolidBrush(color), pos);
		}

		private bool IsPathFree(ref ChessGameField.Cell[,] field, Point posFrom, Point posTo)
		{
			if (Math.Abs(posTo.X - posFrom.X) == Math.Abs(posTo.Y - posFrom.Y))
			{
				int dirX = posTo.X - posFrom.X > 0 ? 1 : -1;
				int dirY = posTo.Y - posFrom.Y > 0 ? 1 : -1;

				int length = Math.Abs(posTo.X - posFrom.X);

				for (int i = 1; i < length; ++i)
				{
					if (field[posFrom.X + dirX * i, posFrom.Y + dirY * i].figure != null)
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
