using Auction.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Auction.Web.Models
{
		public class AuctionItemBiddingModel
		{
				[Required(ErrorMessage = "{0} is verplicht")]
				[Display(Name = "Bod")]
				public decimal Bidding { get; set; }

				public DateTime Date { get; set; }
				public string DateFormatted
				{
						get
						{
								return Date.ToString("dd-MM-yyyy HH:mm");
						}
				}

				[Required(ErrorMessage = "{0} is verplicht")]
				[MaxLength(150, ErrorMessage = "{0} mag maximaal {1} karakters lang zijn")]
				[Display(Name = "Naam")]
				public string BiddingName { get; set; }

				[Required(ErrorMessage = "{0} is verplicht")]
				[MaxLength(150, ErrorMessage = "{0} mag maximaal {1} karakters lang zijn")]
				[Display(Name = "Straat")]
				public string BiddingStreet { get; set; }

				[Required(ErrorMessage = "{0} is verplicht")]
				[MaxLength(10, ErrorMessage = "{0} mag maximaal {1} karakters lang zijn")]
				[Display(Name = "Huisnummer")]
				public string BiddingHouseNumber { get; set; }

				[Required(ErrorMessage = "{0} is verplicht")]
				[MaxLength(10, ErrorMessage = "{0} mag maximaal {1} karakters lang zijn")]
				[Display(Name = "Postcode")]
				public string BiddingZipCode { get; set; }

				[Required(ErrorMessage = "{0} is verplicht")]
				[MaxLength(150, ErrorMessage = "{0} mag maximaal {1} karakters lang zijn")]
				[Display(Name = "Plaats")]
				public string BiddingCity { get; set; }

				[Required(ErrorMessage = "{0} is verplicht")]
				[EmailAddress(ErrorMessage = "Ongeldig e-mail")]
				[MaxLength(150, ErrorMessage = "{0} mag maximaal {1} karakters lang zijn")]
				[Display(Name = "E-mail")]
				public string BiddingEmail { get; set; }

				[Display(Name = "Telefoonnummer")]
				[MaxLength(20, ErrorMessage = "{0} mag maximaal {1} karakters lang zijn")]
				public string BiddingPhoneNumber { get; set; }

				[Display(Name = "Mobiel nummer")]
				[MaxLength(20, ErrorMessage = "{0} mag maximaal {1} karakters lang zijn")]
				public string BiddingMobileNumber { get; set; }


				public AuctionItemBiddingModel() { }
				public AuctionItemBiddingModel(AuctionItemBidding auctionItemBidding)
				{
						Bidding = auctionItemBidding.Bidding;
						Date = auctionItemBidding.Date;
						BiddingName = auctionItemBidding.BiddingName;
						BiddingStreet = auctionItemBidding.BiddingStreet;
						BiddingHouseNumber = auctionItemBidding.BiddingHouseNumber;
						BiddingZipCode = auctionItemBidding.BiddingZipCode;
						BiddingCity = auctionItemBidding.BiddingCity;
						BiddingEmail = auctionItemBidding.BiddingEmail;
						BiddingPhoneNumber = auctionItemBidding.BiddingPhoneNumber;
						BiddingMobileNumber = auctionItemBidding.BiddingMobileNumber;
				}

				public AuctionItemBidding ToDalEntity(int auctionItemId)
				{
						var result = new AuctionItemBidding();

						result.Date = DateTime.Now;
						result.AuctionItemId = auctionItemId;
						result.Bidding = Bidding;
						result.BiddingName = BiddingName;
						result.BiddingStreet = BiddingStreet;
						result.BiddingHouseNumber = BiddingHouseNumber;
						result.BiddingZipCode = BiddingZipCode;
						result.BiddingCity = BiddingCity;
						result.BiddingEmail = BiddingEmail;
						result.BiddingPhoneNumber = BiddingPhoneNumber;
						result.BiddingMobileNumber = BiddingPhoneNumber;

						return result;
				}
		}
}