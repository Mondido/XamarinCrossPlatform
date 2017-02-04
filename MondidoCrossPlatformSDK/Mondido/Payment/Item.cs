using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MondidoCrossPlatformSDK.Payment
{
	[DataContract]
	public class Item
	{
		
		public Item()
		{
			
		}

		[JsonProperty(PropertyName = "artno")]
		public string Artno { get; set; }
		[JsonProperty(PropertyName = "description")]
		public string Description { get; set; }
		[JsonProperty(PropertyName = "amount")]
		public string Amount { get; set; }
		[JsonProperty(PropertyName = "vat")]
		public string Vat { get; set; }
		[JsonProperty(PropertyName = "discount")]
		public string Discount { get; set; }
		[JsonProperty(PropertyName = "qty")]
		public string Qty { get; set; }

	}
}
