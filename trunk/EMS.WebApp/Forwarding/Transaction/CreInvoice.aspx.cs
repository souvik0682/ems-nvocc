using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.BLL;
using EMS.Entity;
using EMS.Common;
using System.Data;
using EMS.Utilities;

namespace EMS.WebApp.Forwarding.Master
{
    public partial class CreInvoice : System.Web.UI.Page
    { 
        #region Private Member Variables


        private int _userId = 0;
        private string countryId = "";
        private string companyId = "1";
        private bool _canAdd = true;
        private bool _canEdit = true;
        private bool _canDelete = true;
        private bool _canView = true;

        private int CreInvoiceId { get { if (ViewState["Id"] != null) { return Convert.ToInt32(ViewState["Id"]); } return 0; } set { ViewState["Id"] = value; } }
        private string Mode { get { if (ViewState["Id"] != null) { return "E"; } return "A"; } }

        bool IsEmptyGrid { get; set; }
        #endregion


        #region Protected Event Handler

        protected void Page_Load(object sender, EventArgs e)
        {

            RetriveParameters();
            if (!IsPostBack)
            {
                LoadDefault();
                PopulateUnitType();
            }

            //   CheckUserAccess(countryId);
        }

        protected void grvInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
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


                //var txt2 = (TextBox)e.Row.FindControl("txtROE");
                //var txt1 = (TextBox)e.Row.FindControl("txtRate");
                //var txt3 = (TextBox)e.Row.FindControl("txtUnit");
                //if (txt1 != null) { txt1.Attributes.Add("onblur", "CalculateINR(this)"); }
                //if (txt3 != null) { txt3.Attributes.Add("onblur", "CalculateINR(this)"); }
                //if (txt2 != null) { txt2.Attributes.Add("onblur", "CalculateINR(this)"); }
            }
            else if (e.Row.RowType == DataControlRowType.DataRow && IsEmptyGrid)
            {
                e.Row.Visible = false;
                IsEmptyGrid = false;
            }
        }

        protected void ddlCharges_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ddlCharges = (DropDownList)grvInvoice.FooterRow.FindControl("ddlCharges");
            var txtSTax = (TextBox)grvInvoice.FooterRow.FindControl("lblStax");
            var ddlCurrency = (DropDownList)grvInvoice.FooterRow.FindControl("ddlCurrency");
            var txtConvRate = (TextBox)grvInvoice.FooterRow.FindControl("txtConvRate");

            DataTable dtCharge = new InvoiceBLL().GetCreChargeDetails(Convert.ToInt32(ddlCharges.SelectedValue));

            //DataTable dtSTax = new InvoiceBLL().GetServiceTax(Convert.ToDateTime(txtReferenceDate.Text));
            if (dtCharge.Rows.Count > 0)
            {
                ddlCurrency.SelectedValue = dtCharge.Rows[0]["Currency"].ToString();
                ddlCurrency_SelectedIndexChanged(null, null);
            }
        }

        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ddlCurrency = (DropDownList)grvInvoice.FooterRow.FindControl("ddlCurrency");
            var txtConvRate = (TextBox)grvInvoice.FooterRow.FindControl("txtConvRate");
            if (ddlCurrency != null)
            {
                var ex = new EstimateBLL().GetExchange(new SearchCriteria() { StringOption1 = ddlCurrency.SelectedValue, Date = lblJobDate.Text.ToDateTime() });
                txtConvRate.Text = "0";
                if (ex != null && ex.Tables.Count > 0 && ex.Tables[0].Rows.Count > 0)
                {
                    //txtConvRate.Text = Convert.ToString(ex.Tables[0].Rows[0]["USDXchRate"]);
                    txtConvRate.Text = txtExRate.Text;
                }
                txtConvRate.Enabled = true;
                if (ddlCurrency.SelectedValue == "1") 
                {
                    txtConvRate.Enabled = false;
                    txtConvRate.Text = "1";
                }
                else if (ddlCurrency.SelectedValue == "2")
                {
                    txtConvRate.Enabled = false;
                }
            }
            CalculateAndAssign();
        }

        private CreditorInvoiceCharge GetCharge()
        {
            var CessAmt = 0.0;
            var ACessAmt = 0.0;
            DataSet ds = new DataSet();

            var txtUnit = (TextBox)grvInvoice.FooterRow.FindControl("txtUnit");
            var txtRate = (TextBox)grvInvoice.FooterRow.FindControl("txtRate");
            var txtSTax = (TextBox)grvInvoice.FooterRow.FindControl("lblStax");
            var txtConvRate = (TextBox)grvInvoice.FooterRow.FindControl("txtConvRate");
            var ddlChargeID = (DropDownList)grvInvoice.FooterRow.FindControl("ddlCharges");

            //var sTax = string.IsNullOrEmpty(txtSTax.Text)?0.0: Convert.ToDouble(txtSTax.Text);
            var unit = string.IsNullOrEmpty(txtUnit.Text) ? 0.0 : Convert.ToDouble(txtUnit.Text);
            var rate = string.IsNullOrEmpty(txtRate.Text) ? 0.0 : Convert.ToDouble(txtRate.Text);
            var convRate = string.IsNullOrEmpty(txtConvRate.Text) ? 0.0 : Convert.ToDouble(txtConvRate.Text);

            int ChargeID = ddlChargeID.SelectedValue.ToInt();
            var total = 0.0;
            var gross = 0.0;
            var gtotal = 0.0;
            try
            {
                total = unit * rate;
            }
            catch
            {
                total = 0;
            }
            try
            {
                gross = total * convRate;
            }
            catch
            {
                gross = 0;
            }

            var stx = 0.0;
            try 
            { 
                //stx = (gross * sTax) / 100;
                //CessAmt = ViewState["CessPer"].ToDouble() * (sTax * gross / 100) / 100;
                //ACessAmt = ViewState["ACessPer"].ToDouble() * (sTax * gross / 100) / 100;
                //stx = sTax;

                ds = new EstimateBLL().GetServiceTax(lblJobDate.Text.ToDateTime(), (unit * rate * convRate).ToDecimal(), ChargeID);
                if (ds.Tables[1].Rows[0]["ServiceTax"].ToInt() == 1)
                {
                    txtSTax.Text = (ds.Tables[0].Rows[0]["stax"].ToDecimal() + ds.Tables[0].Rows[0]["CessAmt"].ToDecimal() + ds.Tables[0].Rows[0]["AddCess"].ToDecimal()).ToString("#######0.00");
                    stx = Convert.ToDouble(txtSTax.Text);
                }
                else
                {
                    txtSTax.Text = "0";
                    stx = 0;
                }

            }
            catch { }

            try
            {
                gtotal = gross + stx; // + CessAmt + ACessAmt;
            }
            catch
            {
                gross = 0;
            }
            var ddlCharges = (DropDownList)grvInvoice.FooterRow.FindControl("ddlCharges");
            var ddlCurrency = (DropDownList)grvInvoice.FooterRow.FindControl("ddlCurrency");
            var ddlSize = (DropDownList)grvInvoice.FooterRow.FindControl("ddlSize");
            var ddlUnitType = (DropDownList)grvInvoice.FooterRow.FindControl("ddlUnitType");

            CreditorInvoiceCharge charge = new CreditorInvoiceCharge();
            
            charge.ChargeId = Convert.ToInt32(ddlCharges.SelectedValue);
            charge.CurrencyId = Convert.ToInt32(ddlCurrency.SelectedValue);
            charge.Currency = ddlCurrency.SelectedItem.Text;

            charge.ChargeName = ddlCharges.SelectedItem.Text;
            charge.ConvRate = convRate;
            charge.Unit = unit;
            charge.UnitType = ddlUnitType.SelectedItem.Text;
            charge.UnitTypeID = ddlUnitType.SelectedValue.ToInt();
            charge.CntrSize = ddlSize.SelectedValue;
            

            charge.Total = total;
            charge.Gross = gross;
            //charge.STaxPercentage = sTax;

            charge.STax = stx;
            charge.STaxCess = CessAmt;
            charge.STaxACess = ACessAmt;

            charge.GTotal = gtotal;
            charge.Rate = rate;
            return charge;
        }

        private void CalculateAndAssign()
        {
            var lblGross = (Label)grvInvoice.FooterRow.FindControl("lblGross");
            var lblSTax = (TextBox)grvInvoice.FooterRow.FindControl("lblSTax");

            //var lblTotal = (Label)grvInvoice.FooterRow.FindControl("lblTotal");
            var lblGTotal = (TextBox)grvInvoice.FooterRow.FindControl("lblGTotal");

            CreditorInvoiceCharge charge = GetCharge();
            if (charge != null)
            {
                try { lblGross.Text = charge.Gross.ToString("0.00"); }
                catch { lblGross.Text = "0"; }
                try { lblSTax.Text = (charge.STax+charge.STaxCess+charge.STaxACess).ToString("0.00"); }
                catch { lblSTax.Text = "0"; }
                //try { lblTotal.Text = charge.Total.ToString("0.00"); }
                //catch { lblTotal.Text = "0"; }
                try { lblGTotal.Text = charge.GTotal.ToString("0.00"); }
                catch { lblGTotal.Text = "0"; }
            }
        }

        protected void Text_TextChanged(object sender, EventArgs e)
        {
            CalculateAndAssign();
        }

        protected void grvInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            List<CreditorInvoiceCharge> charges = null;
            if (ViewState["Charges"] != null)
            {
                charges = (List<CreditorInvoiceCharge>)ViewState["Charges"];
            }
            else
            {
                charges = new List<CreditorInvoiceCharge>();
            }
            if (e.CommandName == "Add")
            {
               
                if (grvInvoice.FooterRow != null)
                {
                    CreditorInvoiceCharge charge = new CreditorInvoiceCharge();


                    //CreditorInvoiceCharge charge = GetCharge();
                    var id = 1;
                    if (charges.Count > 0)
                    {
                        id = charges.Max(f => f.CreditorInvoiceChargeId);
                    }
                    charge.CreditorInvoiceChargeId = id;   
                    var CessAmt = 0.0;


                    var txtUnit = (TextBox)grvInvoice.FooterRow.FindControl("txtUnit");
                    var txtRate = (TextBox)grvInvoice.FooterRow.FindControl("txtRate");
                    var txtSTax = (TextBox)grvInvoice.FooterRow.FindControl("lblStax");
                    var txtConvRate = (TextBox)grvInvoice.FooterRow.FindControl("txtConvRate");
                    var ddlChargeID = (DropDownList)grvInvoice.FooterRow.FindControl("ddlCharges");

                    var sTax = string.IsNullOrEmpty(txtSTax.Text)?0.0: Convert.ToDouble(txtSTax.Text);
                    var unit = string.IsNullOrEmpty(txtUnit.Text) ? 0.0 : Convert.ToDouble(txtUnit.Text);
                    var rate = string.IsNullOrEmpty(txtRate.Text) ? 0.0 : Convert.ToDouble(txtRate.Text);
                    var convRate = string.IsNullOrEmpty(txtConvRate.Text) ? 0.0 : Convert.ToDouble(txtConvRate.Text);

                    int ChargeID = ddlChargeID.SelectedValue.ToInt();
                    var total = 0.0;
                    var gross = 0.0;
                    var gtotal = 0.0;
                    try
                    {
                        total = unit * rate;
                    }
                    catch
                    {
                        total = 0;
                    }
                    try
                    {
                        gross = total * convRate;
                    }
                    catch
                    {
                        gross = 0;
                    }

                    var stx = 0.0;
                    try 
                    { 
                        stx = sTax;

                    }
                    catch 
                    {
                        stx = 0.0;
                    }

                    try
                    {
                        gtotal = gross + stx; // + CessAmt + ACessAmt;
                    }
                    catch
                    {
                        gross = 0;
                    }
                    var ddlCharges = (DropDownList)grvInvoice.FooterRow.FindControl("ddlCharges");
                    var ddlCurrency = (DropDownList)grvInvoice.FooterRow.FindControl("ddlCurrency");
                    var ddlSize = (DropDownList)grvInvoice.FooterRow.FindControl("ddlSize");
                    var ddlUnitType = (DropDownList)grvInvoice.FooterRow.FindControl("ddlUnitType");
            
                    charge.ChargeId = Convert.ToInt32(ddlCharges.SelectedValue);
                    charge.CurrencyId = Convert.ToInt32(ddlCurrency.SelectedValue);
                    charge.Currency = ddlCurrency.SelectedItem.Text;

                    charge.ChargeName = ddlCharges.SelectedItem.Text;
                    charge.ConvRate = convRate;
                    charge.Unit = unit;
                    charge.UnitType = ddlUnitType.SelectedItem.Text;
                    charge.UnitTypeID = ddlUnitType.SelectedValue.ToInt();
                    charge.CntrSize = ddlSize.SelectedValue;
            

                    charge.Total = total;
                    charge.Gross = gross;
                    //charge.STaxPercentage = sTax;

                    charge.STax = sTax;
                    //charge.STaxCess = CessAmt;
                    //charge.STaxACess = ACessAmt;

                    charge.GTotal = gtotal;
                    charge.Rate = rate;

                    charges.Add(charge);

                    var sum = charges.Sum(m => m.GTotal);
                    hdnInvoiceAmount.Value = sum.ToString();

                    if (chkRoff.Checked == true)
                    {
                        txtRoff.Text = Math.Round((Math.Round(sum) - sum), 2).ToString();
                        lblInvoiceAmount.Text = String.Format("{0:0.00}", sum + Math.Round((Math.Round(sum) - sum), 2));
                        
                    }
                    else
                    {
                        txtRoff.Text = "0";
                        lblInvoiceAmount.Text = String.Format("{0:0.00}", sum, 2);

                    }
                }
            }
            else if (e.CommandName == "Remove")
            {
                try
                {
                    var t = charges.FindAll(f => f.CreditorInvoiceChargeId.ToString().Equals(e.CommandArgument)).FirstOrDefault();
                    if (t != null)
                    {

                        charges.Remove(t);

                        var sum = charges.Sum(m => m.GTotal);
                        hdnInvoiceAmount.Value = sum.ToString();
                        if (chkRoff.Checked == true)
                        {
                            txtRoff.Text = Math.Round((Math.Round(sum) - sum), 2).ToString();
                            lblInvoiceAmount.Text = String.Format("{0:0.00}", sum + Math.Round((Math.Round(sum) - sum), 2));
                        }
                        else
                        {
                            txtRoff.Text = "0";
                            lblInvoiceAmount.Text = sum.ToString();
                        }
                        
                    }
                }
                catch { }
            }

            if (charges.Count == 0)
            {
                SetEmptyGrid();
            }
            else
            {
                ViewState["Charges"] = charges;
                grvInvoice.DataSource = charges;
                grvInvoice.DataBind();
                PopulateUnitType();
            }


        }

        private void RetriveParameters()
        {
            _userId = EMS.BLL.UserBLL.GetLoggedInUserId();

            //Get user permission.
            //  EMS.BLL.UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
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
            //trChargesInDays.Visible = false;
            //if (rdoPayment.SelectedValue == "C")
            //{
            //    trChargesInDays.Visible = true;
            //}
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveAdjustmentModel();
        }

        #endregion

        #region Private Methods

        private void GetJobAndSetValue(string jobId)
        {
            var reader = new CreditorInvoiceBLL().GetJobForCreInv(new SearchCriteria { StringOption1=jobId});
            if (reader != null && reader.Tables.Count > 0 && reader.Tables[0].Rows.Count > 0)
            {
                var dr=reader.Tables[0].Rows[0];
                //lblHouseBLDate.Text = Convert.ToDateTime(dr["fwdBLDate"]).ToShortDateString();
                //lblHouseBLNo.Text       = dr["fwdBLNo"].ToString();
                lblJobNumber.Text       = dr["JobNo"].ToString();
                lblLocation.Text        = dr["JobLoc"].ToString();
                lblJobDate.Text = dr["JobDate"].ToString();
                //lblEstimate.Text        = dr["EstimateNo"].ToString();

            }
        }

        private void GetPartyValuesSetToDdl(int PartyType)
        {
            ddlCreditorName.Items.Clear();
            //var creditorInvoice = new CreditorInvoiceBLL().GetCreditor((ISearchCriteria)null);

            var creditorInvoice = new EstimateBLL().GetAllParty(PartyType);
            ddlCreditorName.DataSource = creditorInvoice;
            ddlCreditorName.DataTextField = "PartyName";
            ddlCreditorName.DataValueField = "pk_fwPartyID";
            ddlCreditorName.DataBind();
            ddlCreditorName.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void GetPartyTypes()
        {
            var partyType = new EstimateBLL().GetBillingGroupMaster((ISearchCriteria)null);
            ddlPartyType.DataSource = partyType;
            ddlPartyType.DataTextField = "PartyType";
            ddlPartyType.DataValueField = "pk_PartyTypeID";
            ddlPartyType.DataBind();
            ddlPartyType.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void LoadDefault()
        {
            var creInvoiceId = string.Empty;
            string EstimateId = "";
            
            rdoPayment_SelectedIndexChanged(null, null);
            if (!ReferenceEquals(Request.QueryString["CreInvoiceId"], null))
            {
                creInvoiceId = GeneralFunctions.DecryptQueryString(Request.QueryString["CreInvoiceId"].ToString());
                ViewState["Id"] = creInvoiceId;
            }
            else
                ViewState["Id"] = null;

            if (!ReferenceEquals(Request.QueryString["JobNo"], null))
                lblJobNumber.Text = GeneralFunctions.DecryptQueryString(Request.QueryString["JobNo"].ToString());
                //creInvoiceId = Request.QueryString["CreInvoiceId"];

            string jobId = "";
            txtReferenceDate.Text = DateTime.Now.GetDateTimeFormats('d')[0];
            if (!ReferenceEquals(Request.QueryString["JobID"], null))
                jobId = GeneralFunctions.DecryptQueryString(Request.QueryString["JobID"].ToString());
            JobID = jobId;
            GetJobAndSetValue(jobId);

            ViewState["JobID"] = jobId;

            //GetPartyValuesSetToDdl();
            GetPartyTypes();
            chkRoff.Checked = false;
            txtRoff.Enabled = false;
            txtRoff.Text = "";

            if (!string.IsNullOrEmpty(creInvoiceId))
            {
                var creditorInvoice = new CreditorInvoiceBLL().GetCreditorInvoice(new SearchCriteria { StringOption1 = CreInvoiceId.ToString() }).FirstOrDefault();
                var temp = new List<CreditorInvoiceCharge>();

                if (creditorInvoice != null)
                {
                    if (creditorInvoice.CreditorInvoiceCharges != null)
                    {
                        DataSet ds = new DataSet();
                        if (ViewState["ddlCharges"] == null)
                        {
                            ds = new EstimateBLL().GetCharges((ISearchCriteria)null);
                            ViewState["ddlCharges"] = ds;
                        }
                        else
                        {
                            ds = (DataSet)ViewState["ddlCharges"];
                        }

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

                        foreach (var obj in creditorInvoice.CreditorInvoiceCharges) {
                           
                            DataRow t = ds.Tables[0].AsEnumerable().Where(x => x["ChargeId"].ToString() == obj.ChargeId.ToString()).FirstOrDefault();
                            obj.ChargeName = t["ChargeName"].ToString();
                            DataRow t1 = dllCds.Tables[0].AsEnumerable().Where(x => x["pk_CurrencyID"].ToString() == obj.CurrencyId.ToString()).FirstOrDefault();
                            obj.Currency = t1["CurrencyName"].ToString();
                            try { obj.Total = obj.Rate * obj.Unit; }
                            catch { obj.Total = 0; }
                            }
                        temp = creditorInvoice.CreditorInvoiceCharges;
                        creditorInvoice.InvoiceAmount = creditorInvoice.CreditorInvoiceCharges.Sum(x => x.GTotal);

                    }

                    ViewState["Charges"] = temp;
                    ddlCreditorName.Items.Clear();
                    ddlPartyType.SelectedValue = creditorInvoice.PartyTypeID.ToString();
                    GetPartyValuesSetToDdl(ddlPartyType.SelectedValue.ToInt());
                    //ddlCreditorName.Items.Add(new ListItem("--Select--", "0"));
                    //ddlCreditorName.Items.Add(new ListItem(creditorInvoice.CreditorName, creditorInvoice.CreditorId.ToString()));

                    ddlCreditorName.SelectedValue = creditorInvoice.CreditorId.ToString();
                    

                    txtCreInvoiceDate.Text = creditorInvoice.CreInvoiceDate.Value.ToShortDateString();
                    txtCreInvoiceNo.Text = creditorInvoice.CreInvoiceNo;
                    txtReferenceDate.Text = creditorInvoice.ReferenceDate.Value.ToShortDateString();
                    //lblHouseBLDate.Text      = creditorInvoice.HouseBLDate.Value.ToShortDateString();
                    //lblHouseBLNo.Text        = creditorInvoice.HouseBLNo;
                    lblInvoiceAmount.Text    = creditorInvoice.InvoiceAmount.ToString();
                    hdnInvoiceAmount.Value = creditorInvoice.InvoiceAmount.ToString();
                    txtRoff.Text = creditorInvoice.RoundingOff.ToString();
                    if (creditorInvoice.RoundingOff == 0)
                        chkRoff.Checked = false;
                    else
                        chkRoff.Checked = true;

                    lblJobNumber.Text        = creditorInvoice.JobNumber;
                   // lblLocation.Text         = creditorInvoice.Location;
                    lblOurInvoiceRef.Text    = creditorInvoice.OurInvoiceRef;
                    txtExRate.Text = creditorInvoice.ROE.ToString();
                    GetJobAndSetValue(creditorInvoice.JobNumber);
                    

                    grvInvoice.DataSource = temp;
                    grvInvoice.DataBind();
                    ViewState["Charges"] = temp;


                    //ViewState["jobId"] = creditorInvoice.JobNumber;
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
                //var value = Request.QueryString["IsPayment"];
                //if (string.IsNullOrEmpty(value) || !(value == "1" || value == "0"))
                //{
                //    throw new Exception("Invalid Request Transaction type");
                //}
                //ViewState["IsPayment"] = value;
              
                var ex = new EstimateBLL().GetExchange(new SearchCriteria() { StringOption1 = "2", Date = lblJobDate.Text.ToDateTime() });
                txtExRate.Text = Convert.ToString(ex.Tables[0].Rows[0]["USDXchRate"]);
            }

            //grvInvoice.ShowFooter = false;
        }

        private CreditorInvoice ExtractData()
        {

           var est = new CreditorInvoice
            {
                CreditorInvoiceId = CreInvoiceId,
                CreditorId = Convert.ToInt32(ddlCreditorName.SelectedValue),
                PartyTypeID = Convert.ToInt32(ddlPartyType.SelectedValue),
                CreInvoiceNo = txtCreInvoiceNo.Text,
                OurInvoiceRef = lblOurInvoiceRef.Text,
                //HouseBLNo = lblHouseBLNo.Text,
                Location = lblLocation.Text,
                CreditorInvoiceCharges = (List<CreditorInvoiceCharge>)ViewState["Charges"],
                ReferenceDate = Convert.ToDateTime(txtReferenceDate.Text),
                CompanyID = 1,
                InvoiceAmount = Convert.ToDouble(lblInvoiceAmount.Text),
                RoundingOff = Convert.ToDouble(txtRoff.Text),
                UserID = _userId,
                CreInvoiceDate = Convert.ToDateTime(txtCreInvoiceDate.Text),
                JobNumber = JobID

            };
            return est;

        }
        public string TransactionType { get {
            if (ViewState["IsPayment"] != null) {
                var t = Convert.ToInt32(ViewState["IsPayment"]);
                if (t == 1)
                    return "P";
            }
            return "R";
        } }

        public string JobID
        {
            get
            {
                if (ViewState["jobId"] != null)
                {
                    return Convert.ToString(ViewState["jobId"]);
                }
                throw new Exception("Job Id couldnt be found.");
            }
            set { ViewState["jobId"] = value; }
        }
        private void SaveAdjustmentModel()
        {
            var data = ExtractData();


            hdnLastNo.Value = new CreditorInvoiceBLL().SaveCreditorInvoice(data, Mode).ToString();
            //if (result > 0)
            //{
            if (hdnLastNo.Value.ToInt() > 0)
            {
                ModalPopupExtender1.Show();
                //string encryptedId = GeneralFunctions.EncryptQueryString(ViewState["jobId"].ToString());
                //Response.Redirect("~/Forwarding/Transaction/Dashboard.aspx?JobId=" + encryptedId);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), DateTime.Now.Ticks.ToString(), "alert('Error Occured');", true);
            }

        }

        private void SetEmptyGrid()
        {
            grvInvoice.ShowFooter = true;
            IsEmptyGrid = true;
            List<CreditorInvoiceCharge> dr = new List<CreditorInvoiceCharge>() { new CreditorInvoiceCharge { } };
            grvInvoice.DataSource = dr;
            grvInvoice.DataBind();
            ViewState["Charges"] = new List<CreditorInvoiceCharge>();
        }
        #endregion

        protected void btnCancel_Click(object sender, EventArgs e)
        {
                string encryptedId = GeneralFunctions.EncryptQueryString(ViewState["jobId"].ToString());
                Response.Redirect("~/Forwarding/Transaction/Dashboard.aspx?JobId=" + encryptedId);
        }

        protected void ddlPartyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPartyValuesSetToDdl(ddlPartyType.SelectedValue.ToInt());
        }

        protected void ddlSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateUnitType();
            CheckContainer();

        }

        private void PopulateUnitType()
        {
            var dllU = (DropDownList)grvInvoice.FooterRow.FindControl("ddlUnitType");
            //var dllU = (DropDownList)e.Row.FindControl("ddlUnitType");
            var curtype = "E";

            DataSet dllUs = new DataSet();


            if (ViewState["dllUs"] == null)
            {
                var ddlSize = (DropDownList)grvInvoice.FooterRow.FindControl("ddlSize");
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

        protected void ddlUnitType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            var ddlUnitTypeID = (DropDownList)grvInvoice.FooterRow.FindControl("ddlUnitType");
            ds = new EstimateBLL().GetSingleUnitType(ddlUnitTypeID.SelectedValue.ToInt(), ViewState["jobId"].ToInt());
            var ddlSizeID = (DropDownList)grvInvoice.FooterRow.FindControl("ddlSize");
            //var rfvSize = (RequiredFieldValidator)grvCharges.FooterRow.FindControl("rfvSize");
            var txtUnit = (TextBox)grvInvoice.FooterRow.FindControl("txtUnit");
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

        private void CheckContainer()
        {
            var ddlUnitTypeID = (DropDownList)grvInvoice.FooterRow.FindControl("ddlUnitType");
            var ddlSizeID = (DropDownList)grvInvoice.FooterRow.FindControl("ddlSize");
            var txtUnit = (TextBox)grvInvoice.FooterRow.FindControl("txtUnit");

            if (ddlUnitTypeID.SelectedIndex != 0 && ddlSizeID.SelectedIndex != 0)
            {
                DataSet ds = new DataSet();
                ds = new EstimateBLL().GetContainers(ddlUnitTypeID.SelectedValue.ToInt(), ddlSizeID.SelectedValue.ToString(), ViewState["jobId"].ToInt());
                if (ds.Tables[0].Rows.Count > 0)
                    txtUnit.Text = ds.Tables[0].Rows[0]["Nos"].ToString();
                else
                    txtUnit.Text = "0";
            }
        }

        protected void btnCancelContainer_Click(object sender, EventArgs e)
        {
            if (InvoiceUpload.HasFile)
            {
                var fileName = InvoiceUpload.FileName;
                var filext = fileName.Substring(fileName.LastIndexOf(".") + 1);
                if (filext.ToLower() != "pdf")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('Only pdf file is accepted!');</script>", false);
                    return;
                }
                var path = Server.MapPath("~/Forwarding/QuoUpload");
                var newFileName = "CreInvoice" + hdnLastNo.Value.TrimEnd();  //  Guid.NewGuid().ToString();

                if (!string.IsNullOrEmpty(path))
                {
                    path += @"\" + newFileName + System.IO.Path.GetExtension(fileName);
                    InvoiceUpload.PostedFile.SaveAs(path);
                }
                string encryptedId = GeneralFunctions.EncryptQueryString(Convert.ToInt32(ViewState["jobId"]).ToString());
                Response.Redirect("~/Forwarding/Transaction/Dashboard.aspx?JobId=" + encryptedId);
            }
            else
            {
                string encryptedId = GeneralFunctions.EncryptQueryString(Convert.ToInt32(ViewState["jobId"]).ToString());
                Response.Redirect("~/Forwarding/Transaction/Dashboard.aspx?JobId=" + encryptedId);
            }
        }

        protected void chkRoff_CheckedChanged(object sender, EventArgs e)
        {
      

            if (chkRoff.Checked)
            {
                txtRoff.Text = Math.Round((Math.Round(hdnInvoiceAmount.Value.ToDecimal()) - hdnInvoiceAmount.Value.ToDecimal()), 2).ToString();
                lblInvoiceAmount.Text = String.Format("{0:0.00}", hdnInvoiceAmount.Value.ToDecimal() + Math.Round((Math.Round(hdnInvoiceAmount.Value.ToDecimal()) - hdnInvoiceAmount.Value.ToDecimal()), 2));
            }
            else
            {
                txtRoff.Enabled = false;
                txtRoff.Text = "0";
                lblInvoiceAmount.Text = String.Format("{0:0.00}", hdnInvoiceAmount.Value.ToDecimal(), 2); 
            }
        }

        protected void TextEx_TextChanged(object sender, EventArgs e)
        {
            List<CreditorInvoiceCharge> charges = null;
            DataSet ds = new DataSet();
            var estimate = new Estimate();
            charges = (List<CreditorInvoiceCharge>)ViewState["Charges"];
            if (charges.Count > 0)
            {
                for (int count = 0; count < charges.Count; count++)
                {
                    if (charges[count].CurrencyId.ToInt() == 2)
                    {
                        charges[count].ConvRate = txtExRate.Text.ToDouble();
                        ds = new EstimateBLL().GetServiceTax(lblJobDate.Text.ToDateTime(), (charges[count].Unit * charges[count].Rate * charges[count].ConvRate).ToDecimal(), charges[count].ChargeId);
                        if (ds.Tables[1].Rows[0]["ServiceTax"].ToInt() == 1 && charges[count].STax != 0)
                        {
                            charges[count].STax = (ds.Tables[0].Rows[0]["stax"].ToDecimal() + ds.Tables[0].Rows[0]["CessAmt"].ToDecimal() + ds.Tables[0].Rows[0]["AddCess"].ToDecimal()).ToDouble();
                        }
                        else
                        {
                            charges[count].STax = 0;
                        }
                        charges[count].GTotal = (charges[count].Unit * charges[count].Rate * charges[count].ConvRate) + charges[count].STax;
                    }
                }
                lblInvoiceAmount.Text = charges.Sum(m => m.GTotal).ToString();
                ViewState["Charges"] = charges;
                grvInvoice.DataSource = charges;
                grvInvoice.DataBind();
            }
        }
        protected void lblStax_TextChanged(object sender, EventArgs e)
        {
            var txtUnit = (TextBox)grvInvoice.FooterRow.FindControl("txtUnit");
            var txtRate = (TextBox)grvInvoice.FooterRow.FindControl("txtRate");
            var txtSTax = (TextBox)grvInvoice.FooterRow.FindControl("lblStax");
            var txtConvRate = (TextBox)grvInvoice.FooterRow.FindControl("txtConvRate");
            var sTax = string.IsNullOrEmpty(txtSTax.Text) ? 0.0 : Convert.ToDouble(txtSTax.Text);
            var unit = string.IsNullOrEmpty(txtUnit.Text) ? 0.0 : Convert.ToDouble(txtUnit.Text);
            var rate = string.IsNullOrEmpty(txtRate.Text) ? 0.0 : Convert.ToDouble(txtRate.Text);
            var convRate = string.IsNullOrEmpty(txtConvRate.Text) ? 0.0 : Convert.ToDouble(txtConvRate.Text);

            var lblGross = (Label)grvInvoice.FooterRow.FindControl("lblGross");
            var lblGTotal = (TextBox)grvInvoice.FooterRow.FindControl("lblGTotal");

            var total = 0.0;
            var gross = 0.0;
            var gtotal = 0.0;
            var CessAmt = 0.0;
            var ACessAmt = 0.0;
            try
            {
                total = unit * rate;
            }
            catch
            {
                total = 0;
            }
            try
            {
                gross = total * convRate;
            }
            catch
            {
                gross = 0;
            }

            var stx = 0.0;
            try
            {
                stx = sTax.ToDouble();
                CessAmt = 0;
                ACessAmt = 0;
            }
            catch { }

            try
            {
                gtotal = gross + stx + CessAmt + ACessAmt;
            }
            catch
            {
                gross = 0;
            }
            lblGTotal.Text = gross.ToString();

            
        }
    }
}