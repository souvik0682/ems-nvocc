using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EMS.Utilities;
using EMS.BLL;
using System.Configuration;
using EMS.Utilities.ResourceManager;
using EMS.Entity;
using EMS.Common;
using System.Globalization;
using Microsoft.Win32;
using System.IO;


namespace EMS.WebApp.Forwarding.Transaction
{
     public partial class FwdInvoice : System.Web.UI.Page
    {
        List<ICharge> Charges = null;
        List<IChargeRate> ChargeRates = null;

        private int _userId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private int _isedit = 0;

         protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();

            if (!Page.IsPostBack)
            {
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "setMinDateOnSecondControl", "function setMinDateOnSecondControl(e) { document.getElementById('" + txtInvoiceDate.ClientID + "').dateSelectionChanged += function x(e) { alert(e);} ; }", true);
                string strProcessScript = "this.value='Processing...';this.disabled=true;";
                btnSave.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnSave, "").ToString());
                FillCurrency();
                Session["CHARGES"] = null;

                //LoadInvoiceTypeDDL();
                LoadLocationDDL();
                //LoadPartyDDL();
                //LoadNvoccDDL();

                if (!ReferenceEquals(Request.QueryString["invid"], null))
                {
                    long invoiveId = 0;
                    Int64.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["invid"].ToString()), out invoiveId);
                    LoadPartyTypeDDl();
                    _isedit = 1;
                    if (invoiveId > 0)
                        LoadForEdit(invoiveId);
                }
                else
                //if (!ReferenceEquals(Request.QueryString["jobNo"], null))
                {
                    int docTypeId = 0;
                    _isedit = 0;

                    //Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["docTypeId"].ToString()), out docTypeId);
                    string JobNo = GeneralFunctions.DecryptQueryString(Request.QueryString["jobNo"].ToString());
                    //string EstimateId = GeneralFunctions.DecryptQueryString(Request.QueryString["estimateId"].ToString());
                    //string Containers = GeneralFunctions.DecryptQueryString(Request.QueryString["containers"].ToString());
                    
                    txtJobNo.Text = JobNo;

                    //ddlEstimateNo.SelectedValue = EstimateId;

                    //LoadEstimateDDL(Convert.ToInt32(EstimateId));
                    ddlSize_SelectedIndexChanged(null, null);
                    LoadForBLQuery(JobNo, docTypeId);
                    LoadChargeDDL(docTypeId);
                    btnSave.Enabled = true;
                }
            }
        }

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();
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
                    Response.Redirect("~/Unauthorized.aspx");
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

        private void LoadChargeDDL(int docTypeId)
        {
            List<ICharge> lstCharge = new InvoiceBLL().GetAllFwdCharges();
            Session["CHARGES"] = lstCharge;
            ddlFChargeName.DataValueField = "ChargesID";
            ddlFChargeName.DataTextField = "ChargeDescr";
            ddlFChargeName.DataSource = lstCharge;
            ddlFChargeName.DataBind();
            ddlFChargeName.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
        }

        //private void LoadInvoiceTypeDDL()
        //{
        //    try
        //    {
        //        DataTable dt = new InvoiceBLL().GetFwdInvoiceType();

        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            DataRow dr = dt.NewRow();
        //            dr["pk_DocTypeID"] = "0";
        //            dr["DocName"] = "--Select--";
        //            dt.Rows.InsertAt(dr, 0);
        //            ddlInvoiceType.DataValueField = "pk_DocTypeID";
        //            ddlInvoiceType.DataTextField = "DocName";
        //            ddlInvoiceType.DataSource = dt;
        //            ddlInvoiceType.DataBind();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}

        private void LoadLocationDDL()
        {
            try
            {
                DataTable dt = new InvoiceBLL().GetFwdLocation(_userId);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    //dr["pk_LocID"] = "0";
                    //dr["DocName"] = "--Select--";
                    //dt.Rows.InsertAt(dr, 0);
                    ddlLocation.DataValueField = "ID";
                    ddlLocation.DataTextField = "LocName";
                    ddlLocation.DataSource = dt;
                    ddlLocation.DataBind();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //private void LoadPartyDDL()
        //{
        //    try
        //    {
        //        DataTable dt = InvoiceBLL.GetFwdParty();
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            DataRow dr = dt.NewRow();
        //            ddlParty.DataValueField = "PartyId";
        //            ddlParty.DataTextField = "PartyName";
        //            ddlParty.DataSource = dt;
        //            ddlParty.DataBind();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //private void LoadEstimateDDL(int EstimateId)
        //{
        //    try
        //    {
        //        DataTable dt = InvoiceBLL.GetFwdEstimate(EstimateId);

        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            DataRow dr = dt.NewRow();
        //            dr["pk_EstimateId"] = "0";
        //            dr["EstimateNo"] = "--Select--";
        //            dt.Rows.InsertAt(dr, 0);
        //            ddlEstimateNo.DataValueField = "pk_EstimateId";
        //            ddlEstimateNo.DataTextField = "EstimateNo";
        //            ddlEstimateNo.DataSource = dt;
        //            ddlEstimateNo.DataBind();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //private void GrossWeight(string BLNo)
        //{
        //    DataTable dt = new InvoiceBLL().ExpGrossWeight(BLNo);
        //    ViewState["GROSSWEIGHT"] = dt.Rows[0]["GrossWeight"].ToString();
        //    ViewState["VOLUME"] = dt.Rows[0]["Volume"].ToString();
        //}

        //private void TEU(string BLNo)
        //{
        //    DataTable dt = new InvoiceBLL().ExpBLContainers(BLNo);
        //    ViewState["NOOFTEU"] = dt.Rows[0]["NoofTEU"].ToString();
        //    ViewState["NOOFFEU"] = dt.Rows[0]["NoofFEU"].ToString();

        //}

        //private void LoadNvoccDDL()
        //{
        //    List<ILine> lstLine = new ImportBLL().GetAllLine();
        //    Session["NVOCC"] = lstLine;
        //    ddlNvocc.DataValueField = "NVOCCID";
        //    ddlNvocc.DataTextField = "NVOCCName";
        //    ddlNvocc.DataSource = lstLine;
        //    ddlNvocc.DataBind();
        //    ddlNvocc.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
        //}

        private void LoadExchangeRate()
        {
            decimal ExRate;

            ExRate = new InvoiceBLL().GetExchangeRateByDate(Convert.ToDateTime(txtInvoiceDate.Text), 0).ToDecimal();
            //txtUSDExRate.Text = ExRate.ToString();
        }

        //protected void ddlNvocc_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    List<ILine> lstLine = Session["NVOCC"] as List<ILine>;
        //    long nvoccId = Convert.ToInt64(ddlNvocc.SelectedValue);

        //    if (nvoccId > 0)
        //    {

        //        long locationId = Convert.ToInt64(ddlLocation.SelectedValue);

        //        LoadBLNoDDL(nvoccId, locationId);
        //    }
        //    else
        //    {
        //        ddlBLno.SelectedValue = "0";

        //        ViewState["GROSSWEIGHT"] = null;
        //        ViewState["NOOFTEU"] = null;
        //        ViewState["NOOFFEU"] = null;
        //        ViewState["VOLUME"] = null;
        //        ViewState["BLDATE"] = null;

        //    }
        //}


        private void CalculateCharge()
        {
            decimal grossAmount = 0;
            decimal totalAmount = 0;
            decimal serviceTax = 0;
            decimal cessAmount = 0;
            decimal addCess = 0;

            //decimal Teu = Convert.ToDecimal(txtRatePerTEU.Text);
            //decimal Feu = Convert.ToDecimal(txtRateperFEU.Text);
            decimal BL = Convert.ToDecimal(txtRatePerBL.Text);
            decimal Unit = Convert.ToDecimal(txtUnit.Text);
            //decimal CBM = Convert.ToDecimal(txtRatePerCBM.Text);
            //decimal Ton = Convert.ToDecimal(txtRatePerTon.Text);

            decimal TaxPer = 0;
            decimal TaxCess = 0;
            decimal TaxAddCess = 0;

            grossAmount = (BL*Unit);
            var convRate = Convert.ToDecimal(custTxtConvRate.Text);
            convRate = convRate > default(decimal) ? convRate : 1;
            grossAmount = convRate * grossAmount;

            DataTable dtSTax = new InvoiceBLL().GetServiceTax(Convert.ToDateTime(txtInvoiceDate.Text));

            if (dtSTax != null && dtSTax.Rows.Count > 0)
            {
                TaxPer = Convert.ToDecimal(dtSTax.Rows[0]["TaxPer"].ToString());
                TaxCess = Convert.ToDecimal(dtSTax.Rows[0]["TaxCess"].ToString());
                TaxAddCess = Convert.ToDecimal(dtSTax.Rows[0]["TaxAddCess"].ToString());
            }

            DataTable Charge = new InvoiceBLL().ChargeEditable(Convert.ToInt32(ddlFChargeName.SelectedValue));

            if (Convert.ToBoolean(Charge.Rows[0]["ServiceTax"].ToString()))
            {
                serviceTax = Math.Round((grossAmount * TaxPer) / 100, 2);
                cessAmount = Math.Round((serviceTax * TaxCess) / 100, 2);
                addCess = Math.Round((serviceTax * TaxAddCess) / 100, 2);
            }

            totalAmount = (grossAmount + serviceTax + cessAmount + addCess);


            txtGrossAmount.Text = Math.Round(grossAmount, 2).ToString();
            txtServiceTax.Text = Math.Round((serviceTax + cessAmount + addCess), 2).ToString();
            txtTotal.Text = Math.Round(totalAmount, 2).ToString();

            ViewState["CESSAMOUNT"] = cessAmount;
            ViewState["ADDCESS"] = addCess;
            ViewState["STAX"] = serviceTax;
        }

        //protected void txtRatePerTEU_TextChanged(object sender, EventArgs e)
        //{
        //    CalculateCharge();
        //}

        //protected void txtRateperFEU_TextChanged(object sender, EventArgs e)
        //{
        //    CalculateCharge();
        //}
        //protected void txtRatePerCBM_TextChanged(object sender, EventArgs e)
        //{
        //    CalculateCharge();
        //}
        //protected void txtRatePerTon_TextChanged(object sender, EventArgs e)
        //{
        //    CalculateCharge();
        //}
        //protected void txtRateperFEU_TextChanged(object sender, EventArgs e)
        //{
        //    CalculateCharge();
        //}

        protected void txtRatePerBL_TextChanged(object sender, EventArgs e)
        {
            CalculateCharge();
        }

        //protected void txtRatePerCBM_TextChanged(object sender, EventArgs e)
        //{
        //    CalculateCharge();
        //}
        #region Currency
        private void FillCurrency()
        {
            var cur = CommonBLL.GetAllCurrency();
            ddlCurrency.DataValueField = "pk_CurrencyID";
            ddlCurrency.DataTextField = "CurrencyCode";
            ddlCurrency.DataSource = cur;
            ddlCurrency.DataBind();
            ddlCurrency.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
        }

        #endregion
        //protected void txtRatePerTon_TextChanged(object sender, EventArgs e)
        //{
        //    CalculateCharge();
        //}

        protected void ddlChargeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<IChargeRate> chargeRates = new InvoiceBLL().GetfwdInvoiceCharges(Convert.ToInt32(hdnJobID.Value), 0, Convert.ToInt32(ddlFChargeName.SelectedValue), Convert.ToInt32(0), Convert.ToDateTime(txtInvoiceDate.Text));

            DataTable Charge = new InvoiceBLL().ChargeEditable(Convert.ToInt32(ddlFChargeName.SelectedValue));

            if (Charge != null && Charge.Rows.Count > 0)
            {
                txtRatePerBL.Enabled = true;

                #region Commented
                //if (Convert.ToBoolean(Charge.Rows[0]["RateChangeable"].ToString()))
                //{
                //    int ChargeType = Convert.ToInt32(Charge.Rows[0]["ChargeType"].ToString());

                //    if (ChargeType == (int)Enums.ExportChargeType.PER_UNIT)
                //    {
                //        txtRatePerTEU.Enabled = true;
                //        txtRateperFEU.Enabled = true;

                //        txtRatePerBL.Enabled = false;
                //        txtRatePerCBM.Enabled = false;
                //        txtRatePerTon.Enabled = false;
                //    }
                //    else if (ChargeType == (int)Enums.ExportChargeType.PER_DOCUMENT)
                //    {
                //        txtRatePerBL.Enabled = true;

                //        txtRatePerTEU.Enabled = false;
                //        txtRateperFEU.Enabled = false;
                //        txtRatePerCBM.Enabled = false;
                //        txtRatePerTon.Enabled = false;
                //    }
                //    else if (ChargeType == (int)Enums.ExportChargeType.PER_CBM)
                //    {
                //        txtRatePerCBM.Enabled = true;
                //        txtRatePerTon.Enabled = true;

                //        txtRatePerTEU.Enabled = false;
                //        txtRateperFEU.Enabled = false;
                //        txtRatePerBL.Enabled = false;
                //    }
                //    else if (ChargeType == (int)Enums.ExportChargeType.PER_TON)
                //    {
                //        txtRatePerCBM.Enabled = true;
                //        txtRatePerTon.Enabled = true;

                //        txtRatePerTEU.Enabled = false;
                //        txtRateperFEU.Enabled = false;
                //        txtRatePerBL.Enabled = false;
                //    }
                //    else if (ChargeType == (int)Enums.ExportChargeType.TYPE_SIZE)
                //    {
                //        txtRatePerTEU.Enabled = true;
                //        txtRateperFEU.Enabled = true;

                //        txtRatePerBL.Enabled = false;
                //        txtRatePerCBM.Enabled = false;
                //        txtRatePerTon.Enabled = false;
                //    }
                //}
                //else
                //{
                //    txtRatePerTEU.Enabled = false;
                //    txtRateperFEU.Enabled = false;
                //    txtRatePerBL.Enabled = false;
                //    txtRatePerCBM.Enabled = false;
                //    txtRatePerTon.Enabled = false;
                //}
                #endregion

                string currency = Charge.Rows[0]["Currency"].ToString();
                ddlCurrency.SelectedValue = currency;

                if (ddlCurrency.SelectedValue == "1")
                {
                    custTxtConvRate.Text = "1.00";
                    custTxtConvRate.Enabled = false;
                }
                else if (ddlCurrency.SelectedValue == "2")
                {
                    //decimal exchangeRate = new InvoiceBLL().GetExchangeRateForForwarding(Convert.ToInt64(ddlCurrency.SelectedValue), Convert.ToDateTime(txtInvoiceDate.Text));
                    custTxtConvRate.Enabled = false;
                    custTxtConvRate.Text = txtExRate.Text;
                }
                else
                {
                    custTxtConvRate.Enabled = true;
                    custTxtConvRate.Text = "0.00";
                }
            }

            if (chargeRates != null && chargeRates.Count > 0)
            {
                //txtRatePerTEU.Text = chargeRates[0].RatePerTEU.ToString();
                //txtRateperFEU.Text = chargeRates[0].RatePerFEU.ToString();
                //txtRatePerCBM.Text = chargeRates[0].RatePerCBM.ToString();
                //txtRatePerTon.Text = chargeRates[0].RatePerTON.ToString();

                txtRatePerBL.Text = chargeRates[0].RatePerBL.ToString();
                txtGrossAmount.Text = chargeRates[0].GrossAmount.ToString();
                txtServiceTax.Text = (chargeRates[0].ServiceTax + chargeRates[0].ServiceTaxCessAmount + chargeRates[0].ServiceTaxACess).ToString();
                txtTotal.Text = (chargeRates[0].TotalAmount).ToString();
                ViewState["CESSAMOUNT"] = chargeRates[0].ServiceTaxCessAmount;
                ViewState["ADDCESS"] = chargeRates[0].ServiceTaxACess;
                ViewState["STAX"] = chargeRates[0].ServiceTax;
            }
            else
            {
                //txtRatePerTEU.Text = "0.00";
                //txtRateperFEU.Text = "0.00";
                //txtRatePerCBM.Text = "0.00";
                //txtRatePerTon.Text = "0.00";
                //txtUSD.Text = "0.00";

                txtRatePerBL.Text = "0.00";
                txtGrossAmount.Text = "0.00";
                txtServiceTax.Text = "0.00";
                txtTotal.Text = "0.00";

                //ViewState["RATEPERPTEU"] = null;
                //ViewState["RATEPERPFEU"] = null;
                ViewState["RATEPERPBL"] = null;
                ViewState["CESSAMOUNT"] = null;
                ViewState["ADDCESS"] = null;
                ViewState["STAX"] = null;
            }
        }

        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCurrency.SelectedValue == "1")
            {
                custTxtConvRate.Text = "1.00";
                custTxtConvRate.Enabled = false;
            }
            else if (ddlCurrency.SelectedValue == "2")
            {
                decimal exchangeRate = new InvoiceBLL().GetExchangeRateForForwarding(Convert.ToInt64(ddlCurrency.SelectedValue), Convert.ToDateTime(txtInvoiceDate.Text)); ;
                custTxtConvRate.Enabled = true;
                custTxtConvRate.Text = exchangeRate.ToString();
            }
            else
            {
                custTxtConvRate.Enabled = true;
                custTxtConvRate.Text = "0.00";
            }
        }

        protected void gvwInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                RemoveChargeRate(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "EditRow")
            {
                EditChargeRate(Convert.ToInt32(e.CommandArgument));
            }
        }

        public string GetCurrencyCode(int a)
        {
            if (a != 0)
            {
                var t = ddlCurrency.Items.FindByValue(a.ToString());
                if (t != null)
                {
                    return t.Text;
                }
            }
            return "";
        }

        protected void gvwInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ScriptManager sManager = ScriptManager.GetCurrent(this);

                e.Row.Cells[0].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ChargeName"));
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Size"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "UnitType"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "RatePerBL"));
                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Units"));

                e.Row.Cells[5].Text = Convert.ToString(GetCurrencyCode(Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "fk_CurrencyID"))));
                e.Row.Cells[6].Text = Convert.ToString(Math.Round(Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ExchgRate")), 2));
                e.Row.Cells[7].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "GrossAmount"));
                e.Row.Cells[8].Text = Convert.ToString(Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ServiceTax")) + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ServiceTaxCessAmount")) + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ServiceTaxACess")));
                e.Row.Cells[9].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TotalAmount"));

                if (!ReferenceEquals(Request.QueryString["invid"], null))
                {
                    ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");

                    btnRemove.Visible = false;
                    btnEdit.Visible = false;
                }
                else
                {

                    //Delete link
                    ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    btnRemove.Visible = true;
                    btnRemove.ToolTip = "Remove";
                    btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InvoiceChargeId"));


                    //Edit link
                    ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                    btnEdit.Visible = true;
                    btnEdit.ToolTip = "Edit";
                    btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InvoiceChargeId"));

                    btnRemove.OnClientClick = "javascript:return confirm('Are you sure about delete?');";
                    //}
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            DateTime todaydate = DateTime.Now;
            DateTime dtSuppliedDate = DateTime.Parse(txtInvoiceDate.Text);
            if (dtSuppliedDate > todaydate)
            {
                lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00091");
                return;
            }

            string misc = string.Empty;
            IInvoice invoice = new InvoiceEntity();
            BuildInvoiceEntity(invoice);

            List<IChargeRate> chargeRate = ViewState["CHARGERATE"] as List<IChargeRate>;

            long invoiceID = new InvoiceBLL().SaveFwdInvoice(invoice, misc, chargeRate, _isedit);

            if (invoiceID == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('No landing date. Invoice aborted');</script>", false);
            }
            else
            {
                string invoiceNo = new InvoiceBLL().GetInvoiceNoById(invoiceID);

                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('Record saved successfully! Invoice Number: " + invoiceNo + "');</script>", false);
            }
            string encryptedId = GeneralFunctions.EncryptQueryString(hdnJobID.Value);
            Response.Redirect("~/Forwarding/Transaction/Dashboard.aspx?JobId=" + encryptedId);
            //Response.Redirect("~/Forwarding/Transaction/Dashboard.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string encryptedId = GeneralFunctions.EncryptQueryString(hdnJobID.Value);
            Response.Redirect("~/Forwarding/Transaction/Dashboard.aspx?JobId=" + encryptedId);
            //Response.Redirect("~/Forwarding/Transaction/Dashboard.aspx");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddChargesRate();
            ddlSize_SelectedIndexChanged(null, null);
        }

        private void AddChargesRate()
        {
            IChargeRate chargeRate = new ChargeRateEntity();
            BuildChargesRate(chargeRate);

            if (ViewState["CHARGERATE"] != null)
                ChargeRates = ViewState["CHARGERATE"] as List<IChargeRate>;
            else
                ChargeRates = new List<IChargeRate>();

            ChargeRates.Add(chargeRate);

            gvwInvoice.DataSource = ChargeRates;
            gvwInvoice.DataBind();

            //Update Invoice Amount
            txtROff.Text = (Math.Round(ChargeRates.Sum(cr => cr.TotalAmount), 0) - ChargeRates.Sum(cr => cr.TotalAmount)).ToString();
            txtTotalAmount.Text = Math.Round(ChargeRates.Sum(cr => cr.TotalAmount), 0).ToString();

            ViewState["CHARGERATE"] = ChargeRates;
            ClearChargesRate();
        }

        private void BuildInvoiceEntity(IInvoice invoice)
        {
            List<IChargeRate> chargeRate = ViewState["CHARGERATE"] as List<IChargeRate>;

            if (ViewState["InvoiceID"] == null)
                invoice.InvoiceID = 0;
            else
                invoice.InvoiceID = Convert.ToInt64(ViewState["InvoiceID"]);

            invoice.AddedOn = DateTime.Now;
            invoice.BLID = 0;
            invoice.CHAID = 0;
            invoice.CompanyID = 1;
            invoice.EditedOn = DateTime.Now;
            invoice.ExportImport = "F";
            invoice.GrossAmount = chargeRate.Sum(c => c.GrossAmount);
            invoice.InvoiceDate = Convert.ToDateTime(txtInvoiceDate.Text);
            invoice.InvoiceTypeID = 37;
            //invoice.InvoiceTypeID = Convert.ToInt32(ddlInvoiceType.SelectedValue);
            //invoice.JobID = Convert.ToInt32(ddlJobNo.SelectedValue);
            invoice.JobID = hdnJobID.Value.ToInt();
            //invoice.EstimateID = Convert.ToInt32(ddlEstimateNo.SelectedValue);
            invoice.ServiceTax = chargeRate.Sum(c => c.ServiceTax);
            invoice.ServiceTaxACess = chargeRate.Sum(c => c.ServiceTaxACess);
            invoice.ServiceTaxCess = chargeRate.Sum(c => c.ServiceTaxCessAmount);
            invoice.Roff = Convert.ToDecimal(txtROff.Text);
            invoice.UserAdded = _userId;
            invoice.UserLastEdited = _userId;
            invoice.InvoiceNo = txtInvoiceNo.Text;
            invoice.LocationID = ddlLocation.SelectedValue.ToInt();
            invoice.PartyId = Convert.ToInt32(ddlParty.SelectedValue);
            invoice.PartyTypeID = Convert.ToInt32(ddlPartyType.SelectedValue);
        }

        private void BuildChargesRate(IChargeRate charge)
        {
            if (ViewState["EDITINVOICECHARGEID"] == null)
            {
                if (ViewState["INVOICECHARGEID"] == null)
                    charge.InvoiceChargeId = -1;
                else
                    charge.InvoiceChargeId = Convert.ToInt32(ViewState["INVOICECHARGEID"]) - 1;

                ViewState["INVOICECHARGEID"] = charge.InvoiceChargeId;
            }
            else
            {
                if (ViewState["CHARGERATE"] != null)
                    ChargeRates = ViewState["CHARGERATE"] as List<IChargeRate>;

                charge.InvoiceChargeId = Convert.ToInt32(ViewState["EDITINVOICECHARGEID"]);

                IChargeRate cRate = ChargeRates.Single(c => c.InvoiceChargeId == Convert.ToInt64(ViewState["EDITINVOICECHARGEID"]));
                ChargeRates.Remove(cRate);
                ViewState["CHARGERATE"] = ChargeRates;
                ViewState["EDITINVOICECHARGEID"] = null;
            }

            charge.ChargeName = ddlFChargeName.SelectedItem.Text;
            charge.ChargesID = Convert.ToInt32(ddlFChargeName.SelectedValue);
            charge.GrossAmount = Convert.ToDecimal(txtGrossAmount.Text);
            charge.RatePerBL = Convert.ToDecimal(txtRatePerBL.Text);
            charge.fk_CurrencyID = Convert.ToInt32(ddlCurrency.SelectedValue);
            charge.ExchgRate = Math.Round(Convert.ToDecimal(custTxtConvRate.Text), 2);
            charge.Units = Convert.ToDecimal(txtUnit.Text);
            charge.UnitTypeID = Convert.ToInt32(ddlUnitType.SelectedValue);
            charge.UnitType = ddlUnitType.SelectedItem.Text;
            charge.Size = ddlSize.SelectedItem.Text;

            if (ViewState["STAX"] != null)
                charge.ServiceTax = Convert.ToDecimal(ViewState["STAX"]);

            if (ViewState["CESSAMOUNT"] != null)
                charge.ServiceTaxCessAmount = Convert.ToDecimal(ViewState["CESSAMOUNT"]);
            if (ViewState["ADDCESS"] != null)
                charge.ServiceTaxACess = Convert.ToDecimal(ViewState["ADDCESS"]);

            charge.TotalAmount = Convert.ToDecimal(txtTotal.Text);
        }

        private void ClearChargesRate()
        {
            ddlFChargeName.SelectedValue = "0";
            txtGrossAmount.Text = "0.00";
            txtRatePerBL.Text = "0.00";
            txtUnit.Text = "0.00";
            txtTotal.Text = "0.00";
            txtServiceTax.Text = "0.00";
            txtGrossAmount.Text = "0.00";
            custTxtConvRate.Text = "0.00";
            ddlCurrency.SelectedIndex = 0;
            txtRatePerBL.Enabled = true;
            ddlSize.SelectedIndex = 0;
            ddlUnitType.SelectedIndex = 0;

            //txtRatePerCBM.Text = "0.00";
            //txtRateperFEU.Text = "0.00";
            //txtRatePerTEU.Text = "0.00";
            //txtRatePerTon.Text = "0.00";

            //if (Convert.ToInt32(ddlInvoiceType.SelectedValue) == 19)
            //{
            //    txtRatePerTEU.Enabled = false;
            //    txtRateperFEU.Enabled = false;
            //    txtRatePerBL.Enabled = false;
            //    txtRatePerCBM.Enabled = false;
            //    txtRatePerTon.Enabled = false;
            //    ddlFChargeName.Enabled = false;
            //}
            //else
            //{
            //    txtRatePerTEU.Enabled = true;
            //    txtRateperFEU.Enabled = true;
            //    txtRatePerBL.Enabled = true;
            //    txtRatePerCBM.Enabled = true;
            //    txtRatePerTon.Enabled = true;
            //    ddlFChargeName.Enabled = true;
            //}
        }

        private void RemoveChargeRate(int InvoiceChargeId)
        {
            //Then Delete from List
            if (ViewState["CHARGERATE"] != null)
                ChargeRates = ViewState["CHARGERATE"] as List<IChargeRate>;

            IChargeRate cRate = ChargeRates.Single(c => c.InvoiceChargeId == InvoiceChargeId);
            ChargeRates.Remove(cRate);

            //Delete from DB
            int retVal = new InvoiceBLL().DeleteInvoiceCharge((Convert.ToInt32(cRate.InvoiceChargeId)));

            ViewState["CHARGERATE"] = ChargeRates;
            RefreshGridView();

            //Update Invoice Amount
            txtROff.Text = (Math.Round(ChargeRates.Sum(cr => cr.TotalAmount), 0) - ChargeRates.Sum(cr => cr.TotalAmount)).ToString();
            txtTotalAmount.Text = Math.Round(ChargeRates.Sum(cr => cr.TotalAmount), 0).ToString();
        }

        private void RefreshGridView()
        {
            if (ViewState["CHARGERATE"] != null)
                ChargeRates = ViewState["CHARGERATE"] as List<IChargeRate>;

            gvwInvoice.DataSource = ChargeRates;
            gvwInvoice.DataBind();
        }

        private void LoadForEdit(long InvoiceId)
        {
            //For Invoice
            int docTypeId = 0;
            //Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["docTypeId"].ToString()), out docTypeId);
            //string JobNo = GeneralFunctions.DecryptQueryString(Request.QueryString["jobNo"].ToString());
            string invoiceID = GeneralFunctions.DecryptQueryString(Request.QueryString["invid"].ToString());
            //string Containers = GeneralFunctions.DecryptQueryString(Request.QueryString["containers"].ToString());
                    
            IInvoice invoice = new InvoiceBLL().GetFwdInvoiceById(InvoiceId);

            ViewState["InvoiceID"] = invoice.InvoiceID;
            hdnJobID.Value = invoice.JobID.ToString();
            //txtBLDate.Text = invoice.BLDate.ToShortDateString();
            //txtBLNo.Text = invoice.BLNo;
            txtJobNo.Text = invoice.JobNo;
            txtJobDate.Text = invoice.JobDate.ToString();
            txtInvoiceDate.Text = invoice.InvoiceDate.ToShortDateString();
            txtInvoiceNo.Text = invoice.InvoiceNo;

            //ddlInvoiceType.SelectedValue = invoice.InvoiceTypeID.ToString();
            //ddlJobNo.SelectedValue = invoice.JobID.ToString();
            
            ddlPartyType.SelectedValue = invoice.PartyTypeID.ToString();
            GetPartyValuesSetToDdl(ddlPartyType.SelectedValue.ToInt());
            ddlParty.SelectedValue = invoice.PartyId.ToString();
          
            //TEU(invoice.JobNo);
            //LoadEstimateDDL(invoice.EstimateID);
            //ddlEstimateNo.SelectedValue = invoice.EstimateID.ToString();

            //long nvoccId = Convert.ToInt64(ddlNvocc.SelectedValue);
            //long locationId = Convert.ToInt64(ddlLocation.SelectedValue);

            //LoadExchangeRate();

            //txtContainers.Text = ViewState["NOOFTEU"].ToString() + " x 20' & " + ViewState["NOOFFEU"].ToString() + " x 40'";
            //txtContainers.Text = Containers;
            txtContainers.Text = "20' X " + invoice.TEUS.ToString() + " 40' X " + invoice.FEUS.ToString(); 

            //For Charge Rates
            List<IChargeRate> chargeRates = new InvoiceBLL().GetFwdInvoiceChargesById(InvoiceId);
            ViewState["CHARGERATE"] = chargeRates;

            txtInvoiceDate.Enabled = false;
            //txtUSDExRate.Enabled = false;
            btnAdd.Enabled = false;
            txtROff.Text = (Math.Round(chargeRates.Sum(cr => cr.TotalAmount), 0) - chargeRates.Sum(cr => cr.TotalAmount)).ToString();
            txtTotalAmount.Text = Math.Round(chargeRates.Sum(cr => cr.TotalAmount), 0).ToString();

            gvwInvoice.DataSource = chargeRates;
            gvwInvoice.DataBind();
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
        private void LoadForBLQuery(string JobNo, int DocType)
        {
            DataTable dt = new InvoiceBLL().GetFwdLineLocation(JobNo);
            LoadPartyTypeDDl();
            //var partyType = new EstimateBLL().GetBillingGroupMaster((ISearchCriteria)null);
            //ddlPartyType.DataSource = partyType;

            //ddlPartyType.DataTextField = "PartyType";
            //ddlPartyType.DataValueField = "pk_PartyTypeID";
            //ddlPartyType.DataBind();
            //ddlPartyType.Items.Insert(0, new ListItem("--Select--", "0"));

            //int line = Convert.ToInt32(dt.Rows[0]["fk_flineId"]);
            int location = Convert.ToInt32(dt.Rows[0]["fk_LocationID"]);
            string BlNo = Convert.ToString(dt.Rows[0]["BLNo"]);
            hdnJobID.Value = Convert.ToString(dt.Rows[0]["pk_JobId"]);
            int PartyId = Convert.ToInt32(dt.Rows[0]["PartyId"]);
            string BLDate = Convert.ToString(dt.Rows[0]["BLDate"]);
            txtJobDate.Text = Convert.ToDateTime(dt.Rows[0]["JobDate"]).ToShortDateString();

            txtContainers.Text = "20' X " + Convert.ToString(dt.Rows[0]["teus"]) + " 40' X " + Convert.ToString(dt.Rows[0]["feus"]);

            var ex = new EstimateBLL().GetExchange(new SearchCriteria() { StringOption1 = "2", Date = txtJobDate.Text.ToDateTime() });
            txtExRate.Text = Convert.ToString(ex.Tables[0].Rows[0]["USDXchRate"]);

            
            txtInvoiceDate.Text = DateTime.Now.ToShortDateString();
            //ddlInvoiceType.SelectedValue = DocType.ToString();

            ddlLocation.SelectedValue = location.ToString();
            //txtBLNo.Text = BlNo;
            //txtBLDate.Text = BLDate;
            ddlParty.SelectedValue = PartyId.ToString();
            //ddlNvocc.SelectedValue = line.ToString();
            //LoadBLNoDDL(line, location);
            //ddlBLno.SelectedValue = blId.ToString();

            List<IChargeRate> chargeRates = new InvoiceBLL().GetfwdInvoiceCharges(Convert.ToInt32(hdnJobID.Value), Convert.ToInt32(ddlFChargeName.SelectedValue),
                0, DocType, Convert.ToDateTime(txtInvoiceDate.Text));
            ViewState["CHARGERATE"] = chargeRates;

            if (chargeRates != null && chargeRates.Count > 0)
            {
                if (chargeRates.Min(c => c.InvoiceChargeId) < 0)
                    ViewState["INVOICECHARGEID"] = chargeRates.Min(c => c.InvoiceChargeId);
                else
                    ViewState["INVOICECHARGEID"] = null;

                var Cur = chargeRates.Where(c => c.fk_CurrencyID == 2).FirstOrDefault().ExchgRate;
                //txtUSDExRate.Text = Cur.ToString();
            }
            else
            {
                ViewState["INVOICECHARGEID"] = null;
            }

            LoadChargeDDL(DocType);

            gvwInvoice.DataSource = chargeRates;
            gvwInvoice.DataBind();

            //Update Invoice Amount
            txtROff.Text = (Math.Round(chargeRates.Sum(cr => cr.TotalAmount), 0) - chargeRates.Sum(cr => cr.TotalAmount)).ToString();
            txtTotalAmount.Text = Math.Round(chargeRates.Sum(cr => cr.TotalAmount), 0).ToString();
        }

        private void EditChargeRate(int InvoiceChargeId)
        {
            List<IChargeRate> chargeRates = null;

            if (ViewState["CHARGERATE"] != null)
                chargeRates = ViewState["CHARGERATE"] as List<IChargeRate>;

            IChargeRate chargeRate = chargeRates.Single(c => c.InvoiceChargeId == InvoiceChargeId);
            DataTable Charge = new InvoiceBLL().ChargeEditable(chargeRate.ChargesID);

            if (Charge != null && Charge.Rows.Count > 0)
            {
                if (Convert.ToBoolean(Charge.Rows[0]["RateChangeable"].ToString()))
                {
                    int ChargeType = Convert.ToInt32(Charge.Rows[0]["ChargeType"].ToString());
                    txtRatePerBL.Enabled = true;

                    #region Commented
                    //if (ChargeType == (int)Enums.ExportChargeType.PER_UNIT)
                    //{
                    //    txtRatePerTEU.Enabled = true;
                    //    txtRateperFEU.Enabled = true;

                    //    txtRatePerBL.Enabled = false;
                    //    txtRatePerCBM.Enabled = false;
                    //    txtRatePerTon.Enabled = false;
                    //}
                    //else if (ChargeType == (int)Enums.ExportChargeType.PER_DOCUMENT)
                    //{
                    //    txtRatePerBL.Enabled = true;

                    //    txtRatePerTEU.Enabled = false;
                    //    txtRateperFEU.Enabled = false;
                    //    txtRatePerCBM.Enabled = false;
                    //    txtRatePerTon.Enabled = false;
                    //}
                    //else if (ChargeType == (int)Enums.ExportChargeType.PER_CBM)
                    //{
                    //    txtRatePerCBM.Enabled = true;
                    //    txtRatePerTon.Enabled = true;

                    //    txtRatePerTEU.Enabled = false;
                    //    txtRateperFEU.Enabled = false;
                    //    txtRatePerBL.Enabled = false;
                    //}
                    //else if (ChargeType == (int)Enums.ExportChargeType.PER_TON)
                    //{
                    //    txtRatePerCBM.Enabled = true;
                    //    txtRatePerTon.Enabled = true;

                    //    txtRatePerTEU.Enabled = false;
                    //    txtRateperFEU.Enabled = false;
                    //    txtRatePerBL.Enabled = false;
                    //}
                    //else if (ChargeType == (int)Enums.ExportChargeType.TYPE_SIZE)
                    //{
                    //    txtRatePerTEU.Enabled = true;
                    //    txtRateperFEU.Enabled = true;

                    //    txtRatePerBL.Enabled = false;
                    //    txtRatePerCBM.Enabled = false;
                    //    txtRatePerTon.Enabled = false;
                    //}
                    #endregion
                }
                //else
                //{
                //    txtRatePerTEU.Enabled = false;
                //    txtRateperFEU.Enabled = false;
                //    txtRatePerBL.Enabled = false;
                //    txtRatePerCBM.Enabled = false;
                //    txtRatePerTon.Enabled = false;
                //}
            }

            ddlFChargeName.SelectedValue = chargeRate.ChargesID.ToString();
            txtGrossAmount.Text = chargeRate.GrossAmount.ToString();
            txtRatePerBL.Text = chargeRate.RatePerBL.ToString();
            txtServiceTax.Text = (chargeRate.STax + chargeRate.ServiceTaxCessAmount + chargeRate.ServiceTaxACess).ToString();
            ddlCurrency.SelectedValue = chargeRate.fk_CurrencyID.ToString();

            if (chargeRate.fk_CurrencyID.ToString() == "1")
                custTxtConvRate.Enabled = false;
            else
                custTxtConvRate.Enabled = true;

            custTxtConvRate.Text = chargeRate.ExchgRate.ToString("0.00");
            ViewState["STAX"] = chargeRate.STax;
            ViewState["CESSAMOUNT"] = chargeRate.ServiceTaxCessAmount;
            ViewState["ADDCESS"] = chargeRate.ServiceTaxACess;
            txtTotal.Text = chargeRate.TotalAmount.ToString();
            ViewState["EDITINVOICECHARGEID"] = chargeRate.InvoiceChargeId;

            //txtRatePerCBM.Text = chargeRate.RatePerCBM.ToString();
            //txtRateperFEU.Text = chargeRate.RatePerFEU.ToString();
            //txtRatePerTEU.Text = chargeRate.RatePerTEU.ToString();
            //txtRatePerTon.Text = chargeRate.RatePerTON.ToString();
        }

        //protected void txtUSDExRate_TextChanged(object sender, EventArgs e)
        //{
        //    List<IChargeRate> lstData = ViewState["CHARGERATE"] as List<IChargeRate>;
        //    decimal TaxPer = 0;
        //    decimal TaxCess = 0;
        //    decimal TaxAddCess = 0;

        //    DataTable dtSTax = new InvoiceBLL().GetServiceTax(Convert.ToDateTime(txtInvoiceDate.Text));

        //    if (dtSTax != null && dtSTax.Rows.Count > 0)
        //    {
        //        TaxPer = Convert.ToDecimal(dtSTax.Rows[0]["TaxPer"].ToString());
        //        TaxCess = Convert.ToDecimal(dtSTax.Rows[0]["TaxCess"].ToString());
        //        TaxAddCess = Convert.ToDecimal(dtSTax.Rows[0]["TaxAddCess"].ToString());
        //    }

        //    lstData.Where(d => d.fk_CurrencyID == 2)
        //        .Select(d =>
        //        {
        //            d.ExchgRate = System.Math.Round(Convert.ToDecimal(txtUSDExRate.Text), 2);
        //            d.GrossAmount = System.Math.Round((d.ExchgRate * d.RatePerBL) + (d.ExchgRate * d.RatePerFEU) + (d.ExchgRate * d.RatePerTON) + (d.ExchgRate * d.RatePerUnit) + (d.ExchgRate * d.RatePerCBM), 2);
        //            DataTable Charge = new InvoiceBLL().ChargeEditable(d.ChargesID);
        //            if (Convert.ToBoolean(Charge.Rows.Count.ToInt() > 0))
        //            {
        //                if (Convert.ToBoolean(Charge.Rows[0]["ServiceTax"].ToString()))
        //                {
        //                    d.ServiceTax = Math.Round((d.GrossAmount * TaxPer) / 100, 0);
        //                    d.ServiceTaxCessAmount = Math.Round((d.ServiceTax * TaxCess) / 100, 0);
        //                    d.ServiceTaxACess = Math.Round((d.ServiceTax * TaxAddCess) / 100, 0);
        //                };
        //            };
        //            d.TotalAmount = Math.Round(d.GrossAmount + d.ServiceTax + d.ServiceTaxCessAmount + d.ServiceTaxACess, 0);
        //            return d;
        //        }).ToList();

        //    ViewState["CHARGERATE"] = lstData;

        //    gvwInvoice.DataSource = lstData;
        //    gvwInvoice.DataBind();

        //    txtROff.Text = (Math.Round(lstData.Sum(cr => cr.TotalAmount), 0) - lstData.Sum(cr => cr.TotalAmount)).ToString();
        //    txtTotalAmount.Text = Math.Round(lstData.Sum(cr => cr.TotalAmount), 0).ToString();
        //}

        protected void txtInvoiceDate_TextChanged(object sender, EventArgs e)
        {
            LoadExchangeRate();
            //txtUSDExRate_TextChanged(null, null);
        }

        protected void ddlSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateUnitType();


            //var ddlUnitTypeID = (DropDownList)grvCharges.FooterRow.FindControl("ddlUnitType");
            //var ddlSizeID = (DropDownList)grvCharges.FooterRow.FindControl("ddlSize");
            //var txtUnit = (TextBox)grvCharges.FooterRow.FindControl("txtUnit");

            //if (ddlUnitTypeID.SelectedIndex != -1 && ddlSizeID.SelectedIndex != -1)
            //{
            //    DataSet ds = new DataSet();
            //    ds = new EstimateBLL().GetContainers(ddlUnitTypeID.SelectedValue.ToString(), ddlSizeID.SelectedValue.ToString());
            //    txtUnit.Text = ds.Tables[0].Rows[0]["Unit"].ToString();
            //}
        }

        private void PopulateUnitType()
        {
            //var dllU = (DropDownList)gvwInvoice.FooterRow.FindControl("ddlUnitType");

            var curtype = "E";
            if (ddlSize.SelectedValue.ToInt() == 0)
                curtype = "N";
            else
                curtype = "E";

            DataSet dllUs = new DataSet();
            dllUs = new EstimateBLL().GetUnitMaster(new SearchCriteria { StringOption1 = "1", StringOption2 = curtype, SortExpression = "UnitName" });
            ddlUnitType.DataSource = dllUs;
            ddlUnitType.DataTextField = "UnitName";
            ddlUnitType.DataValueField = "pk_UnitTypeID";
            ddlUnitType.DataBind();
            ddlUnitType.Items.Insert(0, new ListItem("--Select--", "0"));

            if (ViewState["dllUs"] == null)
            {
                //var ddlSize = (DropDownList)gvwInvoice.FooterRow.FindControl("ddlSize");
                if (ddlSize.SelectedValue.ToInt() == 0)
                    curtype = "N";
                else
                    curtype = "E";
                dllUs = new EstimateBLL().GetUnitMaster(new SearchCriteria { StringOption1 = "1", StringOption2 = curtype, SortExpression = "UnitName" });
                ViewState["ddlUnitType"] = dllUs;
            }
            else
            {
                dllUs = (DataSet)ViewState["ddlUnitType"];
            }

        }

        private void CheckContainer()
        {
            //var ddlUnitTypeID = (DropDownList)gvwInvoice.FooterRow.FindControl("ddlUnitType");
            //var ddlSizeID = (DropDownList)gvwInvoice.FooterRow.FindControl("ddlSize");
            //var txtUnit = (TextBox)gvwInvoice.FooterRow.FindControl("txtUnit");

            if (ddlUnitType.SelectedIndex != 0 && ddlSize.SelectedIndex != 0)
            {
                DataSet ds = new DataSet();
                ds = new EstimateBLL().GetContainers(ddlUnitType.SelectedValue.ToInt(), ddlSize.SelectedValue.ToString(), hdnJobID.Value.ToInt());
                if (ds.Tables[0].Rows.Count > 0)
                    txtUnit.Text = ds.Tables[0].Rows[0]["Nos"].ToString();
                else
                    txtUnit.Text = "0";
            }
        }

        protected void ddlUnitType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            //var ddlUnitTypeID = (DropDownList)gvwInvoice.FooterRow.FindControl("ddlUnitType");
            ds = new EstimateBLL().GetSingleUnitType(ddlUnitType.SelectedValue.ToInt(), hdnJobID.Value.ToInt());
            //var ddlSizeID = (DropDownList)gvwInvoice.FooterRow.FindControl("ddlSize");
            //var rfvSize = (RequiredFieldValidator)grvCharges.FooterRow.FindControl("rfvSize");
            //var txtUnit = (TextBox)gvwInvoice.FooterRow.FindControl("txtUnit");
            //ds = new EstimateBLL().GetSelectedCharge(ddlChargeID.SelectedValue.ToInt());
            if (ds.Tables[0].Rows[0]["UnitType"].ToString() == "N")
            {
                //ddlSizeID.Enabled = false;
                //rfvSize.Enabled = false;
                if (ds.Tables[0].Rows[0]["UnitName"].ToString() == "CBM")
                    txtUnit.Text = ds.Tables[1].Rows[0]["volcbm"].ToString();
                else if (ds.Tables[0].Rows[0]["UnitName"].ToString() == "M.TON")
                    txtUnit.Text = ds.Tables[1].Rows[0]["weightMT"].ToString();
                else if (ds.Tables[0].Rows[0]["UnitName"].ToString() == "REV.TON")
                    txtUnit.Text = ds.Tables[1].Rows[0]["revton"].ToString();
                else if (ds.Tables[0].Rows[0]["UnitName"].ToString() == "CHRG. WT.(KGS)")
                    txtUnit.Text = ds.Tables[1].Rows[0]["ChargeWt"].ToString();
            }
            else
                CheckContainer();

            //var ddlUnitTypeID = (DropDownList)grvCharges.FooterRow.FindControl("ddlUnitType");
            //var ddlSizeID = (DropDownList)grvCharges.FooterRow.FindControl("ddlSize");
            //var txtUnit = (TextBox)grvCharges.FooterRow.FindControl("txtUnit");

            //if (ddlUnitTypeID.SelectedIndex <> -1 && ddlSizeID.SelectedIndex != -1)
            //{
            //    DataSet ds = new DataSet();
            //    ds = new EstimateBLL().GetContainers(ddlUnitTypeID.SelectedValue.ToString(), ddlSizeID.SelectedValue.ToString());
            //    txtUnit.Text = ds.Tables[0].Rows[0]["Unit"].ToString();
            //}

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

        protected void TextEx_TextChanged(object sender, EventArgs e)
        {
            List<IChargeRate> charges = null;
            DataSet ds = new DataSet();

            charges = (List<IChargeRate>)ViewState["CHARGERATE"];
            if (charges.Count > 0)
            {
                for (int count = 0; count < charges.Count; count++)
                {
                    if (charges[count].fk_CurrencyID.ToInt() == 2)
                    {
                        charges[count].ExchgRate = txtExRate.Text.ToDecimal();
                        ds = new EstimateBLL().GetServiceTax(txtJobDate.Text.ToDateTime(), (charges[count].Units * charges[count].RatePerBL * charges[count].ExchgRate).ToDecimal(), charges[count].ChargesID);
                        if (ds.Tables[1].Rows[0]["ServiceTax"].ToInt() == 1 && charges[count].STax != 0)
                        {
                            charges[count].STax = (ds.Tables[0].Rows[0]["stax"].ToDecimal() + ds.Tables[0].Rows[0]["CessAmt"].ToDecimal() + ds.Tables[0].Rows[0]["AddCess"].ToDecimal()).ToDecimal();
                        }
                        else
                        {
                            charges[count].STax = 0;
                        }
                        charges[count].TotalAmount = (charges[count].Units * charges[count].RatePerBL * charges[count].ExchgRate) + charges[count].STax;
                    }
                }
                txtTotalAmount.Text = charges.Sum(m => m.TotalAmount).ToString("##########0.00");
                ViewState["CHARGERATE"] = charges;
                gvwInvoice.DataSource = charges;
                gvwInvoice.DataBind();
            }
        }
        
    }
}