﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.BLL;
using EMS.Common;
using EMS.Entity;
using EMS.Utilities;
using EMS.Utilities.ResourceManager;
using System.Data;

namespace EMS.WebApp.Transaction
{
    public partial class AddEditUser : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private int _uId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        MoneyReceiptEntity mrSession = null;
        private int _InvoiceId = 0;
        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();
            SetAttributes();

            if (!IsPostBack)
            {
                //this.getMoneyReceiptObjectInstance(null);

                LoadData();

            }


            //if (Session["CHARGEDETAILS"] != null)
            //{
            //    this.gvwAddEditMoneyReceipts.DataSource = Session["CHARGEDETAILS"] as List<ChargeDetails>;
            //    this.gvwAddEditMoneyReceipts.DataBind();
            //}
        }

        private MoneyReceiptEntity getMoneyReceiptObjectInstance(MoneyReceiptEntity moneyReceipt)
        {
            MoneyReceiptEntity moneyReceiptInstance = null;

            if (moneyReceipt == null)
            {
                moneyReceiptInstance = Session["MROBJECT"] as MoneyReceiptEntity;
                if (moneyReceiptInstance == null)
                {
                    moneyReceiptInstance = new MoneyReceiptEntity();
                    moneyReceiptInstance.IsAdded = 1;
                    moneyReceiptInstance.IsEdited = 1;
                    Session["MROBJECT"] = moneyReceiptInstance;
                }
            }
            else
            {
                moneyReceiptInstance = moneyReceipt;
                Session["MROBJECT"] = moneyReceiptInstance;
            }

            return moneyReceiptInstance;

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveMoneyReceipts();
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddlLoc.Enabled = false;
            //ddlMultiLoc.Enabled = false;

            //IRole role = new UserBLL().GetRole(Convert.ToInt32(ddlRole.SelectedValue));

            //if (!ReferenceEquals(role, null))
            //{
            //    if (role.LocationSpecific.HasValue && role.LocationSpecific.Value)
            //    {
            //        ddlLoc.Enabled = true;
            //        ddlMultiLoc.Enabled = true;
            //    }
            //}
        }

        #endregion

        #region Private Methods

        private void RetriveParameters()
        {
            _userId = MoneyReceiptsBLL.GetLoggedInUserId();

            //Get user permission.
            MoneyReceiptsBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);

            if (!ReferenceEquals(Request.QueryString["invid"], null))
            {
                Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["invid"].ToString()), out _InvoiceId);
            }
        }

        private void SetAttributes()
        {
            //spnName.Style["display"] = "none";
            //spnFName.Style["display"] = "none";
            //spnLName.Style["display"] = "none";
            //spnEmail.Style["display"] = "none";
            //spnRole.Style["display"] = "none";
            //spnLoc.Style["display"] = "none";

            //if (!IsPostBack)
            //{
            //    if (_uId == -1) //Add mode
            //    {
            //        if (!_canAdd) btnSave.Visible = false;
            //    }
            //    else
            //    {
            //        if (!_canEdit) btnSave.Visible = false;
            //    }

            //    ddlLoc.Enabled = false;
            //    ddlMultiLoc.Enabled = false;
            //    //rfvUserName.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00036");
            //    //rfvFName.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00037");
            //    //rfvLName.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00038");
            //    //rfvEmail.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00039");
            //    //rfvRole.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00040");
            //    //rfvLoc.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00025");

            //    btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageUser.aspx','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
            //    revEmail.ValidationExpression = Constants.EMAIL_REGX_EXP;
            //    revEmail.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00026");

            //    spnName.InnerText = ResourceManager.GetStringWithoutName("ERR00048");
            //    spnFName.InnerText = ResourceManager.GetStringWithoutName("ERR00049");
            //    spnLName.InnerText = ResourceManager.GetStringWithoutName("ERR00050");
            //    spnEmail.InnerText = ResourceManager.GetStringWithoutName("ERR00051");
            //    spnRole.InnerText = ResourceManager.GetStringWithoutName("ERR00052");
            //    spnLoc.InnerText = ResourceManager.GetStringWithoutName("ERR00037");
            //}

            //if (_uId == -1)
            //{
            //    chkActive.Checked = true;
            //    chkActive.Enabled = false;
            //}

            //if (_uId > 0)
            //{
            //    txtUserName.Enabled = false;
            //}
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

            //if (_uId == 0)
            //    Response.Redirect("~/Transaction/AddEditMoneyReceipts.aspx");

            //if (!_canView)
            //{
            //    Response.Redirect("~/Unauthorized.aspx");
            //}
        }

        //private bool IsSalesRole(int roleId)
        //{
        //    bool isSalesRole = false;

        //    if (roleId == (int)UserRole.SalesExecutive)
        //    {
        //        isSalesRole = true;
        //    }
        //    else
        //    {
        //        isSalesRole = false;
        //    }

        //    //IRole role = new CommonBLL().GetRole(roleId);
        //    //bool isSalesRole = false;

        //    //if (!ReferenceEquals(role, null))
        //    //{
        //    //    if (role.SalesRole.HasValue && role.SalesRole.Value == 'Y')
        //    //    {
        //    //        isSalesRole = true;
        //    //    }
        //    //}

        //    return isSalesRole;
        //}

        private void PopulateInvoiceTypes()
        {
            //MoneyReceiptsBLL mrBll = new MoneyReceiptsBLL();
            //List<InvoiceTypeEntity> lstInvoiceTypes = mrBll.GetInvoiceTypes();
            //GeneralFunctions.PopulateDropDownList(this.drpDwnInvoiceType, lstInvoiceTypes, "InvoiceTypeId", "InvoiceTypeName", true);
        }

        private void PopulateLocation()
        {
            //CommonBLL commonBll = new CommonBLL();
            //List<ILocation> lstLoc = commonBll.GetActiveLocation();
            //GeneralFunctions.PopulateDropDownList(ddlLoc, lstLoc, "Id", "Name", true);
        }

        private void LoadData()
        {
            MoneyReceiptsBLL mrBll = new MoneyReceiptsBLL();
            DataTable dt = mrBll.GetInvoiceDetailForMoneyReceipt(_InvoiceId);

            txtBLNo.Text = dt.Rows[0]["BLNO"].ToString();
            txtNvocc.Text = dt.Rows[0]["LINE"].ToString();
            txtLocation.Text = dt.Rows[0]["LOCATION"].ToString();
            ddlExportImport.SelectedValue = dt.Rows[0]["EXPIMP"].ToString();

            txtInvoiceNo.Text = dt.Rows[0]["INVNO"].ToString();
            txtInvoiceType.Text = dt.Rows[0]["DOCTYPE"].ToString();
            txtInvoiceDate.Text = Convert.ToDateTime(dt.Rows[0]["INVDT"].ToString()).ToString("dd/MM/yyyy");
            txtInvoiceAmount.Text = dt.Rows[0]["INVAMT"].ToString();




        }

        private bool ValidateControls(IUser user)
        {
            bool isValid = true;

            //if (user.Name == string.Empty)
            //{
            //    isValid = false;
            //    spnName.Style["display"] = "";
            //}

            //if (user.FirstName == string.Empty)
            //{
            //    isValid = false;
            //    spnFName.Style["display"] = "";
            //}

            //if (user.LastName == string.Empty)
            //{
            //    isValid = false;
            //    spnLName.Style["display"] = "";
            //}

            //if (user.EmailId == string.Empty)
            //{
            //    isValid = false;
            //    spnEmail.Style["display"] = "";
            //}

            //if (user.UserRole.Id == 0)
            //{
            //    isValid = false;
            //    spnRole.Style["display"] = "";
            //}
            //else
            //{
            //    if (user.UserRole.LocationSpecific.HasValue && user.UserRole.LocationSpecific.Value)
            //    {
            //        if (user.UserLocation.Id == 0)
            //        {
            //            isValid = false;
            //            spnLoc.Style["display"] = "";
            //        }
            //    }
            //}

            //if (user.UserLocation.Id == 0)
            //{
            //    isValid = false;
            //    spnLoc.Style["display"] = "";
            //}
            //else
            //{
            //    if(user.UserRole.LocationSpecific.Value)
            //        user.UserRole.
            //}

            return isValid;
        }

        private void SaveMoneyReceipts()
        {
            MoneyReceiptsBLL mrBll = new MoneyReceiptsBLL();
            MoneyReceiptEntity moneyReceipt = this.getMoneyReceiptObjectInstance(null);
            int returnVal = 0;
            List<MoneyReceiptEntity> moneyReceipts = BuildMoneyReceipts(moneyReceipt);
            if ((bool)Session["ISADDED"] == true)
            {
                moneyReceipts.ForEach(mr => mr.IsAdded = 1);
            }
            //if (ValidateControls(user))
            //{
            returnVal = mrBll.SaveMoneyReceipts(moneyReceipts);

            if (returnVal == 1)
            {
                //if (_uId == 0)
                //    SendEmail(user);

                Response.Redirect("~/Transaction/ManageMoneyReceipt.aspx");
            }
            else
            {
                GeneralFunctions.RegisterAlertScript(this, "Failed to save money receipt details.");
            }
            //}
        }

        private List<MoneyReceiptEntity> BuildMoneyReceipts(MoneyReceiptEntity moneyReceipt)
        {
            List<MoneyReceiptEntity> lstMoneyReceipts = new List<MoneyReceiptEntity>();

            if (moneyReceipt.ChargeDetails == null)
            {
                moneyReceipt.MRNo = this.txtMRNo.Text;
                //moneyReceipt.MRDate = this.dtPckrMRDate.dt;
                moneyReceipt.UserAddedId = _userId;
                moneyReceipt.UserEditedId = _userId;
                lstMoneyReceipts.Add(moneyReceipt);

            }
            else
            {
                foreach (ChargeDetails cd in moneyReceipt.ChargeDetails)
                {
                    MoneyReceiptEntity mrAdd = new MoneyReceiptEntity();
                    mrAdd.MRNo = this.txtMRNo.Text;
                    //mrAdd.MRDate = this.dtPckrMRDate.dt;
                    mrAdd.UserAddedId = _userId;
                    mrAdd.UserEditedId = _userId;
                    mrAdd.InvoiceId = cd.InvoiceId;
                    mrAdd.CashPayment = cd.CashAmount;
                    mrAdd.ChequePayment = cd.ChequeAmount;
                    mrAdd.TdsDeducted = cd.TDS;
                    mrAdd.ChequeDetails = cd.ChequeDetails;
                    lstMoneyReceipts.Add(mrAdd);
                }
            }

            return lstMoneyReceipts;
            //user.Id = _uId;
            //user.Name = txtUserName.Text.Trim().ToUpper();
            //user.Password = UserBLL.GetDefaultPassword();
            //user.FirstName = txtFName.Text.Trim().ToUpper();
            //user.LastName = txtLName.Text.Trim().ToUpper();
            //user.EmailId = txtEmail.Text.Trim().ToUpper();
            //user.UserRole.Id = Convert.ToInt32(ddlRole.SelectedValue);
            //user.UserLocation.Id = Convert.ToInt32(ddlLoc.SelectedValue);

            //IRole role = new UserBLL().GetRole(Convert.ToInt32(ddlRole.SelectedValue));

            //user.UserRole.LocationSpecific = false;

            //if (!ReferenceEquals(role, null))
            //{
            //    if (role.LocationSpecific.HasValue && role.LocationSpecific.Value)
            //    {
            //        user.UserRole.LocationSpecific = true;
            //    }
            //}

            //if (ddlMultiLoc.SelectedValue == "1")
            //    user.AllowMutipleLocation = true;
            //else
            //    user.AllowMutipleLocation = false;

            //if (chkActive.Checked)
            //    user.IsActive = true;
            //else
            //    user.IsActive = false;
        }

        #endregion

        protected void drpDwnInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if ((sender as DropDownList) != null)
            //{
            //    int invoiceTypeId = Convert.ToInt32((sender as DropDownList).SelectedValue);
            //    MoneyReceiptsBLL mrBll = new MoneyReceiptsBLL();
            //    List<InvoiceDetailsEntity> lstInvoiceDetails = mrBll.GetInvoiceDetails(invoiceTypeId);
            //    GeneralFunctions.PopulateDropDownList(this.drpDwnInvoiceNo, lstInvoiceDetails, "InvoiceId", "InvoiceNo", true);
            //    if (Session["CHARGEDETAILS"] != null)
            //    {
            //        this.bindGridData(Session["CHARGEDETAILS"] as List<ChargeDetails>);

            //    }
            //    //this.populateChargeDetailsGrid(true);
            //}
        }

        protected void drpDwnInvoiceNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as DropDownList) != null)
            {
                //int invoiceTypeId = Convert.ToInt32(this.drpDwnInvoiceType.SelectedValue);
                long invoiceId = long.Parse((sender as DropDownList).SelectedValue);
                //this.populateInvoiceData(invoiceTypeId, invoiceId);
                if (Session["CHARGEDETAILS"] != null)
                {
                    //this.bindGridData(Session["CHARGEDETAILS"] as List<ChargeDetails>);

                }
                //this.populateChargeDetailsGrid(true);
            }
        }

        private void populateInvoiceData(int invoiceTypeId, long invoiceId)
        {
            MoneyReceiptsBLL mrBll = new MoneyReceiptsBLL();
            List<InvoiceDetailsEntity> lstInvoiceDetails = mrBll.GetInvoiceDetails(invoiceTypeId);
            this.txtInvoiceDate.Text = lstInvoiceDetails.FirstOrDefault(ind => ind.InvoiceId == invoiceId).InvoiceDate.ToString();
            this.txtInvoiceAmount.Text = lstInvoiceDetails.FirstOrDefault(ind => ind.InvoiceId == invoiceId).InvoiceAmount.ToString();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //this.populateChargeDetailsGrid(false);
            //this.clearChargeDetails();
        }


        private void clearChargeDetails()
        {             //this.drpDwnInvoiceType.SelectedIndex = 0;
            this.txtInvoiceDate.Text = string.Empty;
            this.txtInvoiceAmount.Text = "0";
            this.txtTDS.Text = "0";
            this.txtCurrentAmount.Text = "0";
        }

        protected void txtBLNo_TextChanged(object sender, EventArgs e)
        {
            MoneyReceiptsBLL mrBll = new MoneyReceiptsBLL();
            try
            {
                //Get BL No from sender and populate location.
                string blNo = (sender as TextBox).Text.Trim();
                BLInformations blHeader = mrBll.GetBLInformation(blNo);
                this.txtLocation.Text = blHeader.LocationName;
                this.txtNvocc.Text = blHeader.NVOCCName;
                this.ddlExportImport.SelectedIndex = (blHeader.ExportImport + 1);
            }
            catch
            {
                // Do nothing.
            }

        }

        protected void txtCashAmount_TextChanged(object sender, EventArgs e)
        {
            // Update Current Amount.
            //this.populateCurrentAndReceivedAmount(sender);
        }

        protected void txtTDS_TextChanged(object sender, EventArgs e)
        {
            //  Update Current Amount.
            //this.populateCurrentAndReceivedAmount(sender);
        }

        protected void txtChequeAmount_TextChanged(object sender, EventArgs e)
        {
            // Update Current Amount.
            //this.populateCurrentAndReceivedAmount(sender);
        }

        protected void txtCurrentAmount_TextChanged(object sender, EventArgs e)
        {
            // Update  received amount.
        }



        protected void gvwAddEditMoneyReceipts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // do nothing.
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            //Response.Redirect("");
        }

        protected void txtReceivedAmount_TextChanged(object sender, EventArgs e)
        {

        }
    }
}