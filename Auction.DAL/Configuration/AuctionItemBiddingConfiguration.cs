using Auction.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Configuration
{
		public class AuctionItemBiddingConfiguration : BaseConfiguration<AuctionItemBidding>
		{
				public AuctionItemBiddingConfiguration()
				{
						if (!string.IsNullOrEmpty(AuctionContext.SchemaName))
						{
								this.ToTable("AuctionItemBidding", AuctionContext.SchemaName);
						}

						this.Property(aib => aib.Bidding).IsRequired();
						this.Property(aib => aib.Date).IsRequired();

						this.Property(aib => aib.BiddingName).HasMaxLength(150)
																								.IsRequired();
						this.Property(aib => aib.BiddingStreet).HasMaxLength(150)
																										.IsRequired();
						this.Property(aib => aib.BiddingHouseNumber).HasMaxLength(10)
																												.IsRequired();
						this.Property(aib => aib.BiddingZipCode).HasMaxLength(10)
																										.IsRequired();
						this.Property(aib => aib.BiddingCity).HasMaxLength(150)
																								.IsRequired();

						this.Property(aib => aib.BiddingEmail).HasMaxLength(150);
						this.Property(aib => aib.BiddingPhoneNumber).HasMaxLength(20);
						this.Property(aib => aib.BiddingMobileNumber).HasMaxLength(20);

						this.HasRequired(aib => aib.AuctionItem).WithMany(ai => ai.Biddings);
				}
		}
}
