using Galiana.EcmAutoLogin;
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

namespace Galiana.EcmAutoLogin.Pipelines.httpRequestBegin
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
            if (SettingsHelper.DefaultSettings.Sites.Contains(Sitecore.Context.Site.Name))
            {
                if (args.Context.User == null)
                {
                    if (Context.AutomationState != null)
                    {
                        Contact contact = Sitecore.Modules.EmailCampaign.Factory.GetContactFromName(Context.AutomationState.UserName);
                        if (contact != null)
                        {
                            if (!contact.IsAnonymousSubscriber && !contact.IsDisabled && Sitecore.Context.Site.Domain == contact.InnerUser.Domain)
                            {
                                if (SettingsHelper.CurrentSettings.VerifyUserAgainsEmail)
                                {
                                    if (Context.Campaign != null)
                                    {
                                        if (Context.Email != null)
                                        {
                                            if (!SettingsHelper.IsLinkExpired(Context.Email))
                                            {
                                                if (Context.Email.Subscribers.Contains(contact))
                                                {
                                                    AuthenticationManager.Login(contact.InnerUser.Name, SettingsHelper.CurrentSettings.PersistentLogin);                                                    
                                                    SessionHelper.ShowAutoLoginMessage = true;
                                                    SessionHelper.UserSettings = (Galiana.poco.Settings)SettingsHelper.CurrentSettings.Clone();
                                                    SettingsHelper.SetPersonalizationManager(SessionHelper.UserSettings, contact);
                                                    if (GlobalSettings.Debug)
                                                        Log.Debug("AutoLogin user  " + contact.Name + " from email: " + Context.Email.Subject);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        AuthenticationManager.Login(contact.InnerUser.Name, SettingsHelper.CurrentSettings.PersistentLogin);
                                        SessionHelper.ShowAutoLoginMessage = true;
                                        SessionHelper.UserSettings = (Galiana.poco.Settings)SettingsHelper.CurrentSettings.Clone();
                                        SettingsHelper.SetPersonalizationManager(SessionHelper.UserSettings, contact);
                                        if (GlobalSettings.Debug)
                                            Log.Debug("AutoLogin user  " + contact.Name);
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
        
    }
}