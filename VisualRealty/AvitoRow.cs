using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VisualRealty
{
	[DataContract]
	public struct TCoords
	{
		[DataMember(Name = "lat")]
		public double Latitude;
		[DataMember(Name = "lng")]
		public double Longitude;
	}

	[DataContract]
	public struct TImgurl
	{
		[DataMember(Name = "imgurl")]
		string ImageUrl;
	}

	[DataContract]
	public struct AvitoRow
	{
		[DataMember]
		long avitoid;
		[DataMember(Name = "coords")]
		public TCoords Coords;
		[DataMember]
		string city;
		[DataMember]
		string person_type;
		[DataMember]
		string source;
		[DataMember]
		string metro;
		[DataMember]
		string param_1943;
		[DataMember(Name = "url")]
		public string Url;
		[DataMember]
		int cat1_id;
		[DataMember]
		int param_2315;
		[DataMember]
		string description;
		[DataMember]
		string nedvigimost_type;
		[DataMember(Name = "price")]
		public int Price;
		[DataMember]
		string cat2;
		[DataMember]
		string param_;
		[DataMember]
		string cat1;
		[DataMember]
		long id;
		[DataMember]
		string param_2078;
		[DataMember]
		int param_2019;
		[DataMember]
		string person;
		[DataMember]
		string address;
		[DataMember]
		int param_2415;
		[DataMember]
		int cat2_id;
		[DataMember]
		DateTime time;
		[DataMember]
		string title;
		[DataMember]
		string param_2016;
		[DataMember(Name = "images")]
		TImgurl[] Images;
		[DataMember(Name = "param_2313")]
		public float Area;
		[DataMember(Name = "param_2515")]
		public float SecondArea;
		[DataMember]
		string phone;
		[DataMember(Name = "params")]
		public Dictionary<string, string> Parameters;
	}
}
