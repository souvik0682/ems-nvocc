using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities;
using EMS.Common;

namespace EMS.WebApp
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //Clears the application cache.
            GeneralFunctions.ClearApplicationCache();

            if (!Request.Path.Contains("ChangePassword.aspx"))
            {
                if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
                {
                    IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                    if (ReferenceEquals(user, null) || user.Id == 0)
                    {
                        Response.Redirect("~/Login.aspx");
                    }

                    SetAttributes(user);
                    ShowMenu(user);
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
            else
            {
                if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
                {
                    IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                    if (!ReferenceEquals(user, null) && user.Id > 0)
                    {
                        SetAttributes(user);
                        ShowMenu(user);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.DataBind();
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }

        protected void lnkPwd_Click(object sender, EventArgs e)
        {

        }

        private void SetAttributes(IUser user)
        {
            lblUserName.Text = "Welcome " + user.UserFullName;
        }

        private void ShowMenu(IUser user)
        {

        }
    }
}