using System;
using System.Drawing;

namespace Chess_Windows_Client
{
	class Pawn : FigureImpl, ChessFigure
	{
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

		public bool IsFirstMove(Point posFrom)
		{
			return ((GetOwner() == Player.White && posFrom.Y == 6) || (GetOwner() == Player.Black && posFrom.Y == 1));
		}

		public bool CanMoveTwoSteps(ChessGameField.Cell cellTo, Point posFrom, Point posTo)
		{
			if (cellTo.figure == null && posFrom.X == posTo.X)
			{
				if (IsFirstMove(posFrom) && ((GetOwner() == Player.White && posFrom.Y == posTo.Y + 2)
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

		public bool CanCaptureByEnPassant(ChessGameField.Cell[,] field, ChessGameField.Cell cellTo, Point posFrom, Point posTo, ChessMove lastMove)
		{
			if (cellTo.figure == null && (posFrom.X == posTo.X - 1 || posFrom.X == posTo.X + 1)) // En passant
			{
				if ((GetOwner() == Player.White && posFrom.Y == posTo.Y + 1)
					|| (GetOwner() == Player.Black && posFrom.Y == posTo.Y - 1))
				{
					Figure opponentsFigure = field[posTo.X, posFrom.Y].figure;
					if (opponentsFigure is Pawn && opponentsFigure.GetOwner() != GetOwner())
					{
						Pawn opponentsPawn = (Pawn)opponentsFigure;
						if (lastMove.Fig == opponentsPawn && Math.Abs(lastMove.GetPrevPos().Y - posFrom.Y) == 2)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		public bool CanMove(ChessGameField.Cell[,] field, Point posFrom, Point posTo, ChessMove lastMove)
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
			else if (CanCaptureByEnPassant(field, cellTo, posFrom, posTo, lastMove))
			{
				return true;
			}

			return false;
		}

		public ChessMove Move(ref ChessGameField.Cell[,] field, Point posFrom, Point posTo, ChessMove lastMove)
		{
			ChessGameField.Cell cellTo = field[posTo.X, posTo.Y];
			ChessMove move = new ChessMove();
            if (CanMoveOneStep(cellTo, posFrom, posTo))
			{
				move.AddAction(new ChessMove.MoveAction(posFrom, posTo));
				return move;
			}
			else if (CanMoveTwoSteps(cellTo, posFrom, posTo))
			{
				move.AddAction(new ChessMove.MoveAction(posFrom, posTo));
				return move;
			}
			else if (CanCapture(cellTo, posFrom, posTo))
			{
				move.AddAction(new ChessMove.RemoveAction(posTo));
				move.AddAction(new ChessMove.MoveAction(posFrom, posTo));
				return move;
			}
			else if (CanCaptureByEnPassant(field, cellTo, posFrom, posTo, lastMove))
			{
				move.AddAction(new ChessMove.RemoveAction(new Point(posTo.X, posFrom.Y)));
				move.AddAction(new ChessMove.MoveAction(posFrom, posTo));
				return move;
            }

			return null;
		}
	}
}
