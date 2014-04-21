using Galiana.Controls;
using Galiana.Helpers;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Layouts;
using Sitecore.Pipelines.InsertRenderings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Galiana.pipelines.insertRenderings
{
    public class AddAutoLoginNotifyMessage : InsertRenderingsProcessor
    {
        public AddAutoLoginNotifyMessage() { }
        /// <summary>Processes the specified args.</summary>
        /// <param name="args">The argumentss.</param>
        public override void Process(InsertRenderingsArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            if (!SessionHelper.AutoLoggedIn)
                return;
            if (args.ContextItem == null)
            {
                return;
            }
            DeviceItem device = Context.Device;
            if (device == null)
            {
                return;
            }
            NotifyMessage nm = new NotifyMessage("Hooola");
            RenderingReference rr = new RenderingReference(nm);
            rr.Placeholder = "content";
            args.Renderings.Add(rr);
           
            args.HasRenderings = args.Renderings.Count > 0;
        }
    }
}