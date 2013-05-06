using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EMS.Common;
using EMS.BLL;
using EMS.Utilities;
using System.IO;
using EMS.Entity;

namespace EMS.WebApp.Transaction
{
    public partial class ManageImportBL : System.Web.UI.Page
    {
        const string VESSELID = "VESSELID";
        const string ISSUEPORTID = "ISSUEPORTID";
        const string PORTOFLOADINGID = "PORTOFLOADINGID";
        const string PORTOFDISCHARGEID = "PORTOFDISCHARGEID";
        const string FINALDESTINATIONID = "FINALDESTINATIONID";
        const string DELIVERYTOID = "DELIVERYTOID";
        const string PACKAGEUNITID = "PACKAGEUNITID";
        const string WEIGHTUNITID = "WEIGHTUNITID";
        const string VOLUMEUNITID = "VOLUMEUNITID";
        //const string SURVEYORID = "SURVEYORID";
        const string SHIPERADDRID = "SHIPERADDRID";
        const string CONSIGNEEEADDRID = "CONSIGNEEEADDRID";
        const string NPADDRID = "NPADDRID";
        const string CANADDRID = "CANADDRID";
        const string CFSADDRID = "CFSADDRID";
        const string FRTPAYBLEATID = "FRTPAYBLEATID";
        const string BLHEADERID = "BLHEADERID";
        const string BLFOOTERID = "BLFOOTERID";
        const string EDITBLFOOTERID = "EDITBLFOOTERID";
        const string FOOTERDETAILS = "FOOTERDETAILS";
        const string EDITBLFOOTER = "EDITBLFOOTER";

        private List<IBLFooter> footerDetails = new List<IBLFooter>();
        private int _userId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();

            if (!IsPostBack)
            {
                InitialActivities();

                if (!ReferenceEquals(Request.QueryString["BLId"], null))
                {
                    long blHeaderId = 0;
                    Int64.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["BLId"].ToString()), out blHeaderId);

                    if (blHeaderId > 0)
                        LoadForEdit(blHeaderId);
                }

                //SetDefaultFreightPayableAt();
                SetDefaultUnit();
            }
            AC_Vessel1.TextChanged += new EventHandler(AC_Vessel1_TextChanged);
            AC_DeliveryTo1.TextChanged += new EventHandler(AC_DeliveryTo1_TextChanged);
            AC_PackageUnit1.TextChanged += new EventHandler(AC_PackageUnit1_TextChanged);
            AC_Port1.TextChanged += new EventHandler(AC_Port1_TextChanged);
            AC_Port2.TextChanged += new EventHandler(AC_Port2_TextChanged);
            AC_Port3.TextChanged += new EventHandler(AC_Port3_TextChanged);
            AC_Port4.TextChanged += new EventHandler(AC_Port4_TextChanged);
            //AC_Surveyor1.TextChanged += new EventHandler(AC_Surveyor1_TextChanged);
            AC_VolumeUnit1.TextChanged += new EventHandler(AC_VolumeUnit1_TextChanged);
            AC_WeightUnit1.TextChanged += new EventHandler(AC_WeightUnit1_TextChanged);
            AC_Shipper1.TextChanged += new EventHandler(AC_Shipper1_TextChanged);
            AC_Consignee1.TextChanged += new EventHandler(AC_Consignee1_TextChanged);
            AC_NParty1.TextChanged += new EventHandler(AC_NParty1_TextChanged);
            //AC_CANotice1.TextChanged += new EventHandler(AC_CANotice1_TextChanged);
            AC_CFSCode1.TextChanged += new EventHandler(AC_CFSCode1_TextChanged);
            AC_Port5.TextChanged += new EventHandler(AC_Port5_TextChanged);
            AC_CHA1.TextChanged += new EventHandler(AC_CHA1_TextChanged);
        }

        void AC_CHA1_TextChanged(object sender, EventArgs e)
        {
            string chaName = ((TextBox)AC_CHA1.FindControl("txtCha")).Text.Trim();

            if (chaName != string.Empty)
            {
                string chaId = new ImportBLL().GetCHAId(chaName);

                if (!ReferenceEquals(chaId, null))
                    ViewState["CHAID"] = chaId;
                else
                    ViewState["CHAID"] = null;
            }
            else
            {
                ViewState["CHAID"] = null;
            }
        }

        void AC_CFSCode1_TextChanged(object sender, EventArgs e)
        {
            string cfsName = ((TextBox)AC_CFSCode1.FindControl("txtCFSCode")).Text.Trim();

            if (cfsName != string.Empty)
            {
                DataTable dt = new ImportBLL().GetCFSCode(cfsName);

                if (dt != null && dt.Rows.Count > 0)
                {
                    txtCFSName.Text = Convert.ToString(dt.Rows[0]["CFSCode"]);
                    ViewState[CFSADDRID] = Convert.ToString(dt.Rows[0]["fk_AddressID"]);
                }
                else
                {
                    ViewState[CFSADDRID] = null;
                }
            }
            else
            {
                ViewState[CFSADDRID] = null;
                txtCFSName.Text = string.Empty;
            }
        }

        //void AC_CANotice1_TextChanged(object sender, EventArgs e)
        //{
        //    string addrName = ((TextBox)AC_CANotice1.FindControl("txtCANotice")).Text.Trim();
        //    int addrId = new ImportBLL().GetAddressId(addrName);
        //    ViewState[CANADDRID] = addrId;
        //}

        void AC_NParty1_TextChanged(object sender, EventArgs e)
        {
            string addrName = ((TextBox)AC_NParty1.FindControl("txtNParty")).Text.Trim();

            if (addrName != string.Empty)
            {
                int addrId = new ImportBLL().GetAddressId(addrName);
                ViewState[NPADDRID] = addrId;

                txtNotifyingPartyAddr.Text = new ImportBLL().GetAddress(addrName);
            }
            else
            {
                ViewState[NPADDRID] = null;
                txtNotifyingPartyAddr.Text = string.Empty;
            }
        }

        void AC_Consignee1_TextChanged(object sender, EventArgs e)
        {
            string addrName = ((TextBox)AC_Consignee1.FindControl("txtConsignee")).Text.Trim();

            if (addrName != string.Empty)
            {
                int addrId = new ImportBLL().GetAddressId(addrName);
                ViewState[CONSIGNEEEADDRID] = addrId;

                txtConsigneeAddr.Text = new ImportBLL().GetAddress(addrName);
            }
            else
            {
                ViewState[CONSIGNEEEADDRID] = null;
                txtConsigneeAddr.Text = string.Empty;
            }
        }

        void AC_Shipper1_TextChanged(object sender, EventArgs e)
        {
            string addrName = ((TextBox)AC_Shipper1.FindControl("txtShipper")).Text.Trim();

            if (addrName != string.Empty)
            {
                int addrId = new ImportBLL().GetAddressId(addrName);
                ViewState[SHIPERADDRID] = addrId;

                txtShipperAddr.Text = new ImportBLL().GetAddress(addrName);
            }
            else
            {
                ViewState[SHIPERADDRID] = null;
                txtShipperAddr.Text = string.Empty;
            }
        }

        void AC_WeightUnit1_TextChanged(object sender, EventArgs e)
        {
            string unitName = ((TextBox)AC_WeightUnit1.FindControl("txtWeightUnit")).Text.Trim();

            if (unitName != string.Empty)
            {
                int unitId = new ImportBLL().GetUnitId(unitName);
                ViewState[WEIGHTUNITID] = unitId;
            }
            else
            {
                ViewState[WEIGHTUNITID] = null;
            }
        }

        void AC_PackageUnit1_TextChanged(object sender, EventArgs e)
        {
            string unitName = ((TextBox)AC_PackageUnit1.FindControl("txtPkgUnit")).Text.Trim();

            if (unitName != string.Empty)
            {
                int unitId = new ImportBLL().GetUnitId(unitName);
                ViewState[PACKAGEUNITID] = unitId;
            }
            else
            {
                ViewState[PACKAGEUNITID] = null;
            }
        }

        void AC_VolumeUnit1_TextChanged(object sender, EventArgs e)
        {
            string unitName = ((TextBox)AC_VolumeUnit1.FindControl("txtVolUnit")).Text.Trim();

            if (unitName != string.Empty)
            {
                int unitId = new ImportBLL().GetUnitId(unitName);
                ViewState[VOLUMEUNITID] = unitId;
            }
            else
            {
                ViewState[VOLUMEUNITID] = null;
            }
        }

        //void AC_Surveyor1_TextChanged(object sender, EventArgs e)
        //{
        //    string surveyor = ((TextBox)AC_Surveyor1.FindControl("txtSurveyor")).Text.Trim();

        //    if (surveyor != string.Empty)
        //    {
        //        int surveyorId = new ImportBLL().GetDeliveryToId(surveyor);
        //        ViewState[SURVEYORID] = surveyorId;
        //    }
        //    else
        //    {
        //        ViewState[SURVEYORID] = null;
        //    }
        //}

        //Frieght Payable At
        void AC_Port5_TextChanged(object sender, EventArgs e)
        {
            string frtPayable = ((TextBox)AC_Port5.FindControl("txtPort")).Text;

            if (frtPayable != string.Empty)
            {
                if (frtPayable.Split('|').Length > 1)
                {
                    string portCode = frtPayable.Split('|')[1].Trim();
                    int portId = new ImportBLL().GetPortId(portCode);
                    ViewState[FRTPAYBLEATID] = portId;
                }
                else
                {
                    ViewState[FRTPAYBLEATID] = null;
                }
            }
            else
            {
                ViewState[FRTPAYBLEATID] = null;
            }
        }

        //FinalDestination
        void AC_Port4_TextChanged(object sender, EventArgs e)
        {
            string fdPort = ((TextBox)AC_Port4.FindControl("txtPort")).Text;

            if (fdPort != string.Empty)
            {
                if (fdPort.Split('|').Length > 1)
                {
                    string portCode = fdPort.Split('|')[1].Trim();
                    int portId = new ImportBLL().GetPortId(portCode);
                    ViewState[FINALDESTINATIONID] = portId;

                    SetCargoMovementCode();
                }
                else
                {
                    ViewState[FINALDESTINATIONID] = null;
                }
            }
            else
            {
                ViewState[FINALDESTINATIONID] = null;
            }
        }

        // PortOfDischarge
        void AC_Port3_TextChanged(object sender, EventArgs e)
        {
            string dischargePort = ((TextBox)AC_Port3.FindControl("txtPort")).Text;

            if (dischargePort != string.Empty)
            {
                if (dischargePort.Split('|').Length > 1)
                {

                    string portCode = dischargePort.Split('|')[1].Trim();
                    int portId = new ImportBLL().GetPortId(portCode);
                    ViewState[PORTOFDISCHARGEID] = portId;

                    hdnPortDischarge.Value = dischargePort.Split('|')[0].Trim();

                    if (((TextBox)AC_Port4.FindControl("txtPort")).Text == string.Empty)
                    {
                        ((TextBox)AC_Port4.FindControl("txtPort")).Text = dischargePort;
                        ViewState[FINALDESTINATIONID] = portId;
                    }
                    SetCargoMovementCode();
                }
                else
                {
                    ViewState[PORTOFDISCHARGEID] = null;
                }
            }
            else
            {
                ViewState[PORTOFDISCHARGEID] = null;
            }
        }

        //PortOfLoading
        void AC_Port2_TextChanged(object sender, EventArgs e)
        {
            string loadingPort = ((TextBox)AC_Port2.FindControl("txtPort")).Text;

            if (loadingPort != string.Empty)
            {
                if (loadingPort.Split('|').Length > 1)
                {
                    string portCode = loadingPort.Split('|')[1].Trim();
                    int portId = new ImportBLL().GetPortId(portCode);
                    ViewState[PORTOFLOADINGID] = portId;

                    hdnPortLoading.Value = loadingPort.Split('|')[0].Trim();
                }
                else
                {
                    ViewState[PORTOFLOADINGID] = null;
                }
            }
            else
            {
                ViewState[PORTOFLOADINGID] = null;
            }
        }

        //IssuePort
        void AC_Port1_TextChanged(object sender, EventArgs e)
        {
            string issuePort = ((TextBox)AC_Port1.FindControl("txtPort")).Text;

            if (issuePort != string.Empty)
            {
                if (issuePort.Split('|').Length > 1)
                {
                    string portCode = issuePort.Split('|')[1].Trim();
                    int portId = new ImportBLL().GetPortId(portCode);
                    ViewState[ISSUEPORTID] = portId;

                    if (((TextBox)AC_Port2.FindControl("txtPort")).Text == string.Empty)
                    {
                        ((TextBox)AC_Port2.FindControl("txtPort")).Text = issuePort;
                        ViewState[PORTOFLOADINGID] = portId;

                        hdnPortLoading.Value = issuePort.Split('|')[0].Trim();
                    }
                }
                else
                {
                    ViewState[ISSUEPORTID] = null;
                }
            }
            else
            {
                ViewState[ISSUEPORTID] = null;
            }
        }

        void AC_DeliveryTo1_TextChanged(object sender, EventArgs e)
        {
            string deliveryTo = ((TextBox)AC_DeliveryTo1.FindControl("txtDeliveryTo")).Text.Trim();

            if (deliveryTo != string.Empty)
            {
                int deliveryToId = new ImportBLL().GetDeliveryToId(deliveryTo);
                ViewState[DELIVERYTOID] = deliveryToId;
            }
            else
            {
                ViewState[DELIVERYTOID] = null;
            }
        }

        void AC_Vessel1_TextChanged(object sender, EventArgs e)
        {
            string vessel = ((TextBox)AC_Vessel1.FindControl("txtVessel")).Text;

            if (vessel != string.Empty)
            {
                //if (vessel.Split('|').Length > 1)
                //{
                //    string vesselName = vessel.Split('|')[0].Trim();
                int VesselId = new ImportBLL().GetVesselId(vessel);
                ViewState[VESSELID] = VesselId;
                LoadVoyageDDL();
                //}
                //else
                //{
                //    ViewState[VESSELID] = null;
                //}
            }
            else
            {
                ViewState[VESSELID] = null;
                ddlVoyage.Items.Clear();
                ddlVoyage.Items.Add(new ListItem("--Select", "0"));
            }
        }

        //protected void txtCFSCode_TextChanged(object sender, EventArgs e)
        //{
        //    txtCFSName.Text = new ImportBLL().GetCFSName(txtCFSCode.Text.Trim());
        //}

        protected void txtLineBL_TextChanged(object sender, EventArgs e)
        {
            if (txtIgmBLNo.Text.Trim() == string.Empty)
                txtIgmBLNo.Text = txtLineBL.Text;

            if (txtIgmBLNo.Text.Trim().ToUpper() != txtLineBL.Text.Trim().ToUpper())
                rfvLineBLVessel.Visible = true;
            else
                rfvLineBLVessel.Visible = false;

        }

        protected void txtIgmBLNo_TextChanged(object sender, EventArgs e)
        {
            if (txtIgmBLNo.Text.Trim().ToUpper() != txtLineBL.Text.Trim().ToUpper())
                rfvLineBLVessel.Visible = true;
            else
                rfvLineBLVessel.Visible = false;
        }

        protected void txtLineBLDate_TextChanged(object sender, EventArgs e)
        {
            if (txtIgmBLDate.Text.Trim() == string.Empty)
                txtIgmBLDate.Text = txtLineBLDate.Text;
        }

        protected void gvwFooter_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditBLFooter")
            {
                EditFooter(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "Remove")
            {
                DeleteFooter(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void gvwFooter_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 9);
                ScriptManager sManager = ScriptManager.GetCurrent(this);

                e.Row.Cells[0].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "CntrNo"));
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ContainerType"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "CntrSize"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "GrossWeight"));
                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Package"));
                //e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Waiver"));

                // Edit link
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.ToolTip = "Edit";
                btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BLFooterID"));

                //Delete link
                ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                btnRemove.ToolTip = "Remove";
                btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BLFooterID"));


                btnRemove.OnClientClick = "javascript:return confirm('Are you sure about delete?');";
            }
        }

        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            errContainer.Text = "";

            if (rdoCargoType.SelectedValue == "F" || rdoCargoType.SelectedValue == "E")
            {
                btnAddRow.Enabled = true;
            }
            else if (rdoCargoType.SelectedValue == "L")
            {
                if (gvwFooter.Rows.Count > 0)
                {
                    btnAddRow.Enabled = false;
                    return;
                }

                btnAddRow.Enabled = false;
            }

            //if (IsValidContainerNo())
            //{

            if (rdoFtrSoc.SelectedValue == "N")
                IsValidContainerNo();


            CheckContainerNumber();

            if (Convert.ToBoolean(ViewState["IsValidContainer"]))
            {
                if (Convert.ToString(ViewState[EDITBLFOOTER]) == "")
                    AddBLFooterDetails();
                else
                    EditFooterDetails();
            }
            else
            {
                errContainer.Text = "Please enter unique Container No";
            }
            //}
        }

        protected void rdoFrightType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoFrightType.SelectedValue == "PP")
            {
                txtFrightToCollect.Enabled = false;
                //txtFreightPayable.Enabled = false;
                ((TextBox)AC_Port5.FindControl("txtPort")).Enabled = false;
                ((TextBox)AC_Port5.FindControl("txtPort")).Text = "";
                ViewState[FRTPAYBLEATID] = null;
                rfvFrightToCollect.Visible = false;
                //rfvFreightPayable.Visible = false;
            }
            else
            {
                txtFrightToCollect.Enabled = true;
                //txtFreightPayable.Enabled = true;
                ((TextBox)AC_Port5.FindControl("txtPort")).Enabled = true;
                rfvFrightToCollect.Visible = true;
                //rfvFreightPayable.Visible = true;

                SetDefaultFreightPayableAt();
            }
        }

        protected void rdoHazardousCargo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUNOCode.Text = "ZZZZZ";
            txtIMOCode.Text = "ZZZ";

            if (rdoHazardousCargo.SelectedValue == "No")
            {
                txtUNOCode.Enabled = false;
                txtIMOCode.Enabled = false;
            }
            else
            {
                txtUNOCode.Enabled = true;
                txtIMOCode.Enabled = true;
            }
        }

        protected void rdoDPT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoDPT.SelectedValue == "Yes")
            {
                ((TextBox)AC_DeliveryTo1.FindControl("txtDeliveryTo")).Enabled = true;
                rdoCfsNominated.SelectedValue = "N";
                //txtCFSCode.Enabled = false;
                ((TextBox)AC_CFSCode1.FindControl("txtCFSCode")).Enabled = false;
                txtCFSName.Enabled = false;
                rdoCfsNominated.Enabled = false;
                //rfvCFSCode.Visible = false;
            }
            else
            {
                ((TextBox)AC_DeliveryTo1.FindControl("txtDeliveryTo")).Enabled = false;
                rdoCfsNominated.SelectedValue = "L";
                //txtCFSCode.Enabled = true;
                ((TextBox)AC_CFSCode1.FindControl("txtCFSCode")).Enabled = true;
                txtCFSName.Enabled = false;
                rdoCfsNominated.Enabled = true;
                //rfvCFSCode.Visible = true;
            }
        }

        protected void rdoLockDO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoLockDO.SelectedValue == "Yes")
            {
                txtLockDOComment.Enabled = true;
                rfvLockDOComment.Visible = true;
            }
            else
            {
                txtLockDOComment.Enabled = false;
                rfvLockDOComment.Visible = false;
            }
        }

        protected void rdoNatureCargo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoNatureCargo.SelectedValue == "C")
            {
                txtTEU.Enabled = true;
                txtFEU.Enabled = true;
                rfvTEU.Visible = true;
                //rfvFEU.Visible = true;  -- As per Tapas Da -- 10-03-13
                rfvFEU.Visible = false;

                txtGrossWeight.Enabled = true;
                txtVolume.Enabled = true;
                ((TextBox)AC_WeightUnit1.FindControl("txtWeightUnit")).Enabled = true;
                ((TextBox)AC_VolumeUnit1.FindControl("txtVolUnit")).Enabled = true;
                //rfvVolume.Visible = true;
                rfvGrossWeight.Visible = true;

                rdoCargoType.Enabled = true;
                rdoCargoType.SelectedValue = "F";

                btnAddRow.Enabled = true;
            }
            else
            {
                txtTEU.Enabled = false;
                txtFEU.Enabled = false;
                txtTEU.Text = "";
                txtFEU.Text = "";
                rfvTEU.Visible = false;
                rfvFEU.Visible = false;

                rdoCargoType.Enabled = false;
                rdoCargoType.SelectedValue = "N";

                btnAddRow.Enabled = false;
            }

            if (rdoNatureCargo.SelectedValue == "LB")
            {
                //lblGrossWt.Text = "Volume";
                //lblUnit.Text = "Unit of Volume";

                txtGrossWeight.Enabled = false;
                txtVolume.Enabled = true;

                ((TextBox)AC_WeightUnit1.FindControl("txtWeightUnit")).Enabled = false;
                ((TextBox)AC_VolumeUnit1.FindControl("txtVolUnit")).Enabled = true;

                //rfvVolume.Visible = true;
                rfvGrossWeight.Visible = false;
            }
            else
            {
                //lblGrossWt.Text = "Gross Weight";
                //lblUnit.Text = "Unit of Weight";

                txtGrossWeight.Enabled = true;
                txtVolume.Enabled = false;

                ((TextBox)AC_WeightUnit1.FindControl("txtWeightUnit")).Enabled = true;
                ((TextBox)AC_VolumeUnit1.FindControl("txtVolUnit")).Enabled = false;

                //rfvVolume.Visible = false;
                rfvGrossWeight.Visible = false;
            }
        }

        protected void rdoCargoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoCargoType.SelectedValue == "L")
            {
                txtSublineNo.Enabled = true;

                if (Convert.ToInt64(ViewState[BLHEADERID]) > 0)
                {
                    btnAddRow.Enabled = false;
                }
                else
                {
                    btnAddRow.Enabled = true;
                }
            }
            else if (rdoCargoType.SelectedValue == "F" || rdoCargoType.SelectedValue == "E")
            {
                txtSublineNo.Enabled = false;
                btnAddRow.Enabled = true;
            }
            else
            {
                txtSublineNo.Enabled = false;
                btnAddRow.Enabled = false;
            }
        }

        protected void txtWaiver_TextChanged(object sender, EventArgs e)
        {
            if (txtWaiver.Text != string.Empty)
            {
                if (Convert.ToDecimal(txtWaiver.Text) == 0)
                {
                    rdoWaiverType.SelectedValue = "N";
                    rdoWaiverType.Enabled = false;
                    btnUpload.Enabled = false;
                }
                else
                {
                    rdoWaiverType.SelectedValue = "BW";
                    rdoWaiverType.Enabled = true;
                    btnUpload.Enabled = true;
                }
            }
            else
            {
                txtWaiver.Text = "0.000";
            }
        }

        protected void ddlNvocc_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ILine> lstLine = Session["NVOCC"] as List<ILine>;
            int nvoccId = Convert.ToInt32(ddlNvocc.SelectedValue);

            //imgLineLogo.ImageUrl = lstLine.Where(l => l.NVOCCID == nvoccId).FirstOrDefault().LogoPath;

            int defaultFreeDays = new ImportBLL().GetDefaultFreeDays(nvoccId);
            txtDestFreeDays.Text = defaultFreeDays.ToString();
        }

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            int PGRFreeDays = new ImportBLL().GetPGRFreeDays(Convert.ToInt32(ddlLocation.SelectedValue));
            txtPGRFreeDays.Text = PGRFreeDays.ToString();

            ((AjaxControlToolkit.AutoCompleteExtender)AC_Consignee1.FindControl("AutoPort")).ContextKey = ddlLocation.SelectedValue;
            ((AjaxControlToolkit.AutoCompleteExtender)AC_NParty1.FindControl("AutoPort")).ContextKey = ddlLocation.SelectedValue;
            ((AjaxControlToolkit.AutoCompleteExtender)AC_CHA1.FindControl("AutoPort")).ContextKey = ddlLocation.SelectedValue;
            ((AjaxControlToolkit.AutoCompleteExtender)AC_CFSCode1.FindControl("AutoPort")).ContextKey = ddlLocation.SelectedValue;
            LoadSurveyorDDL();
        }

        protected void ddlFtrContainerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string TareWeight = RetriveTareWeight().ToString();
            txtFtrTareWt.Text = TareWeight;

            if (rdoCargoType.SelectedValue == "E")
            {
                if (txtFtrGrossWeight.Text == string.Empty)
                    txtFtrGrossWeight.Text = TareWeight;
            }
            //if (txtFtrCargoWt.Text == string.Empty)
            //    txtFtrCargoWt.Text = (Convert.ToDecimal(TareWeight) / 1000).ToString();
        }

        protected void ddlFtrContainerSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int64 LocationId = Convert.ToInt64(ddlLocation.SelectedValue);
            int ContainerSize = Convert.ToInt32(ddlFtrContainerSize.SelectedValue);
            string IsoCode = new ImportBLL().GetISOCode(LocationId, ContainerSize);

            txtFtrISOCode.Text = IsoCode;
        }

        protected void rdoFtrTemperatureUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            rdoFtrTempUnit.SelectedValue = rdoFtrTemperatureUnit.SelectedValue;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            IBLHeader blHeader = new BLHeaderEntity();

            if (ValidateSave())
            {
                BuildImportBLEntity(blHeader);

                if (rdoCargoType.SelectedValue != "N")
                {
                    List<IBLFooter> footers = ViewState[FOOTERDETAILS] as List<IBLFooter>;
                    blHeader.BLFooter = footers;
                }

                //Add-Update
                int blId = new ImportBLL().SaveImportBL(blHeader);

                //ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('Record saved successfully!');</script>", false);
                //Response.Redirect("~/Transaction/ImportBL.aspx");

                //ScriptManager.RegisterStartupScript(this, typeof(Page), "save", "<script> alert('Save successfully');  window.location.href ='~/Transaction/ImportBL.aspx'<script>", false);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Redit", "alert('Record saved successfully!'); window.location='" + string.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.ServerVariables["HTTP_HOST"], (HttpContext.Current.Request.ApplicationPath.Equals("/")) ? string.Empty : HttpContext.Current.Request.ApplicationPath) + "/Transaction/ImportBL.aspx';", true);
            }
        }


        private decimal RetriveTareWeight()
        {
            DataTable dtTareWeight = new ImportBLL().GetTareWeight(Convert.ToInt32(ddlFtrContainerType.SelectedValue));

            decimal TareWeight = 0;

            if (ddlFtrContainerSize.SelectedValue == "20")
            {
                if (dtTareWeight.Rows[0]["TareWeight20"] != DBNull.Value)
                    TareWeight = Convert.ToDecimal(dtTareWeight.Rows[0]["TareWeight20"]);
            }
            else
            {
                if (dtTareWeight.Rows[0]["TareWeight40"] != DBNull.Value)
                    TareWeight = Convert.ToDecimal(dtTareWeight.Rows[0]["TareWeight40"]);
            }

            return TareWeight;
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

        private void InitialActivities()
        {
            //txtLineBLDate.Text = Convert.ToString(DateTime.Now.ToString("dd-MM-yyyy"));
            //txtIgmBLDate.Text = Convert.ToString(DateTime.Now.ToString("dd-MM-yyyy"));
            ((TextBox)AC_Port5.FindControl("txtPort")).Enabled = false;

            LoadNvoccDDL();
            LoadLocationDDL();
            LoadContainerTypeDDL();
            //ISOCodeDDL();
            LoadSurveyorDDL();
            //LoadCHADDL();

            string PAN = new ImportBLL().GetPanNoById(1);
            txtMLOCode.Text = PAN;
            txtFtrAgentCode.Text = PAN;
        }

        private void SetDefaultFreightPayableAt()
        {
            if (((TextBox)AC_Port5.FindControl("txtPort")).Text.Trim() == string.Empty)
            {
                int locationId = Convert.ToInt32(ddlLocation.SelectedValue);
                DataTable dt = new ImportBLL().GetDefaultFreightPayableAt(locationId);

                if (dt != null && dt.Rows.Count > 0)
                {
                    ((TextBox)AC_Port5.FindControl("txtPort")).Text = Convert.ToString(dt.Rows[0]["Name"]);
                    ViewState[FRTPAYBLEATID] = Convert.ToString(dt.Rows[0]["PortID"]);
                }
            }
        }

        private void SetDefaultUnit()
        {
            //For Package
            if (((TextBox)AC_PackageUnit1.FindControl("txtPkgUnit")).Text.Trim() == string.Empty)
            {
                DataTable dt = new ImportBLL().GetDefaultUnit("P", "PKG");

                if (dt != null && dt.Rows.Count > 0)
                {
                    ((TextBox)AC_PackageUnit1.FindControl("txtPkgUnit")).Text = Convert.ToString(dt.Rows[0]["UnitName"]);
                    ViewState[PACKAGEUNITID] = Convert.ToString(dt.Rows[0]["pk_UOMId"]);
                }
            }

            //For Weight
            if (((TextBox)AC_WeightUnit1.FindControl("txtWeightUnit")).Text.Trim() == string.Empty)
            {
                DataTable dt = new ImportBLL().GetDefaultUnit("W", "KGS");

                if (dt != null && dt.Rows.Count > 0)
                {
                    ((TextBox)AC_WeightUnit1.FindControl("txtWeightUnit")).Text = Convert.ToString(dt.Rows[0]["UnitName"]);
                    ViewState[WEIGHTUNITID] = Convert.ToString(dt.Rows[0]["pk_UOMId"]);
                }
            }

            //For Volume
            if (((TextBox)AC_VolumeUnit1.FindControl("txtVolUnit")).Text.Trim() == string.Empty)
            {
                DataTable dt = new ImportBLL().GetDefaultUnit("V", "CBM");

                if (dt != null && dt.Rows.Count > 0)
                {
                    ((TextBox)AC_VolumeUnit1.FindControl("txtVolUnit")).Text = Convert.ToString(dt.Rows[0]["UnitName"]);
                    ViewState[VOLUMEUNITID] = Convert.ToString(dt.Rows[0]["pk_UOMId"]);
                }
            }
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

        private void LoadLocationDDL()
        {
            List<ILocation> lstLocation = new ImportBLL().GetLocation(_userId, false);
            ddlLocation.DataValueField = "Id";
            ddlLocation.DataTextField = "Name";
            ddlLocation.DataSource = lstLocation;
            ddlLocation.DataBind();
            ddlLocation.SelectedValue = lstLocation[0].DefaultLocation.ToString();

            ((AjaxControlToolkit.AutoCompleteExtender)AC_Consignee1.FindControl("AutoPort")).ContextKey = ddlLocation.SelectedValue;
            ((AjaxControlToolkit.AutoCompleteExtender)AC_NParty1.FindControl("AutoPort")).ContextKey = ddlLocation.SelectedValue;
            ((AjaxControlToolkit.AutoCompleteExtender)AC_CHA1.FindControl("AutoPort")).ContextKey = ddlLocation.SelectedValue;
            ((AjaxControlToolkit.AutoCompleteExtender)AC_CFSCode1.FindControl("AutoPort")).ContextKey = ddlLocation.SelectedValue;

            int PGRFreeDays = new ImportBLL().GetPGRFreeDays(Convert.ToInt32(ddlLocation.SelectedValue));
            txtPGRFreeDays.Text = PGRFreeDays.ToString();

            List<ILocation> lstStockLocation = new ImportBLL().GetLocation(_userId, true);
            ddlStockLocation.DataValueField = "Id";
            ddlStockLocation.DataTextField = "Name";
            ddlStockLocation.DataSource = lstStockLocation;
            ddlStockLocation.DataBind();
            ddlStockLocation.SelectedValue = lstStockLocation[0].DefaultLocation.ToString();
        }

        private void LoadVoyageDDL()
        {
            DataTable dt = new ImportBLL().GetVoyages(Convert.ToInt32(ViewState[VESSELID]));
            DataRow dr = dt.NewRow();
            dr["pk_VoyageID"] = "0";
            dr["VoyageNo"] = "--Select--";
            dt.Rows.InsertAt(dr, 0);
            ddlVoyage.DataValueField = "pk_VoyageID";
            ddlVoyage.DataTextField = "VoyageNo";
            ddlVoyage.DataSource = dt;
            ddlVoyage.DataBind();

        }

        private void LoadContainerTypeDDL()
        {
            DataTable dt = new ImportBLL().GetContainerType();
            DataRow dr = dt.NewRow();
            dr["pk_ContainerTypeID"] = "0";
            dr["ContainerAbbr"] = "--Select--";
            dt.Rows.InsertAt(dr, 0);
            ddlFtrContainerType.DataValueField = "pk_ContainerTypeID";
            ddlFtrContainerType.DataTextField = "ContainerAbbr";
            ddlFtrContainerType.DataSource = dt;
            ddlFtrContainerType.DataBind();
        }

        private void LoadSurveyorDDL()
        {
            DataTable dt = new ImportBLL().GetEmptyYard(Convert.ToInt64(ddlLocation.SelectedValue));
            DataRow dr = dt.NewRow();
            dr["fk_AddressID"] = "0";
            dr["AddrName"] = "--Select--";
            dt.Rows.InsertAt(dr, 0);
            ddlSurveyor.DataValueField = "fk_AddressID";
            ddlSurveyor.DataTextField = "AddrName";
            ddlSurveyor.DataSource = dt;
            ddlSurveyor.DataBind();
        }

        /*
        private void LoadCHADDL()
        {
            try
            {
                DataTable dt = new ImportBLL().GetCHAId();

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

        //private void ISOCodeDDL()
        //{
        //    DataTable dt = new ImportBLL().GetISOCode();
        //    DataRow dr = dt.NewRow();
        //    dr["ISOAbbr"] = "0";
        //    dr["ISOName"] = "--Select--";
        //    dt.Rows.InsertAt(dr, 0);
        //    ddlFtrIsoCode.DataValueField = "ISOAbbr";
        //    ddlFtrIsoCode.DataTextField = "ISOName";
        //    ddlFtrIsoCode.DataSource = dt;
        //    ddlFtrIsoCode.DataBind();
        //}

        private void SetCargoMovementCode()
        {
            if (((TextBox)AC_Port4.FindControl("txtPort")).Text == ((TextBox)AC_Port3.FindControl("txtPort")).Text)
            {
                txtCMCode.Text = "LC";

                txtTPBondNo.Enabled = false;
            }
            else
            {
                string code = (((TextBox)AC_Port4.FindControl("txtPort")).Text.Split('|')[1]).Trim();
                code = code.Substring(code.Length - 1, 1);

                if (code == "6")
                    txtCMCode.Text = "TI";
                else
                    txtCMCode.Text = "TC";

                txtTPBondNo.Enabled = true;
            }
        }

        private void BuildImportBLEntity(IBLHeader header)
        {

            if (ViewState[BLHEADERID] == null)
                header.BLID = 0;
            else
                header.BLID = Convert.ToInt64(ViewState[BLHEADERID]);

            header.BillOfEntryNo = Convert.ToString(txtBillEntery.Text.Trim());
            header.BLIssuePortID = Convert.ToInt32(ViewState[ISSUEPORTID]);
            //header.CACode = Convert.ToString(txtCACode.Text.Trim());
            header.CargoMovement = Convert.ToString(txtCMCode.Text.Trim());
            header.CargoNature = Convert.ToString(rdoNatureCargo.SelectedValue);
            header.CargoType = Convert.ToString(rdoCargoType.SelectedValue);

            if (txtDestFreeDays.Text.Trim() != string.Empty)
                header.DetentionFree = Convert.ToInt32(txtDestFreeDays.Text.Trim());

            header.DetentionSlabType = Convert.ToString(rdoDestSlab.SelectedValue);

            if (Convert.ToString(rdoLockDO.SelectedValue) == "Yes")
                header.DOLock = true;
            else
                header.DOLock = false;

            header.DOLockReason = Convert.ToString(txtLockDOComment.Text.Trim());

            if (Convert.ToString(rdoDPT.SelectedValue) == "Yes")
                header.DPT = true;
            else
                header.DPT = false;

            header.FinalDestination = Convert.ToInt32(ViewState[FINALDESTINATIONID]);

            if (Convert.ToString(rdoFreeOut.SelectedValue) == "Yes")
                header.FreeOut = true;
            else
                header.FreeOut = false;

            header.FreightType = Convert.ToString(rdoFrightType.SelectedValue);

            if (txtFrightToCollect.Text.Trim() != string.Empty)
                header.FreigthToCollect = Convert.ToDouble(txtFrightToCollect.Text);

            if (txtGrossWeight.Text.Trim() != string.Empty)
                header.GrossWeight = Convert.ToDouble(txtGrossWeight.Text);

            if (Convert.ToString(rdoHazardousCargo.SelectedValue) == "Yes")
                header.HazFlag = true;
            else
                header.HazFlag = false;

            header.IGMBLDate = Convert.ToDateTime(txtIgmBLDate.Text);
            header.IGMBLNumber = Convert.ToString(txtIgmBLNo.Text);
            header.IMOCode = Convert.ToString(txtIMOCode.Text.Trim());
            header.ImpLineBLDate = Convert.ToDateTime(txtLineBLDate.Text.Trim());
            header.ImpLineBLNo = Convert.ToString(txtLineBL.Text);
            header.ImpVesselID = Convert.ToInt32(ViewState[VESSELID]);
            header.ImpVoyageID = Convert.ToInt32(ddlVoyage.SelectedValue);
            header.ItemLineNo = Convert.ToString(txtLINo.Text.Trim());
            header.ItemLinePrefix = Convert.ToString(txtLinePrefix.Text.Trim());
            header.ItemSubLineNo = Convert.ToString(txtSublineNo.Text.Trim());
            header.ItemType = Convert.ToString(rdoItemType.SelectedValue);
            header.LineBLType = Convert.ToString(rdoLineBLType.SelectedValue);
            header.LocationID = Convert.ToInt32(ddlLocation.SelectedValue);
            header.MLOCode = Convert.ToString(txtMLOCode.Text.Trim());
            header.NVOCCID = Convert.ToInt32(ddlNvocc.SelectedValue);

            if (txtFEU.Text.Trim() != string.Empty)
                header.NoofFEU = Convert.ToInt32(txtFEU.Text);

            if (txtTEU.Text.Trim() != string.Empty)
                header.NoofTEU = Convert.ToInt32(txtTEU.Text);


            header.PackageDetail = Convert.ToString(txtPackage.Text.Trim());

            if (Convert.ToString(rdoPartBL.SelectedValue) == "Yes")
                header.PartBL = true;
            else
                header.PartBL = false;

            if (txtPGRFreeDays.Text.Trim() != string.Empty)
                header.PGR_FreeDays = Convert.ToInt32(txtPGRFreeDays.Text.Trim());

            header.PortDischarge = Convert.ToInt32(ViewState[PORTOFDISCHARGEID]);
            header.PortLoading = Convert.ToInt32(ViewState[PORTOFLOADINGID]);

            if (Convert.ToString(rdoReefer.SelectedValue) == "Yes")
                header.Reefer = true;
            else
                header.Reefer = false;

            header.StockLocationID = Convert.ToInt32(ddlStockLocation.SelectedValue);
            header.SurveyorAddressID = Convert.ToInt32(ddlSurveyor.SelectedValue); //Convert.ToInt32(ViewState[SURVEYORID]);

            if (Convert.ToString(rdoTaxExempted.SelectedValue) == "Yes")
                header.TaxExemption = true;
            else
                header.TaxExemption = false;

            header.TPBondNo = Convert.ToString(txtTPBondNo.Text.Trim());
            header.TransportMode = Convert.ToString(rdoTransportMode.SelectedValue);
            header.UnitOfVolume = Convert.ToInt32(ViewState[VOLUMEUNITID]);
            header.UnitOfWeight = Convert.ToInt32(ViewState[WEIGHTUNITID]);
            header.UnitPackage = Convert.ToInt32(ViewState[PACKAGEUNITID]);
            header.UNOCode = Convert.ToString(txtUNOCode.Text.Trim());

            if (txtVolume.Text.Trim() != string.Empty)
                header.Volume = Convert.ToDouble(txtVolume.Text.Trim());

            if (txtWaiver.Text.Trim() != string.Empty)
                header.WaiverPercent = Convert.ToDouble(txtWaiver.Text);

            header.WaiverType = Convert.ToString(rdoWaiverType.SelectedValue);
            header.TranShipment = Convert.ToString(txtTransinfo.Text.Trim());
            header.ShipperInformation = Convert.ToString(txtShipperAddr.Text.Trim());
            header.ShipperName = Convert.ToString(((TextBox)AC_Shipper1.FindControl("txtShipper")).Text.Trim());
            header.MarksNumbers = Convert.ToString(txtMarks.Text.Trim());
            header.GoodDescription = Convert.ToString(txtDescGoods.Text.Trim());
            header.ConsigneeInformation = Convert.ToString(txtConsigneeAddr.Text.Trim());
            header.ConsigneeName = Convert.ToString(((TextBox)AC_Consignee1.FindControl("txtConsignee")).Text.Trim());
            header.BLComment = Convert.ToString(txtBLComment.Text.Trim());
            header.NotifyName = Convert.ToString(((TextBox)AC_NParty1.FindControl("txtNParty")).Text.Trim());
            header.NotifyPartyInformation = Convert.ToString(txtNotifyingPartyAddr.Text.Trim());
            header.dtAdded = DateTime.Now;
            header.dtEdited = DateTime.Now;
            header.RsStatus = true;
            header.ONBR = true;
            header.CompanyID = 1;
            header.Billing = false;

            header.NumberPackage = Convert.ToInt32(txtPackage.Text);

            header.WaiverDate = DateTime.Now;
            header.Commodity = Convert.ToString(txtCommodity.Text);
            header.LineBLVesselDetail = Convert.ToString(txtLineBLVessel.Text);
            header.CANTo = txtCargoArrivalNotice.Text.Trim();//Convert.ToString(((TextBox)AC_CANotice1.FindControl("txtCANotice")).Text.Trim());
            header.WaiverFk_UserID = _userId;
            header.UserAdded = _userId;
            header.UserEdited = _userId;

            header.CHAId = Convert.ToInt64(ViewState["CHAID"]);

            if (hdnFilePath.Value != string.Empty)
                header.WaiverLetterUpload = @"~\Transaction\WaiverFile\" + hdnFilePath.Value;

            header.CFSNomination = Convert.ToString(rdoCfsNominated.SelectedValue);

            if (ViewState[CFSADDRID] != null)
                header.AddressCFSId = Convert.ToInt32(ViewState[CFSADDRID]);

            if (ViewState[FRTPAYBLEATID] != null)
                header.PortFrtPayableID = Convert.ToInt32(ViewState[FRTPAYBLEATID]);

            if (ViewState[DELIVERYTOID] != null)
                header.DPTId = Convert.ToInt32(ViewState[DELIVERYTOID]);
            //During add this will empty. During edit if the user changes detention free days or slab then his id will be stored here
            //header.DetChangedByID = Convert.ToInt32(); 



            //Extra
            //header.AddressCHAID = Convert.ToInt32();
            //header.BLID = Convert.ToInt32(); //Auto Generated
            //header.MotherVessel = Convert.ToString(); //remove from db
            //header.HookPoint = Convert.ToBoolean(); //remove from db
            //header.PlaceDelivery = Convert.ToInt32(); //allow null in db
            //header.PlaceReceipt = Convert.ToInt32(); //allow null in db
            //header.DocFact = Convert.ToBoolean();
        }

        private void BuildFooterEntity(IBLFooter footer)
        {
            if (ViewState[BLFOOTERID] == null)
                footer.BLFooterID = -1;
            else
                footer.BLFooterID = Convert.ToInt32(ViewState[BLFOOTERID]) - 1;

            ViewState[BLFOOTERID] = footer.BLFooterID;

            footer.CAgent = Convert.ToString(txtFtrAgentCode.Text.Trim());
            footer.CallNo = Convert.ToString(txtFtrCallNo.Text.Trim());
            footer.Cargo = Convert.ToString(txtFtrCargo.Text.Trim());

            if (txtFtrCargoWt.Text.Trim() != string.Empty)
                footer.CargoWtTon = Convert.ToDecimal(txtFtrCargoWt.Text.Trim());

            footer.CntrNo = Convert.ToString(txtFtrContainerNo.Text.Trim().ToUpper());
            footer.CntrSize = Convert.ToString(ddlFtrContainerSize.SelectedValue);
            footer.Comodity = Convert.ToString(txtFtrCommodity.Text.Trim());
            footer.ContainerTypeID = Convert.ToInt32(ddlFtrContainerType.SelectedValue);
            footer.ContainerType = ddlFtrContainerType.SelectedItem.Text;
            //footer.ContainerType = Convert.ToString(ddlFtrContainerType.Text);
            footer.CustomSeal = Convert.ToString(txtFtrCustSeal.Text.Trim());
            footer.DIMCode = Convert.ToString(txtFtrDimCode.Text.Trim());

            if (txtFtrGrossWeight.Text.Trim() != string.Empty)
                footer.GrossWeight = Convert.ToDecimal(txtFtrGrossWeight.Text.Trim());

            footer.IMCO = Convert.ToString(txtFtrImcoNo.Text.Trim());
            //footer.ISOCode = Convert.ToString(ddlFtrIsoCode.SelectedValue);
            footer.ISOCode = txtFtrISOCode.Text.Trim();

            if (txtFtrODHeight.Text.Trim() != string.Empty)
                footer.ODHeight = Convert.ToDecimal(txtFtrODHeight.Text.Trim());
            if (txtFtrODLength.Text.Trim() != string.Empty)
                footer.ODLength = Convert.ToDecimal(txtFtrODLength.Text.Trim());
            if (txtFtrODWidth.Text.Trim() != string.Empty)
                footer.ODWidth = Convert.ToDecimal(txtFtrODWidth.Text.Trim());
            if (txtFtrPackage.Text.Trim() != string.Empty)
                footer.Package = Convert.ToInt32(txtFtrPackage.Text.Trim());

            footer.SealNo = Convert.ToString(txtFtrSealNo.Text.Trim());
            footer.SOC = Convert.ToString(rdoFtrSoc.SelectedValue);
            footer.Stowage = Convert.ToString(txtFtrStowage.Text.Trim());

            if (txtFtrTemperature.Text.Trim() != string.Empty)
                footer.Temperature = Convert.ToDecimal(txtFtrTemperature.Text.Trim());

            if (txtFtrTempMax.Text.Trim() != string.Empty)
                footer.TempMax = Convert.ToDecimal(txtFtrTempMax.Text.Trim());

            if (txtFtrTempMin.Text.Trim() != string.Empty)
                footer.TempMin = Convert.ToDecimal(txtFtrTempMin.Text.Trim());

            footer.TempUnit = Convert.ToString(rdoFtrTemperatureUnit.Text.Trim());
            footer.PCSTemp = Convert.ToString(rdoFtrTemperatureUnit.SelectedValue);

            //LCLDuplicate
            footer.LCLDuplicate = Convert.ToBoolean(ViewState["LCLDuplicate"]);


            //Waiver from Grid


            // Not required for this page
            //footer.DateLastMoved = Convert.ToDateTime(); 
            //footer.MovementID = Convert.ToInt32();
        }

        private bool ValidateSave()
        {
            bool IsValid = true;
            errVessel.Text = "";
            errIssuePort.Text = "";
            errPortOfLoading.Text = "";
            errPortOfDischarge.Text = "";
            errFinalDestination.Text = "";
            errDeliveryTo.Text = "";
            errUnitPackage.Text = "";
            errUnitVW.Text = "";
            //errSurveyor.Text = "";
            errShipper.Text = "";
            errConsignee.Text = "";
            errNP.Text = "";
            errCFS.Text = "";
            errFreight.Text = "";
            errBL.Text = "";
            errContainer.Text = "";
            lblErr.Text = "";

            if (Convert.ToString(ViewState[VESSELID]) == string.Empty || Convert.ToString(ViewState[VESSELID]) == "0")
            {
                IsValid = false;
                errVessel.Text = "This field is required";
            }

            if (Convert.ToString(ViewState[ISSUEPORTID]) == string.Empty || Convert.ToString(ViewState[ISSUEPORTID]) == "0")
            {
                IsValid = false;
                errIssuePort.Text = "This field is required";
            }

            if (Convert.ToString(ViewState[PORTOFLOADINGID]) == string.Empty || Convert.ToString(ViewState[PORTOFLOADINGID]) == "0")
            {
                IsValid = false;
                errPortOfLoading.Text = "This field is required";
            }

            if (Convert.ToString(ViewState[PORTOFDISCHARGEID]) == string.Empty || Convert.ToString(ViewState[PORTOFDISCHARGEID]) == "0")
            {
                IsValid = false;
                errPortOfDischarge.Text = "This field is required";
            }

            if (Convert.ToString(ViewState[FINALDESTINATIONID]) == string.Empty || Convert.ToString(ViewState[FINALDESTINATIONID]) == "0")
            {
                IsValid = false;
                errFinalDestination.Text = "This field is required";
            }

            if (rdoDPT.SelectedValue == "Yes")
            {
                if (Convert.ToString(ViewState[DELIVERYTOID]) == string.Empty || Convert.ToString(ViewState[DELIVERYTOID]) == "0")
                {
                    IsValid = false;
                    errDeliveryTo.Text = "This field is required";
                }
            }

            if (Convert.ToString(ViewState[PACKAGEUNITID]) == string.Empty || Convert.ToString(ViewState[PACKAGEUNITID]) == "0")
            {
                IsValid = false;
                errUnitPackage.Text = "This field is required";
            }

            if (rdoNatureCargo.SelectedValue == "LB")
            {
                if (Convert.ToString(ViewState[VOLUMEUNITID]) == string.Empty)
                {
                    IsValid = false;
                    errUnitVW.Text = "This field is required";
                }
            }
            else
            {
                if (Convert.ToString(ViewState[WEIGHTUNITID]) == string.Empty || Convert.ToString(ViewState[WEIGHTUNITID]) == "0")
                {
                    IsValid = false;
                    errUnitVW.Text = "This field is required";
                }
            }

            //if (((TextBox)AC_Surveyor1.FindControl("txtSurveyor")).Text.Trim() != string.Empty)
            //{
            //    if (Convert.ToString(ViewState[SURVEYORID]) == string.Empty)
            //    {
            //        IsValid = false;
            //        errSurveyor.Text = "Please enter valid surveyor";
            //    }
            //}

            //if (Convert.ToString(ViewState[SHIPERADDRID]) == string.Empty || Convert.ToString(ViewState[SHIPERADDRID]) == "0")
            //{
            //    IsValid = false;
            //    errShipper.Text = "This field is required";
            //}

            /*  -- Blocking on Client Request
            if (Convert.ToString(ViewState[CONSIGNEEEADDRID]) == string.Empty || Convert.ToString(ViewState[CONSIGNEEEADDRID]) == "0")
            {
                IsValid = false;
                errConsignee.Text = "This field is required";
            }

            if (Convert.ToString(ViewState[NPADDRID]) == string.Empty || Convert.ToString(ViewState[NPADDRID]) == "0")
            {
                IsValid = false;
                errNP.Text = "This field is required";
            }
            */

            //if (Convert.ToString(ViewState[CANADDRID]) == string.Empty)
            //{
            //    IsValid = false;
            //}

            if (rdoDPT.SelectedValue == "No")
            {
                if (Convert.ToString(ViewState[CFSADDRID]) == string.Empty || Convert.ToString(ViewState[CFSADDRID]) == "0")
                {
                    IsValid = false;
                    errCFS.Text = "This field is required";
                }
            }

            if (rdoFrightType.SelectedValue == "TC")
            {
                if (Convert.ToString(ViewState[FRTPAYBLEATID]) == string.Empty || Convert.ToString(ViewState[FRTPAYBLEATID]) == "0")
                {
                    IsValid = false;
                    errFreight.Text = "This field is required";
                }
            }

            if (new ImportBLL().IsDuplicateBL(txtLineBL.Text.Trim(), Convert.ToInt64(ViewState[VESSELID]), Convert.ToInt64(ddlVoyage.SelectedValue), Convert.ToInt64(ViewState[BLHEADERID])))
            {
                IsValid = false;
                errBL.Text = "BL Number should be unique";
            }

            //if (Convert.ToBoolean(ViewState["IsValidContainer"]))
            //{
            //    IsValid = false;
            //    errContainer.Text = "Please enter valid container no";
            //}

            List<IBLFooter> footers = ViewState[FOOTERDETAILS] as List<IBLFooter>;

            if (rdoNatureCargo.SelectedValue == "C")
            {
                if (footers == null || footers.Count == 0)
                {
                    IsValid = false;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('No record available for Footer!');</script>", false);
                }
            }

            if (footers != null || footers.Count > 0)
            {
                int headerPackage = Convert.ToInt32(txtPackage.Text.Trim());
                decimal headerGrossWeight = Convert.ToDecimal(txtGrossWeight.Text.Trim());

                int footerPackage = footers.Sum(f => f.Package);
                decimal footerGrossWeight = footers.Sum(f => f.GrossWeight);

                if ((headerPackage != footerPackage) || (headerGrossWeight != footerGrossWeight))
                {
                    IsValid = false;
                    lblErr.Text = "Total Packages & Gross Weight should be equal with B/L Header";
                }
            }

            int teu = 0;
            int feu = 0;

            if (txtTEU.Text != string.Empty)
                teu = Convert.ToInt32(txtTEU.Text);

            if (txtFEU.Text != string.Empty)
                feu = Convert.ToInt32(txtFEU.Text);

            if ((teu + feu) == 0)
            {
                IsValid = false;
                lblErr.Text = "Total number of (TEU + FEU) should be greater than 0";
            }

            //Container in Header & Footer should be same
            if (footers != null || footers.Count > 0)
            {
                int footerTeu = (footers.Where(f => f.CntrSize == "20").Sum(f => Convert.ToInt32(f.CntrSize)) / 20);
                int footerFeu = (footers.Where(f => f.CntrSize == "40").Sum(f => Convert.ToInt32(f.CntrSize)) / 40);

                if ((teu != footerTeu) || (feu != footerFeu))
                {
                    IsValid = false;
                    lblErr.Text = "Total TEU & FEU in header should be equal to B/L Footer";
                }
            }


            return IsValid;
        }

        private void ClearFooterDetail()
        {
            txtFtrContainerNo.Text = "";
            ddlFtrContainerSize.SelectedValue = "0";
            ddlFtrContainerType.SelectedValue = "0";
            txtFtrSealNo.Text = "";
            txtFtrCommodity.Text = "";
            rdoFtrSoc.SelectedValue = "N";
            txtFtrGrossWeight.Text = "";
            txtFtrPackage.Text = "";
            txtFtrCargoWt.Text = "";
            //ddlFtrIsoCode.SelectedValue = "0";
            txtFtrISOCode.Text = "";
            txtFtrAgentCode.Text = "";
            txtFtrTareWt.Text = "";
            txtFtrTemperature.Text = "";
            rdoFtrTemperatureUnit.SelectedValue = "C";
            txtFtrCustSeal.Text = "";
            txtFtrImcoNo.Text = "";
            txtFtrTempMax.Text = "";
            txtFtrTempMin.Text = "";
            rdoFtrTempUnit.SelectedValue = "C";
            txtFtrDimCode.Text = "";
            txtFtrODLength.Text = "";
            txtFtrODWidth.Text = "";
            txtFtrODHeight.Text = "";
            txtFtrCargo.Text = "";
            txtFtrStowage.Text = "";
            txtFtrCallNo.Text = "";
        }

        private void RefreshGridView()
        {
            if (ViewState[FOOTERDETAILS] != null)
                footerDetails = ViewState[FOOTERDETAILS] as List<IBLFooter>;

            gvwFooter.DataSource = footerDetails;
            gvwFooter.DataBind();
        }

        private void EditFooter(int FooterId)
        {
            ViewState[EDITBLFOOTER] = "1";

            if (ViewState[FOOTERDETAILS] != null)
                footerDetails = ViewState[FOOTERDETAILS] as List<IBLFooter>;

            IBLFooter footer = footerDetails.Single(c => c.BLFooterID == FooterId);

            txtFtrContainerNo.Text = footer.CntrNo;
            ddlFtrContainerSize.SelectedValue = footer.CntrSize;
            ddlFtrContainerType.SelectedValue = footer.ContainerTypeID.ToString();
            txtFtrSealNo.Text = footer.SealNo;
            txtFtrCommodity.Text = footer.Comodity;
            rdoFtrSoc.SelectedValue = footer.SOC;
            txtFtrGrossWeight.Text = Convert.ToString(footer.GrossWeight);
            txtFtrPackage.Text = Convert.ToString(footer.Package);
            txtFtrCargoWt.Text = Convert.ToString(footer.CargoWtTon);
            //ddlFtrIsoCode.SelectedValue = footer.ISOCode;
            txtFtrISOCode.Text = footer.ISOCode;
            txtFtrAgentCode.Text = footer.CAgent;
            //txtFtrTareWt.Text = Convert.ToString(footer.TareWt);
            txtFtrTemperature.Text = Convert.ToString(footer.Temperature);
            rdoFtrTemperatureUnit.SelectedValue = footer.TempUnit;
            txtFtrCustSeal.Text = footer.CustomSeal;
            txtFtrImcoNo.Text = footer.IMCO;
            txtFtrTempMax.Text = Convert.ToString(footer.TempMax);
            txtFtrTempMin.Text = Convert.ToString(footer.TempMin);
            //rdoFtrTempUnit.SelectedValue = footer.TempUnit;
            txtFtrDimCode.Text = footer.DIMCode;
            txtFtrODLength.Text = Convert.ToString(footer.ODLength);
            txtFtrODWidth.Text = Convert.ToString(footer.ODWidth);
            txtFtrODHeight.Text = Convert.ToString(footer.ODHeight);
            txtFtrCargo.Text = footer.Cargo;
            txtFtrStowage.Text = footer.Stowage;
            txtFtrCallNo.Text = footer.CallNo;

            if (FooterId < 0)
                ViewState[BLFOOTERID] = FooterId;
            else
                ViewState[EDITBLFOOTERID] = FooterId;

            btnAddRow.Enabled = true;
        }

        private void DeleteFooter(int FooterId)
        {
            if (ViewState[FOOTERDETAILS] != null)
                footerDetails = ViewState[FOOTERDETAILS] as List<IBLFooter>;

            IBLFooter footer = footerDetails.Single(c => c.BLFooterID == FooterId);
            footerDetails.Remove(footer);

            new ImportBLL().DeleteBLFooter(FooterId);

            ViewState[FOOTERDETAILS] = footerDetails;

            ClearFooterDetail();
            RefreshGridView();

            if (footerDetails.Count == 0)
            {
                if (rdoCargoType.SelectedValue != "N")
                    btnAddRow.Enabled = true;
            }
        }

        private void AddBLFooterDetails()
        {
            IBLFooter footer = new BLFooterEntity();
            BuildFooterEntity(footer);

            if (ViewState[FOOTERDETAILS] != null)
                footerDetails = ViewState[FOOTERDETAILS] as List<IBLFooter>;

            footerDetails.Add(footer);

            gvwFooter.DataSource = footerDetails;
            gvwFooter.DataBind();

            ViewState[FOOTERDETAILS] = footerDetails;
            ClearFooterDetail();
        }

        private void EditFooterDetails()
        {
            IBLFooter blFooter;

            if (ViewState[FOOTERDETAILS] != null)
                footerDetails = ViewState[FOOTERDETAILS] as List<IBLFooter>;

            if (Convert.ToInt32(ViewState[BLFOOTERID]) < 0)
                blFooter = footerDetails.Single(c => c.BLFooterID == Convert.ToInt32(ViewState[BLFOOTERID]));
            else
                blFooter = footerDetails.Single(c => c.BLFooterID == Convert.ToInt32(ViewState[EDITBLFOOTERID]));

            footerDetails.Remove(blFooter);

            blFooter.CAgent = Convert.ToString(txtFtrAgentCode.Text.Trim());
            blFooter.CallNo = Convert.ToString(txtFtrCallNo.Text.Trim());
            blFooter.Cargo = Convert.ToString(txtFtrCargo.Text.Trim());
            blFooter.CargoWtTon = Convert.ToDecimal(txtFtrCargoWt.Text.Trim());
            blFooter.CntrNo = Convert.ToString(txtFtrContainerNo.Text.Trim()).ToUpper();
            blFooter.CntrSize = Convert.ToString(ddlFtrContainerSize.SelectedValue);
            blFooter.Comodity = Convert.ToString(txtFtrCommodity.Text.Trim());
            blFooter.ContainerTypeID = Convert.ToInt32(ddlFtrContainerType.SelectedValue);
            blFooter.ContainerType = Convert.ToString(ddlFtrContainerType.SelectedItem.Text);
            blFooter.CustomSeal = Convert.ToString(txtFtrCustSeal.Text.Trim());
            blFooter.DIMCode = Convert.ToString(txtFtrDimCode.Text.Trim());
            blFooter.GrossWeight = Convert.ToDecimal(txtFtrGrossWeight.Text.Trim());
            blFooter.IMCO = Convert.ToString(txtFtrImcoNo.Text.Trim());
            //blFooter.ISOCode = Convert.ToString(ddlFtrIsoCode.SelectedValue);
            blFooter.ISOCode = txtFtrISOCode.Text.Trim();
            blFooter.ODHeight = Convert.ToDecimal(txtFtrODHeight.Text.Trim());
            blFooter.ODLength = Convert.ToDecimal(txtFtrODLength.Text.Trim());
            blFooter.ODWidth = Convert.ToDecimal(txtFtrODWidth.Text.Trim());
            blFooter.Package = Convert.ToInt32(txtFtrPackage.Text.Trim());
            blFooter.SealNo = Convert.ToString(txtFtrSealNo.Text.Trim());
            blFooter.SOC = Convert.ToString(rdoFtrSoc.SelectedValue);
            blFooter.Stowage = Convert.ToString(txtFtrStowage.Text.Trim());
            blFooter.Temperature = Convert.ToDecimal(txtFtrTemperature.Text.Trim());
            blFooter.TempMax = Convert.ToDecimal(txtFtrTempMax.Text.Trim());
            blFooter.TempMin = Convert.ToDecimal(txtFtrTempMin.Text.Trim());
            blFooter.TempUnit = Convert.ToString(rdoFtrTemperatureUnit.Text.Trim());
            blFooter.PCSTemp = Convert.ToString(rdoFtrTemperatureUnit.SelectedValue);

            footerDetails.Add(blFooter);
            ViewState[FOOTERDETAILS] = footerDetails;
            RefreshGridView();
            ClearFooterDetail();

            ViewState[EDITBLFOOTER] = "";
        }

        private void LoadForEdit(long BlId)
        {
            //=================== BL Header ===============================
            IBLHeader header = new ImportBLL().GetBLHeaderinformation(BlId);

            txtBillEntery.Text = header.BillOfEntryNo;
            ViewState[ISSUEPORTID] = header.BLIssuePortID;
            //txtCACode.Text = header.CACode;  -- Replaced on demand
            txtCMCode.Text = header.CargoMovement;
            rdoNatureCargo.SelectedValue = header.CargoNature;
            rdoCargoType.SelectedValue = header.CargoType;
            txtDestFreeDays.Text = Convert.ToString(header.DetentionFree);
            rdoDestSlab.SelectedValue = header.DetentionSlabType;

            if (header.DOLock == true)
                rdoLockDO.SelectedValue = "Yes";
            else
                rdoLockDO.SelectedValue = "No";

            txtLockDOComment.Text = header.DOLockReason;

            if (header.DPT == true)
                rdoDPT.SelectedValue = "Yes";
            else
                rdoDPT.SelectedValue = "No";

            ViewState[FINALDESTINATIONID] = header.FinalDestination;

            if (header.FreeOut == true)
                rdoFreeOut.SelectedValue = "Yes";
            else
                rdoFreeOut.SelectedValue = "No";

            rdoFrightType.SelectedValue = header.FreightType;
            txtFrightToCollect.Text = header.FreigthToCollect.ToString();
            txtGrossWeight.Text = header.GrossWeight.ToString();

            if (header.HazFlag == true)
                rdoHazardousCargo.SelectedValue = "Yes";
            else
                rdoHazardousCargo.SelectedValue = "No";

            txtIgmBLDate.Text = header.IGMBLDate.ToShortDateString();
            txtIgmBLNo.Text = header.IGMBLNumber;
            txtIMOCode.Text = header.IMOCode;
            txtLineBLDate.Text = header.ImpLineBLDate.ToShortDateString();
            txtLineBL.Text = header.ImpLineBLNo;
            ViewState[VESSELID] = header.ImpVesselID;
            //ddlVoyage.SelectedValue = header.ImpVoyageID.ToString();
            txtLINo.Text = header.ItemLineNo;
            txtLinePrefix.Text = header.ItemLinePrefix;
            txtSublineNo.Text = header.ItemSubLineNo;
            rdoItemType.SelectedValue = header.ItemType;
            rdoLineBLType.SelectedValue = header.LineBLType;
            ddlLocation.SelectedValue = header.LocationID.ToString();
            txtMLOCode.Text = header.MLOCode;
            ddlNvocc.SelectedValue = header.NVOCCID.ToString();
            txtFEU.Text = header.NoofFEU.ToString();
            txtTEU.Text = header.NoofTEU.ToString();
            txtPackage.Text = header.PackageDetail;

            if (header.PartBL == true)
                rdoPartBL.SelectedValue = "Yes";
            else
                rdoPartBL.SelectedValue = "No";

            txtPGRFreeDays.Text = header.PGR_FreeDays.ToString();
            ViewState[PORTOFDISCHARGEID] = header.PortDischarge;
            ViewState[PORTOFLOADINGID] = header.PortLoading;

            //ddlCHAid.SelectedValue = header.CHAId.ToString();

            ViewState["CHAID"] = header.CHAId;
            ((TextBox)AC_CHA1.FindControl("txtCha")).Text = new ImportBLL().GetCHAName(header.CHAId);

            if (header.Reefer == true)
                rdoReefer.SelectedValue = "Yes";
            else
                rdoReefer.SelectedValue = "No";

            ddlStockLocation.SelectedValue = header.StockLocationID.ToString();
            //ViewState[SURVEYORID] = header.SurveyorAddressID;
            //ddlSurveyor.SelectedValue = Convert.ToString(header.SurveyorAddressID);

            if (header.TaxExemption == true)
                rdoTaxExempted.SelectedValue = "Yes";
            else
                rdoTaxExempted.SelectedValue = "No";

            txtTPBondNo.Text = header.TPBondNo;
            rdoTransportMode.SelectedValue = header.TransportMode;
            ViewState[VOLUMEUNITID] = header.UnitOfVolume;
            ViewState[WEIGHTUNITID] = header.UnitOfWeight;
            ViewState[PACKAGEUNITID] = header.UnitPackage;
            txtUNOCode.Text = header.UNOCode;
            txtVolume.Text = header.Volume.ToString();
            txtWaiver.Text = header.WaiverPercent.ToString();
            rdoWaiverType.SelectedValue = header.WaiverType;
            txtTransinfo.Text = header.TranShipment;
            txtShipperAddr.Text = header.ShipperInformation;

            ((TextBox)AC_Shipper1.FindControl("txtShipper")).Text = header.ShipperName;
            int ShipperId = new ImportBLL().GetAddressId(header.ShipperName);
            ViewState[SHIPERADDRID] = ShipperId;

            txtMarks.Text = header.MarksNumbers;
            txtDescGoods.Text = header.GoodDescription;
            txtConsigneeAddr.Text = header.ConsigneeInformation;

            ((TextBox)AC_Consignee1.FindControl("txtConsignee")).Text = header.ConsigneeName;
            int ConsigneeId = new ImportBLL().GetAddressId(header.ConsigneeName);
            ViewState[CONSIGNEEEADDRID] = ConsigneeId;

            txtBLComment.Text = header.BLComment;

            ((TextBox)AC_NParty1.FindControl("txtNParty")).Text = header.NotifyName;
            int NotifyId = new ImportBLL().GetAddressId(header.NotifyName);
            ViewState[NPADDRID] = NotifyId;

            txtNotifyingPartyAddr.Text = header.NotifyPartyInformation;
            //header.dtAdded = DateTime.Now;
            //header.dtEdited = DateTime.Now;
            //header.RsStatus = true;
            //header.ONBR = true;
            //header.CompanyID = 1;
            //header.Billing = false;
            txtPackage.Text = header.NumberPackage.ToString();
            //header.WaiverDate = DateTime.Now;
            txtCommodity.Text = header.Commodity;
            txtLineBLVessel.Text = header.LineBLVesselDetail;
            //((TextBox)AC_CANotice1.FindControl("txtCANotice")).Text = header.CANTo;
            txtCargoArrivalNotice.Text = header.CANTo;
            //header.WaiverFk_UserID = UserId;
            //header.UserAdded = UserId;
            //header.UserEdited = UserId;
            hdnFilePath.Value = header.WaiverLetterUpload;
            rdoCfsNominated.SelectedValue = header.CFSNomination;
            ViewState[CFSADDRID] = header.AddressCFSId;
            ViewState[FRTPAYBLEATID] = header.PortFrtPayableID;
            //During add this will empty. During edit if the user changes detention free days or slab then his id will be stored here
            //header.DetChangedByID = Convert.ToInt32();
            ViewState[BLHEADERID] = header.BLID; ;

            ViewState[NPADDRID] = new ImportBLL().GetAddressId(header.NotifyName);
            ViewState[CONSIGNEEEADDRID] = new ImportBLL().GetAddressId(header.ConsigneeName);
            ViewState[SHIPERADDRID] = new ImportBLL().GetAddressId(header.ShipperName);

            //Auto Complete Population

            //Vessel/Voyage
            ((TextBox)AC_Vessel1.FindControl("txtVessel")).Text = new ImportBLL().GetVesselNameById(header.ImpVesselID);
            LoadVoyageDDL();
            ddlVoyage.SelectedValue = header.ImpVoyageID.ToString();

            //Final Dest
            ((TextBox)AC_Port4.FindControl("txtPort")).Text = new ImportBLL().GetPortNameById(header.FinalDestination);

            //Port of Discharge
            string dischargePort = new ImportBLL().GetPortNameById(header.PortDischarge);
            hdnPortDischarge.Value = dischargePort.Split('|')[0].Trim();
            ((TextBox)AC_Port3.FindControl("txtPort")).Text = dischargePort;

            //Port of Loading
            string loadingPort = new ImportBLL().GetPortNameById(header.PortLoading);
            hdnPortLoading.Value = loadingPort.Split('|')[0].Trim();
            ((TextBox)AC_Port2.FindControl("txtPort")).Text = loadingPort;

            //Issue Port
            ((TextBox)AC_Port1.FindControl("txtPort")).Text = new ImportBLL().GetPortNameById(header.BLIssuePortID);

            //Frieght Payable At
            ((TextBox)AC_Port5.FindControl("txtPort")).Text = new ImportBLL().GetPortNameById(header.PortFrtPayableID);

            if (header.FreightType == "TC")
            {
                ((TextBox)AC_Port5.FindControl("txtPort")).Enabled = true;
            }

            //CFS Code
            string cfsCode = new ImportBLL().GetCFSCodeById(header.AddressCFSId);
            DataTable dt = new ImportBLL().GetCFSName(cfsCode);

            txtCFSName.Text = cfsCode;

            if (dt != null && dt.Rows.Count > 0)
                ((TextBox)AC_CFSCode1.FindControl("txtCFSCode")).Text = Convert.ToString(dt.Rows[0]["AddrName"]);

            //Delivery To
            string deliveryTo = new ImportBLL().GetDeliveryToById(header.DPTId);
            ((TextBox)AC_DeliveryTo1.FindControl("txtDeliveryTo")).Text = deliveryTo;

            //Unit of Package/Weight/Volume
            if (header.UnitOfVolume != 0)
                ((TextBox)AC_WeightUnit1.FindControl("txtWeightUnit")).Text = new ImportBLL().GetUnitNameById(header.UnitOfWeight);
            if (header.UnitOfWeight != 0)
                ((TextBox)AC_PackageUnit1.FindControl("txtPkgUnit")).Text = new ImportBLL().GetUnitNameById(header.UnitPackage);
            if (header.UnitPackage != 0)
                ((TextBox)AC_VolumeUnit1.FindControl("txtVolUnit")).Text = new ImportBLL().GetUnitNameById(header.UnitOfVolume);

            //Surveyor
            //if (header.SurveyorAddressID != 0)
            //    ((TextBox)AC_Surveyor1.FindControl("txtSurveyor")).Text = new ImportBLL().GetSurveyorNameById(header.SurveyorAddressID);

            LoadSurveyorDDL();
            ddlSurveyor.SelectedValue = Convert.ToString(header.SurveyorAddressID);

            //================= BL Footer =========================
            List<IBLFooter> footers = new ImportBLL().GetBLFooterInfo(BlId);

            //ViewState["BLFooterID"] = footers[0].BLFooterID;
            ViewState[FOOTERDETAILS] = footers;

            gvwFooter.DataSource = footers;
            gvwFooter.DataBind();


            if (rdoCargoType.SelectedValue == "F" || rdoCargoType.SelectedValue == "E")
            {
                btnAddRow.Enabled = true;
            }
            else if (rdoCargoType.SelectedValue == "L" || rdoCargoType.SelectedValue == "N")
            {
                btnAddRow.Enabled = false;
            }
        }

        private void CheckContainerNumber()
        {
            bool IsValidContainer = false;
            bool LCLDuplicate = false;
            bool AllowForOnHire = true;
            bool IsPresentonBLFooter = new ImportBLL().IsDuplicateContainerNo(txtFtrContainerNo.Text.Trim());

            DataTable dtOnHireContainers = new ImportBLL().GetOnHireContainers(txtFtrContainerNo.Text.Trim());

            if (dtOnHireContainers != null && dtOnHireContainers.Rows.Count > 0)
            {
                int MoventOptionId = 0;

                if (dtOnHireContainers.Rows[0]["fk_MovementOptID"] != DBNull.Value)
                    MoventOptionId = Convert.ToInt32(dtOnHireContainers.Rows[0]["fk_MovementOptID"]);

                if (MoventOptionId != (int)Enums.ContainerMovement.LODF
                    || MoventOptionId != (int)Enums.ContainerMovement.LODE
                    || MoventOptionId != (int)Enums.ContainerMovement.OFFH
                    || MoventOptionId != 0)
                    AllowForOnHire = false;
            }

            if (rdoCargoType.SelectedValue == "L")
            {
                if (IsPresentonBLFooter)
                {
                    DataTable dtFooterContainers = new ImportBLL().GetContainerFromFooter(txtFtrContainerNo.Text.Trim(), Convert.ToInt64(ViewState[VESSELID]), Convert.ToInt64(ddlVoyage.SelectedValue));

                    if (dtFooterContainers != null && dtFooterContainers.Rows.Count > 0)
                    {
                        if (dtFooterContainers.Rows[0]["CntrNo"] != DBNull.Value)
                            LCLDuplicate = true;
                    }
                }
            }

            if (IsPresentonBLFooter)
            {
                if (AllowForOnHire)
                {
                    IsValidContainer = true;
                }
            }
            else
            {
                if (AllowForOnHire)
                {
                    IsValidContainer = true;
                }
            }

            ViewState["IsValidContainer"] = IsValidContainer;
            ViewState["LCLDuplicate"] = LCLDuplicate;
        }

        protected bool IsValidContainerNo()
        {
            bool IsValid = true;
            int total = 0;
            int step = 10;
            string containerNo = txtFtrContainerNo.Text.ToUpper();

            Dictionary<char, int> AlphabetCodes = new Dictionary<char, int>();
            List<int> PowerOfMultipliers = new List<int>();
            for (int i = 65; i < 91; i++)
            {
                char c = (char)i;
                int pos = i - 65 + step;
                if (c == 'A' || c == 'K' || c == 'U') //omit multiples of 11.
                    step += 1;
                AlphabetCodes.Add(c, pos); //add to dictionary
            }
            for (int i = 0; i < 10; i++)
            {
                int result = (int)Math.Pow(2, i); //power of 2 calculation.
                PowerOfMultipliers.Add(result); //add to list.
            }
            if (containerNo.Length == 11) //container numbers must be 11 characters long.
            {
                for (int i = 0; i < 10; i++) //loop through the first 10 characters (the 11th is the check digit!).
                {
                    if (AlphabetCodes.ContainsKey(containerNo[i])) //if the current character is in the dictionary.
                        total += (AlphabetCodes[containerNo[i]] * PowerOfMultipliers[i]); //add it's value to the total.
                    else
                    {
                        int serialNumber = (int)containerNo[i] - 48; //it must be a number, so get the number from the char ascii value.
                        total += (serialNumber * PowerOfMultipliers[i]); //and add it to the total.
                    }
                }
                int checkDigit = (int)total % 11; //this should give you the check digit
                //The check digit shouldn't equal 10 according to ISO best practice - BUT there are containers out there that do, so we'll
                //double check and set the check digit to 0...again according to ISO best practice.
                if (checkDigit == 10)
                    checkDigit = 0;
                if (checkDigit != (int)containerNo[10] - 48) //check digit should equal the last character in the textbox.
                {
                    //errContainer.Text = "Container Number NOT Valid";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "container", "<script>javascript:void alert('Container Number NOT Valid!');</script>", false);
                    IsValid = false;
                }
                else
                {
                    //errContainer.Text = "Container Number Valid";
                    IsValid = true;
                }
            }
            else
            {
                //errContainer.Text = "Container Number must be 11 characters in length";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "container", "<script>javascript:void alert('Container Number must be 11 characters in length!');</script>", false);
                IsValid = false;
            }
            return IsValid;
        }

        protected void txtFtrGrossWeight_TextChanged(object sender, EventArgs e)
        {
            if (txtFtrGrossWeight.Text != string.Empty)
            {
                //if (txtFtrCargoWt.Text == string.Empty)
                txtFtrCargoWt.Text = (Convert.ToDecimal(txtFtrGrossWeight.Text) / 1000).ToString();
            }
        }


    }
}