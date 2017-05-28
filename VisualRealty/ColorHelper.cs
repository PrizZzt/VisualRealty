using System;
using System.Drawing;

namespace VisualRealty
{
	public class ColorHelper
	{
		public static Color ColorByRate(double rate)
		{
			if (double.IsNaN(rate) || rate < 0 || rate > 1)
				throw new ArgumentException($"Rate = {rate}");

			return Color.FromArgb(
				(int)(255 * rate),
				(int)(rate >= 0.5 ? 255 : rate * 2 * 255),
				(int)(rate >= 0.5 ? (1 - (rate - 0.5) * 2) * 255 : 255),
				0
				);
		}
	}
}
