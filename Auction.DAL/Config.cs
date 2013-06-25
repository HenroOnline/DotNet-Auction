using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL
{
		public static class Config
		{
				public static string GetConfigValue(string key)
				{
						return ConfigurationManager.AppSettings[key];
				}

				public static int GetConfigValueAsInteger(string key, int defaultValue = 0)
				{
						var rawResult = GetConfigValue(key);

						if (!string.IsNullOrEmpty(rawResult))
						{
								return int.Parse(rawResult);
						}

						return defaultValue;
				}

				public static class Auction
				{
						public static int DaysValid
						{
								get
								{
										return GetConfigValueAsInteger("Auction.DaysValid", 14);
								}
						}
				}
		}
}
