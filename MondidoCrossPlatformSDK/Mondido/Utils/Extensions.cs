using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Mondido.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;

namespace Mondido.Utils
{
    public static class Extensions
    {
        public static T FromJson<T>(this string s)
        {
            return JsonConvert.DeserializeObject<T>(s);
        }

        public static IEnumerable ToJson(this IEnumerable s)
        {
            return JsonConvert.SerializeObject(s);
        }

		public static string ToJsonString(this string s, bool base64Encode = false)
		{
			string str = string.Empty;
			if (s.StartsWith("["))
			{
				str =  JArray.Parse(s).ToString();
			}
			str =  JObject.Parse(s).ToString();

			if (base64Encode)
			{
				return Base64Encode(str);
			}
			return str;
		}
		/// <summary>
		/// String to MD5
		/// </summary>
		/// <returns>The hash.</returns>
		/// <param name="s">String</param>
		public static string ToMD5(this string s)
		{
			var dig = new Org.BouncyCastle.Crypto.Digests.MD5Digest();
			byte[] msgBytes = Encoding.UTF8.GetBytes(s);
			dig.BlockUpdate(msgBytes, 0, msgBytes.Length);
			byte[] result = new byte[dig.GetDigestSize()];
			dig.DoFinal(result, 0);
			return ByteArrayToHex(result);
		}

		private static string ByteArrayToHex(byte[] hash)
		{
			var hex = new StringBuilder(hash.Length * 2);
			foreach (byte b in hash)
				hex.AppendFormat("{0:x2}", b);

			return hex.ToString();
		}

		public static string ToBase64(this string s)
		{
			return Base64Encode(s);
		}

		public static string Base64Encode(string plainText)
		{
			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
			return System.Convert.ToBase64String(plainTextBytes);
		}

		public static string RSAEncrypt(this string s)
		{

			var bytesToEncrypt = Encoding.UTF8.GetBytes(Base64Encode(s));

			var encryptEngine = new Pkcs1Encoding(new RsaEngine());

			using (var txtreader = new StringReader(Settings.RSAKey))
			{
				var keyParameter = (AsymmetricKeyParameter)new PemReader(txtreader).ReadObject();

				encryptEngine.Init(true, keyParameter);
			}

			var encrypted = Convert.ToBase64String(encryptEngine.ProcessBlock(bytesToEncrypt, 0, bytesToEncrypt.Length));
			return encrypted;
		}
    }
}