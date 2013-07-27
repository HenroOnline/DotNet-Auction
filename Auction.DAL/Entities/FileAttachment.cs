using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Entities
{
		public class FileAttachment : Base
		{
				public string Name { get; set; }
				public string Extension { get; set; }
				public String ContentType { get; set; }
				public Int32 SizeInBytes { get; set; }

				public ICollection<FileAttachmentAuctionItem> FileAttachmentAuctionItems { get; set; }
		}
}
