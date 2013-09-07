using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EMS.Common;
using EMS.BLL;
using EMS.Entity;
using EMS.Utilities;
using EMS.Utilities.ResourceManager;


namespace EMS.WebApp.Export
{
    public partial class ManageBooking : System.Web.UI.Page
    {
        BookingEntity oBookingEntity;
        BookingBLL oBookingBll;
        UserEntity oUserEntity;


        #region Private Member Variables

        private int _userId = 0;
        private int _locId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private int _CompanyId = 1;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            RetriveParameters();
            pnlContainer.Visible = false;
            pnlTransit.Visible = false;
            if (!Page.IsPostBack)
            {
                fillAllDropdown();
                txtImo.Enabled = false;
                txtUno.Enabled = false;
                txtTempMax.Enabled = false;
                txtTempMin.Enabled = false;
                if (hdnBookingID.Value != "0")
                    LoadData();
            }
            CheckUserAccess(hdnBookingID.Value);
        }

        //private void CheckUserAccess()
        //{
        //    if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
        //    {
        //        IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

        //        if (ReferenceEquals(user, null) || user.Id == 0)
        //        {
        //            Response.Redirect("~/Login.aspx");
        //        }

        //        if (user.UserRole.Id != (int)UserRole.Admin)
        //        {
        //            Response.Redirect("~/Unauthorized.aspx");
        //        }
        //    }
        //    else
        //    {
        //        Response.Redirect("~/Login.aspx");
        //    }
        //}

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

        void fillAllDropdown()
        {
            ListItem Li = null;
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();

            #region Location
            //
            PopulateDropDown((int)Enums.DropDownPopulationFor.Location, ddlLocation, 0);
            //Li = new ListItem("SELECT", "0");
            //ddlLocation.Items.Insert(0, Li);
            //Li = new ListItem("ALL", "-1");
            //ddlLocation.Items.Insert(1, Li);
            #endregion

            #region ShipmentType
            foreach (Enums.ShipmentType r in Enum.GetValues(typeof(Enums.ShipmentType)))
            {
                Li = new ListItem("SELECT", "0");
                ListItem item = new ListItem(Enum.GetName(typeof(Enums.ShipmentType), r).Replace('_', ' '), ((int)r).ToString());
                ddlShipmentType.Items.Add(item);
            }
            ddlShipmentType.Items.Insert(0, Li);
            #endregion

            #region Line

            PopulateDropDown((int)Enums.DropDownPopulationFor.Line, ddlNvocc, 0);
            //Li = new ListItem("NA", "0");
            //ddlNvocc.Items.Insert(0, Li);
            #endregion

            #region Party Master

            //Li = new ListItem("ALL", "-1");
            //GeneralFunctions.PopulateDropDownList(ddlBookingParty, dbinteract.PopulateDDLDS("DSR.dbo.mstCustomer", "pk_CustID", "CustName", true), false);
            GeneralFunctions.PopulateDropDownList(ddlBookingParty, dbinteract.PopulateDDLDS("dsr.dbo.mstCustomer", "pk_CustID", "CustName", "Where Active='Y' and (CorporateorLocal='C' OR fk_LocID=" + ddlLocation.SelectedValue, true), false);
            //PopulateDropDown((int)Enums.DropDownPopulationFor.ExpBookingParty, ddlBookingParty, 0);
            //ddlBookingParty.Items.Insert(0, Li);

            #endregion


            //#region Services
            //GeneralFunctions.PopulateDropDownList(ddlService, dbinteract.PopulateDDLDS("exp.mstServices", "pk_ServiceID", "ServiceName", "Where ServiceStatus=1 AND fpod=" + hdnFPOD.Value + " AND fk_LineID=" + ddlNvocc.SelectedValue));
            ////Li = new ListItem("ALL", "-1");
            ////PopulateDropDown((int)Enums.DropDownPopulationFor.Services, ddlService, 0);
            ////ddlService.Items.Insert(0, Li);

            //#endregion

        }
        private void LoadData()
        {
            BookingEntity oBooking = (BookingEntity)BookingBLL.GetBooking(Convert.ToInt32(hdnBookingID.Value));

            //ddlFromLocation.SelectedIndex = Convert.ToInt32(ddlFromLocation.Items.IndexOf(ddlFromLocation.Items.FindByValue(oImportHaulage.LocationFrom)));
            hdnFPOD.Value = oBooking.FPODID.ToString();
            hdnPOD.Value = oBooking.PODID.ToString();
            hdnPOL.Value = oBooking.POLID.ToString();
            hdnPOR.Value = oBooking.PORID.ToString();
            hdnVessel.Value = oBooking.VesselID.ToString();
            hdnMainLineVessel.Value = oBooking.MainLineVesselID.ToString();

            
            ddlNvocc.SelectedValue = oBooking.LinerID.ToString();
            ddlService.SelectedValue = oBooking.ServicesID.ToString();
            ddlBookingParty.SelectedValue = oBooking.CustID.ToString();
            ddlLoadingVoyage.SelectedValue = oBooking.VoyageID.ToString();
            ddlMainLineVoyage.SelectedValue = oBooking.MainLineVoyageID.ToString();
            ddlShipmentType.SelectedIndex = ddlShipmentType.Items.IndexOf(ddlShipmentType.Items.FindByValue(oBooking.ShipmentType.ToString()));
            txtAccounts.Text = oBooking.Accounts.ToString();
            txtBookingDate.Text = oBooking.BookingDate.ToShortDateString();
            txtBookingNo.Text = oBooking.BookingNo.ToString();
            txtCommodity.Text = oBooking.Commodity.ToString();
            txtGrossSeight.Text = oBooking.GrossWt.ToString();
            txtCbm.Text = oBooking.CBM.ToString();
            txtFPOD.Text = oBooking.FPOD.ToString();
            txtPOD.Text = oBooking.POD.ToString();
            txtPOL.Text = oBooking.POL.ToString();
            txtPOR.Text = oBooking.POR.ToString();
            txtImo.Text = oBooking.IMO.ToString();
            txtUno.Text = oBooking.UNO.ToString();
            txtVessel.Text = oBooking.VesselName.ToString();
            txtMainLineVessel.Text = oBooking.MainLineVesselName.ToString();
            txtRefBookingNo.Text = oBooking.RefBookingNo.ToString();
            txtRefBookingDate.Text = oBooking.RefBookingDate.ToString();
            txtCommodity.Text = oBooking.Commodity.ToString();
            if (oBooking.Reefer == true)
                rdoReefer.SelectedValue = "Yes";
            else
                rdoReefer.SelectedValue = "No";

            if (oBooking.HazCargo == true)
                rdoHazardousCargo.SelectedValue = "Yes";
            else
                rdoHazardousCargo.SelectedValue = "No";

            if (oBooking.BLThruApp == true)
                rdoBLThruEdge.SelectedValue = "Yes";
            else
                rdoBLThruEdge.SelectedValue = "No";

            txtGrossSeight.Text = oBooking.GrossWt.ToString();
            txtCbm.Text = oBooking.CBM.ToString();
            txtTempMax.Text = oBooking.TempMax.ToString();
            txtTempMin.Text = oBooking.TempMin.ToString();
            hdnBookingID.Value = Convert.ToString(oBooking.BookingID);

        }

        void PopulateDropDown(int Number, DropDownList ddl, int? Filter)
        {
            CommonBLL.PopulateDropdown(Number, ddl, Filter, 0);
        }

        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    if (Page.IsValid)
        //    {

        //        //if (Convert.ToDecimal(txtWFrom.Text) > Convert.ToDecimal(txtWTo.Text))
        //        //{
        //        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00077") + "');</script>", false);
        //        //    return;
        //        //}

        //        oBookingBll = new BookingBLL();
        //        oBookingEntity = new BookingEntity();
        //        //oUserEntity = (UserEntity)Session[Constants.SESSION_USER_INFO]; // This section has been commented temporarily

        //        oBookingEntity.LinerID = ddlNvocc.SelectedValue.ToInt();
        //        oBookingEntity.LocationID = ddlLocation.SelectedValue.ToInt();

        //        oBookingEntity.VesselID = hdnVessel.Value.ToInt();
        //        oBookingEntity.MainLineVesselID = hdnMainLineVessel.Value.ToInt();
        //        oBookingEntity.VoyageID = ddlLoadingVoyage.SelectedValue.ToInt();
        //        oBookingEntity.MainLineVoyageID = ddlMainLineVoyage.SelectedValue.ToInt();
        //        oBookingEntity.BookingNo = txtBookingNo.Text.ToString();

        //        oBookingEntity.FPODID = hdnFPOD.Value;
        //        oBookingEntity.PODID = hdnPOD.Value;
        //        oBookingEntity.POLID = hdnPOL.Value;
        //        oBookingEntity.PORID = hdnPOR.Value;

        //        oBookingEntity.BookingDate = txtBookingDate.Text.ToDateTime();
        //        oBookingEntity.Accounts = Convert.ToString(txtAccounts.Text.Trim());
        //        oBookingEntity.Commodity = Convert.ToString(txtCommodity.Text.Trim());
        //        oBookingEntity.BookingStatus = true;
        //        oBookingEntity.CustID = ddlBookingParty.SelectedValue.ToInt();
        //        oBookingEntity.IMO = Convert.ToString(txtImo.Text);
        //        oBookingEntity.UNO = Convert.ToString(txtUno.Text);
        //        oBookingEntity.RefBookingNo = txtRefBookingNo.Text.ToString();
        //        oBookingEntity.RefBookingDate = txtRefBookingDate.Text.ToDateTime();
        //        oBookingEntity.ShipmentType = Convert.ToChar(ddlShipmentType.SelectedValue);
        //        oBookingEntity.TempMin = Convert.ToDecimal(txtTempMin.Text);
        //        oBookingEntity.TempMax = Convert.ToDecimal(txtTempMax.Text);
        //        if (Convert.ToString(rdoHazardousCargo.SelectedValue) == "Yes")
        //            oBookingEntity.HazCargo = true;
        //        else
        //            oBookingEntity.HazCargo = false;

        //        if (Convert.ToString(rdoBLThruEdge.SelectedValue) == "Yes")
        //            oBookingEntity.BLThruApp = true;
        //        else
        //            oBookingEntity.BLThruApp = false;

        //        if (Convert.ToString(rdoReefer.SelectedValue) == "Yes")
        //            oBookingEntity.Reefer = true;
        //        else
        //            oBookingEntity.Reefer = false;

        //        if (hdnBookingID.Value == "0") // Insert
        //        {
        //            oBookingEntity.CreatedBy = _userId;// oUserEntity.Id;
        //            oBookingEntity.CreatedOn = DateTime.Today.Date;
        //            oBookingEntity.ModifiedBy = _userId;// oUserEntity.Id;
        //            oBookingEntity.ModifiedOn = DateTime.Today.Date;

        //            switch (oBookingBll.AddEditBooking(oBookingEntity, _CompanyId))
        //            {
        //                case -1: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00076");
        //                    break;
        //                case 0: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00011");
        //                    ClearAll();
        //                    break;
        //                case 1: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00009");
        //                    ClearAll();
        //                    break;
        //            }
        //        }
        //        else // Update
        //        {
        //            oBookingEntity.BookingID = Convert.ToInt32(hdnBookingID.Value);
        //            oBookingEntity.ModifiedBy = _userId;// oUserEntity.Id;
        //            oBookingEntity.ModifiedOn = DateTime.Today.Date;
        //            oBookingEntity.Action = true;
        //            //
        //            switch (oBookingBll.AddEditBooking(oBookingEntity, _CompanyId))
        //            {
        //                case -1: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00076");
        //                    break;
        //                case 0: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00011");
        //                    break;
        //                case 1: Response.Redirect("~/Export/ManageBooking.aspx");
        //                    break;
        //            }
        //        }


        //    }
        //}

        private void RetriveParameters()
        {
            //_userId = UserBLL.GetLoggedInUserId();

            if (!ReferenceEquals(Request.QueryString["id"], null))
            {
                Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _locId);
                hdnBookingID.Value = _locId.ToString();
            }
            _userId = EMS.BLL.UserBLL.GetLoggedInUserId();

            EMS.BLL.UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Export/Booking.aspx");
        }

        protected void rdoHazardousCargo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUno.Text = "ZZZZZ";
            txtImo.Text = "ZZZ";

            if (rdoHazardousCargo.SelectedValue == "No")
            {
                txtUno.Enabled = false;
                txtImo.Enabled = false;
            }
            else
            {
                txtUno.Enabled = true;
                txtImo.Enabled = true;
            }
        }
        void ClearAll()
        {
            rdoBLThruEdge.SelectedValue = "Yes";
            ddlBookingParty.SelectedIndex = 0;
            rdoHazardousCargo.SelectedValue = "No";
            ddlLoadingVoyage.SelectedIndex = -1;
            ddlLocation.SelectedIndex = -1;
            ddlMainLineVoyage.SelectedIndex = -1;
            ddlNvocc.SelectedIndex = 0;
            rdoReefer.SelectedValue = "No";
            ddlService.SelectedIndex = -1;
            ddlShipmentType.SelectedIndex = 0;

            txtFPOD.Text = string.Empty;
            txtPOD.Text = string.Empty;
            txtPOL.Text = string.Empty;
            txtPOR.Text = string.Empty;

            hdnFPOD.Value = "0";
            hdnPOD.Value = "0";
            hdnPOL.Value = "0";
            hdnPOR.Value = "0";

            txtAccounts.Text = string.Empty;
            txtBookingDate.Text = string.Empty;
            txtBookingNo.Text = string.Empty;
            txtCbm.Text = string.Empty;
            txtCommodity.Text = string.Empty;

            txtContainerDtls.Text = string.Empty;
            txtGrossSeight.Text = string.Empty;
            txtImo.Text = string.Empty;
            txtVessel.Text = string.Empty;
            txtMainLineVessel.Text = string.Empty;

            txtRefBookingDate.Text = string.Empty;
            txtRefBookingNo.Text = string.Empty;
            txtTempMax.Text = string.Empty;
            txtTempMin.Text = string.Empty;
            txtTransitRoute.Text = string.Empty;
        }

        protected void lnkContainerDtls_Click(object sender, EventArgs e)
        {

        }

        protected void lnkTransitRoute_Click(object sender, EventArgs e)
        {

        }

        protected void rdoBLThruEdge_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void rdoReffer_SelectedIndexChanged(object sender, EventArgs e)
        {
            {

                if (rdoReefer.SelectedValue == "No")
                {
                    txtTempMin.Text = string.Empty;
                    txtTempMax.Text = string.Empty;

                    txtTempMax.Enabled = false;
                    txtTempMin.Enabled = false;
                }
                else
                {
                    txtTempMax.Enabled = true;
                    txtTempMin.Enabled = true;
                }
            }
        }

        //protected void txtVessel_TextChanged(object sender, EventArgs e)
        //{
        //    //string VesselNm = txtVessel.Text;
        //    //txtVessel.Text = VesselNm.Split('(')[0].Trim();
        //    Filler.FillData(ddlLoadingVoyage, CommonBLL.GetExportVoyages(hdnVessel.Value), "VoyageNo", "VoyageID", "Voyage");

        //}

        //protected void txtMainLineVessel_TextChanged(object sender, EventArgs e)
        //{
        //    //string VesselNm = txtMainLineVessel.Text;
        //    //txtMainLineVessel.Text = VesselNm.Split('(')[0].Trim();
        //    Filler.FillData(ddlMainLineVoyage, CommonBLL.GetExportVoyages(hdnMainLineVessel.Value), "VoyageNo", "VoyageID", "Voyage");
        //}

        //protected void txtPOD_TextChanged(object sender, EventArgs e)
        //{
        //    string FDPort = txtFPOD.Text;

        //    if (FDPort == string.Empty)
        //    {
        //        hdnFPOD.Value = hdnPOD.Value;
        //        txtFPOD.Text = txtPOD.Text;
        //    }


        //}

        //protected void txtPOR_TextChanged(object sender, EventArgs e)
        //{
        //    string LPort = txtPOL.Text;

        //    if (LPort == string.Empty)
        //    {
        //        hdnPOL.Value = hdnPOR.Value;
        //        txtPOL.Text = txtPOR.Text;
        //    }

                
                
            
        //}

        protected void btnSave_Click1(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                //if (Convert.ToDecimal(txtWFrom.Text) > Convert.ToDecimal(txtWTo.Text))
                //{
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00077") + "');</script>", false);
                //    return;
                //}

                oBookingBll = new BookingBLL();
                oBookingEntity = new BookingEntity();
                //oUserEntity = (UserEntity)Session[Constants.SESSION_USER_INFO]; // This section has been commented temporarily

                oBookingEntity.LinerID = ddlNvocc.SelectedValue.ToInt();
                oBookingEntity.LocationID = ddlLocation.SelectedValue.ToInt();

                oBookingEntity.VesselID = hdnVessel.Value.ToInt();
                oBookingEntity.MainLineVesselID = hdnMainLineVessel.Value.ToInt();
                oBookingEntity.VoyageID = ddlLoadingVoyage.SelectedValue.ToInt();
                oBookingEntity.MainLineVoyageID = ddlMainLineVoyage.SelectedValue.ToInt();
                oBookingEntity.BookingNo = txtBookingNo.Text.ToString();

                oBookingEntity.FPODID = hdnFPOD.Value;
                oBookingEntity.PODID = hdnPOD.Value;
                oBookingEntity.POLID = hdnPOL.Value;
                oBookingEntity.PORID = hdnPOR.Value;

                oBookingEntity.BookingDate = txtBookingDate.Text.ToDateTime();
                oBookingEntity.Accounts = Convert.ToString(txtAccounts.Text.Trim());
                oBookingEntity.Commodity = Convert.ToString(txtCommodity.Text.Trim());
                oBookingEntity.BookingStatus = true;
                oBookingEntity.CustID = ddlBookingParty.SelectedValue.ToInt();
                oBookingEntity.IMO = Convert.ToString(txtImo.Text);
                oBookingEntity.UNO = Convert.ToString(txtUno.Text);
                oBookingEntity.RefBookingNo = txtRefBookingNo.Text.ToString();
                oBookingEntity.RefBookingDate = txtRefBookingDate.Text.ToDateTime();
                oBookingEntity.ShipmentType = Convert.ToChar(ddlShipmentType.SelectedValue);
                oBookingEntity.TempMin = Convert.ToDecimal(txtTempMin.Text);
                oBookingEntity.TempMax = Convert.ToDecimal(txtTempMax.Text);
                if (Convert.ToString(rdoHazardousCargo.SelectedValue) == "Yes")
                    oBookingEntity.HazCargo = true;
                else
                    oBookingEntity.HazCargo = false;

                if (Convert.ToString(rdoBLThruEdge.SelectedValue) == "Yes")
                    oBookingEntity.BLThruApp = true;
                else
                    oBookingEntity.BLThruApp = false;

                if (Convert.ToString(rdoReefer.SelectedValue) == "Yes")
                    oBookingEntity.Reefer = true;
                else
                    oBookingEntity.Reefer = false;

                if (hdnBookingID.Value == "0") // Insert
                {
                    oBookingEntity.CreatedBy = _userId;// oUserEntity.Id;
                    oBookingEntity.CreatedOn = DateTime.Today.Date;
                    oBookingEntity.ModifiedBy = _userId;// oUserEntity.Id;
                    oBookingEntity.ModifiedOn = DateTime.Today.Date;

                    switch (oBookingBll.AddEditBooking(oBookingEntity, _CompanyId))
                    {
                        case -1: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00076");
                            break;
                        case 0: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00011");
                            ClearAll();
                            break;
                        case 1: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00009");
                            ClearAll();
                            break;
                    }
                }
                else // Update
                {
                    oBookingEntity.BookingID = Convert.ToInt32(hdnBookingID.Value);
                    oBookingEntity.ModifiedBy = _userId;// oUserEntity.Id;
                    oBookingEntity.ModifiedOn = DateTime.Today.Date;
                    oBookingEntity.Action = true;
                    //
                    switch (oBookingBll.AddEditBooking(oBookingEntity, _CompanyId))
                    {
                        case -1: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00076");
                            break;
                        case 0: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00011");
                            break;
                        case 1: Response.Redirect("~/Export/ManageBooking.aspx");
                            break;
                    }
                }


            }
        }

        protected void txtPOR_TextChanged(object sender, EventArgs e)
        {
            string LPort = txtPOL.Text;

            if (LPort == string.Empty)
            {
                hdnPOL.Value = hdnPOR.Value;
                txtPOL.Text = txtPOR.Text;
            }


        }

        protected void txtPOD_TextChanged(object sender, EventArgs e)
        {
            string FDPort = txtFPOD.Text;

            if (FDPort == string.Empty)
            {
                hdnFPOD.Value = hdnPOD.Value;
                txtFPOD.Text = txtPOD.Text;
            }
        }

        protected void txtVessel_TextChanged(object sender, EventArgs e)
        {
            PopulateVessel(Convert.ToInt32(hdnVessel.Value));
            //Filler.FillData(ddlLoadingVoyage, CommonBLL.GetExportVoyages(hdnVessel.Value), "VoyageNo", "VoyageID", "Voyage");
        }

        protected void txtMainLineVessel_TextChanged(object sender, EventArgs e)
        {
            PopulateVessel(Convert.ToInt32(hdnMainLineVessel.Value));
            //Filler.FillData(ddlMainLineVoyage, CommonBLL.GetExportVoyages(hdnMainLineVessel.Value), "VoyageNo", "VoyageID", "Voyage");
        }

        private void PopulateVessel(int vesselID)
        {
            //BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            DataSet ds = BookingBLL.GetExportVoyages(vesselID);
            ddlLoadingVoyage.DataValueField = "VoyageID";
            ddlLoadingVoyage.DataTextField = "VoyageNo";
            ddlLoadingVoyage.DataSource = ds;
            ddlLoadingVoyage.DataBind();
            //ddlLoadingVoyage.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
        }

        private void PopulateSevices(int Lineid, Int32 fpod)
        {
            DataSet ds = BookingBLL.GetExportServices(Lineid, fpod);
            ddlLoadingVoyage.DataValueField = "ServiceID";
            ddlLoadingVoyage.DataTextField = "ServiceName";
            ddlLoadingVoyage.DataSource = ds;
            ddlLoadingVoyage.DataBind();
        }

        protected void txtFPOD_TextChanged(object sender, EventArgs e)
        {
            PopulateSevices(ddlNvocc.SelectedValue.ToInt(), Convert.ToInt32(hdnFPOD.Value));
        }

    }
}