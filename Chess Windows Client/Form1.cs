using System.Drawing;
using System.Windows.Forms;

namespace Chess_Windows_Client
{
	public partial class Form1 : Form
	{
		IFieldView Field;

		public Form1()
		{
			InitializeComponent();
			Field = new ChessFieldView(new PointF(20.0f, 20.0f), new PointF(Width - 60.0f, Height - 80.0f));
		}

		private void Form1_MouseClick(object sender, MouseEventArgs e)
		{
			Field.OnMouseClick(new Point(e.X, e.Y));
			Invalidate();
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			Field.Draw(e.Graphics);
		}

		private void Form1_SizeChanged(object sender, System.EventArgs e)
		{
			Field.Resize(new PointF(Width - 60.0f, Height - 80.0f));
			Invalidate();
		}
	}
}
