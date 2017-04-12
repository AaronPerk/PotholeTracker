using Capstone.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Capstone.Web.Filter
{
    public class PotholeAuthorizationFilter : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            // Check to see if we have a username in the url
            if (filterContext.ActionParameters.ContainsKey("user"))
            {

                //gets the username from the url
                var impliedUserId = (int)filterContext.ActionParameters["userId"];
                var controller = (PotholeController)filterContext.Controller;
                var actualUserId = controller.CurrentUser.UserId;

                // If the user is not logged in, then take them to the login page
                if (!controller.IsAuthenticated)
                {
                    // then redirect to login page
                    var routeValue = new RouteValueDictionary(new
                    {
                        controller = "Users",
                        action = "Login",
                        landingPage = filterContext.HttpContext.Request.Url
                    });
                    filterContext.Result = new RedirectToRouteResult(routeValue);
                }
                else
                {
                    if (impliedUserId != actualUserId) //They're liars
                    {
                        filterContext.Result = new HttpStatusCodeResult(403);
                    }
                }

            }

            base.OnActionExecuting(filterContext);
        }

    }
}