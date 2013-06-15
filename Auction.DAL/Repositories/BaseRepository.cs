using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Repositories
{
		public class BaseRepository<T, R>
				where T : Entities.Base
				where R : BaseRepository<T, R>, new()
		{
				public AuctionContext Context
				{
						get
						{
								return AuctionContext.Instance;
						}
				}

				private static R _instance;
				public static R Instance
				{
						get
						{
								if (_instance == null)
								{
										_instance = new R();
								}
								return _instance;
						}
				}

				public BaseRepository()
				{

				}

				public T Select(int id)
				{
						return this.Context.Set<T>().FirstOrDefault(t => t.Id == id
																													&& t.ModifiedKind != "D");
				}

				public List<T> List()
				{
						return this.Context.Set<T>().Where(t => t.ModifiedKind != "D")
																				.OrderBy(t => t.Id)
																				.ToList();
				}

				public void Save(T entity)
				{
						if (entity.Id == 0)
						{
								Context.Set<T>().Add(entity);
						}

						//Context.SaveChanges();
				}

				public virtual void Delete(T entity)
				{
						if (entity.ModifiedKind != "D")
						{
								entity.ModifiedKind = "D";
						}
				}

				public void PersistChanges()
				{
						Context.SaveChanges();
				}
		}

}
