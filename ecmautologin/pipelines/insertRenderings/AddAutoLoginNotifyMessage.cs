using Galiana.EcmAutoLogin.Helpers;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Layouts;
using Sitecore.Pipelines.InsertRenderings;
using Sitecore.Web.UI.HtmlControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Galiana.EcmAutoLogin.pipelines.insertRenderings
{
    public class AddAutoLoginNotifyMessage : InsertRenderingsProcessor
    {
        public AddAutoLoginNotifyMessage() { }
        /// <summary>Processes the specified args.</summary>
        /// <param name="args">The argumentss.</param>
        public override void Process(InsertRenderingsArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            if (!SessionHelper.ShowAutoLoginMessage)
                return;
            if (args.ContextItem == null)
            {
                return;
            }
            DeviceItem device = Sitecore.Context.Device;
            if (device == null)
            {
                return;
            }            
            System.Web.UI.Page page = new System.Web.UI.Page();
            UserControl controlToLoad = page.LoadControl("~"+SessionHelper.UserSettings.NotificationControl.FilePath) as UserControl;
            RenderingReference rr = new RenderingReference(controlToLoad);
            rr.Placeholder = SessionHelper.UserSettings.NotificationPlaceHolder;
            args.Renderings.Add(rr);
           
            args.HasRenderings = args.Renderings.Count > 0;            
        }
    }
}