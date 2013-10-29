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
using EMS.Entity.Report;
using EMS.Utilities;
using EMS.Utilities.ReportManager;
using EMS.WebApp.CustomControls;
using Microsoft.Reporting.WebForms;

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
        private bool _isEditable = true;
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
                //LoadContainerList();
            }
        }        

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                List<DeliveryOrderContainerEntity> lstContainer = new List<DeliveryOrderContainerEntity>();
                IDeliveryOrder deliveryOrder = new DeliveryOrderEntity();
                BuildDeliveryOrderEntity(deliveryOrder);
                BuildContainerEntity(lstContainer);
                DoSave(deliveryOrder, lstContainer);
                LoadContainerList();
                _isEditable = false;
                LockControls(true);
                Response.Redirect("~/Export/DOList.aspx");
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                List<DeliveryOrderContainerEntity> lstContainer = new List<DeliveryOrderContainerEntity>();
                IDeliveryOrder deliveryOrder = new DeliveryOrderEntity();
                BuildDeliveryOrderEntity(deliveryOrder);
                BuildContainerEntity(lstContainer);
                DoSave(deliveryOrder, lstContainer);
                LoadContainerList();
                _isEditable = false;
                LockControls(true);
                GenerateReport(deliveryOrder.DeliveryOrderId);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Export/DOList.aspx");
        }

        protected void ddlBooking_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateEmptyYard();
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

                CustomTextBox txtReqUnit = (CustomTextBox)e.Row.Cells[4].FindControl("txtReqUnit");
                txtReqUnit.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "RequiredUnit"));
                txtReqUnit.ReadOnly = !_isEditable;
            }
        }

        #endregion

        #region Private Methods

        private void SetAttributes()
        {
            LockControls(!_isEditable);
        }

        private void RetriveParameters()
        {
            if (!ReferenceEquals(ViewState["DOId"], null))
            {
                Int64.TryParse(Convert.ToString(ViewState["DOId"]), out _doId);
            }
            else
            {
                if (!ReferenceEquals(Request.QueryString["id"], null))
                {
                    Int64.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _doId);
                    ViewState["DOId"] = _doId;
                }
            }

            if (_doId > 0)
                _isEditable = false;
            else
                _isEditable = true;

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
            ListItem Li = null;
            PopulateDropDown((int)Enums.DropDownPopulationFor.Location, ddlLocation, 0);
            Li = new ListItem("SELECT", "0");
            ddlLocation.Items.Insert(0, Li);


            PopulateDropDown((int)Enums.DropDownPopulationFor.Line, ddlNVOCC, 0);
            ddlNVOCC.Items.Insert(0, Li);
            //PopulateBookingNo();

        }

        void PopulateDropDown(int Number, DropDownList ddl, int? Filter)
        {
            CommonBLL.PopulateDropdown(Number, ddl, Filter, 0);
        }

        private void PopulateBookingNo(int Loc, int Line)
        {
            DataTable dt = DOBLL.GetBookingList(Loc, Line);

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
            //DataTable dt = DOBLL.GetEmptyYard(_userId);
            DataTable dt = DOBLL.GetEmptyYard(ddlBooking.SelectedValue.ToInt());

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

            if (bookingId > 0 && emptyYardId > 0)
            {
                List<IDeliveryOrderContainer> lstCntr = DOBLL.GetDeliveryOrderContriner(bookingId, emptyYardId);

                gvwList.DataSource = lstCntr;
                gvwList.DataBind();
            }
            else
            {
                List<IDeliveryOrderContainer> lstCntr = new List<IDeliveryOrderContainer>();

                gvwList.DataSource = lstCntr;
                gvwList.DataBind();
            }
        }

        private void BuildDeliveryOrderEntity(IDeliveryOrder deliveryOrder)
        {
            deliveryOrder.DeliveryOrderId = _doId;
            deliveryOrder.EmptyYardId = Convert.ToInt32(ddlYard.SelectedValue);
            deliveryOrder.BookingId = Convert.ToInt64(ddlBooking.SelectedValue);
            deliveryOrder.DeliveryOrderDate = Convert.ToDateTime(txtDoDate.Text.Trim(), _culture);
        }

        private void BuildContainerEntity(List<DeliveryOrderContainerEntity> lstContainer)
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

                    DeliveryOrderContainerEntity container = new DeliveryOrderContainerEntity();
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

        private void DoSave(IDeliveryOrder deliveryOrder, List<DeliveryOrderContainerEntity> lstContainer)
        {
            string message = string.Empty;

            if (DOBLL.ValidateContainer(lstContainer, out message))
            {
                message = DOBLL.SaveDeliveryOrder(deliveryOrder, lstContainer, _userId);
                ViewState["DOId"] = deliveryOrder.DeliveryOrderId;
                GeneralFunctions.RegisterAlertScript(this, message);
            }
            else
            {
                GeneralFunctions.RegisterAlertScript(this, message);
            }
        }

        private bool ValidateData()
        {
            bool isValid = true;

            if (ddlYard.SelectedValue == "0")
            {
                GeneralFunctions.RegisterAlertScript(this, "Please select empty yard");
                isValid = false;
            }

            if (ddlBooking.SelectedValue == "0")
            {
                GeneralFunctions.RegisterAlertScript(this, "Please select booking number");
                isValid = false;
            }

            if (txtDoDate.Text.Trim() == string.Empty)
            {
                GeneralFunctions.RegisterAlertScript(this, "Please select empty yard");
                isValid = false;
            }

            return isValid;
        }

        private void GenerateReport(Int64 doId)
        {
            LocalReportManager reportManager = new LocalReportManager("DeliveryOrder", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            ReportBLL cls = new ReportBLL();

            List<DOPrintEntity> lstDO = ReportBLL.GetDeliveryOrder(doId);
            List<DOPrintEntity> lstDOCntr = ReportBLL.GetDeliveryOrderContainer(doId);
            ReportDataSource dsDO = new ReportDataSource("dsDeliveryOrder", lstDO);
            ReportDataSource dsDOCntr = new ReportDataSource("dsDeliveryOrderContainer", lstDOCntr);
            reportManager.ReportFormat = ReportFormat.PDF;
            reportManager.AddDataSource(dsDO);
            reportManager.AddDataSource(dsDOCntr);
            reportManager.Export();    
        }

        private void LockControls(bool isLock)
        {
            ddlBooking.Enabled = !isLock;
            ddlYard.Enabled = !isLock;
        }

        #endregion

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNVOCC.SelectedIndex != -1 && ddlLocation.SelectedIndex != -1)
                PopulateBookingNo(ddlLocation.SelectedValue.ToInt(), ddlNVOCC.SelectedValue.ToInt());

        }

        protected void ddlLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNVOCC.SelectedIndex != -1 && ddlLocation.SelectedIndex != -1)
                PopulateBookingNo(ddlLocation.SelectedValue.ToInt(), ddlNVOCC.SelectedValue.ToInt());
        }
    }
}