using System.Drawing;

namespace Chess_Windows_Client
{
	public interface ChessFigure : Figure
	{
		// check and move (false if can't do thi move)
		bool Move(ref ChessGameField.Cell[,] field, Point posFrom, Point posTo, ChessFigure lastMovedFig);

		void Draw(Graphics g, Color color, PointF pos, PointF size);
	}
}
