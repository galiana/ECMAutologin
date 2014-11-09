using Galiana.EcmAutoLogin.Helpers;
using Galiana.poco;
using Sitecore.Data.Items;
using Sitecore.Modules.EmailCampaign;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Galiana.EcmAutoLogin.Extensions
{
    public static class AutoLogin
    {
        private static Hashtable SettingsList
        {
            get
            {
                return null;
            }
        }
        public static Settings GetAutoLoginSettings(this ManagerRoot root)
        {
            if (root == null)
                return SettingsHelper.DefaultSettings;
            else
            {
                Item item = root.InnerItem.Axes.SelectSingleItem("descendant-or-self::*[@@TemplateID='{8EF19A2A-ABF9-4D25-B72F-3B7D83239E3E}']");
                if (item != null)
                {
                    Settings s = new Settings(item);
                    if (item.Fields[ConstantsHelper.AutoLoginOptions.NotificationControl] != null)
                    {
                        Sitecore.Data.Fields.TextField very = (Sitecore.Data.Fields.TextField)item.Fields[ConstantsHelper.AutoLoginOptions.NotificationControl];
                        if (!string.IsNullOrWhiteSpace(very.Value))
                        {
                            Item sl = item.Database.GetItem(very.Value);
                            if (sl != null)
                            {
                                SublayoutItem sublayout = new SublayoutItem(sl);
                                s.NotificationControl = sublayout;
                            }
                        }
                    }
                    return s;
                }
                else
                    return SettingsHelper.DefaultSettings;
            }           
        }
    }
}