﻿using System;
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

namespace EMS.WebApp.Export
{
    public partial class ExportInvoice : System.Web.UI.Page
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
            int NewType = 0;
            //CheckUserAccess();

            if (!Page.IsPostBack)
            {
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "setMinDateOnSecondControl", "function setMinDateOnSecondControl(e) { document.getElementById('" + txtInvoiceDate.ClientID + "').dateSelectionChanged += function x(e) { alert(e);} ; }", true);
                string strProcessScript = "this.value='Processing...';this.disabled=true;";
                btnSave.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnSave, "").ToString());
                FillCurrency();
                Session["CHARGES"] = null;

                LoadInvoiceTypeDDL();
                LoadLocationDDL();
                LoadNvoccDDL();
                //rngInvoiceDate.MaximumValue = DateTime.Now.Date.ToString("dd-MM-yy");
                //rngInvoiceDate.MaximumValue = DateTime.Now.ToShortDateString();
                //rvDate.MaximumValue = DateTime.Now.ToShortDateString();
    
                /*
                LoadCHADDL();
                 */

                //Charge


                if (!ReferenceEquals(Request.QueryString["invid"], null))
                {
                    long invoiveId = 0;
                    Int64.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["invid"].ToString()), out invoiveId);
                    _isedit = 1;
                    if (invoiveId > 0)
                        LoadForEdit(invoiveId);

                    //btnSave.Enabled = false;
                }
                if (!ReferenceEquals(Request.QueryString["p1"], null))
                {
                    string blNo = string.Empty;
                    int docTypeId = 0;
                    string misc = string.Empty;
                    _isedit = 0;
                    blNo = GeneralFunctions.DecryptQueryString(Request.QueryString["p1"].ToString());
                    Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["p3"].ToString()), out docTypeId);
                    misc = GeneralFunctions.DecryptQueryString(Request.QueryString["p4"].ToString());
                    var BookingNo = GeneralFunctions.DecryptQueryString(Request.QueryString["p5"].ToString());
                    var BLDate = GeneralFunctions.DecryptQueryString(Request.QueryString["p6"].ToString());
                    var BookingDate = GeneralFunctions.DecryptQueryString(Request.QueryString["p7"].ToString());
                    var Containers = GeneralFunctions.DecryptQueryString(Request.QueryString["p8"].ToString());
                    txtBLDate.Text = BLDate;
                    txtBooking.Text = BookingNo;
                    txtContainers.Text = Containers;
                  
                    //if (docTypeId == 1)
                    //    NewType = 19;
                    //else
                    //    NewType = 9;

                    //if (docTypeId == 3)
                    //    NewType = -1;

                    
                    LoadForBLQuery(blNo, docTypeId, misc);
                    LoadChargeDDL(docTypeId);
                    SetDefaultTerminal();
                    btnSave.Enabled = true;
                }
            }
        }

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            //Get user permission.
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
            List<ICharge> lstCharge = new InvoiceBLL().GetAllExpCharges(docTypeId, ddlLocation.SelectedValue.ToInt(), ddlNvocc.SelectedValue.ToInt(), txtBooking.Text);
            Session["CHARGES"] = lstCharge;
            ddlFChargeName.DataValueField = "ChargesID";
            ddlFChargeName.DataTextField = "ChargeDescr";
            ddlFChargeName.DataSource = lstCharge;
            ddlFChargeName.DataBind();
            ddlFChargeName.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
        }

        private void LoadInvoiceTypeDDL()
        {
            try
            {
                DataTable dt = new InvoiceBLL().GetExpInvoiceType();

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["pk_DocTypeID"] = "0";
                    dr["DocName"] = "--Select--";
                    dt.Rows.InsertAt(dr, 0);
                    ddlInvoiceType.DataValueField = "pk_DocTypeID";
                    ddlInvoiceType.DataTextField = "DocName";
                    ddlInvoiceType.DataSource = dt;
                    ddlInvoiceType.DataBind();
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        private void LoadLocationDDL()
        {
            List<ILocation> lstLocation = new ImportBLL().GetLocation(_userId, true);  //Replace with UserId
            ddlLocation.DataValueField = "Id";
            ddlLocation.DataTextField = "Name";
            ddlLocation.DataSource = lstLocation;
            ddlLocation.DataBind();
            ddlLocation.SelectedValue = lstLocation[0].DefaultLocation.ToString();
        }

        private void LoadBLNoDDL(long nvoccId, long locationId)
        {

            try
            {
                DataTable dt = new InvoiceBLL().GetExpBLno(nvoccId, locationId);
                DataRow dr = dt.NewRow();
                dr["pk_ExpBLID"] = "0";//pk_ExpBLID,ExpBLNo
                dr["ExpBLNo"] = "--Select--";
                dt.Rows.InsertAt(dr, 0);
                ddlBLno.DataValueField = "pk_ExpBLID";
                ddlBLno.DataTextField = "ExpBLNo";
                ddlBLno.DataSource = dt;
                ddlBLno.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GrossWeight(string BLNo)
        {
            DataTable dt = new InvoiceBLL().ExpGrossWeight(BLNo);
            ViewState["GROSSWEIGHT"] = dt.Rows[0]["GrossWeight"].ToString();
            ViewState["VOLUME"] = dt.Rows[0]["Volume"].ToString();

        }

        private void TEU(string BLNo)
        {
            DataTable dt = new InvoiceBLL().ExpBLContainers(BLNo);
            ViewState["NOOFTEU"] = dt.Rows[0]["NoofTEU"].ToString();
            ViewState["NOOFFEU"] = dt.Rows[0]["NoofFEU"].ToString();

        }
        /*
        private void FEU(string BLNo)
        {
            DataTable dt = new InvoiceBLL().FEU(BLNo);
            ViewState["NOOFFEU"] = dt.Rows[0]["NoofFEU"].ToString();

        }

        private void Volume(string BLNo)
        {
            DataTable dt = new InvoiceBLL().Volume(BLNo);
            ViewState["VOLUME"] = dt.Rows[0]["Volume"].ToString();

        }
        */
        /*
        private void BLdate(string BLNo)
        {
            DataTable dt = new InvoiceBLL().BLdate(BLNo);
            ViewState["BLDATE"] = Convert.ToDateTime(dt.Rows[0]["ImpLineBLDate"].ToString()).ToShortDateString();

        }
        */

        private void LoadNvoccDDL()
        {
            List<ILine> lstLine = new ImportBLL().GetAllLine();
            Session["NVOCC"] = lstLine;
            ddlNvocc.DataValueField = "NVOCCID";
            ddlNvocc.DataTextField = "NVOCCName";
            ddlNvocc.DataSource = lstLine;
            ddlNvocc.DataBind();
            ddlNvocc.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
        }

        /*
        private void LoadCHADDL()
        {
            try
            {
                DataTable dt = new InvoiceBLL().GetCHAId();

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["fk_AddressID"] = "0";
                    dr["AddrName"] = "--Select--";
                    dt.Rows.InsertAt(dr, 0);
                    ddlCHAid.DataValueField = "fk_AddressID";
                    ddlCHAid.DataTextField = "AddrName";
                    ddlCHAid.DataSource = dt;
                    ddlCHAid.DataBind();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
         */

        private void LoadExchangeRate()
        {
            decimal ExRate;

            ExRate = new InvoiceBLL().GetExchangeRateByDate(Convert.ToDateTime(txtInvoiceDate.Text), ddlNvocc.SelectedValue.ToInt()).ToDecimal();
            txtUSDExRate.Text = ExRate.ToString();
        }

        private void SetDefaultTerminal()
        {
            LoadTerminalDDL();

            long TerminalId = new InvoiceBLL().GetExpDefaultTerminal(Convert.ToInt64(ddlBLno.SelectedValue));
            ddlFTerminal.SelectedValue = TerminalId.ToString();
        }

        private void LoadTerminalDDL()
        {
            try
            {
                DataTable dt = new InvoiceBLL().GetTerminals(Convert.ToInt64(ddlLocation.SelectedValue));

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["pk_TerminalID"] = "0";
                    dr["TerminalName"] = "--Select--";
                    dt.Rows.InsertAt(dr, 0);
                    ddlFTerminal.DataValueField = "pk_TerminalID";
                    ddlFTerminal.DataTextField = "TerminalName";
                    ddlFTerminal.DataSource = dt;
                    ddlFTerminal.DataBind();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void ddlNvocc_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ILine> lstLine = Session["NVOCC"] as List<ILine>;
            long nvoccId = Convert.ToInt64(ddlNvocc.SelectedValue);

            if (nvoccId > 0)
            {
                //imgLineLogo.ImageUrl = lstLine.Where(l => l.NVOCCID == nvoccId).FirstOrDefault().LogoPath;

                long locationId = Convert.ToInt64(ddlLocation.SelectedValue);

                LoadBLNoDDL(nvoccId, locationId);
            }
            else
            {
                //imgLineLogo.ImageUrl = "";
                ddlBLno.SelectedValue = "0";

                /*
                txtGrossWeightTON.Text = "";
                txtTEU.Text = "";
                txtFFU.Text = "";
                txtVolume.Text = "";
                txtBLdate.Text = "";
                 */

                ViewState["GROSSWEIGHT"] = null;
                ViewState["NOOFTEU"] = null;
                ViewState["NOOFFEU"] = null;
                ViewState["VOLUME"] = null;
                ViewState["BLDATE"] = null;

                //txtBooking.Text = "";
                ddlFTerminal.SelectedValue = "0";
            }
        }

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            long nvoccId = Convert.ToInt64(ddlNvocc.SelectedValue);
            long locationId = Convert.ToInt64(ddlLocation.SelectedValue);

            LoadBLNoDDL(nvoccId, locationId);
        }

        protected void ddlBLno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBLno.SelectedValue != "0")
            {
                GrossWeight(ddlBLno.SelectedItem.Text);
                TEU(ddlBLno.SelectedItem.Text);
                //FEU(ddlBLno.SelectedItem.Text);
                //Volume(ddlBLno.SelectedItem.Text);
                /*
                BLdate(ddlBLno.SelectedItem.Text);
                */
                LoadExchangeRate();
                SetDefaultTerminal();
            }
            else
            {
                /*
                txtGrossWeightTON.Text = "";
                txtTEU.Text = "";
                txtFFU.Text = "";
                txtVolume.Text = "";
                txtBLdate.Text = "";
                 */

                ViewState["GROSSWEIGHT"] = null;
                ViewState["NOOFTEU"] = null;
                ViewState["NOOFFEU"] = null;
                ViewState["VOLUME"] = null;
                ViewState["BLDATE"] = null;

                //txtBooking.Text = "";
                ddlFTerminal.SelectedValue = "0";
            }
        }


        private void CalculateCharge()
        {
            decimal grossAmount = 0;
            decimal totalAmount = 0;
            decimal serviceTax = 0;
            decimal cessAmount = 0;
            decimal addCess = 0;

            decimal Teu = Convert.ToDecimal(txtRatePerTEU.Text);
            decimal Feu = Convert.ToDecimal(txtRateperFEU.Text);
            decimal BL = Convert.ToDecimal(txtRatePerBL.Text);
            decimal CBM = Convert.ToDecimal(txtRatePerCBM.Text);
            decimal Ton = Convert.ToDecimal(txtRatePerTon.Text);

            decimal TaxPer = 0;
            decimal TaxCess = 0;
            decimal TaxAddCess = 0;

            grossAmount = (Teu + Feu + BL + CBM + Ton);
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

        protected void txtRatePerTEU_TextChanged(object sender, EventArgs e)
        {
            CalculateCharge();
        }

        protected void txtRateperFEU_TextChanged(object sender, EventArgs e)
        {
            CalculateCharge();
        }

        protected void txtRatePerBL_TextChanged(object sender, EventArgs e)
        {
            CalculateCharge();
        }

        protected void txtRatePerCBM_TextChanged(object sender, EventArgs e)
        {
            CalculateCharge();
        }
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
        protected void txtRatePerTon_TextChanged(object sender, EventArgs e)
        {
            CalculateCharge();
        }
        protected void ddlChargeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //List<IChargeRate> chargeRates = new InvoiceBLL().GetInvoiceCharges_New(Convert.ToInt64(ddlBLno.SelectedValue), Convert.ToInt32(ddlFChargeName.SelectedValue), Convert.ToInt32(ddlFTerminal.SelectedValue), Convert.ToDecimal(txtExchangeRate.Text), 0, "", Convert.ToDateTime(txtInvoiceDate.Text));
            //List<IExpChargeRate> chargeRates = new InvoiceBLL().GetExpInvoiceCharges_New(Convert.ToInt64(ddlBLno.SelectedValue), Convert.ToInt32(ddlFChargeName.SelectedValue), Convert.ToInt32(ddlFTerminal.SelectedValue), Convert.ToDecimal(0), 0, "", Convert.ToDateTime(txtInvoiceDate.Text));

            List<IChargeRate> chargeRates = new InvoiceBLL().GetExpInvoiceCharges(Convert.ToInt64(ddlBLno.SelectedValue), Convert.ToInt32(ddlFChargeName.SelectedValue), Convert.ToInt32(ddlFTerminal.SelectedValue), Convert.ToInt32(0), Convert.ToDateTime(txtInvoiceDate.Text));


            DataTable Charge = new InvoiceBLL().ChargeEditable(Convert.ToInt32(ddlFChargeName.SelectedValue));

            if (Charge != null && Charge.Rows.Count > 0)
            {
                if (Convert.ToBoolean(Charge.Rows[0]["RateChangeable"].ToString()))
                {
                    int ChargeType = Convert.ToInt32(Charge.Rows[0]["ChargeType"].ToString());

                    if (ChargeType == (int)Enums.ExportChargeType.PER_UNIT)
                    {
                        txtRatePerTEU.Enabled = true;
                        txtRateperFEU.Enabled = true;

                        txtRatePerBL.Enabled = false;
                        txtRatePerCBM.Enabled = false;
                        txtRatePerTon.Enabled = false;
                    }
                    else if (ChargeType == (int)Enums.ExportChargeType.PER_DOCUMENT)
                    {
                        txtRatePerBL.Enabled = true;

                        txtRatePerTEU.Enabled = false;
                        txtRateperFEU.Enabled = false;
                        txtRatePerCBM.Enabled = false;
                        txtRatePerTon.Enabled = false;
                    }
                    else if (ChargeType == (int)Enums.ExportChargeType.PER_CBM)
                    {
                        txtRatePerCBM.Enabled = true;
                        txtRatePerTon.Enabled = true;

                        txtRatePerTEU.Enabled = false;
                        txtRateperFEU.Enabled = false;
                        txtRatePerBL.Enabled = false;
                    }
                    else if (ChargeType == (int)Enums.ExportChargeType.PER_TON)
                    {
                        txtRatePerCBM.Enabled = true;
                        txtRatePerTon.Enabled = true;

                        txtRatePerTEU.Enabled = false;
                        txtRateperFEU.Enabled = false;
                        txtRatePerBL.Enabled = false;
                    }
                    else if (ChargeType == (int)Enums.ExportChargeType.TYPE_SIZE)
                    {
                        txtRatePerTEU.Enabled = true;
                        txtRateperFEU.Enabled = true;

                        txtRatePerBL.Enabled = false;
                        txtRatePerCBM.Enabled = false;
                        txtRatePerTon.Enabled = false;
                    }
                }
                else
                {
                    txtRatePerTEU.Enabled = false;
                    txtRateperFEU.Enabled = false;
                    txtRatePerBL.Enabled = false;
                    txtRatePerCBM.Enabled = false;
                    txtRatePerTon.Enabled = false;
                }

                string currency = Charge.Rows[0]["Currency"].ToString();
                ddlCurrency.SelectedValue = currency;

                if (ddlCurrency.SelectedValue == "1")
                {
                    custTxtConvRate.Text = "1.00";
                    custTxtConvRate.Enabled = false;
                }
                else if (ddlCurrency.SelectedValue == "2")
                {
                    decimal exchangeRate = new InvoiceBLL().GetExchangeRateForExport(Convert.ToInt64(ddlBLno.SelectedValue));
                    custTxtConvRate.Enabled = true;
                    custTxtConvRate.Text = exchangeRate.ToString();
                }
                else
                {
                    custTxtConvRate.Enabled = true;
                    custTxtConvRate.Text = "0.00";
                }
            }

            if (Convert.ToBoolean(Charge.Rows[0]["TerminalReq"].ToString()))
            {
                SetDefaultTerminal();
                //ddlFTerminal.Enabled = true;
                rfvTerminal.Visible = true;
            }
            else
            {
                //ddlFTerminal.SelectedValue = "0";
                SetDefaultTerminal();
                //ddlFTerminal.Enabled = false;
                rfvTerminal.Visible = false;
            }

            if (chargeRates != null && chargeRates.Count > 0)
            {
                txtRatePerTEU.Text = chargeRates[0].RatePerTEU.ToString();
                txtRateperFEU.Text = chargeRates[0].RatePerFEU.ToString();
                txtRatePerBL.Text = chargeRates[0].RatePerBL.ToString();
                txtRatePerCBM.Text = chargeRates[0].RatePerCBM.ToString();
                txtRatePerTon.Text = chargeRates[0].RatePerTON.ToString();
                //  txtUSD.Text = chargeRates[0].Usd.ToString();
                txtGrossAmount.Text = chargeRates[0].GrossAmount.ToString();
                txtServiceTax.Text = (chargeRates[0].ServiceTax + chargeRates[0].ServiceTaxCessAmount + chargeRates[0].ServiceTaxACess).ToString();
                txtTotal.Text = (chargeRates[0].TotalAmount).ToString();
                //+ chargeRates[0].STax + chargeRates[0].ServiceTaxCessAmount + chargeRates[0].ServiceTaxACess).ToString();

                //ViewState["RATEPERPTEU"] = chargeRates[0].SharingTEU;
                //ViewState["RATEPERPFEU"] = chargeRates[0].SharingFEU;
                //ViewState["RATEPERPBL"] = chargeRates[0].SharingBL;
                ViewState["CESSAMOUNT"] = chargeRates[0].ServiceTaxCessAmount;
                ViewState["ADDCESS"] = chargeRates[0].ServiceTaxACess;
                ViewState["STAX"] = chargeRates[0].ServiceTax;
            }
            else
            {
                txtRatePerTEU.Text = "0.00";
                txtRateperFEU.Text = "0.00";
                txtRatePerBL.Text = "0.00";
                txtRatePerCBM.Text = "0.00";
                txtRatePerTon.Text = "0.00";
                //  txtUSD.Text = "0.00";
                txtGrossAmount.Text = "0.00";
                txtServiceTax.Text = "0.00";
                txtTotal.Text = "0.00";

                ViewState["RATEPERPTEU"] = null;
                ViewState["RATEPERPFEU"] = null;
                ViewState["RATEPERPBL"] = null;
                ViewState["CESSAMOUNT"] = null;
                ViewState["ADDCESS"] = null;
                ViewState["STAX"] = null;
            }
        }

        /*
        protected void ddlChargeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal ratePerTeu = 0;
            decimal ratePerFeu = 0;
            decimal ratePerPTeu = 0;
            decimal ratePerPFeu = 0;

            decimal ratePerBL = 0;
            decimal ratePerPBL = 0;

            decimal TaxPer = 0;
            decimal TaxCess = 0;
            decimal TaxAddCess = 0;

            decimal ratePerCBM = 0;
            decimal ratePerTon = 0;

            decimal grossAmount = 0;
            decimal serviceTax = 0;
            decimal cessAmount = 0;
            decimal addCess = 0;
            decimal totalAmount = 0;

            decimal detentionAmount = 0;

            List<ICharge> lstCharge = Session["CHARGES"] as List<ICharge>;
            ICharge charge = null; //= lstCharge.Where(c => c.ChargesID == Convert.ToInt32(ddlFChargeName.SelectedValue)) as ICharge;

            int chargeId = Convert.ToInt32(ddlFChargeName.SelectedValue);

            foreach (ICharge charg in lstCharge)
            {
                if (charg.ChargesID == chargeId)
                {
                    charge = charg;
                    break;
                }
            }

            if (charge != null)
            {
                //if (charge.IsWashing)
                //{
                //    ddlFWashingType.Enabled = true;
                //    rfvWashing.Visible = true;
                //}
                //else
                //{
                //    ddlFWashingType.Enabled = false;
                //    rfvWashing.Visible = false;
                //}

                //if (charge.IsTerminal)
                //{
                //    LoadTerminalDDL();
                //    //ddlFTerminal.Enabled = true;
                //    //rfvTerminal.Visible = true;
                //}
                //else
                //{
                //    //ddlFTerminal.SelectedValue = "0";
                //    //ddlFTerminal.Enabled = false;
                //    //rfvTerminal.Visible = false;
                //}

                int terminalId = 0;

                if (charge.IsTerminal)
                {
                    terminalId = Convert.ToInt32(//ddlFTerminal.SelectedValue);
                }

                List<IChargeRate> chargeRates = new InvoiceBLL().GetAllChargeRate(Convert.ToInt32(ddlFChargeName.SelectedValue), Convert.ToInt64(ddlLocation.SelectedValue), terminalId); //, Convert.ToInt32(ddlFWashingType.SelectedValue)

                if (chargeRates != null && chargeRates.Count > 0)
                {
                    DataTable dtSTax = new InvoiceBLL().GetServiceTax(Convert.ToDateTime(txtInvoiceDate.Text));

                    if (dtSTax != null && dtSTax.Rows.Count > 0)
                    {
                        TaxPer = Convert.ToDecimal(dtSTax.Rows[0]["TaxPer"].ToString());
                        TaxCess = Convert.ToDecimal(dtSTax.Rows[0]["TaxCess"].ToString());
                        TaxAddCess = Convert.ToDecimal(dtSTax.Rows[0]["TaxAddCess"].ToString());
                    }

                    if (charge.ChargeType == (int)Enums.ChargeType.PER_UNIT)
                    {
                        ratePerTeu = chargeRates[0].RatePerTEU;
                        ratePerFeu = chargeRates[0].RatePerFEU;

                        if (charge.PrincipleSharing)
                        {
                            ratePerPTeu = chargeRates[0].SharingTEU * Convert.ToInt32(ViewState["NOOFTEU"]);
                            ratePerPFeu = chargeRates[0].SharingFEU * Convert.ToInt32(ViewState["NOOFFEU"]);
                        }

                        txtRatePerTEU.Text = ratePerTeu.ToString();
                        txtRateperFEU.Text = ratePerFeu.ToString();
                        grossAmount = ((ratePerTeu * Convert.ToInt32(ViewState["NOOFTEU"])) + (ratePerFeu * Convert.ToInt32(ViewState["NOOFFEU"])));

                        txtGrossAmount.Text = grossAmount.ToString();

                        if (charge.ServiceTax)
                        {
                            serviceTax = (grossAmount * TaxPer) / 100;
                            cessAmount = (serviceTax * TaxCess) / 100;
                            addCess = (serviceTax * TaxAddCess) / 100;
                        }

                        txtServiceTax.Text = serviceTax.ToString();
                        totalAmount = (grossAmount + serviceTax + cessAmount + addCess);

                        txtTotal.Text = totalAmount.ToString();
                    }
                    else if (charge.ChargeType == (int)Enums.ChargeType.PER_DOCUMENT)
                    {
                        ratePerBL = chargeRates[0].RatePerBL;

                        if (charge.PrincipleSharing)
                        {
                            ratePerPBL = chargeRates[0].SharingBL;
                        }

                        txtRatePerBL.Text = ratePerBL.ToString();
                        txtGrossAmount.Text = ratePerBL.ToString();

                        if (charge.ServiceTax)
                        {
                            serviceTax = (ratePerPBL * TaxPer) / 100;
                            cessAmount = (serviceTax * TaxCess) / 100;
                            addCess = (serviceTax * TaxAddCess) / 100;
                        }

                        txtServiceTax.Text = serviceTax.ToString();
                        totalAmount = (ratePerPBL + serviceTax + cessAmount + addCess);

                        txtTotal.Text = totalAmount.ToString();
                    }
                    else if (charge.ChargeType == (int)Enums.ChargeType.LCL)
                    {
                        ratePerCBM = chargeRates[0].RatePerCBM;
                        ratePerTon = chargeRates[0].RatePerTON;

                        txtRatePerCBM.Text = ratePerCBM.ToString();
                        txtRatePerTon.Text = ratePerTon.ToString();

                        if ((ratePerCBM * Convert.ToDecimal(ViewState["GROSSWEIGHT"])) > (ratePerTon * Convert.ToDecimal(ViewState["VOLUME"])))
                        {
                            grossAmount = (ratePerCBM * Convert.ToDecimal(ViewState["GROSSWEIGHT"]));
                        }
                        else
                        {
                            grossAmount = (ratePerTon * Convert.ToDecimal(ViewState["VOLUME"]));
                        }

                        txtGrossAmount.Text = grossAmount.ToString();

                        if (charge.ServiceTax)
                        {
                            serviceTax = (grossAmount * TaxPer) / 100;
                            cessAmount = (serviceTax * TaxCess) / 100;
                            addCess = (serviceTax * TaxAddCess) / 100;
                        }

                        txtServiceTax.Text = serviceTax.ToString();
                        totalAmount = (grossAmount + serviceTax + cessAmount + addCess);

                        txtTotal.Text = totalAmount.ToString();
                    }
                    else if (charge.ChargeType == (int)Enums.ChargeType.SLAB)
                    {
                        int numOfContainer = new InvoiceBLL().GetNumberOfContainer(Convert.ToInt32(ddlBLno.SelectedValue));
                        //ratePerBL = chargeRates[0].RatePerBL;

                        foreach (var chargeRate in chargeRates)
                        {
                            if (chargeRate.Low >= numOfContainer && chargeRate.High <= numOfContainer)
                            {
                                ratePerBL = chargeRate.RatePerBL;
                                break;
                            }
                        }
                        txtRatePerBL.Text = ratePerBL.ToString();
                        txtGrossAmount.Text = ratePerBL.ToString();

                        if (charge.ServiceTax)
                        {
                            serviceTax = (ratePerPBL * TaxPer) / 100;
                            cessAmount = (serviceTax * TaxCess) / 100;
                            addCess = (serviceTax * TaxAddCess) / 100;
                        }

                        txtServiceTax.Text = serviceTax.ToString();
                        totalAmount = (ratePerPBL + serviceTax + cessAmount + addCess);
                        txtTotal.Text = totalAmount.ToString();
                    }
                    else if (charge.ChargeType == (int)Enums.ChargeType.DETENTION || charge.ChargeType == (int)Enums.ChargeType.PORT_GROUND_RENT)
                    {
                        detentionAmount = new InvoiceBLL().GetDetentionAmount(Convert.ToInt32(ddlBLno.SelectedValue));

                        if (detentionAmount > 0)
                        {
                            txtGrossAmount.Text = detentionAmount.ToString();

                            if (charge.ServiceTax)
                            {
                                serviceTax = (detentionAmount * TaxPer) / 100;
                                cessAmount = (serviceTax * TaxCess) / 100;
                                addCess = (serviceTax * TaxAddCess) / 100;
                            }

                            txtServiceTax.Text = serviceTax.ToString();
                            totalAmount = (detentionAmount + serviceTax + cessAmount + addCess);
                            txtTotal.Text = totalAmount.ToString();
                        }
                        else if (detentionAmount == 0)
                        {
                            //No detention amount
                        }
                        else
                        {
                            //All containers should have RCVE status
                        }
                    }

                    ViewState["RATEPERPTEU"] = ratePerPTeu;
                    ViewState["RATEPERPFEU"] = ratePerPFeu;
                    ViewState["RATEPERPBL"] = ratePerPBL;
                    ViewState["CESSAMOUNT"] = cessAmount;
                    ViewState["ADDCESS"] = addCess;
                }
            }
        }
        */

        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCurrency.SelectedValue == "1")
            {
                custTxtConvRate.Text = "1.00";
                custTxtConvRate.Enabled = false;
            }
            else if (ddlCurrency.SelectedValue == "2")
            {
                decimal exchangeRate = new InvoiceBLL().GetExchangeRateForExport(Convert.ToInt64(ddlBLno.SelectedValue));
                custTxtConvRate.Enabled = true;
                custTxtConvRate.Text = exchangeRate.ToString();
            }
            else
            {
                custTxtConvRate.Enabled = true;
                custTxtConvRate.Text = "0.00";
            }
        }

        /*
        protected void txtExchangeRate_OnTextChanged(object sender, EventArgs e)
        {
            if (ReferenceEquals(Request.QueryString["invid"], null))
            {
                string blNo = string.Empty;
                int docTypeId = 0;
                string misc = string.Empty;

                blNo = Request.QueryString["p1"].ToString();
                Int32.TryParse(Request.QueryString["p3"].ToString(), out docTypeId);
                misc = Request.QueryString["p4"].ToString();

                LoadForBLQuery(blNo, docTypeId, misc);
            }
            else
            {

            }
        }
        */

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
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TerminalName"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "RatePerBL"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "RatePerTEU"));
                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "RateperFEU"));
                e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "RatePerCBM"));
                e.Row.Cells[6].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "RatePerTON"));
                e.Row.Cells[7].Text = Convert.ToString(GetCurrencyCode(Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "fk_CurrencyID"))));
                e.Row.Cells[8].Text = Convert.ToString(Math.Round(Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ExchgRate")), 2));
                e.Row.Cells[9].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "GrossAmount"));
                e.Row.Cells[10].Text = Convert.ToString(Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ServiceTax")) + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ServiceTaxCessAmount")) + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ServiceTaxACess")));
                e.Row.Cells[11].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TotalAmount"));

                if (!ReferenceEquals(Request.QueryString["invid"], null))
                {
                    ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");

                    btnRemove.Visible = false;
                    btnEdit.Visible = false;
                }
                else
                {
                    if (Convert.ToInt32(ddlInvoiceType.SelectedValue) == 19)
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
                    }
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


            //misc = GeneralFunctions.DecryptQueryString(Request.QueryString["p4"].ToString());

            //Add-Update
            long invoiceID = new InvoiceBLL().SaveInvoiceExp(invoice, misc, chargeRate, _isedit);

            if (invoiceID == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('No landing date. Invoice aborted');</script>", false);
            }
            else
            {
                string invoiceNo = new InvoiceBLL().GetInvoiceNoById(invoiceID);

                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('Record saved successfully! Invoice Number: " + invoiceNo + "');</script>", false);
            }
            Response.Redirect("~/Export/Export-bl-query.aspx?BLNumber=" + GeneralFunctions.EncryptQueryString(ddlBLno.SelectedItem.Text));

            //ViewState["CHARGERATE"] = null;
            //gvwInvoice.DataSource = null;
            //gvwInvoice.DataBind();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Export/Export-bl-query.aspx?BLNumber=" + GeneralFunctions.EncryptQueryString(ddlBLno.SelectedItem.Text));
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddChargesRate();
        }


        private void AddChargesRate()
        {
            IChargeRate chargeRate = new ChargeRateEntity();
            BuildChargesRate(chargeRate);

            //if (ChargeRates == null)
            //{
            //    ChargeRates = new List<IChargeRate>();
            //}

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

            invoice.Account = rdblAccountFor.SelectedValue;
            invoice.AddedOn = DateTime.Now;

            if (rdoAllInFreight.SelectedValue == "Yes")
                invoice.AllInFreight = true;
            else
                invoice.AllInFreight = false;

            /*
            if (rdblAllinFreight.SelectedValue == "0")
                invoice.AllInFreight = false;
            else
                invoice.AllInFreight = true;
             */

            //invoice.AllInFreight = false;

            invoice.BLID = Convert.ToInt64(ddlBLno.SelectedValue);

            /*
            invoice.CHAID = Convert.ToInt32(ddlCHAid.SelectedValue);
             */
            invoice.CHAID = 0;

            invoice.CompanyID = 1;
            invoice.EditedOn = DateTime.Now;
            invoice.ExportImport = "E";
            invoice.GrossAmount = chargeRate.Sum(c => c.GrossAmount);

            invoice.InvoiceDate = Convert.ToDateTime(txtInvoiceDate.Text);

            invoice.InvoiceTypeID = Convert.ToInt32(ddlInvoiceType.SelectedValue);
            invoice.LocationID = Convert.ToInt32(ddlLocation.SelectedValue);
            invoice.NVOCCID = Convert.ToInt32(ddlNvocc.SelectedValue);
            invoice.ServiceTax = chargeRate.Sum(c => c.ServiceTax);
            invoice.ServiceTaxACess = chargeRate.Sum(c => c.ServiceTaxACess);
            invoice.ServiceTaxCess = chargeRate.Sum(c => c.ServiceTaxCessAmount);
            invoice.Roff = Convert.ToDecimal(txtROff.Text);

            invoice.UserAdded = _userId;
            invoice.UserLastEdited = _userId;

            invoice.InvoiceNo = txtInvoiceNo.Text;

            //if (txtExchangeRate.Text != string.Empty)
            //    invoice.XchangeRate = Convert.ToDecimal(txtExchangeRate.Text);
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
            charge.RatePerCBM = Convert.ToDecimal(txtRatePerCBM.Text);
            charge.RatePerFEU = Convert.ToDecimal(txtRateperFEU.Text);
            charge.RatePerTEU = Convert.ToDecimal(txtRatePerTEU.Text);
            charge.RatePerTON = Convert.ToDecimal(txtRatePerTon.Text);
            //charge.RatePerTon = Convert.ToDecimal(custTxtConvRate.Text);
            charge.fk_CurrencyID = Convert.ToInt32(ddlCurrency.SelectedValue);
            charge.ExchgRate = Math.Round(Convert.ToDecimal(custTxtConvRate.Text), 2);
            if (ViewState["STAX"] != null)
                charge.ServiceTax = Convert.ToDecimal(ViewState["STAX"]);  //Convert.ToDecimal(txtServiceTax.Text);

            //if (ViewState["RATEPERPBL"] != null)
            //    charge.SharingBL = Convert.ToDecimal(ViewState["RATEPERPBL"]);
            //if (ViewState["RATEPERPFEU"] != null)
            //    charge.SharingFEU = Convert.ToDecimal(ViewState["RATEPERPFEU"]);
            //if (ViewState["RATEPERPTEU"] != null)
            //    charge.SharingTEU = Convert.ToDecimal(ViewState["RATEPERPTEU"]);

            if (ViewState["CESSAMOUNT"] != null)
                charge.ServiceTaxCessAmount = Convert.ToDecimal(ViewState["CESSAMOUNT"]);
            if (ViewState["ADDCESS"] != null)
                charge.ServiceTaxACess = Convert.ToDecimal(ViewState["ADDCESS"]);

            //charge.STax = Convert.ToDecimal(txtServiceTax.Text);

            if (Convert.ToInt32(ddlFTerminal.SelectedValue) != 0)
            {
                charge.TerminalId = Convert.ToInt32(ddlFTerminal.SelectedValue);
                charge.TerminalName = Convert.ToString(ddlFTerminal.SelectedItem.Text);
            }

            charge.TotalAmount = Convert.ToDecimal(txtTotal.Text);
            //    charge.Usd = Convert.ToDecimal(//txtUSD.Text);

            //if (Convert.ToInt32(ddlFWashingType.SelectedValue) != 0)
            //{
            //    charge.WashingType = Convert.ToInt32(ddlFWashingType.SelectedValue);
            //    charge.WashingTypeName = Convert.ToString(ddlFWashingType.SelectedItem.Text);
            //}
        }

        private void ClearChargesRate()
        {
            ddlFChargeName.SelectedValue = "0";
            txtGrossAmount.Text = "0.00";
            txtRatePerBL.Text = "0.00";
            txtRatePerCBM.Text = "0.00";
            txtRateperFEU.Text = "0.00";
            txtRatePerTEU.Text = "0.00";
            txtRatePerTon.Text = "0.00";
            ////ddlFTerminal.SelectedValue = "0";
            //ddlFWashingType.SelectedValue = "0";
            txtTotal.Text = "0.00";
            //txtUSD.Text = "0.00";
            txtServiceTax.Text = "0.00";
            txtGrossAmount.Text = "0.00";
            custTxtConvRate.Text = "0.00";
            ddlCurrency.SelectedIndex = 0;
            if (Convert.ToInt32(ddlInvoiceType.SelectedValue) == 19)
            {
                txtRatePerTEU.Enabled = false;
                txtRateperFEU.Enabled = false;
                txtRatePerBL.Enabled = false;
                txtRatePerCBM.Enabled = false;
                txtRatePerTon.Enabled = false;
                ddlFChargeName.Enabled = false;
            }
            else
            {
                txtRatePerTEU.Enabled = true;
                txtRateperFEU.Enabled = true;
                txtRatePerBL.Enabled = true;
                txtRatePerCBM.Enabled = true;
                txtRatePerTon.Enabled = true;
                ddlFChargeName.Enabled = true;
            }
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
            IInvoice invoice = new InvoiceBLL().GetExpInvoiceById(InvoiceId);

            ViewState["InvoiceID"] = invoice.InvoiceID;


            // rdblAccountFor.SelectedValue = invoice.Account;

            
            if (invoice.AllInFreight)
                rdoAllInFreight.SelectedValue = "Yes";
            else
                rdoAllInFreight.SelectedValue = "No";
             

            ddlBLno.SelectedValue = Convert.ToString(invoice.BLID);

            /*
            ddlCHAid.SelectedValue = Convert.ToString(invoice.CHAID);
             */
            /*
            txtGrossAmount.Text = invoice.GrossAmount.ToString();
             */


            txtBLDate.Text = invoice.BLDate.ToShortDateString();
            txtBooking.Text = invoice.BookingNo;
            
            txtInvoiceDate.Text = invoice.InvoiceDate.ToShortDateString();
            txtInvoiceNo.Text = invoice.InvoiceNo;
            ddlInvoiceType.SelectedValue = invoice.InvoiceTypeID.ToString();
            ddlLocation.SelectedValue = invoice.LocationID.ToString();
            ddlNvocc.SelectedValue = invoice.NVOCCID.ToString();
            //txtServiceTax.Text = invoice.ServiceTax.ToString();
            // txtExchangeRate.Text = invoice.XchangeRate.ToString();

            long nvoccId = Convert.ToInt64(ddlNvocc.SelectedValue);
            long locationId = Convert.ToInt64(ddlLocation.SelectedValue);

            LoadBLNoDDL(nvoccId, locationId);

            GrossWeight(ddlBLno.SelectedItem.Text);
            TEU(ddlBLno.SelectedItem.Text);
            //FEU(ddlBLno.SelectedItem.Text);
            //Volume(ddlBLno.SelectedItem.Text);
            /*
            BLdate(ddlBLno.SelectedItem.Text);
            */

            LoadExchangeRate();
            SetDefaultTerminal();

            txtContainers.Text = ViewState["NOOFTEU"].ToString() + " x 20' & " + ViewState["NOOFFEU"] + " x 40'";

            //For Charge Rates
            List<IChargeRate> chargeRates = new InvoiceBLL().GetExpInvoiceChargesById(InvoiceId);

            ViewState["CHARGERATE"] = chargeRates;
            txtInvoiceDate.Enabled = false;
            txtUSDExRate.Enabled = false;
            btnAdd.Enabled = false;
            txtROff.Text = (Math.Round(chargeRates.Sum(cr => cr.TotalAmount), 0) - chargeRates.Sum(cr => cr.TotalAmount)).ToString();
            txtTotalAmount.Text = Math.Round(chargeRates.Sum(cr => cr.TotalAmount), 0).ToString();

            gvwInvoice.DataSource = chargeRates;
            gvwInvoice.DataBind();
        }

        private void LoadForBLQuery(string BlNo, int DocType, string Misc)
        {
            //For Invoice
            //IInvoice invoice = null;//new InvoiceBLL().GetInvoiceById(InvoiceId);


            DataTable dt = new InvoiceBLL().GetExpLineLocation(BlNo);

            int line = Convert.ToInt32(dt.Rows[0]["fk_NVOCCID"]);
            int location = Convert.ToInt32(dt.Rows[0]["fk_LocationID"]);
            long blId = Convert.ToInt64(dt.Rows[0]["pk_ExpBLID"]);

            txtInvoiceDate.Text = DateTime.Now.ToShortDateString();
            ddlInvoiceType.SelectedValue = DocType.ToString();
            ddlLocation.SelectedValue = location.ToString();
            ddlNvocc.SelectedValue = line.ToString();

            //GrossWeight(GeneralFunctions.DecryptQueryString(Request.QueryString["p1"].ToString()));
            //TEU(GeneralFunctions.DecryptQueryString(Request.QueryString["p1"].ToString()));
            //FEU(GeneralFunctions.DecryptQueryString(Request.QueryString["p1"].ToString()));
            //Volume(GeneralFunctions.DecryptQueryString(Request.QueryString["p1"].ToString()));

            /*
            BLdate(Request.QueryString["BLNo"].ToString());
            */

            //txtContainers.Text = ViewState["NOOFTEU"].ToString() + " x 20' & " + ViewState["NOOFFEU"] + " x 40'";

            LoadBLNoDDL(line, location);
            ddlBLno.SelectedValue = blId.ToString();

            //LoadExchangeRate();
            //SetDefaultTerminal();

            //For Charge Rates
            //List<IChargeRate> chargeRates = new InvoiceBLL().GetInvoiceCharges_New(Convert.ToInt64(ddlBLno.SelectedValue), Convert.ToInt32(ddlFChargeName.SelectedValue), Convert.ToInt32(ddlFTerminal.SelectedValue), Convert.ToDecimal(txtExchangeRate.Text), DocType, Misc, Convert.ToDateTime(txtInvoiceDate.Text));
            //var chargeRates = InvoiceBLL.GetExpInvoiceCharges_New(Convert.ToInt64(ddlBLno.SelectedValue), 0, Convert.ToInt32(ddlFTerminal.SelectedValue), DocType, Convert.ToDateTime(txtInvoiceDate.Text));
            List<IChargeRate> chargeRates = new InvoiceBLL().GetExpInvoiceCharges(Convert.ToInt64(ddlBLno.SelectedValue), Convert.ToInt32(ddlFChargeName.SelectedValue), Convert.ToInt32(ddlFTerminal.SelectedValue), DocType, Convert.ToDateTime(txtInvoiceDate.Text));

            ViewState["CHARGERATE"] = chargeRates;

            if (chargeRates != null && chargeRates.Count > 0)
            {
                if (chargeRates.Min(c => c.InvoiceChargeId) < 0)
                    ViewState["INVOICECHARGEID"] = chargeRates.Min(c => c.InvoiceChargeId);
                else
                    ViewState["INVOICECHARGEID"] = null;

                var Cur = chargeRates.Where(c => c.fk_CurrencyID == 2).FirstOrDefault().ExchgRate;
                txtUSDExRate.Text = Cur.ToString();
            }
            else
            {   
                ViewState["INVOICECHARGEID"] = null;
            }



            LoadChargeDDL(DocType);

            if (DocType == 9)
            {

                ddlFChargeName.Enabled = true;
            }
            else
            {
                ddlFChargeName.Enabled = false;
            }

            gvwInvoice.DataSource = chargeRates;
            gvwInvoice.DataBind();


            //Update Invoice Amount
            txtROff.Text = (Math.Round(chargeRates.Sum(cr => cr.TotalAmount), 0) - chargeRates.Sum(cr => cr.TotalAmount)).ToString();
            txtTotalAmount.Text = Math.Round(chargeRates.Sum(cr => cr.TotalAmount), 0).ToString();
        }

        private void EditChargeRate(int InvoiceChargeId)
        {
            ddlFChargeName.Enabled = false;

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

                    if (ChargeType == (int)Enums.ExportChargeType.PER_UNIT)
                    {
                        txtRatePerTEU.Enabled = true;
                        txtRateperFEU.Enabled = true;

                        txtRatePerBL.Enabled = false;
                        txtRatePerCBM.Enabled = false;
                        txtRatePerTon.Enabled = false;
                    }
                    else if (ChargeType == (int)Enums.ExportChargeType.PER_DOCUMENT)
                    {
                        txtRatePerBL.Enabled = true;

                        txtRatePerTEU.Enabled = false;
                        txtRateperFEU.Enabled = false;
                        txtRatePerCBM.Enabled = false;
                        txtRatePerTon.Enabled = false;
                    }
                    else if (ChargeType == (int)Enums.ExportChargeType.PER_CBM)
                    {
                        txtRatePerCBM.Enabled = true;
                        txtRatePerTon.Enabled = true;

                        txtRatePerTEU.Enabled = false;
                        txtRateperFEU.Enabled = false;
                        txtRatePerBL.Enabled = false;
                    }
                    else if (ChargeType == (int)Enums.ExportChargeType.PER_TON)
                    {
                        txtRatePerCBM.Enabled = true;
                        txtRatePerTon.Enabled = true;

                        txtRatePerTEU.Enabled = false;
                        txtRateperFEU.Enabled = false;
                        txtRatePerBL.Enabled = false;
                    }
                    else if (ChargeType == (int)Enums.ExportChargeType.TYPE_SIZE)
                    {
                        txtRatePerTEU.Enabled = true;
                        txtRateperFEU.Enabled = true;

                        txtRatePerBL.Enabled = false;
                        txtRatePerCBM.Enabled = false;
                        txtRatePerTon.Enabled = false;
                    }
                }
                else
                {
                    txtRatePerTEU.Enabled = false;
                    txtRateperFEU.Enabled = false;
                    txtRatePerBL.Enabled = false;
                    txtRatePerCBM.Enabled = false;
                    txtRatePerTon.Enabled = false;
                }
            }

            if (Convert.ToBoolean(Charge.Rows[0]["TerminalReq"].ToString()))
            {
                SetDefaultTerminal();
                ////ddlFTerminal.Enabled = true;
                rfvTerminal.Visible = true;
            }
            else
            {
                ////ddlFTerminal.SelectedValue = "0";
                SetDefaultTerminal();
                ////ddlFTerminal.Enabled = false;
                rfvTerminal.Visible = false;
            }

            ddlFChargeName.SelectedValue = chargeRate.ChargesID.ToString();
            txtGrossAmount.Text = chargeRate.GrossAmount.ToString();
            txtRatePerBL.Text = chargeRate.RatePerBL.ToString();
            txtRatePerCBM.Text = chargeRate.RatePerCBM.ToString();
            txtRateperFEU.Text = chargeRate.RatePerFEU.ToString();
            txtRatePerTEU.Text = chargeRate.RatePerTEU.ToString();
            txtRatePerTon.Text = chargeRate.RatePerTON.ToString();
            txtServiceTax.Text = (chargeRate.STax + chargeRate.ServiceTaxCessAmount + chargeRate.ServiceTaxACess).ToString();
            ddlCurrency.SelectedValue = chargeRate.fk_CurrencyID.ToString();

            if (chargeRate.fk_CurrencyID.ToString() == "1")
            {
                custTxtConvRate.Enabled = false;
            }
            else
            {
                custTxtConvRate.Enabled = true;
            }

            custTxtConvRate.Text = chargeRate.ExchgRate.ToString("0.00");
            ViewState["STAX"] = chargeRate.STax;
            ViewState["CESSAMOUNT"] = chargeRate.ServiceTaxCessAmount;
            ViewState["ADDCESS"] = chargeRate.ServiceTaxACess;

            ddlFTerminal.SelectedValue = chargeRate.TerminalId.ToString();
            txtTotal.Text = chargeRate.TotalAmount.ToString();
            //txtUSD.Text = chargeRate.Usd.ToString();

            ViewState["EDITINVOICECHARGEID"] = chargeRate.InvoiceChargeId;
        }

        protected void txtUSDExRate_TextChanged(object sender, EventArgs e)
        {
            List<IChargeRate> lstData = ViewState["CHARGERATE"] as List<IChargeRate>;
            decimal TaxPer = 0;
            decimal TaxCess = 0;
            decimal TaxAddCess = 0;

            DataTable dtSTax = new InvoiceBLL().GetServiceTax(Convert.ToDateTime(txtInvoiceDate.Text));

            if (dtSTax != null && dtSTax.Rows.Count > 0)
            {
                TaxPer = Convert.ToDecimal(dtSTax.Rows[0]["TaxPer"].ToString());
                TaxCess = Convert.ToDecimal(dtSTax.Rows[0]["TaxCess"].ToString());
                TaxAddCess = Convert.ToDecimal(dtSTax.Rows[0]["TaxAddCess"].ToString());
            }

            lstData.Where(d => d.fk_CurrencyID == 2)
                .Select(d =>
                    {
                        d.ExchgRate = System.Math.Round(Convert.ToDecimal(txtUSDExRate.Text),2);
                        d.GrossAmount = System.Math.Round((d.ExchgRate * d.RatePerTEU) + (d.ExchgRate * d.RatePerFEU) + (d.ExchgRate * d.RatePerTON) + (d.ExchgRate * d.RatePerUnit) + (d.ExchgRate * d.RatePerCBM), 2);
                        DataTable Charge = new InvoiceBLL().ChargeEditable(d.ChargesID);
                        if (Convert.ToBoolean(Charge.Rows.Count.ToInt() > 0))
                        {
                            if (Convert.ToBoolean(Charge.Rows[0]["ServiceTax"].ToString()))
                            {
                                d.ServiceTax = Math.Round((d.GrossAmount * TaxPer) / 100, 0);
                                d.ServiceTaxCessAmount = Math.Round((d.ServiceTax * TaxCess) / 100, 0);
                                d.ServiceTaxACess = Math.Round((d.ServiceTax * TaxAddCess) / 100, 0);
                            };
                        };
                        d.TotalAmount = Math.Round(d.GrossAmount + d.ServiceTax + d.ServiceTaxCessAmount + d.ServiceTaxACess,0);
                        return d;
                    }).ToList();

            ViewState["CHARGERATE"] = lstData;

            gvwInvoice.DataSource = lstData;
            gvwInvoice.DataBind();

            txtROff.Text = (Math.Round(lstData.Sum(cr => cr.TotalAmount), 0) - lstData.Sum(cr => cr.TotalAmount)).ToString();
            txtTotalAmount.Text = Math.Round(lstData.Sum(cr => cr.TotalAmount), 0).ToString();
        }

        protected void txtInvoiceDate_TextChanged(object sender, EventArgs e)
        {
        
            LoadExchangeRate();
            txtUSDExRate_TextChanged(null, null);
        }

        //protected void valDateRange_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    DateTime minDate = DateTime.Parse("2000/12/28");
        //    DateTime maxDate = DateTime.Now.Date;
        //    DateTime dt;

        //    args.IsValid = (DateTime.TryParse(args.Value, out dt)
        //                    && dt <= maxDate
        //                    && dt >= minDate);
        //}
    }
}