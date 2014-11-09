namespace Galiana.EcmAutoLogin.Controls
{
    using Galiana.EcmAutoLogin;
    using System;

    public partial class NotifyMessage : System.Web.UI.UserControl
    {

       

        public NotifyMessage()
            : base()
        {
            
        }
        private void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.ShowAutoLoginMessage)
            {
                if (!this.Page.ClientScript.IsClientScriptIncludeRegistered("jquery"))
                    this.Page.ClientScript.RegisterClientScriptInclude("jquery", "http://code.jquery.com/jquery-1.11.0.min.js");
                if (!this.Page.ClientScript.IsClientScriptIncludeRegistered("autologinNotify"))
                    this.Page.ClientScript.RegisterClientScriptInclude("autologinNotify", "/assets/notify.js");
                if (!this.Page.ClientScript.IsClientScriptIncludeRegistered("autologinNotifyStyle") && !String.IsNullOrWhiteSpace(SessionHelper.UserSettings.NotifyCustomization))
                    this.Page.ClientScript.RegisterClientScriptInclude("autologinNotifyStyle", SessionHelper.UserSettings.NotifyCustomization);                    

                
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "autologinNotifyMessage", "<script type=\"text/javascript\">$.notify(\"" + SessionHelper.UserSettings.PersonManager.ModifyText(SessionHelper.UserSettings.NotificationMessage) + "\",\"success\");</script>");
                SessionHelper.ShowAutoLoginMessage = false;
                SessionHelper.ClearCache();
            }
        }
    }
}