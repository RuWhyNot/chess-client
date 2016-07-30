using System.Drawing;

namespace Chess_Windows_Client.Figures.Specific
{
	class Queen : FigureImpl, ChessFigure
	{
		public Queen(Player player) : base(player)
		{
		}

		public void Draw(Graphics g, Color color, PointF pos, PointF size)
		{
			g.DrawString("Q", SystemFonts.DefaultFont, new SolidBrush(color), pos);
		}

		public bool CanMove(ChessGameField.Cell[,] field, Point posFrom, Point posTo, ChessFigure lastMovedFig)
		{
			Figure targetFig = field[posTo.X, posTo.Y].figure;
			if (targetFig == null || targetFig.GetOwner() != GetOwner())
			{
				return Rook.IsPathFree(field, posFrom, posTo) || Bishop.IsPathFree(field, posFrom, posTo);
			}
			return false;
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
