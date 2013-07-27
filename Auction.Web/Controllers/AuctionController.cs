using Auction.BLL.Repositories;
using Auction.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auction.Web.Controllers
{
    public class AuctionController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {						
						var data = AuctionItemRepository.Instance.ListOrderedByDate();
						
            return View(data.Select(d => new AuctionItemOverviewModel(d)).ToList());
        }

				public ActionResult Detail(int id)
				{
						var data = AuctionItemRepository.Instance.Select(id);

						return View(new AuctionItemDetailModel(data));
				}
    }
}
