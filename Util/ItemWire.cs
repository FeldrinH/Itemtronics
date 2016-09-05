using System.Collections.Generic;
using Terraria.DataStructures;

namespace Itemtronics.Util
{
	internal static class ItemWire
	{
		public static List<Point16> inputs;
		public static List<Point16> outputs;

		public static readonly int[] xOffset = { 0, -1, 0, 1 };
		public static readonly int[] yOffset = { -1, 0, 1, 0 };
	}
}
