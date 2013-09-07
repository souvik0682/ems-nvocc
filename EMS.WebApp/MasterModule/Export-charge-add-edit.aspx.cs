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
    public partial class Export_charge_add_edit : System.Web.UI.Page
    {
        #region Variables
        List<ChargeRateEntity> oChargeRates;
        ChargeBLL oChargeBLL;
        ChargeRateEntity oEntity;
        ChargeEntity oChargeEntity;
        private int _locId = 0;
        private int _userId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        List<IChargeRate> Rates = new List<IChargeRate>();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //IUser user = (IUser)Session[Constants.SESSION_USER_INFO];
            //_userId = user == null ? 0 : user.Id;

            RetriveParameters();
            if (!Page.IsPostBack)
            {
                fillAllDropdown();
                //dgChargeRates.DataBind();

                if (hdnChargeID.Value != "0")
                {
                    FillChargeDetails(Convert.ToInt32(hdnChargeID.Value));
                    FillChargeRate(Convert.ToInt32(hdnChargeID.Value));
                    ActionOnLocationChange(ddlHeaderLocation as object);
                    //WashingSelection(rdbWashing);
                    ShowHideControlofFooter(ddlChargeType);
                    DisableAllField();
                }
                else
                {
                    DefaultSelection();

                    //FillDocType();
                    oChargeRates = new List<ChargeRateEntity>();
                    ChargeRateEntity Ent = new ChargeRateEntity();
                    oChargeRates.Add(Ent);
                    dgChargeRates.DataSource = oChargeRates;
                    dgChargeRates.DataBind();
                    dgChargeRates.Rows[0].Visible = false;

                    //WashingSelection(rdbWashing);
                }

            }
            CheckUserAccess(hdnChargeID.Value);
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
                //IUser user = (IUser)Session[Constants.SESSION_USER_INFO];
                oChargeEntity.CompanyID = 1;
                oChargeEntity.Currency = Convert.ToInt32(ddlCurrency.SelectedValue);
                oChargeEntity.EffectDt = Convert.ToDateTime(txtEffectDate.Text.Trim());
                oChargeEntity.IEC = 'E';
                oChargeEntity.IsTerminal = Convert.ToBoolean(Convert.ToInt32(rdbTerminalRequired.SelectedItem.Value));
                //oChargeEntity.IsWashing = Convert.ToBoolean(Convert.ToInt32(rdbWashing.SelectedItem.Value));
                oChargeEntity.NVOCCID = Convert.ToInt32(ddlLine.SelectedValue);
                oChargeEntity.RateChangeable = Convert.ToBoolean(Convert.ToInt32(rdbRateChange.SelectedItem.Value));
                oChargeEntity.ServiceTax = Convert.ToBoolean(Convert.ToInt32(rdbServiceTaxApplicable.SelectedItem.Value));
                oChargeEntity.Location = Convert.ToInt32(ddlHeaderLocation.SelectedValue);

                if (ddlService.SelectedIndex > 0)
                    oChargeEntity.Service = Convert.ToInt32(ddlService.SelectedValue);

                oChargeEntity.DestinationCharge = Convert.ToBoolean(rdbDestinationCharge.SelectedValue == "1" ? true : false);
                oChargeEntity.FPOD = hdnFPOD.Value;

                if (ddlInvLink.Items != null)
                    oChargeEntity.DocumentType = Convert.ToInt32(ddlInvLink.SelectedValue);

                oChargeEntity.DeliveryMode = '0';

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
                        //WashingSelection(rdbWashing);
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
                            Response.Redirect("~/MasterModule/Export-charge-list.aspx");
                            break;
                        case -1: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00076");
                            break;
                    }
                }

            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MasterModule/Export-charge-list.aspx");
        }

        protected void dgChargeRates_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                ListItem Li = new ListItem("Select", "0");
                DropDownList ddlType = (DropDownList)e.Row.FindControl("ddlType");
                PopulateDropDown((int)Enums.DropDownPopulationFor.ContainerType, ddlType, 0, 0);
                ddlType.Items.Insert(0, Li);
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnRemove = (ImageButton)e.Row.FindControl("lnkDelete");
                btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00012");

                btnRemove.OnClientClick = "javascript:return confirm('" + ResourceManager.GetStringWithoutName("ERR00014") + "');";
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


                DropDownList ddlFTerminal = (DropDownList)Row.FindControl("ddlFTerminal");
                DropDownList ddlType = (DropDownList)Row.FindControl("ddlType");
                DropDownList ddlSize = (DropDownList)Row.FindControl("ddlSize");


                TextBox txtRateperUnit = (TextBox)Row.FindControl("txtRateperUnit");
                TextBox txtRatePerDoc = (TextBox)Row.FindControl("txtRatePerDoc");
                TextBox txtRatePerCBM = (TextBox)Row.FindControl("txtRatePerCBM");
                TextBox txtRatePerTON = (TextBox)Row.FindControl("txtRatePerTON");


                HiddenField hdnFId = (HiddenField)Row.FindControl("hdnFId");
                HiddenField hdnId = (HiddenField)Row.FindControl("hdnId");
                HiddenField hdnFSlno = (HiddenField)Row.FindControl("hdnFSlno");


                oEntity = new ChargeRateEntity();
                oEntity.ChargesID = Convert.ToInt32(hdnChargeID.Value);

                if (ddlFTerminal.Items.Count > 0 && ddlFTerminal.SelectedIndex >= 0)
                    oEntity.TerminalId = Convert.ToInt32(ddlFTerminal.SelectedValue);

                if (ddlType.Items.Count > 0 && ddlType.SelectedIndex >= 0)
                    oEntity.Type = Convert.ToInt32(ddlType.SelectedValue);

                if (ddlSize.Items.Count > 0 && ddlSize.SelectedIndex >= 0)
                    oEntity.Size = ddlSize.SelectedValue;

                oEntity.RateActive = true;

                oEntity.RatePerUnit = Convert.ToDecimal(string.IsNullOrEmpty(txtRateperUnit.Text) == false ? txtRateperUnit.Text : "0.00");
                oEntity.RatePerDoc = Convert.ToDecimal(string.IsNullOrEmpty(txtRatePerDoc.Text) == false ? txtRatePerDoc.Text : "0.00");
                oEntity.RatePerCBM = Convert.ToDecimal(string.IsNullOrEmpty(txtRatePerCBM.Text) == false ? txtRatePerCBM.Text : "0.00");
                oEntity.RatePerTON = Convert.ToDecimal(string.IsNullOrEmpty(txtRatePerTON.Text) == false ? txtRatePerTON.Text : "0.00");



                if (Convert.ToInt32(hdnFSlno.Value) >= 0)
                {
                    oEntity.ChargesRateID = Convert.ToInt32(hdnFId.Value);
                    Rates.RemoveAt(Convert.ToInt32(hdnFSlno.Value));
                    Rates.Insert(Convert.ToInt32(hdnFSlno.Value), oEntity);
                    lblMessage.Text = string.Empty;

                    //IEnumerable<IChargeRate> cr = RangeValidationCheck(Convert.ToInt32(ddlHeaderLocation.SelectedValue), Convert.ToInt32(ddlFTerminal.SelectedValue), Convert.ToInt32(hdnFId.Value));
                    //if (cr.Count() <= 0)
                    //{
                    //    Rates.RemoveAt(Convert.ToInt32(hdnFSlno.Value));
                    //    Rates.Insert(Convert.ToInt32(hdnFSlno.Value), oEntity);
                    //    lblMessage.Text = string.Empty;
                    //}
                    //else
                    //{
                    //    lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00079");
                    //    return;
                    //}
                }
                else
                {
                    Rates.Add(oEntity);
                }


                ViewState["ChargeRates"] = Rates;
                FillRates();
                DisableAllField();
                ShowHideControlofFooter(ddlChargeType);
                
            }

            #endregion

            #region Edit
            if (e.CommandArgument == "Edit")
            {
                GridViewRow HeaderRow = dgChargeRates.HeaderRow;

                DropDownList ddlType = (DropDownList)HeaderRow.FindControl("ddlType");

                
                DropDownList ddlFTerminal = (DropDownList)HeaderRow.FindControl("ddlFTerminal");
                DropDownList ddlSize = (DropDownList)HeaderRow.FindControl("ddlSize");
                TextBox txtRateperUnit = (TextBox)HeaderRow.FindControl("txtRateperUnit");
                TextBox txtRatePerDoc = (TextBox)HeaderRow.FindControl("txtRatePerDoc");
                TextBox txtRatePerCBM = (TextBox)HeaderRow.FindControl("txtRatePerCBM");
                TextBox txtRatePerTON = (TextBox)HeaderRow.FindControl("txtRatePerTON");
                HiddenField hdnFId = (HiddenField)HeaderRow.FindControl("hdnFId");
                HiddenField hdnFSlno = (HiddenField)HeaderRow.FindControl("hdnFSlno");


                GridViewRow ItemRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                HiddenField hdnTerminalId = (HiddenField)ItemRow.FindControl("hdnTerminalId");
                HiddenField hdnTypeId = (HiddenField)ItemRow.FindControl("hdnTypeId");
                HiddenField hdnId = (HiddenField)ItemRow.FindControl("hdnId");
                

                Label lblSize = (Label)ItemRow.FindControl("lblSize");
                Label lblRatePerUnit = (Label)ItemRow.FindControl("lblRatePerUnit");
                Label lblRatePerDoc = (Label)ItemRow.FindControl("lblRatePerDoc");
                Label lblRatePerCBM = (Label)ItemRow.FindControl("lblRatePerCBM");
                Label lblRatePerTON = (Label)ItemRow.FindControl("lblRatePerTON");

                //ddlType.SelectedIndex = ddlType.Items.IndexOf(ddlType.Items.FindByValue(hdnTypeId.Value));
                ddlType.SelectedValue = hdnTypeId.Value;
                
                ddlFTerminal.SelectedIndex = ddlFTerminal.Items.IndexOf(ddlFTerminal.Items.FindByValue(hdnTerminalId.Value));
                ddlSize.SelectedIndex = ddlSize.Items.IndexOf(ddlSize.Items.FindByValue(lblSize.Text));

                txtRateperUnit.Text = lblRatePerUnit.Text;
                txtRatePerTON.Text = lblRatePerTON.Text;
                txtRatePerDoc.Text = lblRatePerDoc.Text;
                txtRatePerCBM.Text = lblRatePerCBM.Text;

                hdnFId.Value = hdnId.Value;
                hdnFSlno.Value = ItemRow.RowIndex.ToString();
                //UpdatePanel1.Update();

            }
            #endregion

            #region Delete
            if (e.CommandArgument == "Delete")
            {
                GridViewRow Row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                Rates = (List<IChargeRate>)ViewState["ChargeRates"];
                Rates.RemoveAt(Row.RowIndex);
                ViewState["ChargeRates"] = Rates;
                FillRates();
                ShowHideControlofFooter(ddlChargeType);
            }
            #endregion

            #region Cancel
            if (e.CommandArgument == "Cancel")
            {
                GridViewRow Row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);


                DropDownList ddlType = (DropDownList)Row.FindControl("ddlType");
                DropDownList ddlFTerminal = (DropDownList)Row.FindControl("ddlFTerminal");
                DropDownList ddlSize = (DropDownList)Row.FindControl("ddlSize");

                TextBox txtRateperUnit = (TextBox)Row.FindControl("txtRateperUnit");
                TextBox txtRatePerDoc = (TextBox)Row.FindControl("txtRatePerDoc");
                TextBox txtRatePerCBM = (TextBox)Row.FindControl("txtRatePerCBM");
                TextBox txtRatePerTON = (TextBox)Row.FindControl("txtRatePerTON");

                HiddenField hdnFId = (HiddenField)Row.FindControl("hdnFId");
                HiddenField hdnFSlno = (HiddenField)Row.FindControl("hdnFSlno");

                ddlType.SelectedIndex = 0;
                ddlFTerminal.SelectedIndex = 0;
                //if (ddlFTerminal.Items.Count > 0)
                //{
                //    ddlFTerminal.Items.Clear();
                //}


                txtRateperUnit.Text = String.Empty;// "0.00";
                txtRatePerDoc.Text = String.Empty;//"0.00";
                txtRatePerCBM.Text = String.Empty;//"0.00";
                txtRatePerTON.Text = String.Empty;//"0.00";               

                hdnFId.Value = "0";
                hdnFSlno.Value = "-1";
                ShowHideControlofFooter(ddlChargeType);
            }
            #endregion

            
            //WashingSelection(rdbWashing);
            ////ShowHideControlofFooter(ddlChargeType);
            ////DisableAllField();

        }

        protected void rdbTerminalRequired_SelectedIndexChanged(object sender, EventArgs e)
        {
            //RadioButtonList rdl = (RadioButtonList)sender;
            //TerminalSelection(rdl);

            ////TerminalSelection
            ActionOnLocationChange(ddlHeaderLocation as object);
        }


        private void ShowHideControlofFooter(DropDownList ddlChargeType)
        {
            //DropDownList ddlChargeType = (DropDownList)sender;
            GridViewRow FootetRow = dgChargeRates.HeaderRow;

            DropDownList ddlFTerminal = (DropDownList)FootetRow.FindControl("ddlFTerminal");
            DropDownList ddlType = (DropDownList)FootetRow.FindControl("ddlType");
            DropDownList ddlSize = (DropDownList)FootetRow.FindControl("ddlSize");

            TextBox txtRateperUnit = (TextBox)FootetRow.FindControl("txtRateperUnit");
            TextBox txtRatePerDoc = (TextBox)FootetRow.FindControl("txtRatePerDoc");
            TextBox txtRatePerCBM = (TextBox)FootetRow.FindControl("txtRatePerCBM");
            TextBox txtRatePerTON = (TextBox)FootetRow.FindControl("txtRatePerTON");

            RequiredFieldValidator rfvRatePerUnit = (RequiredFieldValidator)FootetRow.FindControl("rfvRatePerUnit");
            RequiredFieldValidator rfvRatePerDoc = (RequiredFieldValidator)FootetRow.FindControl("rfvRatePerDoc");
            RequiredFieldValidator rfvRatePerCBM = (RequiredFieldValidator)FootetRow.FindControl("rfvRatePerCBM");
            RequiredFieldValidator rfvRatePerTON = (RequiredFieldValidator)FootetRow.FindControl("rfvRatePerTON");
            //rfvType
            RequiredFieldValidator rfvType = (RequiredFieldValidator)FootetRow.FindControl("rfvType");
            //rfvSize
            RequiredFieldValidator rfvSize = (RequiredFieldValidator)FootetRow.FindControl("rfvSize");

            //////ddlType.Enabled = false;
            rfvType.Enabled = false;
            ddlType.Enabled = false;
            ddlType.SelectedIndex = 0;

            ddlSize.Enabled = false;
            rfvSize.Enabled = false;
            ddlSize.SelectedIndex = 0;

            txtRateperUnit.Enabled = false;
            rfvRatePerUnit.Enabled = false;
            //txtRateperUnit.Text = "0.00";

            txtRatePerDoc.Enabled = false;
            rfvRatePerDoc.Enabled = false;
            //txtRatePerDoc.Text = "0.00";

            txtRatePerCBM.Enabled = false;
            rfvRatePerCBM.Enabled = false;
            //txtRatePerCBM.Text = "0.00";

            txtRatePerTON.Enabled = false;
            rfvRatePerTON.Enabled = false;
            //txtRatePerTON.Text = "0.00";

            switch (ddlChargeType.SelectedValue)
            {
                case "54": // Per Unit TYPE & SIZE
                    ddlType.Enabled = true;
                    rfvType.Enabled = true;

                    ddlSize.Enabled = true;
                    rfvSize.Enabled = true;

                    txtRateperUnit.Enabled = true;
                    rfvRatePerUnit.Enabled = true;
                    break;

                case "51": // Per Document
                    txtRatePerDoc.Enabled = true;
                    rfvRatePerDoc.Enabled = true;
                    break;

                case "52": // Per CBM
                    txtRatePerCBM.Enabled = true;
                    rfvRatePerCBM.Enabled = true;
                    break;

                case "53": // Per TON
                    txtRatePerTON.Enabled = true;
                    rfvRatePerTON.Enabled = true;
                    break;

                case "50": // Per Unit 
                    ddlType.Enabled = false;
                    rfvType.Enabled = false;

                    ddlSize.Enabled = true;
                    rfvSize.Enabled = true;

                    txtRateperUnit.Enabled = true;
                    rfvRatePerUnit.Enabled = true;
                    break;


            }
        }

        void DefaultSelection()
        {
            rdbRateChange.SelectedIndex = 1;
            rdbServiceTaxApplicable.SelectedIndex = 1;
            rdbDestinationCharge.SelectedIndex = 1;
            rdbTerminalRequired.SelectedIndex = 1;
            ddlLine.SelectedIndex = -1;
            ddlService.Items.Clear();
            rfvFPOD.Enabled = false;
            txtFPOD.Text = string.Empty;
            hdnFPOD.Value = "0";
            ddlInvLink.SelectedIndex = 0;
            hdnChargeID.Value = "0";

        }
        void fillAllDropdown()
        {
            ListItem Li = null;

            #region Location
            //
            PopulateDropDown((int)Enums.DropDownPopulationFor.Location, ddlHeaderLocation, 0, 0);
            Li = new ListItem("SELECT", "0");
            ddlHeaderLocation.Items.Insert(0, Li);
            Li = new ListItem("ALL", "-1");
            ddlHeaderLocation.Items.Insert(1, Li);
            #endregion

            #region ChargeType
            foreach (Enums.ChargeType r in Enum.GetValues(typeof(Enums.ExportChargeType)))
            {
                Li = new ListItem("SELECT", "0");
                ListItem item = new ListItem(Enum.GetName(typeof(Enums.ExportChargeType), r).Replace('_', ' '), ((int)r).ToString());
                ddlChargeType.Items.Add(item);
            }
            ddlChargeType.Items.Insert(0, Li);
            #endregion

            #region Currency

            PopulateDropDown((int)Enums.DropDownPopulationFor.ExpCurrency, ddlCurrency, 0, 0);

            //foreach (Enums.Currency r in Enum.GetValues(typeof(Enums.Currency)))
            //{
            //    //Li = new ListItem("SELECT", "0");
            //    ListItem item = new ListItem(Enum.GetName(typeof(Enums.Currency), r).Replace('_', ' '), ((int)r).ToString());
            //    ddlCurrency.Items.Add(item);
            //}
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

            PopulateDropDown((int)Enums.DropDownPopulationFor.Line, ddlLine, 0, 0);
            Li = new ListItem("NA", "0");
            ddlLine.Items.Insert(0, Li);
            #endregion

            #region Service

            //PopulateDropDown((int)Enums.DropDownPopulationFor.Service, ddlService, Convert.ToInt32(ddlLine.SelectedValue));
            //Li = new ListItem("SELECT", "0");
            //ddlService.Items.Insert(0, Li);

            #endregion

            #region Master list of Location, Terminal, WashinfType

            Li = new ListItem("ALL", "-1");
            PopulateDropDown((int)Enums.DropDownPopulationFor.Location, ddlMLocation, 0, 0);
            ddlMLocation.Items.Insert(0, Li);

            Li = new ListItem("ALL", "-1");
            PopulateDropDown((int)Enums.DropDownPopulationFor.TerminalCode, ddlMTerminal, 0, 0);
            ddlMTerminal.Items.Insert(0, Li);
            //PopulateDropDown((int)Enums.DropDownPopulationFor.Wa, ddlMWashingType, 0);


            Li = new ListItem("Select", "0");
            PopulateDropDown((int)Enums.DropDownPopulationFor.ContainerType, ddlMType, 0, 0);
            ddlMType.Items.Insert(0, Li);

            #endregion

        }

        void FillChargeDetails(int ChargesID)
        {
            oChargeEntity = new ChargeEntity();
            oChargeBLL = new ChargeBLL();
            oChargeEntity = (ChargeEntity)oChargeBLL.GetChargeDetails(ChargesID);


            //oChargeEntity.ChargeActive = true;
            txtChargeName.Text = oChargeEntity.ChargeDescr;
            ddlChargeType.SelectedIndex = ddlChargeType.Items.IndexOf(ddlChargeType.Items.FindByValue(oChargeEntity.ChargeType.ToString()));


            ddlCurrency.SelectedIndex = ddlCurrency.Items.IndexOf(ddlCurrency.Items.FindByValue(oChargeEntity.Currency.ToString()));
            txtEffectDate.Text = oChargeEntity.EffectDt.ToShortDateString();
            ddlInvLink.SelectedIndex = ddlInvLink.Items.IndexOf(ddlInvLink.Items.FindByValue(oChargeEntity.DocumentType.ToString()));
            ddlLine.SelectedIndex = ddlLine.Items.IndexOf(ddlLine.Items.FindByValue(oChargeEntity.NVOCCID.ToString()));
            ddlHeaderLocation.SelectedIndex = ddlHeaderLocation.Items.IndexOf(ddlHeaderLocation.Items.FindByValue(oChargeEntity.Location.ToString()));



            rdbServiceTaxApplicable.Items.FindByValue(oChargeEntity.ServiceTax.ToString().ToLower() == "true" ? "1" : "0").Selected = true;
            rdbTerminalRequired.Items.FindByValue(oChargeEntity.IsTerminal.ToString().ToLower() == "true" ? "1" : "0").Selected = true;
            rdbRateChange.Items.FindByValue(oChargeEntity.RateChangeable.ToString().ToLower() == "true" ? "1" : "0").Selected = true;
            rdbDestinationCharge.Items.FindByValue(oChargeEntity.DestinationCharge.ToString().ToLower() == "true" ? "1" : "0").Selected = true;

            //hdnFPOD.Value = oChargeEntity.FPOD;
            if (oChargeEntity.FPOD != string.Empty)
            {
                hdnFPOD.Value = oChargeEntity.FPOD.Substring(oChargeEntity.FPOD.IndexOf('|') + 1);
                txtFPOD.Text = oChargeEntity.FPOD.Substring(0, oChargeEntity.FPOD.IndexOf('|'));
            }


            FillServices();
            if (ddlService.Items.Count > 0)
            {
                ddlService.SelectedIndex = ddlService.Items.IndexOf(ddlService.Items.FindByValue(oChargeEntity.Service.ToString()));
            }

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
        void DisableAllField()
        {
            txtChargeName.Enabled = false;
            txtEffectDate.Enabled = false;
            txtFPOD.Enabled = false;
            rfvFPOD.Enabled = false;
            ddlChargeType.Enabled = false;
            ddlCurrency.Enabled = false;
            ddlLine.Enabled = false;
            ddlHeaderLocation.Enabled = false;
            ddlService.Enabled = false;

            rdbRateChange.Enabled = false;
            rdbServiceTaxApplicable.Enabled = false;
            rdbTerminalRequired.Enabled = false;
            rdbDestinationCharge.Enabled = false;
            
            ddlInvLink.Enabled = false;
            
        }
        void EnableAllField()
        {
            txtChargeName.Enabled = true;
            txtEffectDate.Enabled = true;
            txtFPOD.Enabled = true;
            rfvFPOD.Enabled = true;
            ddlChargeType.Enabled = true;
            ddlCurrency.Enabled = true;
            ddlLine.Enabled = true;
            ddlHeaderLocation.Enabled = true;
            
            rdbRateChange.Enabled = true;
            rdbServiceTaxApplicable.Enabled = true;
            rdbTerminalRequired.Enabled = true;
            rdbDestinationCharge.Enabled = true;
            //rdbWashing.Enabled = true;
            ddlInvLink.Enabled = true;
            ddlService.Enabled = true;
        }
        void ClearAll()
        {
            hdnChargeID.Value = "0";
            ddlChargeType.SelectedIndex = 0;
            ddlCurrency.SelectedIndex = 0;
            ddlLine.SelectedIndex = 0;
            ddlMLocation.SelectedIndex = 0;
            ddlMTerminal.SelectedIndex = 0;
            ddlHeaderLocation.SelectedIndex = 0;

            txtChargeName.Text = string.Empty;
            txtEffectDate.Text = string.Empty;

            DefaultSelection();

            oChargeRates = new List<ChargeRateEntity>();
            ChargeRateEntity Ent = new ChargeRateEntity();
            oChargeRates.Add(Ent);
            dgChargeRates.DataSource = oChargeRates;
            dgChargeRates.DataBind();
            dgChargeRates.Rows[0].Visible = false;

        }

        void PopulateDropDown(int Number, DropDownList ddl, int? Filter, int? Filter2)
        {
            CommonBLL.PopulateDropdown(Number, ddl, Filter, Filter2);
        }
        void CheckUserAccess(string xID)
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

                    if (!_canEdit && xID != "0")
                    {
                        btnSave.Visible = false;
                    }
                    else if (!_canAdd && xID == "0")
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
        void RetriveParameters()
        {
            //_userId = UserBLL.GetLoggedInUserId();

            if (!ReferenceEquals(Request.QueryString["id"], null))
            {
                Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _locId);
                hdnChargeID.Value = _locId.ToString();
            }
            _userId = EMS.BLL.UserBLL.GetLoggedInUserId();

            EMS.BLL.UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
        }
        void ActionOnLocationChange(object sender)
        {
            DropDownList ddlFLocation = (DropDownList)sender;
            GridViewRow item = dgChargeRates.HeaderRow;
            DropDownList ddlFTerminal = (DropDownList)item.FindControl("ddlFTerminal");

            if (rdbTerminalRequired.SelectedValue == "1")
            {
                ddlFTerminal.Enabled = true;
                PopulateDropDown((int)Enums.DropDownPopulationFor.TerminalCode, ddlFTerminal, Convert.ToInt32(ddlFLocation.SelectedValue), 0);

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
            else
            {
                if (ddlFTerminal.Items.Count > 0)
                {
                    ddlFTerminal.Items.Clear();
                }
                ddlFTerminal.Enabled = false;
            }
        }
        IEnumerable<IChargeRate> RangeValidationCheck(int Locn, int Ter, int ChargesRateID)
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
                        if (ChargesRateID > 0)  //For Edit
                        {
                            if (rt.TerminalId == Ter && rt.ChargesRateID != ChargesRateID)
                            {
                                isValidRange = false;
                                break;
                            }
                        }
                        else //For new addition
                        {
                            //if (i >= Convert.ToInt32(txtLow.Text) && i <= Convert.ToInt32(txtHigh.Text) && rt.TerminalId == Ter)
                            //{
                            //    isValidRange = false;
                            //    break;
                            //}
                        }




                    }

                }
            }

            //IEnumerable<IChargeRate> cr = from rate in Rates
            //                              where rate.LocationId == Locn && rate.TerminalId == Ter && rate.WashingType == Was && isValidRange == false
            //                              select rate;

            IEnumerable<IChargeRate> cr = null;
            if (ChargesRateID > 0) //Incase of Edit
            {
                cr = from rate in Rates
                     where rate.TerminalId == Ter && isValidRange == false && rate.ChargesRateID != ChargesRateID
                     select rate;
            }
            else //Incase of New addition
            {
                cr = from rate in Rates
                     where rate.TerminalId == Ter && isValidRange == false
                     select rate;
            }
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


        protected void ddlImportExportGeneral_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FillDocType();
        }
        protected void ddlHeaderLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActionOnLocationChange(sender);
        }
        protected void ddlLocationID_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActionOnLocationChange(sender);
        }
        protected void ddlChargeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ShowHideControlofFooter(sender);
            DropDownList ddlChargeType = (DropDownList)sender;
            ShowHideControlofFooter(ddlChargeType);
        }

        protected void rdbDestinationCharge_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdbDestinationCharge.SelectedValue == "1")
            {
                txtFPOD.Enabled = true;
                rfvFPOD.Enabled = true;
            }
            else
            {
                txtFPOD.Enabled = false;
                rfvFPOD.Enabled = false;
            }
        }

        protected void ddlHeaderLocation_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void ddlLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillServices();
        }

        private void FillServices()
        {
            if (ddlLine.SelectedIndex > 0) // && Convert.ToInt32(hdnFPOD.Value) > 0)
            {
                ListItem Li;
                PopulateDropDown((int)Enums.DropDownPopulationFor.Service, ddlService, Convert.ToInt32(ddlLine.SelectedValue), 0);

                Li = new ListItem("ALL", "0");
                ddlService.Items.Insert(0, Li);
            }
            else
            {
                ddlService.Items.Clear();
            }
        }

        protected void txtFPOD_TextChanged(object sender, EventArgs e)
        {
            FillServices();
        }

        protected void dgChargeRates_DataBound(object sender, EventArgs e)
        {
            //ShowHideControlofFooter(ddlChargeType);
            ActionOnLocationChange(ddlHeaderLocation as object);
        }

       


    }
}