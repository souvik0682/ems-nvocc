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
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;

        #endregion


        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            //_userId = EMS.BLL.UserBLL.GetLoggedInUserId();
           

            if (!IsPostBack)
            {
                VesselId = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);
                ClearText();
                BLL.DBInteraction dbinteract = new BLL.DBInteraction();
                GeneralFunctions.PopulateDropDownList(ddlVesselPrefix, dbinteract.PopulateDDLDS("mstVesselPrefix", "pk_VesselPrefixID", "VesselPrefix"));
                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageVessel.aspx','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
               
                if (VesselId != "-1")
                    LoadData(VesselId);
                else
                    LoadShip_Tax(_userId, dbinteract);
            }
            RetriveParameters();
            CheckUserAccess(VesselId);
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

        private void RetriveParameters()
        {
            _userId = EMS.BLL.UserBLL.GetLoggedInUserId();

            //Get user permission.
            EMS.BLL.UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
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

                btnSave.Visible=true;

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
                txtPan.Text = ds.Tables[0].Rows[0]["PAN"].ToString();
                txtShipLineCode.Text = ds.Tables[0].Rows[0]["ShippingLineCode"].ToString();
                ((TextBox)AutoCompleteCountry1.FindControl("txtCountry")).Text = ds.Tables[0].Rows[0]["flag"].ToString();
                txtVesselName.Text = ds.Tables[0].Rows[0]["VesselName"].ToString();
                TxtCallSign.Text = ds.Tables[0].Rows[0]["CallSign"].ToString();
                ddlVesselPrefix.SelectedValue = ds.Tables[0].Rows[0]["fk_VesselPrefixID"].ToString();
                txtVesselAbbr.Text = ds.Tables[0].Rows[0]["VesselAbbr"].ToString();
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
            txtVesselAbbr.Text = "";
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
            vessel.AgentCode = txtAgentCode.Text.ToUpper();
            //vessel.CallSign = txtCallSign.Text;
            vessel.IMONumber = txtIMO.Text.ToUpper();
            //string port = ((TextBox)AutoCompletepPort1.FindControl("txtPort")).Text;
            //port = port.Contains(',') && port.Split(',').Length >= 1 ? port.Split(',')[1].Trim() : "";
            //vessel.LastPortCalled = dbinteract.GetId("Port", port);
            vessel.MasterName = txtMasterCode.Text.ToUpper();
            vessel.PANNo = txtPan.Text.ToUpper();
            vessel.ShippingLineCode = txtShipLineCode.Text.ToUpper();
            vessel.VesselFlag = dbinteract.GetId("Country", ((TextBox)AutoCompleteCountry1.FindControl("txtCountry")).Text);
            vessel.VesselName = txtVesselName.Text.ToUpper(); 
            vessel.VesselPrefix = Convert.ToInt32(ddlVesselPrefix.SelectedValue);
            vessel.CallSign = TxtCallSign.Text.ToUpper();
            vessel.VesselAbbr = txtVesselAbbr.Text.ToUpper();

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