﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL
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

		public static bool GetConfigValueAsBoolean(string key)
		{
			var rawResult = GetConfigValue(key);

			if (!string.IsNullOrEmpty(rawResult))
			{
				return bool.Parse(rawResult);
			}

			return false;
		}

		public static string DataRootFolder
		{
			get { return GetConfigValue("DataRootFolder"); }
		}

		public static string AccountNumber
		{
			get { return GetConfigValue("AccountNumber"); }
		}

		public static string GoogleTrackingId
		{
			get
			{
				return GetConfigValue("GoogleTrackingId");
			}
		}

		public static string GoogleTrackingName
		{
			get
			{
				return GetConfigValue("GoogleTrackingName");
			}
		}

		public static class Mail
		{
			public static string SmtpHostname
			{
				get
				{
					return GetConfigValue("Mail.SmtpHostname");
				}
			}

			public static int SmtpPortnumber
			{
				get
				{
					return GetConfigValueAsInteger("Mail.SmtpPortnumber");
				}
			}

			public static string SmtpUsername
			{
				get
				{
					return GetConfigValue("Mail.SmtpUsername");
				}
			}

			public static string SmtpPassword
			{
				get
				{
					return GetConfigValue("Mail.SmtpPassword");
				}
			}

			public static bool SmtpEnableSsl
			{
				get
				{
					return GetConfigValueAsBoolean("Mail.SmtpEnableSsl");
				}
			}

			public static string SenderAddress
			{
				get
				{
					return GetConfigValue("Mail.SenderAddress");
				}
			}

			public static string AdminAddress
			{
				get
				{
					return GetConfigValue("Mail.AdminAddress");
				}
			}
		}
	}
}
