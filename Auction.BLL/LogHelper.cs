using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL
{
		public class LogHelper
		{
				public static void LogEvent(Exception ex)
				{
						try
						{
								MailHelper.Send(Config.Mail.SenderAddress, Config.Mail.AdminAddress, "Er is een fout opgetreden", ex.ToString());
						}
						catch (Exception)
						{
								// Even logging of the exception fails :(..
						}
				}
		}
}
