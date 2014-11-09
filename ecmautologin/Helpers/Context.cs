using Sitecore.Analytics.Data.DataAccess.DataSets;
using Sitecore.Analytics.Data.Items;
using Sitecore.Data;
using Sitecore.Modules.EmailCampaign;
using Sitecore.Modules.EmailCampaign.Core;
using Sitecore.Modules.EmailCampaign.Core.Analytics;
using Sitecore.Modules.EmailCampaign.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Galiana.EcmAutoLogin
{
    public static class Context
    {
        public static VisitorDataSet.AutomationStatesRow AutomationState
        {
            get
            {
                string autstate = Sitecore.Context.Request.QueryString[GlobalSettings.AutomationStateQueryKey];
                if (!string.IsNullOrEmpty(autstate) && ShortID.IsShortID(autstate))
                {
                    return AnalyticsHelper.GetAutomationState(new Guid(autstate));
                }
                else
                    return null;
            }
        }

        public static CampaignItem Campaign
        {
            get
            {
                string camp = Sitecore.Context.Request.QueryString[GlobalSettings.CampaignQueryStringKey];
                if (!string.IsNullOrEmpty(camp) && ShortID.IsShortID(camp))
                {
                    return (CampaignItem)ItemUtilExt.GetContentDbItem(new ID(camp));
                }
                else

                    return null;
            }
        }
        
        public static MessageItem Email
        {
            get
            {                
                if (Context.Campaign!= null)
                {
                    return Sitecore.Modules.EmailCampaign.Factory.GetMessage(GetMessageId(Context.Campaign.Data));;
                }
                else

                    return null;
            }
        }
        private static string GetMessageId(string campaignData)
        {
            string str;
            if (campaignData.Length != 76)
            {
                str = (campaignData.Length != 38 ? string.Empty : campaignData);
            }
            else
            {
                str = campaignData.Substring(38);
            }
            return str;
        }
    }
}