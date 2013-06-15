using Auction.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Configuration
{
		public class BaseConfiguration<T> : EntityTypeConfiguration<T>
			where T : Base
		{
				public BaseConfiguration()
				{
						Property(p => p.CreatedUser).HasMaxLength(50)
																				.IsRequired();
						Property(p => p.ModifiedUser).HasMaxLength(50)
																				 .IsRequired();
						Property(p => p.ModifiedKind).HasMaxLength(1)
																				 .IsFixedLength()
																				 .IsRequired();
				}
		}
}
