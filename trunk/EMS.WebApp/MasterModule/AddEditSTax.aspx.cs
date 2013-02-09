using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Common;
using EMS.Utilities;
using EMS.Utilities.ResourceManager;

namespace EMS.WebApp.MasterModule
{
    public partial class AddEditSTax : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private bool _hasEditAccess = true;
        private string ServTaxId = "";
        #endregion


        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            _userId = EMS.BLL.UserBLL.GetLoggedInUserId();

            if (!IsPostBack)
            {
                ServTaxId = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);
                ClearText();
                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageServTax.aspx','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
                if (ServTaxId == "-1")
                {
                    txtStDate.Visible = false;
                    ddlStatus.Enabled = false;
                }
                else
                {
                    DatePicker1.Visible = false;
                    LoadData(ServTaxId);
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ServTaxId = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);
            SaveData(ServTaxId);

        }

        #endregion

        #region Private Methods

        private void LoadData(string ServTaxId)
        {
            ClearText();

            BLL.DBInteraction dbinteract = new BLL.DBInteraction();




            int intServTaxId = 0;
            if (ServTaxId == "" || !Int32.TryParse(ServTaxId, out intServTaxId))
                return;

            System.Data.DataSet ds = dbinteract.GetSTax(Convert.ToInt32(ServTaxId), null);

            if (!ReferenceEquals(ds, null) && ds.Tables[0].Rows.Count > 0)
            {
                txtTax.Text = ds.Tables[0].Rows[0]["TaxPer"].ToString();
                txtAddiCess.Text = Convert.ToString(ds.Tables[0].Rows[0]["TaxAddCess"]);
                txtCess.Text = Convert.ToString(ds.Tables[0].Rows[0]["TaxCess"]);
                ddlStatus.SelectedIndex = Convert.ToString(ds.Tables[0].Rows[0]["Status"]) == "Active" ? 0 : 1;
                txtStDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["StartDate"]).Split(' ')[0];

            }
        }
        private void ClearText()
        {
            txtTax.Text = "";
            txtAddiCess.Text = "";
            txtCess.Text = "";
            txtStDate.Text = "";
        }
        private void SaveData(string ServTaxId)
        {
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            DateTime maxDate = Convert.ToDateTime(GeneralFunctions.DecryptQueryString(Request.QueryString["dt"]));


            if (ServTaxId == "-1")
            {
                txtStDate.Text = DatePicker1.dt.ToShortDateString();
                if (DatePicker1.dt.CompareTo(maxDate) < 0)
                {

                    GeneralFunctions.RegisterAlertScript(this, "Start Date must be greater than: " + maxDate.ToShortDateString());
                    return;
                }
            }
            else
            {
                if (!dbinteract.IsUnique("finInvoice", "InvoiceDate", txtStDate.Text) )
                {
                    GeneralFunctions.RegisterAlertScript(this, "This Date has been already used for Invoice. It can not be edited");
                    return;
                }

            }



            bool isedit = ServTaxId != "-1" ? true : false;

            int result = dbinteract.AddEditSTax(_userId, Convert.ToInt32(ServTaxId),Convert.ToDateTime(txtStDate.Text), ExtentionClass.TryParseBlankAsZero(txtAddiCess.Text), ExtentionClass.TryParseBlankAsZero(txtCess.Text), ExtentionClass.TryParseBlankAsZero(txtTax.Text), ddlStatus.SelectedIndex == 0 ? true : false, isedit);


            if (result > 0)
            {
                Response.Redirect("~/MasterModule/ManageServTax.aspx");
            }
            else
            {
                GeneralFunctions.RegisterAlertScript(this, "Error Occured");
            }
        }


        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }

    }
}