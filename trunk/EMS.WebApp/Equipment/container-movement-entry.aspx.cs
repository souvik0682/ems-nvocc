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


namespace EMS.WebApp.Equipment
{
    public partial class container_movement_entry : System.Web.UI.Page
    {
        private int _userId = 0;
        private int _ContainerMovementId = 0;
        DataTable dtFilteredContainer = new DataTable();
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private int _userLocation = 0;
        //private int _reqfield = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            _userId = UserBLL.GetLoggedInUserId();
            _userLocation = UserBLL.GetUserLocation();

            //Get user permission.
            UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);


            //_userId = EMS.BLL.UserBLL.GetLoggedInUserId();
            if (!Page.IsPostBack)
            {
                fillAllDropdown();
                CheckUserAccess(hdnContainerTransactionId.Value);
                //if (lblTranCode.Text == string.Empty && hdnContainerTransactionId.Value == "0")
                if (hdnTranCode.Value == string.Empty && hdnContainerTransactionId.Value == "0")
                {
                    DataTable Dt = CreateDataTable();
                    DataRow dr = Dt.NewRow();
                    dr["Editable"] = true;
                    Dt.Rows.Add(dr);
                    gvSelectedContainer.DataSource = Dt;
                    gvSelectedContainer.DataBind();
                    txtDate.Text = DateTime.Now.ToShortDateString();

                }
                else
                {
                    btnShow.Visible = false;
                    txtDate.Attributes.Add("onchange", "ChangeActivityDate(this);");

                    ContainerTranBLL oContainerTranBLL = new ContainerTranBLL();
                    SearchCriteria searchCriteria = new SearchCriteria();
                    DataSet ds = new DataSet();

                    if (!string.IsNullOrEmpty(hdnContainerTransactionId.Value))
                    {
                        ds = oContainerTranBLL.GetContainerTransactionList(searchCriteria, Convert.ToInt32(hdnContainerTransactionId.Value), _userLocation);
                    }
                    //else if(!string.IsNullOrEmpty(lblTranCode.Text))
                    else if (!string.IsNullOrEmpty(hdnTranCode.Value))
                    {
                        searchCriteria.StringOption4 = hdnTranCode.Value;
                        ds = oContainerTranBLL.GetContainerTransactionList(searchCriteria, 0, _userLocation);
                    }
                    FillHeaderDetail(ds.Tables[0]);
                    DisableHeaderSection();
                    FillContainers(ds.Tables[1]);
                }
            }
            //CheckUserAccess(hdnContainerTransactionId.Value);
        }

        void DisableHeaderSection()
        {
            ddlFromStatus.Enabled = false;
            ddlFromLocation.Enabled = false;
            ddlToStatus.Enabled = false;
            ddlTolocation.Enabled = false;
            ddlEmptyYard.Enabled = false;
            ddlLine.Enabled = false;

            txtTeus.Enabled = false;
            txtNarration.Enabled = false;
            //txtDate.Enabled = false;
            txtFEUs.Enabled = false;


            rfvFromStatus.Enabled = false;
            rfvFromLocation.Enabled = false;
            //rfvTeus.Enabled = false;
            rfvDate.Enabled = false;
            rfvToStatus.Enabled = false;
            rfvToLocation.Enabled = false;
            //_reqfield = 0;
            //rfvFeus.Enabled = false;
            rfvEmptyYard.Enabled = false;
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
                {
                    ddlFromLocation.SelectedValue = _userLocation.ToString();
                    ddlFromLocation.Enabled = false;
                    ActionOnFromLocationChange();

                }

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
                txtDate.Text = Convert.ToDateTime(dt.Rows[0]["MovementDate"].ToString()).ToShortDateString();
                ddlFromStatus.SelectedIndex = ddlFromStatus.Items.IndexOf(ddlFromStatus.Items.FindByValue(dt.Rows[0]["FromStatus"].ToString()));
                ActionOnFromStatusChange();

                ddlToStatus.SelectedIndex = ddlToStatus.Items.IndexOf(ddlToStatus.Items.FindByValue(dt.Rows[0]["ToStatus"].ToString()));
                ActionOnToStatusChange();

                ddlFromLocation.SelectedIndex = ddlFromLocation.Items.IndexOf(ddlFromLocation.Items.FindByValue(dt.Rows[0]["FromLocation"].ToString()));
                ActionOnFromLocationChange();

                ddlTolocation.SelectedIndex = ddlTolocation.Items.IndexOf(ddlTolocation.Items.FindByValue(dt.Rows[0]["ToLocation"].ToString()));
                ddlEmptyYard.SelectedIndex = ddlEmptyYard.Items.IndexOf(ddlEmptyYard.Items.FindByValue(dt.Rows[0]["EmptyYard"].ToString()));
                ddlBookingNo.SelectedIndex = ddlBookingNo.Items.IndexOf(ddlBookingNo.Items.FindByValue(dt.Rows[0]["fk_BookingID"].ToString()));

                ddlBookingNo_SelectedIndexChanged(null, null);

                ddlDONo.SelectedIndex = ddlDONo.Items.IndexOf(ddlDONo.Items.FindByValue(dt.Rows[0]["fk_DOID"].ToString()));

                ddlLine.SelectedIndex = ddlLine.Items.IndexOf(ddlLine.Items.FindByValue(dt.Rows[0]["LINEID"].ToString()));

                txtTeus.Text = dt.Rows[0]["TEUs"].ToString();
                txtFEUs.Text = dt.Rows[0]["FEUs"].ToString();
                txtNarration.Text = dt.Rows[0]["Narration"].ToString();
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
                Dr["FromStatus"] = ddlFromStatus.Items.FindByValue(Row["FromStatus"].ToString()).Text;
                Dr["ToStatus"] = ddlFromStatus.Items.FindByValue(Row["ToStatus"].ToString()).Text;
                Dr["LandingDate"] = Row["LandingDate"];
                Dr["ChangeDate"] = Row["MovementDate"];
                Dr["LMDT"] = Row["LMDT"];
                Dr["Editable"] = Row["Editable"];

                oTable.Rows.Add(Dr);
            }
            gvSelectedContainer.DataSource = oTable;
            gvSelectedContainer.DataBind();

            if (ViewState["Container"] == null)
                ViewState["Container"] = oTable;
        }

        private void RetriveParameters()
        {
            //_userId = UserBLL.GetLoggedInUserId();

            if (!ReferenceEquals(Request.QueryString["id"], null))
            {
                Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _ContainerMovementId);
                hdnContainerTransactionId.Value = _ContainerMovementId.ToString();
            }
            else if (!ReferenceEquals(Request.QueryString["tcode"], null))
            {
                lblTranCode.Text = GeneralFunctions.DecryptQueryString(Request.QueryString["tcode"].ToString());
                hdnTranCode.Value = GeneralFunctions.DecryptQueryString(Request.QueryString["tcode"].ToString());
            }
        }

        void fillAllDropdown()
        {
            ListItem Li = null;

            #region FromStatus

            Li = new ListItem("Select", "0");
            PopulateDropDown((int)Enums.DropDownPopulationFor.ContainerMovementStatus, ddlFromStatus, 0, 0);
            ddlFromStatus.Items.Insert(0, Li);
            ddlFromStatus.Items.Remove(ddlFromStatus.Items.FindByText("ONBR"));
            //ddlFromStatus.Items.Remove(ddlFromStatus.Items.FindByText());
            #endregion

            #region FromLocation

            Li = new ListItem("Select", "0");
            PopulateDropDown((int)Enums.DropDownPopulationFor.Location, ddlFromLocation, 0, 0);
            ddlFromLocation.Items.Insert(0, Li);

            #endregion

            #region ToLocation

            Li = new ListItem("Select", "0");
            PopulateDropDown((int)Enums.DropDownPopulationFor.Location, ddlTolocation, 0, 0);
            ddlTolocation.Items.Insert(0, Li);

            #endregion

            #region Line/Nvocc
            Li = new ListItem("Select", "0");
            PopulateDropDown((int)Enums.DropDownPopulationFor.Line, ddlLine, 0, 0);
            ddlLine.Items.Insert(0, Li);
            #endregion
        }

        void PopulateDropDown(int Number, DropDownList ddl, int? Filter1, int? Filter2)
        {
            CommonBLL.PopulateDropdown(Number, ddl, Filter1, Filter2);
        }

        protected void ddlFromStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActionOnFromStatusChange();
        }

        private void ActionOnFromStatusChange()
        {
            ListItem Li = new ListItem("Select", "0");
            PopulateDropDown((int)Enums.DropDownPopulationFor.ContainerMovementStatus, ddlToStatus, Convert.ToInt32(ddlFromStatus.SelectedValue), 0);
            ddlToStatus.Items.Insert(0, Li);


            if (ddlFromStatus.SelectedItem.Text == "RCVE")
            {
                ddlEmptyYard.Enabled = true;
                //_reqfield = 1;
            }
            else
            {
                ddlEmptyYard.Enabled = false;
            }

            if (ddlFromStatus.SelectedItem.Text == "TRFE")
            {
                //ddlToStatus.Enabled = false;
                ddlTolocation.Enabled = true;
                //_reqfield = 3;
            }
            else
            {
                ddlToStatus.Enabled = true;
                ddlTolocation.Enabled = false;
            }

        }

        protected void ddlToStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActionOnToStatusChange();

        }

        private void ActionOnToStatusChange()
        {
            //_reqfield = 0;
            if (ddlToStatus.SelectedItem.Text == "RCVE" || ddlFromStatus.SelectedItem.Text == "RCVE")
            {
                ddlEmptyYard.Enabled = true;
                rfvEmptyYard.Enabled = true;
                //_reqfield = 1;
            }
            else
            {
                ddlEmptyYard.Enabled = false;
                rfvEmptyYard.Enabled = false;
                if (ddlEmptyYard.Items.Count > 0)
                    ddlEmptyYard.SelectedIndex = 0;
            }

            if (ddlToStatus.SelectedItem.Text == "TRFE" || ddlToStatus.SelectedItem.Text == "ICDO" || ddlFromStatus.SelectedItem.Text == "ICDI")
            {
                //txtToLocation.Enabled = true;
                ddlTolocation.Enabled = true;
                rfvToLocation.Enabled = true;
                ddlTolocation.SelectedIndex = 0;
                //if (_reqfield == 1)
                //    _reqfield = 2;
                //else
                //    _reqfield = 3;
                //hdnToLocation.Value = "0";
            }
            else
            {
                //txtToLocation.Enabled = false;
                //txtToLocation.Text = string.Empty;
                ddlTolocation.SelectedIndex = -1;
                ddlTolocation.Enabled = false;
                rfvToLocation.Enabled = false;
                //_reqfield = 0;
                //hdnToLocation.Value = "0";
            }

            if (ddlToStatus.SelectedItem.Text == "SNTS" && ddlFromStatus.SelectedItem.Text == "RCVE")
            {
                ddlBookingNo.Enabled = true;
                ddlDONo.Enabled = true;
                ListItem Li = new ListItem("Select", "0");
                PopulateDropDown((int)Enums.DropDownPopulationFor.Booking, ddlBookingNo, Convert.ToInt32(ddlFromLocation.SelectedValue), 0);
                ddlBookingNo.Items.Insert(0, Li);
                ddlBookingNo.SelectedIndex = 0;
                //_reqfield = 1;
            }
            else
            {
                ddlBookingNo.Items.Clear();
                ddlDONo.Items.Clear();
                ddlBookingNo.Enabled = false;
                ddlDONo.Enabled = false;
            }

            //if (ddlToStatus.SelectedItem.Text == "ICDO" || ddlFromStatus.SelectedItem.Text == "ICDI")
            //{
            //    ddlTolocation.Enabled = true;
            //    ddlTolocation.SelectedIndex = 0;
            //    rfvToLocation.Enabled = true;
            //}
          

        }

        void fillContainer(int EmptyYardId)
        {

            ContainerTranBLL oContainerTranBLL = new ContainerTranBLL();
            dtFilteredContainer = oContainerTranBLL.GetContainerTransactionListFiltered(Convert.ToInt16(ddlFromStatus.SelectedValue), EmptyYardId, Convert.ToDateTime(txtDate.Text), Convert.ToInt16(ddlLine.SelectedValue));
            ViewState["Container"] = dtFilteredContainer;
            gvContainer.DataSource = dtFilteredContainer;
            gvContainer.DataBind();
        }

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;

            //Checking for container requirement for SNTS with DO and Booking


            DataTable Dt = CreateDataTable();

            foreach (GridViewRow Row in gvContainer.Rows)
            {
                DataRow dr = Dt.NewRow();

                CheckBox chkContainer = (CheckBox)Row.FindControl("chkContainer");

                if (chkContainer.Checked == true)
                {
                    HiddenField hdnOldTransactionId = (HiddenField)Row.FindControl("hdnOldTransactionId");
                    HiddenField hdnStatus = (HiddenField)Row.FindControl("hdnStatus");
                    HiddenField hdnLandingDate = (HiddenField)Row.FindControl("hdnLandingDate");
                    HiddenField hdnLMDT = (HiddenField)Row.FindControl("hdnLMDT");
                    Label lblContainerNo = (Label)Row.FindControl("lblContainerNo");
                    Label lblContainerSize = (Label)Row.FindControl("lblContainerSize");
                    Label lblContainerType = (Label)Row.FindControl("lblContainerType");

                    dr["OldTransactionId"] = hdnOldTransactionId.Value;
                    dr["NewTransactionId"] = "0";
                    dr["ContainerNo"] = lblContainerNo.Text;
                    dr["FromStatus"] = hdnStatus.Value;
                    dr["LandingDate"] = hdnLandingDate.Value;
                    dr["ToStatus"] = ddlToStatus.SelectedItem.Text;
                    dr["ChangeDate"] = txtDate.Text;
                    dr["LMDT"] = hdnLMDT.Value;
                    dr["Editable"] = true;
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

            dc = new DataColumn("FromStatus");
            Dt.Columns.Add(dc);

            dc = new DataColumn("LandingDate");
            Dt.Columns.Add(dc);

            dc = new DataColumn("ToStatus");
            Dt.Columns.Add(dc);

            dc = new DataColumn("ChangeDate");
            Dt.Columns.Add(dc);

            dc = new DataColumn("LMDT");
            Dt.Columns.Add(dc);

            dc = new DataColumn("Editable");
            Dt.Columns.Add(dc);


            return Dt;
        }

        protected void ddlEmptyYard_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFromStatus.SelectedItem.Text == "RCVE")
            {
                //fillContainer(Convert.ToInt32(ddlEmptyYard.SelectedValue));
                //btnShow.Enabled = true;
                //btnShow.Attributes.Add("style", "background-color:transparent;cursor:pointer;");
            }
        }

        protected void ddlFromLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActionOnFromLocationChange();
            ActionOnLocLineChange();

        }

        private void ActionOnFromLocationChange()
        {
            ListItem Li = new ListItem("Select", "0");
            PopulateDropDown((int)Enums.DropDownPopulationFor.VendorList, ddlEmptyYard, Convert.ToInt32(ddlFromLocation.SelectedValue), 3);
            ddlEmptyYard.Items.Insert(0, Li);

            //if (ddlToStatus.SelectedItem.Text == "SNTS" && ddlFromStatus.SelectedItem.Text == "RCVE")
            //{
            //    ddlBookingNo.Enabled = true;
            //    ddlDONo.Enabled = true;
            //    ListItem Lx = new ListItem("Select", "0");
            //    PopulateDropDown((int)Enums.DropDownPopulationFor.Booking, ddlBookingNo, Convert.ToInt32(ddlFromLocation.SelectedValue), 0);
            //    ddlBookingNo.Items.Insert(0, Lx);
            //    //_reqfield = 1;
            //}
            //else
            //{
            //    ddlBookingNo.Enabled = false;
            //    ddlDONo.Enabled = false;
            //}
        }

        private void ActionOnLocLineChange()
        {
            
            if (ddlFromLocation.SelectedIndex > 0 && ddlLine.SelectedIndex > 0)

                if (ddlToStatus.SelectedItem.Text == "SNTS" && ddlFromStatus.SelectedItem.Text == "RCVE")
                {
                    ddlBookingNo.Enabled = true;
                    ddlDONo.Enabled = true;
                    ListItem Lx = new ListItem("Select", "0");
                    PopulateDropDown((int)Enums.DropDownPopulationFor.Booking, ddlBookingNo, Convert.ToInt32(ddlFromLocation.SelectedValue), 0);
                    ddlBookingNo.Items.Insert(0, Lx);
                    //_reqfield = 1;
                }
                else
                {
                    ddlBookingNo.Enabled = false;
                    ddlDONo.Enabled = false;
                }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            if ((ddlFromStatus.SelectedItem.Text == "RCVE") || (ddlToStatus.SelectedItem.Text == "RCVE"))
            {
                if (ddlEmptyYard.SelectedIndex == 0)
                    return;
            }

            if (ddlToStatus.SelectedItem.Text == "TRFE" || ddlToStatus.SelectedItem.Text == "ICDO" || ddlFromStatus.SelectedItem.Text == "ICDI")
            {
                if (ddlTolocation.SelectedIndex == 0)
                    return;
            }

            //if (_reqfield == 1 && ddlEmptyYard.SelectedIndex < 1)
            //    return;

            //if (_reqfield == 2 && (ddlTolocation.SelectedIndex < 1 || ddlEmptyYard.SelectedIndex < 1))
            //    return;

            //if (_reqfield == 3 && ddlTolocation.SelectedIndex < 1)
            //    return;

            if (ddlFromStatus.SelectedItem.Text == "RCVE")
            {
                fillContainer(Convert.ToInt32(ddlEmptyYard.SelectedValue));
            }
            else
            {
                fillContainer(Convert.ToInt32(ddlFromLocation.SelectedValue));
            }

            ModalPopupExtender1.Show();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Int32 bkID;
            Int32 doID;

            if (Page.IsValid)
            {


                foreach (GridViewRow gvRow in gvSelectedContainer.Rows)
                {
                    //HiddenField hdnLMDT = (HiddenField)gvRow.FindControl("hdnLMDT");
                    HiddenField hdnCDT = (HiddenField)gvRow.Cells[0].FindControl("hdnCDT");
                    DateTime dt = Convert.ToDateTime(hdnCDT.Value);
                    if (dt > Convert.ToDateTime(txtDate.Text))
                    {
                        lblMessage.Text = "Activity date can not be greater than last movement date";
                        return;
                    }
                }


                ContainerTranBLL oContainerTranBLL = new ContainerTranBLL();
                string TranCode = string.Empty;

                if (ViewState["Container"] != null)
                    dtFilteredContainer = (DataTable)ViewState["Container"];

                if (dtFilteredContainer.Rows.Count <= 0)
                {
                    lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00078");
                    hdnTranCode.Value = string.Empty;
                    return; ;
                }

                if (String.IsNullOrEmpty(txtTeus.Text))
                    txtTeus.Text = "0";

                if (String.IsNullOrEmpty(txtFEUs.Text))
                    txtFEUs.Text = "0";


                int FLocation = Convert.ToInt32(ddlFromLocation.SelectedValue);
                int TLocation = Convert.ToInt32(ddlTolocation.SelectedValue);
                int EYard = 0;
                if (ddlEmptyYard.SelectedIndex > -1)
                {
                    EYard = Convert.ToInt32(ddlEmptyYard.SelectedValue);
                }
                
                if (ddlBookingNo.SelectedValue == "")
                    bkID = 0;
                else
                    bkID = Convert.ToInt32(ddlBookingNo.SelectedValue);

                if (ddlDONo.SelectedValue == "")
                    doID = 0;
                else
                    doID = Convert.ToInt32(ddlDONo.SelectedValue);

                int Result = oContainerTranBLL.AddEditContainerTransaction(out TranCode, hdnTranCode.Value, GenerateContainerXMLString(),
                    Convert.ToInt32(ddlToStatus.SelectedValue), Convert.ToInt32(txtTeus.Text), Convert.ToInt32(txtFEUs.Text), Convert.ToDateTime(txtDate.Text),
                    Convert.ToString(txtNarration.Text), FLocation, TLocation,
                    EYard, _userId, DateTime.Now.Date, _userId, DateTime.Now.Date,bkID, doID);


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
            txtFEUs.Text = string.Empty;
            txtNarration.Text = string.Empty;
            txtTeus.Text = string.Empty;

            //ddlFromLocation.SelectedIndex = 0;
            ddlFromStatus.SelectedIndex = 0;
            ddlToStatus.SelectedIndex = 0;

            if (ddlEmptyYard.Items.Count > 0)
                ddlEmptyYard.SelectedIndex = 0;

            if (ddlTolocation.Items.Count > 0)
                ddlTolocation.SelectedIndex = 0;

            if (ddlLine.Items.Count > 0)
                ddlLine.SelectedIndex = 0;

            ddlEmptyYard.Enabled = false;
            //hdnChargeID.Value = string.Empty;
            hdnContainerTransactionId.Value = "0";
            hdnTranCode.Value = string.Empty;
            //lblTranCode.Text = string.Empty;

            DataTable Dt = CreateDataTable();
            DataRow dr = Dt.NewRow();
            dr["Editable"] = true;
            Dt.Rows.Add(dr);
            gvSelectedContainer.DataSource = Dt;
            gvSelectedContainer.DataBind();

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("container-movement.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
        }

        protected void ddlBookingNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem Li = new ListItem("Select", "0");
            PopulateDropDown((int)Enums.DropDownPopulationFor.DO, ddlDONo, Convert.ToInt32(ddlBookingNo.SelectedValue), 3);
            ddlDONo.Items.Insert(0, Li);
            ddlDONo.SelectedIndex = 0;

        }

        protected void ddlDONo_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        protected void ddlLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActionOnLocLineChange();
            //if (ddlToStatus.SelectedItem.Text == "SNTS" && ddlFromStatus.SelectedItem.Text == "RCVE")
            //{
            //    ddlBookingNo.Enabled = true;
            //    ddlDONo.Enabled = true;
            //    ListItem Lx = new ListItem("Select", "0");
            //    PopulateDropDown((int)Enums.DropDownPopulationFor.Booking, ddlBookingNo, Convert.ToInt32(ddlFromLocation.SelectedValue), 0);
            //    ddlBookingNo.Items.Insert(0, Lx);
            //    //_reqfield = 1;
            //}
            //else
            //{
            //    ddlBookingNo.Enabled = false;
            //    ddlDONo.Enabled = false;
            //}
        }

        protected void gvSelectedContainer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



    }
}
