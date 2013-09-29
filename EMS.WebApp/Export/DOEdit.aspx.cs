using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.BLL;
using EMS.Common;
using EMS.Utilities;

namespace EMS.WebApp.Export
{
    public partial class DOEdit : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private Int64 _doId = 0;
        private IFormatProvider _culture = new CultureInfo(ConfigurationManager.AppSettings["Culture"].ToString());

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();
            SetAttributes();

            if (!IsPostBack)
            {

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Export/DOList.aspx");
        }

        protected void gvwList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        #endregion

        #region Private Methods

        private void SetAttributes()
        {

        }

        private void RetriveParameters()
        {
            if (!ReferenceEquals(Request.QueryString["id"], null))
            {
                Int64.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _doId);
            }

            _userId = EMS.BLL.UserBLL.GetLoggedInUserId();
            UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
        }

        private void CheckUserAccess()
        {
            if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                if (ReferenceEquals(user, null) || user.Id == 0)
                {
                    Response.Redirect("~/Login.aspx");
                }

                if (user.UserRole.Id != (int)UserRole.Admin)
                {
                    if (!_canView)
                    {
                        Response.Redirect("~/Unauthorized.aspx");
                    }

                    if (!_canEdit)
                    {
                        btnSave.Visible = false;
                    }
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!_canView)
            {
                Response.Redirect("~/Unauthorized.aspx");
            }
        }

        #endregion
    }
}