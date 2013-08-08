using Auction.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Auction.Web.Models
{
		public class AuctionItemAddModel
		{
				[Required(ErrorMessage = "{0} is verplicht")]
				[MaxLength(150, ErrorMessage = "{0} mag maximaal {1} karakters lang zijn")]
				[Display(Name = "Titel")]
				public string Title { get; set; }

				[Required(ErrorMessage = "{0} is verplicht")]
				[Display(Name = "Omschrijving")]
				public string Description { get; set; }

				[Required(ErrorMessage = "{0} is verplicht")]
				[Display(Name = "Minimum prijs")]
				public decimal MinimumPrice { get; set; }

				[Required(ErrorMessage = "{0} is verplicht")]
				[MaxLength(150, ErrorMessage = "{0} mag maximaal {1} karakters lang zijn")]
				[Display(Name = "Naam")]
				public string VendorName { get; set; }

				[Required(ErrorMessage = "{0} is verplicht")]
				[MaxLength(150, ErrorMessage = "{0} mag maximaal {1} karakters lang zijn")]
				[Display(Name = "Straat")]
				public string VendorStreet { get; set; }

				[Required(ErrorMessage = "{0} is verplicht")]
				[MaxLength(10, ErrorMessage = "{0} mag maximaal {1} karakters lang zijn")]
				[Display(Name = "Huisnummer")]
				public string VendorHouseNumber { get; set; }

				[Required(ErrorMessage = "{0} is verplicht")]
				[MaxLength(10, ErrorMessage = "{0} mag maximaal {1} karakters lang zijn")]
				[Display(Name = "Postcode")]
				public string VendorZipCode { get; set; }

				[Required(ErrorMessage = "{0} is verplicht")]
				[MaxLength(150, ErrorMessage = "{0} mag maximaal {1} karakters lang zijn")]
				[Display(Name = "Plaats")]
				public string VendorCity { get; set; }

				[Required(ErrorMessage = "{0} is verplicht")]
				[EmailAddress(ErrorMessage = "Ongeldig e-mail")]
				[MaxLength(150, ErrorMessage = "{0} mag maximaal {1} karakters lang zijn")]
				[Display(Name = "E-mail")]
				public string VendorEmail { get; set; }

				[Display(Name = "Telefoonnummer")]
				[MaxLength(20, ErrorMessage = "{0} mag maximaal {1} karakters lang zijn")]
				public string VendorPhoneNumber { get; set; }

				[Display(Name = "Mobiel nummer")]
				[MaxLength(20, ErrorMessage = "{0} mag maximaal {1} karakters lang zijn")]
				public string VendorMobileNumber { get; set; }

				public AuctionItem ToDalEntity()
				{
						var result = new AuctionItem();

						result.Date = DateTime.Now;
						result.Title = Title;
						result.Description = Description;
						result.MinimumPrice = MinimumPrice;
						result.VendorName = VendorName;
						result.VendorStreet = VendorStreet;
						result.VendorHouseNumber = VendorHouseNumber;
						result.VendorZipCode = VendorZipCode;
						result.VendorCity = VendorCity;
						result.VendorEmail = VendorEmail;
						result.VendorPhoneNumber = VendorPhoneNumber;
						result.VendorMobileNumber = VendorMobileNumber;

						return result;
				}
		}
}