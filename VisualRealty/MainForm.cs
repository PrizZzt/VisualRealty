using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace VisualRealty
{
	public partial class MainForm : Form
	{
		GMapOverlay markers;
		GMapMarker mapMarker;

		MapController mapController;
		string MarkersDataFileName = "MarkersData.json";

		public MainForm()
		{
			InitializeComponent();
			markers = new GMapOverlay("markers");
			MainMap.Overlays.Add(markers);

			MainMap.MapProvider = OpenStreetMapProvider.Instance;
			GMaps.Instance.Mode = AccessMode.ServerOnly;
			MainMap.SetPositionByKeywords("Izhevsk, Russia");
			MainMap.ShowCenter = false;
			MainMap.MinZoom = 12;
			MainMap.MaxZoom = 15;
			MainMap.Zoom = 12;

			mapController = new MapController();
			mapController.LoadPoints(MarkersDataFileName);
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			MainMap_OnMapZoomChanged();
			mapController.PlaceMarkers(markers);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			MainMap.SetPositionByKeywords("Izhevsk, Russia");
			MainMap.Zoom = 12;
		}

		private void MainMap_OnMapZoomChanged()
		{
			if (mapMarker != null)
				markers.Markers.Remove(mapMarker);

			Bitmap map = mapController.GetMap(MapController.SizeFromZoom(MainMap.Zoom));
			if (map != null)
			{
				mapMarker = new GMarkerGoogle(new PointLatLng(56.773518, 53.225074), map);
				mapMarker.IsHitTestVisible = false;
				markers.Markers.Add(mapMarker);
			}

			mapController.PlaceMarkers(markers);
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			foreach (var marker in markers.Markers)
				marker.Dispose();

			mapController.SavePoints(MarkersDataFileName);
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
			List<ValuePoint> points = new List<ValuePoint>();
			GPoint basePoint = MainMap.FromLatLngToLocal(new PointLatLng(56.773518, 53.225074));
			foreach (AvitoRow row in data)
			{
				GPoint localCoords = MainMap.FromLatLngToLocal(new PointLatLng(row.Coords.Latitude, row.Coords.Longitude));
				long x = localCoords.X - (basePoint.X - 500);
				long y = localCoords.Y - (basePoint.Y - 1000);

				double del = double.IsInfinity(row.Area) ? (double.IsInfinity(row.SecondArea) ? double.Parse(row.Parameters["Площадь"]) : row.SecondArea) : row.Area;

				if (double.IsInfinity(del) || double.IsNaN(del) || del <= 0)
					continue;
				double value = row.Price / del;
				points.Add(new ValuePoint(x, y, row.Coords.Latitude, row.Coords.Longitude, value, row.Url));
			}

			mapController.AddPoints(points);
			MainMap_OnMapZoomChanged();

			new Task(() =>
			{
				mapController.CreateMaps();
			}).Start();
		}

		private void MainMap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
		{
			System.Diagnostics.Process.Start(item.ToolTipText);
		}
	}
}
