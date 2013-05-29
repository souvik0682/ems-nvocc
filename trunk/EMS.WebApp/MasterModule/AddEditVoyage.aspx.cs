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
        private string _PrevPage = string.Empty;
        private DateTime OldLandingDate;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        #endregion

        BLL.DBInteraction dbinteract = new BLL.DBInteraction();
        protected void Page_Load(object sender, EventArgs e)
        {
            _userId = EMS.BLL.UserBLL.GetLoggedInUserId();
            RetriveParameters();
            VoyageId = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);

            if (!IsPostBack)
            {

                GeneralFunctions.PopulateDropDownList(ddlLoc, dbinteract.PopulateDDLDS("DSR.dbo.mstLocation", "pk_LocID", "LocName", true), true);
                GeneralFunctions.PopulateDropDownList(ddlVessel, dbinteract.PopulateDDLDS("trnVessel", "pk_VesselID", "VesselName"));
                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('MangeVoyage.aspx?p=" + Request.QueryString["p"] + "','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
                if (VoyageId != "-1")
                {
                    //btnSave.OnClientClick = "javascript:return confirm('All Containers of the voyage will be deleted. Confirm with Yes');";
                    btnSave.OnClientClick = "javascript:return CheckForDeletion();";
                    LoadData(VoyageId);

                    if (_PrevPage == "import")
                    {
                        DisableFields();
                    }

                }
                else
                    LandingDateCheck(dbinteract, VoyageId);
            }
            else
                LandingDateCheck(dbinteract, VoyageId);
            CheckUserAccess(VoyageId);
        }

        private void LandingDateCheck(BLL.DBInteraction dbinteract, string VoyageId)
        {
            Decimal ERate;

            if (VoyageId != "-1")
            {


                if (txtdtLand.Text.Trim() != "")
                {
                    ERate = dbinteract.GetExchnageRate(Convert.ToDateTime(txtdtLand.Text));

                    //txtExcRate.Text = hdntxtExcRate.Value = dbinteract.GetExchnageRate(Convert.ToDateTime(txtdtLand.Text)).ToString();

                    trLandDate.Style.Add("display", " ");
                }
                else
                {
                    trLandDate.Style.Add("display", "none");

                    ERate = dbinteract.GetExchnageRate(DateTime.Today);

                }

                if (decimal.Parse(txtExcRate.Text) < ERate)
                {
                    string msg;
                    msg = "Rate should be Greater than Today's EXchange Rate ";
                    {
                        GeneralFunctions.RegisterAlertScript(this, msg);
                        return;
                    }
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

        void DisableFields()
        {

            ddlLoc.Enabled = false;
            ddlVessel.Enabled = false;
            AutoCompletepPort4.Enabled = false;
            ddlVesselType.Enabled = false;
            txtTotLine.Enabled = false;
            txtCallSign.Enabled = false;
            txtdtETA.Enabled = false;
            txtTime.Enabled = false;
            ddlSameButton.Enabled = false;
            ddlCrewList.Enabled = false;
            ddlCrewEffList.Enabled = false;
            txtPCCNo.Enabled = false;
            txtVIA.Enabled = false;
            txtCargoDesc.Enabled = false;
            txtLGNo.Enabled = false;
            txtExcRate.Enabled = false;
            txtVoyageNo.Enabled = false;
            ddlTerminalID.Enabled = false;
            AutoCompletepPort1.Enabled = false;
            AutoCompletepPort2.Enabled = false;
            AutoCompletepPort3.Enabled = false;
            txtLightHouse.Enabled = false;
            ddlShipStoreSubmitted.Enabled = false;
            ddlPessengerList.Enabled = false;
            ddlMaritime.Enabled = false;
            txtdtPCC.Enabled = false;
            txtVCN.Enabled = false;
            txtMotherDaughter.Enabled = false;
            txtdtAddLand.Enabled = false;
            txtAltLGNo.Enabled = false;
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

                GeneralFunctions.PopulateDropDownList(ddlTerminalID, dbinteract.PopulateDDLDS("mstTerminal", "pk_TerminalID", "TerminalName] + '-' + [terminal", "Where fk_LocationID=" + ddlLoc.SelectedValue));
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
                hdnLandingDT.Value = ds.Tables[0].Rows[0]["LandingDate"].ToString().Split(' ')[0]; //Rajen
                hdnOldLandingDT.Value=ds.Tables[0].Rows[0]["LandingDate"].ToString().Split(' ')[0];
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
            GeneralFunctions.PopulateDropDownList(ddlTerminalID, dbinteract.PopulateDDLDS("mstTerminal", "pk_TerminalID", "TerminalName] + '-' + [terminal", "Where fk_LocationID=" + loc));
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
            //voyage.OLandingDate = Convert.ToDateTime(hdnOldLandingDT.Value);
            voyage.OLandingDate = string.IsNullOrEmpty(hdnOldLandingDT.Value.Trim()) ? (Nullable<DateTime>)null : Convert.ToDateTime(hdnOldLandingDT.Value);
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

            //if (isedit && OldLandingDate!=txtdtLand.Text && OldLandingDate != System.Convert.DBNull)
            //{
                
            //    //GeneralFunctions.RegisterAlertScript(this, "All Container transaction will be deleted");

            //    //  "javascript:return confirm('All Containers of the voyage will be deleted. Confirm with Yes');";
                
            //    //if (retval!="yes") return;

           
                
            //}

            if (txtdtLand.Text != "")
            {
                string msg = EMS.BLL.VoyageBLL.CheckVoyageEntryAbilty(voyage.fk_VesselID, txtVoyageNo.Text.Trim(), voyage.fk_Pod, voyage.LandingDate, isedit);
                //int c = dbinteract.PopulateDDLDS("trnVoyage", "fk_VesselID", "VoyageNo", "where fk_VesselID=" + ddlVessel.SelectedValue + " and VoyageNo='" + txtVoyageNo.Text.Trim() + "'").Tables[0].Rows.Count;
                if (msg != "" && msg.ToLower() != "true")
                {

                    GeneralFunctions.RegisterAlertScript(this, msg);
                    return;


                }
            }


            int result = dbinteract.AddEditVoyage(_userId, isedit, voyage);
            int result1 = EMS.BLL.VoyageBLL.VoyageLandingDateEntry(voyage.fk_VesselID, Convert.ToInt32(VoyageId), voyage.fk_Pod, voyage.LandingDate, voyage.OLandingDate, _userId);

            if (result > 0)
            {
                //if (!isedit)
                Response.Redirect("~/MasterModule/MangeVoyage.aspx?p=" + Request.QueryString["p"]);

                //else if (result1 > 0)
                //    Response.Redirect("~/MasterModule/MangeVoyage.aspx");
                //else
                //{
                //    GeneralFunctions.RegisterAlertScript(this, "Error Occured");
                //}
            }
            else
            {
                GeneralFunctions.RegisterAlertScript(this, "Error Occured");
            }
        }

        private void RetriveParameters()
        {
            if (ReferenceEquals(Request.QueryString["p"], null))
            {
                Response.Redirect("~/View/Home.aspx");
            }
            else
            {
                _PrevPage = GeneralFunctions.DecryptQueryString(Request.QueryString["p"]);
            }
            _userId = EMS.BLL.UserBLL.GetLoggedInUserId();

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

    }
}