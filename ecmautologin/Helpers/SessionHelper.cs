using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Galiana.Helpers
{
    public static class SessionHelper
    {
        public static bool AutoLoggedIn
        {
            get
            {
                if (HttpContext.Current.Cache[HttpContext.Current.User.Identity.Name + "AutoLoggedIn"] == null)                
                    return false;
                else
                    return (bool)HttpContext.Current.Cache[HttpContext.Current.User.Identity.Name + "AutoLoggedIn"];
            }
            set
            {
                //if (HttpContext.Current.Items["AutoLoggedIn"] == null)
                //    HttpContext.Current.Items.Add("AutoLoggedIn", value);
                //else
                    HttpContext.Current.Cache[HttpContext.Current.User.Identity.Name + "AutoLoggedIn"] = value;
            }
        }

        public static void ClearCache()
        {
            if (HttpContext.Current.Cache.Get(HttpContext.Current.User.Identity.Name + "AutoLoggedIn")!= null)
                HttpContext.Current.Cache.Remove(HttpContext.Current.User.Identity.Name + "AutoLoggedIn");
        }
    }
}