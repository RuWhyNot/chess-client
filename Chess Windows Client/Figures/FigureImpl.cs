
namespace Chess_Windows_Client
{
	class FigureImpl : Figure
	{
		private Player Owner;

		protected FigureImpl(Player player)
		{
			Owner = player;
		}

		public Player GetOwner()
		{
			return Owner;
		}
	}
}
