using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace VisualRealty
{
	public class MapController
	{
		private List<ValuePoint> points = new List<ValuePoint>();

		public static int SizeFromZoom(double _zoom)
		{
			if (_zoom < 12 || _zoom > 15)
				throw new ArgumentException();

			return (int)_zoom - 11;
		}

		public static int ZoomFromSize(double _size)
		{
			if (_size < 1 || _size > 4)
				throw new ArgumentException();
			return (int)_size + 11;
		}

		public MapController(List<ValuePoint> _points)
		{
			points = _points;
		}

		public void CreateMaps()
		{
			for (int k = 1; k <= 2; k++)
			{
				CreateMap(k);
			}
		}

		public void DeleteMaps()
		{
			for (int k = 1; k <= 4; k++)
			{
				string fileName = $"{ZoomFromSize(k)}.bmp";
				if (File.Exists(fileName))
				{
					try
					{
						File.Delete(fileName);
					}
					catch (Exception)
					{ }
				}
			}
		}

		public void CreateMap(int _size)
		{
			int sizeSquared = (int)(Math.Pow(2, _size - 1));
			int sizeMap = sizeSquared * 1000;

			string fileName = $"{ZoomFromSize(_size)}.bmp";
			if (File.Exists(fileName))
				File.Delete(fileName);

			using (Bitmap curMap = new Bitmap(sizeMap, sizeMap))
			{
				for (int j = 0; j < sizeMap; j++)
				{
					for (int i = 0; i < sizeMap; i++)
					{
						curMap.SetPixel(i, j, ColorHelper.ColorByRate(GaussByCoords(i, j, sizeSquared)));
					}
				}

				curMap.Save(fileName);
			}
		}

		public Bitmap GetMap(int _size)
		{
			string fileName = $"{ZoomFromSize(_size)}.bmp";
			if (File.Exists(fileName))
			{
				return new Bitmap(fileName);
			}
			return null;
		}

		private double GaussByCoords(int _x, int _y, int _sizeSquared)
		{
			long sizeMap = _sizeSquared * 1000;

			if (_x < 0 || _x > sizeMap)
				throw new ArgumentException($"X = {_x}");
			if (_y < 0 || _y > sizeMap)
				throw new ArgumentException($"Y = {_y}");
			if (points.Count == 0)
				throw new ArgumentException("Недостаточно точек для расчета");
			if (_sizeSquared < 1 || _sizeSquared > 16)
				throw new ArgumentException();

			double maxValue = points.Max(p => p.Value);
			double maxCurValue = 0;
			foreach (var point in points)
			{
				double distance = DistanceSquared(_x, _y, point.X * _sizeSquared, point.Y * _sizeSquared);

				double c = 2 * sizeMap * _sizeSquared * (point.Value / maxValue);
				double weight = Math.Exp(-(distance * distance) / (2 * c * c));
				if (weight < 0 || weight > 1)
					throw new ArgumentException($"Weight = {weight}");

				double v = weight * point.Value;
				maxCurValue = maxCurValue > v ? maxCurValue : v;
			}

			double result = maxCurValue / maxValue;
			if (double.IsNaN(result) || result < 0 || result > 1)
				throw new ArgumentException($"Gauss = {result}");

			return result;
		}

		private double DistanceSquared(double x1, double y1, double x2, double y2)
		{
			return (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
		}
	}
}
