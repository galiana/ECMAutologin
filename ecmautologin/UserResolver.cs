using Sitecore.Analytics.Data.DataAccess.DataSets;
using Sitecore.Analytics.Data.Items;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Modules.EmailCampaign;
using Sitecore.Modules.EmailCampaign.Core;
using Sitecore.Modules.EmailCampaign.Core.Analytics;
using Sitecore.Modules.EmailCampaign.Messages;
using Sitecore.Pipelines.HttpRequest;
using Sitecore.Security.Authentication;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Galiana
{
    public class UserResolver : HttpRequestProcessor
    {
        public UserResolver()
        {
        }

        /// <summary>
        /// Runs the processor.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public override void Process(HttpRequestArgs args)
        {
            Stopwatch timer = Stopwatch.StartNew();
            Assert.ArgumentNotNull(args, "args");
            if (args.Context.User == null)
            {
                string autstate = args.Context.Request.QueryString[GlobalSettings.AutomationStateQueryKey];
                if (!string.IsNullOrEmpty(autstate) && ShortID.IsShortID(autstate))
                {
                    string camp = args.Context.Request.QueryString[GlobalSettings.CampaignQueryStringKey];
                    if (!string.IsNullOrEmpty(camp) && ShortID.IsShortID(camp))
                    {
                        VisitorDataSet.AutomationStatesRow automationState = AnalyticsHelper.GetAutomationState(new Guid(autstate));
                        if (automationState != null)
                        {
                            Contact contact = Sitecore.Modules.EmailCampaign.Factory.GetContactFromName(automationState.UserName);
                            if (contact != null)
                            {
                                if (!contact.IsAnonymousSubscriber && !contact.IsDisabled && Sitecore.Context.Site.Domain == contact.InnerUser.Domain)
                                {
                                    bool VeifyUserAgainstEmail;
                                    Boolean.TryParse(Settings.GetSetting("ECMAutoLogin.VeifyUserAgainstEmail", "true"), out VeifyUserAgainstEmail);
                                    if (VeifyUserAgainstEmail)
                                    {
                                        CampaignItem contentDbItem = (CampaignItem)ItemUtilExt.GetContentDbItem(new ID(camp));

                                        if (contentDbItem != null)
                                        {
                                            MessageItem message = Sitecore.Modules.EmailCampaign.Factory.GetMessage(GetMessageId(contentDbItem.Data));

                                            if (message.Subscribers.Contains(contact))
                                            {
                                                AuthenticationManager.Login(contact.InnerUser);
                                                if (GlobalSettings.Debug)
                                                    Log.Debug("AutoLogin user  " + contact.Name + " from email: " + message.Subject);
                                            }
                                        }
                                        else
                                        {
                                            AuthenticationManager.Login(contact.InnerUser);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            TimeSpan elapsed = timer.Elapsed;
            timer.Stop();
            if (GlobalSettings.Debug)
                Log.Debug("AutoLogin took " + elapsed.TotalMilliseconds.ToString() + " miliseconds");
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