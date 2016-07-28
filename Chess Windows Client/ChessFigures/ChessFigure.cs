using System.Drawing;

namespace Chess_Windows_Client
{
	public interface ChessFigure
	{
		// additional check
		bool CanBeMoved(ChessGameField field, Point posFrom, Point posTo);
		// check and move (false if can't do thi move)
		bool Move(ref ChessGameField field, Point posFrom, Point posTo);

		void Draw(Graphics g, Color color, PointF pos, PointF size);
	}
}
