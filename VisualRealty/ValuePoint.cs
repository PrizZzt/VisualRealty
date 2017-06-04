namespace VisualRealty
{
	public struct ValuePoint
	{
		public long X;
		public long Y;

		public double Latitude;
		public double Longitude;

		public double Value;

		public string Url;

		public ValuePoint(long x, long y, double lat, double lng, double value,string url)
		{
			X = x;
			Y = y;

			Latitude = lat;
			Longitude = lng;

			Value = value;

			Url = url;
		}
	}
}
