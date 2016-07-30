using System;
using System.Drawing;

namespace Chess_Windows_Client.Figures.Specific
{
	class Knight : FigureImpl, ChessFigure
	{
		public Knight(Player player) : base(player)
		{
		}

		public bool CanMove(ChessGameField.Cell[,] field, Point posFrom, Point posTo, ChessFigure lastMovedFig)
		{
			Figure targetFig = field[posTo.X, posTo.Y].figure;
			if (targetFig == null || targetFig.GetOwner() != GetOwner())
			{
				int difX = Math.Abs(posTo.X - posFrom.X);
				int difY = Math.Abs(posTo.Y - posFrom.Y);
				int minDir = Math.Min(difX, difY);
				int dirDif = Math.Abs(difY - difX);

				if (dirDif == 1 && minDir == 1)
				{
					return true;
				}
			}

			return false;
		}

		public void Draw(Graphics g, Color color, PointF pos, PointF size)
		{
			g.DrawString("Kt", SystemFonts.DefaultFont, new SolidBrush(color), pos);
		}

		public bool Move(ref ChessGameField.Cell[,] field, Point posFrom, Point posTo, ChessFigure lastMovedFig)
		{
			if (CanMove(field, posFrom, posTo, lastMovedFig))
			{
				field[posTo.X, posTo.Y].figure = this;
				field[posFrom.X, posFrom.Y].figure = null;
				return true;
			}

			return false;
		}
	}
}
