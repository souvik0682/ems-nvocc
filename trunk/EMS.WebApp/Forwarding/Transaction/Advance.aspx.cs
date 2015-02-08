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
using System.Data;

namespace EMS.WebApp.Forwarding.Transaction
{
    public partial class Advance : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private int _uId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        MoneyReceiptEntity mrSession = null;
        private int _AdvanceId = 0;
        private int _jobid = 0;
        private string _PaymentType = "";
        #endregion

        #region Protected Event Handler
        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();

            if (!IsPostBack)
            {
                if (_PaymentType == "C")
                    litHeader.Text = "ADD / EDIT ADVACE PAID";
                else
                    litHeader.Text = "ADD / EDIT ADVACE RECEIVED";
                string strProcessScript = "this.value='Processing...';this.disabled=true;";
                btnSave.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnSave, "").ToString());
                LoadPartyTypeDDl();
                LoadData();
            }
        }

        private void LoadData()
        {
            MoneyReceiptsBLL mrBll = new MoneyReceiptsBLL();
            int PartyId;
            DataTable dt;
            if (_AdvanceId == 0)
            {
                dt = mrBll.GetInvoiceDetailForAdvance(_AdvanceId, _jobid, _PaymentType);

                txtDate.Text = DateTime.Now.ToShortDateString();
                txtJobNo.Text = dt.Rows[0]["JOBNO"].ToString();
                txtJobDate.Text = Convert.ToDateTime(dt.Rows[0]["JOBDATE"].ToString()).ToString("dd/MM/yyyy");
                hdnJobID.Value = dt.Rows[0]["pk_JobID"].ToString();
            }
            else
            {
                dt = mrBll.GetAdvanceforEdit(_AdvanceId, _jobid, _PaymentType);
                txtChqNo.Text = dt.Rows[0]["ChequeNo"].ToString();
                txtChqDate.Text = dt.Rows[0]["ChequeDate"].ToString().Split(' ')[0];
                //ddlParty.SelectedValue = dt.Rows[0]["fk_PartyID"].ToString();
                ddlPartyType.SelectedValue = dt.Rows[0]["fk_PartyTypeID"].ToString();
                txtJobNo.Text = dt.Rows[0]["JobNo"].ToString();
                txtChequeAmt.Text = dt.Rows[0]["ChequePayment"].ToString();
                txtCashAmt.Text = dt.Rows[0]["CashPayment"].ToString();
                txtBankName.Text = dt.Rows[0]["ChequeBank"].ToString();
                txtMRNo.Text = dt.Rows[0]["pmtReference"].ToString();
                PartyId = dt.Rows[0]["fk_PartyID"].ToInt();
                txtDate.Text = dt.Rows[0]["pmtDate"].ToString().Split(' ')[0];
                txtJobDate.Text = dt.Rows[0]["JobDate"].ToString().Split(' ')[0];
                txtCurrentAmount.Text = (txtCashAmt.Text.ToDecimal() + txtChequeAmt.Text.ToDecimal()).ToString();
                hdnJobID.Value = dt.Rows[0]["fk_JobID"].ToString();
                GetPartyValuesSetToDdl(ddlPartyType.SelectedValue.ToInt());
                ddlParty.SelectedValue = PartyId.ToString();
            }
            //if (_PaymentType == "C")
            //{
            //    ddlParty.SelectedValue = Convert.ToString(dt.Rows[0]["fk_CreditorID"]);
            //    ddlParty.Enabled = false;
            //}

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            decimal Cash = 0, Chq = 0, Tds = 0;

            if (!String.IsNullOrEmpty(txtCashAmt.Text))
                Cash = Convert.ToDecimal(txtCashAmt.Text);

            if (!String.IsNullOrEmpty(txtChequeAmt.Text))
                Chq = Convert.ToDecimal(txtChequeAmt.Text);

            if (!String.IsNullOrEmpty(txtTDS.Text))
                Tds = Convert.ToDecimal(txtTDS.Text);

            SaveMoneyReceipts();

        }

        #endregion

        #region Private Methods

        //private void GetPartyValuesSetToDdl()
        //{
        //    //ddlParty.Items.Clear();
        //    //var creditorInvoice = new CreditorInvoiceBLL().GetCreditor((ISearchCriteria)null);

        //    var creditorInvoice = new EstimateBLL().GetAllParty(0);
        //    ddlParty.DataSource = creditorInvoice;
        //    ddlParty.DataTextField = "PartyName";
        //    ddlParty.DataValueField = "pk_fwPartyID";
        //    ddlParty.DataBind();
        //    ddlParty.Items.Insert(0, new ListItem("--Select--", "0"));
        //}

        private void RetriveParameters()
        {
            _userId = MoneyReceiptsBLL.GetLoggedInUserId();

            //Get user permission.
            MoneyReceiptsBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);

            //if (!ReferenceEquals(Request.QueryString["invid"], null))
            //{
            if (Request.QueryString["AdvanceID"] != null)
                Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["AdvanceID"].ToString()), out _AdvanceId);
            Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["jobid"].ToString()), out _jobid);
            _PaymentType = GeneralFunctions.DecryptQueryString(Request.QueryString["paymenttype"].ToString());
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

        private void SaveMoneyReceipts()
        {
            MoneyReceiptsBLL mrBll = new MoneyReceiptsBLL();
            MoneyReceiptEntity moneyReceipt = new MoneyReceiptEntity();
            //int returnVal = 0;

            moneyReceipt.MoneyReceiptId = Convert.ToInt64(_AdvanceId);
            //moneyReceipt.LocationId = Convert.ToInt32(hdnLocationID.Value);
            //moneyReceipt.NvoccId = Convert.ToInt32(hdnNvoccId.Value);
            //moneyReceipt.ExportImport = Convert.ToChar(ddlExportImport.SelectedValue);
            moneyReceipt.MRDate = Convert.ToDateTime(txtDate.Text);
            moneyReceipt.ChequeNo = txtChqNo.Text;
            moneyReceipt.ChequeBank = txtBankName.Text.ToUpper();
            moneyReceipt.CHA = _PaymentType;
            if (!string.IsNullOrEmpty(txtChqDate.Text))
                moneyReceipt.ChequeDate = Convert.ToDateTime(txtChqDate.Text);

            if (!string.IsNullOrEmpty(txtCashAmt.Text))
                moneyReceipt.CashPayment = Convert.ToDecimal(txtCashAmt.Text);

            if (!string.IsNullOrEmpty(txtChequeAmt.Text))
                moneyReceipt.ChequePayment = Convert.ToDecimal(txtChequeAmt.Text);

            if (!string.IsNullOrEmpty(txtTDS.Text))
                moneyReceipt.TdsDeducted = Convert.ToDecimal(txtTDS.Text);

            Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["jobid"].ToString()), out _jobid);
            moneyReceipt.BLId = hdnJobID.Value.ToInt();
            moneyReceipt.CREID = ddlParty.SelectedValue.ToInt();
            moneyReceipt.NvoccId = ddlPartyType.SelectedValue.ToInt();
            moneyReceipt.UserAddedId = _userId;
            moneyReceipt.UserEditedId = _userId;
            moneyReceipt.UserAddedOn = DateTime.Now.Date;
            moneyReceipt.UserEditedOn = DateTime.Now.Date;
            moneyReceipt.Status = true;

            switch (mrBll.SaveAdvance(moneyReceipt))
            {
                case 0: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00011");
                    break;
                case 1:
                    if (moneyReceipt.MoneyReceiptId == 0)
                        Response.Redirect("~/Forwarding/Transaction/Dashboard.aspx?JobId=" + GeneralFunctions.EncryptQueryString(_jobid.ToString()));
                    else
                        Response.Redirect("~/Forwarding/Transaction/ManageAdvanceAdjustment.aspx");
                    break;
            }
        }

        protected void ddlPartyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPartyValuesSetToDdl(ddlPartyType.SelectedValue.ToInt());
        }

        private void GetPartyValuesSetToDdl(int PartyType)
        {
            ddlParty.Items.Clear();

            DataSet dll = new DataSet();

            dll = new EstimateBLL().GetAllParty(PartyType);

            ddlParty.DataSource = dll;
            ddlParty.DataTextField = "PartyName";
            ddlParty.DataValueField = "pk_fwPartyID";
            ddlParty.DataBind();
            ddlParty.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void LoadPartyTypeDDl()
        {
            var partyType = new EstimateBLL().GetBillingGroupMaster((ISearchCriteria)null);
            ddlPartyType.DataSource = partyType;

            ddlPartyType.DataTextField = "PartyType";
            ddlPartyType.DataValueField = "pk_PartyTypeID";
            ddlPartyType.DataBind();
            ddlPartyType.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (_AdvanceId == 0)
                Response.Redirect("~/Forwarding/Transaction/Dashboard.aspx?JobId=" + GeneralFunctions.EncryptQueryString(_jobid.ToString()));
            else
                Response.Redirect("~/Forwarding/Transaction/ManageAdvanceAdjustment.aspx");
        }
    }
}