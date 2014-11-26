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


namespace EMS.WebApp.Forwarding.Master
{
    public partial class AddEditEstimate : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private string countryId = "";
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

        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {

           
            var ddlCurrency = (DropDownList)grvCharges.FooterRow.FindControl("ddlCurrency");
            var txtROE = (TextBox)grvCharges.FooterRow.FindControl("txtROE");
            if (ddlCurrency != null)
            {
                var ex = new EstimateBLL().GetExchange(new SearchCriteria() { StringOption1 = ddlCurrency.SelectedValue });

                txtROE.Text = "0";
                if (ex != null && ex.Tables.Count > 0 && ex.Tables[0].Rows.Count > 0)
                {
                    txtROE.Text = Convert.ToString(ex.Tables[0].Rows[0]["USDXchRate"]);
                }
                txtROE.Enabled = true;
                if (ddlCurrency.SelectedValue == "1") { 
                    
                    txtROE.Enabled = false;
                    txtROE.Text = "1";
                }
            }
            CalculateAndAssign();


        }

        private void CalculateAndAssign()
        {
            var txtUnit = (TextBox)grvCharges.FooterRow.FindControl("txtUnit");
            var txtRate = (TextBox)grvCharges.FooterRow.FindControl("txtRate");
            var txtROE = (TextBox)grvCharges.FooterRow.FindControl("txtROE");
            var spINR = (Label)grvCharges.FooterRow.FindControl("lblINR");


            var unit = 0;
            var rate = 0.0;
            var roe = 0.0;

            try
            {
                unit = Convert.ToInt32(txtUnit.Text);
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
            if (spINR != null)
            {
                spINR.Text = (unit * rate * roe).ToString();
            }
        }

        protected void Text_TextChanged(object sender, EventArgs e)
        {
            CalculateAndAssign();
        }

        protected void grvCharges_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            List<Charge> charges = null;
            if (e.CommandName == "Add")
            {
                var estimate = new Estimate();

                if (ViewState["Charges"] != null)
                {
                    charges = (List<Charge>)ViewState["Charges"];
                }
                else
                {
                    charges = new List<Charge>();
                }

                if (grvCharges.FooterRow != null)
                {
                    Charge charge = new Charge();
                    var id = 1;
                    if (charges.Count > 0)
                    {
                      id=  charges.Max(f => f.ChargeId);
                    }
                    var ddlCharges = (DropDownList)grvCharges.FooterRow.FindControl("ddlCharges");
                    var txtUnit = (TextBox)grvCharges.FooterRow.FindControl("txtUnit");
                    var txtRate = (TextBox)grvCharges.FooterRow.FindControl("txtRate");
                    var ddlCurrency = (DropDownList)grvCharges.FooterRow.FindControl("ddlCurrency");
                    var txtROE = (TextBox)grvCharges.FooterRow.FindControl("txtROE");
                    charge.ChargeMasterId = Convert.ToInt32(ddlCharges.SelectedValue);
                    charge.ChargeMasterName = ddlCharges.SelectedItem.Text;
                    charge.CurrencyId = Convert.ToInt32(ddlCurrency.SelectedValue);
                    charge.Currency = ddlCurrency.SelectedItem.Text;
                    charge.ChargeId = id + 1;
                    charge.ChargeType = rdoPayment.SelectedValue;
                    try
                    {
                        charge.Unit = Convert.ToInt32(txtUnit.Text);
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
                    charges.Add(charge);

                    lblCharges.Text = charges.Sum(m => m.INR).ToString();

                    lblTotalUnit.Text = charges.Sum(m => m.Unit).ToString();
                }
            }
            else if (e.CommandName == "Remove")
            {
                charges.Remove(charges.FindAll(f => f.ChargeId.ToString().Equals(e.CommandArgument)).FirstOrDefault());
            }

            if (charges.Count == 0)
            {
                SetEmptyGrid();
            }
            else
            {
                ViewState["Charges"] = charges;
                grvCharges.DataSource = charges;
                grvCharges.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            RetriveParameters();
            if (!IsPostBack)
            {
                LoadDefault();
            }

            //   CheckUserAccess(countryId);
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
            trChargesInDays.Visible = false;
            if (rdoPayment.SelectedValue == "C")
            {
                trChargesInDays.Visible = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveAdjustmentModel();

        }

        #endregion

        #region Private Methods

        private void GetPartyValuesSetToDdl()
        {
            ddlParty.Items.Clear();
            ddlParty.Items.Add(new ListItem("--Select--", "0"));
            if (Mode == "A")
            {
                if (ViewState["IsPayment"].ToInt() == 0)
                {
                    var value = GeneralFunctions.DecryptQueryString(Request.QueryString["DlName"]);
                    var id = GeneralFunctions.DecryptQueryString(Request.QueryString["DlValues"]);
                    if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(id) || value.Trim().Length == 0 || id.Trim().Length == 0)
                    {
                        throw new Exception("Please provide Party Data");
                    }
                    var ids = id.Split(',');
                    var values = value.Split(',');

                    if (ids != null && values != null && ids.Length == values.Length)
                    {
                        for (int i = 0; i < ids.Length; i++)
                        {
                            ddlParty.Items.Add(new ListItem(values[i], ids[i]));
                        }
                    }
                }
                else
                {
                    DataSet dllCreditor = new DataSet();

                    dllCreditor = new EstimateBLL().GetParty();

                    ddlParty.DataSource = dllCreditor;
                    ddlParty.DataTextField = "PartyName";
                    ddlParty.DataValueField = "pk_fwPartyID";
                    ddlParty.DataBind();
                    ddlParty.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
        }

        private void LoadDefault()
        {
            var estimateId = Request.QueryString["EstimateId"];
            rdoPayment_SelectedIndexChanged(null, null);
            if (Request.QueryString["EstimateId"] != null)
                estimateId = GeneralFunctions.DecryptQueryString(Request.QueryString["EstimateId"]);

            ViewState["Id"] = estimateId;
            var units = new EstimateBLL().GetUnitMaster(new SearchCriteria { StringOption1 = companyId });
            ddlUnitType.DataSource = units;
            ddlUnitType.DataTextField = "UnitName";
            ddlUnitType.DataValueField = "pk_UnitTypeID";
            ddlUnitType.DataBind();
            ddlUnitType.Items.Insert(0, new ListItem("--Select--", "0"));

            var billingFrom = new EstimateBLL().GetBillingGroupMaster((ISearchCriteria)null);
            ddlBillingFrom.DataSource = billingFrom;

            ddlBillingFrom.DataTextField = "BillFrom";
            ddlBillingFrom.DataValueField = "pk_BillFromID";
            ddlBillingFrom.DataBind();
            ddlBillingFrom.Items.Insert(0, new ListItem("--Select--", "0"));

            if (!string.IsNullOrEmpty(estimateId))
            {
                var estimate = new EstimateBLL().GetEstimate(new SearchCriteria { StringOption1 = estimateId });
                var temp = new List<Charge>();

                ViewState["IsPayment"] = estimate.TransactionType == "P" ? 1 : 0;
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

                    ddlUnitType.SelectedValue = estimate.UnitTypeId.ToString();
                    rdoPayment.SelectedValue = estimate.PorR.ToString();
                    txtChargesInDays.Text = estimate.CreditDays.ToString();
                    lblTotalUnit.Text = temp.Sum(x => x.Unit).ToString();
                    lblCharges.Text = temp.Sum(x => x.INR).ToString();
                    grvCharges.DataSource = temp;
                    grvCharges.DataBind();
                    ViewState["Charges"] = temp;
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
                var value = GeneralFunctions.DecryptQueryString(Request.QueryString["IsPayment"]);
                if (string.IsNullOrEmpty(value) || !(value == "1" || value == "0"))
                {
                    throw new Exception("Invalid Request Transaction type");
                }
                ViewState["IsPayment"] = value;

           
                var jobId = GeneralFunctions.DecryptQueryString(Request.QueryString["jobId"]);
                if (string.IsNullOrEmpty(jobId))
                {
                    throw new Exception("Invalid jobId");
                }
                ViewState["jobId"] = jobId;
               
                GetPartyValuesSetToDdl();

            }
           
            //grvCharges.ShowFooter = false;

        }

        private Estimate ExtractData()
        {
            var unitId= Convert.ToInt32(ddlUnitType.SelectedValue);
            var est= new Estimate
            {
                EstimateId = EstmateId,
                BillFromId = Convert.ToInt32(ddlBillingFrom.SelectedValue),
                PartyId = Convert.ToInt32(ddlParty.SelectedValue),
                UnitTypeId = unitId,
                PaymentIn = rdoPayment.SelectedValue,
                CreditDays = string.IsNullOrEmpty(txtChargesInDays.Text)?(int?)null: Convert.ToInt32(txtChargesInDays.Text),
                Charges = (List<Charge>)ViewState["Charges"],
                PorR=rdoPayment.SelectedValue,
                CompanyID = 1,
                TotalCharges=Convert.ToDouble(lblCharges.Text),
                TransactionType= TransactionType,
                UserID = _userId,
                EstimateDate=DateTime.Now,
                JobID = JobID

            };
            est.Charges.ForEach(x => x.UnitId = unitId);
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


            var result = new EstimateBLL().SaveEstimate(data, Mode);
            if (result > 0)
            {
                string encryptedId = GeneralFunctions.EncryptQueryString(Convert.ToInt32(ViewState["jobId"]).ToString());
                Response.Redirect("~/Forwarding/Transaction/Dashboard.aspx?JobId=" + encryptedId);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), DateTime.Now.Ticks.ToString(), "alert('Error Occured');", true);
            }

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
            Response.Redirect("~/Forwarding/Transaction/Dashboard.aspx?JobId=" + encryptedId);
        }
    }
}