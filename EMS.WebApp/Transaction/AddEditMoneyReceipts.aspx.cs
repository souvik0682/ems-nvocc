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
            //CheckUserAccess();


            if (!IsPostBack)
            {
                string strProcessScript = "this.value='Processing...';this.disabled=true;";
                btnSave.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnSave, "").ToString());

                LoadData();
            }

        }

        //private MoneyReceiptEntity getMoneyReceiptObjectInstance(MoneyReceiptEntity moneyReceipt)
        //{
        //    MoneyReceiptEntity moneyReceiptInstance = null;

        //    if (moneyReceipt == null)
        //    {
        //        moneyReceiptInstance = Session["MROBJECT"] as MoneyReceiptEntity;
        //        if (moneyReceiptInstance == null)
        //        {
        //            moneyReceiptInstance = new MoneyReceiptEntity();
        //            moneyReceiptInstance.IsAdded = 1;
        //            moneyReceiptInstance.IsEdited = 1;
        //            Session["MROBJECT"] = moneyReceiptInstance;
        //        }
        //    }
        //    else
        //    {
        //        moneyReceiptInstance = moneyReceipt;
        //        Session["MROBJECT"] = moneyReceiptInstance;
        //    }

        //    return moneyReceiptInstance;

        //}

        protected void btnSave_Click(object sender, EventArgs e)
        {
            decimal Cash = 0, Chq=0, Tds=0;

            if (!String.IsNullOrEmpty(txtCashAmt.Text))
                Cash = Convert.ToDecimal(txtCashAmt.Text);

            if (!String.IsNullOrEmpty(txtChequeAmt.Text))
                Chq = Convert.ToDecimal(txtChequeAmt.Text);

            if (!String.IsNullOrEmpty(txtTDS.Text))
                Tds = Convert.ToDecimal(txtTDS.Text);

            if (Convert.ToDecimal(txtPendingAmt.Text) >= (Cash + Chq + Tds))
                SaveMoneyReceipts();
            else
            {                
                Label lbl = new Label();
                lbl.Text = "<script type='text/javascript'>alert('Receipt Amount can not be more than O/s Amount');</script>";
                Page.Controls.Add(lbl);
            }
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

        private void LoadData()
        {
            MoneyReceiptsBLL mrBll = new MoneyReceiptsBLL();
            DataTable dt = mrBll.GetInvoiceDetailForMoneyReceipt(_InvoiceId);

            txtDate.Text = DateTime.Now.ToShortDateString();
            txtBLNo.Text = dt.Rows[0]["BLNO"].ToString();
            txtNvocc.Text = dt.Rows[0]["LINE"].ToString();
            txtLocation.Text = dt.Rows[0]["LOCATION"].ToString();
            ddlExportImport.SelectedValue = dt.Rows[0]["EXPIMP"].ToString();

            txtInvoiceNo.Text = dt.Rows[0]["INVNO"].ToString();
            txtInvoiceType.Text = dt.Rows[0]["DOCTYPE"].ToString();
            txtInvoiceDate.Text = Convert.ToDateTime(dt.Rows[0]["INVDT"].ToString()).ToString("dd/MM/yyyy");
            hdnInvDt.Value = Convert.ToDateTime(dt.Rows[0]["INVDT"].ToString()).ToString("MM/dd/yyyy");
            txtInvoiceAmount.Text = dt.Rows[0]["INVAMT"].ToString();
            txtPendingAmt.Text = dt.Rows[0]["PENDING"].ToString();

            hdnLocationID.Value = dt.Rows[0]["LOCID"].ToString();
            hdnNvoccId.Value = dt.Rows[0]["LINEID"].ToString();
        }

        private void SaveMoneyReceipts()
        {
            MoneyReceiptsBLL mrBll = new MoneyReceiptsBLL();
            MoneyReceiptEntity moneyReceipt = new MoneyReceiptEntity();
            int returnVal = 0;

            moneyReceipt.InvoiceId = Convert.ToInt64(_InvoiceId);
            moneyReceipt.LocationId = Convert.ToInt32(hdnLocationID.Value);
            moneyReceipt.NvoccId = Convert.ToInt32(hdnNvoccId.Value);
            moneyReceipt.ExportImport = Convert.ToChar(ddlExportImport.SelectedValue);
            moneyReceipt.MRDate = Convert.ToDateTime(txtDate.Text);
            moneyReceipt.ChequeNo = txtChqNo.Text;
            moneyReceipt.ChequeBank = txtBankName.Text.ToUpper();
            if (!string.IsNullOrEmpty(txtChqDate.Text))
                moneyReceipt.ChequeDate = Convert.ToDateTime(txtChqDate.Text);

            if (!string.IsNullOrEmpty(txtCashAmt.Text))
                moneyReceipt.CashPayment = Convert.ToDecimal(txtCashAmt.Text);

            if (!string.IsNullOrEmpty(txtChequeAmt.Text))
                moneyReceipt.ChequePayment = Convert.ToDecimal(txtChequeAmt.Text);

            if (!string.IsNullOrEmpty(txtTDS.Text))
                moneyReceipt.TdsDeducted = Convert.ToDecimal(txtTDS.Text);

            moneyReceipt.UserAddedId = _userId;
            moneyReceipt.UserEditedId = _userId;
            moneyReceipt.UserAddedOn = DateTime.Now.Date;
            moneyReceipt.UserEditedOn = DateTime.Now.Date;
            moneyReceipt.Status = true;

            switch (mrBll.SaveMoneyReceipt(moneyReceipt))
            {
                case 0: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00011");
                    break;
                case 1:
                    Response.Redirect("~/Transaction/BL-Query.aspx?BlNo=" + GeneralFunctions.EncryptQueryString(txtBLNo.Text));
                    break;
            }

        }



        #endregion



        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Transaction/BL-Query.aspx?BlNo=" + GeneralFunctions.EncryptQueryString(txtBLNo.Text));
        }

    }
}