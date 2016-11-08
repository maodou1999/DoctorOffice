using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CherryCitySoftware.MedicalOffice.Context
{

    public class NoSslAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            
            var request = filterContext.HttpContext.Request;
            if (request.Url != null && request.IsSecureConnection)
                filterContext.Result = new RedirectResult("http://" + request.Url.Host + request.RawUrl);
        }
    }


}