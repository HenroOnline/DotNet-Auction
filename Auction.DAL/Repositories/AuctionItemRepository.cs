using Auction.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Repositories
{
		public class AuctionItemRepository : BaseRepository<AuctionItem, AuctionItemRepository>
		{
				public List<AuctionItem> ListOrderedByDate()
				{
						var result = List();

						return result.OrderByDescending(ai => ai.Date).ToList();
				}
		}
}
