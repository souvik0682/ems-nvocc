using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EMS.Utilities;
using EMS.Utilities.ResourceManager;
using EMS.Common;

namespace EMS.WebApp.MasterModule
{
    public partial class AddEditVessel : System.Web.UI.Page
    {

        #region Private Member Variables

        private int _userId = 0;
        private bool _hasEditAccess = true;
        private string VesselId = "";
        #endregion


        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            IUser user = (IUser)Session[Constants.SESSION_USER_INFO];
            _userId = user == null ? 0 : user.Id;

            if (!IsPostBack)
            {
                VesselId = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);
                ClearText();
                BLL.DBInteraction dbinteract = new BLL.DBInteraction();
                GeneralFunctions.PopulateDropDownList(ddlVesselPrefix, dbinteract.PopulateDDLDS("mstVesselPrefix", "pk_VesselPrefixID", "VesselPrefix"));
                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageVessel.aspx','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
                LoadShip_Tax(_userId, dbinteract);
                if (VesselId != "-1")
                    LoadData(VesselId);
            }
        }

        private void LoadShip_Tax(int _userId, BLL.DBInteraction dbinteract)
        {
            System.Data.DataSet ds = dbinteract.GetShipLine_Tax(_userId);
            txtPan.Text = ds.Tables[0].Rows[0]["PANNo"].ToString();
            txtShipLineCode.Text = ds.Tables[0].Rows[0]["ShippingLineCode"].ToString();


        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            VesselId = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);
            SaveData(VesselId);

        }

        #endregion

        #region Private Methods

        private void LoadData(string VesselId)
        {
            ClearText();

            int intVesselId = 0;
            if (VesselId == "" || !Int32.TryParse(VesselId, out intVesselId))
                return;
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            System.Data.DataSet ds = dbinteract.GetVessel(Convert.ToInt32(VesselId), 0, "", 0);

            if (!ReferenceEquals(ds, null) && ds.Tables[0].Rows.Count > 0)
            {
                txtAgentCode.Text = ds.Tables[0].Rows[0]["AgentCode"].ToString();
                txtIMO.Text = ds.Tables[0].Rows[0]["IMONumber"].ToString();
                txtMasterCode.Text = ds.Tables[0].Rows[0]["MasterName"].ToString();
                //txtPan.Text = ds.Tables[0].Rows[0]["PAN"].ToString();
                //txtShipLineCode.Text = ds.Tables[0].Rows[0]["ShippingLineCode"].ToString();
                ((TextBox)AutoCompleteCountry1.FindControl("txtCountry")).Text = ds.Tables[0].Rows[0]["flag"].ToString();
                txtVesselName.Text = ds.Tables[0].Rows[0]["VesselName"].ToString();
                ddlVesselPrefix.SelectedValue = ds.Tables[0].Rows[0]["fk_VesselPrefixID"].ToString();
            }
        }
        private void ClearText()
        {
            txtAgentCode.Text = "";
            //  txtCallSign.Text = "";
            txtIMO.Text = "";
            // txtlastPort.Text = "";
            txtMasterCode.Text = "";
            txtPan.Text = "";
            txtShipLineCode.Text = "";
            // txtVesselFlag.Text = "";
            txtVesselName.Text = "";
        }
        private void SaveData(string VesselId)
        {
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();

            bool isedit = VesselId != "-1" ? true : false;
            if (!isedit)
                // if (dbinteract.GetPort(-1, txtPortCode.Text.Trim(), "").Tables[0].Rows.Count > 0)
                if (!dbinteract.IsUnique("trnVessel", "VesselName", txtVesselName.Text.Trim()))
                {
                    GeneralFunctions.RegisterAlertScript(this, "Vessel Name must be unique. Please try with another one.");
                    return;
                }


            Entity.Vessel vessel = new Entity.Vessel();
            vessel.VesselID = Convert.ToInt32(VesselId);
            vessel.AgentCode = txtAgentCode.Text;
            //vessel.CallSign = txtCallSign.Text;
            vessel.IMONumber = txtIMO.Text;
            //string port = ((TextBox)AutoCompletepPort1.FindControl("txtPort")).Text;
            //port = port.Contains(',') && port.Split(',').Length >= 1 ? port.Split(',')[1].Trim() : "";
            //vessel.LastPortCalled = dbinteract.GetId("Port", port);
            vessel.MasterName = txtMasterCode.Text;
            vessel.PANNo = txtPan.Text;
            vessel.ShippingLineCode = txtShipLineCode.Text;
            vessel.VesselFlag = dbinteract.GetId("Country", ((TextBox)AutoCompleteCountry1.FindControl("txtCountry")).Text);
            vessel.VesselName = txtVesselName.Text;
            vessel.VesselPrefix = Convert.ToInt32(ddlVesselPrefix.SelectedValue);

            //if (vessel.LastPortCalled == 0)
            //{
            //    GeneralFunctions.RegisterAlertScript(this, "Last_Port_Called is Invalid. Please rectify.");
            //    return;
            //}
            if (vessel.VesselFlag == 0)
            {
                GeneralFunctions.RegisterAlertScript(this, "Vessel_Flag is Invalid. Please rectify.");
                return;
            }
            else if (ddlVesselPrefix.SelectedValue == "0")
            {
                GeneralFunctions.RegisterAlertScript(this, "Please Select a vessel prefix first");
                return;
            }

            int result = dbinteract.AddEditVessel(_userId, isedit, vessel);


            if (result > 0)
            {
                Response.Redirect("~/MasterModule/ManageVessel.aspx");
            }
            else
            {
                GeneralFunctions.RegisterAlertScript(this, "Error Occured");
            }
        }


        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }
    }
}