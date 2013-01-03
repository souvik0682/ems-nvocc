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
using System.Data;


namespace EMS.WebApp.Equipment
{
    public partial class container_movement_entry : System.Web.UI.Page
    {       

        protected void Page_Load(object sender, EventArgs e)
        {          

            //    RetriveParameters();
            if (!Page.IsPostBack)
            {
                fillAllDropdown();

                if (lblTranCode.Text == string.Empty && hdnContainerId.Value == "0")
                {
                    DataTable Dt = CreateDataTable();
                    DataRow dr = Dt.NewRow();
                    Dt.Rows.Add(dr);
                    gvSelectedContainer.DataSource = Dt;
                    gvSelectedContainer.DataBind();

                }
                //        //dgChargeRates.DataBind();

                //        if (hdnChargeID.Value != "0")
                //        {

                //            FillChargeRate(Convert.ToInt32(hdnChargeID.Value));
                //            FillChargeDetails(Convert.ToInt32(hdnChargeID.Value));
                //        }
                //        else
                //        {
                //            oChargeRates = new List<ChargeRateEntity>();
                //            ChargeRateEntity Ent = new ChargeRateEntity();
                //            oChargeRates.Add(Ent);
                //            dgChargeRates.DataSource = oChargeRates;
                //            dgChargeRates.DataBind();
                //        }

            }
        }        

        //private void RetriveParameters()
        //{
        //    //_userId = UserBLL.GetLoggedInUserId();

        //    if (!ReferenceEquals(Request.QueryString["id"], null))
        //    {
        //        Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _locId);
        //        hdnChargeID.Value = _locId.ToString();
        //    }
        //}

        void fillAllDropdown()
        {
            ListItem Li = null;

            #region ChargeType

            Li = new ListItem("Select", "0");
            PopulateDropDown((int)Enums.DropDownPopulationFor.ContainerMovementStatus, ddlFromStatus, 0);
            ddlFromStatus.Items.Insert(0, Li);

            #endregion


        }

        void PopulateDropDown(int Number, DropDownList ddl, int? Filter)
        {
            CommonBLL.PopulateDropdown(Number, ddl, Filter);
        }

        protected void ddlFromStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem Li = new ListItem("Select", "0");
            PopulateDropDown((int)Enums.DropDownPopulationFor.ContainerMovementStatus, ddlToStatus, Convert.ToInt32(ddlFromStatus.SelectedValue));
            ddlToStatus.Items.Insert(0, Li);
        }

        protected void ddlToStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFromStatus.SelectedItem.Text == "DCHE" && ddlToStatus.SelectedItem.Text == "RCVE")
            {
                //Populate Empty Yard
                ListItem Li = new ListItem();
                #region VendorList
                Li = new ListItem("Select", "0");
                PopulateDropDown((int)Enums.DropDownPopulationFor.VendorList, ddlEmptyYard, 0);
                ddlEmptyYard.Items.Insert(0, Li);
                #endregion

                ddlEmptyYard.Enabled = true;
                rfvEmptyYard.Enabled = true;
                ddlEmptyYard.SelectedIndex = 0;
            }
            else
            {
                ddlEmptyYard.Enabled = false;
                rfvEmptyYard.Enabled = false;
                if (ddlEmptyYard.Items.Count > 0)
                    ddlEmptyYard.SelectedIndex = 0;
            }

            if (ddlToStatus.SelectedItem.Text == "TRFE")
            {
                txtToLocation.Enabled = true;
                rfvToLocation.Enabled = true;
                hdnToLocation.Value = "0";
            }
            else
            {
                txtToLocation.Enabled = false;
                txtToLocation.Text = string.Empty;
                rfvToLocation.Enabled = false;
                hdnToLocation.Value = "0";
            }

        }

        protected void txtFromLocation_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = ContainerTranBLL.GetContainerTransactionListFiltered(Convert.ToInt16(ddlFromStatus.SelectedValue), Convert.ToInt32(hdnFromLocation.Value));
            
            gvContainer.DataSource = dt;
            gvContainer.DataBind();
        }

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            DataTable Dt = CreateDataTable();

            foreach (GridViewRow Row in gvContainer.Rows)
            {
                DataRow dr = Dt.NewRow();
                
                CheckBox chkContainer = (CheckBox)Row.FindControl("chkContainer");

                if (chkContainer.Checked == true)
                {
                    HiddenField hdnTransactionId = (HiddenField)Row.FindControl("hdnTransactionId");
                    HiddenField hdnStatus = (HiddenField)Row.FindControl("hdnStatus");
                    HiddenField hdnLandingDate = (HiddenField)Row.FindControl("hdnLandingDate");
                    Label lblContainerNo = (Label)Row.FindControl("lblContainerNo");
                    Label lblContainerSize = (Label)Row.FindControl("lblContainerSize");

                    dr["TransactionId"] = hdnTransactionId.Value;
                    dr["ContainerNo"] = lblContainerNo.Text;
                    dr["FromStatus"] = hdnStatus.Value;
                    dr["LandingDate"] = hdnLandingDate.Value;
                    dr["ToStatus"] = ddlToStatus.SelectedItem.Text;
                    dr["ChangeDate"] = txtDate.Text;

                    Dt.Rows.Add(dr);
                }
            }

            gvSelectedContainer.DataSource = Dt;
            gvSelectedContainer.DataBind();

        }

        private DataTable CreateDataTable()
        {
            DataTable Dt = new DataTable();
            DataColumn dc;

            dc = new DataColumn("TransactionId");
            Dt.Columns.Add(dc);

            dc = new DataColumn("ContainerNo");
            Dt.Columns.Add(dc);

            dc = new DataColumn("FromStatus");
            Dt.Columns.Add(dc);

            dc = new DataColumn("LandingDate");
            Dt.Columns.Add(dc);

            dc = new DataColumn("ToStatus");
            Dt.Columns.Add(dc);

            dc = new DataColumn("ChangeDate");
            Dt.Columns.Add(dc);
            return Dt;
        }



        //protected void ddlLocationID_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DropDownList ddlFLocation = (DropDownList)sender;
        //    GridViewRow item = (GridViewRow)((DropDownList)sender).NamingContainer;
        //    DropDownList ddlFTerminal = (DropDownList)item.FindControl("ddlFTerminal");
        //    PopulateDropDown((int)Enums.DropDownPopulationFor.TerminalCode, ddlFTerminal, Convert.ToInt32(ddlFLocation.SelectedValue));
        //}

        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    //if (Page.IsValid)
        //    //{
        //    //    oChargeEntity = new ChargeEntity();
        //    //    oChargeBLL = new ChargeBLL();

        //    //    oChargeEntity.ChargeActive = true;
        //    //    oChargeEntity.ChargeDescr = txtChargeName.Text.Trim();
        //    //    oChargeEntity.ChargeType = Convert.ToInt32(ddlChargeType.SelectedValue);

        //    //    //This ID will be the companyid of the currently loggedin user
        //    //    oChargeEntity.CompanyID = 1;
        //    //    oChargeEntity.Currency = Convert.ToInt32(ddlCurrency.SelectedValue);
        //    //    oChargeEntity.EffectDt = Convert.ToDateTime(txtEffectDate.Text.Trim());
        //    //    oChargeEntity.IEC = Convert.ToChar(ddlImportExportGeneral.SelectedValue);
        //    //    oChargeEntity.IsFreightComponent = Convert.ToBoolean(Convert.ToInt32(rdbFreightComponent.SelectedItem.Value));
        //    //    oChargeEntity.IsWashing = Convert.ToBoolean(Convert.ToInt32(rdbWashing.SelectedItem.Value));
        //    //    oChargeEntity.NVOCCID = Convert.ToInt32(ddlLine.SelectedValue);
        //    //    oChargeEntity.PrincipleSharing = Convert.ToBoolean(Convert.ToInt32(rdbPrincipleSharing.SelectedItem.Value));
        //    //    oChargeEntity.RateChangeable = Convert.ToBoolean(Convert.ToInt32(rdbRateChange.SelectedItem.Value));
        //    //    oChargeEntity.Sequence = Convert.ToInt32(txtDisplayOrder.Text.Trim());
        //    //    oChargeEntity.ServiceTax = Convert.ToBoolean(Convert.ToInt32(rdbServiceTaxApplicable.SelectedItem.Value));



        //    //    if (hdnChargeID.Value == "0") //insert
        //    //    {
        //    //        oChargeEntity.CreatedBy = 1;// oUserEntity.Id;
        //    //        oChargeEntity.CreatedOn = DateTime.Today.Date;
        //    //        oChargeEntity.ModifiedBy = 1;// oUserEntity.Id;
        //    //        oChargeEntity.ModifiedOn = DateTime.Today.Date;

        //    //        hdnChargeID.Value = oChargeBLL.AddEditCharge(oChargeEntity).ToString();

        //    //        if (hdnChargeID.Value != "0")
        //    //            lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00009");
        //    //        else
        //    //            lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00011");

        //    //    }
        //    //    else   //update
        //    //    {
        //    //        oChargeEntity.ChargesID = Convert.ToInt32(hdnChargeID.Value);
        //    //        oChargeEntity.ModifiedBy = 2;// oUserEntity.Id;
        //    //        oChargeEntity.ModifiedOn = DateTime.Today.Date;

        //    //        switch (oChargeBLL.AddEditCharge(oChargeEntity))
        //    //        {
        //    //            case 0: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00011");
        //    //                break;
        //    //            case 1: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00009");
        //    //                break;
        //    //        }
        //    //    }
        //    //}
        //}

        //void FillChargeDetails(int ChargesID)
        //{
        //    oChargeEntity = new ChargeEntity();
        //    oChargeBLL = new ChargeBLL();
        //    oChargeEntity = (ChargeEntity)oChargeBLL.GetChargeDetails(ChargesID);


        //    //oChargeEntity.ChargeActive = true;
        //    txtChargeName.Text = oChargeEntity.ChargeDescr;
        //    ddlChargeType.SelectedIndex = ddlChargeType.Items.IndexOf(ddlChargeType.Items.FindByValue(oChargeEntity.ChargeType.ToString()));

        //    //This ID will be the companyid of the currently loggedin user
        //    //oChargeEntity.CompanyID = 1;
        //    ddlCurrency.SelectedIndex = ddlCurrency.Items.IndexOf(ddlCurrency.Items.FindByValue(oChargeEntity.Currency.ToString()));
        //    txtEffectDate.Text = oChargeEntity.EffectDt.ToShortDateString();
        //    ddlImportExportGeneral.SelectedIndex = ddlImportExportGeneral.Items.IndexOf(ddlImportExportGeneral.Items.FindByValue(oChargeEntity.IEC.ToString()));
        //    ddlLine.SelectedIndex = ddlLine.Items.IndexOf(ddlLine.Items.FindByValue(oChargeEntity.NVOCCID.ToString()));
        //    txtDisplayOrder.Text = oChargeEntity.Sequence.ToString();


        //    rdbServiceTaxApplicable.Items.FindByValue(oChargeEntity.ServiceTax.ToString().ToLower() == "true" ? "1" : "0").Selected = true;
        //    rdbFreightComponent.Items.FindByValue(oChargeEntity.IsFreightComponent.ToString().ToLower() == "true" ? "1" : "0").Selected = true;
        //    rdbWashing.Items.FindByValue(oChargeEntity.IsWashing.ToString().ToLower() == "true" ? "1" : "0").Selected = true;
        //    WashingSelection(rdbWashing);
        //    rdbPrincipleSharing.Items.FindByValue(oChargeEntity.PrincipleSharing.ToString().ToLower() == "true" ? "1" : "0").Selected = true;
        //    SharingSelection(rdbPrincipleSharing);
        //    rdbRateChange.Items.FindByValue(oChargeEntity.RateChangeable.ToString().ToLower() == "true" ? "1" : "0").Selected = true;

        //    hdnChargeID.Value = oChargeEntity.ChargesID.ToString();


        //}
        //void FillChargeRate(int ChargesID)
        //{
        //    //oChargeRates = new List<ChargeRateEntity>();
        //    oChargeBLL = new ChargeBLL();
        //    List<IChargeRate> Rates = new List<IChargeRate>();
        //    Rates = oChargeBLL.GetChargeRates(ChargesID);
        //    if (Rates.Count <= 0)
        //    {
        //        IChargeRate rt = new ChargeRateEntity();
        //        Rates.Add(rt);
        //    }
        //    dgChargeRates.DataSource = Rates;
        //    dgChargeRates.DataBind();
        //}
        //protected void btnBack_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/charge-list.aspx");
        //}



        //protected void dgChargeRates_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.Footer)
        //    {
        //        ListItem Li;
        //        DropDownList ddlFLocation = (DropDownList)e.Row.FindControl("ddlFLocation");

        //        DropDownList ddlFWashingType = (DropDownList)e.Row.FindControl("ddlFWashingType");

        //        Li = new ListItem("Select", "0");
        //        PopulateDropDown((int)Enums.DropDownPopulationFor.Location, ddlFLocation, 0);
        //        ddlFLocation.Items.Insert(0, Li);

        //        foreach (Enums.WashingType r in Enum.GetValues(typeof(Enums.WashingType)))
        //        {
        //            Li = new ListItem("Select", "0");
        //            ListItem item = new ListItem(Enum.GetName(typeof(Enums.WashingType), r).Replace('_', ' '), ((int)r).ToString());
        //            ddlFWashingType.Items.Add(item);
        //        }
        //        ddlFWashingType.Items.Insert(0, Li);

        //    }
        //}
        //protected void dgChargeRates_RowCommand(object sender, GridViewCommandEventArgs e)
        //{

        //    #region Save
        //    if (e.CommandArgument == "Save")
        //    {
        //        if (hdnChargeID.Value != "0")
        //        {
        //            oChargeBLL = new ChargeBLL();
        //            GridViewRow Row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

        //            TextBox txtHigh = (TextBox)Row.FindControl("txtHigh");
        //            TextBox txtLow = (TextBox)Row.FindControl("txtLow");

        //            if (Convert.ToDecimal(txtHigh.Text) < Convert.ToDecimal(txtLow.Text))
        //            {
        //                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00069") + "');</script>", false);
        //                return;
        //            }


        //            DropDownList ddlFLocation = (DropDownList)Row.FindControl("ddlFLocation");
        //            DropDownList ddlFTerminal = (DropDownList)Row.FindControl("ddlFTerminal");
        //            DropDownList ddlFWashingType = (DropDownList)Row.FindControl("ddlFWashingType");

        //            //TextBox txtHigh = (TextBox)Row.FindControl("txtHigh");
        //            //TextBox txtLow = (TextBox)Row.FindControl("txtLow");
        //            TextBox txtRatePerBL = (TextBox)Row.FindControl("txtRatePerBL");
        //            TextBox txtRatePerTEU = (TextBox)Row.FindControl("txtRatePerTEU");
        //            TextBox txtRateperFEU = (TextBox)Row.FindControl("txtRateperFEU");
        //            TextBox txtSharingBL = (TextBox)Row.FindControl("txtSharingBL");
        //            TextBox txtSharingTEU = (TextBox)Row.FindControl("txtSharingTEU");
        //            TextBox txtSharingFEU = (TextBox)Row.FindControl("txtSharingFEU");
        //            HiddenField hdnFId = (HiddenField)Row.FindControl("hdnFId");
        //            HiddenField hdnId = (HiddenField)Row.FindControl("hdnId");

        //            oEntity = new ChargeRateEntity();
        //            oEntity.ChargesID = Convert.ToInt32(hdnChargeID.Value);
        //            oEntity.High = Convert.ToInt32(txtHigh.Text);
        //            oEntity.LocationId = Convert.ToInt32(ddlFLocation.SelectedValue);
        //            oEntity.Low = Convert.ToInt32(txtLow.Text);
        //            oEntity.RatePerBL = Convert.ToDecimal(txtRatePerBL.Text);
        //            oEntity.SharingBL = Convert.ToDecimal(txtSharingBL.Text);
        //            oEntity.SharingFEU = Convert.ToDecimal(txtSharingFEU.Text);
        //            oEntity.SharingTEU = Convert.ToDecimal(txtSharingTEU.Text);
        //            oEntity.WashingType = Convert.ToInt32(ddlFWashingType.SelectedValue);
        //            if (ddlFTerminal.Items.Count > 0 && ddlFTerminal.SelectedIndex > 0)
        //                oEntity.TerminalId = Convert.ToInt32(ddlFTerminal.SelectedValue);

        //            //Header change & CBM/TON selection to be implemented below
        //            oEntity.RateActive = true;
        //            oEntity.RatePerFEU = Convert.ToDecimal(txtRateperFEU.Text);
        //            oEntity.RatePerTEU = Convert.ToDecimal(txtRatePerTEU.Text);
        //            oEntity.RatePerCBM = Convert.ToDecimal(txtRateperFEU.Text);
        //            oEntity.RatePerTON = Convert.ToDecimal(txtRatePerTEU.Text);


        //            if (hdnFId.Value == "0") //Inser in local list
        //            {
        //                oChargeBLL.AddEditChargeRates(oEntity);
        //            }
        //            else //Update local list
        //            {
        //                oEntity.ChargesRateID = Convert.ToInt32(hdnFId.Value);
        //                oChargeBLL.AddEditChargeRates(oEntity);
        //            }
        //            FillChargeRate(Convert.ToInt32(hdnChargeID.Value));

        //        }
        //        else
        //        {
        //            ClientScript.RegisterClientScriptBlock(typeof(Page), "myscript", "alert('Please enter the charge detail first')", true);
        //        }

        //    }
        //    #endregion

        //    #region Edit
        //    if (e.CommandArgument == "Edit")
        //    {
        //        GridViewRow FootetRow = dgChargeRates.FooterRow;

        //        DropDownList ddlFLocation = (DropDownList)FootetRow.FindControl("ddlFLocation");
        //        DropDownList ddlFTerminal = (DropDownList)FootetRow.FindControl("ddlFTerminal");
        //        DropDownList ddlFWashingType = (DropDownList)FootetRow.FindControl("ddlFWashingType");
        //        TextBox txtLow = (TextBox)FootetRow.FindControl("txtLow");
        //        TextBox txtHigh = (TextBox)FootetRow.FindControl("txtHigh");
        //        TextBox txtRatePerBL = (TextBox)FootetRow.FindControl("txtRatePerBL");
        //        TextBox txtRatePerTEU = (TextBox)FootetRow.FindControl("txtRatePerTEU");
        //        TextBox txtRateperFEU = (TextBox)FootetRow.FindControl("txtRateperFEU");
        //        TextBox txtSharingBL = (TextBox)FootetRow.FindControl("txtSharingBL");
        //        TextBox txtSharingTEU = (TextBox)FootetRow.FindControl("txtSharingTEU");
        //        TextBox txtSharingFEU = (TextBox)FootetRow.FindControl("txtSharingFEU");
        //        HiddenField hdnFId = (HiddenField)FootetRow.FindControl("hdnFId");


        //        GridViewRow Row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
        //        HiddenField hdnLocationId = (HiddenField)Row.FindControl("hdnLocationId");
        //        HiddenField hdnTerminalId = (HiddenField)Row.FindControl("hdnTerminalId");
        //        HiddenField hdnWashingTypeId = (HiddenField)Row.FindControl("hdnWashingTypeId");
        //        HiddenField hdnId = (HiddenField)Row.FindControl("hdnId");

        //        Label lblLow = (Label)Row.FindControl("lblLow");
        //        Label lblHigh = (Label)Row.FindControl("lblHigh");
        //        Label lblRatePerBl = (Label)Row.FindControl("lblRatePerBl");
        //        Label lblRatePerTEU = (Label)Row.FindControl("lblRatePerTEU");
        //        Label lblRatePerFEU = (Label)Row.FindControl("lblRatePerFEU");
        //        Label lblSharingBL = (Label)Row.FindControl("lblSharingBL");
        //        Label lblSharingTEU = (Label)Row.FindControl("lblSharingTEU");
        //        Label lblSharingFEU = (Label)Row.FindControl("lblSharingFEU");

        //        ddlFLocation.SelectedIndex = ddlFLocation.Items.IndexOf(ddlFLocation.Items.FindByValue(hdnLocationId.Value));
        //        ddlFTerminal.SelectedIndex = ddlFTerminal.Items.IndexOf(ddlFTerminal.Items.FindByValue(hdnTerminalId.Value));
        //        ddlFWashingType.SelectedIndex = ddlFWashingType.Items.IndexOf(ddlFWashingType.Items.FindByValue(hdnWashingTypeId.Value));


        //        txtLow.Text = lblLow.Text;
        //        txtHigh.Text = lblHigh.Text;
        //        txtRatePerBL.Text = lblRatePerBl.Text;
        //        txtRatePerTEU.Text = lblRatePerTEU.Text;
        //        txtRateperFEU.Text = lblRatePerFEU.Text;
        //        txtSharingBL.Text = lblSharingBL.Text;
        //        txtSharingTEU.Text = lblSharingTEU.Text;
        //        txtSharingFEU.Text = lblSharingFEU.Text;
        //        hdnFId.Value = hdnId.Value.ToString();

        //    }
        //    #endregion

        //    #region Delete
        //    if (e.CommandArgument == "Delete")
        //    {
        //        GridViewRow Row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
        //        HiddenField hdnId = (HiddenField)Row.FindControl("hdnId");
        //        ChargeBLL.DeleteChargeRate(Convert.ToInt32(hdnId.Value));
        //        FillChargeRate(Convert.ToInt32(hdnChargeID.Value));
        //    }
        //    #endregion
        //}
        //protected void rdbPrincipleSharing_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    RadioButtonList rdl = (RadioButtonList)sender;
        //    SharingSelection(rdl);
        //}

        //private void SharingSelection(RadioButtonList rdl)
        //{
        //    GridViewRow Row = dgChargeRates.FooterRow;
        //    TextBox txtSharingBL = (TextBox)Row.FindControl("txtSharingBL");
        //    TextBox txtSharingTEU = (TextBox)Row.FindControl("txtSharingTEU");
        //    TextBox txtSharingFEU = (TextBox)Row.FindControl("txtSharingFEU");

        //    if (rdl.SelectedItem.Value == "0")
        //    {
        //        txtSharingBL.ReadOnly = true;
        //        txtSharingBL.Text = "0.0";

        //        txtSharingTEU.ReadOnly = true;
        //        txtSharingTEU.Text = "0.0";

        //        txtSharingFEU.ReadOnly = true;
        //        txtSharingFEU.Text = "0.0";
        //    }
        //    else
        //    {
        //        txtSharingBL.ReadOnly = false;
        //        txtSharingTEU.ReadOnly = false;
        //        txtSharingFEU.ReadOnly = false;
        //    }
        //}
        //protected void rdbWashing_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    RadioButtonList rdl = (RadioButtonList)sender;
        //    WashingSelection(rdl);
        //}

        //private void WashingSelection(RadioButtonList rdl)
        //{
        //    GridViewRow Row = dgChargeRates.FooterRow;
        //    DropDownList ddlFWashingType = (DropDownList)Row.FindControl("ddlFWashingType");

        //    if (rdl.SelectedItem.Value == "0")
        //    {
        //        ddlFWashingType.SelectedIndex = 0;
        //        ddlFWashingType.Enabled = false;
        //    }
        //    else
        //    {
        //        ddlFWashingType.Enabled = true;
        //    }
        //}
        //protected void rdbServiceTaxApplicable_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

    }
}