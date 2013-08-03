using Auction.DAL.Configuration;
using Auction.DAL.Entities;
using Auction.DAL.Initializer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL
{
		public class AuctionContext : DbContext
		{
				internal static string SchemaName
				{
						get;
						set;
				}

				public DbSet<AuctionItem> AuctionItems { get; set; }				

				private static AuctionContext _instance;
				public static AuctionContext Instance
				{
						get
						{
								if (System.Web.HttpContext.Current != null)
								{
										string key = typeof(AuctionContext).ToString();

										if (System.Web.HttpContext.Current.Items[key] == null)
										{
												System.Web.HttpContext.Current.Items[key] = CreateContext();
										}

										return (AuctionContext)System.Web.HttpContext.Current.Items[key];
								}
								else
								{
										if (_instance == null)
										{
												_instance = CreateContext();
										}
										return _instance;
								}
						}
				}

				protected static AuctionContext CreateContext()
				{
						var result = new AuctionContext();
						//Database.SetInitializer<AuctionContext>(new AuctionInitializer());
						Database.SetInitializer<AuctionContext>(null);
						return result;
				}

				public AuctionContext()
				{
						if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["SchemaName"]))
						{
								AuctionContext.SchemaName = ConfigurationManager.AppSettings["SchemaName"];
						}

						this.Configuration.LazyLoadingEnabled = false;
				}

				protected override void OnModelCreating(DbModelBuilder modelBuilder)
				{
						modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

						modelBuilder.Configurations.Add(new AuctionItemBiddingConfiguration());
						modelBuilder.Configurations.Add(new AuctionItemConfiguration());
						modelBuilder.Configurations.Add(new FileAttachmentConfiguration());
						modelBuilder.Configurations.Add(new FileAttachmentAuctionItemConfiguration());
				}

				public override int SaveChanges()
				{
						DateTime currentDate = DateTime.Now;
						var user = "Auction";
						foreach (var addedEntity in ChangeTracker.Entries<Base>().Where(b => b.State == System.Data.EntityState.Added))
						{
								addedEntity.Entity.CreatedDate = currentDate;
								addedEntity.Entity.CreatedUser = user;
								addedEntity.Entity.ModifiedKind = "I";

								addedEntity.Entity.ModifiedDate = currentDate;
								addedEntity.Entity.ModifiedUser = user;
						}

						foreach (var entity in ChangeTracker.Entries<Base>().Where(b => b.State == System.Data.EntityState.Modified))
						{
								entity.Entity.ModifiedDate = currentDate;
								entity.Entity.ModifiedUser = user;

								if (entity.Entity.ModifiedKind != "D")
								{
										entity.Entity.ModifiedKind = "M";
								}
						}

						try
						{
								return base.SaveChanges();
						}
						catch (DbEntityValidationException dbEx)
						{
								foreach (var validationErrors in dbEx.EntityValidationErrors)
								{
										foreach (var validationError in validationErrors.ValidationErrors)
										{
												Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
										}
								}

								throw;
						}
				}
		}
}
