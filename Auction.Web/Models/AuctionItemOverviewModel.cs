using Auction.BLL.Repositories;
using Auction.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auction.Web.Models
{
		public class AuctionItemOverviewModel
		{
				public int Id { get; set; }

				private DateTime Date { get; set; }

				public string DateFormatted
				{
						get
						{
								return Date.ToString("dd-MM-yyyy");
						}
				}

				public string Title { get; set; }

				public string VenderZipCode { get; set; }

				public string VendorCity { get; set; }

				public decimal MinimumPrice { get; set; }

				protected decimal HighestBidding { get; set; }

				public string HighestBiddingFormatted
				{
						get
						{
								return HighestBidding.ToString("0.00");
						}
				}

				private DateTime HighestBiddingDate { get; set; }
				public string HighestBiddingDateFormatted
				{
						get
						{
								return HighestBiddingDate.ToString("dd-MM-yyyy HH:mm");
						}
				}

				public MvcHtmlString HighestBiddingAsString
				{
						get
						{
								if (HighestBidding != 0)
								{
										return new MvcHtmlString(string.Format("<strong>{0}</strong> op <strong>{1}</strong>", HighestBiddingFormatted, HighestBiddingDateFormatted));
								}
								return new MvcHtmlString("-");
						}
				}

				public string ImageUrl { get; set; }

				public AuctionItemOverviewModel(AuctionItem auctionItem)
				{
						Id = auctionItem.Id;
						Date = auctionItem.Date;
						Title = auctionItem.Title;
						VenderZipCode = auctionItem.VendorZipCode;
						VendorCity = auctionItem.VendorCity;
						MinimumPrice = auctionItem.MinimumPrice;

						var highestBidding = AuctionItemBiddingRepository.Instance.SelectHighest(auctionItem.Id);
						if (highestBidding != null)
						{
								HighestBidding = highestBidding.Bidding;
								HighestBiddingDate = highestBidding.Date;
						}

						var fileAttachmentAuctionItem = FileAttachmentAuctionItemRepository.Instance.SelectFirstByAuctionItem(auctionItem.Id);
						var fileAttachmentId = 0;
						if (fileAttachmentAuctionItem != null)
						{
								fileAttachmentId = fileAttachmentAuctionItem.FileAttachmentId;
						}

						ImageUrl = string.Format("/FileAttachment/{0}?maxheight=250&maxwidth=250", fileAttachmentId);
				}
		}
}