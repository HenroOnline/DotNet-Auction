﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auction.Web.Controllers
{
    public class ContentController : Controller
    {
        //
        // GET: /Content/

        public ActionResult FAQ()
        {
            return View();
        }

    }
}
