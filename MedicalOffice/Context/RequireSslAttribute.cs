using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CherryCitySoftware.MedicalOffice.Context
{
    public class RequireSslAttribute : RequireHttpsAttribute
    {
        protected override void HandleNonHttpsRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.RequestContext.HttpContext.Request.IsLocal)
            {
                base.HandleNonHttpsRequest(filterContext);
            }
        }
    }
}