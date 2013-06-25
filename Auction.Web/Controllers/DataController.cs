using Auction.DAL.Entities;
using Auction.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Auction.Web.Controllers
{
    public class DataController : ApiController
    {
				public List<AuctionItem> GetAuctionItems()
				{
						return AuctionItemRepository.Instance.ListOrderedByDate();
				}

				[System.Web.Mvc.HttpPost]
				public HttpResponseMessage CreateAuctionItem(AuctionItem auctionItem)
				{
						//
						AuctionItemRepository.Instance.Save(auctionItem);

						return Request.CreateResponse<AuctionItem>(HttpStatusCode.Created, auctionItem);
				}
    }
}
