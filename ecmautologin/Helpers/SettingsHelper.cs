using Sitecore.Configuration;
using Sitecore.Modules.EmailCampaign.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Galiana.Helpers
{
    public static class SettingsHelper
    {
        private static bool _verifyUserAgainstEmail = true;
        private static bool _verifyUserAgainstEmailInitialized = false;

        public static bool VeifyUserAgainstEmail
        {
            get
            {
                if (!_verifyUserAgainstEmailInitialized)
                {
                    if(Boolean.TryParse(Settings.GetSetting("ECMAutoLogin.VerifyUserAgainstEmail", "true"), out _verifyUserAgainstEmail))
                        _verifyUserAgainstEmailInitialized = true;
                }
                return _verifyUserAgainstEmail; 
            }          
        }

        private static bool _persistentLogin = true;
        private static bool _persistentLoginInitialized = false;

        public static bool PersistentLogin
        {
            get
            {
                if (!_persistentLoginInitialized)
                {
                    if (Boolean.TryParse(Settings.GetSetting("ECMAutoLogin.PersistentLogin", "true"), out _persistentLogin))
                        _persistentLoginInitialized = true;
                }
                return _persistentLogin;
            }
        }

        private static TimeSpan _linksExporation;
        public static TimeSpan LinksExporation
        {
            get
            {
                if (_linksExporation == null)
                    if (!TimeSpan.TryParse(Settings.GetSetting("ECMAutoLogin.LinksExpiration"), out _linksExporation))
                        return new TimeSpan(0);
                return SettingsHelper._linksExporation;
            }
        }

        public static bool IsLinkExpired(MessageItem email)
        {
            if (email != null)
                return (email.StartTime - DateTime.Now) > LinksExporation;
            else
                return true;
        }

    }
}