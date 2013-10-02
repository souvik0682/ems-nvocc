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
using EMS.Entity;
using EMS.Utilities;
using EMS.WebApp.CustomControls;

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
            List<IDeliveryOrderContainer> lstContainer = new List<IDeliveryOrderContainer>();
            IDeliveryOrder deliveryOrder = new DeliveryOrderEntity();
            BuildDeliveryOrderEntity(deliveryOrder);
            BuildContainerEntity(lstContainer);
            DoSave(deliveryOrder, lstContainer);
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
                ((Label)e.Row.Cells[0].FindControl("lblSlNo")).Text = ((gvwList.PageSize * gvwList.PageIndex) + e.Row.RowIndex + 1).ToString();
                ((HiddenField)e.Row.Cells[0].FindControl("hdnBookingCntrId")).Value = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BookingContainerId"));
                ((HiddenField)e.Row.Cells[0].FindControl("hdnTypeId")).Value = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ContainerTypeId"));
                ((HiddenField)e.Row.Cells[0].FindControl("hdnSize")).Value = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ContainerSize"));
                ((HiddenField)e.Row.Cells[0].FindControl("hdnAvlUnit")).Value = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "AvailableUnit"));
                ((HiddenField)e.Row.Cells[0].FindControl("hdnBookingUnit")).Value = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BookingUnit"));
                
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ContainerType"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ContainerSize"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "AvailableUnit"));
                ((CustomTextBox)e.Row.Cells[4].FindControl("txtReqUnit")).Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "RequiredUnit"));
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

        private void BuildDeliveryOrderEntity(IDeliveryOrder deliveryOrder)
        {
            deliveryOrder.EmptyYardId = Convert.ToInt32(ddlYard.SelectedValue);
            deliveryOrder.BookingId = Convert.ToInt64(ddlBooking.SelectedValue);
            deliveryOrder.DeliveryOrderDate = Convert.ToDateTime(txtDoDate.Text.Trim(), _culture);
        }

        private void BuildContainerEntity(List<IDeliveryOrderContainer> lstContainer)
        {
            for (int index = 0; index < gvwList.Rows.Count; index++)
            {
                HiddenField hdnBookingCntrId = (HiddenField)gvwList.Rows[index].Cells[0].FindControl("hdnBookingCntrId");
                HiddenField hdnTypeId = (HiddenField)gvwList.Rows[index].Cells[0].FindControl("hdnTypeId");
                HiddenField hdnSize = (HiddenField)gvwList.Rows[index].Cells[0].FindControl("hdnSize");
                HiddenField hdnAvlUnit = (HiddenField)gvwList.Rows[index].Cells[0].FindControl("hdnAvlUnit");
                HiddenField hdnBookingUnit = (HiddenField)gvwList.Rows[index].Cells[0].FindControl("hdnBookingUnit");
                CustomTextBox txtReqUnit = (CustomTextBox)gvwList.Rows[index].Cells[4].FindControl("txtReqUnit");

                if (!ReferenceEquals(hdnTypeId, null) && !ReferenceEquals(hdnSize, null) && !ReferenceEquals(txtReqUnit, null))
                {
                    Int64 bookingCntrId = 0;
                    int containerTypeId = 0;
                    int availableUnit = 0;
                    int requiredUnit = 0;
                    int bookingUnit = 0;
                    Int64.TryParse(hdnBookingCntrId.Value.Trim(), out bookingCntrId);
                    int.TryParse(hdnTypeId.Value.Trim(), out containerTypeId);
                    int.TryParse(hdnAvlUnit.Value.Trim(), out availableUnit);
                    int.TryParse(hdnBookingUnit.Value.Trim(), out bookingUnit);
                    int.TryParse(txtReqUnit.Text.Trim(), out requiredUnit);

                    IDeliveryOrderContainer container = new DeliveryOrderContainerEntity();
                    container.BookingContainerId = bookingCntrId;
                    container.ContainerTypeId = containerTypeId;
                    container.ContainerSize = hdnSize.Value.Trim();
                    container.AvailableUnit = availableUnit;
                    container.BookingUnit = bookingUnit;
                    container.RequiredUnit = requiredUnit;

                    lstContainer.Add(container);
                }
            }
        }

        private void DoSave(IDeliveryOrder deliveryOrder, List<IDeliveryOrderContainer> lstContainer)
        {
            string message = string.Empty;

            if (DOBLL.ValidateContainer(lstContainer, out message))
            {
                message = DOBLL.SaveDeliveryOrder(deliveryOrder, lstContainer, _userId);
                GeneralFunctions.RegisterAlertScript(this, message);
            }
            else
            {
                GeneralFunctions.RegisterAlertScript(this, message);
            }
        }

        #endregion
    }
}