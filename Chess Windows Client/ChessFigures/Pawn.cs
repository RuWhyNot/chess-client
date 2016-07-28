using System;
using System.Drawing;

namespace Chess_Windows_Client
{
	class Pawn : ChessFigure
	{
		public bool CanBeMoved(ChessGameField field, Point posFrom, Point posTo)
		{
			return false;
		}

		public void Draw(Graphics g, Color color, PointF pos, PointF size)
		{
			g.DrawString("P", SystemFonts.DefaultFont, new SolidBrush(color), pos);
		}

		public bool Move(ref ChessGameField field, Point posFrom, Point posTo)
		{
			return false;
		}
	}
}
