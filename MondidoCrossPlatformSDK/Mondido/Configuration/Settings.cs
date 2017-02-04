
using System.Collections.Generic;

namespace Mondido.Configuration
{
    public static class Settings
    {
		private static List<string> _errors;
		public static string ApiBaseUrl 
        {
            get
            {
                return "http://api.localmondido.com:3000/v1";
            }
        }

		public static string ApiUsername { get; set; }

		public static string ApiPassword { get; set; }

		public static string ApiSecret { get; set; }

		public static string RSAKey { get; set; }
		public static List<string> Errors
		{
			get
			{
				if (_errors == null)
				{
					_errors = new List<string>();
				}
				return _errors;
			}
		}

    }
}
