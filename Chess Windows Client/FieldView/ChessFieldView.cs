using System;
using System.Drawing;

namespace Chess_Windows_Client
{
	public class ChessFieldView : IFieldView
	{
		public PointF Pos { get; private set; }
		public PointF Size { get; private set; }
		public ActionState State { get; private set; }

		private ChessGameField Field = new ChessGameField();
		private Player CurrentPlayer = Player.White;
		private Point SelectedPoint = new Point(-1, -1);

		public enum ActionState
		{
			Idle,
			Selected,
			WaitOtherPlayer
		}

		public ChessFieldView(PointF pos, PointF size)
		{
			Pos = pos;
			Size = size;
			State = ActionState.Idle;
		}

		public void OnMouseClick(Point pos)
		{
			PointF cellSize = new PointF((float)Size.X / (ChessGameField.FIELD_SIZE - 1), (float)Size.Y / (ChessGameField.FIELD_SIZE - 1));

			if (pos.X > Pos.X && pos.Y > Pos.Y
				&& pos.X < Pos.X + Size.X && pos.Y < Pos.Y + Size.Y)
			{
				Point cellPos = new Point((int)Math.Floor((pos.X - Pos.X) / (Size.X) * ChessGameField.FIELD_SIZE),
					(int)Math.Floor((pos.Y - Pos.Y) / (Size.Y) * ChessGameField.FIELD_SIZE));

				ChessGameField.Cell cell = Field.GetCell(cellPos);
				if (State == ActionState.Idle)
				{
					SelectCell(cellPos, cell);
				}
				else if (State == ActionState.Selected)
				{
					if (Field.MakeMove(CurrentPlayer, SelectedPoint, cellPos))
					{
						ChangePlayer();
						State = ActionState.Idle;
                    }
					else
					{
						SelectCell(cellPos, cell);
					}
				}
			}
		}

		private void SelectCell(Point cellPos, ChessGameField.Cell cell)
		{
			if (cell.figure != null && cell.figure.GetOwner() == CurrentPlayer)
			{
				SelectedPoint = cellPos;
				State = ActionState.Selected;
			}
		}

		private Color GetColor(Player player)
		{
			return (player == Player.Black) ? Color.Black : Color.White;
		}

		private PointF GetCellPos(Point posInField, PointF cellSize)
		{
			return new PointF(Pos.X + posInField.X * cellSize.X, Pos.Y + posInField.Y * cellSize.Y);
        }

		public void Draw(Graphics g)
		{
			g.Clear(Color.Gray);

			PointF cellSize = new PointF((float)Size.X / (ChessGameField.FIELD_SIZE), (float)Size.Y / (ChessGameField.FIELD_SIZE));

			for (int i = 0; i <= ChessGameField.FIELD_SIZE; ++i)
			{
				g.DrawLine(Pens.Black, Pos.X + i * cellSize.X, Pos.Y, Pos.X + i * cellSize.X, Pos.Y + Size.Y);
			}

			for (int i = 0; i <= ChessGameField.FIELD_SIZE; ++i)
			{
				g.DrawLine(Pens.Black, Pos.X, Pos.Y + i * cellSize.Y, Pos.X + Size.X, Pos.Y + i * cellSize.Y);
			}

			Field.ForEachCell((pos, cell) => { if (cell.figure != null) cell.figure.Draw(g, GetColor(cell.figure.GetOwner()), GetCellPos(pos, cellSize), cellSize); return true; });

			if (State == ActionState.Selected)
			{
				PointF pos = GetCellPos(SelectedPoint, cellSize);
				Pen pen = new Pen(Brushes.Green, 2);
				int border = 4;
                g.DrawRectangle(pen, new Rectangle((int)pos.X + border + 1, (int)pos.Y + border + 1, (int)cellSize.X - border*2, (int)cellSize.Y - border*2));
			}
		}

		private void ChangePlayer()
		{
			CurrentPlayer = ChessGameField.GetOpponent(CurrentPlayer);
		}

		public void Resize(PointF newSize)
		{
			Size = newSize;
		}
	}
}
