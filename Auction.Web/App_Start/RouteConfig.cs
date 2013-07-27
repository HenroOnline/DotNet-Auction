using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Auction.Web
{
		public class RouteConfig
		{
				public static void RegisterRoutes(RouteCollection routes)
				{
						routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

						routes.MapRoute(
								name: "FileAttachment",
								url: "FileAttachment/{id}/{maxheight}/{maxwidth}",
								defaults: new { controller = "FileAttachment", action = "Index", maxheight = UrlParameter.Optional, maxwidth = UrlParameter.Optional }
						);

						routes.MapRoute(
								name: "Default",
								url: "{controller}/{action}/{id}",
								defaults: new { controller = "Auction", action = "Index", id = UrlParameter.Optional }
						);
				}
		}
}