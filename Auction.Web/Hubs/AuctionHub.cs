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

				public void Join(int auctionItemId)
				{
						var groupKey = (auctionItemId != 0) ? string.Format("AuctionItem_{0}", auctionItemId) : "AuctionOverview";
						Groups.Add(Context.ConnectionId, groupKey);
				}

				public static void UpdateAuctionHighestBid(int auctionItemId, DateTime biddingDate, decimal highestBid)
				{
						// Send message to all clients which are viewing the overview
						Instance.Clients.Group("AuctionOverview").updateAuctionHighestBid(auctionItemId, highestBid.ToString("0.00"));

						// Send message to all clients which are viewing detail page
						Instance.Clients.Group(string.Format("AuctionItem_{0}", auctionItemId)).updateAuctionBiddings(biddingDate.ToString("dd-MM-yyyy"), highestBid.ToString("0.00"));
				}
		}
}