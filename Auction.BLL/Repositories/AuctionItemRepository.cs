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
		public AuctionItem Select(Guid uniqueKey)
		{
			return this.Context.Set<AuctionItem>().FirstOrDefault(a => a.UniqueKey == uniqueKey
																														&& a.ModifiedKind != "D");
		}

		public List<AuctionItem> ListOrderedByDate()
		{
			var result = List();

			var currentDate = DateTime.Now.Date;

			return result.Where(ai => ai.ModifiedKind != "D")
									 .Where(ai => ai.Ended == false)
									 .OrderByDescending(ai => ai.Date).ToList();
		}

		public void EndAuction(int auctionId)
		{
			var data = Select(auctionId);
			if (data == null || data.Ended)
			{
				return;
			}

			data.Ended = true;
			data.EndedDate = DateTime.Now;

			Save(data);
		}
	}
}
