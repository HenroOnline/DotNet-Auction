using Auction.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Configuration
{
		public class FileAttachmentAuctionItemConfiguration : BaseConfiguration<FileAttachmentAuctionItem>
		{
				public FileAttachmentAuctionItemConfiguration()
				{
						if (!string.IsNullOrEmpty(AuctionContext.SchemaName))
						{
								this.ToTable("FileAttachmentAuctionItem", AuctionContext.SchemaName);
						}
						
						HasRequired(fai => fai.AuctionItem);
						HasRequired(fai => fai.FileAttachment);

						this.Property(fai => fai.Sequence).IsRequired();
				}
		}
}
