﻿using System;
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
        List<BookingContainerEntity> oBookingContainers;
        BookingContainerEntity oBookingContainerEntity;
        BookingEntity oBookingEntity;
        BookingBLL oBookingBll;
        UserEntity oUserEntity;
        //DataTable Dt;

        List<IBookingContainer> Containers = new List<IBookingContainer>();
        List<IBookingTranshipment> Transhipments = new List<IBookingTranshipment>();

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

            if (!Page.IsPostBack)
            {
                fillAllDropdown();
                if (hdnBookingID.Value == "0" || hdnBookingID.Value == string.Empty)
                {
                    txtImo.Enabled = false;
                    txtUno.Enabled = false;
                    txtTempMax.Enabled = false;
                    txtTempMin.Enabled = false;
                    txtImo.Text = "ZZZ";
                    txtUno.Text = "ZZZZZ";
                    //oBookingContainers = new List<BookingContainerEntity>();
                    //BookingContainerEntity Ent = new BookingContainerEntity();
                    //oBookingContainers.Add(Ent);
                    //gvContainer.DataSource = oBookingContainers;
                    //gvContainer.DataBind();
                    //gvContainer.Rows[0].Visible = false;
                }
                else
                {
                    FillBooking(Convert.ToInt32(hdnBookingID.Value));
                    FillBookingContainer(Convert.ToInt32(hdnBookingID.Value));
                    FillBookingTranshipment(Convert.ToInt32(hdnBookingID.Value));
                }
            }
            CheckUserAccess(hdnBookingID.Value);
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
                //Li = new ListItem("SELECT", "0");
                ListItem item = new ListItem(Enum.GetName(typeof(Enums.ShipmentType), r).Replace('_', ' '), ((int)r).ToString());
                ddlShipmentType.Items.Add(item);
            }
            //ddlShipmentType.Items.Insert(0, Li);
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


            #region ContainerType

            //GridViewRow cntrtypes = gvContainer.HeaderRow;

            ////GridViewRow item = (GridViewRow)((DropDownList)sender).NamingContainer;

            //DropDownList ddlCntrType = (DropDownList)cntrtypes.FindControl("ddlCntrType");
            GeneralFunctions.PopulateDropDownList(ddlCntrType, dbinteract.PopulateDDLDS("mstContainerType", "pk_ContainerTypeID", "ContainerAbbr"));
            ////Li = new ListItem("ALL", "-1");
            ////PopulateDropDown((int)Enums.DropDownPopulationFor.Services, ddlService, 0);
            ////ddlService.Items.Insert(0, Li);
            #endregion
        }

        private void FillBooking(Int32 BookingID)
        {
            //oBookingEntity = new BookingEntity();
            //oBookingBll = new BookingBLL();
            //oBookingEntity = (BookingEntity)oBookingBll.GetBooking(BookingID);

            BookingEntity oBooking = (BookingEntity)BookingBLL.GetBooking(Convert.ToInt32(hdnBookingID.Value), "N");

            //ddlFromLocation.SelectedIndex = Convert.ToInt32(ddlFromLocation.Items.IndexOf(ddlFromLocation.Items.FindByValue(oImportHaulage.LocationFrom)));
            hdnFPOD.Value = oBooking.FPODID.ToString();
            hdnPOD.Value = oBooking.PODID.ToString();
            hdnPOL.Value = oBooking.POLID.ToString();
            hdnPOR.Value = oBooking.PORID.ToString();
            hdnVessel.Value = oBooking.VesselID.ToString();
            hdnMainLineVessel.Value = oBooking.MainLineVesselID.ToString();
            ddlNvocc.SelectedValue = oBooking.LinerID.ToString();

            PopulateSevices(ddlNvocc.SelectedValue.ToInt(), Convert.ToInt32(hdnFPOD.Value));
            PopulateVoyage(Convert.ToInt32(hdnVessel.Value));
            PopulateMLVoyage(Convert.ToInt32(hdnMainLineVessel.Value));

            ddlService.SelectedValue = oBooking.ServicesID.ToString();
            ddlBookingParty.SelectedValue = oBooking.CustID.ToString();
            ddlLoadingVoyage.SelectedValue = oBooking.VoyageID.ToString();
            ddlMainLineVoyage.SelectedValue = oBooking.MainLineVoyageID.ToString();
            ddlShipmentType.SelectedIndex = ddlShipmentType.Items.IndexOf(ddlShipmentType.Items.FindByValue(oBooking.ShipmentType.ToString()));
            txtAccounts.Text = oBooking.Accounts.ToString();
            txtBookingDate.Text = oBooking.BookingDate.ToShortDateString();
            txtBookingNo.Text = oBooking.BookingNo.ToString();
            txtCommodity.Text = oBooking.Commodity.ToString();
            txtGrossWeight.Text = oBooking.GrossWt.ToString();
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

            txtGrossWeight.Text = oBooking.GrossWt.ToString();
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
            txtGrossWeight.Text = string.Empty;
            txtImo.Text = string.Empty;
            txtVessel.Text = string.Empty;
            txtMainLineVessel.Text = string.Empty;

            txtRefBookingDate.Text = string.Empty;
            txtRefBookingNo.Text = string.Empty;
            txtTempMax.Text = string.Empty;
            txtTempMin.Text = string.Empty;
            txtTransitRoute.Text = string.Empty;

            ViewState["BookingCntr"] = null;

            gvContainer.DataSource = null;
            gvContainer.DataBind();
            hdnBookingID.Value = "0";
        }

        protected void lnkContainerDtls_Click(object sender, EventArgs e)
        {

            //DataTable Dt = CreateDataTable();
            ModalPopupExtender1.Show();
        }

        protected void lnkTransitRoute_Click(object sender, EventArgs e)
        {
            ModalPopupExtender2.Show();
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
                oBookingEntity.BookingID = Convert.ToInt32(hdnBookingID.Value);

                oBookingEntity.VesselID = hdnVessel.Value.ToInt();
                oBookingEntity.MainLineVesselID = hdnMainLineVessel.Value.ToInt();
                oBookingEntity.VoyageID = ddlLoadingVoyage.SelectedValue.ToInt();
                oBookingEntity.MainLineVoyageID = ddlMainLineVoyage.SelectedValue.ToInt();
                oBookingEntity.BookingNo = txtBookingNo.Text.ToString();
                oBookingEntity.BookingDate = txtBookingDate.Text.ToDateTime();

                oBookingEntity.FPODID = hdnFPOD.Value;
                oBookingEntity.PODID = hdnPOD.Value;
                oBookingEntity.POLID = hdnPOL.Value;
                oBookingEntity.PORID = hdnPOR.Value;

                oBookingEntity.Accounts = Convert.ToString(txtAccounts.Text.Trim());
                oBookingEntity.Commodity = Convert.ToString(txtCommodity.Text.Trim());
                oBookingEntity.BookingStatus = true;
                oBookingEntity.CustID = ddlBookingParty.SelectedValue.ToInt();
                oBookingEntity.IMO = Convert.ToString(txtImo.Text);
                oBookingEntity.UNO = Convert.ToString(txtUno.Text);
                oBookingEntity.RefBookingNo = txtRefBookingNo.Text.ToString();
                oBookingEntity.RefBookingDate = txtRefBookingDate.Text.ToDateTime();
                oBookingEntity.ShipmentType = Convert.ToChar(ddlShipmentType.SelectedValue);
                oBookingEntity.ServicesID = ddlService.SelectedValue.ToInt();
                if (txtTempMin.Text.Trim() != string.Empty)
                    oBookingEntity.TempMin = Convert.ToDecimal(txtTempMin.Text);
                if (txtTempMax.Text.Trim() != string.Empty)
                    oBookingEntity.TempMax = Convert.ToDecimal(txtTempMax.Text);
                //oBookingEntity.TempMin = Convert.ToDecimal(txtTempMin.Text);
                //oBookingEntity.TempMax = Convert.ToDecimal(txtTempMax.Text);
                if (txtGrossWeight.Text.Trim() != string.Empty)
                    oBookingEntity.GrossWt = Convert.ToDecimal(txtGrossWeight.Text);
                if (txtCbm.Text.Trim() != string.Empty)
                    oBookingEntity.CBM = Convert.ToDecimal(txtCbm.Text);
                //oBookingEntity.GrossWt = Convert.ToDecimal(txtGrossWeight.Text);
                //oBookingEntity.CBM = Convert.ToDecimal(txtCbm.Text);
                oBookingEntity.AcceptBooking = false;
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
                    int outBookingId = 0;
                    switch (oBookingBll.AddEditBooking(oBookingEntity, _CompanyId, ref outBookingId))
                    {
                        case -1: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00076");
                            break;
                        case 0: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00011");
                            ClearAll();
                            break;
                        case 1:
                            oBookingBll.DeactivateAllContainersAgainstBookingId(outBookingId);
                            switch (AddContainers(outBookingId))
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
                    int outBookingId = 0;
                    switch (oBookingBll.AddEditBooking(oBookingEntity, _CompanyId, ref outBookingId))
                    {
                        case -1: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00076");
                            break;
                        case 0: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00011");
                            break;
                        case 1: 
                            oBookingBll.DeactivateAllContainersAgainstBookingId(outBookingId);
                            switch (AddContainers(outBookingId))
                            {
                                case -1: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00076");
                                    break;
                                case 0: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00011");
                                    ClearAll();
                                    break;
                                case 1:
                                    Response.Redirect("~/Export/ManageBooking.aspx");
                                    break;
                            }                            
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

        protected void txtPOT_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtPOD_TextChanged(object sender, EventArgs e)
        {
            string FDPort = txtFPOD.Text;

            if (FDPort == string.Empty)
            {
                hdnFPOD.Value = hdnPOD.Value;
                txtFPOD.Text = txtPOD.Text;
                PopulateSevices(ddlNvocc.SelectedValue.ToInt(), Convert.ToInt32(hdnFPOD.Value));
            }
        }

        protected void txtVessel_TextChanged(object sender, EventArgs e)
        {
            PopulateVoyage(Convert.ToInt32(hdnVessel.Value));
            //Filler.FillData(ddlLoadingVoyage, CommonBLL.GetExportVoyages(hdnVessel.Value), "VoyageNo", "VoyageID", "Voyage");
        }

        protected void txtMainLineVessel_TextChanged(object sender, EventArgs e)
        {
            PopulateMLVoyage(Convert.ToInt32(hdnMainLineVessel.Value));
            //Filler.FillData(ddlMainLineVoyage, CommonBLL.GetExportVoyages(hdnMainLineVessel.Value), "VoyageNo", "VoyageID", "Voyage");
        }

        private void PopulateVoyage(int vesselID)
        {
            //BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            DataSet ds = BookingBLL.GetExportVoyages(vesselID);
            ddlLoadingVoyage.DataValueField = "VoyageID";
            ddlLoadingVoyage.DataTextField = "VoyageNo";
            ddlLoadingVoyage.DataSource = ds;
            ddlLoadingVoyage.DataBind();
            //ddlLoadingVoyage.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
        }

        private void PopulateMLVoyage(int vesselID)
        {
            //BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            DataSet ds = BookingBLL.GetExportVoyages(vesselID);
            ddlMainLineVoyage.DataValueField = "VoyageID";
            ddlMainLineVoyage.DataTextField = "VoyageNo";
            ddlMainLineVoyage.DataSource = ds;
            ddlMainLineVoyage.DataBind();
            //ddlLoadingVoyage.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
        }

        private void PopulateSevices(int Lineid, Int32 fpod)
        {
            DataSet ds = BookingBLL.GetExportServices(Lineid, fpod);
            ddlService.DataValueField = "ServiceID";
            ddlService.DataTextField = "ServiceName";
            ddlService.DataSource = ds;
            ddlService.DataBind();
        }

        protected void txtFPOD_TextChanged(object sender, EventArgs e)
        {
            PopulateSevices(ddlNvocc.SelectedValue.ToInt(), Convert.ToInt32(hdnFPOD.Value));
        }

        protected void ddlNvocc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (hdnFPOD.Value != "")
                PopulateSevices(ddlNvocc.SelectedValue.ToInt(), Convert.ToInt32(hdnFPOD.Value));

        }

        void AddContainers()
        {
            Containers = (List<IBookingContainer>)ViewState["BookingCntr"];
            if (Containers == null)
                Containers = new List<IBookingContainer>();
            oBookingBll = new BookingBLL();

            oBookingBll.DeactivateAllContainersAgainstBookingId(Convert.ToInt32(hdnBookingID.Value));

            foreach (IBookingContainer Container in Containers)
            {
                Container.BookingID = Convert.ToInt32(hdnBookingID.Value);
                oBookingBll.AddEditBookingContainer(Container);
            }
        }

        void FillBookingContainer(Int32 BookingID)
        {
            //oChargeRates = new List<ChargeRateEntity>();
            //oChargeRates = new List<ChargeRateEntity>();
            oBookingBll = new BookingBLL();
            Containers = new List<IBookingContainer>();
            Containers = oBookingBll.GetBookingContainers(BookingID);
            
            ViewState["BookingCntr"] = Containers;
            
            gvContainer.DataSource = Containers;
            gvContainer.DataBind();
        }

        void FillContainers()
        {
            Containers = (List<IBookingContainer>)ViewState["BookingCntr"];
            IEnumerable<IBookingContainer> Rts = from IBookingContainer rt in Containers
                                                 where rt.BkCntrStatus == true
                                                 select rt;

            gvContainer.DataSource = Rts.ToList();
            gvContainer.DataBind();

            //if (Rts.Count() <= 0)
            //{
            //    List<IBookingContainer> EmptyRates = new List<IBookingContainer>();
            //    IBookingContainer rt = new BookingContainerEntity();
            //    EmptyRates.Add(rt);

            //    gvContainer.DataSource = EmptyRates;
            //    gvContainer.DataBind();
            //    gvContainer.Rows[0].Visible = false;
            //}


        }

        void FillBookingTranshipment(Int32 BookingID)
        {
            //oChargeRates = new List<ChargeRateEntity>();
            //oChargeRates = new List<ChargeRateEntity>();
            oBookingBll = new BookingBLL();
            Transhipments = new List<IBookingTranshipment>();
            Transhipments = oBookingBll.GetBookingTranshipments(BookingID);

            ViewState["BookingTran"] = Transhipments;

            gvTransit.DataSource = Transhipments;
            gvTransit.DataBind();
        }

        void FillTranshipments()
        {
            Transhipments = (List<IBookingTranshipment>)ViewState["BookingTran"];
            IEnumerable<IBookingTranshipment> Rts = from IBookingTranshipment rt in Containers
                                                 where rt.BkTransStatus == true
                                                 select rt;

            gvTransit.DataSource = Rts.ToList();
            gvTransit.DataBind();

            //if (Rts.Count() <= 0)
            //{
            //    List<IBookingContainer> EmptyRates = new List<IBookingContainer>();
            //    IBookingContainer rt = new BookingContainerEntity();
            //    EmptyRates.Add(rt);

            //    gvContainer.DataSource = EmptyRates;
            //    gvContainer.DataBind();
            //    gvContainer.Rows[0].Visible = false;
            //}


        }

        protected void gvContainer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        private DataTable CreateDataTable(DataTable Dt)
        {
            Dt = new DataTable();
            DataColumn dc;

            dc = new DataColumn("BookingContainerID");
            Dt.Columns.Add(dc);

            dc = new DataColumn("ContainerTypeID");
            Dt.Columns.Add(dc);

            dc = new DataColumn("ContainerType");
            Dt.Columns.Add(dc);

            dc = new DataColumn("CntrSize");
            Dt.Columns.Add(dc);

            dc = new DataColumn("NoofContainers");
            Dt.Columns.Add(dc);

            dc = new DataColumn("wtPerCntr");
            Dt.Columns.Add(dc);

            return Dt;
        }

        protected void btnimgSave_Click(object sender, ImageClickEventArgs e)
        {
            if(ViewState["BookingCntr"] != null)
                Containers = (List<IBookingContainer>)ViewState["BookingCntr"];
            //IEnumerable<IBookingContainer> Rts = from IBookingContainer rt in Containers
            //                                     where rt.BkCntrStatus == true
            //                                     select rt;

            lblMessage.Text = string.Empty;

            IBookingContainer obj = new BookingContainerEntity();
            if (!string.IsNullOrEmpty(hdnIndex.Value))
                obj = Containers.ElementAt(Convert.ToInt32(hdnIndex.Value));
            else
            {
                foreach (BookingContainerEntity objBookingContainer in Containers)
                {
                    if (objBookingContainer.ContainerTypeID.ToString() == ddlCntrType.SelectedValue && objBookingContainer.CntrSize == ddlSize.SelectedValue)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00076") + "');</script>", false);
                        return;
                    }
                }
            }

            if ((String.IsNullOrEmpty(txtNos.Text) || String.IsNullOrEmpty(txtWtPerCntr.Text)))
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00077") + "');</script>", false);
                return;
            }

            obj.BookingContainerID = Convert.ToInt32(hdnBookingContainerID.Value);
            obj.ContainerTypeID = Convert.ToInt32(ddlCntrType.SelectedValue);
            obj.ContainerType = ddlCntrType.SelectedItem.Text;
            obj.CntrSize = ddlSize.SelectedValue;
            obj.NoofContainers = Convert.ToInt32(txtNos.Text);
            obj.wtPerCntr = Convert.ToDecimal(txtWtPerCntr.Text);
            if (string.IsNullOrEmpty(hdnIndex.Value))
                Containers.Add(obj);

            gvContainer.DataSource = Containers;
            gvContainer.DataBind();

            ViewState["BookingCntr"] = Containers;
            ResetContainer();
            //ModalPopupExtender1.Show();
        }

        protected void btnimgReset_Click(object sender, ImageClickEventArgs e)
        {
            ResetContainer();
        }

        private void ResetContainer()
        {
            hdnBookingContainerID.Value = "0";
            hdnIndex.Value = string.Empty;
            ddlCntrType.SelectedIndex = 0;
            ddlSize.SelectedIndex = 0;
            txtNos.Text = string.Empty;
            txtWtPerCntr.Text = string.Empty;
        }

        protected void gvContainer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int RowIndex = row.RowIndex;

            if (e.CommandName == "EditGrid")
            {
                HiddenField gvhdnBookingContainerID = (HiddenField)gvContainer.Rows[RowIndex].FindControl("gvhdnBookingContainerID");
                HiddenField gvhdnContainerTypeId = (HiddenField)gvContainer.Rows[RowIndex].FindControl("gvhdnContainerTypeId");
                Label lblContainerSize = (Label)gvContainer.Rows[RowIndex].FindControl("lblContainerSize");
                Label lblUnit = (Label)gvContainer.Rows[RowIndex].FindControl("lblUnit");
                Label lblwtPerCont = (Label)gvContainer.Rows[RowIndex].FindControl("lblwtPerCont");

                hdnIndex.Value = RowIndex.ToString();
                hdnBookingContainerID.Value = gvhdnBookingContainerID.Value;
                ddlCntrType.SelectedValue = gvhdnContainerTypeId.Value;
                ddlSize.SelectedValue = lblContainerSize.Text;
                txtNos.Text = lblUnit.Text;
                txtWtPerCntr.Text = lblwtPerCont.Text;
            }
            else if (e.CommandName == "Remove")
            {
                if (ViewState["BookingCntr"] != null)
                    Containers = (List<IBookingContainer>)ViewState["BookingCntr"];
                
                if (Containers.Count>0)
                {
                    //dt = (DataTable)otabSelItems;
                    Containers.RemoveAt(RowIndex);
                    //dt.Rows.RemoveAt(RowIndex);
                    //dt.AcceptChanges();

                    gvContainer.DataSource = Containers;
                    gvContainer.DataBind();

                    ViewState["BookingCntr"] = gvContainer.DataSource = Containers;
                    
                    //ModalPopupExtender1.Show();
                }
            }
        }

        private int AddContainers(int BookingId)
        {
            if (ViewState["BookingCntr"] != null)
                Containers = (List<IBookingContainer>)ViewState["BookingCntr"];

            if (Containers.Count > 0)
            {
                foreach(BookingContainerEntity obj in Containers)
                {
                    oBookingBll = new BookingBLL();
                    //oBookingContainerEntity = new BookingContainerEntity();

                    obj.BookingID = BookingId;
                    //oBookingContainerEntity.BookingContainerID = Convert.ToInt32(dt.Rows[i]["BookingContainerID"].ToString());
                    //oBookingContainerEntity.ContainerTypeID = Convert.ToInt32(dt.Rows[i]["ContainerTypeID"].ToString());
                    //oBookingContainerEntity.CntrSize = dt.Rows[i]["CntrSize"].ToString();
                    //oBookingContainerEntity.NoofContainers = Convert.ToInt32(dt.Rows[i]["NoofContainers"].ToString());
                    //oBookingContainerEntity.wtPerCntr = Convert.ToDecimal(dt.Rows[i]["wtPerCntr"].ToString());
                    
                    int res = oBookingBll.AddEditBookingContainer(obj);
                    if (res != 1)
                        return res;
                }
                return 1;
            }
            return 1;
        }

        private int AddTranshipments(int BookingId)
        {
            if (ViewState["BookingTran"] != null)
                Transhipments = (List<IBookingTranshipment>)ViewState["BookingTran"];

            if (Transhipments.Count > 0)
            {
                foreach (BookingTranshipmentEntity obj in Transhipments)
                {
                    oBookingBll = new BookingBLL();
                    //oBookingContainerEntity = new BookingContainerEntity();

                    obj.BookingID = BookingId;
                    //oBookingContainerEntity.BookingContainerID = Convert.ToInt32(dt.Rows[i]["BookingContainerID"].ToString());
                    //oBookingContainerEntity.ContainerTypeID = Convert.ToInt32(dt.Rows[i]["ContainerTypeID"].ToString());
                    //oBookingContainerEntity.CntrSize = dt.Rows[i]["CntrSize"].ToString();
                    //oBookingContainerEntity.NoofContainers = Convert.ToInt32(dt.Rows[i]["NoofContainers"].ToString());
                    //oBookingContainerEntity.wtPerCntr = Convert.ToDecimal(dt.Rows[i]["wtPerCntr"].ToString());

                    int res = oBookingBll.AddEditBookingTranshipment(obj);
                    if (res != 1)
                        return res;
                }
                return 1;
            }
            return 1;
        }

        protected void gvContainer_DataBound(object sender, EventArgs e)
        {
            if (ViewState["BookingCntr"] != null)
                Containers = (List<IBookingContainer>)ViewState["BookingCntr"];
            
            txtContainerDtls.Text = string.Empty;

            if (Containers.Count > 0)
            {
                foreach (BookingContainerEntity obj in Containers)
                {
                    if (string.IsNullOrEmpty(txtContainerDtls.Text))
                        txtContainerDtls.Text = obj.ContainerType + "(" + obj.CntrSize + ") X " + obj.NoofContainers.ToString();
                    else
                        txtContainerDtls.Text += "," + obj.ContainerType + "(" + obj.CntrSize + ") X " + obj.NoofContainers.ToString();
                }
            }
        }

    }
}