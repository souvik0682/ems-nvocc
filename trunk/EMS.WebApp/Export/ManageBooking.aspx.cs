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
        List<BookingContainerEntity> oBookingContainers;
        BookingContainerEntity oBookingContainerEntity;
        BookingEntity oBookingEntity;
        BookingBLL oBookingBll;
        UserEntity oUserEntity;
        //DataTable Dt;

        List<IBookingContainer> Containers = new List<IBookingContainer>();

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
                    oBookingContainers = new List<BookingContainerEntity>();
                    BookingContainerEntity Ent = new BookingContainerEntity();
                    oBookingContainers.Add(Ent);
                    gvContainer.DataSource = oBookingContainers;
                    gvContainer.DataBind();
                    gvContainer.Rows[0].Visible = false;
                }
                else
                {
                    FillBooking(Convert.ToInt32(hdnBookingID.Value));
                    FillBookingContainer(Convert.ToInt32(hdnBookingID.Value));
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
            PopulateVessel(Convert.ToInt32(hdnVessel.Value));
            //Filler.FillData(ddlLoadingVoyage, CommonBLL.GetExportVoyages(hdnVessel.Value), "VoyageNo", "VoyageID", "Voyage");
        }

        protected void txtMainLineVessel_TextChanged(object sender, EventArgs e)
        {
            PopulateMLVessel(Convert.ToInt32(hdnMainLineVessel.Value));
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

        private void PopulateMLVessel(int vesselID)
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
            if (Containers.Count <= 0)
            {
                IBookingContainer rt = new BookingContainerEntity();
                Containers.Add(rt);
            }
            else
            {
                ViewState["BookingCntr"] = Containers;
            }

            gvContainer.DataSource = Containers;
            gvContainer.DataBind();
        }

        protected void gvContainer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                BLL.DBInteraction dbinteract = new BLL.DBInteraction();

                DropDownList ddlCntrType = (DropDownList)e.Row.FindControl("ddlCntrType");
                DropDownList ddlSize = (DropDownList)e.Row.FindControl("ddlSize");
                GeneralFunctions.PopulateDropDownList(ddlCntrType, dbinteract.PopulateDDLDS("mstContainerType", "pk_ContainerTypeID", "ContainerAbbr"));
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {

                ImageButton btnRemove = (ImageButton)e.Row.FindControl("lnkDelete");
                btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00012");

                btnRemove.OnClientClick = "javascript:return confirm('" + ResourceManager.GetStringWithoutName("ERR00014") + "');";

                ////Delete link
                //GridViewRow Row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                //HiddenField hdnId = (HiddenField)Row.FindControl("hdnId");

                ////ChargeBLL.DeleteChargeRate(Convert.ToInt32(hdnId.Value));
                ////FillChargeRate(Convert.ToInt32(hdnChargeID.Value));
                //Containers = (List<IBookingContainer>)ViewState["BookingCntr"];
                ////Rates[Row.RowIndex].RateActive = false;
                //Containers.RemoveAt(Row.RowIndex);
                //ViewState["BookingCntr"] = Containers;

                //FillContainers();

                //ImageButton btnRemove = (ImageButton)e.Row.FindControl("lnkDelete");
                //btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00012");
                ////btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pk_PortId"));


                //btnRemove.OnClientClick = "javascript:return confirm('" + ResourceManager.GetStringWithoutName("ERR00014") + "');";

                ////btnEdit.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00008") + "');return false;";
                ////btnRemove.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00008") + "');return false;";

            }
        }
   
        protected void gvContainer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (ViewState["BookingCntr"] == null)
            {
                ViewState["BookingCntr"] = Containers;
            }
            else
            {
                Containers = (List<IBookingContainer>)ViewState["BookingCntr"];
            }


            #region Save
            if (e.CommandArgument == "Save")
            {
                //if (hdnChargeID.Value != "0")
                //{


                oBookingBll = new BookingBLL();
                GridViewRow Row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

                TextBox txtNos = (TextBox)Row.FindControl("txtNos");
                TextBox txtWtPerCntr = (TextBox)Row.FindControl("txtWtPerCntr");
                DropDownList ddlCntrType = (DropDownList)Row.FindControl("ddlCntrType");
                DropDownList ddlSize = (DropDownList)Row.FindControl("ddlSize");

                if ((String.IsNullOrEmpty(txtNos.Text) || String.IsNullOrEmpty(txtWtPerCntr.Text)))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00077") + "');</script>", false);
                    return;
                }

                oBookingContainerEntity = new BookingContainerEntity();

                oBookingContainerEntity.BookingID = Convert.ToInt32(hdnBookingID.Value);
                oBookingContainerEntity.CntrSize = Convert.ToString(ddlSize.SelectedValue);
                oBookingContainerEntity.ContainerTypeID = Convert.ToInt32(ddlCntrType.SelectedValue);
                oBookingContainerEntity.NoofContainers = Convert.ToInt32(txtNos.Text);
                oBookingContainerEntity.wtPerCntr = Convert.ToInt32(txtWtPerCntr.Text);
                ViewState["BookingCntr"] = Containers;
                FillContainers();
                //DisableAllField();
            }

            #endregion

            #region Edit
            if (e.CommandArgument == "Edit")
            {
                GridViewRow FootetRow = gvContainer.HeaderRow;

                DropDownList ddlCntrtype = (DropDownList)FootetRow.FindControl("ddlCntrType");
                DropDownList ddlSize = (DropDownList)FootetRow.FindControl("ddlSize");

                TextBox txtNos = (TextBox)FootetRow.FindControl("txtNos");
                TextBox txtwtPerCntr = (TextBox)FootetRow.FindControl("txtWtPerCntr");

                HiddenField hdnBookingContainerID = (HiddenField)FootetRow.FindControl("hdnBookingContainerID");

                GridViewRow Row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                HiddenField hdnLocationId = (HiddenField)Row.FindControl("hdnLocationId");
                HiddenField hdnTerminalId = (HiddenField)Row.FindControl("hdnTerminalId");
                HiddenField hdnWashingTypeId = (HiddenField)Row.FindControl("hdnWashingTypeId");
                HiddenField hdnId = (HiddenField)Row.FindControl("hdnId");

                Label lblLow = (Label)Row.FindControl("lblLow");
                Label lblHigh = (Label)Row.FindControl("lblHigh");
                Label lblRatePerBl = (Label)Row.FindControl("lblRatePerBl");
                Label lblRatePerTEU = (Label)Row.FindControl("lblRatePerTEU");
                Label lblRatePerFEU = (Label)Row.FindControl("lblRatePerFEU");
                Label lblSharingBL = (Label)Row.FindControl("lblSharingBL");
                Label lblSharingTEU = (Label)Row.FindControl("lblSharingTEU");
                Label lblSharingFEU = (Label)Row.FindControl("lblSharingFEU");

                //ddlFLocation.SelectedIndex = ddlFLocation.Items.IndexOf(ddlFLocation.Items.FindByValue(hdnLocationId.Value));
                //PopulateDropDown((int)Enums.DropDownPopulationFor.TerminalCode, ddlFTerminal, Convert.ToInt32(ddlFLocation.SelectedValue));

                //ddlFTerminal.SelectedIndex = ddlFTerminal.Items.IndexOf(ddlFTerminal.Items.FindByValue(hdnTerminalId.Value));
                //ddlFWashingType.SelectedIndex = ddlFWashingType.Items.IndexOf(ddlFWashingType.Items.FindByValue(hdnWashingTypeId.Value));


                //txtLow.Text = lblLow.Text;
                //txtHigh.Text = lblHigh.Text;
                //txtRatePerBL.Text = lblRatePerBl.Text;
                //txtRatePerTEU.Text = lblRatePerTEU.Text;
                //txtRateperFEU.Text = lblRatePerFEU.Text;
                //txtSharingBL.Text = lblSharingBL.Text;
                //txtSharingTEU.Text = lblSharingTEU.Text;
                //txtSharingFEU.Text = lblSharingFEU.Text;
                //hdnFId.Value = hdnId.Value.ToString();
                //hdnFSlno.Value = Row.RowIndex.ToString();

            }
            #endregion

            #region Delete
            if (e.CommandArgument == "Delete")
            {
                GridViewRow Row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                HiddenField hdnId = (HiddenField)Row.FindControl("hdnBookingContainerID");
                //ChargeBLL.DeleteChargeRate(Convert.ToInt32(hdnId.Value));
                //FillChargeRate(Convert.ToInt32(hdnChargeID.Value));
                Containers = (List<IBookingContainer>)ViewState["BookingCntr"];
                //Rates[Row.RowIndex].RateActive = false;
                Containers.RemoveAt(Row.RowIndex);
                ViewState["BookingCntr"] = Containers;
                //if (Rates.Count <= 0 && hdnChargeID.Value == "0")
                //{
                //    EnableAllField();
                //    UpdatePanel1.Update();
                //}
                FillContainers();
                //FillRates();

            }
            #endregion

            #region Cancel
            if (e.CommandArgument == "Cancel")
            //if (e.CommandArgument == "Cancel")
            {
                GridViewRow Row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                TextBox txtHigh = (TextBox)Row.FindControl("txtHigh");
                TextBox txtLow = (TextBox)Row.FindControl("txtLow");
                DropDownList ddlFLocation = (DropDownList)Row.FindControl("ddlFLocation");
                DropDownList ddlFTerminal = (DropDownList)Row.FindControl("ddlFTerminal");
                DropDownList ddlFWashingType = (DropDownList)Row.FindControl("ddlFWashingType");

                TextBox txtRatePerBL = (TextBox)Row.FindControl("txtRatePerBL");
                TextBox txtRatePerTEU = (TextBox)Row.FindControl("txtRatePerTEU");
                TextBox txtRateperFEU = (TextBox)Row.FindControl("txtRateperFEU");
                TextBox txtSharingBL = (TextBox)Row.FindControl("txtSharingBL");
                TextBox txtSharingTEU = (TextBox)Row.FindControl("txtSharingTEU");
                TextBox txtSharingFEU = (TextBox)Row.FindControl("txtSharingFEU");
                HiddenField hdnFId = (HiddenField)Row.FindControl("hdnFId");
                HiddenField hdnFSlno = (HiddenField)Row.FindControl("hdnFSlno");

                txtHigh.Text = String.Empty;// "0";
                txtLow.Text = String.Empty;// "0";
                ddlFLocation.SelectedIndex = 0;
                if (ddlFTerminal.Items.Count > 0)
                {
                    //ddlFTerminal.SelectedIndex = 0;
                    ddlFTerminal.Items.Clear();
                    //ddlFTerminal.Enabled = false;
                }
                ddlFWashingType.SelectedIndex = 0;
                txtRatePerBL.Text = String.Empty;// "0.00";
                txtRatePerTEU.Text = String.Empty;//"0.00";
                txtRateperFEU.Text = String.Empty;//"0.00";
                txtSharingBL.Text = String.Empty;//"0.00";
                txtSharingTEU.Text = String.Empty;//"0.00";
                txtSharingFEU.Text = String.Empty;//"0.00";

                hdnFId.Value = "0";
                hdnFSlno.Value = "-1";
            }
            #endregion

            //TerminalSelection(rdbTerminalRequired);
            //WashingSelection(rdbWashing);
            //SharingSelection(rdbPrincipleSharing);
            //ShowHideControlofFooterOnEdit(ddlChargeType);
            ////DisableAllField();

        }

        void FillContainers()
        {
            Containers = (List<IBookingContainer>)ViewState["BookingCntr"];
            IEnumerable<IBookingContainer> Rts = from IBookingContainer rt in Containers
                                           where rt.BkCntrStatus == true
                                           select rt;

            gvContainer.DataSource = Rts.ToList();
            gvContainer.DataBind();

            if (Rts.Count() <= 0)
            {
                List<IBookingContainer> EmptyRates = new List<IBookingContainer>();
                IBookingContainer rt = new BookingContainerEntity();
                EmptyRates.Add(rt);

                gvContainer.DataSource = EmptyRates;
                gvContainer.DataBind();
                gvContainer.Rows[0].Visible = false;
            }


        }

        protected void gvContainer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //oBookingBll = new BookingBLL();
            //GridViewRow Row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

            //TextBox txtNos = (TextBox)Row.FindControl("txtNos");
            //TextBox txtWtPerCntr = (TextBox)Row.FindControl("txtWtPerCntr");
            //DropDownList ddlCntrType = (DropDownList)Row.FindControl("ddlCntrType");
            //DropDownList ddlSize = (DropDownList)Row.FindControl("ddlSize");

            DataTable dt;
            object otabSelItems = Session["dt"];

            if (otabSelItems is DataTable)

                dt = (DataTable)otabSelItems;
            else
            {
                dt = new DataTable();
                dt = CreateDataTable(dt);
            }

            lblMessage.Text = string.Empty;
            DataRow dr = dt.NewRow();

            //DataTable Dt = CreateDataTable();

            if ((String.IsNullOrEmpty(txtNos.Text) || String.IsNullOrEmpty(txtWtPerCntr.Text)))
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00077") + "');</script>", false);
                return;
            }
            Label lblContainerType = (Label)FindControl("lblContainerType");
            Label lblContainerSize = (Label)FindControl("lblContainerSize");
            Label lblUnit = (Label)FindControl("lblUnit");
            Label lblwtPerCont = (Label)FindControl("lblwtPerCont");
            HiddenField cntsize = (HiddenField)FindControl("hdnSize");

            dr["BookingID"] = hdnBookingID.Value;
            dr["CntrSize"] = lblSize.Text;
            dr["ContainerType"] = lblType.Text;
            dr["NoofContainers"] = lblUnit.Text;
            dr["wtPerCntr"] = lblWt.Text;
            dt.Rows.Add(dr);

            //oBookingContainerEntity = new BookingContainerEntity();

            //oBookingContainerEntity.BookingID = Convert.ToInt32(hdnBookingID.Value);
            //oBookingContainerEntity.CntrSize = Convert.ToString(ddlSize.SelectedValue);
            //oBookingContainerEntity.ContainerTypeID = Convert.ToInt32(ddlCntrType.SelectedValue);
            //oBookingContainerEntity.NoofContainers = Convert.ToInt32(txtNos.Text);
            //oBookingContainerEntity.wtPerCntr = Convert.ToInt32(txtWtPerCntr.Text);
            ViewState["BookingCntr"] = dt;
            //FillContainers();
        }


        private DataTable CreateDataTable(DataTable Dt)
        {
            Dt = new DataTable();
            DataColumn dc;

            dc = new DataColumn("BookingID");
            Dt.Columns.Add(dc);

            dc = new DataColumn("CntrSize");
            Dt.Columns.Add(dc);

            dc = new DataColumn("ContainerType");
            Dt.Columns.Add(dc);

            dc = new DataColumn("NoofContainers");
            Dt.Columns.Add(dc);

            dc = new DataColumn("wtPerCntr");
            Dt.Columns.Add(dc);

            return Dt;
        }

    }
}