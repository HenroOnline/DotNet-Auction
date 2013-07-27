using Auction.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Configuration
{
		public class AuctionItemConfiguration : BaseConfiguration<AuctionItem>
		{
				public AuctionItemConfiguration()
				{
						if (!string.IsNullOrEmpty(AuctionContext.SchemaName))
						{
								this.ToTable("AuctionItem", AuctionContext.SchemaName);
						}

						this.Property(ai => ai.Title).HasMaxLength(150)
																				 .IsRequired();
						this.Property(ai => ai.Description).IsMaxLength()
																							 .IsRequired();

						this.Property(ai => ai.MinimumPrice).IsRequired();

						this.Property(ai => ai.VendorName).HasMaxLength(150)
																							.IsRequired();
						this.Property(ai => ai.VendorStreet).HasMaxLength(150)
																							  .IsRequired();
						this.Property(ai => ai.VendorHouseNumber).HasMaxLength(10)
																										 .IsRequired();
						this.Property(ai => ai.VendorZipCode).HasMaxLength(10)
																								 .IsRequired();
						this.Property(ai => ai.VendorCity).HasMaxLength(150)
																							.IsRequired();

						this.Property(ai => ai.VendorEmail).HasMaxLength(150);
						this.Property(ai => ai.VendorPhoneNumber).HasMaxLength(20);
						this.Property(ai => ai.VendorMobileNumber).HasMaxLength(20);
				}
		}
}
