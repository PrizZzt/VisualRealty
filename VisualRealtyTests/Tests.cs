using Microsoft.VisualStudio.TestTools.UnitTesting;
using VisualRealty;
using System.Drawing;
using System;
using System.IO;

namespace VisualRealtyTests
{
	[TestClass]
	public class Tests
	{
		[TestMethod]
		public void ColorByRateTest()
		{
			Color green = ColorHelper.ColorByRate(0);
			Color yellow = ColorHelper.ColorByRate(0.5);
			Color red = ColorHelper.ColorByRate(1);

			Assert.AreEqual(0, green.R);
			Assert.AreEqual(255, green.G);
			Assert.AreEqual(0, green.B);

			Assert.AreEqual(255, yellow.R);
			Assert.AreEqual(255, yellow.G);
			Assert.AreEqual(0, yellow.B);

			Assert.AreEqual(255, red.R);
			Assert.AreEqual(0, red.G);
			Assert.AreEqual(0, red.B);
		}

		[TestMethod]
		public void AvitoHelperTest()
		{
			string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AvitoHelperTestData.txt");
			AvitoHelper.Load(fileName);
		}
	}
}
