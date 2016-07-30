using System.Drawing;

namespace Chess_Windows_Client
{
	public interface ChessFigure : Figure
	{
		bool CanMove(ChessGameField.Cell[,] field, Point posFrom, Point posTo, ChessMove lastMove);

		// check and move (false if can't do thi move)
		ChessMove Move(ref ChessGameField.Cell[,] field, Point posFrom, Point posTo, ChessMove lastMove);

		void Draw(Graphics g, Color color, PointF pos, PointF size);
	}
}
