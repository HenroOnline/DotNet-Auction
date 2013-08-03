using Auction.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.Repositories
{
		public class FileAttachmentAuctionItemRepository : BaseRepository<FileAttachmentAuctionItem, FileAttachmentAuctionItemRepository>
		{
				public FileAttachmentAuctionItem SelectFirstByAuctionItem(int auctionItemId)
				{
						return this.Context.Set<FileAttachmentAuctionItem>().Where(fai => fai.AuctionItem.Id == auctionItemId
																																				&& fai.ModifiedKind != "D")
																																.OrderBy(fai => fai.Sequence).FirstOrDefault();
				}

				public List<FileAttachmentAuctionItem> ListByAuctionItem(int auctionItemId)
				{
						return this.Context.Set<FileAttachmentAuctionItem>().Where(fai => fai.AuctionItem.Id == auctionItemId
																																				&& fai.ModifiedKind != "D")
																																.OrderBy(fai => fai.Sequence).ToList();
				}

				public void Create(int auctionItemId, int fileAttachmentId, int sequence)
				{
						var result = new FileAttachmentAuctionItem
						{
								AuctionItemId = auctionItemId,
								FileAttachmentId = fileAttachmentId,
								Sequence = sequence
						};

						Save(result);
				}
		}
}
