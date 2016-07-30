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

		public bool CanMoveOneStep(ChessGameField.Cell cellTo, Point posFrom, Point posTo)
		{
			if (cellTo.figure == null && posFrom.X == posTo.X)
			{
				if ((GetOwner() == Player.White && posFrom.Y == posTo.Y + 1)
					|| (GetOwner() == Player.Black && posFrom.Y == posTo.Y - 1))
				{
					return true;
				}
			}
			return false;
		}

		public bool CanMoveTwoSteps(ChessGameField.Cell cellTo, Point posFrom, Point posTo)
		{
			if (cellTo.figure == null && posFrom.X == posTo.X)
			{
				if (FirstMove && ((GetOwner() == Player.White && posFrom.Y == posTo.Y + 2)
					|| (GetOwner() == Player.Black && posFrom.Y == posTo.Y - 2)))
				{
					return true;
				}
			}
			return false;
		}


		public bool CanCapture(ChessGameField.Cell cellTo, Point posFrom, Point posTo)
		{
			if (cellTo.figure != null && cellTo.figure.GetOwner() != GetOwner()) // Capture
			{
				bool diag = posFrom.X == posTo.X - 1 || posFrom.X == posTo.X + 1;
				bool playerDir = (GetOwner() == Player.White && posFrom.Y == posTo.Y + 1)
					|| (GetOwner() == Player.Black && posFrom.Y == posTo.Y - 1);

				if (diag && playerDir)
				{
					return true;
				}
			}
			return false;
		}

		public bool CanCaptureByEnPassant(ChessGameField.Cell[,] field, ChessGameField.Cell cellTo, Point posFrom, Point posTo, ChessFigure lastMovedFig)
		{
			if (cellTo.figure == null && (posFrom.X == posTo.X - 1 || posFrom.X == posTo.X + 1)) // En passant
			{
				if ((GetOwner() == Player.White && posFrom.Y == posTo.Y + 1)
					|| (GetOwner() == Player.Black && posFrom.Y == posTo.Y - 1))
				{
					Figure opponentsFigure = field[posTo.X, posFrom.Y].figure;
					Pawn opponentsPawn = (Pawn)opponentsFigure;
					if (opponentsPawn != null && lastMovedFig == opponentsPawn && opponentsPawn.PreviousWasLongMove)
					{
						return true;
					}
				}
			}
			return false;
		}

		public bool CanMove(ChessGameField.Cell[,] field, Point posFrom, Point posTo, ChessFigure lastMovedFig)
		{
			ChessGameField.Cell cellTo = field[posTo.X, posTo.Y];
			if (CanMoveOneStep(cellTo, posFrom, posTo))
			{
				return true;
			}
			else if (CanMoveTwoSteps(cellTo, posFrom, posTo))
			{
				return true;
			}
			else if (CanCapture(cellTo, posFrom, posTo))
			{
				return true;
			}
			else if (CanCaptureByEnPassant(field, cellTo, posFrom, posTo, lastMovedFig))
			{
				return true;
			}

			return false;
		}

		public bool Move(ref ChessGameField.Cell[,] field, Point posFrom, Point posTo, ChessFigure lastMovedFig)
		{
			ChessGameField.Cell cellTo = field[posTo.X, posTo.Y];
			if (CanMoveOneStep(cellTo, posFrom, posTo))
			{
				MoveInField(field, posFrom, posTo);
				return true;
			}
			else if (CanMoveTwoSteps(cellTo, posFrom, posTo))
			{
				MoveInField(field, posFrom, posTo);
				PreviousWasLongMove = true;
				return true;
			}
			else if (CanCapture(cellTo, posFrom, posTo))
			{
				MoveInField(field, posFrom, posTo);
				return true;
			}
			else if (CanCaptureByEnPassant(field, cellTo, posFrom, posTo, lastMovedFig))
			{
				MoveInField(field, posFrom, posTo);
				field[posTo.X, posFrom.Y].figure = null;
				return true;
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
