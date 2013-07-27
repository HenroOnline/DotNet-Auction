using Auction.BLL.Repositories;
using Auction.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auction.Web.Models
{
		public class AuctionItemDetailModel
		{
				public int Id { get; set; }

				public string Title { get; set; }

				public string Description { get; set; }

				public string ImageUrl { get; set; }

				public List<string> ImageUrls { get; set; }

				public AuctionItemDetailModel(AuctionItem auctionItem)
				{
						Id = auctionItem.Id;
						Title = auctionItem.Title;
						Description = auctionItem.Description;

						var firstFileAttachmentAuctionItem = FileAttachmentAuctionItemRepository.Instance.SelectFirstByAuctionItem(auctionItem.Id);

						if (firstFileAttachmentAuctionItem != null)
						{
								ImageUrl = string.Format("/FileAttachment/{0}?maxheight=250&maxwidth=250", firstFileAttachmentAuctionItem.FileAttachmentId);
						}
						else
						{
								ImageUrl = "/Content/img/NoImageAvailable.jpg";
						}

						ImageUrls = new List<string>();

						foreach (var fileAttachmentAuctionItem in FileAttachmentAuctionItemRepository.Instance.ListByAuctionItem(auctionItem.Id))
						{
								ImageUrls.Add(string.Format("/FileAttachment/{0}?maxheight=250&maxwidth=250", fileAttachmentAuctionItem.FileAttachmentId));
						}

				}
		}
}