﻿using Auction.BLL;
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

				[HttpPost]
				public ActionResult Detail(AuctionItemDetailModel auctionItem)
				{
						var dalAuctionItem = AuctionItemRepository.Instance.Select(auctionItem.Id);

						if (auctionItem.NewBidding != null)
						{
								if (string.IsNullOrEmpty(auctionItem.NewBidding.BiddingPhoneNumber) && string.IsNullOrEmpty(auctionItem.NewBidding.BiddingMobileNumber))
								{
										ModelState.AddModelError("NewBidding.BiddingPhoneNumber", "Een telefoonnummer is verplicht");
										ModelState.AddModelError("NewBidding.BiddingMobileNumber", string.Empty);
								}

								if (ModelState.IsValidField("NewBidding.Bidding"))
								{
										// Check if minimum bid is reached
										if (auctionItem.NewBidding.Bidding < dalAuctionItem.MinimumPrice)
										{
												ModelState.AddModelError("NewBidding.Bidding", string.Format("Bieding moet minimaal € {0} zijn", dalAuctionItem.MinimumPrice));
										}
										else
										{
												// Minimum bid is reached.. Now check if higher than highest bid so far
												var highestBid = AuctionItemBiddingRepository.Instance.SelectHighest(auctionItem.Id);
												decimal highestBidPrice = 0;
												if (highestBid != null)
												{
														highestBidPrice = highestBid.Bidding;
												}
												if (auctionItem.NewBidding.Bidding <= highestBidPrice)
												{
														ModelState.AddModelError("NewBidding.Bidding", string.Format("Bieding moet hoger zijn dan € {0}", highestBidPrice));
												}												
										}

								}

								if (ModelState.IsValid)
								{
										var dalEntity = auctionItem.NewBidding.ToDalEntity(auctionItem.Id);
										AuctionItemBiddingRepository.Instance.Save(dalEntity);

										return RedirectToAction("Detail", new { id = auctionItem.Id });
								}
						}

						return View(new AuctionItemDetailModel(dalAuctionItem));
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

				public void SendAuctionEndedMails()
				{
						MailHelper.SendAuctionEndedMails();
				}
		}
}
