using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Entities
{
		public class AuctionItemBidding : Base
		{
				public decimal Bidding { get; set; }

				public DateTime Date { get; set; }

				public string BiddingName { get; set; }

				public string BiddingStreet { get; set; }
				public string BiddingHouseNumber { get; set; }
				public string BiddingZipCode { get; set; }
				public string BiddingCity { get; set; }

				public string BiddingEmail { get; set; }
				public string BiddingPhoneNumber { get; set; }
				public string BiddingMobileNumber { get; set; }

				public AuctionItem AuctionItem { get; set; }
		}
}
