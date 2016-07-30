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

		public bool CanMove(ChessGameField.Cell[,] field, Point posFrom, Point posTo, ChessMove lastMove)
		{
			Figure targetFig = field[posTo.X, posTo.Y].figure;
			if (targetFig == null || targetFig.GetOwner() != GetOwner())
			{
				return Rook.IsPathFree(field, posFrom, posTo) || Bishop.IsPathFree(field, posFrom, posTo);
			}
			return false;
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
