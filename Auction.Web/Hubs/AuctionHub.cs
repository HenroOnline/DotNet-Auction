using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auction.Web.Hubs
{
		public class AuctionHub : Hub
		{
				private static IHubContext instance;
				protected static IHubContext Instance
				{
						get
						{
								if (instance == null)
								{
										instance = GlobalHost.ConnectionManager.GetHubContext<AuctionHub>();
								}
								return instance;
						}
				}

				public static void UpdateAuctionHighestBid(int auctionItemId, DateTime biddingDate, decimal highestBid)
				{
						Instance.Clients.All.updateAuctionHighestBid(auctionItemId, biddingDate.ToString("dd-MM-yyyy HH:mm"), highestBid.ToString("0.00"));
				}
		}
}