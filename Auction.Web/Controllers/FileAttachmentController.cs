using Auction.BLL;
using Auction.BLL.Repositories;
using Auction.DAL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auction.Web.Controllers
{
		public class FileAttachmentController : Controller
		{
				public ActionResult Index(int id, int? maxheight, int? maxwidth)
				{
						FileAttachment attachment = null;

						if (id != 0)
						{
								attachment = FileAttachmentRepository.Instance.Select(id);
						}

						if (id != 0 && attachment == null)
						{
								return null;
						}

						int heightToUse = 0;
						int widthToUse = 0;
						if (maxheight.HasValue && maxwidth.HasValue)
						{
								heightToUse = maxheight.Value;
								widthToUse = maxwidth.Value;
						}

						if (attachment != null)
						{
								return File(FileHelper.SelectDataFromFileAttachment(attachment.Id, new System.Drawing.Size(widthToUse, heightToUse)), attachment.ContentType);
						}
						else
						{
								return File(FileHelper.ReadFromDisk("NoImageAvailable", "jpg", Server.MapPath("~/Content/img"), new System.Drawing.Size(widthToUse, heightToUse)), "image/jpg");
						}
				}
		}
}
