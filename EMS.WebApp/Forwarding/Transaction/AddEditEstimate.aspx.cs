using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities;
using EMS.Common;
using EMS.BLL;
using EMS.Entity;
using EMS.WebApp.CustomControls;
using System.Data;
using System.Web.UI.HtmlControls;
using Microsoft.Win32;
using System.IO;

namespace EMS.WebApp.Forwarding.Master
{
    public partial class AddEditEstimate : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private string companyId = "1";
        private bool _canAdd = true;
        private bool _canEdit = true;
        private bool _canDelete = true;
        private bool _canView = true;

        private int EstmateId { get { if (ViewState["Id"] != null) { return Convert.ToInt32(ViewState["Id"]); } return 0; } set { ViewState["Id"] = value; } }
        private string Mode { get { if (ViewState["Id"] != null) { return "E"; } return "A"; } }

        bool IsEmptyGrid { get; set; }
        #endregion


        #region Protected Event Handler
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //lnkQuoUpload.Text = "";

            RetriveParameters();
            if (!IsPostBack)
            {
                LoadDefault();
                PopulateUnitType();
            }

            //   CheckUserAccess(countryId);
        }

        protected void grvCharges_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                var dll = (DropDownList)e.Row.FindControl("ddlCharges");
                DataSet ds = new DataSet();
                if (ViewState["ddlCharges"] == null)
                {
                    ds = new EstimateBLL().GetCharges((ISearchCriteria)null);
                    ViewState["ddlCharges"] = ds;
                }
                else{
                    ds = (DataSet)ViewState["ddlCharges"];
                }
                dll.DataSource = ds;
                dll.DataTextField = "ChargeName";
                dll.DataValueField = "ChargeId";
                dll.DataBind();
                dll.Items.Insert(0, new ListItem("--Select--", "0"));

                var dllC = (DropDownList)e.Row.FindControl("ddlCurrency");
                DataSet dllCds = new DataSet();
                if (ViewState["ddlCurrency"] == null)
                {
                    dllCds = new EstimateBLL().GetCurrency((ISearchCriteria)null);
                    ViewState["ddlCurrency"] = dllCds;
                }
                else
                {
                    dllCds = (DataSet)ViewState["ddlCurrency"];
                }

                dllC.DataSource = dllCds;
                dllC.DataTextField = "CurrencyName";
                dllC.DataValueField = "pk_CurrencyID";
                dllC.DataBind();
                dllC.Items.Insert(0, new ListItem("--Select--", "0"));

                //var dllU = (DropDownList)e.Row.FindControl("ddlUnitType");
                //DataSet dllUs = new DataSet();
                //if (ViewState["ddlUnitType"] == null)
                //{
                //    dllUs = new EstimateBLL().GetUnitMaster(new SearchCriteria { StringOption1 = companyId, StringOption2 = lblShippingMode.Text });
                //    ViewState["ddlUnitType"] = dllUs;
                //}
                //else
                //{
                //    dllUs = (DataSet)ViewState["ddlUnitType"];
                //}

                ////var units = new EstimateBLL().GetUnitMaster(new SearchCriteria { StringOption1 = companyId });
                //dllU.DataSource = dllUs;
                //dllU.DataTextField = "UnitName";
                //dllU.DataValueField = "pk_UnitTypeID";
                //dllU.DataBind();
                //dllU.Items.Insert(0, new ListItem("--Select--", "0"));

              
            }
            else if (e.Row.RowType == DataControlRowType.DataRow && IsEmptyGrid)
            {
                e.Row.Visible = false;
                IsEmptyGrid = false;

            }

        }

        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ddlCurrency = (DropDownList)grvCharges.FooterRow.FindControl("ddlCurrency");
            var txtROE = (TextBox)grvCharges.FooterRow.FindControl("txtROE");
            if (ddlCurrency != null)
            {
                var ex = new EstimateBLL().GetExchange(new SearchCriteria() { StringOption1 = ddlCurrency.SelectedValue, Date = lblJobDate.Text.ToDateTime() });

                txtROE.Text = "0";
                if (ex != null && ex.Tables.Count > 0 && ex.Tables[0].Rows.Count > 0)
                {
                    //txtROE.Text = Convert.ToString(ex.Tables[0].Rows[0]["USDXchRate"]);
                    txtROE.Text = txtExRate.Text;
                }
                txtROE.Enabled = true;
                if (ddlCurrency.SelectedValue == "1" || ddlCurrency.SelectedValue == "2") { 
                    
                    txtROE.Enabled = false;
                    if (ddlCurrency.SelectedValue == "1")
                        txtROE.Text = "1";
                }
            }
            CalculateAndAssign();
        }

        private void CalculateAndAssign()
        {
            DataSet ds = new DataSet();
            var txtUnit = (TextBox)grvCharges.FooterRow.FindControl("txtUnit");
            var txtRate = (TextBox)grvCharges.FooterRow.FindControl("txtRate");
            var txtROE = (TextBox)grvCharges.FooterRow.FindControl("txtROE");
            //var spINR = (Label)grvCharges.FooterRow.FindControl("lblINR");
            var spINR = (TextBox)grvCharges.FooterRow.FindControl("lblINR");
            var spSTAX = (TextBox)grvCharges.FooterRow.FindControl("lblStax");
            var ddlChargeID = (DropDownList)grvCharges.FooterRow.FindControl("ddlCharges");

            var unit = 0.0;
            var rate = 0.0;
            var roe = 0.0;
            var stax = 0.0;
            int ChargeID = ddlChargeID.SelectedValue.ToInt();

            try
            {
                unit = Convert.ToDouble(txtUnit.Text);
            }
            catch
            {
                unit = 0;
            }
            try
            {
                rate = Convert.ToDouble(txtRate.Text);
            }
            catch
            {
                rate = 0;
            }
            try
            {
                roe = Convert.ToDouble(txtROE.Text);
            }
            catch
            {
                roe = 0;
            }
            try
            {
                ds = new EstimateBLL().GetServiceTax(lblJobDate.Text.ToDateTime(), (unit * rate * roe).ToDecimal(), ChargeID);
                if (ds.Tables[1].Rows[0]["ServiceTax"].ToInt() == 1) 
                {
                    spSTAX.Text = (ds.Tables[0].Rows[0]["stax"].ToDecimal() + ds.Tables[0].Rows[0]["CessAmt"].ToDecimal() + ds.Tables[0].Rows[0]["AddCess"].ToDecimal()).ToString("#######0.00");
                    stax = Convert.ToDouble(spSTAX.Text);
                }
                else
                {
                    spSTAX.Text = "0";
                    stax = 0;
                }
                //stax = Convert.ToDouble(txtROE.Text);
            }
            catch
            {
                stax = 0;
            }

            if (spINR != null)
            {
                spINR.Text = ((unit * rate * roe)+stax).ToString("#######0.00");
            }
        }

        protected void Text_TextChanged(object sender, EventArgs e)
        {
            CalculateAndAssign();
          
        }

        protected void lblStax_TextChanged(object sender, EventArgs e)
        {
            var txtUnit = (TextBox)grvCharges.FooterRow.FindControl("txtUnit");
            var txtRate = (TextBox)grvCharges.FooterRow.FindControl("txtRate");
            var txtROE = (TextBox)grvCharges.FooterRow.FindControl("txtROE");
            var spINR = (TextBox)grvCharges.FooterRow.FindControl("lblINR");
            var spSTAX = (TextBox)grvCharges.FooterRow.FindControl("lblStax");
            var unit = 0.0;
            var rate = 0.0;
            var roe = 0.0;
            var stax = 0.0;
            try
            {
                unit = Convert.ToDouble(txtUnit.Text);
            }
            catch
            {
                unit = 0;
            }
            try
            {
                rate = Convert.ToDouble(txtRate.Text);
            }
            catch
            {
                rate = 0;
            }
            try
            {
                roe = Convert.ToDouble(txtROE.Text);
            }
            catch
            {
                roe = 0;
            }
            try
            {
                stax = Convert.ToDouble(spSTAX.Text);
            }
            catch
            {
                stax = 0;
            }
            spINR.Text = ((unit * rate * roe) + stax).ToString("#######0.00");
        }

        protected void ddlCharges_SelectedIndexChanged(object sender, EventArgs e)
        {

            var ddlChargeID = (DropDownList)grvCharges.FooterRow.FindControl("ddlCharges");
            var ddlCurrency = (DropDownList)grvCharges.FooterRow.FindControl("ddlCurrency");
            DataSet ds = new DataSet();
            ds = new EstimateBLL().GetSelectedCharge(ddlChargeID.SelectedValue.ToInt());
            ddlCurrency.SelectedValue = ds.Tables[0].Rows[0]["Currency"].ToString();
            //StaxExist = ds.Tables[0].Rows[0]["ServiceTax"].ToInt();
            ddlCurrency_SelectedIndexChanged(null, null);
            //List<IChargeRate> chargeRates = new InvoiceBLL().GetInvoiceCharges_New(Convert.ToInt64(ddlBLno.SelectedValue), Convert.ToInt32(ddlFChargeName.SelectedValue), Convert.ToInt32(ddlFTerminal.SelectedValue), Convert.ToDecimal(txtExchangeRate.Text), 0, "", Convert.ToDateTime(txtInvoiceDate.Text));
            //List<IExpChargeRate> chargeRates = new InvoiceBLL().GetExpInvoiceCharges_New(Convert.ToInt64(ddlBLno.SelectedValue), Convert.ToInt32(ddlFChargeName.SelectedValue), Convert.ToInt32(ddlFTerminal.SelectedValue), Convert.ToDecimal(0), 0, "", Convert.ToDateTime(txtInvoiceDate.Text));

        }

        protected void ddlUnitType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            var ddlUnitTypeID = (DropDownList)grvCharges.FooterRow.FindControl("ddlUnitType");
            ds = new EstimateBLL().GetSingleUnitType(ddlUnitTypeID.SelectedValue.ToInt(), ViewState["jobId"].ToInt());
            var ddlSizeID = (DropDownList)grvCharges.FooterRow.FindControl("ddlSize");
            //var rfvSize = (RequiredFieldValidator)grvCharges.FooterRow.FindControl("rfvSize");
            var txtUnit = (TextBox)grvCharges.FooterRow.FindControl("txtUnit");
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

        protected void ddlSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateUnitType();
            CheckContainer();

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

        private void CheckContainer()
        {
            var ddlUnitTypeID = (DropDownList)grvCharges.FooterRow.FindControl("ddlUnitType");
            var ddlSizeID = (DropDownList)grvCharges.FooterRow.FindControl("ddlSize");
            var txtUnit = (TextBox)grvCharges.FooterRow.FindControl("txtUnit");

            if (ddlUnitTypeID.SelectedIndex != 0 && ddlSizeID.SelectedIndex != 0)
            {
                DataSet ds = new DataSet();
                ds = new EstimateBLL().GetContainers(ddlUnitTypeID.SelectedValue.ToInt(), ddlSizeID.SelectedValue.ToString(), ViewState["jobId"].ToInt());
                if (ds.Tables[0].Rows.Count > 0)
                    txtUnit.Text = ds.Tables[0].Rows[0]["Nos"].ToString();
                else
                    txtUnit.Text = "0.00";
            }
        }

        protected void grvCharges_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            List<Charge> charges = null;
            DataSet ds = new DataSet();
            if (ViewState["Charges"] != null)
            {
                charges = (List<Charge>)ViewState["Charges"];
            }
            else
            {
                charges = new List<Charge>();
            }
            if (e.CommandName == "Add")
            {
                var estimate = new Estimate();

             

                if (grvCharges.FooterRow != null)
                {
                    Charge charge = new Charge();
                    var id = 1;
                    if (charges.Count > 0)
                    {
                      id=  charges.Max(f => f.ChargeId);
                    }
                    var ddlCharges = (DropDownList)grvCharges.FooterRow.FindControl("ddlCharges");
                    var ddlUnitType = (DropDownList)grvCharges.FooterRow.FindControl("ddlUnitType");
                    var txtUnit = (TextBox)grvCharges.FooterRow.FindControl("txtUnit");
                    var txtRate = (TextBox)grvCharges.FooterRow.FindControl("txtRate");
                    var ddlCurrency = (DropDownList)grvCharges.FooterRow.FindControl("ddlCurrency");
                    var txtROE = (TextBox)grvCharges.FooterRow.FindControl("txtROE");
                    var ddlSize = (DropDownList)grvCharges.FooterRow.FindControl("ddlSize");
                    //var hdnServiceTaxExists = ((HiddenField)grvCharges.FooterRow.FindControl("hdnServiceTaxExists")).ToInt(); 
                    var lblStax = (TextBox)grvCharges.FooterRow.FindControl("lblStax");
                    charge.ChargeMasterId = Convert.ToInt32(ddlCharges.SelectedValue);
                    charge.ChargeMasterName = ddlCharges.SelectedItem.Text;
                    charge.CurrencyId = Convert.ToInt32(ddlCurrency.SelectedValue);
                    charge.Currency = ddlCurrency.SelectedItem.Text;
                    charge.UnitType = ddlUnitType.SelectedItem.Text;
                    charge.CntrSize = ddlSize.SelectedValue;
                    
                    charge.ChargeId = id + 1;
                    charge.ChargeType = rdoPayment.SelectedValue;
                    
                    try
                    {
                        charge.Unit = Convert.ToDouble(txtUnit.Text);
                    }
                    catch { ScriptManager.RegisterStartupScript(this, this.GetType(), DateTime.Now.Ticks.ToString(), "alert('Please provide Numeric Unit value');", true); return; }
                    try
                    {
                         charge.Rate = Convert.ToDouble(txtRate.Text);
                    }
                    catch { ScriptManager.RegisterStartupScript(this, this.GetType(), DateTime.Now.Ticks.ToString(), "alert('Please provide Numeric Rate value');", true); return; }
                    try
                    {
                         charge.CurrencyId = Convert.ToInt32(ddlCurrency.SelectedValue);
                    }
                    catch { }
                    try
                    {
                        charge.ROE = Convert.ToDouble(txtROE.Text);
                    }
                    catch { ScriptManager.RegisterStartupScript(this, this.GetType(), DateTime.Now.Ticks.ToString(), "alert('Please provide Numeric ROE value');", true); return; }

                    try { charge.INR = charge.Unit * charge.Rate * charge.ROE; }
                    catch { }

                    try
                    {
                        charge.UnitId =  Convert.ToInt32(ddlUnitType.SelectedValue);
                    }
                    catch { ScriptManager.RegisterStartupScript(this, this.GetType(), DateTime.Now.Ticks.ToString(), "alert('Please provide Numeric ROE value');", true); return; }
                    
                   
                    try 
                    {
                        charge.STax = lblStax.Text.ToDouble();
                    }
                    catch { }

                    try 
                    { 
                        charge.INR = (charge.Unit * charge.Rate * charge.ROE) + charge.STax;
         
                    }
                    catch { }


                    charges.Add(charge);

                    lblCharges.Text = charges.Sum(m => m.INR).ToString("#########0.00");
                    PopulateUnitType();
                    //lblTotalUnit.Text = charges.Sum(m => m.Unit).ToString();
                }
            }
            else if (e.CommandName == "Remove")
       
            //charges.Remove(charges.FindAll(f => f.ChargeId.ToString().Equals(e.CommandArgument)).FirstOrDefault());
            {
                try
                {
                    var t = charges.FindAll(f => f.ChargeId.ToString().Equals(e.CommandArgument)).FirstOrDefault();
                    if (t != null)
                    {
                        charges.Remove(t);
                        var sum = charges.Sum(m => m.INR);
                        lblCharges.Text = sum.ToString("##########0.00");
                    }
                }
                catch { }
            }
          

            if (charges.Count == 0)
            {
                SetEmptyGrid();
                PopulateUnitType();
            }
            else
            {
                ViewState["Charges"] = charges;
                grvCharges.DataSource = charges;
                grvCharges.DataBind();
                PopulateUnitType();
            }
        }

       
        private void RetriveParameters()
        {
            _userId = EMS.BLL.UserBLL.GetLoggedInUserId();
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

                    if (!_canEdit && xID != "-1")
                    {
                        btnSave.Visible = false;
                    }
                    else if (!_canAdd && xID == "-1")
                    {
                        btnSave.Visible = false;
                    }
                }

            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void rdoPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoPayment.SelectedValue == "C")
            {
                txtCreditInDays.Enabled = true;
                RequiredFieldValidator3.Enabled = true;
            }
            else
            {
                txtCreditInDays.Enabled = false;
                RequiredFieldValidator3.Enabled = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveAdjustmentModel();

        }

        #endregion

        #region Private Methods

        private void GetPartyValuesSetToDdl(int PartyType)
        {
            ddlParty.Items.Clear();
            ddlParty.Items.Add(new ListItem("--Select--", "0"));
            if (Mode == "A")
            {
                if (ViewState["IsPayment"].ToInt() == 0)
                {

                    DataSet dll = new DataSet();

                    dll = new EstimateBLL().GetAllParty(PartyType);

                    ddlParty.DataSource = dll;
                    ddlParty.DataTextField = "PartyName";
                    ddlParty.DataValueField = "pk_fwPartyID";
                    ddlParty.DataBind();
                    ddlParty.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    DataSet dllCreditor = new DataSet();

                    dllCreditor = new EstimateBLL().GetParty(PartyType);

                    ddlParty.DataSource = dllCreditor;
                    ddlParty.DataTextField = "PartyName";
                    ddlParty.DataValueField = "pk_fwPartyID";
                    ddlParty.DataBind();
                    ddlParty.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            else
            {
                DataSet dll = new DataSet();

                dll = new EstimateBLL().GetAllParty(PartyType);

                ddlParty.DataSource = dll;
                ddlParty.DataTextField = "PartyName";
                ddlParty.DataValueField = "pk_fwPartyID";
                ddlParty.DataBind();
                ddlParty.Items.Insert(0, new ListItem("--Select--", "0"));
               
            }
        }

        private void LoadDefault()
        {
            var estimateId = Request.QueryString["EstimateId"];
            rdoPayment_SelectedIndexChanged(null, null);
            if (Request.QueryString["EstimateId"] != null)
                estimateId = GeneralFunctions.DecryptQueryString(Request.QueryString["EstimateId"]);

            ViewState["Id"] = estimateId;
            //var units = new EstimateBLL().GetUnitMaster(new SearchCriteria { StringOption1 = companyId });
            //ddlUnitType.DataSource = units;
            //ddlUnitType.DataTextField = "UnitName";
            //ddlUnitType.DataValueField = "pk_UnitTypeID";
            //ddlUnitType.DataBind();
            //ddlUnitType.Items.Insert(0, new ListItem("--Select--", "0"));

            var partyType = new EstimateBLL().GetBillingGroupMaster((ISearchCriteria)null);
            ddlBillingFrom.DataSource = partyType;

            ddlBillingFrom.DataTextField = "PartyType";
            ddlBillingFrom.DataValueField = "pk_PartyTypeID";
            ddlBillingFrom.DataBind();
            ddlBillingFrom.Items.Insert(0, new ListItem("--Select--", "0"));

            if (!string.IsNullOrEmpty(estimateId))
            {
                var estimate = new EstimateBLL().GetEstimate(new SearchCriteria { StringOption1 = estimateId });
                var temp = new List<Charge>();

                ViewState["IsPayment"] = estimate.PorR == "P" ? 1 : 0;
                ViewState["jobId"] = estimate.JobID;
                
                if (estimate != null)
                {
                    if (estimate.Charges != null)
                    {
                        temp = estimate.Charges;
                    }
                    ViewState["Estimate"] = estimate;
                    ddlBillingFrom.SelectedValue = estimate.BillFromId.ToString();

                    ddlParty.Items.Clear();
                    ddlParty.Items.Add(new ListItem("--Select--", "0"));
                    ddlParty.Items.Add(new ListItem(estimate.PartyName, estimate.PartyId.ToString()));

                    ddlParty.SelectedValue = estimate.PartyId.ToString();
                    if(temp!=null && temp.Count>0){
                        estimate.UnitTypeId = temp.FirstOrDefault().UnitId;
                        
                    }

                    txtExRate.Text = estimate.ROE.ToString();
                    //ddlUnitType.SelectedValue = estimate.UnitTypeId.ToString();
                    rdoPayment.SelectedValue = estimate.TransactionType.ToString();
                    txtCreditInDays.Text = estimate.CreditDays.ToString();
                    lblJobNo.Text = estimate.JobNo.ToString();

                    txtEstimateDate.Text = estimate.EstimateDate.ToString().Split(' ')[0];
                    lblEstimateNo.Text = estimate.EstimateNo.ToString();
                    hdnQuoPath.Value = "Quotation" + estimateId.ToString().TrimEnd();
                    //lblTotalUnit.Text = temp.Sum(x => x.Unit).ToString();
                    lblCharges.Text = temp.Sum(x => x.INR).ToString("#########0.00");
                    grvCharges.DataSource = temp;
                    grvCharges.DataBind();
                    ViewState["Charges"] = temp;
                    var path = Server.MapPath("~/Forwarding/QuoUpload");
                    var newFileName = "Quotation" + estimateId.ToString().TrimEnd();  //  Guid.NewGuid().ToString();

                    //if (!string.IsNullOrEmpty(path))
                    //{
                    //    path += @"\" + hdnQuoPath.Value + ".pdf";
                    //    //System.IO.Path.GetExtension(fileName);
                    //    hdnQuoPath.Value = path;
                    //    if (File.Exists(path))
                    //    {
                    //        lnkQuoUpload.Enabled = true;
                    //        lnkQuoUpload.Text = newFileName + ".pdf";
                    //        //lblUploadedFileName.Text = newFileName;
                    //        //lblUploadedFileName.Visible = true;
                    //    }
                    //}

                    var jobId = GeneralFunctions.DecryptQueryString(Request.QueryString["jobId"]);
                    
                    ViewState["jobId"] = jobId;

                    var reader = new CreditorInvoiceBLL().GetJobForCreInv(new SearchCriteria { StringOption1 = jobId });
                    if (reader != null && reader.Tables.Count > 0 && reader.Tables[0].Rows.Count > 0)
                    {
                        var dr = reader.Tables[0].Rows[0];
                        lblJobDate.Text = Convert.ToDateTime(dr["JobDate"]).ToShortDateString();
                        lblJobNo.Text = dr["JobNo"].ToString();
                        lblShippingMode.Text = dr["ShippingMode"].ToString();
                        txtEstimateDate.Text = Convert.ToDateTime(dr["JobDate"]).ToShortDateString();
                    }
                    PopulateUnitType();
                }
                else
                {
                    SetEmptyGrid();
                }
            }
            else
            {
                SetEmptyGrid();
            }
            
            if (Mode == "A")
            {
                txtCreditInDays.Enabled = false;
                RequiredFieldValidator3.Enabled = false;
                var value = GeneralFunctions.DecryptQueryString(Request.QueryString["IsPayment"]);
                if (string.IsNullOrEmpty(value) || !(value == "1" || value == "0"))
                {
                    throw new Exception("Invalid Request Transaction type");
                }
                ViewState["IsPayment"] = value;
                txtEstimateDate.Text = DateTime.Today.ToShortDateString();
           
                var jobId = GeneralFunctions.DecryptQueryString(Request.QueryString["jobId"]);
                if (string.IsNullOrEmpty(jobId))
                {
                    throw new Exception("Invalid jobId");
                }
                ViewState["jobId"] = jobId;

                var reader = new CreditorInvoiceBLL().GetJobForCreInv(new SearchCriteria { StringOption1 = jobId });
                if (reader != null && reader.Tables.Count > 0 && reader.Tables[0].Rows.Count > 0)
                {
                    var dr = reader.Tables[0].Rows[0];
                    lblJobDate.Text = Convert.ToDateTime(dr["JobDate"]).ToShortDateString();
                    lblJobNo.Text = dr["JobNo"].ToString();
                    lblShippingMode.Text = dr["ShippingMode"].ToString();
                    txtEstimateDate.Text = Convert.ToDateTime(dr["JobDate"]).ToShortDateString();
                }
                var ex = new EstimateBLL().GetExchange(new SearchCriteria() { StringOption1 = "2", Date = lblJobDate.Text.ToDateTime() });
                txtExRate.Text = Convert.ToString(ex.Tables[0].Rows[0]["USDXchRate"]);
                PopulateUnitType();

                //lblJobNo.Text = GeneralFunctions.DecryptQueryString(Request.QueryString["JobNo"]);
                //GetPartyValuesSetToDdl();

            }
           
            //grvCharges.ShowFooter = false;

        }

        private Estimate ExtractData()
        {
            //var unitId = Convert.ToInt32(ddlUnitType.SelectedValue);
            var est= new Estimate
            {
                EstimateId = EstmateId,
                BillFromId = Convert.ToInt32(ddlBillingFrom.SelectedValue),
                PartyId = Convert.ToInt32(ddlParty.SelectedValue),
                //UnitTypeId = unitId,
                PaymentIn = rdoPayment.SelectedValue,
                CreditDays = string.IsNullOrEmpty(txtCreditInDays.Text) ? (int?)null : Convert.ToInt32(txtCreditInDays.Text),
                Charges = (List<Charge>)ViewState["Charges"],
                EstimateDate = txtEstimateDate.Text.ToDateTime(),
                TransactionType = rdoPayment.SelectedValue,
                ROE = txtExRate.Text.ToDecimal(),
                EstimateNo = lblEstimateNo.Text,
                CompanyID = 1,
                TotalCharges=Convert.ToDouble(lblCharges.Text),
                UserID = _userId,
                PorR = ViewState["IsPayment"].ToInt() == 1 ? "P" : "R",
                //EstimateDate=DateTime.Now,
                JobID = JobID

            };
            //est.Charges.ForEach(x => x.UnitId = unitId);
            return est;

        }
        //public string TransactionType { get {
        //    if (ViewState["IsPayment"] != null) {
        //        var t = Convert.ToInt32(ViewState["IsPayment"]);
        //        if (t == 1)
        //            return "P";
        //    }
        //    return "R";
        //} }

        public int JobID
        {
            get
            {
                if (ViewState["jobId"] != null)
                {
                    return Convert.ToInt32(ViewState["jobId"]);
                }
                throw new Exception("Job Id couldnt be found.");
            }
        }
        private void SaveAdjustmentModel()
        {
            var data = ExtractData();

            hdnLastNo.Value = new EstimateBLL().SaveEstimate(data, Mode).ToString();

            if (hdnLastNo.Value.ToInt() > 0)
            {
                ModalPopupExtender1.Show();
            }
            //{
            //    if (QuotationUpload.HasFile)
            //    {
            //        var fileName = QuotationUpload.FileName;
            //        var filext = fileName.Substring(fileName.LastIndexOf(".") + 1);
            //        if (filext.ToLower() != "pdf")
            //        {
            //            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('Only pdf file is accepted!');</script>", false);
            //            return;
            //        }
            //        var path = Server.MapPath("~/Forwarding/QuoUpload");
            //        var newFileName = "Quotation" + result.ToString().TrimEnd();  //  Guid.NewGuid().ToString();

            //        if (!string.IsNullOrEmpty(path))
            //        {
            //            path += @"\" + newFileName + System.IO.Path.GetExtension(fileName);
            //            QuotationUpload.PostedFile.SaveAs(path);
            //        }
            //    }


            //}
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), DateTime.Now.Ticks.ToString(), "alert('Error Occured');", true);
            }

        }

        private void PopulateUnitType()
        {
            var dllU = (DropDownList)grvCharges.FooterRow.FindControl("ddlUnitType");
            //var dllU = (DropDownList)e.Row.FindControl("ddlUnitType");
            var curtype = "E";
            
            DataSet dllUs = new DataSet();


            if (ViewState["dllUs"] == null)
            {
                var ddlSize = (DropDownList)grvCharges.FooterRow.FindControl("ddlSize");
                if (ddlSize.SelectedValue.ToInt() == 0)
                    curtype = "N";
                else
                    curtype = "E";
                dllUs = new EstimateBLL().GetUnitMaster(new SearchCriteria { StringOption1 = companyId, StringOption2 = curtype, SortExpression = "UnitName" });
                ViewState["ddlUnitType"] = dllUs;
            }
            else
            {
                dllUs = (DataSet)ViewState["ddlUnitType"];
            }

            //var units = new EstimateBLL().GetUnitMaster(new SearchCriteria { StringOption1 = companyId });
            dllU.DataSource = dllUs;
            dllU.DataTextField = "UnitName";
            dllU.DataValueField = "pk_UnitTypeID";
            dllU.DataBind();
            dllU.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void SetEmptyGrid()
        {
            grvCharges.ShowFooter = true;
            IsEmptyGrid = true;
            List<Charge> dr = new List<Charge>() { new Charge { } };
            grvCharges.DataSource = dr;
            grvCharges.DataBind();
            ViewState["Charges"] = new List<Charge>();
        }
        #endregion

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string encryptedId = GeneralFunctions.EncryptQueryString(Convert.ToInt32(ViewState["jobId"]).ToString());
            //var jobId = GeneralFunctions.DecryptQueryString(Request.QueryString["jobId"]);
            Response.Redirect("~/Forwarding/Transaction/Dashboard.aspx?JobId=" + encryptedId);
        }

        protected void ddlBillingFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPartyValuesSetToDdl(ddlBillingFrom.SelectedValue.ToInt());
        }

        protected void lnkQuoUpload_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();

            //var documentName = hdnQuoPath.Value;
            //var ext = System.IO.Path.GetExtension(hdnQuoPath.Value);
            //string filePath = string.Format(hdnQuoPath.Value);
            //System.IO.FileInfo file = new System.IO.FileInfo(filePath);

            //if (file.Exists)
            //{
            //    Response.Clear();
            //    Response.AddHeader("Content-Length", file.Length.ToString());
            //    Response.Buffer = true;
            //    Response.ContentType = MimeType(ext);
            //    Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}.{1}", documentName, ext));
            //    Response.WriteFile(filePath);
            //    Response.End();
            //}
        }

        private static string MimeType(string Extension)
        {
            string mime = "application/octetstream";
            if (string.IsNullOrEmpty(Extension))
                return mime;

            string ext = Extension.ToLower();
            RegistryKey rk = Registry.ClassesRoot.OpenSubKey(ext);
            if (rk != null && rk.GetValue("Content Type") != null)
                mime = rk.GetValue("Content Type").ToString();
            return mime;
        }

        protected void btnCancelContainer_Click(object sender, EventArgs e)
        {
            if (QuotationUpload.HasFile)
            {
                var fileName = QuotationUpload.FileName;
                var filext = fileName.Substring(fileName.LastIndexOf(".") + 1);
                if (filext.ToLower() != "pdf")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('Only pdf file is accepted!');</script>", false);
                    return;
                }
                var path = Server.MapPath("~/Forwarding/QuoUpload");
                var newFileName = "Quotation" + hdnLastNo.Value.TrimEnd();  //  Guid.NewGuid().ToString();

                if (!string.IsNullOrEmpty(path))
                {
                    path += @"\" + newFileName + System.IO.Path.GetExtension(fileName);
                    QuotationUpload.PostedFile.SaveAs(path);
                }
                string encryptedId = GeneralFunctions.EncryptQueryString(Convert.ToInt32(ViewState["jobId"]).ToString());
                Response.Redirect("~/Forwarding/Transaction/Dashboard.aspx?JobId=" + encryptedId);
            }
            else
            {
                string encryptedId = GeneralFunctions.EncryptQueryString(Convert.ToInt32(ViewState["jobId"]).ToString());
                Response.Redirect("~/Forwarding/Transaction/Dashboard.aspx?JobId=" + encryptedId);
            }
            //if (QuotationUpload.HasFile)
            //{
            //    var fileName = QuotationUpload.FileName;
            //    lnkQuoUpload.Text = fileName;
            //    var filext = fileName.Substring(fileName.LastIndexOf(".") + 1);
            //    if (filext.ToLower() != "pdf")
            //    {
            //        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('Only pdf file is accepted!');</script>", false);
            //        fileName = "";
            //        return;
            //    }
            //    else
            //        fileName += filext;

            //}
        }

        protected void TextEx_TextChanged(object sender, EventArgs e)
        {
            List<Charge> charges = null;
            DataSet ds = new DataSet();
            var estimate = new Estimate();
            charges = (List<Charge>)ViewState["Charges"];
            if (charges.Count > 0)
            {
                for (int count = 0; count < charges.Count; count++)
                {
                    if (charges[count].CurrencyId.ToInt() == 2)
                    {
                        charges[count].ROE = txtExRate.Text.ToDouble();
                        ds = new EstimateBLL().GetServiceTax(lblJobDate.Text.ToDateTime(), (charges[count].Unit * charges[count].Rate * charges[count].ROE).ToDecimal(), charges[count].ChargeMasterId);
                        if (ds.Tables[1].Rows[0]["ServiceTax"].ToInt() == 1)
                        {
                            charges[count].STax = (ds.Tables[0].Rows[0]["stax"].ToDecimal() + ds.Tables[0].Rows[0]["CessAmt"].ToDecimal() + ds.Tables[0].Rows[0]["AddCess"].ToDecimal()).ToDouble();
                        }
                        else
                        {
                            charges[count].STax = 0;
                        }
                        charges[count].INR = (charges[count].Unit * charges[count].Rate * charges[count].ROE) + charges[count].STax;
                    }
                }
                lblCharges.Text = charges.Sum(m => m.INR).ToString("#########0.00");
                ViewState["Charges"] = charges;
                grvCharges.DataSource = charges;
                grvCharges.DataBind();
                PopulateUnitType();
            }
        }
    }
}