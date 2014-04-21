namespace Galiana.Controls
{
    using Galiana.Helpers;
    using System;

    public partial class NotifyMessage : System.Web.UI.UserControl
    {
        public string Message;
        public NotifyMessage(string message):base()
        {
            this.Message = message;
        }
        private void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.AutoLoggedIn)
            {
                if (!this.Page.ClientScript.IsClientScriptIncludeRegistered("autologinNotify"))
                {
                    this.Page.ClientScript.RegisterClientScriptInclude("jquery", "http://code.jquery.com/jquery-1.11.0.min.js");
                    this.Page.ClientScript.RegisterClientScriptInclude("autologinNotify", "/assets/notify.min.js");
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "autologinNotifyMessage", "<script type=\"text/javascript\">$.notify('" + this.Message + "');</script>");

                }
                SessionHelper.AutoLoggedIn = false;
            }
        }
    }
}