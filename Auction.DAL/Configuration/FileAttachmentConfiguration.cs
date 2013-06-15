using Auction.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Configuration
{
		public class FileAttachmentConfiguration : BaseConfiguration<FileAttachment>
		{
				public FileAttachmentConfiguration()
				{
						this.HasMany(f => f.AuctionItems);
				}
		}
}
