using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Galiana.EcmAutoLogin
{
    public static class SessionHelper
    {

        public static Galiana.poco.Settings UserSettings
        {

            get
            {
                if (HttpContext.Current.Cache[HttpContext.Current.User.Identity.Name + "AutoLoginUSerSettings"] == null)
                    return SettingsHelper.CurrentSettings;
                else
                    return (Galiana.poco.Settings)HttpContext.Current.Cache[HttpContext.Current.User.Identity.Name + "AutoLoginUSerSettings"];
            }
            set
            {
                HttpContext.Current.Cache[HttpContext.Current.User.Identity.Name + "AutoLoginUSerSettings"] = value;
            }

        }

        public static bool ShowAutoLoginMessage
        {
            get
            {
                if (HttpContext.Current.Cache[HttpContext.Current.User.Identity.Name + "ShowAutoLoginMessage"] == null)
                    return false;
                else
                    return (bool)HttpContext.Current.Cache[HttpContext.Current.User.Identity.Name + "ShowAutoLoginMessage"];
            }
            set
            {
                //if (HttpContext.Current.Items["AutoLoggedIn"] == null)
                //    HttpContext.Current.Items.Add("AutoLoggedIn", value);
                //else
                HttpContext.Current.Cache[HttpContext.Current.User.Identity.Name + "ShowAutoLoginMessage"] = value;
            }
        }

        public static void ClearCache()
        {
            if (HttpContext.Current.Cache.Get(HttpContext.Current.User.Identity.Name + "ShowAutoLoginMessage") != null)
                HttpContext.Current.Cache.Remove(HttpContext.Current.User.Identity.Name + "ShowAutoLoginMessage");
            if (HttpContext.Current.Cache.Get(HttpContext.Current.User.Identity.Name + "AutoLoginUSerSettings") != null)
                HttpContext.Current.Cache.Remove(HttpContext.Current.User.Identity.Name + "AutoLoginUSerSettings");
        }
    }
}