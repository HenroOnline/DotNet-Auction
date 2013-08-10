using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Entities
{
		public class AuctionItem : Base
		{
				[NotMapped]
				public string AuctionNumber
				{
						get
						{
								return string.Concat("VN", Id.ToString().PadLeft(5, '0'));
						}
				}

				public DateTime Date { get; set; }

				public string Title { get; set; }

				public string Description { get; set; }

				public decimal MinimumPrice { get; set; }

				public string VendorName { get; set; }

				public string VendorStreet { get; set; }
				public string VendorHouseNumber { get; set; }
				public string VendorZipCode { get; set; }
				public string VendorCity { get; set; }

				public string VendorEmail { get; set; }
				public string VendorPhoneNumber { get; set; }
				public string VendorMobileNumber { get; set; }

				public bool AuctionEndedMailSended { get; set; }

				public ICollection<AuctionItemBidding> Biddings { get; set; }

				public ICollection<FileAttachmentAuctionItem> FileAttachmentAuctionItems { get; set; }
		}
}
