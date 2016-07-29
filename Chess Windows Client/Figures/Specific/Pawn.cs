using System;
using System.Drawing;

namespace Chess_Windows_Client
{
	class Pawn : FigureImpl, ChessFigure
	{
		private bool FirstMove = true;
		public bool PreviousWasLongMove { get; private set; } = false;

		public Pawn(Player player) : base(player)
		{
		}

		public void Draw(Graphics g, Color color, PointF pos, PointF size)
		{
			g.DrawString("P", SystemFonts.DefaultFont, new SolidBrush(color), pos);
		}

		public bool Move(ref ChessGameField.Cell[,] field, Point posFrom, Point posTo, ChessFigure lastMovedFig)
		{
			ChessGameField.Cell cellTo = field[posTo.X, posTo.Y];
			if (cellTo.figure == null && posFrom.X == posTo.X) // Move
			{
				if ((GetOwner() == Player.White && posFrom.Y == posTo.Y + 1)
					|| (GetOwner() == Player.Black && posFrom.Y == posTo.Y - 1))
				{
					MoveInField(field, posFrom, posTo);
					return true;
				}

				if (FirstMove && ((GetOwner() == Player.White && posFrom.Y == posTo.Y + 2)
					|| (GetOwner() == Player.Black && posFrom.Y == posTo.Y - 2)))
				{
					MoveInField(field, posFrom, posTo);
					PreviousWasLongMove = true;
					return true;
				}
			}
			else if (cellTo.figure != null && cellTo.figure.GetOwner() != GetOwner()) // Capture
			{
				bool diag = posFrom.X == posTo.X - 1 || posFrom.X == posTo.X + 1;
				bool playerDir = (GetOwner() == Player.White && posFrom.Y == posTo.Y + 1)
					|| (GetOwner() == Player.Black && posFrom.Y == posTo.Y - 1);

				if (diag && playerDir)
				{
					MoveInField(field, posFrom, posTo);
					return true;
				}
			}
			else if (cellTo.figure == null && (posFrom.X == posTo.X - 1 || posFrom.X == posTo.X + 1)) // En passant
			{
				if ((GetOwner() == Player.White && posFrom.Y == posTo.Y + 1)
					|| (GetOwner() == Player.Black && posFrom.Y == posTo.Y - 1))
				{
					Figure opponentsFigure = field[posTo.X, posFrom.Y].figure;
					Pawn opponentsPawn = (Pawn)opponentsFigure;
					if (opponentsPawn != null && lastMovedFig == opponentsPawn && opponentsPawn.PreviousWasLongMove)
					{
						MoveInField(field, posFrom, posTo);
						field[posTo.X, posFrom.Y].figure = null;
						return true;
                    }
				}
            }

			return false;
		}

		private void MoveInField(ChessGameField.Cell[,] field, Point posFrom, Point posTo)
		{
			field[posTo.X, posTo.Y].figure = this;
			field[posFrom.X, posFrom.Y].figure = null;
			PreviousWasLongMove = false;
			FirstMove = false;
		}
	}
}
