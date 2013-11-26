using Auction.BLL.Repositories;
using Auction.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auction.Web.Models
{
		public class AuctionItemDetailModel
		{
				public int Id { get; set; }

				public string AuctionNumber { get; set; }

				public string Title { get; set; }

				public MvcHtmlString Description { get; set; }

				public decimal MinimumPrice { get; set; }

				public int ImageFileAttachment { get; set; }

				public List<int> ImageFileAttachments { get; set; }

				public List<AuctionItemBiddingModel> Biddings { get; set; }

				public AuctionItemBiddingModel NewBidding { get; set; }

				public bool Ended { get; set; }

				public AuctionItemDetailModel() { }

				public AuctionItemDetailModel(AuctionItem auctionItem)
				{
						Id = auctionItem.Id;
						AuctionNumber = auctionItem.AuctionNumber;
						Title = auctionItem.Title;
						Description = new MvcHtmlString(auctionItem.Description.Replace(Environment.NewLine, "<br/>"));
						MinimumPrice = auctionItem.MinimumPrice;
						Ended = auctionItem.Ended;
					
						ImageFileAttachments = new List<int>();
						foreach (var fileAttachmentAuctionItem in FileAttachmentAuctionItemRepository.Instance.ListByAuctionItem(auctionItem.Id))
						{
								ImageFileAttachments.Add(fileAttachmentAuctionItem.FileAttachmentId);
						}

						Biddings = new List<AuctionItemBiddingModel>();

						AuctionItemBiddingRepository.Instance.ListByAuctionItem(auctionItem.Id).ForEach(b => Biddings.Add(new AuctionItemBiddingModel(b)));

						ImageFileAttachment = 0;
						if (ImageFileAttachments.Count > 0)
						{
								ImageFileAttachment = ImageFileAttachments[0];
						}
				}
		}
}