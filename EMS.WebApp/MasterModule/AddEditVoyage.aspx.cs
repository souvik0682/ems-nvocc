using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities;
using EMS.Common;
using EMS.Utilities.ResourceManager;


namespace EMS.WebApp.MasterModule
{
    public partial class AddEditVoyage : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private bool _hasEditAccess = true;
        private string VoyageId = "";
        #endregion

        BLL.DBInteraction dbinteract = new BLL.DBInteraction();
        protected void Page_Load(object sender, EventArgs e)
        {
            _userId = EMS.BLL.UserBLL.GetLoggedInUserId();

            VoyageId = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);

            if (!IsPostBack)
            {

                GeneralFunctions.PopulateDropDownList(ddlLoc, dbinteract.PopulateDDLDS("DSR.dbo.mstLocation", "pk_LocID", "LocName", true), true);
                GeneralFunctions.PopulateDropDownList(ddlVessel, dbinteract.PopulateDDLDS("trnVessel", "pk_VesselID", "VesselName"));
                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('MangeVoyage.aspx','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
                if (VoyageId != "-1")
                    LoadData(VoyageId);
            }
            LandingDateCheck(dbinteract, VoyageId);
            //
        }

        private void LandingDateCheck(BLL.DBInteraction dbinteract, string VoyageId)
        {

            if (VoyageId != "-1")
            {
                //int c = dbinteract.PopulateDDLDS("ImpBLHeader", "fk_ImpVesselID", "fk_ImpVoyageID", "where fk_ImpVesselID=" + ddlVessel.SelectedValue + " and fk_ImpVoyageID='" + txtVoyageNo.Text.Trim()+"'").Tables[0].Rows.Count;
                //int c = dbinteract.CountLandDate(Convert.ToInt32(ddlVessel.SelectedValue), txtVoyageNo.Text.Trim());

                if (txtdtLand.Text.Trim() != "")
                {

                    txtExcRate.Text = hdntxtExcRate.Value = dbinteract.GetExchnageRate(Convert.ToDateTime(txtdtLand.Text)).ToString();

                    trLandDate.Style.Add("display", " ");
                }
                else
                {
                    trLandDate.Style.Add("display", "none");

                    txtExcRate.Text = hdntxtExcRate.Value = dbinteract.GetExchnageRate(DateTime.Today).ToString();

                }

            }
            else
            {
                trLandDate.Style.Add("display", "none");
                txtExcRate.Text = hdntxtExcRate.Value = dbinteract.GetExchnageRate(DateTime.Today).ToString();
                txtdtLand.ReadOnly = true;

            }
            if (VoyageId == "-1")
            {
                int cnt = EMS.BLL.VoyageBLL.IfExistInBL(ddlVessel.SelectedIndex > 0 ? Convert.ToInt32(ddlVessel.SelectedValue) : 0, Convert.ToInt32(VoyageId)).Rows.Count;
                if (cnt <= 0)
                {
                    txtdtLand.ReadOnly = true;
                    txtdtLand.Enabled = false;
                    dtLand_.Enabled = false;
                    txtdtLand.Style.Add("background-color", "#E6E6E6");
                }
            }
        }



        private void LoadData(string VoyageId)
        {
            int intVoyageId = 0;
            if (VoyageId == "" || !Int32.TryParse(VoyageId, out intVoyageId))
                return;
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            System.Data.DataSet ds = dbinteract.GetVoyage(Convert.ToInt32(VoyageId), "a", "", "", "");

            if (!ReferenceEquals(ds, null) && ds.Tables[0].Rows.Count > 0)
            {
                ddlLoc.SelectedValue = ds.Tables[0].Rows[0]["pk_LocID"].ToString();

                GeneralFunctions.PopulateDropDownList(ddlTerminalID, dbinteract.PopulateDDLDS("mstTerminal", "pk_TerminalID", "TerminalName", "Where fk_LocationID=" + ddlLoc.SelectedValue));
                ddlTerminalID.SelectedValue = ds.Tables[0].Rows[0]["fl_TerminalID"].ToString();
                txtAltLGNo.Text = ds.Tables[0].Rows[0]["AltLGNo"].ToString();
                txtCargoDesc.Text = ds.Tables[0].Rows[0]["CargoDesc"].ToString();
                txtExcRate.Text = ds.Tables[0].Rows[0]["ImpXChangeRate"].ToString();
                txtIGMNo.Text = ds.Tables[0].Rows[0]["IGMNo"].ToString();
                txtCallSign.Text = ds.Tables[0].Rows[0]["CallSign"].ToString();
                txtLGNo.Text = ds.Tables[0].Rows[0]["LGNo"].ToString();
                txtLightHouse.Text = ds.Tables[0].Rows[0]["LightHouseDue"].ToString();
                txtMotherDaughter.Text = ds.Tables[0].Rows[0]["MotherDaughterDtl"].ToString();
                txtPCCNo.Text = ds.Tables[0].Rows[0]["PCCNo"].ToString();
                txtTime.Text = ds.Tables[0].Rows[0]["ETATime"].ToString();
                txtTotLine.Text = ds.Tables[0].Rows[0]["TotalLines"].ToString();
                txtVCN.Text = ds.Tables[0].Rows[0]["VCN"].ToString();
                ddlVessel.SelectedValue = ds.Tables[0].Rows[0]["fk_VesselID"].ToString();
                txtVIA.Text = ds.Tables[0].Rows[0]["VIANo"].ToString();
                txtVoyageNo.Text = ds.Tables[0].Rows[0]["VoyageNo"].ToString();
                ddlCrewEffList.SelectedValue = ds.Tables[0].Rows[0]["CrewEffectList"].ToString();
                ddlCrewList.SelectedValue = ds.Tables[0].Rows[0]["CrewList"].ToString();
                ddlMaritime.SelectedValue = ds.Tables[0].Rows[0]["MaritimeList"].ToString();
                ddlPessengerList.SelectedValue = ds.Tables[0].Rows[0]["PassengerList"].ToString();
                ddlSameButton.SelectedValue = ds.Tables[0].Rows[0]["SameButtonCargo"].ToString();
                ddlShipStoreSubmitted.SelectedValue = ds.Tables[0].Rows[0]["ShipStoreSubmitted"].ToString();
                ddlTerminalID.SelectedValue = ds.Tables[0].Rows[0]["fl_TerminalID"].ToString();


                txtdtETA.Text = ds.Tables[0].Rows[0]["ETADate"].ToString().Split(' ')[0];
                txtdtLand.Text = ds.Tables[0].Rows[0]["AddLandingDate"].ToString().Split(' ')[0];
                txtDtIGM.Text = ds.Tables[0].Rows[0]["IGMDate"].ToString().Split(' ')[0];
                txtdtLand.Text = ds.Tables[0].Rows[0]["LandingDate"].ToString().Split(' ')[0];
                txtdtPCC.Text = ds.Tables[0].Rows[0]["PCCDate"].ToString().Split(' ')[0];
                //txtdtAddLand.Text = ds.Tables[0].Rows[0]["__"].ToString();
                ((TextBox)AutoCompletepPort1.FindControl("txtPort")).Text = ds.Tables[0].Rows[0]["lastport"].ToString();
                ((TextBox)AutoCompletepPort2.FindControl("txtPort")).Text = ds.Tables[0].Rows[0]["lastport1"].ToString();
                ((TextBox)AutoCompletepPort3.FindControl("txtPort")).Text = ds.Tables[0].Rows[0]["lastport2"].ToString();
                ((TextBox)AutoCompletepPort4.FindControl("txtPort")).Text = ds.Tables[0].Rows[0]["portDischarge"].ToString();

            }
        }

        protected void ddlLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string loc = ddlLoc.SelectedValue;
            GeneralFunctions.PopulateDropDownList(ddlTerminalID, dbinteract.PopulateDDLDS("mstTerminal", "pk_TerminalID", "TerminalName", "Where fk_LocationID=" + loc));
            //GeneralFunctions.PopulateDropDownList(ddlLoc, dbinteract.PopulateDDLDS("DSR.dbo.mstLocation", "pk_LocID", "LocName", true),false);
            //ddlLoc.SelectedValue = loc;
        }


        private void SaveData(string VoyageId)
        {
            throw new NotImplementedException();
        }

        private Entity.VoyageEntity AssignVoyageProp(string voyageID)
        {
            Entity.VoyageEntity voyage = new Entity.VoyageEntity();
            voyage.pk_VoyageID = Convert.ToInt32(voyageID);
            voyage.AltLGNo = txtAltLGNo.Text.ToUpper();
            voyage.CargoDesc = txtCargoDesc.Text.ToUpper();
            voyage.ImpXChangeRate = txtExcRate.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtExcRate.Text.Trim());
            voyage.IGMNo = txtIGMNo.Text.ToUpper();
            voyage.CallSign = txtCallSign.Text.ToUpper();
            voyage.LGNo = txtLGNo.Text.ToUpper();
            voyage.LightHouseDue = txtLightHouse.Text.Trim() == "" ? 0 : Convert.ToInt32(txtLightHouse.Text.Trim());
            voyage.MotherDaughterDtl = txtMotherDaughter.Text.ToUpper();
            voyage.PCCNo = txtPCCNo.Text.ToUpper();
            voyage.ETATime = txtTime.Text.ToUpper();
            voyage.TotalLines = txtTotLine.Text.ToUpper();
            voyage.VCN = txtVCN.Text.ToUpper();
            voyage.fk_VesselID = Convert.ToInt32(ddlVessel.SelectedValue);
            voyage.VIANo = txtVIA.Text.ToUpper();
            voyage.VoyageNo = txtVoyageNo.Text.ToUpper();
            voyage.CrewEffectList = ddlCrewEffList.SelectedValue;
            voyage.CrewList = ddlCrewList.SelectedValue;
            voyage.MaritimeList = ddlMaritime.SelectedValue;
            voyage.PassengerList = ddlPessengerList.SelectedValue;
            voyage.SameButtonCargo = ddlSameButton.SelectedValue;
            voyage.ShipStoreSubmitted = ddlShipStoreSubmitted.SelectedValue;
            voyage.fl_TerminalID = Convert.ToInt32(ddlTerminalID.SelectedValue);
            voyage.VesselType = ddlVesselType.SelectedValue;
            voyage.AddLandingDate = string.IsNullOrEmpty(txtdtAddLand.Text.Trim()) ? (Nullable<DateTime>)null : Convert.ToDateTime(txtdtAddLand.Text);
            voyage.ETADate = string.IsNullOrEmpty(txtdtETA.Text.Trim()) ? (Nullable<DateTime>)null : Convert.ToDateTime(txtdtETA.Text);
            voyage.IGMDate = string.IsNullOrEmpty(txtDtIGM.Text.Trim()) ? (Nullable<DateTime>)null : Convert.ToDateTime(txtDtIGM.Text);
            voyage.LandingDate = string.IsNullOrEmpty(txtdtLand.Text.Trim()) ? (Nullable<DateTime>)null : Convert.ToDateTime(txtdtLand.Text);
            voyage.PCCDate = string.IsNullOrEmpty(txtdtPCC.Text.Trim()) ? (Nullable<DateTime>)null : Convert.ToDateTime(txtdtPCC.Text);
            voyage.locid = Convert.ToInt32(ddlLoc.SelectedValue);
            string port = ((TextBox)AutoCompletepPort1.FindControl("txtPort")).Text;
            port = port.Contains(',') && port.Split(',').Length >= 1 ? port.Split(',')[1].Trim() : "";
            voyage.fk_LPortID = dbinteract.GetId("Port", port);
            if (port != "HALDIA,INHAL1")
                voyage.AddLandingDate = null;

            port = ((TextBox)AutoCompletepPort2.FindControl("txtPort")).Text;
            port = port.Contains(',') && port.Split(',').Length >= 1 ? port.Split(',')[1].Trim() : "";
            voyage.fk_LPortID1 = dbinteract.GetId("Port", port);

            port = ((TextBox)AutoCompletepPort3.FindControl("txtPort")).Text;
            port = port.Contains(',') && port.Split(',').Length >= 1 ? port.Split(',')[1].Trim() : "";
            voyage.fk_LPortID2 = dbinteract.GetId("Port", port);

            port = ((TextBox)AutoCompletepPort4.FindControl("txtPort")).Text;
            port = port.Contains(',') && port.Split(',').Length >= 1 ? port.Split(',')[1].Trim() : "";
            voyage.fk_Pod = dbinteract.GetId("Port", port);

            return voyage;
        }

        protected void btnSave_Click1(object sender, EventArgs e)
        {
            VoyageId = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);
            bool isedit = VoyageId != "-1" ? true : false;
            Entity.VoyageEntity voyage = AssignVoyageProp(VoyageId);
            if (ddlLoc.SelectedIndex == 0)
            {
                GeneralFunctions.RegisterAlertScript(this, "Please select any location");
                return;
            }
            if (voyage.fk_LPortID == 0)
            {
                GeneralFunctions.RegisterAlertScript(this, "Please provide Last_Port_Called");
                return;
            }

            if (voyage.fk_Pod == 0)
            {
                GeneralFunctions.RegisterAlertScript(this, "Please provide PoD");
                return;
            }

            if (txtdtLand.Text != "")
            {
                string msg = EMS.BLL.VoyageBLL.CheckVoyageEntryAbilty(voyage.fk_VesselID, txtVoyageNo.Text.Trim(), voyage.fk_Pod, voyage.LandingDate, isedit);
                //int c = dbinteract.PopulateDDLDS("trnVoyage", "fk_VesselID", "VoyageNo", "where fk_VesselID=" + ddlVessel.SelectedValue + " and VoyageNo='" + txtVoyageNo.Text.Trim() + "'").Tables[0].Rows.Count;
                if (msg != "" && msg.ToLower()!="true")
                {

                    GeneralFunctions.RegisterAlertScript(this, msg);
                    return;


                }
            }
            

            int result = dbinteract.AddEditVoyage(_userId, isedit, voyage);
            int result1 = EMS.BLL.VoyageBLL.VoyageLandingDateEntry(voyage.fk_VesselID, Convert.ToInt32(VoyageId), voyage.fk_Pod, voyage.LandingDate, _userId);

            if (result > 0)
            {
                if (!isedit)
                    Response.Redirect("~/MasterModule/MangeVoyage.aspx");
                else if (result1 > 0)
                    Response.Redirect("~/MasterModule/MangeVoyage.aspx");
                else
                {
                    GeneralFunctions.RegisterAlertScript(this, "Error Occured");
                }
            }
            else
            {
                GeneralFunctions.RegisterAlertScript(this, "Error Occured");
            }
        }


    }
}