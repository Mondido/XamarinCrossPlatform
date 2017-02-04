using System;
using System.Collections.Generic;
using Mondido.Utils;

namespace MondidoCrossPlatformSDK.Payment
{
	public class Items : List<Item>
	{
		public Items()
		{
		}

		public override string ToString()
		{
			return ToString(false);
		}

		public string ToString(bool base64Encode)
		{
			var str = Newtonsoft.Json.JsonConvert.SerializeObject(this);
			if (base64Encode)
			{
				return str.ToBase64();
			}
			return str;
		}
	}
}
