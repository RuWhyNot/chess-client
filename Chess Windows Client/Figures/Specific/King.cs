using System;
using System.Drawing;

namespace Chess_Windows_Client.Figures.Specific
{
	class King : FigureImpl, ChessFigure
	{
		public King(Player player) : base(player)
		{
		}

		public void Draw(Graphics g, Color color, PointF pos, PointF size)
		{
			g.DrawString("K", SystemFonts.DefaultFont, new SolidBrush(color), pos);
		}

		public bool Move(ref ChessGameField.Cell[,] field, Point posFrom, Point posTo, ChessFigure lastMovedFig)
		{
			Figure targetFig = field[posTo.X, posTo.Y].figure;
			if (targetFig == null || targetFig.GetOwner() != GetOwner())
			{
				int difX = Math.Abs(posTo.X - posFrom.X);
				int difY = Math.Abs(posTo.Y - posFrom.Y);

				if (difX <= 1 && difY <= 1)
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
