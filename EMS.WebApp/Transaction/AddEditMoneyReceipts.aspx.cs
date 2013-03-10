using System;
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
                PopulateInvoiceTypes();
                PopulateLocation();
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

            if (!ReferenceEquals(Request.QueryString["id"], null))
            {
                Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _uId);
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
            //if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
            //{
            //    IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

            //    if (ReferenceEquals(user, null) || user.Id == 0)
            //    {
            //        Response.Redirect("~/Login.aspx");
            //    }

            //    if (user.UserRole.Id != (int)UserRole.Admin)
            //    {
            //        Response.Redirect("~/Unauthorized.aspx");
            //    }
            //}
            //else
            //{
            //    Response.Redirect("~/Login.aspx");
            //}

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
            MoneyReceiptsBLL mrBll = new MoneyReceiptsBLL();
            List<InvoiceTypeEntity> lstInvoiceTypes = mrBll.GetInvoiceTypes();
            GeneralFunctions.PopulateDropDownList(this.drpDwnInvoiceType, lstInvoiceTypes, "InvoiceTypeId", "InvoiceTypeName", true);
        }

        private void PopulateLocation()
        {
            //CommonBLL commonBll = new CommonBLL();
            //List<ILocation> lstLoc = commonBll.GetActiveLocation();
            //GeneralFunctions.PopulateDropDownList(ddlLoc, lstLoc, "Id", "Name", true);
        }

        private void LoadData()
        {
            mrSession = this.getMoneyReceiptObjectInstance(null);
            if (mrSession.MRNo != null)
            {
                this.txtMRNo.Text = mrSession.MRNo;
                this.dtPckrMRDate.DBDate = mrSession.MRDate.ToString();
                this.txtBLNo.Text = mrSession.BLNo;
                this.txtBLNo_TextChanged(this.txtBLNo, null);
                this.drpDwnInvoiceType.Items.FindByValue(mrSession.InvoiceTypeId.ToString()).Selected = true;
                this.drpDwnInvoiceType_SelectedIndexChanged(this.drpDwnInvoiceType, null);
                this.drpDwnInvoiceNo.Items.FindByText(mrSession.InvoiceNo.ToString()).Selected = true;
                this.populateInvoiceData(mrSession.InvoiceTypeId, mrSession.InvoiceId);




                mrSession.ChargeDetails.ForEach(cd =>
                    {
                        MoneyReceiptsBLL mrBll = new MoneyReceiptsBLL();
                        cd.InvoiceTypeName = mrBll.GetInvoiceTypes().FirstOrDefault(inv => inv.InvoiceTypeId == cd.InvoiceTypeId).InvoiceTypeName;
                    });

                this.bindGridData(mrSession.ChargeDetails);
            }
            else
            {
                Session["ISADDED"] = true;
            }
            //IUser user = new UserBLL().GetUser(_uId);

            //if (!ReferenceEquals(user, null))
            //{
            //    txtUserName.Text = user.Name;
            //    txtFName.Text = user.FirstName;
            //    txtLName.Text = user.LastName;
            //    txtEmail.Text = user.EmailId;
            //    ddlRole.SelectedValue = Convert.ToString(user.UserRole.Id);
            //    ddlLoc.SelectedValue = Convert.ToString(user.UserLocation.Id);

            //    if (user.AllowMutipleLocation)
            //        ddlMultiLoc.SelectedValue = "1";
            //    else
            //        ddlMultiLoc.SelectedValue = "0";

            //    if (user.IsActive)
            //        chkActive.Checked = true;
            //    else
            //        chkActive.Checked = false;

            //    if (_uId == 1)
            //        chkActive.Enabled = false;
            //}
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
                moneyReceipt.MRDate = this.dtPckrMRDate.dt;
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
                    mrAdd.MRDate = this.dtPckrMRDate.dt;
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
            if ((sender as DropDownList) != null)
            {
                int invoiceTypeId = Convert.ToInt32((sender as DropDownList).SelectedValue);
                MoneyReceiptsBLL mrBll = new MoneyReceiptsBLL();
                List<InvoiceDetailsEntity> lstInvoiceDetails = mrBll.GetInvoiceDetails(invoiceTypeId);
                GeneralFunctions.PopulateDropDownList(this.drpDwnInvoiceNo, lstInvoiceDetails, "InvoiceId", "InvoiceNo", true);
                if (Session["CHARGEDETAILS"] != null)
                {
                    this.bindGridData(Session["CHARGEDETAILS"] as List<ChargeDetails>);
                 
                }
                //this.populateChargeDetailsGrid(true);
            }
        }

        protected void drpDwnInvoiceNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as DropDownList) != null)
            {
                int invoiceTypeId = Convert.ToInt32(this.drpDwnInvoiceType.SelectedValue);
                long invoiceId = long.Parse((sender as DropDownList).SelectedValue);
                this.populateInvoiceData(invoiceTypeId, invoiceId);
                if (Session["CHARGEDETAILS"] != null)
                {
                    this.bindGridData(Session["CHARGEDETAILS"] as List<ChargeDetails>);

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
            this.populateChargeDetailsGrid(false);
            this.clearChargeDetails();
        }

        private void populateChargeDetailsGrid(bool selectionChanged)
        {
            mrSession = this.getMoneyReceiptObjectInstance(null);
            if (mrSession.ChargeDetails == null)
            {
                if (selectionChanged) return;
                mrSession.ChargeDetails = new List<ChargeDetails>();
            }

            int id = 0;
            foreach (GridViewRow row in this.gvwAddEditMoneyReceipts.Rows)
            {
                if (!string.IsNullOrEmpty(row.Cells[0].Text))
                {
                    if (Convert.ToInt32(row.Cells[0].Text) > id)
                    {
                        id = Convert.ToInt32(row.Cells[0].Text);
                    }
                }
            }

            ChargeDetails chargeDetails = new ChargeDetails();
            chargeDetails.ID = id; //((gvwAddEditMoneyReceipts.PageSize * gvwAddEditMoneyReceipts.PageIndex) + e.Row.RowIndex + 1).ToString();
            chargeDetails.InvoiceId = long.Parse(this.drpDwnInvoiceNo.SelectedValue);
            chargeDetails.InvoiceNo = this.drpDwnInvoiceNo.SelectedItem.Text;
            chargeDetails.InvoiceTypeId = Convert.ToInt32(this.drpDwnInvoiceType.SelectedValue);
            chargeDetails.InvoiceTypeName = this.drpDwnInvoiceType.SelectedItem.Text;
            if(!string.IsNullOrEmpty(this.txtInvoiceDate.Text))
            chargeDetails.InvoiceDate = Convert.ToDateTime(this.txtInvoiceDate.Text.Trim());
            chargeDetails.InvoiceAmount = Convert.ToDecimal(string.IsNullOrEmpty(this.txtInvoiceAmount.Text.Trim()) ? "0" : this.txtInvoiceAmount.Text.Trim());
            chargeDetails.ReceivedAmount = Convert.ToDecimal(string.IsNullOrEmpty(this.txtReceivedAmount.Text.Trim()) ? "0" : this.txtReceivedAmount.Text.Trim());
            chargeDetails.TDS = Convert.ToDecimal(this.txtTDS.Text);
            chargeDetails.CashAmount = Convert.ToDecimal(string.IsNullOrEmpty(this.txtCashAmount.Text.Trim()) ? "0" : this.txtCashAmount.Text.Trim());
            chargeDetails.ChequeAmount = Convert.ToDecimal(string.IsNullOrEmpty(this.txtChequeAmount.Text.Trim()) ? "0" : this.txtChequeAmount.Text.Trim());
            chargeDetails.CurrentReceivedAmount = chargeDetails.TDS + chargeDetails.CashAmount + chargeDetails.CurrentReceivedAmount;
            chargeDetails.ChequeDetails = this.txtChequeDetails.Text;
            mrSession.ChargeDetails.Add(chargeDetails);

            this.bindGridData(mrSession.ChargeDetails);
           

           
        }

        private void bindGridData(List<ChargeDetails> lstChargeDetails)
        {
            this.gvwAddEditMoneyReceipts.DataSource = null;
            this.gvwAddEditMoneyReceipts.DataSource = lstChargeDetails;
            this.gvwAddEditMoneyReceipts.DataBind();
            Session["CHARGEDETAILS"] = lstChargeDetails;

        }


        private void clearChargeDetails()
        {
            this.drpDwnInvoiceNo.SelectedIndex = 0;
            this.drpDwnInvoiceType.SelectedIndex = 0;
            this.txtInvoiceDate.Text = string.Empty;
            this.txtInvoiceAmount.Text = "0";
            this.populateCurrentAndReceivedAmount(null);
            this.txtTDS.Text = "0";
            this.txtCashAmount.Text = "0";
            this.txtChequeAmount.Text = "0";
            this.txtCurrentAmount.Text = "0";
            this.txtChequeDetails.Text = string.Empty;
        }

        protected void gvwAddEditMoneyReceipts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 8);

                ScriptManager sManager = ScriptManager.GetCurrent(this);
                string slNo = ((gvwAddEditMoneyReceipts.PageSize * gvwAddEditMoneyReceipts.PageIndex) + e.Row.RowIndex + 1).ToString();
                e.Row.Cells[0].Text = slNo;//Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ID")); ;
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InvoiceTypeName"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InvoiceNo"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InvoiceDate"));
                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InvoiceAmount"));
                e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ReceivedAmount"));
                e.Row.Cells[6].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ReceivedAmount"));
                e.Row.Cells[7].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TDS"));
                e.Row.Cells[8].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "CashAmount"));
                e.Row.Cells[9].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ChequeAmount"));
                e.Row.Cells[10].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ChequeDetails"));
                (e.Row.DataItem as ChargeDetails).ID = Convert.ToInt32(slNo);

                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.ToolTip = ResourceManager.GetStringWithoutName("ERR00013");
                btnEdit.CommandArgument = slNo;// Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ID"));

                //Delete link
                ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00012");
                btnRemove.CommandArgument = slNo;// Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ID")); 

            }
        }

        protected void gvwAddEditMoneyReceipts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                // ChargeDetails chargeDetail = null;
                foreach (GridViewRow row in (sender as GridView).Rows)
                {
                    if (row.Cells[0].Text == e.CommandArgument.ToString())
                    {
                        this.drpDwnInvoiceType.Items.FindByText(row.Cells[1].Text).Selected = true; //chargeDetail.InvoiceTypeId.ToString();
                        this.drpDwnInvoiceType_SelectedIndexChanged(this.drpDwnInvoiceType, null);
                        if(this.drpDwnInvoiceNo.Items.FindByText(row.Cells[2].Text) != null)
                        this.drpDwnInvoiceNo.Items.FindByText(row.Cells[2].Text).Selected = true;
                        this.txtInvoiceDate.Text = row.Cells[3].Text;
                        this.txtInvoiceAmount.Text = row.Cells[4].Text;
                        this.txtReceivedAmount.Text = row.Cells[5].Text;
                        this.txtCurrentAmount.Text = row.Cells[6].Text;
                        this.txtTDS.Text = row.Cells[7].Text;
                        this.txtCashAmount.Text = row.Cells[8].Text;
                        this.txtChequeAmount.Text = row.Cells[9].Text;
                        this.txtChequeDetails.Text = row.Cells[7].Text;
                        if (Session["CHARGEDETAILS"] != null)
                        {
                            this.bindGridData(Session["CHARGEDETAILS"] as List<ChargeDetails>);

                        }
                        //chargeDetail = row.DataItem as ChargeDetails;
                    }

                }
                // ChargeDetails chargeDetail = (sender as GridView).Rows.Cells[0] as ChargeDetails;


            }
            else if (e.CommandName == "Remove")
            {
                if (Session["CHARGEDETAILS"] != null)
                {
                    List<ChargeDetails> lstChargeDetails = Session["CHARGEDETAILS"] as List<ChargeDetails>;
                    lstChargeDetails.Remove(lstChargeDetails.FirstOrDefault(cd=>cd.ID==Convert.ToInt32(e.CommandArgument)));
                    this.bindGridData(lstChargeDetails);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00010") + "');</script>", false);

                }
                //GridViewRow rowDeleted = null;
                //foreach (GridViewRow row in (sender as GridView).Rows)
                //{
                //    if (row.Cells[0].Text == e.CommandArgument.ToString())
                //    {
                //        rowDeleted = row;
                //    }
                //}

                //(sender as GridView).DeleteRow(rowDeleted.RowIndex);
            }
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
            this.populateCurrentAndReceivedAmount(sender);
        }

        protected void txtTDS_TextChanged(object sender, EventArgs e)
        {
            //  Update Current Amount.
            this.populateCurrentAndReceivedAmount(sender);
        }

        protected void txtChequeAmount_TextChanged(object sender, EventArgs e)
        {
            // Update Current Amount.
            this.populateCurrentAndReceivedAmount(sender);
        }

        protected void txtCurrentAmount_TextChanged(object sender, EventArgs e)
        {
            // Update  received amount.
        }

        private void populateCurrentAndReceivedAmount(object sender)
        {
            if (sender != null)
                this.txtCurrentAmount.Text = (Convert.ToDecimal(this.txtCurrentAmount.Text) + Convert.ToDecimal((sender as TextBox).Text)).ToString();

            mrSession = this.getMoneyReceiptObjectInstance(null);
            if (mrSession.ChargeDetails != null)
                this.txtReceivedAmount.Text = (mrSession.ChargeDetails.Sum(cd => cd.CurrentReceivedAmount) + (Convert.ToDecimal(this.txtCurrentAmount.Text))).ToString();
            else
                this.txtReceivedAmount.Text = (Convert.ToDecimal(this.txtCurrentAmount.Text)).ToString();
        }

        protected void gvwAddEditMoneyReceipts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // do nothing.
        }
    }
}