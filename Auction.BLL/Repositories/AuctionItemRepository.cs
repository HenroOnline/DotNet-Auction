using Auction.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.Repositories
{
		public class AuctionItemRepository : BaseRepository<AuctionItem, AuctionItemRepository>
		{
				public IEnumerable<AuctionItem> ListOrderedByDate(bool onlyNotExpiredItems = true)
				{						
						var result = List();

						var currentDate = DateTime.Now.Date;
						var daysValid = Config.Auction.DaysValid;

						return result.Where(ai => !onlyNotExpiredItems || ai.Date.Date.AddDays(daysValid) >= currentDate)
												 .OrderBy(ai => ai.Date).ToList();
				}				
		}
}
