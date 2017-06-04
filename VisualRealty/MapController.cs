using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;

namespace VisualRealty
{
	public class MapController
	{
		public List<ValuePoint> Points => points;
		private List<ValuePoint> points;

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

		public void LoadPoints(string fileName)
		{
			if (File.Exists(fileName))
			{
				DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<ValuePoint>));
				using (FileStream fs = new FileStream(fileName, FileMode.Open))
				{
					points = ((List<ValuePoint>)serializer.ReadObject(fs)).Distinct().ToList();
				}
			}
		}

		public void SavePoints(string fileName)
		{
			if (string.IsNullOrEmpty(fileName))
				return;

			if (File.Exists(fileName))
			{
				string oldDataFile = Path.ChangeExtension(fileName, "bak");
				File.Delete(oldDataFile);
				File.Move(fileName, oldDataFile);
			}

			DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<ValuePoint>));
			using (FileStream fs = new FileStream(fileName, FileMode.Create))
			{
				serializer.WriteObject(fs, points);
			}
		}

		public void AddPoints(IEnumerable<ValuePoint> addPoints)
		{
			foreach (var point in addPoints)
			{
				if (points.Any(p => p.Url == point.Url) == false)
					points.Add(point);
			}
		}

		public void PlaceMarkers(GMapOverlay overlay)
		{
			overlay.Markers.Where(m => string.IsNullOrEmpty(m.ToolTipText) == false).ToList().Clear();
			foreach (var point in points)
			{
				GMapMarker thisMarker = new GMarkerGoogle(new PointLatLng(point.Latitude, point.Longitude), GMarkerGoogleType.arrow);
				thisMarker.IsHitTestVisible = true;
				thisMarker.ToolTipText = point.Url;
				overlay.Markers.Add(thisMarker);
			}
		}

		public MapController()
		{
			points = new List<ValuePoint>();
		}

		public void CreateMaps()
		{
			for (int k = 1; k <= 4; k++)
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

			string newFileName = $"{ZoomFromSize(_size)}_new.bmp";
			if (File.Exists(newFileName))
				File.Delete(newFileName);

			using (Bitmap curMap = new Bitmap(sizeMap, sizeMap))
			{
				for (int j = 0; j < sizeMap; j++)
				{
					for (int i = 0; i < sizeMap; i++)
					{
						curMap.SetPixel(i, j, ColorHelper.ColorByRate(GaussByCoords(i, j, sizeSquared)));
					}
				}

				curMap.Save(newFileName);
			}
		}

		public Bitmap GetMap(int _size)
		{
			string fileName = $"{ZoomFromSize(_size)}.bmp";
			string newFileName = $"{ZoomFromSize(_size)}_new.bmp";

			try
			{
				if (File.Exists(newFileName))
				{
					File.Delete(fileName);
					File.Move(newFileName, fileName);
				}
			}
			catch (Exception) { }
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
