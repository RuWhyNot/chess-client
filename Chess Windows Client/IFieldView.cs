using System.Drawing;

namespace Chess_Windows_Client
{
	public interface IFieldView
	{
		void OnMouseClick(Point pos);
		void Draw(Graphics g);
		void Resize(PointF newSize);
	}
}