using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.BLL;
using EMS.Utilities;
using EMS.Entity;
using EMS.Utilities.ResourceManager;
using EMS.Common;

namespace EMS.WebApp.MasterModule
{
    public partial class charge_add_edit : System.Web.UI.Page
    {
        List<ChargeRateEntity> oChargeRates;
        ChargeBLL oChargeBLL;
        ChargeRateEntity oEntity;
        ChargeEntity oChargeEntity;
        private int _locId = 0;
        private int _userId = 0;
        List<IChargeRate> Rates = new List<IChargeRate>();

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserAccess();

            IUser user = (IUser)Session[Constants.SESSION_USER_INFO];
            _userId = user == null ? 0 : user.Id;

            RetriveParameters();
            if (!Page.IsPostBack)
            {
                fillAllDropdown();
                //dgChargeRates.DataBind();


                if (hdnChargeID.Value != "0")
                {
                    FillChargeDetails(Convert.ToInt32(hdnChargeID.Value));
                    FillChargeRate(Convert.ToInt32(hdnChargeID.Value));
                    TerminalSelection(rdbTerminalRequired);
                    WashingSelection(rdbWashing);
                    SharingSelection(rdbPrincipleSharing);
                    ShowHideControlofFooter(ddlChargeType);
                    DisableAllField();
                }
                else
                {
                    DefaultSelection();
                    oChargeRates = new List<ChargeRateEntity>();
                    ChargeRateEntity Ent = new ChargeRateEntity();
                    oChargeRates.Add(Ent);
                    dgChargeRates.DataSource = oChargeRates;
                    dgChargeRates.DataBind();
                    dgChargeRates.Rows[0].Visible = false;

                    WashingSelection(rdbWashing);
                }

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
        }

        private void RetriveParameters()
        {
            //_userId = UserBLL.GetLoggedInUserId();

            if (!ReferenceEquals(Request.QueryString["id"], null))
            {
                Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _locId);
                hdnChargeID.Value = _locId.ToString();
            }
        }

        void fillAllDropdown()
        {
            ListItem Li = null;

            #region ChargeType
            foreach (Enums.ChargeType r in Enum.GetValues(typeof(Enums.ChargeType)))
            {
                Li = new ListItem("SELECT", "0");
                ListItem item = new ListItem(Enum.GetName(typeof(Enums.ChargeType), r).Replace('_', ' '), ((int)r).ToString());
                ddlChargeType.Items.Add(item);
            }
            ddlChargeType.Items.Insert(0, Li);
            #endregion

            #region Currency
            foreach (Enums.Currency r in Enum.GetValues(typeof(Enums.Currency)))
            {
                //Li = new ListItem("SELECT", "0");
                ListItem item = new ListItem(Enum.GetName(typeof(Enums.Currency), r).Replace('_', ' '), ((int)r).ToString());
                ddlCurrency.Items.Add(item);
            }
            //ddlCurrency.Items.Insert(0, Li);
            #endregion

            #region ImportExportGeneral
            //foreach (Enums.ImportExportGeneral r in Enum.GetValues(typeof(Enums.ImportExportGeneral)))
            //{
            //    Li = new ListItem("SELECT", "0");
            //    ListItem item = new ListItem(Enum.GetName(typeof(Enums.ImportExportGeneral), r).Replace('_', ' '), ((int)r).ToString());
            //    ddlImportExportGeneral.Items.Add(item);
            //}
            //ddlImportExportGeneral.Items.Insert(0, Li);
            #endregion

            #region Line

            PopulateDropDown((int)Enums.DropDownPopulationFor.Line, ddlLine, 0);
            Li = new ListItem("NA", "0");
            ddlLine.Items.Insert(0, Li);
            #endregion

            #region Master list of Location, Terminal, WashinfType

            Li = new ListItem("ALL", "-1");
            PopulateDropDown((int)Enums.DropDownPopulationFor.Location, ddlMLocation, 0);
            ddlMLocation.Items.Insert(0, Li);

            Li = new ListItem("ALL", "-1");
            PopulateDropDown((int)Enums.DropDownPopulationFor.TerminalCode, ddlMTerminal, 0);
            ddlMTerminal.Items.Insert(0, Li);
            //PopulateDropDown((int)Enums.DropDownPopulationFor.Wa, ddlMWashingType, 0);

            foreach (Enums.WashingType r in Enum.GetValues(typeof(Enums.WashingType)))
            {
                Li = new ListItem("SELECT", "0");
                ListItem item = new ListItem(Enum.GetName(typeof(Enums.WashingType), r).Replace('_', ' '), ((int)r).ToString());
                ddlMWashingType.Items.Add(item);
            }
            ddlMWashingType.Items.Insert(0, Li);
            #endregion


        }

        void PopulateDropDown(int Number, DropDownList ddl, int? Filter)
        {
            CommonBLL.PopulateDropdown(Number, ddl, Filter);
        }

        protected void ddlLocationID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdbTerminalRequired.SelectedValue == "1")
            {
                DropDownList ddlFLocation = (DropDownList)sender;
                GridViewRow item = (GridViewRow)((DropDownList)sender).NamingContainer;
                DropDownList ddlFTerminal = (DropDownList)item.FindControl("ddlFTerminal");
                PopulateDropDown((int)Enums.DropDownPopulationFor.TerminalCode, ddlFTerminal, Convert.ToInt32(ddlFLocation.SelectedValue));
                if (ddlFLocation.SelectedValue == "0")
                {
                    ddlFTerminal.Items.Clear();
                }
                else if (ddlFLocation.SelectedValue == "-1")
                {
                    ddlFTerminal.Items.Clear();
                    ListItem Li = new ListItem("ALL", "-1");
                    ddlFTerminal.Items.Insert(0, Li);
                }

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                oChargeEntity = new ChargeEntity();
                oChargeBLL = new ChargeBLL();


                if (rdbRateChange.SelectedItem.Value == "0")
                {
                    if (ViewState["ChargeRates"] == null)
                    {
                        lblMessage.Text = "Please enter rates!";
                        return;
                    }
                    else
                    {
                        Rates = (List<IChargeRate>)ViewState["ChargeRates"];
                        if (Rates.Count() <= 0)
                        {
                            lblMessage.Text = "Please enter rates!";
                            return;
                        }
                    }
                }

                oChargeEntity.ChargeActive = true;
                oChargeEntity.ChargeDescr = txtChargeName.Text.Trim();
                oChargeEntity.ChargeType = Convert.ToInt32(ddlChargeType.SelectedValue);

                //This ID will be the companyid of the currently loggedin user
                oChargeEntity.CompanyID = 1;
                oChargeEntity.Currency = Convert.ToInt32(ddlCurrency.SelectedValue);
                oChargeEntity.EffectDt = Convert.ToDateTime(txtEffectDate.Text.Trim());
                oChargeEntity.IEC = Convert.ToChar(ddlImportExportGeneral.SelectedValue);
                oChargeEntity.IsFreightComponent = Convert.ToBoolean(Convert.ToInt32(rdbFreightComponent.SelectedItem.Value));
                oChargeEntity.IsTerminal = Convert.ToBoolean(Convert.ToInt32(rdbTerminalRequired.SelectedItem.Value));
                oChargeEntity.IsWashing = Convert.ToBoolean(Convert.ToInt32(rdbWashing.SelectedItem.Value));
                oChargeEntity.NVOCCID = Convert.ToInt32(ddlLine.SelectedValue);
                oChargeEntity.PrincipleSharing = Convert.ToBoolean(Convert.ToInt32(rdbPrincipleSharing.SelectedItem.Value));
                oChargeEntity.RateChangeable = Convert.ToBoolean(Convert.ToInt32(rdbRateChange.SelectedItem.Value));
                if (!String.IsNullOrEmpty(txtDisplayOrder.Text.Trim()))
                    oChargeEntity.Sequence = Convert.ToInt32(txtDisplayOrder.Text.Trim());
                oChargeEntity.ServiceTax = Convert.ToBoolean(Convert.ToInt32(rdbServiceTaxApplicable.SelectedItem.Value));



                if (Convert.ToInt32(hdnChargeID.Value) <= 0) //insert
                {
                    oChargeEntity.CreatedBy = _userId;// oUserEntity.Id;
                    oChargeEntity.CreatedOn = DateTime.Today.Date;
                    oChargeEntity.ModifiedBy = _userId;// oUserEntity.Id;
                    oChargeEntity.ModifiedOn = DateTime.Today.Date;

                    hdnChargeID.Value = oChargeBLL.AddEditCharge(oChargeEntity).ToString();

                    if (Convert.ToInt32(hdnChargeID.Value) > 0)
                    {
                        AddRates();
                        lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00009");
                        ClearAll();
                        EnableAllField();
                        ViewState["ChargeRates"] = null;
                        WashingSelection(rdbWashing);
                    }
                    else if (hdnChargeID.Value == "-1")
                    {
                        lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00076");
                    }
                    else
                    {
                        lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00011");
                    }

                }
                else   //update
                {
                    oChargeEntity.ChargesID = Convert.ToInt32(hdnChargeID.Value);
                    oChargeEntity.ModifiedBy = _userId;// oUserEntity.Id;
                    oChargeEntity.ModifiedOn = DateTime.Today.Date;

                    switch (oChargeBLL.AddEditCharge(oChargeEntity))
                    {
                        case 0: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00011");
                            break;
                        case 1:
                            AddRates();
                            Response.Redirect("~/MasterModule/charge-list.aspx");
                            break;
                        case -1: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00076");
                            break;
                    }
                }

            }
        }

        void AddRates()
        {
            Rates = (List<IChargeRate>)ViewState["ChargeRates"];
            if (Rates == null)
                Rates = new List<IChargeRate>();
            oChargeBLL = new ChargeBLL();

            oChargeBLL.DeactivateAllRatesAgainstChargeId(Convert.ToInt32(hdnChargeID.Value));

            foreach (IChargeRate Rate in Rates)
            {
                Rate.ChargesID = Convert.ToInt32(hdnChargeID.Value);
                oChargeBLL.AddEditChargeRates(Rate);
            }
        }

        void FillChargeDetails(int ChargesID)
        {
            oChargeEntity = new ChargeEntity();
            oChargeBLL = new ChargeBLL();
            oChargeEntity = (ChargeEntity)oChargeBLL.GetChargeDetails(ChargesID);


            //oChargeEntity.ChargeActive = true;
            txtChargeName.Text = oChargeEntity.ChargeDescr;
            ddlChargeType.SelectedIndex = ddlChargeType.Items.IndexOf(ddlChargeType.Items.FindByValue(oChargeEntity.ChargeType.ToString()));

            //This ID will be the companyid of the currently loggedin user
            //oChargeEntity.CompanyID = 1;
            ddlCurrency.SelectedIndex = ddlCurrency.Items.IndexOf(ddlCurrency.Items.FindByValue(oChargeEntity.Currency.ToString()));
            txtEffectDate.Text = oChargeEntity.EffectDt.ToShortDateString();
            ddlImportExportGeneral.SelectedIndex = ddlImportExportGeneral.Items.IndexOf(ddlImportExportGeneral.Items.FindByValue(oChargeEntity.IEC.ToString()));
            ddlLine.SelectedIndex = ddlLine.Items.IndexOf(ddlLine.Items.FindByValue(oChargeEntity.NVOCCID.ToString()));
            txtDisplayOrder.Text = oChargeEntity.Sequence.ToString();


            rdbServiceTaxApplicable.Items.FindByValue(oChargeEntity.ServiceTax.ToString().ToLower() == "true" ? "1" : "0").Selected = true;
            rdbFreightComponent.Items.FindByValue(oChargeEntity.IsFreightComponent.ToString().ToLower() == "true" ? "1" : "0").Selected = true;
            rdbTerminalRequired.Items.FindByValue(oChargeEntity.IsTerminal.ToString().ToLower() == "true" ? "1" : "0").Selected = true;
            rdbWashing.Items.FindByValue(oChargeEntity.IsWashing.ToString().ToLower() == "true" ? "1" : "0").Selected = true;
            //WashingSelection(rdbWashing);
            string ss = oChargeEntity.PrincipleSharing.ToString().ToLower() == "true" ? "1" : "0";
            rdbPrincipleSharing.Items.FindByValue(oChargeEntity.PrincipleSharing.ToString().ToLower() == "true" ? "1" : "0").Selected = true;
            //SharingSelection(rdbPrincipleSharing);
            rdbRateChange.Items.FindByValue(oChargeEntity.RateChangeable.ToString().ToLower() == "true" ? "1" : "0").Selected = true;

            hdnChargeID.Value = oChargeEntity.ChargesID.ToString();


        }
        void FillChargeRate(int ChargesID)
        {
            //oChargeRates = new List<ChargeRateEntity>();
            oChargeBLL = new ChargeBLL();
            Rates = new List<IChargeRate>();
            Rates = oChargeBLL.GetChargeRates(ChargesID);
            if (Rates.Count <= 0)
            {
                IChargeRate rt = new ChargeRateEntity();
                Rates.Add(rt);
            }
            else
            {
                ViewState["ChargeRates"] = Rates;
            }

            dgChargeRates.DataSource = Rates;
            dgChargeRates.DataBind();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MasterModule/charge-list.aspx");
        }



        protected void dgChargeRates_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                ListItem Li;
                DropDownList ddlFLocation = (DropDownList)e.Row.FindControl("ddlFLocation");
                DropDownList ddlFWashingType = (DropDownList)e.Row.FindControl("ddlFWashingType");

                PopulateDropDown((int)Enums.DropDownPopulationFor.Location, ddlFLocation, 0);
                Li = new ListItem("SELECT", "0");
                ddlFLocation.Items.Insert(0, Li);
                Li = new ListItem("ALL", "-1");
                ddlFLocation.Items.Insert(1, Li);

                foreach (Enums.WashingType r in Enum.GetValues(typeof(Enums.WashingType)))
                {
                    Li = new ListItem("SELECT", "0");
                    ListItem item = new ListItem(Enum.GetName(typeof(Enums.WashingType), r).Replace('_', ' '), ((int)r).ToString());
                    ddlFWashingType.Items.Add(item);
                }
                ddlFWashingType.Items.Insert(0, Li);

            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Delete link
                ImageButton btnRemove = (ImageButton)e.Row.FindControl("lnkDelete");
                btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00012");
                //btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pk_PortId"));


                btnRemove.OnClientClick = "javascript:return confirm('" + ResourceManager.GetStringWithoutName("ERR00014") + "');";

                //btnEdit.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00008") + "');return false;";
                //btnRemove.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00008") + "');return false;";

            }
        }
        protected void dgChargeRates_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (ViewState["ChargeRates"] == null)
            {
                ViewState["ChargeRates"] = Rates;
            }
            else
            {
                Rates = (List<IChargeRate>)ViewState["ChargeRates"];
            }


            #region Save
            if (e.CommandArgument == "Save")
            {
                //if (hdnChargeID.Value != "0")
                //{
                oChargeBLL = new ChargeBLL();
                GridViewRow Row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

                TextBox txtHigh = (TextBox)Row.FindControl("txtHigh");
                TextBox txtLow = (TextBox)Row.FindControl("txtLow");

                if ((!String.IsNullOrEmpty(txtHigh.Text) && !String.IsNullOrEmpty(txtLow.Text)))
                {
                    if (Convert.ToDecimal(txtHigh.Text) < Convert.ToDecimal(txtLow.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00077") + "');</script>", false);
                        return;
                    }
                }

                DropDownList ddlFLocation = (DropDownList)Row.FindControl("ddlFLocation");
                DropDownList ddlFTerminal = (DropDownList)Row.FindControl("ddlFTerminal");
                DropDownList ddlFWashingType = (DropDownList)Row.FindControl("ddlFWashingType");


                TextBox txtRatePerBL = (TextBox)Row.FindControl("txtRatePerBL");
                TextBox txtRatePerTEU = (TextBox)Row.FindControl("txtRatePerTEU");
                TextBox txtRateperFEU = (TextBox)Row.FindControl("txtRateperFEU");
                TextBox txtSharingBL = (TextBox)Row.FindControl("txtSharingBL");
                TextBox txtSharingTEU = (TextBox)Row.FindControl("txtSharingTEU");
                TextBox txtSharingFEU = (TextBox)Row.FindControl("txtSharingFEU");

                HiddenField hdnFId = (HiddenField)Row.FindControl("hdnFId");
                HiddenField hdnId = (HiddenField)Row.FindControl("hdnId");
                HiddenField hdnFSlno = (HiddenField)Row.FindControl("hdnFSlno");

                if (rdbPrincipleSharing.SelectedValue == "1")
                {
                    if (String.IsNullOrEmpty(txtRatePerBL.Text))
                        txtRatePerBL.Text = "0.00";
                    if (String.IsNullOrEmpty(txtSharingBL.Text))
                        txtSharingBL.Text = "0.00";
                    if (String.IsNullOrEmpty(txtRateperFEU.Text))
                        txtRateperFEU.Text = "0.00";
                    if (String.IsNullOrEmpty(txtSharingFEU.Text))
                        txtSharingFEU.Text = "0.00";
                    if (String.IsNullOrEmpty(txtRatePerTEU.Text))
                        txtRatePerTEU.Text = "0.00";
                    if (String.IsNullOrEmpty(txtSharingTEU.Text))
                        txtSharingTEU.Text = "0.00";


                    if (Convert.ToDecimal(txtRatePerBL.Text) < Convert.ToDecimal(txtSharingBL.Text))
                    {
                        lblMessage.Text = "Sharing value can not be greater than original value";
                        return;
                    }

                    if (Convert.ToDecimal(txtRateperFEU.Text) < Convert.ToDecimal(txtSharingFEU.Text))
                    {
                        lblMessage.Text = "Sharing value can not be greater than original value";
                        return;
                    }

                    if (Convert.ToDecimal(txtRatePerTEU.Text) < Convert.ToDecimal(txtSharingTEU.Text))
                    {
                        lblMessage.Text = "Sharing value can not be greater than original value";
                        return;
                    }
                }

                oEntity = new ChargeRateEntity();
                oEntity.ChargesID = Convert.ToInt32(hdnChargeID.Value);
                if (!String.IsNullOrEmpty(txtHigh.Text))
                    oEntity.High = Convert.ToInt32(txtHigh.Text);
                oEntity.LocationId = Convert.ToInt32(ddlFLocation.SelectedValue);
                if (!String.IsNullOrEmpty(txtLow.Text))
                    oEntity.Low = Convert.ToInt32(txtLow.Text);
                if (!String.IsNullOrEmpty(txtRatePerBL.Text))
                    oEntity.RatePerBL = Convert.ToDecimal(txtRatePerBL.Text);
                if (!String.IsNullOrEmpty(txtSharingBL.Text))
                    oEntity.SharingBL = Convert.ToDecimal(txtSharingBL.Text);
                if (!String.IsNullOrEmpty(txtSharingFEU.Text))
                    oEntity.SharingFEU = Convert.ToDecimal(txtSharingFEU.Text);
                if (!String.IsNullOrEmpty(txtSharingTEU.Text))
                    oEntity.SharingTEU = Convert.ToDecimal(txtSharingTEU.Text);
                oEntity.WashingType = Convert.ToInt32(ddlFWashingType.SelectedValue);
                if (ddlFTerminal.Items.Count > 0 && ddlFTerminal.SelectedIndex >= 0)
                    oEntity.TerminalId = Convert.ToInt32(ddlFTerminal.SelectedValue);

                //Header change & CBM/TON selection to be implemented below
                oEntity.RateActive = true;
                if (ddlChargeType.SelectedValue == "6") //If LCL selected, then data will go to CBM & TON
                {
                    if (!String.IsNullOrEmpty(txtRateperFEU.Text))
                        oEntity.RatePerCBM = Convert.ToDecimal(txtRateperFEU.Text);
                    if (!String.IsNullOrEmpty(txtRatePerTEU.Text))
                        oEntity.RatePerTON = Convert.ToDecimal(txtRatePerTEU.Text);
                }
                else
                {
                    if (!String.IsNullOrEmpty(txtRateperFEU.Text))
                        oEntity.RatePerFEU = Convert.ToDecimal(txtRateperFEU.Text);
                    if (!String.IsNullOrEmpty(txtRatePerTEU.Text))
                        oEntity.RatePerTEU = Convert.ToDecimal(txtRatePerTEU.Text);
                }


                int Locn = 0;
                int Ter = 0;
                int Was = 0;

                Locn = Convert.ToInt32(ddlFLocation.SelectedValue);
                if (ddlFTerminal.Items.Count > 0)
                    Ter = Convert.ToInt32(ddlFTerminal.SelectedValue);
                if (ddlFWashingType.Items.Count > 0)
                    Was = Convert.ToInt32(ddlFWashingType.SelectedValue);

                if (Convert.ToInt32(hdnFSlno.Value) >= 0)
                {
                    IEnumerable<IChargeRate> cr = RangeValidationCheck(txtHigh, txtLow, Locn, Ter, Was);
                    if (cr.Count() <= 0)
                    {
                        Rates.RemoveAt(Convert.ToInt32(hdnFSlno.Value));
                        Rates.Insert(Convert.ToInt32(hdnFSlno.Value), oEntity);
                    }
                    else
                    {
                        lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00079");
                        return;
                    }
                }
                else
                {
                    //int Locn = 0;
                    //int Ter = 0;
                    //int Was = 0;

                    //Locn = Convert.ToInt32(ddlFLocation.SelectedValue);
                    //if (ddlFTerminal.Items.Count > 0)
                    //    Ter = Convert.ToInt32(ddlFTerminal.SelectedValue);
                    //if (ddlFWashingType.Items.Count > 0)
                    //    Was = Convert.ToInt32(ddlFWashingType.SelectedValue);


                    if (txtHigh.Enabled == false && txtLow.Enabled == false)
                    {
                        IEnumerable<IChargeRate> cr = from rate in Rates
                                                      where rate.LocationId == Locn && rate.TerminalId == Ter && rate.WashingType == Was
                                                      select rate;

                        if (cr.Count() <= 0)
                        {
                            Rates.Add(oEntity);
                            lblMessage.Text = string.Empty;
                        }
                        else
                        {
                            lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00076");
                            return;
                        }
                    }

                    else
                    {
                        IEnumerable<IChargeRate> cr = RangeValidationCheck(txtHigh, txtLow, Locn, Ter, Was);

                        if (cr.Count() <= 0)
                        {
                            Rates.Add(oEntity);
                            lblMessage.Text = string.Empty;
                        }
                        else
                        {
                            lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00079");
                            return;
                        }
                    }
                }


                ViewState["ChargeRates"] = Rates;
                FillRates();

            }
            #endregion

            #region Edit
            if (e.CommandArgument == "Edit")
            {
                GridViewRow FootetRow = dgChargeRates.HeaderRow;

                DropDownList ddlFLocation = (DropDownList)FootetRow.FindControl("ddlFLocation");
                DropDownList ddlFTerminal = (DropDownList)FootetRow.FindControl("ddlFTerminal");
                DropDownList ddlFWashingType = (DropDownList)FootetRow.FindControl("ddlFWashingType");
                TextBox txtLow = (TextBox)FootetRow.FindControl("txtLow");
                TextBox txtHigh = (TextBox)FootetRow.FindControl("txtHigh");
                TextBox txtRatePerBL = (TextBox)FootetRow.FindControl("txtRatePerBL");
                TextBox txtRatePerTEU = (TextBox)FootetRow.FindControl("txtRatePerTEU");
                TextBox txtRateperFEU = (TextBox)FootetRow.FindControl("txtRateperFEU");
                TextBox txtSharingBL = (TextBox)FootetRow.FindControl("txtSharingBL");
                TextBox txtSharingTEU = (TextBox)FootetRow.FindControl("txtSharingTEU");
                TextBox txtSharingFEU = (TextBox)FootetRow.FindControl("txtSharingFEU");
                HiddenField hdnFId = (HiddenField)FootetRow.FindControl("hdnFId");
                HiddenField hdnFSlno = (HiddenField)FootetRow.FindControl("hdnFSlno");


                GridViewRow Row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                HiddenField hdnLocationId = (HiddenField)Row.FindControl("hdnLocationId");
                HiddenField hdnTerminalId = (HiddenField)Row.FindControl("hdnTerminalId");
                HiddenField hdnWashingTypeId = (HiddenField)Row.FindControl("hdnWashingTypeId");
                HiddenField hdnId = (HiddenField)Row.FindControl("hdnId");

                Label lblLow = (Label)Row.FindControl("lblLow");
                Label lblHigh = (Label)Row.FindControl("lblHigh");
                Label lblRatePerBl = (Label)Row.FindControl("lblRatePerBl");
                Label lblRatePerTEU = (Label)Row.FindControl("lblRatePerTEU");
                Label lblRatePerFEU = (Label)Row.FindControl("lblRatePerFEU");
                Label lblSharingBL = (Label)Row.FindControl("lblSharingBL");
                Label lblSharingTEU = (Label)Row.FindControl("lblSharingTEU");
                Label lblSharingFEU = (Label)Row.FindControl("lblSharingFEU");

                ddlFLocation.SelectedIndex = ddlFLocation.Items.IndexOf(ddlFLocation.Items.FindByValue(hdnLocationId.Value));
                PopulateDropDown((int)Enums.DropDownPopulationFor.TerminalCode, ddlFTerminal, Convert.ToInt32(ddlFLocation.SelectedValue));

                ddlFTerminal.SelectedIndex = ddlFTerminal.Items.IndexOf(ddlFTerminal.Items.FindByValue(hdnTerminalId.Value));
                ddlFWashingType.SelectedIndex = ddlFWashingType.Items.IndexOf(ddlFWashingType.Items.FindByValue(hdnWashingTypeId.Value));


                txtLow.Text = lblLow.Text;
                txtHigh.Text = lblHigh.Text;
                txtRatePerBL.Text = lblRatePerBl.Text;
                txtRatePerTEU.Text = lblRatePerTEU.Text;
                txtRateperFEU.Text = lblRatePerFEU.Text;
                txtSharingBL.Text = lblSharingBL.Text;
                txtSharingTEU.Text = lblSharingTEU.Text;
                txtSharingFEU.Text = lblSharingFEU.Text;
                hdnFId.Value = hdnId.Value.ToString();
                hdnFSlno.Value = Row.RowIndex.ToString();

            }
            #endregion

            #region Delete
            if (e.CommandArgument == "Delete")
            {
                GridViewRow Row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                HiddenField hdnId = (HiddenField)Row.FindControl("hdnId");
                //ChargeBLL.DeleteChargeRate(Convert.ToInt32(hdnId.Value));
                //FillChargeRate(Convert.ToInt32(hdnChargeID.Value));
                Rates = (List<IChargeRate>)ViewState["ChargeRates"];
                //Rates[Row.RowIndex].RateActive = false;
                Rates.RemoveAt(Row.RowIndex);
                ViewState["ChargeRates"] = Rates;
                if (Rates.Count <= 0 && hdnChargeID.Value == "0")
                {
                    EnableAllField();
                }
                FillRates();

            }
            #endregion

            #region Cancel
            if (e.CommandArgument == "Cancel")
            {
                GridViewRow Row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                TextBox txtHigh = (TextBox)Row.FindControl("txtHigh");
                TextBox txtLow = (TextBox)Row.FindControl("txtLow");
                DropDownList ddlFLocation = (DropDownList)Row.FindControl("ddlFLocation");
                DropDownList ddlFTerminal = (DropDownList)Row.FindControl("ddlFTerminal");
                DropDownList ddlFWashingType = (DropDownList)Row.FindControl("ddlFWashingType");

                TextBox txtRatePerBL = (TextBox)Row.FindControl("txtRatePerBL");
                TextBox txtRatePerTEU = (TextBox)Row.FindControl("txtRatePerTEU");
                TextBox txtRateperFEU = (TextBox)Row.FindControl("txtRateperFEU");
                TextBox txtSharingBL = (TextBox)Row.FindControl("txtSharingBL");
                TextBox txtSharingTEU = (TextBox)Row.FindControl("txtSharingTEU");
                TextBox txtSharingFEU = (TextBox)Row.FindControl("txtSharingFEU");
                HiddenField hdnFId = (HiddenField)Row.FindControl("hdnFId");
                HiddenField hdnFSlno = (HiddenField)Row.FindControl("hdnFSlno");

                txtHigh.Text = String.Empty;// "0";
                txtLow.Text = String.Empty;// "0";
                ddlFLocation.SelectedIndex = 0;
                if (ddlFTerminal.Items.Count > 0)
                {
                    //ddlFTerminal.SelectedIndex = 0;
                    ddlFTerminal.Items.Clear();
                    //ddlFTerminal.Enabled = false;
                }
                ddlFWashingType.SelectedIndex = 0;
                txtRatePerBL.Text = String.Empty;// "0.00";
                txtRatePerTEU.Text = String.Empty;//"0.00";
                txtRateperFEU.Text = String.Empty;//"0.00";
                txtSharingBL.Text = String.Empty;//"0.00";
                txtSharingTEU.Text = String.Empty;//"0.00";
                txtSharingFEU.Text = String.Empty;//"0.00";

                hdnFId.Value = "0";
                hdnFSlno.Value = "0";
            }
            #endregion

            TerminalSelection(rdbTerminalRequired);
            WashingSelection(rdbWashing);
            SharingSelection(rdbPrincipleSharing);
            ShowHideControlofFooter(ddlChargeType);
            DisableAllField();
        }

        private IEnumerable<IChargeRate> RangeValidationCheck(TextBox txtHigh, TextBox txtLow, int Locn, int Ter, int Was)
        {
            int Min = 0, Max = 0;
            bool isValidRange = true;
            if (Rates.Count > 0)
            {
                foreach (IChargeRate rt in Rates)
                {
                    Min = rt.Low;
                    Max = rt.High;

                    for (int i = Min; i <= Max; i++)
                    {
                        if (i >= Convert.ToInt32(txtLow.Text) && i <= Convert.ToInt32(txtHigh.Text))
                        {
                            isValidRange = false;
                            break;
                        }
                    }
                }
            }

            IEnumerable<IChargeRate> cr = from rate in Rates
                                          where rate.LocationId == Locn && rate.TerminalId == Ter && rate.WashingType == Was && isValidRange == false
                                          select rate;
            return cr;
        }

        void FillRates()
        {
            Rates = (List<IChargeRate>)ViewState["ChargeRates"];
            IEnumerable<IChargeRate> Rts = from IChargeRate rt in Rates
                                           where rt.RateActive == true
                                           select rt;

            dgChargeRates.DataSource = Rts.ToList();
            dgChargeRates.DataBind();

            if (Rts.Count() <= 0)
            {
                List<IChargeRate> EmptyRates = new List<IChargeRate>();
                IChargeRate rt = new ChargeRateEntity();
                EmptyRates.Add(rt);

                dgChargeRates.DataSource = EmptyRates;
                dgChargeRates.DataBind();
                dgChargeRates.Rows[0].Visible = false;
            }


        }


        protected void rdbPrincipleSharing_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList rdl = (RadioButtonList)sender;
            SharingSelection(rdl);
        }

        protected void rdbTerminalRequired_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList rdl = (RadioButtonList)sender;
            TerminalSelection(rdl);
        }
        private void TerminalSelection(RadioButtonList rdl)
        {
            GridViewRow Row = dgChargeRates.HeaderRow;
            DropDownList ddlFTerminal = (DropDownList)Row.FindControl("ddlFTerminal");
            DropDownList ddlFLocation = (DropDownList)Row.FindControl("ddlFLocation");

            if (rdl.SelectedItem.Value == "0")
            {
                if (ddlFTerminal.Items.Count > 0)
                {
                    //ddlFTerminal.SelectedIndex = 0;
                    ddlFTerminal.Items.Clear();
                }
                ddlFTerminal.Enabled = false;
            }
            else
            {
                ddlFTerminal.Enabled = true;
                ddlFTerminal.Items.Clear();
                if (Convert.ToInt32(ddlFLocation.SelectedValue) > 0)
                    PopulateDropDown((int)Enums.DropDownPopulationFor.TerminalCode, ddlFTerminal, Convert.ToInt32(ddlFLocation.SelectedValue));

                if (ddlFLocation.SelectedValue == "-1")
                {
                    ListItem Li = new ListItem("ALL", "-1");
                    ddlFTerminal.Items.Insert(0, Li);
                }
            }
        }

        private void SharingSelection(RadioButtonList rdl)
        {
            GridViewRow Row = dgChargeRates.HeaderRow;
            TextBox txtSharingBL = (TextBox)Row.FindControl("txtSharingBL");
            TextBox txtSharingTEU = (TextBox)Row.FindControl("txtSharingTEU");
            TextBox txtSharingFEU = (TextBox)Row.FindControl("txtSharingFEU");

            if (rdl.SelectedItem.Value == "0")
            {
                txtSharingBL.Enabled = false;
                txtSharingBL.Text = String.Empty;//"0.0";

                txtSharingTEU.Enabled = false;
                txtSharingTEU.Text = String.Empty;//"0.0";

                txtSharingFEU.Enabled = false;
                txtSharingFEU.Text = String.Empty;//"0.0";
            }
            else
            {
                switch (ddlChargeType.SelectedValue)
                {
                    case "1":
                    case "3":
                    case "4":
                        txtSharingBL.Enabled = false;
                        txtSharingTEU.Enabled = true;
                        txtSharingFEU.Enabled = true;
                        break;
                    case "2":
                    case "5":
                        txtSharingBL.Enabled = true;
                        txtSharingTEU.Enabled = false;
                        txtSharingFEU.Enabled = false;
                        break;
                }


            }
        }
        protected void rdbWashing_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList rdl = (RadioButtonList)sender;
            WashingSelection(rdl);
        }

        private void WashingSelection(RadioButtonList rdl)
        {
            GridViewRow Row = dgChargeRates.HeaderRow;
            DropDownList ddlFWashingType = (DropDownList)Row.FindControl("ddlFWashingType");
            RequiredFieldValidator rfvWashing = (RequiredFieldValidator)Row.FindControl("rfvWashing");

            if (rdl.SelectedItem.Value == "0")
            {
                ddlFWashingType.SelectedIndex = 0;
                ddlFWashingType.Enabled = false;
                rfvWashing.Enabled = false;
            }
            else
            {
                ddlFWashingType.Enabled = true;
                rfvWashing.Enabled = true;
            }
        }
        protected void rdbServiceTaxApplicable_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void ClearAll()
        {
            hdnChargeID.Value = "0";
            ddlChargeType.SelectedIndex = 0;
            ddlCurrency.SelectedIndex = 0;
            ddlImportExportGeneral.SelectedIndex = 0;
            ddlLine.SelectedIndex = 0;
            ddlMLocation.SelectedIndex = 0;
            ddlMTerminal.SelectedIndex = 0;
            ddlMWashingType.SelectedIndex = 0;

            //rdbFreightComponent.SelectedIndex = 0;
            //rdbPrincipleSharing.SelectedIndex = 0;
            //rdbRateChange.SelectedIndex = 0;
            //rdbServiceTaxApplicable.SelectedIndex = 0;
            //rdbWashing.SelectedIndex = 0;

            txtChargeName.Text = string.Empty;
            txtDisplayOrder.Text = string.Empty;
            txtEffectDate.Text = string.Empty;

            DefaultSelection();

            oChargeRates = new List<ChargeRateEntity>();
            ChargeRateEntity Ent = new ChargeRateEntity();
            oChargeRates.Add(Ent);
            dgChargeRates.DataSource = oChargeRates;
            dgChargeRates.DataBind();
            dgChargeRates.Rows[0].Visible = false;
        }

        void DefaultSelection()
        {
            rdbFreightComponent.SelectedIndex = 1;
            rdbPrincipleSharing.SelectedIndex = 0;
            rdbRateChange.SelectedIndex = 1;
            rdbServiceTaxApplicable.SelectedIndex = 0;
            rdbWashing.SelectedIndex = 1;
            rdbTerminalRequired.SelectedIndex = 1;


        }

        protected void ddlChargeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ShowHideControlofFooter(sender);
            DropDownList ddlChargeType = (DropDownList)sender;
            ShowHideControlofFooter(ddlChargeType);
        }

        private void ShowHideControlofFooter(DropDownList ddlChargeType)
        {
            //DropDownList ddlChargeType = (DropDownList)sender;
            GridViewRow FootetRow = dgChargeRates.HeaderRow;
            GridViewRow HeaderRow = dgChargeRates.HeaderRow;

            TextBox txtLow = (TextBox)FootetRow.FindControl("txtLow");
            TextBox txtHigh = (TextBox)FootetRow.FindControl("txtHigh");
            TextBox txtRatePerBL = (TextBox)FootetRow.FindControl("txtRatePerBL");
            TextBox txtRatePerTEU = (TextBox)FootetRow.FindControl("txtRatePerTEU");
            TextBox txtRateperFEU = (TextBox)FootetRow.FindControl("txtRateperFEU");

            TextBox txtSharingBL = (TextBox)FootetRow.FindControl("txtSharingBL");
            TextBox txtSharingTEU = (TextBox)FootetRow.FindControl("txtSharingTEU");
            TextBox txtSharingFEU = (TextBox)FootetRow.FindControl("txtSharingFEU");

            Label lblFRateTeu = (Label)FootetRow.FindControl("lblFRateTeu");
            Label lblFRateFeu = (Label)FootetRow.FindControl("lblFRateFeu");
            Label lblFShareTeu = (Label)FootetRow.FindControl("lblFShareTeu");
            Label lblFShareFeu = (Label)FootetRow.FindControl("lblFShareFeu");


            RequiredFieldValidator rfvLow = (RequiredFieldValidator)FootetRow.FindControl("rfvLow");
            RequiredFieldValidator rfvHigh = (RequiredFieldValidator)FootetRow.FindControl("rfvHigh");
            RequiredFieldValidator rfvRatePerBl = (RequiredFieldValidator)FootetRow.FindControl("rfvRatePerBl");
            RequiredFieldValidator rfvRatePerTEU = (RequiredFieldValidator)FootetRow.FindControl("rfvRatePerTEU");
            RequiredFieldValidator rfvRatePerFEU = (RequiredFieldValidator)FootetRow.FindControl("rfvRatePerFEU");

            RequiredFieldValidator rfvSharingBL = (RequiredFieldValidator)FootetRow.FindControl("rfvSharingBL");
            RequiredFieldValidator rfvSharingTEU = (RequiredFieldValidator)FootetRow.FindControl("rfvSharingTEU");
            RequiredFieldValidator rfvSharingFEU = (RequiredFieldValidator)FootetRow.FindControl("rfvSharingFEU");



            //HeaderRow.Cells[6].Text = "Rate/TEU";
            lblFRateTeu.Text = "Rate/TEU";
            //HeaderRow.Cells[7].Text = "Rate/FEU";
            lblFRateFeu.Text = "Rate/FEU";
            //HeaderRow.Cells[9].Text = "Share/TEU";
            lblFShareTeu.Text = "Share/TEU";
            //HeaderRow.Cells[10].Text = "Share/FEU";
            lblFShareFeu.Text = "Share/FEU";


            switch (ddlChargeType.SelectedValue)
            {
                case "1":
                    txtRatePerTEU.Enabled = true;
                    txtRateperFEU.Enabled = true;
                    txtRatePerBL.Enabled = false;
                    txtRatePerBL.Text = String.Empty;//"0.00";
                    txtLow.Enabled = false;
                    txtLow.Text = String.Empty;//"0";
                    txtHigh.Enabled = false;
                    txtHigh.Text = String.Empty;//"0";

                    rfvRatePerTEU.Enabled = true;
                    rfvRatePerFEU.Enabled = true;
                    rfvLow.Enabled = false;
                    rfvHigh.Enabled = false;
                    rfvRatePerBl.Enabled = false;

                    if (rdbPrincipleSharing.SelectedItem.Value == "0")
                    {
                        txtSharingFEU.Enabled = false;
                        txtSharingTEU.Enabled = false;
                        txtSharingBL.Enabled = false;

                        rfvSharingBL.Enabled = false;
                        rfvSharingFEU.Enabled = false;
                        rfvSharingTEU.Enabled = false;

                    }
                    else
                    {
                        txtSharingFEU.Enabled = true;
                        txtSharingTEU.Enabled = true;
                        txtSharingBL.Enabled = false;

                        rfvSharingBL.Enabled = false;
                        rfvSharingFEU.Enabled = true;
                        rfvSharingTEU.Enabled = true;
                    }

                    break;

                case "2":
                    txtRatePerBL.Enabled = true;

                    txtRatePerTEU.Enabled = false;
                    txtRatePerTEU.Text = String.Empty;//"0.00";
                    txtRateperFEU.Enabled = false;
                    txtRateperFEU.Text = String.Empty;//"0.00";
                    txtLow.Enabled = false;
                    txtLow.Text = String.Empty;//"0";
                    txtHigh.Enabled = false;
                    txtHigh.Text = String.Empty;//"0";

                    rfvRatePerBl.Enabled = true;
                    rfvLow.Enabled = false;
                    rfvHigh.Enabled = false;
                    rfvRatePerTEU.Enabled = false;
                    rfvRatePerFEU.Enabled = false;

                    if (rdbPrincipleSharing.SelectedItem.Value == "0")
                    {
                        txtSharingBL.Enabled = false;
                        txtSharingFEU.Enabled = false;
                        txtSharingTEU.Enabled = false;
                        rfvSharingBL.Enabled = false;
                        rfvSharingFEU.Enabled = false;
                        rfvSharingTEU.Enabled = false;
                    }
                    else
                    {
                        txtSharingBL.Enabled = true;
                        txtSharingFEU.Enabled = false;
                        txtSharingTEU.Enabled = false;

                        rfvSharingBL.Enabled = true;
                        rfvSharingFEU.Enabled = false;
                        rfvSharingTEU.Enabled = false;
                    }

                    break;

                case "3":
                case "4":
                    txtLow.Enabled = true;
                    txtHigh.Enabled = true;
                    txtRatePerTEU.Enabled = true;
                    txtRateperFEU.Enabled = true;

                    txtRatePerBL.Enabled = false;
                    txtRatePerBL.Text = String.Empty;//"0.00";

                    rfvLow.Enabled = true;
                    rfvHigh.Enabled = true;

                    rfvRatePerBl.Enabled = false;
                    rfvRatePerTEU.Enabled = true;
                    rfvRatePerFEU.Enabled = true;

                    if (rdbPrincipleSharing.SelectedItem.Value == "0")
                    {
                        txtSharingFEU.Enabled = false;
                        txtSharingTEU.Enabled = false;
                        txtSharingBL.Enabled = false;
                        rfvSharingBL.Enabled = false;
                        rfvSharingFEU.Enabled = false;
                        rfvSharingTEU.Enabled = false;
                    }
                    else
                    {
                        txtSharingFEU.Enabled = true;
                        txtSharingTEU.Enabled = true;
                        txtSharingBL.Enabled = false;

                        rfvSharingBL.Enabled = false;
                        rfvSharingFEU.Enabled = true;
                        rfvSharingTEU.Enabled = true;

                    }

                    break;
                case "5":
                    txtLow.Enabled = true;
                    txtHigh.Enabled = true;
                    txtRatePerBL.Enabled = true;

                    txtRatePerTEU.Enabled = true;
                    //txtRatePerTEU.Text = "0.00";
                    txtRateperFEU.Enabled = true;
                    //txtRateperFEU.Text = "0.00";


                    rfvLow.Enabled = true;
                    rfvHigh.Enabled = true;

                    rfvRatePerBl.Enabled = true;
                    rfvRatePerTEU.Enabled = true;
                    rfvRatePerFEU.Enabled = true;

                    if (rdbPrincipleSharing.SelectedItem.Value == "0")
                    {
                        txtSharingBL.Enabled = false;
                        txtSharingFEU.Enabled = false;
                        txtSharingTEU.Enabled = false;
                        rfvSharingBL.Enabled = false;
                        rfvSharingFEU.Enabled = false;
                        rfvSharingTEU.Enabled = false;
                    }
                    else
                    {
                        txtSharingBL.Enabled = true;
                        txtSharingFEU.Enabled = true;
                        txtSharingTEU.Enabled = true;

                        rfvSharingBL.Enabled = true;
                        rfvSharingFEU.Enabled = true;
                        rfvSharingTEU.Enabled = true;
                    }

                    break;

                case "6":

                    //HeaderRow.Cells[6].Text = "Rate/CBM";
                    lblFRateTeu.Text = "Rate/CBM";
                    //HeaderRow.Cells[7].Text = "Rate/TON";
                    lblFRateFeu.Text = "Rate/TON";
                    //HeaderRow.Cells[9].Text = "Share/CBM";
                    lblFShareTeu.Text = "Share/CBM";
                    //HeaderRow.Cells[10].Text = "Share/TON";
                    lblFShareFeu.Text = "Share/TON";

                    txtRatePerTEU.Enabled = true;
                    txtRateperFEU.Enabled = true;
                    txtRatePerBL.Enabled = false;
                    txtRatePerBL.Text = String.Empty;//"0.00";
                    txtLow.Enabled = false;
                    txtLow.Text = String.Empty;//"0";
                    txtHigh.Enabled = false;
                    txtHigh.Text = String.Empty;//"0";

                    rfvRatePerTEU.Enabled = true;
                    rfvRatePerFEU.Enabled = true;
                    rfvLow.Enabled = false;
                    rfvHigh.Enabled = false;
                    rfvRatePerBl.Enabled = false;

                    if (rdbPrincipleSharing.SelectedItem.Value == "0")
                    {
                        txtSharingFEU.Enabled = false;
                        txtSharingTEU.Enabled = false;
                        txtSharingBL.Enabled = false;
                        rfvSharingBL.Enabled = false;
                        rfvSharingFEU.Enabled = false;
                        rfvSharingTEU.Enabled = false;
                    }
                    else
                    {
                        txtSharingFEU.Enabled = true;
                        txtSharingTEU.Enabled = true;
                        txtSharingBL.Enabled = false;
                        rfvSharingBL.Enabled = false;
                        rfvSharingFEU.Enabled = true;
                        rfvSharingTEU.Enabled = true;
                    }

                    break;
            }
        }

        void DisableAllField()
        {
            txtChargeName.Enabled = false;
            txtDisplayOrder.Enabled = false;
            txtEffectDate.Enabled = false;

            ddlChargeType.Enabled = false;
            ddlCurrency.Enabled = false;
            ddlImportExportGeneral.Enabled = false;
            ddlLine.Enabled = false;

            rdbFreightComponent.Enabled = false;
            rdbPrincipleSharing.Enabled = false;
            rdbRateChange.Enabled = false;
            rdbServiceTaxApplicable.Enabled = false;
            rdbTerminalRequired.Enabled = false;
            rdbWashing.Enabled = false;

        }
        void EnableAllField()
        {
            txtChargeName.Enabled = true;
            txtDisplayOrder.Enabled = true;
            txtEffectDate.Enabled = true;

            ddlChargeType.Enabled = true;
            ddlCurrency.Enabled = true;
            ddlImportExportGeneral.Enabled = true;
            ddlLine.Enabled = true;

            rdbFreightComponent.Enabled = true;
            rdbPrincipleSharing.Enabled = true;
            rdbRateChange.Enabled = true;
            rdbServiceTaxApplicable.Enabled = true;
            rdbTerminalRequired.Enabled = true;
            rdbWashing.Enabled = true;

        }
    }
}