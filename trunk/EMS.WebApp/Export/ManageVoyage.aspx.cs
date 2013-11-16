using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.BLL;
using EMS.Common;
using EMS.Entity;
using EMS.Utilities;
using System.Data;
using EMS.Utilities.ResourceManager;


namespace EMS.WebApp.Export
{
    public partial class ManageVoyage : System.Web.UI.Page
    {
        private int _userId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        BLL.DBInteraction dbinteract = new BLL.DBInteraction();

        protected void Page_Load(object sender, EventArgs e)
        {
            //GeneralFunctions.PopulateDropDownList(ddlVessel, dbinteract.PopulateDDLDS("trnVessel", "pk_VesselID", "VesselName"));
            RetriveParameters();
            if (!Page.IsPostBack)
            {
                LoadLocationDDL();
                LoadVesselDDL();
                LoadTerminalDDL();
                if (!ReferenceEquals(Request.QueryString["VoyageID"], null))
                {
                    long voyageId = 0;
                    Int64.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["VoyageID"].ToString()), out voyageId);
                    lblSailingDate.Visible = false;
                    txtSailingDateTime.Visible = false;
                    if (voyageId > 0)
                    {
                        LoadForEdit(voyageId);
                        lblSailingDate.Visible = true;
                        txtSailingDateTime.Visible = true;
                    }
                }
            }

        }
        private void LoadForEdit(long voyageId)
        {
                IexpVoyage voyage = new expVoyageBLL().GetVoyageById(voyageId);
                ViewState["VoyageID"] = voyage.VoyageID;           
                //txtGatewayPort.Text= voyage.GatewayPort.ToString();
                ddlVessel.SelectedValue=voyage.VesselID.ToString();
                ddlLocation.SelectedValue = voyage.LocationID.ToString();
                hdnPOL.Value = voyage.POL.ToString();
                hdnNextPortCall.Value = voyage.NextPortID.ToString();
                //txtPOD.Text= voyage.POD.ToString();
                txtVoyageNo.Text= voyage.VoyageNo;
                txtAgentcode.Text = voyage.AgentCode.ToString();
                //txtAgentcode.Text = voyage.AgentCode.ToString();
                txtPOL.Text = voyage.LoadPort.ToString();
                txtLinecode.Text=voyage.LineCode;
                txtETD.Text = voyage.ETD.ToString();
                txtPCCNo.Text= voyage.PCCNo;
                ddlTerminalID.SelectedValue = voyage.TerminalID.ToString();
                ddlLocation.SelectedValue = voyage.LocationID.ToString();
                txtPCCDate.Text = voyage.PCCDate.ToString();
                txtNextportcall.Text = voyage.NextPort.ToString();
                txtETA.Text = voyage.ETA.ToString();
                txtETANextPort.Text = voyage.ETANextPort.ToString();
                txtVIA.Text= voyage.VIA;
                txtVCNNo.Text= voyage.VCNNo;
                txtRotationNo.Text= voyage.RotationNo;
                txtSailingDateTime.Text = voyage.SailDate.ToString();
                txtRotationDate.Text = voyage.RotationDate.ToString();
                txtVesselcutoffDate.Text = voyage.VesselCutOffDate.ToString();
                txtlstsiDockscutoffDate.Text = voyage.DocsCutOffDate.ToString();                      
        }
        public void loaddata()
        {
            //expVoyageEntity expvoyage = (expVoyageEntity)expVoyageBLL.GetImportHaulage(Convert.ToInt32(hdnHaulageChrgID.Value));
        }
        private void LoadVesselDDL()
        {
            DataTable dt = new expVoyageBLL().GetVessels(); // Convert.ToInt64(ddlLocation.SelectedValue));

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dr["pk_VesselID"] = "0";
                dr["VesselName"] = "--Select--";
                dt.Rows.InsertAt(dr, 0);
                ddlVessel.DataValueField = "pk_VesselID";
                ddlVessel.DataTextField = "VesselName";
                ddlVessel.DataSource = dt;
                ddlVessel.DataBind();
            }

        }

        private void LoadLocationDDL()
        {
            GeneralFunctions.PopulateDropDownList(ddlLocation, dbinteract.PopulateDDLDS("DSR.dbo.mstLocation", "pk_LocID", "LocName", true), false);
            //GeneralFunctions.PopulateDropDownList(ddlTerminalID, dbinteract.PopulateDDLDS("dsr.dbo.mstLocation", "pk_LocationID", "LocationName", ""));
            //DataTable dt = new expVoyageBLL().Getlocation(); // Convert.ToInt64(ddlLocation.SelectedValue));

            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    DataRow dr = dt.NewRow();
            //    dr["pk_LocationID"] = "0";
            //    dr["LocationName"] = "--Select--";
            //    dt.Rows.InsertAt(dr, 0);
            //    ddlLocation.DataValueField = "pk_LocationID";
            //    ddlLocation.DataTextField = "LocationName";
            //    ddlLocation.DataSource = dt;
            //    ddlLocation.DataBind();
            //}

        }

        private void LoadTerminalDDL()
        {
            try
            {
                DataTable dt = new expVoyageBLL().GetTerminals(0); // Convert.ToInt64(ddlLocation.SelectedValue));

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["pk_TerminalID"] = "0";
                    dr["TerminalName"] = "--Select--";
                    dt.Rows.InsertAt(dr, 0);
                    ddlTerminalID.DataValueField = "pk_TerminalID";
                    ddlTerminalID.DataTextField = "TerminalName";
                    ddlTerminalID.DataSource = dt;
                    ddlTerminalID.DataBind();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();
            //Get user permission.
            UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                //string misc = string.Empty;
                IexpVoyage voyage = new expVoyageEntity();
                BuildInvoiceEntity(voyage);
                if (voyage.POL == 0)
                {
                    GeneralFunctions.RegisterAlertScript(this, "Please provide Port of Loading");
                    return;
                }

                if (voyage.NextPortID == 0)
                {
                    GeneralFunctions.RegisterAlertScript(this, "Please provide Next Port Call");
                    return;
                }

                if (voyage.ETA > voyage.ETD)
                {
                    GeneralFunctions.RegisterAlertScript(this, "ETA should be less than Equals ETD");
                    return;
                }

                if (voyage.ETD > voyage.ETANextPort)
                {
                    GeneralFunctions.RegisterAlertScript(this, "ETD should be less than ETA next port");
                    return;
                }

                if (voyage.VesselCutOffDate.ToString() != "" && voyage.ETD < voyage.VesselCutOffDate)
                {
                    GeneralFunctions.RegisterAlertScript(this, "ETD should be greater than Vessel Cut off Date");
                    return;
                }

                bool isedit = false;
                long qrystrvoyageid = long.Parse(GeneralFunctions.DecryptQueryString(Request.QueryString["VoyageID"].ToString()) != "" ?
                    GeneralFunctions.DecryptQueryString(Request.QueryString["VoyageID"].ToString()) : "0");
                //Add-Update
                //false for insert and true for update
                if (qrystrvoyageid.Equals(-1)) isedit = false;
                else isedit = true;
                long voyageid = new expVoyageBLL().SaveVoyage(voyage, isedit);               
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('Record saved successfully!');</script>", false);
                if (isedit==true)
                    Response.Redirect("~/Export/Voyage.aspx");
                else
                    clearPage();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('Record could not be saved due to ! " + ex.Message.ToString() + "');</script>", false);   
            }
        }
        private void clearPage()
        {
                //txtGatewayPort.Text=string.Empty;
                ddlVessel.SelectedIndex=0;
                //txtPOD.Text=string.Empty;
                txtVoyageNo.Text = string.Empty;
                txtAgentcode.Text=string.Empty;
                txtPOL.Text=string.Empty;
                txtLinecode.Text=string.Empty;
                txtETD.Text=string.Empty;
                txtPCCNo.Text=string.Empty;
                ddlTerminalID.SelectedValue="0";                
                txtPCCDate.Text=string.Empty;
                txtNextportcall.Text=string.Empty;
                txtETA.Text=string.Empty;
                txtETANextPort.Text = string.Empty;
                txtVIA.Text=string.Empty;
                txtVCNNo.Text=string.Empty;
                txtRotationNo.Text=string.Empty;
                txtSailingDateTime.Text=string.Empty;
                txtRotationDate.Text=string.Empty;
                txtVesselcutoffDate.Text=string.Empty;
                txtlstsiDockscutoffDate.Text=string.Empty;
        }
        private void BuildInvoiceEntity(IexpVoyage voyage)
        {           
            if (ViewState["VoyageID"] == null)                
            voyage.VoyageID = 0;
            else
            voyage.VoyageID = Convert.ToInt64(ViewState["VoyageID"]);
            //voyage.GatewayPort =Convert.ToInt32(hdnGateWayPort.Value);
            voyage.VesselID = Convert.ToInt32(ddlVessel.SelectedValue);
            //voyage.POD = Convert.ToInt32(hdnPOD.Value);
            voyage.VoyageNo = Convert.ToString(txtVoyageNo.Text);
            voyage.AgentCode = Convert.ToString(txtAgentcode.Text);
            voyage.POL = Convert.ToInt32(hdnPOL.Value);
            voyage.LineCode = Convert.ToString(txtLinecode.Text);
            voyage.ETD = string.IsNullOrEmpty(txtETD.Text.Trim()) ? (Nullable<DateTime>)null : Convert.ToDateTime(txtETD.Text);
            voyage.PCCNo = Convert.ToString(txtPCCNo.Text);
            voyage.TerminalID = Convert.ToInt32(ddlTerminalID.SelectedValue);
            voyage.LocationID = Convert.ToInt32(ddlLocation.SelectedValue);
            voyage.PCCDate = string.IsNullOrEmpty(txtPCCDate.Text.Trim()) ? (Nullable<DateTime>)null : Convert.ToDateTime(txtPCCDate.Text);
            voyage.NextPortID = Convert.ToInt32(hdnNextPortCall.Value);
            voyage.ETA = string.IsNullOrEmpty(txtETA.Text.Trim()) ? (Nullable<DateTime>)null : Convert.ToDateTime(txtETA.Text);
            voyage.VIA = Convert.ToString(txtVIA.Text);
            voyage.ETANextPort = string.IsNullOrEmpty(txtETANextPort.Text.Trim()) ? (Nullable<DateTime>)null : Convert.ToDateTime(txtETANextPort.Text);
            voyage.VCNNo = Convert.ToString(txtVCNNo.Text);
            voyage.RotationNo = Convert.ToString(txtRotationNo.Text);

            voyage.SailDate = string.IsNullOrEmpty(txtSailingDateTime.Text.Trim()) ? (Nullable<DateTime>)null : Convert.ToDateTime(txtSailingDateTime.Text);
            voyage.RotationDate = string.IsNullOrEmpty(txtRotationDate.Text.Trim()) ? (Nullable<DateTime>)null : Convert.ToDateTime(txtRotationDate.Text);
            voyage.VesselCutOffDate = string.IsNullOrEmpty(txtVesselcutoffDate.Text.Trim()) ? (Nullable<DateTime>)null : Convert.ToDateTime(txtVesselcutoffDate.Text);
            voyage.DocsCutOffDate = string.IsNullOrEmpty(txtlstsiDockscutoffDate.Text.Trim()) ? (Nullable<DateTime>)null : Convert.ToDateTime(txtlstsiDockscutoffDate.Text);     
            voyage.dtAdded = DateTime.Now;                      
            voyage.dtEdited = DateTime.Now;
            voyage.UserEdited = _userId;
            voyage.UserAdded = _userId;          

        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Export/Voyage.aspx");
        }

        protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string loc = ddlVessel.SelectedValue;
            //int loc1 = ddlVessel.SelectedIndex;
            //GeneralFunctions.PopulateDropDownList(ddlTerminalID, dbinteract.PopulateDDLDS("mstTerminal", "pk_TerminalID", "TerminalName] + '-' + [terminal", "Where fk_LocationID=" + loc));
            //GeneralFunctions.PopulateDropDownList(ddlLoc, dbinteract.PopulateDDLDS("DSR.dbo.mstLocation", "pk_LocID", "LocName", true),false);
            //ddlLoc.SelectedValue = loc;
        }

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            string loc = ddlLocation.SelectedValue;
            GeneralFunctions.PopulateDropDownList(ddlTerminalID, dbinteract.PopulateDDLDS("mstTerminal", "pk_TerminalID", "TerminalName] + '-' + [terminal", "Where fk_LocationID=" + loc));
            //GeneralFunctions.PopulateDropDownList(ddlLoc, dbinteract.PopulateDDLDS("DSR.dbo.mstLocation", "pk_LocID", "LocName", true),false);
            //ddlLoc.SelectedValue = loc;
        }
    }
}