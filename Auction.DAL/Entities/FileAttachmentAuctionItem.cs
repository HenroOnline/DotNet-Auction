using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Entities
{
		public class FileAttachmentAuctionItem : Base
		{
				public int Sequence { get; set; }

				public int FileAttachmentId { get; set; }
				public FileAttachment FileAttachment { get; set; }

				public int AuctionItemId { get; set; }
				public AuctionItem AuctionItem { get; set; }
		}
}
