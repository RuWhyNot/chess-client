using System;
using System.Drawing;

namespace Chess_Windows_Client.Figures.Specific
{
	class King : FigureImpl, ChessFigure
	{
		public King(Player player) : base(player)
		{
		}

		public bool CanMove(ChessGameField.Cell[,] field, Point posFrom, Point posTo, ChessMove lastMove)
		{
			Figure targetFig = field[posTo.X, posTo.Y].figure;
			if (targetFig == null || targetFig.GetOwner() != GetOwner())
			{
				int difX = Math.Abs(posTo.X - posFrom.X);
				int difY = Math.Abs(posTo.Y - posFrom.Y);

				if (difX <= 1 && difY <= 1)
				{
					return true;
				}
			}
			return false;
		}

		public void Draw(Graphics g, Color color, PointF pos, PointF size)
		{
			g.DrawString("K", SystemFonts.DefaultFont, new SolidBrush(color), pos);
		}

		public ChessMove Move(ref ChessGameField.Cell[,] field, Point posFrom, Point posTo, ChessMove lastMove)
		{
			if (CanMove(field, posFrom, posTo, lastMove))
			{
				ChessMove move = new ChessMove();
				if (field[posTo.X, posTo.Y].figure != null)
				{
					move.AddAction(new ChessMove.RemoveAction(posTo));
				}
				move.AddAction(new ChessMove.MoveAction(posFrom, posTo));
				return move;
			}

			return null;
		}
	}
}
