using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
                PopulateControls();
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

        protected void ddlBooking_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadContainerList();
        }

        protected void ddlYard_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadContainerList();
        }

        protected void gvwList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 5);
                e.Row.Cells[0].Text = ((gvwList.PageSize * gvwList.PageIndex) + e.Row.RowIndex + 1).ToString();
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ContainerType"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ContainerSize"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "AvailableUnit"));
                ((TextBox)e.Row.Cells[4].FindControl("txtReqUnit")).Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "RequiredUnit"));
            }
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

        private void PopulateControls()
        {
            PopulateBookingNo();
            PopulateEmptyYard();
        }

        private void PopulateBookingNo()
        {
            DataTable dt = DOBLL.GetBookingList(_userId);

            if (!ReferenceEquals(dt, null))
            {
                ddlBooking.DataValueField = "BookingId";
                ddlBooking.DataTextField = "BookingNo";
                ddlBooking.DataSource = dt;
                ddlBooking.DataBind();
                ddlBooking.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
            }
        }

        private void PopulateEmptyYard()
        {
            DataTable dt = DOBLL.GetEmptyYard(_userId);

            if (!ReferenceEquals(dt, null))
            {
                ddlYard.DataValueField = "AddrId";
                ddlYard.DataTextField = "AddrName";
                ddlYard.DataSource = dt;
                ddlYard.DataBind();
                ddlYard.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
            }
        }

        private void LoadContainerList()
        {
            Int64 bookingId = Convert.ToInt64(ddlBooking.SelectedValue);
            Int32 emptyYardId = Convert.ToInt32(ddlYard.SelectedValue);

            List<IDeliveryOrderContainer> lstCntr = DOBLL.GetDeliveryOrderContriner(bookingId, emptyYardId);

            gvwList.DataSource = lstCntr;
            gvwList.DataBind();
        }

        #endregion
    }
}