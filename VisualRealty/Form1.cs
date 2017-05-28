using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VisualRealty
{
	public partial class Form1 : Form
	{
		GMapOverlay markers;
		GMapMarker marker;

		MapController mapController;

		List<ValuePoint> points = new List<ValuePoint>
		{
			new ValuePoint(645, 459,1000),
			new ValuePoint(645, 359,500),
			new ValuePoint(645, 559,500),
			new ValuePoint(545, 459,500),
			new ValuePoint(745, 459,500),
			new ValuePoint(745, 429,300)
		};

		public Form1()
		{
			InitializeComponent();
			markers = new GMapOverlay("markers");
			MainMap.Overlays.Add(markers);


			MainMap.MapProvider = OpenStreetMapProvider.Instance;
			GMaps.Instance.Mode = AccessMode.ServerOnly;
			MainMap.SetPositionByKeywords("Izhevsk, Russia");
			//MainMap.ShowCenter = false;
			MainMap.MinZoom = 12;
			MainMap.MaxZoom = 15;
			MainMap.Zoom = 12;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
		}

		private void button1_Click(object sender, EventArgs e)
		{
			MainMap.SetPositionByKeywords("Izhevsk, Russia");
			MainMap.Zoom = 12;
		}

		private void MainMap_OnMapZoomChanged()
		{
			if (marker != null)
				markers.Markers.Remove(marker);

			marker = new GMarkerGoogle(new PointLatLng(56.773518, 53.225074), mapController.GetMap(MapController.SizeFromZoom(MainMap.Zoom)));
			marker.IsHitTestVisible = false;
			markers.Markers.Add(marker);
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			foreach (var marker in markers.Markers)
				marker.Dispose();

			mapController.DeleteMaps();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			List<AvitoRow> data;
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				if (ofd.ShowDialog() != DialogResult.OK) return;

				data = AvitoHelper.Load(ofd.FileName);
			}

			MainMap.Zoom = 12;
			points.Clear();
			GPoint basePoint = MainMap.FromLatLngToLocal(new PointLatLng(56.773518, 53.225074));
			foreach (AvitoRow row in data)
			{
				GPoint localCoords = MainMap.FromLatLngToLocal(new PointLatLng(row.Coords.Latitude, row.Coords.Longitude));
				long x = localCoords.X - (basePoint.X - 500);
				long y = localCoords.Y - (basePoint.Y - 1000);

				double del = double.IsInfinity(row.Area) ? double.Parse(row.Parameters["Площадь"]) : row.Area;

				if (double.IsInfinity(del) || double.IsNaN(del) || del <= 0)
					throw new Exception("Жопа");
				double value = row.Price / del;
				points.Add(new ValuePoint(x, y, value));
			}

			mapController = new MapController(points);
			mapController.CreateMaps();
			MainMap_OnMapZoomChanged();
		}
	}
}
