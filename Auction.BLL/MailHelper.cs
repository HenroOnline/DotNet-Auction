using Auction.BLL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

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

				public static void SendAuctionEndedMails()
				{
						var auctionItems = AuctionItemRepository.Instance.ListExpiredWithNoMailSended();

						var messageToAdmin = "Er zijn geen veiling items gestopt vandaag";
						if (auctionItems.Count > 0)
						{
								messageToAdmin = "";
								foreach (var auctionItem in auctionItems)
								{
										messageToAdmin = string.Concat(messageToAdmin, auctionItem.Id, ": ", auctionItem.Title, "<br/>");
								}
						}

						Send(Config.Mail.SenderAddress, Config.Mail.AdminAddress, "Stopzetten veiling items", messageToAdmin);

						foreach (var auctionItem in auctionItems)
						{
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
										messageToBidder.Append(string.Format("Wanneer u het bedrag van € {0} overgemaakt heeft op rekeningnummer {1} onder vermelding van {2} kunt u het veiling item ophalen op onderstaand adres.<br/><br/>", highestBid.Bidding, Config.AccountNumber, auctionItem.AuctionNumber));
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

								auctionItem.AuctionEndedMailSended = true;
								AuctionItemRepository.Instance.Save(auctionItem);
						}
				}
		}
}
