using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
