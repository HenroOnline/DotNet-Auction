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
				public List<AuctionItem> ListOrderedByDate(bool onlyNotExpiredItems = true)
				{						
						var result = List();

						var currentDate = DateTime.Now.Date;
						var daysValid = Config.Auction.DaysValid;

						return result.Where(ai => ai.ModifiedKind != "D")	
												 .Where(ai => !onlyNotExpiredItems || ai.Date.Date.AddDays(daysValid) >= currentDate)
												 .OrderByDescending(ai => ai.Date).ToList();
				}

				public List<AuctionItem> ListExpiredWithNoMailSended()
				{
						var result = List();

						var currentDate = DateTime.Now.Date;
						var daysValid = Config.Auction.DaysValid;

						return result.Where(ai => ai.ModifiedKind != "D")
												 .Where(ai => ai.Date.Date.AddDays(daysValid) < currentDate)
												 .Where(ai => ai.AuctionEndedMailSended == false)
												 .OrderByDescending(ai => ai.Date).ToList();
				}
		}
}
