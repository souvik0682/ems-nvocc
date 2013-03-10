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

namespace EMS.WebApp.Transaction
{
    public partial class ManageInvoice : System.Web.UI.Page
    {
        List<ICharge> Charges = null;
        List<IChargeRate> ChargeRates = null;

        private int _userId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();

            if (!Page.IsPostBack)
            {
                Session["CHARGES"] = null;

                LoadInvoiceTypeDDL();
                LoadLocationDDL();
                LoadNvoccDDL();
                LoadCHADDL();

                //Charge
                LoadChargeDDL();


                if (!ReferenceEquals(Request.QueryString["InvoiceId"], null))
                {
                    long invoiveId = 0;
                    Int64.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["InvoiceId"].ToString()), out invoiveId);

                    if (invoiveId > 0)
                        LoadForEdit(invoiveId);
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

        private void LoadChargeDDL()
        {
            List<ICharge> lstCharge = new InvoiceBLL().GetAllCharges();
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
                DataTable dt = new InvoiceBLL().GetInvoiceType();

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

        //private void LoadLocationDDL()
        //{

        //    try
        //    {
        //        DataTable dt = new InvoiceBLL().GetLocation();

        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            DataRow dr = dt.NewRow();
        //            dr["pk_LocID"] = "0";
        //            dr["LocAbbr"] = "--Select--";
        //            dt.Rows.InsertAt(dr, 0);
        //            ddlLocation.DataValueField = "pk_LocID";
        //            ddlLocation.DataTextField = "LocAbbr";
        //            ddlLocation.DataSource = dt;
        //            ddlLocation.DataBind();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        private void LoadLocationDDL()
        {
            List<ILocation> lstLocation = new ImportBLL().GetLocation(3);  //Replace with UserId
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
                DataTable dt = new InvoiceBLL().GetBLno(nvoccId, locationId);
                DataRow dr = dt.NewRow();
                dr["pk_BLID"] = "0";
                dr["ImpLineBLNo"] = "--Select--";
                dt.Rows.InsertAt(dr, 0);
                ddlBLno.DataValueField = "pk_BLID";
                ddlBLno.DataTextField = "ImpLineBLNo";
                ddlBLno.DataSource = dt;
                ddlBLno.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GrossWeight()
        {
            string BLno = ddlBLno.SelectedItem.Text;
            DataTable dt = new InvoiceBLL().GrossWeight(BLno);
            txtGrossWeightTON.Text = dt.Rows[0]["GrossWeight"].ToString();

        }

        private void TEU()
        {
            string BLno = ddlBLno.SelectedItem.Text;
            DataTable dt = new InvoiceBLL().TEU(BLno);
            txtTEU.Text = dt.Rows[0]["NoofTEU"].ToString();

        }

        private void FEU()
        {
            string BLno = ddlBLno.SelectedItem.Text;
            DataTable dt = new InvoiceBLL().FEU(BLno);
            txtFFU.Text = dt.Rows[0]["NoofFEU"].ToString();

        }

        private void Volume()
        {
            string BLno = ddlBLno.SelectedItem.Text;
            DataTable dt = new InvoiceBLL().Volume(BLno);
            txtVolume.Text = dt.Rows[0]["Volume"].ToString();

        }

        private void BLdate()
        {
            string BLno = ddlBLno.SelectedItem.Text;
            DataTable dt = new InvoiceBLL().BLdate(BLno);
            txtBLdate.Text = Convert.ToDateTime(dt.Rows[0]["ImpLineBLDate"].ToString()).ToShortDateString();

        }

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

        private void LoadExchangeRate()
        {
            new InvoiceBLL().GetExchangeRate(Convert.ToInt64(ddlBLno.SelectedValue));
        }

        private void SetDefaultTerminal()
        {
            LoadTerminalDDL();

            long TerminalId = new InvoiceBLL().GetDefaultTerminal(Convert.ToInt64(ddlBLno.SelectedValue));
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
                txtGrossWeightTON.Text = "";
                txtTEU.Text = "";
                txtFFU.Text = "";
                txtVolume.Text = "";
                txtBLdate.Text = "";
                txtExchangeRate.Text = "";
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
                GrossWeight();
                TEU();
                FEU();
                Volume();
                BLdate();
                LoadExchangeRate();
                SetDefaultTerminal();
            }
            else
            {
                txtGrossWeightTON.Text = "";
                txtTEU.Text = "";
                txtFFU.Text = "";
                txtVolume.Text = "";
                txtBLdate.Text = "";
                txtExchangeRate.Text = "";
                ddlFTerminal.SelectedValue = "0";
            }
        }

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
                //    ddlFTerminal.Enabled = true;
                //    rfvTerminal.Visible = true;
                //}
                //else
                //{
                //    ddlFTerminal.SelectedValue = "0";
                //    ddlFTerminal.Enabled = false;
                //    rfvTerminal.Visible = false;
                //}

                int terminalId = 0;

                if (charge.IsTerminal)
                {
                    terminalId = Convert.ToInt32(ddlFTerminal.SelectedValue);
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
                            ratePerPTeu = chargeRates[0].SharingTEU * Convert.ToInt32(txtTEU.Text);
                            ratePerPFeu = chargeRates[0].SharingFEU * Convert.ToInt32(txtFFU.Text);
                        }

                        txtRatePerTEU.Text = ratePerTeu.ToString();
                        txtRateperFEU.Text = ratePerFeu.ToString();
                        grossAmount = ((ratePerTeu * Convert.ToInt32(txtTEU.Text)) + (ratePerFeu * Convert.ToInt32(txtFFU.Text)));

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

                        if ((ratePerCBM * Convert.ToDecimal(txtGrossWeightTON.Text)) > (ratePerTon * Convert.ToDecimal(txtVolume.Text)))
                        {
                            grossAmount = (ratePerCBM * Convert.ToDecimal(txtGrossWeightTON.Text));
                        }
                        else
                        {
                            grossAmount = (ratePerTon * Convert.ToDecimal(txtVolume.Text));
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

        protected void gvwInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                RemoveChargeRate(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void gvwInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ScriptManager sManager = ScriptManager.GetCurrent(this);

                e.Row.Cells[0].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ChangeName"));
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TerminalName"));
                //e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "WashingTypeName"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "RatePerBL"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "RatePerTEU"));
                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "RatePerFEU"));
                e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "RatePerCBM"));
                e.Row.Cells[6].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "RatePerTON"));
                e.Row.Cells[7].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Usd"));
                e.Row.Cells[8].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "GrossAmount"));
                e.Row.Cells[9].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "STax"));
                e.Row.Cells[10].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TotalAmount"));

                //Delete link
                ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                btnRemove.ToolTip = "Remove";
                btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InvoiceChargeId"));


                btnRemove.OnClientClick = "javascript:return confirm('Are you sure about delete?');";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            IInvoice invoice = new InvoiceEntity();
            BuildInvoiceEntity(invoice);

            List<IChargeRate> chargeRate = ViewState["CHARGERATE"] as List<IChargeRate>;
            invoice.ChargeRates = chargeRate;

            //Add-Update
            long invoiceID = new InvoiceBLL().SaveInvoice(invoice);

            string invoiceNo = new InvoiceBLL().GetInvoiceNoById(invoiceID);

            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('Record saved successfully! Invoice Number: " + invoiceNo + "');</script>", false);

            ViewState["CHARGERATE"] = null;
            gvwInvoice.DataSource = null;
            gvwInvoice.DataBind();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddChargesRate();
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

            if (rdblAllinFreight.SelectedValue == "0")
                invoice.AllInFreight = false;
            else
                invoice.AllInFreight = true;

            invoice.BLID = Convert.ToInt64(ddlBLno.SelectedValue);
            //invoice.BookingID = 0;
            invoice.CHAID = Convert.ToInt32(ddlCHAid.SelectedValue);
            invoice.CompanyID = 1;
            invoice.EditedOn = DateTime.Now;
            invoice.ExportImport = "I";
            invoice.GrossAmount = chargeRate.Sum(c => c.GrossAmount);
            invoice.InvoiceDate = Convert.ToDateTime(txtInvoiceDate.Text);
            //invoice.InvoiceNo = "";
            invoice.InvoiceTypeID = Convert.ToInt32(ddlInvoiceType.SelectedValue);
            invoice.LocationID = Convert.ToInt32(ddlLocation.SelectedValue);
            invoice.NVOCCID = Convert.ToInt32(ddlNvocc.SelectedValue);
            invoice.ServiceTax = chargeRate.Sum(c => c.STax);
            invoice.ServiceTaxACess = chargeRate.Sum(c => c.ServiceTaxACess);
            invoice.ServiceTaxCess = chargeRate.Sum(c => c.ServiceTaxCessAmount);
            invoice.UserAdded = _userId;
            invoice.UserLastEdited = _userId;

            if (txtExchangeRate.Text != string.Empty)
                invoice.XchangeRate = Convert.ToDecimal(txtExchangeRate.Text);
        }

        private void BuildChargesRate(IChargeRate charge)
        {
            if (ViewState["INVOICECHARGEID"] == null)
                charge.InvoiceChargeId = -1;
            else
                charge.InvoiceChargeId = Convert.ToInt32(ViewState["INVOICECHARGEID"]) - 1;

            ViewState["INVOICECHARGEID"] = charge.InvoiceChargeId;


            charge.ChangeName = ddlFChargeName.SelectedItem.Text;
            charge.ChargesID = Convert.ToInt32(ddlFChargeName.SelectedValue);
            charge.GrossAmount = Convert.ToDecimal(txtGrossAmount.Text);
            //charge.LocationId = Convert.ToInt32(ddlLocation.SelectedValue);
            charge.RatePerBL = Convert.ToDecimal(txtRatePerBL.Text);
            charge.RatePerCBM = Convert.ToDecimal(txtRatePerCBM.Text);
            charge.RatePerFEU = Convert.ToDecimal(txtRateperFEU.Text);
            charge.RatePerTEU = Convert.ToDecimal(txtRatePerTEU.Text);
            charge.RatePerTON = Convert.ToDecimal(txtRatePerTon.Text);
            charge.STax = Convert.ToDecimal(txtServiceTax.Text);

            if (ViewState["RATEPERPBL"] != null)
                charge.SharingBL = Convert.ToDecimal(ViewState["RATEPERPBL"]);
            if (ViewState["RATEPERPFEU"] != null)
                charge.SharingFEU = Convert.ToDecimal(ViewState["RATEPERPFEU"]);
            if (ViewState["RATEPERPTEU"] != null)
                charge.SharingTEU = Convert.ToDecimal(ViewState["RATEPERPTEU"]);

            if (ViewState["CESSAMOUNT"] != null)
                charge.ServiceTaxCessAmount = Convert.ToDecimal(ViewState["CESSAMOUNT"]);
            if (ViewState["ADDCESS"] != null)
                charge.ServiceTaxACess = Convert.ToDecimal(ViewState["ADDCESS"]);

            charge.STax = Convert.ToDecimal(txtServiceTax.Text);

            if (Convert.ToInt32(ddlFTerminal.SelectedValue) != 0)
            {
                charge.TerminalId = Convert.ToInt32(ddlFTerminal.SelectedValue);
                charge.TerminalName = Convert.ToString(ddlFTerminal.SelectedItem.Text);
            }

            charge.TotalAmount = Convert.ToDecimal(txtTotal.Text);
            charge.Usd = Convert.ToDecimal(txtUSD.Text);

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
            ddlFTerminal.SelectedValue = "0";
            //ddlFWashingType.SelectedValue = "0";
            txtTotal.Text = "0.00";
            txtUSD.Text = "0.00";
            txtServiceTax.Text = "0.00";
            txtGrossAmount.Text = "0.00";

        }

        private void RemoveChargeRate(int InvoiceChargeId)
        {
            //Then Delete from List
            if (ViewState["CHARGERATE"] != null)
                ChargeRates = ViewState["CHARGERATE"] as List<IChargeRate>;

            IChargeRate cRate = ChargeRates.Single(c => c.InvoiceChargeId == InvoiceChargeId);
            ChargeRates.Remove(cRate);

            //Delete from DB
            int retVal = new InvoiceBLL().DeleteInvoiceCharge(cRate.InvoiceChargeId);

            ViewState["CHARGERATE"] = ChargeRates;
            RefreshGridView();
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
            IInvoice invoice = new InvoiceBLL().GetInvoiceById(InvoiceId);

            ViewState["InvoiceID"] = invoice.InvoiceID;


            rdblAccountFor.SelectedValue = invoice.Account;

            if (invoice.AllInFreight)
                rdblAllinFreight.SelectedValue = "1";
            else
                rdblAllinFreight.SelectedValue = "0";

            ddlBLno.SelectedValue = Convert.ToString(invoice.BLID);
            ddlCHAid.SelectedValue = Convert.ToString(invoice.CHAID);
            txtGrossAmount.Text = invoice.GrossAmount.ToString();
            txtInvoiceDate.Text = invoice.InvoiceDate.ToShortDateString();
            txtInvoiceNo.Text = invoice.InvoiceNo;
            ddlInvoiceType.SelectedValue = invoice.InvoiceTypeID.ToString();
            ddlLocation.SelectedValue = invoice.LocationID.ToString();
            ddlNvocc.SelectedValue = invoice.NVOCCID.ToString();
            txtServiceTax.Text = invoice.ServiceTax.ToString();
            txtExchangeRate.Text = invoice.XchangeRate.ToString();

            long nvoccId = Convert.ToInt64(ddlNvocc.SelectedValue);
            long locationId = Convert.ToInt64(ddlLocation.SelectedValue);

            LoadBLNoDDL(nvoccId, locationId);
            GrossWeight();
            TEU();
            FEU();
            Volume();
            BLdate();
            LoadExchangeRate();
            SetDefaultTerminal();

            //For Charge Rates
            List<IChargeRate> chargeRates = new InvoiceBLL().GetInvoiceChargesById(InvoiceId);

            ViewState["CHARGERATE"] = chargeRates;

            gvwInvoice.DataSource = chargeRates;
            gvwInvoice.DataBind();
        }
    }
}