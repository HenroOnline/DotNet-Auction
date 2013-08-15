using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Auction.Web
{
		// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
		// visit http://go.microsoft.com/?LinkId=9394801
		public class MvcApplication : System.Web.HttpApplication
		{
				protected void Application_Start()
				{
						// https://github.com/SignalR/SignalR/issues/1406
						// Fix for IOS
						// On safari we must use a sort of longpolling.
						// If we don't do this we cannot load aditional resources (like images) because connection is in use
						GlobalHost.Configuration.ConnectionTimeout = TimeSpan.FromSeconds(1);
						Microsoft.AspNet.SignalR.Transports.LongPollingTransport.LongPollDelay = 5000;
						RouteTable.Routes.MapHubs();

						AreaRegistration.RegisterAllAreas();

						WebApiConfig.Register(GlobalConfiguration.Configuration);
						FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
						RouteConfig.RegisterRoutes(RouteTable.Routes);
				}
		}
}