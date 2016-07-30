using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Chess_Windows_Client
{
	// Command pattern
	public class ChessMove
	{
		private LinkedList<Action> Actions = new LinkedList<Action>();
		public ChessFigure Fig { get; private set; }

		public interface Action
		{
			ChessFigure GetFigure();
			void Do(ref ChessGameField.Cell[,] field);
			void UnDo(ref ChessGameField.Cell[,] field);
		}

		public class MoveAction : Action
		{
			public Point From { get; }
			public Point To { get; }
			public ChessFigure Fig { get; private set; }

			public MoveAction(Point from, Point to)
			{
				From = from;
				To = to;
			}

			public ChessFigure GetFigure()
			{
				return Fig;
			}

			public void Do(ref ChessGameField.Cell[,] field)
			{
				Fig = field[From.X, From.Y].figure;
				field[From.X, From.Y].figure = null;
				field[To.X, To.Y].figure = Fig;
			}

			public void UnDo(ref ChessGameField.Cell[,] field)
			{
				field[From.X, From.Y].figure = Fig;
				field[To.X, To.Y].figure = null;
				Fig = null;
			}
		}

		public class RemoveAction : Action
		{
			public Point Pos { get; }
			public ChessFigure Fig { get; private set; }

			public RemoveAction(Point pos)
			{
				Pos = pos;
			}

			public ChessFigure GetFigure()
			{
				return Fig;
			}

			public void Do(ref ChessGameField.Cell[,] field)
			{
				Fig = field[Pos.X, Pos.Y].figure;
				field[Pos.X, Pos.Y].figure = null;
			}

			public void UnDo(ref ChessGameField.Cell[,] field)
			{
				field[Pos.X, Pos.Y].figure = Fig;
				Fig = null;
			}
		}

		public void AddAction(Action action)
		{
			Actions.AddLast(action);
		}

		public void Do(ref ChessGameField.Cell[,] field, ChessFigure figure)
		{
			Fig = figure;

			foreach (Action action in Actions)
			{
				action.Do(ref field);
			}
		}

		public void UnDo(ref ChessGameField.Cell[,] field)
		{
			foreach (Action action in Actions.Reverse())
			{
				action.UnDo(ref field);
			}
		}

		public Point GetPrevPos()
		{
			foreach (Action action in Actions)
			{
				if (action is MoveAction)
				{
					return ((MoveAction)action).From;
				}
			}
			return new Point(0, 0);
        }
	}
}
