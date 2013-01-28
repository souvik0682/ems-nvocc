﻿using System;
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

        private int _ContainerMovementId = 0;
        DataTable dtFilteredContainer = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserAccess();
            RetriveParameters();
            if (!Page.IsPostBack)
            {
                fillAllDropdown();

                if (lblTranCode.Text == string.Empty && hdnContainerTransactionId.Value == "0")
                {
                    DataTable Dt = CreateDataTable();
                    DataRow dr = Dt.NewRow();
                    Dt.Rows.Add(dr);
                    gvSelectedContainer.DataSource = Dt;
                    gvSelectedContainer.DataBind();

                }
                else
                {
                    ContainerTranBLL oContainerTranBLL = new ContainerTranBLL();
                    SearchCriteria searchCriteria = new SearchCriteria();
                    DataSet ds = new DataSet();

                    if (!string.IsNullOrEmpty(hdnContainerTransactionId.Value))
                    {
                        ds = oContainerTranBLL.GetContainerTransactionList(searchCriteria, Convert.ToInt32(hdnContainerTransactionId.Value));
                    }
                    else if(!string.IsNullOrEmpty(lblTranCode.Text))
                    {
                        searchCriteria.StringOption4 = lblTranCode.Text;
                        ds = oContainerTranBLL.GetContainerTransactionList(searchCriteria, 0);
                    }
                    FillHeaderDetail(ds.Tables[0]);
                    FillContainers(ds.Tables[1]);
                }
            }
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
                    Response.Redirect("~/Unauthorized.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

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

                txtTeus.Text = dt.Rows[0]["TEUs"].ToString();
                txtFEUs.Text = dt.Rows[0]["FEUs"].ToString();
                txtNarration.Text = dt.Rows[0]["Narration"].ToString();
                lblTranCode.Text = dt.Rows[0]["TransactionCode"].ToString();
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

                oTable.Rows.Add(Dr);
            }
            gvSelectedContainer.DataSource = oTable;
            gvSelectedContainer.DataBind();

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
            }
        }

        void fillAllDropdown()
        {
            ListItem Li = null;

            #region FromStatus

            Li = new ListItem("Select", "0");
            PopulateDropDown((int)Enums.DropDownPopulationFor.ContainerMovementStatus, ddlFromStatus, 0);
            ddlFromStatus.Items.Insert(0, Li);
            ddlFromStatus.Items.Remove(ddlFromStatus.Items.FindByText("ONBR"));
            #endregion

            #region FromLocation

            Li = new ListItem("Select", "0");
            PopulateDropDown((int)Enums.DropDownPopulationFor.Location, ddlFromLocation, 0);
            ddlFromLocation.Items.Insert(0, Li);

            #endregion

            #region ToLocation

            Li = new ListItem("Select", "0");
            PopulateDropDown((int)Enums.DropDownPopulationFor.Location, ddlTolocation, 0);
            ddlTolocation.Items.Insert(0, Li);

            #endregion
        }

        void PopulateDropDown(int Number, DropDownList ddl, int? Filter)
        {
            CommonBLL.PopulateDropdown(Number, ddl, Filter);
        }

        protected void ddlFromStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActionOnFromStatusChange();
        }

        private void ActionOnFromStatusChange()
        {
            ListItem Li = new ListItem("Select", "0");
            PopulateDropDown((int)Enums.DropDownPopulationFor.ContainerMovementStatus, ddlToStatus, Convert.ToInt32(ddlFromStatus.SelectedValue));
            ddlToStatus.Items.Insert(0, Li);


            if (ddlFromStatus.SelectedItem.Text == "RCVE")
            {
                ddlEmptyYard.Enabled = true;
            }
            else
            {
                ddlEmptyYard.Enabled = false;
            }
        }

        protected void ddlToStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActionOnToStatusChange();

        }

        private void ActionOnToStatusChange()
        {
            if (ddlToStatus.SelectedItem.Text == "RCVE" || ddlFromStatus.SelectedItem.Text == "RCVE")
            {
                ddlEmptyYard.Enabled = true;
                rfvEmptyYard.Enabled = true;
            }
            else
            {
                ddlEmptyYard.Enabled = false;
                rfvEmptyYard.Enabled = false;
                if (ddlEmptyYard.Items.Count > 0)
                    ddlEmptyYard.SelectedIndex = 0;
            }

            if (ddlToStatus.SelectedItem.Text == "TRFE")
            {
                //txtToLocation.Enabled = true;
                ddlTolocation.Enabled = true;
                rfvToLocation.Enabled = true;
                //hdnToLocation.Value = "0";
            }
            else
            {
                //txtToLocation.Enabled = false;
                //txtToLocation.Text = string.Empty;
                ddlTolocation.SelectedIndex = 0;
                ddlTolocation.Enabled = false;
                rfvToLocation.Enabled = false;
                //hdnToLocation.Value = "0";
            }
        }

        void fillContainer(int EmptyYardId)
        {
            
            ContainerTranBLL oContainerTranBLL = new ContainerTranBLL();
            dtFilteredContainer = oContainerTranBLL.GetContainerTransactionListFiltered(Convert.ToInt16(ddlFromStatus.SelectedValue), EmptyYardId);

            gvContainer.DataSource = dtFilteredContainer;
            gvContainer.DataBind();
        }

        protected void btnProceed_Click(object sender, EventArgs e)
        {
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
                    Label lblContainerNo = (Label)Row.FindControl("lblContainerNo");
                    Label lblContainerSize = (Label)Row.FindControl("lblContainerSize");

                    dr["OldTransactionId"] = hdnOldTransactionId.Value;
                    dr["NewTransactionId"] = "0";
                    dr["ContainerNo"] = lblContainerNo.Text;
                    dr["FromStatus"] = hdnStatus.Value;
                    dr["LandingDate"] = hdnLandingDate.Value;
                    dr["ToStatus"] = ddlToStatus.SelectedItem.Text;
                    dr["ChangeDate"] = txtDate.Text;

                    Dt.Rows.Add(dr);
                }
            }

            gvSelectedContainer.DataSource = Dt;
            gvSelectedContainer.DataBind();

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

        }

        private void ActionOnFromLocationChange()
        {
            ListItem Li = new ListItem("Select", "0");
            PopulateDropDown((int)Enums.DropDownPopulationFor.VendorList, ddlEmptyYard, Convert.ToInt32(ddlFromLocation.SelectedValue));
            ddlEmptyYard.Items.Insert(0, Li);
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            if (ddlFromStatus.SelectedItem.Text == "RCVE")
            {
                fillContainer(Convert.ToInt32(ddlEmptyYard.SelectedValue));
            }
            else
            {
                fillContainer(0);
            }

            ModalPopupExtender1.Show();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ContainerTranBLL oContainerTranBLL = new ContainerTranBLL();
                string TranCode = string.Empty;

                if (dtFilteredContainer.Rows.Count <= 0)
                {
                    lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00078");
                    return; ;
                }

                int Result = oContainerTranBLL.AddEditContainerTransaction(out TranCode, lblTranCode.Text, GenerateContainerXMLString(),
                    Convert.ToInt32(ddlToStatus.SelectedValue), Convert.ToInt32(txtTeus.Text), Convert.ToInt32(txtFEUs.Text), Convert.ToDateTime(txtDate.Text),
                    Convert.ToString(txtNarration.Text), Convert.ToInt32(ddlFromLocation.SelectedValue), Convert.ToInt32(ddlTolocation.SelectedValue),
                    Convert.ToInt32(ddlEmptyYard.SelectedValue), 1, DateTime.Now.Date, 1, DateTime.Now.Date);


                switch (Result)
                {
                    case -1:
                        lblMessage.Text = ResourceManager.GetStringWithoutName("ERR000078");
                        break;
                    case 1:
                        lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00009");
                        if (string.IsNullOrEmpty(lblTranCode.Text))
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
            txtDate.Text = string.Empty;
            txtFEUs.Text = string.Empty;
            txtNarration.Text = string.Empty;
            txtTeus.Text = string.Empty;

            ddlFromLocation.SelectedIndex = 0;
            ddlFromStatus.SelectedIndex = 0;
            ddlToStatus.SelectedIndex = 0;

            if (ddlEmptyYard.Items.Count > 0)
                ddlEmptyYard.SelectedIndex = 0;

            if (ddlTolocation.Items.Count > 0)
                ddlTolocation.SelectedIndex = 0;

            //hdnChargeID.Value = string.Empty;
            hdnContainerTransactionId.Value = string.Empty;
            //lblTranCode.Text = string.Empty;

            DataTable Dt = CreateDataTable();
            DataRow dr = Dt.NewRow();
            Dt.Rows.Add(dr);
            gvSelectedContainer.DataSource = Dt;
            gvSelectedContainer.DataBind();

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("container-movement.aspx");
        }

    }
}
