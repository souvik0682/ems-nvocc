using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.BLL;
using EMS.Utilities;
using EMS.Entity;
using EMS.Utilities.ResourceManager;
using EMS.Common;
using System.Data;
using System.Text;

namespace EMS.WebApp.Export
{
    public partial class AddEditLBCntr : System.Web.UI.Page
    {
        private int _userId = 0;
        private int _LocationId = 0;
        DataTable dtFilteredContainer = new DataTable();
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private int _userLocation = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            _userId = UserBLL.GetLoggedInUserId();
            _userLocation = UserBLL.GetUserLocation();

            //Get user permission.
            UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);


            //_userId = EMS.BLL.UserBLL.GetLoggedInUserId();
            if (!Page.IsPostBack)
            {
                fillAllDropdown();
                CheckUserAccess(hdnLBCntrTransactionId.Value);
                //if (lblTranCode.Text == string.Empty && hdnLBCntrTransactionId.Value == "0")
                if (hdnTranCode.Value == string.Empty && hdnLBCntrTransactionId.Value == "0")
                {
                    DataTable Dt = CreateDataTable();
                    DataRow dr = Dt.NewRow();
                    Dt.Rows.Add(dr);
                    gvSelectedContainer.DataSource = Dt;
                    gvSelectedContainer.DataBind();
                    txtDate.Text = DateTime.Now.ToShortDateString();

                }
                else
                {
                    btnShow.Visible = false;
                    txtDate.Attributes.Add("onchange", "ChangeActivityDate(this);");

                    LBCntrBLL oContainerTranBLL = new LBCntrBLL();
                    SearchCriteria searchCriteria = new SearchCriteria();
                    DataSet ds = new DataSet();

                    ds = oContainerTranBLL.GetLBCntrList(searchCriteria, Convert.ToInt32(hdnLBCntrTransactionId.Value), 0);
       
                    FillHeaderDetail(ds.Tables[0]);
                    DisableHeaderSection();
                    FillContainers(ds.Tables[1]);
                }
            }
            //CheckUserAccess(hdnLBCntrTransactionId.Value);
        }

        void DisableHeaderSection()
        {
            ddlBookingNo.Enabled = false;
            ddlVessel.Enabled = false;
            ddlVoyage.Enabled = false;
            rfvBookingNo.Enabled = false;
            //rfvTeus.Enabled = false;
            rfvDate.Enabled = false;
            rfvVessel.Enabled = false;
            rfvVoyage.Enabled = false;
            //rfvFeus.Enabled = false;
        }

        private void CheckUserAccess(string xID)
        {
            if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                if (ReferenceEquals(user, null) || user.Id == 0)
                {
                    Response.Redirect("~/Login.aspx");
                }

                btnSave.Visible = true;

                if (!_canAdd && !_canEdit)
                    btnSave.Visible = false;
                else
                {

                    if (!_canEdit && xID != "0")
                    {
                        btnSave.Visible = false;
                    }
                    else if (!_canAdd && xID == "0")
                    {
                        btnSave.Visible = false;
                    }
                }

                if (user.UserRole.Id != (int)UserRole.Admin)
                    _LocationId = _userLocation;
                else
                    _LocationId = 0;
                    //ddlLocation.SelectedValue = _userLocation.ToString();
                    //ddlLocation.Enabled = false;
                    //ActionOnFromLocationChange();
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }



        //private void CheckUserAccess()
        //{
        //    if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
        //    {
        //        IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

        //        if (ReferenceEquals(user, null) || user.Id == 0)
        //        {
        //            Response.Redirect("~/Login.aspx");
        //        }

        //        if (user.UserRole.Id != (int)UserRole.Admin)
        //        {
        //            Response.Redirect("~/Unauthorized.aspx");
        //        }
        //    }
        //    else
        //    {
        //        Response.Redirect("~/Login.aspx");
        //    }
        //}

        void FillHeaderDetail(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                txtDate.Text = Convert.ToDateTime(dt.Rows[0]["LBDate"].ToString()).ToShortDateString();

                //ddlLocation.SelectedValue = dt.Rows[0]["fk_LocationID"].ToString();
                //ActionOnFromLocationChange();
                ddlBookingNo.SelectedValue = dt.Rows[0]["fk_BookingID"].ToString();
                txtBookingDate.Text = Convert.ToDateTime(dt.Rows[0]["BookingDate"].ToString()).ToShortDateString();
                lblParty.Text =  dt.Rows[0]["PartyName"].ToString();
                ddlVessel.SelectedValue = dt.Rows[0]["fk_VesselID"].ToString();
                ddlVoyage.SelectedValue = dt.Rows[0]["fk_VoyageID"].ToString();
                lblLoadPort.Text = dt.Rows[0]["PortName"].ToString();

                //ddlLocation.SelectedIndex = ddlLocation.Items.IndexOf(ddlLocation.Items.FindByValue(dt.Rows[0]["ToLocation"].ToString()));
                //ddlVessel

                //ddlEmptyYard.SelectedIndex = ddlEmptyYard.Items.IndexOf(ddlEmptyYard.Items.FindByValue(dt.Rows[0]["EmptyYard"].ToString()));

                //txtTeus.Text = dt.Rows[0]["TEUs"].ToString();
                //txtFEUs.Text = dt.Rows[0]["FEUs"].ToString();
                //txtNarration.Text = dt.Rows[0]["Narration"].ToString();
                lblTranCode.Text = dt.Rows[0]["TransactionCode"].ToString();
                hdnTranCode.Value = dt.Rows[0]["TransactionCode"].ToString();
            }
        }

        void FillContainers(DataTable dt)
        {
            DataTable oTable = CreateDataTable();

            foreach (DataRow Row in dt.Rows)
            {
                DataRow Dr = oTable.NewRow();

                Dr["OldTransactionId"] = "0";
                Dr["NewTransactionId"] = Row["TranId"];
                Dr["ContainerNo"] = Row["ContainerNo"];
                Dr["CntrSize"] = Row["CntrSize"];
                Dr["CntrType"] = Row["CntrType"];
                //Dr["Vessel"] = ddlVessel.Items.FindByValue(Row["fk_VesselID"].ToString()).Text;
                //Dr["Voyage"] = ddlVoyage.Items.FindByValue(Row["fk_VoyageID"].ToString()).Text;

                oTable.Rows.Add(Dr);
            }
            gvSelectedContainer.DataSource = oTable;
            gvSelectedContainer.DataBind();

            if (ViewState["Container"] == null)
                ViewState["Container"] = oTable;
        }

        //private void RetriveParameters()
        //{
        //    //_userId = UserBLL.GetLoggedInUserId();

        //    if (!ReferenceEquals(Request.QueryString["id"], null))
        //    {
        //        Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _ContainerMovementId);
        //        hdnLBCntrTransactionId.Value = _ContainerMovementId.ToString();
        //    }
        //    else if (!ReferenceEquals(Request.QueryString["tcode"], null))
        //    {
        //        lblTranCode.Text = GeneralFunctions.DecryptQueryString(Request.QueryString["tcode"].ToString());
        //        hdnTranCode.Value = GeneralFunctions.DecryptQueryString(Request.QueryString["tcode"].ToString());
        //    }
        //}

        void fillAllDropdown()
        {
            ListItem Li = null;

            //#region FromStatus

            //Li = new ListItem("Select", "0");
            //PopulateDropDown((int)Enums.DropDownPopulationFor.ContainerMovementStatus, ddlFromStatus, 0, 0);

            ////ddlFromStatus.Items.Remove(ddlFromStatus.Items.FindByText());
            //#endregion

            #region Booking

            Li = new ListItem("Select", "0");
            PopulateDropDown((int)Enums.DropDownPopulationFor.Booking, ddlBookingNo, 0, 0);
            ddlBookingNo.Items.Insert(0, Li);

            #endregion

            //#region Booking

            //Li = new ListItem("Select", "0");
            //PopulateDropDown((int)Enums.DropDownPopulationFor.Booking, ddlBookingNo, 0, 0);
            //ddlBookingNo.Items.Insert(0, Li);

            //#endregion

            #region Vessel
            Li = new ListItem("Select", "0");
            PopulateDropDown((int)Enums.DropDownPopulationFor.Vessel, ddlVessel, 0, 0);
            ddlVessel.Items.Insert(0, Li);
            #endregion
        }

        void PopulateDropDown(int Number, DropDownList ddl, int? Filter1, int? Filter2)
        {
            CommonBLL.PopulateDropdown(Number, ddl, Filter1, Filter2);
        }


        void fillContainer(int BookingId)
        {

            LBCntrBLL oContainerTranBLL = new LBCntrBLL();
            dtFilteredContainer = oContainerTranBLL.GetContainerTransactionListFiltered(BookingId, hdnLineID.Value.ToInt());
            ViewState["Container"] = dtFilteredContainer;
            gvContainer.DataSource = dtFilteredContainer;
            gvContainer.DataBind();
        }

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;

            DataTable Dt = CreateDataTable();

            foreach (GridViewRow Row in gvContainer.Rows)
            {
                DataRow dr = Dt.NewRow();

                CheckBox chkContainer = (CheckBox)Row.FindControl("chkContainer");

                if (chkContainer.Checked == true)
                {
                    HiddenField hdnOldTransactionId = (HiddenField)Row.FindControl("hdnOldTransactionId");
                    //HiddenField hdnStatus = (HiddenField)Row.FindControl("hdnStatus");
                    HiddenField hdnContainerType = (HiddenField)Row.FindControl("hdnContainerType");
                    //HiddenField hdnLMDT = (HiddenField)Row.FindControl("hdnLMDT");
                    Label lblContainerNo = (Label)Row.FindControl("lblContainerNo");
                    Label lblContainerSize = (Label)Row.FindControl("lblContainerSize");
                    Label lblContainerType = (Label)Row.FindControl("lblContainerType");

                    dr["OldTransactionId"] = hdnOldTransactionId.Value;
                    dr["NewTransactionId"] = "0";
                    dr["ContainerNo"] = lblContainerNo.Text;    
                    dr["CntrSize"] = lblContainerSize.Text;
                    dr["CntrType"] = lblContainerType.Text;
                    //dr["Vessel"] = ddlVessel.SelectedItem.Text;
                    //dr["Voyage"] = ddlVoyage.SelectedItem.Text;
                    Dt.Rows.Add(dr);
                }
            }

            gvSelectedContainer.DataSource = Dt;
            gvSelectedContainer.DataBind();

            ViewState["Container"] = Dt;

        }

        private DataTable CreateDataTable()
        {
            DataTable Dt = new DataTable();
            DataColumn dc;

            dc = new DataColumn("OldTransactionId");
            Dt.Columns.Add(dc);

            dc = new DataColumn("NewTransactionId");
            Dt.Columns.Add(dc);

            dc = new DataColumn("ContainerNo");
            Dt.Columns.Add(dc);

            dc = new DataColumn("CntrSize");
            Dt.Columns.Add(dc);

            dc = new DataColumn("CntrType");
            Dt.Columns.Add(dc);

            //dc = new DataColumn("Vessel");
            //Dt.Columns.Add(dc);

            //dc = new DataColumn("Voyage");
            //Dt.Columns.Add(dc);

            return Dt;
        }



        private void ActionOnVesselSelectedIndexChanged()
        {
            ListItem Li = new ListItem("Select", "0");
            PopulateDropDown((int)Enums.DropDownPopulationFor.ExpVoyage, ddlVoyage, Convert.ToInt32(ddlVessel.SelectedValue), 3);
            ddlVoyage.Items.Insert(0, Li);
            ddlVoyage.Enabled = true;
        }

        private void ActionOnBookingChange()
        {
            DataSet ds = new DataSet();
            LBCntrBLL oBookingDetailBLL = new LBCntrBLL();
            ds = oBookingDetailBLL.GetBookingDetail(Convert.ToInt32(ddlBookingNo.SelectedValue));
            hdnLineID.Value = ds.Tables[0].Rows[0]["fk_NVOCCID"].ToString();
            txtBookingDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["BookingDate"].ToString()).ToShortDateString();
            lblLoadPort.Text = ds.Tables[0].Rows[0]["BookingNo"].ToString();
            //txtBookingDate.Text = ds.Tables[0].Rows[0]["BookingDate"].ToString();
            lblParty.Text = ds.Tables[0].Rows[0]["CustName"].ToString();
            lblLoadPort.Text = ds.Tables[0].Rows[0]["PortCode"].ToString();
            lblVessel.Text = ds.Tables[0].Rows[0]["VesselName"].ToString();
            lblVoyage.Text = ds.Tables[0].Rows[0]["VoyageNo"].ToString();
            ddlVessel.SelectedIndex = -1;
            ddlVoyage.Items.Clear();
     
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            fillContainer(Convert.ToInt32(ddlBookingNo.SelectedValue));
            //if (ddlFromStatus.SelectedItem.Text == "RCVE")
            //{
            //    fillContainer(Convert.ToInt32(ddlEmptyYard.SelectedValue));
            //}
            //else
            //{
            //    fillContainer(Convert.ToInt32(ddlFromLocation.SelectedValue));
            //}
            ModalPopupExtender1.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //foreach (GridViewRow gvRow in gvSelectedContainer.Rows)
                //{
                //    //HiddenField hdnLMDT = (HiddenField)gvRow.FindControl("hdnLMDT");
                //    HiddenField hdnCDT = (HiddenField)gvRow.Cells[0].FindControl("hdnCDT");
                //    DateTime dt = Convert.ToDateTime(hdnCDT.Value);
                //    if (dt > Convert.ToDateTime(txtDate.Text))
                //    {
                //        lblMessage.Text = "Activity date can not be greater than last movement date";
                //        return;
                //    }
                //}


                LBCntrBLL oContainerTranBLL = new LBCntrBLL();
                string TranCode = string.Empty;

                if (ViewState["Container"] != null)
                    dtFilteredContainer = (DataTable)ViewState["Container"];

                if (dtFilteredContainer.Rows.Count <= 0)
                {
                    lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00078");
                    hdnTranCode.Value = string.Empty;
                    return;
                }

                int BookingID = Convert.ToInt32(ddlBookingNo.SelectedValue);
                int VesselID = Convert.ToInt32(ddlVessel.SelectedValue);
                int VoyageID = Convert.ToInt32(ddlVoyage.SelectedValue);

                int Result = oContainerTranBLL.AddEditLBCntr(out TranCode, hdnTranCode.Value, GenerateContainerXMLString(),
                    Convert.ToDateTime(txtDate.Text), BookingID, VesselID, VoyageID,
                   _userId, DateTime.Now.Date, _userId, DateTime.Now.Date);


                switch (Result)
                {
                    case -1:
                        lblMessage.Text = ResourceManager.GetStringWithoutName("ERR000078");
                        break;
                    case 1:
                        lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00009");
                        if (string.IsNullOrEmpty(hdnTranCode.Value))
                        {
                            lblTranCode.Text = TranCode;
                            ClearAll();
                        }
                        else
                        {
                            Response.Redirect("container-movement.aspx");
                        }

                        break;
                    case 0: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00011");
                        break;
                }

            }
        }

        string GenerateContainerXMLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<Conts>");

            foreach (GridViewRow gvRow in gvSelectedContainer.Rows)
            {
                HiddenField hdnOldTransactionId = (HiddenField)gvRow.FindControl("hdnOldTransactionId");
                HiddenField hdnCurrentTransactionId = (HiddenField)gvRow.FindControl("hdnCurrentTransactionId");
                CheckBox chkItem = (CheckBox)gvRow.FindControl("chkItem");

                sb.Append("<Cont>");

                sb.Append("<Oid>" + hdnOldTransactionId.Value + "</Oid>");
                sb.Append("<Nid>" + hdnCurrentTransactionId.Value + "</Nid>");
                sb.Append("<Stats>" + chkItem.Checked.ToString() + "</Stats>");

                sb.Append("</Cont>");

            }

            sb.Append("</Conts>");

            return sb.ToString();
        }

        void ClearAll()
        {
            //txtDate.Text = string.Empty;
            txtBookingDate.Text = string.Empty;
            txtDate.Text = string.Empty;
            lblLoadPort.Text = string.Empty;
            lblParty.Text = string.Empty;
            lblLoadPort.Text = string.Empty;

            //ddlFromLocation.SelectedIndex = 0;
            ddlBookingNo.SelectedIndex = 0;
            ddlVessel.SelectedIndex = 0;
            ddlVoyage.SelectedIndex = -1;

            ddlVoyage.Enabled = false;

            //hdnChargeID.Value = string.Empty;
            hdnLBCntrTransactionId.Value = "0";
            hdnTranCode.Value = string.Empty;
            //lblTranCode.Text = string.Empty;

            DataTable Dt = CreateDataTable();
            DataRow dr = Dt.NewRow();
            Dt.Rows.Add(dr);
            gvSelectedContainer.DataSource = Dt;
            gvSelectedContainer.DataBind();

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageLBCntr.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
        }

        protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActionOnVesselSelectedIndexChanged();

        }

        protected void ddlVoyage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlBookingNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActionOnBookingChange();
        }


    }
}