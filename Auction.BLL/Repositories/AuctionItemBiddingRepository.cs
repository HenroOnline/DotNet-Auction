using Auction.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.Repositories
{
		public class AuctionItemBiddingRepository : BaseRepository<AuctionItemBidding, AuctionItemBiddingRepository>
		{
				public AuctionItemBidding SelectHighest(int auctionItemId)
				{
						return this.Context.Set<AuctionItemBidding>().Where(t => t.ModifiedKind != "D")
														.Where(t => t.AuctionItemId == auctionItemId)
														.OrderByDescending(t => t.Bidding)
														.Take(1)														
														.FirstOrDefault();														
				}

				public AuctionItemBidding SelectPreviousHighestBid(int auctionItemId)
				{
						var result = ListByAuctionItem(auctionItemId);

						if (result.Count > 1)
						{
								return result[1];
						}

						return null;
				}

				public List<AuctionItemBidding> ListByAuctionItem(int auctionItemId)
				{
						return this.Context.Set<AuctionItemBidding>().Where(t => t.ModifiedKind != "D")
														.Where(t => t.AuctionItemId == auctionItemId)
														.OrderByDescending(t => t.Bidding)
														.ToList();

				}
		}
}
