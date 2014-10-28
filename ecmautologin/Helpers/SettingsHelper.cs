using Sitecore.Configuration;
using Sitecore.Modules.EmailCampaign;
using Sitecore.Modules.EmailCampaign.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Galiana.Extensions;
using Sitecore.Data.Items;
using Sitecore.Modules.EmailCampaign.Core;

namespace Galiana.Helpers
{
    public static class SettingsHelper
    {
        private static bool _verifyUserAgainstEmail = true;
        private static bool _verifyUserAgainstEmailInitialized = false;

        public static ManagerRoot GetManagerRoot()
        {
            return Context.Email != null ? Sitecore.Modules.EmailCampaign.Factory.GetManagerRootFromChildItem(Context.Email.InnerItem) : null;
        }
        public static Galiana.poco.Settings CurrentSettings
        {
            get
            {
                ManagerRoot root = GetManagerRoot();
                return root == null ? DefaultSettings : root.GetAutoLoginSettings();
            }
        }
        private static Galiana.poco.Settings _defaultSettings;
        public static Galiana.poco.Settings DefaultSettings
        {
            get
            {
                if (_defaultSettings != null)
                    return _defaultSettings;
                else
                {
                    _defaultSettings = new poco.Settings();
                    _defaultSettings.VerifyUserAgainsEmail = VerifyUserAgainstEmail;
                    _defaultSettings.PersistentLogin = PersistentLogin;
                    _defaultSettings.LinksExpiration = LinksExporation;
                    _defaultSettings.Sites = Sites;
                    _defaultSettings.NotificationMessage = "Welcome.";
                    return _defaultSettings;
                }
            }

        }




        private static List<string> _sites = null;

        private static List<string> Sites
        {
            get
            {
                if (_sites != null)
                    return _sites;
                else
                {
                    if (!String.IsNullOrWhiteSpace(SiteList))
                        _sites = SiteList.Split('|').ToList<string>();
                    else
                        _sites = new List<string>();
                    return _sites;
                }
            }
            set { _sites = value; }
        }

        private static string SiteList
        {
            get
            {
                return Settings.GetSetting("ECMAutoLogin.Sites", "");
            }
        }
        private static bool VerifyUserAgainstEmail
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

        private static bool PersistentLogin
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
        private static TimeSpan LinksExporation
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
                return (email.StartTime - DateTime.Now) > (CurrentSettings != null ? CurrentSettings.LinksExpiration : DefaultSettings.LinksExpiration);
            else
                return true;
        }

        public static void SetPersonalizationManager(Galiana.poco.Settings s, Contact c)
        {
            PersonalizationManager p = (Context.Email!= null && c != null ? new PersonalizationManager(c, GetManagerRoot()) : new PersonalizationManager());
            if (Context.Email.CustomPersonTokens != null)
            {
                foreach (string key in Context.Email.CustomPersonTokens.Keys)
                {
                    p.AddTokenValue(key, Context.Email.CustomPersonTokens[key].ToString());
                }
            }
            s.PersonManager = p;
        }

    }
}