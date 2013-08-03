using Auction.BLL;
using Auction.BLL.Repositories;
using Auction.DAL.Entities;
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

				public ActionResult Add()
				{
						return View(new AuctionItemAddModel());
				}

				[HttpPost]
				public ActionResult Add(AuctionItemAddModel auctionItem)
				{
						if (string.IsNullOrEmpty(auctionItem.VendorPhoneNumber) && string.IsNullOrEmpty(auctionItem.VendorMobileNumber))
						{
								ModelState.AddModelError("VendorPhoneNumber", "Een telefoonnummer is verplicht");
								ModelState.AddModelError("VendorMobileNumber", string.Empty);
						}

						if (ModelState.IsValid)
						{
								// Save and redirect to overview
								var dalEntity = auctionItem.ToDalEntity();
								AuctionItemRepository.Instance.Save(dalEntity);

								for (int i = 0; i < Request.Files.Count; i++)
								{
										var file = Request.Files[i];
										if (file.InputStream.Length == 0)
										{
												continue;
										}										

										var fileAttachmentId = FileHelper.SaveFileAttachment(0, file.InputStream, file.FileName, file.ContentType);
										FileAttachmentAuctionItemRepository.Instance.Create(dalEntity.Id, fileAttachmentId, (i + 1) * 10);
								}

								return RedirectToAction("Index");
						}

						return View();
				}
    }
}
