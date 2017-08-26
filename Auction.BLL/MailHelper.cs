using Auction.BLL.Repositories;
using Auction.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Auction.BLL
{
	public class MailHelper
	{
		public static bool Send(string from, string to, string subject, string body)
		{
			var bodyContent = string.Format("<html><head><title>{0}</title></head><body>{1}</body></html>", subject, body);

			Boolean result = false;

			SmtpClient smtpClient = new SmtpClient(Config.Mail.SmtpHostname, Config.Mail.SmtpPortnumber);
			if (!String.IsNullOrEmpty(Config.Mail.SmtpUsername))
			{
				smtpClient.Credentials = new NetworkCredential(Config.Mail.SmtpUsername, Config.Mail.SmtpPassword);
			}
			smtpClient.EnableSsl = Config.Mail.SmtpEnableSsl;

			var mailMessage = new MailMessage(from, to, subject, body);
			mailMessage.IsBodyHtml = true;

			Int32 counter = 1;
			Int32 tries = 3;

			do
			{
				try
				{
					smtpClient.Send(mailMessage);
					result = true;
					break;
				}
				catch
				{
					if (counter < tries)
					{
						System.Threading.Thread.Sleep(200);
						counter++;
					}
				}
			} while (counter <= tries);


			return result;
		}

		public static bool SendAuctionEndedMails(int auctionItemId)
		{
			var auctionItem = AuctionItemRepository.Instance.Select(auctionItemId);
			if (auctionItem == null || auctionItem.Ended)
			{
				return false;
			}

			try
			{
				var messageToAdmin = string.Format("Veiling item {0}, {1} wordt gestopt.", auctionItem.AuctionNumber, auctionItem.Title);
				Send(Config.Mail.SenderAddress, Config.Mail.AdminAddress, "Stopzetten veiling item", messageToAdmin);

				var highestBid = AuctionItemBiddingRepository.Instance.SelectHighest(auctionItem.Id);
				{
					var messageToVendor = new StringBuilder();
					messageToVendor.Append(string.Format("Beste {0},<br/>", auctionItem.VendorName));
					messageToVendor.Append("<br/>");
					messageToVendor.Append(string.Format("De veiling voor veiling item {0}, {1} is stopgezet.<br/><br/>", auctionItem.AuctionNumber, auctionItem.Title));

					if (highestBid != null)
					{
						messageToVendor.Append(string.Format("De hoogste bieder met een bedrag van € {0} is:<br/>", highestBid.Bidding));
						messageToVendor.Append(string.Format("{0}<br/>", highestBid.BiddingName));
						messageToVendor.Append(string.Format("{0} {1}<br/>", highestBid.BiddingStreet, highestBid.BiddingHouseNumber));
						messageToVendor.Append(string.Format("{0} {1}<br/>", highestBid.BiddingZipCode, highestBid.BiddingCity));
						messageToVendor.Append(string.Format("{0}<br/>", highestBid.BiddingEmail));

						if (!string.IsNullOrEmpty(highestBid.BiddingPhoneNumber))
						{
							messageToVendor.Append(string.Format("{0}<br/>", highestBid.BiddingPhoneNumber));
						}

						if (!string.IsNullOrEmpty(highestBid.BiddingMobileNumber))
						{
							messageToVendor.Append(string.Format("{0}<br/>", highestBid.BiddingMobileNumber));
						}

						messageToVendor.Append(string.Format("<br />U kunt u bijdrage overmaken op rekeningnummer {0} o.v.v. het veiling nummer.<br/>", Config.AccountNumber));
					}
					else
					{
						messageToVendor.Append("Er zijn geen biedingen uitgebracht.<br/>");
					}

					messageToVendor.Append("<br/>");
					messageToVendor.Append("Mvg,<br/><br/>");
					messageToVendor.Append("Veiling Hervormd Rouveen");

					Send(Config.Mail.SenderAddress, auctionItem.VendorEmail, string.Format("Veiling item {0}, {1}", auctionItem.AuctionNumber, auctionItem.Title), messageToVendor.ToString());
				}

				if (highestBid != null)
				{
					var messageToBidder = new StringBuilder();

					messageToBidder.Append(string.Format("Beste {0},<br/>", highestBid.BiddingName));
					messageToBidder.Append("<br/>");
					messageToBidder.Append(string.Format("De veiling voor veiling item {0}, {1} is stopgezet.<br/>", auctionItem.AuctionNumber, auctionItem.Title));
					messageToBidder.Append("U bent de hoogste bieder.<br/>");
					messageToBidder.Append("<br/>");
					messageToBidder.Append(string.Format("U kunt het veiling item ophalen op onderstaand adres.<br/><br/>", highestBid.Bidding, Config.AccountNumber, auctionItem.AuctionNumber));
					messageToBidder.Append(string.Format("{0}<br/>", auctionItem.VendorName));
					messageToBidder.Append(string.Format("{0} {1}<br/>", auctionItem.VendorStreet, auctionItem.VendorHouseNumber));
					messageToBidder.Append(string.Format("{0} {1}<br/>", auctionItem.VendorZipCode, auctionItem.VendorCity));
					messageToBidder.Append(string.Format("{0}<br/>", auctionItem.VendorEmail));
					if (!string.IsNullOrEmpty(auctionItem.VendorPhoneNumber))
					{
						messageToBidder.Append(string.Format("{0}<br/>", auctionItem.VendorPhoneNumber));
					}

					if (!string.IsNullOrEmpty(auctionItem.VendorMobileNumber))
					{
						messageToBidder.Append(string.Format("{0}<br/>", auctionItem.VendorMobileNumber));
					}
					messageToBidder.Append("<br/>");
					messageToBidder.Append("Mvg,<br/><br/>");
					messageToBidder.Append("Veiling Hervormd Rouveen");

					Send(Config.Mail.SenderAddress, highestBid.BiddingEmail, string.Format("Veiling item {0}, {1}", auctionItem.AuctionNumber, auctionItem.Title), messageToBidder.ToString());
				}

				return true;
			}
			catch (Exception ex)
			{
				LogHelper.LogEvent(ex);
				return false;
			}
		}

		public static void SendNewAuctionItemMail(int auctionItemId)
		{
			var auctionItem = AuctionItemRepository.Instance.Select(auctionItemId);
			if (auctionItem == null)
			{
				return;
			}

			try
			{
				var messageToVendor = new StringBuilder();
				var manageLink = string.Format("{0}/Auction/Manage/{1}", HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), auctionItem.UniqueKey);

				messageToVendor.Append(string.Format("Beste {0},<br/>", auctionItem.VendorName));
				messageToVendor.Append("<br/>");				
				messageToVendor.Append(string.Format("Via deze <a href=\"{0}\">link</a> kunt u uw veiling item beheren.<br/>", manageLink));
				messageToVendor.Append("<br/>");
				messageToVendor.Append("Mvg,<br/><br/>");
				messageToVendor.Append("Veiling Hervormd Rouveen");

				Send(Config.Mail.SenderAddress, auctionItem.VendorEmail, string.Format("Bevestiging plaatsing veiling item {0}, {1}", auctionItem.AuctionNumber, auctionItem.Title), messageToVendor.ToString());
			}
			catch (Exception ex)
			{
				LogHelper.LogEvent(ex);
			}
		}
		
		public static void SendNewBidMails(int auctionItemId, string newBidEmailAddress)
		{
			var auctionItem = AuctionItemRepository.Instance.Select(auctionItemId);

			if (auctionItem == null)
			{
				return;
			}

			var previousHighestBid = AuctionItemBiddingRepository.Instance.SelectPreviousHighestBid(auctionItemId);
			if (previousHighestBid == null || previousHighestBid.BiddingEmail.Equals(newBidEmailAddress, StringComparison.InvariantCultureIgnoreCase))
			{
				// Highest bidder keeps the same.. No need to notify
				return;
			}

			try
			{
				var messageToPreviousBidders = new StringBuilder();

				messageToPreviousBidders.Append(string.Format("Beste {0},<br/>", previousHighestBid.BiddingName));
				messageToPreviousBidders.Append("<br/>");
				messageToPreviousBidders.Append(string.Format("Uw bod op veiling item {0}, {1} is overboden.<br/>", auctionItem.AuctionNumber, auctionItem.Title));
				messageToPreviousBidders.Append("<br/>");
				messageToPreviousBidders.Append(string.Format("Klik <a href=\"{0}/Auction/Detail/{1}\">hier</a> om naar de veiling site te gaan.<br/>", HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), auctionItem.Id));
				messageToPreviousBidders.Append("<br/>");
				messageToPreviousBidders.Append("Mvg,<br/><br/>");
				messageToPreviousBidders.Append("Veiling Hervormd Rouveen");

				Send(Config.Mail.SenderAddress, previousHighestBid.BiddingEmail, string.Format("Veiling item {0}, {1}", auctionItem.AuctionNumber, auctionItem.Title), messageToPreviousBidders.ToString());
			}
			catch (Exception ex)
			{
				LogHelper.LogEvent(ex);
			}
		}
		
		public static void SendBidConfirmationMail(int auctionItemBiddingId)
		{
			var auctionItemBidding = AuctionItemBiddingRepository.Instance.Select(auctionItemBiddingId);
			if (auctionItemBidding == null)
			{
				return;
			}

			var auctionItem = AuctionItemRepository.Instance.Select(auctionItemBidding.AuctionItemId);
			if (auctionItem == null)
			{
				return;
			}

			try
			{
				var messageToBidder = new StringBuilder();

				messageToBidder.Append(string.Format("Beste {0},<br/>", auctionItemBidding.BiddingName));
				messageToBidder.Append("<br/>");
				messageToBidder.Append(string.Format("Hierbij de bevestiging van uw bod van € {0} op veiling item {1}, {2}.<br/>", auctionItemBidding.Bidding, auctionItem.AuctionNumber, auctionItem.Title));
				messageToBidder.Append("<br/>");
				messageToBidder.Append("Mvg,<br/><br/>");
				messageToBidder.Append("Veiling Hervormd Rouveen");

				Send(Config.Mail.SenderAddress, auctionItemBidding.BiddingEmail, string.Format("Bevestiging bieding op veiling item {0}, {1}", auctionItem.AuctionNumber, auctionItem.Title), messageToBidder.ToString());
			}
			catch (Exception ex)
			{
				LogHelper.LogEvent(ex);
			}

			try
			{
				var messageToVendor= new StringBuilder();
				var manageLink = string.Format("{0}/Auction/Manage/{1}", HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), auctionItem.UniqueKey);

				messageToVendor.Append(string.Format("Beste {0},<br/>", auctionItem.VendorName));
				messageToVendor.Append("<br/>");
				messageToVendor.Append(string.Format("Er is een nieuw bod geplaatst op uw veiling item {0}, {1}.<br/>", auctionItem.AuctionNumber, auctionItem.Title));
				messageToVendor.Append("<br/>");
				messageToVendor.Append(string.Format("Via deze <a href=\"{0}\">link</a> kunt u de details bekijken.<br/>", manageLink));
				messageToVendor.Append("<br/>");
				messageToVendor.Append("Mvg,<br/><br/>");
				messageToVendor.Append("Veiling Hervormd Rouveen");

				Send(Config.Mail.SenderAddress, auctionItem.VendorEmail, string.Format("Nieuwe bieding op veiling item {0}, {1}", auctionItem.AuctionNumber, auctionItem.Title), messageToVendor.ToString());
			}
			catch (Exception ex)
			{
				LogHelper.LogEvent(ex);
			}
		}
	}
}
