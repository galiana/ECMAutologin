using Galiana.Helpers;
using Sitecore.Data.Items;
using Sitecore.Modules.EmailCampaign;
using Sitecore.Modules.EmailCampaign.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Galiana.poco
{
    public class Settings:ICloneable
    {
        public Settings()
        { }
        private bool _verifyUserAgainsEmail;

        public bool VerifyUserAgainsEmail
        {
            get { return _verifyUserAgainsEmail; }
            set { _verifyUserAgainsEmail = value; }
        }
        private bool _persistentLogin;

        public bool PersistentLogin
        {
            get { return _persistentLogin; }
            set { _persistentLogin = value; }
        }
        private TimeSpan _linksExpiration;

        public TimeSpan LinksExpiration
        {
            get { return _linksExpiration; }
            set { _linksExpiration = value; }
        }
        private string _notificationMessage;

        public string NotificationMessage
        {
            get { return _notificationMessage; }
            set { _notificationMessage = value; }
        }
        private string _notifyCustomization;

        public string NotifyCustomization
        {
            get { return _notifyCustomization; }
            set { _notifyCustomization = value; }
        }
        private List<string> _sites;

        public List<string> Sites
        {
            get { return _sites; }
            set { _sites = value; }
        }
        private SublayoutItem notificationControl;

        public SublayoutItem NotificationControl
        {
            get { return notificationControl; }
            set { notificationControl = value; }
        }

        

        private PersonalizationManager personManager;
        public virtual PersonalizationManager PersonManager
        {
            get
            {               
                return this.personManager;
            }
            set
            {
                this.personManager = value;
            }
        }

        public Settings(Item settings)
        {
            if (settings != null)
            {
                if (settings.Fields[ConstantsHelper.AutoLoginOptions.VerifyUserAgainstEmail] != null)
                {
                    Sitecore.Data.Fields.CheckboxField very = (Sitecore.Data.Fields.CheckboxField)settings.Fields[ConstantsHelper.AutoLoginOptions.VerifyUserAgainstEmail];
                    this.VerifyUserAgainsEmail = very.Checked;
                }

                if (settings.Fields[ConstantsHelper.AutoLoginOptions.PersistentLogin] != null)
                {
                    Sitecore.Data.Fields.CheckboxField very = (Sitecore.Data.Fields.CheckboxField)settings.Fields[ConstantsHelper.AutoLoginOptions.PersistentLogin];
                    this.PersistentLogin = very.Checked;
                }
                if (settings.Fields[ConstantsHelper.AutoLoginOptions.LinksExpiration] != null)
                {
                    Sitecore.Data.Fields.TextField very = (Sitecore.Data.Fields.TextField)settings.Fields[ConstantsHelper.AutoLoginOptions.LinksExpiration];
                    TimeSpan tmp;
                    if (!TimeSpan.TryParse(very.Value, out tmp))
                        this.LinksExpiration = new TimeSpan(0);
                    else
                    {
                        this.LinksExpiration = tmp;
                    }
                }
                if (settings.Fields[ConstantsHelper.AutoLoginOptions.NotificationMessage] != null)
                {
                    Sitecore.Data.Fields.TextField very = (Sitecore.Data.Fields.TextField)settings.Fields[ConstantsHelper.AutoLoginOptions.NotificationMessage];
                    this.NotificationMessage = very.Value;
                }
                if (settings.Fields[ConstantsHelper.AutoLoginOptions.NotifyCustomization] != null)
                {
                    Sitecore.Data.Fields.TextField very = (Sitecore.Data.Fields.TextField)settings.Fields[ConstantsHelper.AutoLoginOptions.NotifyCustomization];
                    this.NotifyCustomization = very.Value;
                }

                Sites = SettingsHelper.DefaultSettings.Sites;
            }                  

        }

        public object Clone()
        {
            Settings s = new Settings();
            s.LinksExpiration = this.LinksExpiration;
            s.NotificationMessage = this.NotificationMessage;
            s.NotifyCustomization = this.NotifyCustomization;
            s.PersistentLogin = this.PersistentLogin;
            s.Sites = this.Sites;
            s.VerifyUserAgainsEmail = this.VerifyUserAgainsEmail;
            s.NotificationControl = this.NotificationControl;
            return s;
        }
    }
}