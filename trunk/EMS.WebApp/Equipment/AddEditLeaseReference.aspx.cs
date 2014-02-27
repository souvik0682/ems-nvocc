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

namespace EMS.WebApp.Equipment
{
    public partial class AddEditLeaseReference : System.Web.UI.Page
    {
        List<BookingContainerEntity> oBookingContainers;
        BookingContainerEntity oBookingContainerEntity;
        LeaseEntity oBookingEntity;
        LeaseBLL oBookingBll;
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
        private int _userLocation = 0;
  
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            _userId = UserBLL.GetLoggedInUserId();
            _userLocation = UserBLL.GetUserLocation();

            if (!Page.IsPostBack)
            {
                fillAllDropdown();
                CheckUserAccess(hdnLeaseID.Value);
                if (hdnLeaseID.Value == "0" || hdnLeaseID.Value == string.Empty)
                {
                    //txtImo.Enabled = false;
                    //txtUno.Enabled = false;
                    //txtTempMax.Enabled = false;
                    //txtTempMin.Enabled = false;
                    //txtImo.Text = "ZZZ";
                    //txtUno.Text = "ZZZZZ";
                    //oBookingContainers = new List<BookingContainerEntity>();
                    //BookingContainerEntity Ent = new BookingContainerEntity();
                    //oBookingContainers.Add(Ent);
                    //gvContainer.DataSource = oBookingContainers;
                    //gvContainer.DataBind();
                    //gvContainer.Rows[0].Visible = false;
                    //checkTransitRoot();
                }
                else
                {
                    FillLease(Convert.ToInt32(hdnLeaseID.Value));
                    FillLeaseContainer(Convert.ToInt32(hdnLeaseID.Value));
                    //FillBookingTranshipment(Convert.ToInt32(hdnBookingID.Value));
                    //CheckForBookingCharges(Convert.ToInt32(hdnBookingID.Value));
                    //LoadModalPortDDL();
                }
            }

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

                if (user.UserRole.Id != (int)UserRole.Admin)
                {
                    ddlLocation.SelectedValue = _userLocation.ToString();
                    ddlLocation.Enabled = false;
                    ddlLocation_SelectedIndexChanged(null, null);
                    //ActionOnFromLocationChange();

                }
                else
                    _userLocation = 0;

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
            Li = new ListItem("SELECT", "0");
            ddlLocation.Items.Insert(0, Li);
            //Li = new ListItem("ALL", "-1");
            //ddlLocation.Items.Insert(1, Li);
            #endregion

            #region Line

            PopulateDropDown((int)Enums.DropDownPopulationFor.Line, ddlNvocc, 0);
            Li = new ListItem("SELECT", "0");
            ddlNvocc.Items.Insert(0, Li);

            #endregion

            #region Party Master

            ////Li = new ListItem("ALL", "-1");
            ////GeneralFunctions.PopulateDropDownList(ddlBookingParty, dbinteract.PopulateDDLDS("DSR.dbo.mstCustomer", "pk_CustID", "CustName", true), false);
            //GeneralFunctions.PopulateDropDownList(ddlBookingParty, dbinteract.PopulateDDLDS("dsr.dbo.mstCustomer", "pk_CustID", "CustName", "Where Active='Y' and (CorporateorLocal='C' OR fk_LocID=" + ddlLocation.SelectedValue, true), false);
            ////PopulateDropDown((int)Enums.DropDownPopulationFor.ExpBookingParty, ddlBookingParty, 0);
            ////ddlBookingParty.Items.Insert(0, Li);

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

        private void FillLease(Int32 LeaseID)
        {
            //oBookingEntity = new BookingEntity();
            //oBookingBll = new BookingBLL();
            //oBookingEntity = (BookingEntity)oBookingBll.GetBooking(BookingID);

            LeaseEntity oBooking = (LeaseEntity)LeaseBLL.GetLease(Convert.ToInt32(hdnLeaseID.Value));

            //ddlLocation.SelectedIndexChanged += new EventHandler(ddlLocation_SelectedIndexChanged);
            //ddlFromLocation.SelectedIndex = Convert.ToInt32(ddlFromLocation.Items.IndexOf(ddlFromLocation.Items.FindByValue(oImportHaulage.LocationFrom)));
            ddlLocation.SelectedValue = oBooking.LocationID.ToString();
            ddlLocation_SelectedIndexChanged(null, null);
            ddlEmptyYard.SelectedValue = oBooking.fk_EmptyYardID.ToString();

            //hdnFPOD.Value = oBooking.FPODID.ToString();
            //hdnPOD.Value = oBooking.PODID.ToString();
            //hdnPOL.Value = oBooking.POLID.ToString();
            //hdnPOR.Value = oBooking.PORID.ToString();
            //hdnVessel.Value = oBooking.VesselID.ToString();
            //hdnMainLineVessel.Value = oBooking.MainLineVesselID.ToString();
            ddlNvocc.SelectedValue = oBooking.LinerID.ToString();
            ddlLocation.SelectedValue = oBooking.LocationID.ToString();
            txtLeaseCompany.Text = oBooking.LeaseCompany.ToString();
            txtLeaseDate.Text = oBooking.LeaseDate.ToShortDateString();
            txtLeaseNo.Text = oBooking.LeaseNo.ToString();
            txtLeaseValidity.Text = oBooking.LeaseValidTill.ToShortDateString();
            hdnLeaseID.Value = Convert.ToString(oBooking.LeaseID);

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
                hdnLeaseID.Value = _locId.ToString();
            }
            _userId = EMS.BLL.UserBLL.GetLoggedInUserId();

            EMS.BLL.UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Equipment/LeaseReference.aspx");
        }

 
        void ClearAll()
        {

            ddlLocation.SelectedIndex = -1;
            ddlNvocc.SelectedIndex = 0;
            txtLeaseCompany.Text = string.Empty;
            txtLeaseCompany.Text = string.Empty;
            txtLeaseDate.Text = string.Empty;
            txtLeaseNo.Text = string.Empty;
            txtLeaseValidity.Text = string.Empty;
            txtContainerDtls.Text = string.Empty;
            ViewState["BookingCntr"] = null;

            gvContainer.DataSource = null;
            gvContainer.DataBind();
            hdnLeaseID.Value = "0";
    
        }

        protected void lnkContainerDtls_Click(object sender, EventArgs e)
        {

            //DataTable Dt = CreateDataTable();
            ModalPopupExtender1.Show();
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
                

                if (ViewState["BookingCntr"] != null)
                    Containers = (List<IBookingContainer>)ViewState["BookingCntr"];
               
                oBookingBll = new LeaseBLL();
                oBookingEntity = new LeaseEntity();
                if (hdnLeaseID.Value == "0" || hdnLeaseID.Value == string.Empty)
                   
                    if (oBookingBll.CheckForDuplicateLease(txtLeaseNo.Text) != 0)
                    {
                        lblError.Text = "Duplicate Lease No. Entry rejected";
                        return;
                    }


                //oUserEntity = (UserEntity)Session[Constants.SESSION_USER_INFO]; // This section has been commented temporarily

                oBookingEntity.LinerID = ddlNvocc.SelectedValue.ToInt();
                oBookingEntity.LocationID = ddlLocation.SelectedValue.ToInt();
                oBookingEntity.LeaseID = Convert.ToInt32(hdnLeaseID.Value);
                oBookingEntity.LeaseNo = Convert.ToString(txtLeaseNo.Text);
                oBookingEntity.LeaseValidTill = Convert.ToDateTime(txtLeaseValidity.Text);
                oBookingEntity.LeaseCompany = txtLeaseCompany.Text.ToString();
                oBookingEntity.LeaseDate = txtLeaseDate.Text.ToDateTime();
                oBookingEntity.fk_EmptyYardID = ddlEmptyYard.SelectedValue.ToInt();
    

                if (hdnLeaseID.Value == "0") // Insert
                {
                    oBookingEntity.CreatedBy = _userId;// oUserEntity.Id;
                    oBookingEntity.CreatedOn = DateTime.Today.Date;
                    oBookingEntity.ModifiedBy = _userId;// oUserEntity.Id;
                    oBookingEntity.ModifiedOn = DateTime.Today.Date;
                    int outLeaseId = 0;
                    switch (oBookingBll.AddEditLease(oBookingEntity, _CompanyId, ref outLeaseId))
                      {
                        case -1: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00076");
                            break;
                        case 0: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00011");
                            ClearAll();
                            break;
                        case 1:
                            oBookingBll.DeactivateAllContainersAgainstLeaseId(outLeaseId);
                            switch (AddContainers(outLeaseId))
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
                    oBookingEntity.LeaseID = Convert.ToInt32(hdnLeaseID.Value);
                    oBookingEntity.ModifiedBy = _userId;// oUserEntity.Id;
                    oBookingEntity.ModifiedOn = DateTime.Today.Date;
                    oBookingEntity.Action = true;
                    //
                    int outLeaseId = 0;
                    switch (oBookingBll.AddEditLease(oBookingEntity, _CompanyId, ref outLeaseId))
                    {
                        case -1: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00076");
                            break;
                        case 0: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00011");
                            break;
                        case 1:
                            oBookingBll.DeactivateAllContainersAgainstLeaseId(outLeaseId);
                            switch (AddContainers(outLeaseId))
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
                Response.Redirect("~/Equipment/LeaseReference.aspx");
            }
        }

        protected void gvContainer_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                btnEdit.Visible = false;
                
                if (BookingBLL.GetBookingChargeExists(Convert.ToInt32(hdnLeaseID.Value)) != string.Empty)
                {
                    btnRemove.Visible = false;
                }
            }
        }

        protected void ddlNvocc_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddlLocation_SelectedIndexChanged(null, null);
            //if (hdnFPOD.Value != "")
            //    PopulateSevices(ddlNvocc.SelectedValue.ToInt(), Convert.ToInt32(hdnFPOD.Value));

            //if (BookingBLL.GetBLFromEDGE(ddlNvocc.SelectedValue.ToInt()).ToString() == "False")
            //    rdoBLThruEdge.SelectedValue = "No";
            //else
            //    rdoBLThruEdge.SelectedValue = "Yes";
        }

        void AddContainers()
        {
            Containers = (List<IBookingContainer>)ViewState["BookingCntr"];
            if (Containers == null)
                Containers = new List<IBookingContainer>();
            oBookingBll = new LeaseBLL();

            oBookingBll.DeactivateAllContainersAgainstLeaseId(Convert.ToInt32(hdnLeaseID.Value));

            foreach (IBookingContainer Container in Containers)
            {
                Container.BookingID = Convert.ToInt32(hdnLeaseID.Value);
                oBookingBll.AddEditLeaseContainer(Container);
            }
        }

        void FillLeaseContainer(Int32 LeaseID)
        {
            //oChargeRates = new List<ChargeRateEntity>();
            //oChargeRates = new List<ChargeRateEntity>();
            oBookingBll = new LeaseBLL();
            Containers = new List<IBookingContainer>();
            Containers = oBookingBll.GetLeaseContainers(LeaseID);
            //foreach (BookingContainerEntity obj in Containers)
            //{
            //    _TotWeight = _TotWeight + obj.NoofContainers.ToDecimal() * obj.wtPerCntr.ToDecimal();
            //}

            //txtGrossWeight.Text = _TotWeight.ToString();


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

            dc = new DataColumn("LeaseContainerID");
            Dt.Columns.Add(dc);

            dc = new DataColumn("ContainerTypeID");
            Dt.Columns.Add(dc);

            dc = new DataColumn("ContainerType");
            Dt.Columns.Add(dc);

            dc = new DataColumn("CntrSize");
            Dt.Columns.Add(dc);

            dc = new DataColumn("NoofContainers");
            Dt.Columns.Add(dc);

            //dc = new DataColumn("wtPerCntr");
            //Dt.Columns.Add(dc);

            return Dt;
        }

        protected void btnimgSave_Click(object sender, ImageClickEventArgs e)
        {
            if (ViewState["BookingCntr"] != null)
                Containers = (List<IBookingContainer>)ViewState["BookingCntr"];
            //IEnumerable<IBookingContainer> Rts = from IBookingContainer rt in Containers
            //                                     where rt.BkCntrStatus == true
            //                                     select rt;
            //_TotWeight = 0;
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
                    else
                    {
                        //_TotWeight = _TotWeight + (objBookingContainer.NoofContainers.ToDecimal() * objBookingContainer.wtPerCntr.ToDecimal());
                    }
                }
            }

            if ((String.IsNullOrEmpty(txtNos.Text)))  // || String.IsNullOrEmpty(txtWtPerCntr.Text)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00077") + "');</script>", false);
                return;
            }

            obj.BookingContainerID = Convert.ToInt32(hdnLeaseContainerID.Value);
            obj.ContainerTypeID = Convert.ToInt32(ddlCntrType.SelectedValue);
            obj.ContainerType = ddlCntrType.SelectedItem.Text;
            obj.CntrSize = ddlSize.SelectedValue;
            obj.NoofContainers = Convert.ToInt32(txtNos.Text);
            //obj.wtPerCntr = Convert.ToDecimal(txtWtPerCntr.Text);
            //_TotWeight = _TotWeight + (obj.NoofContainers.ToDecimal() * obj.wtPerCntr.ToDecimal());
            if (string.IsNullOrEmpty(hdnIndex.Value))
                Containers.Add(obj);

            gvContainer.DataSource = Containers;
            gvContainer.DataBind();
            //txtGrossWeight.Text = _TotWeight.ToString();

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
            hdnLeaseContainerID.Value = "0";
            hdnIndex.Value = string.Empty;
            ddlCntrType.SelectedIndex = 0;
            ddlSize.SelectedIndex = 0;
            txtNos.Text = string.Empty;
            //txtWtPerCntr.Text = string.Empty;
        }

        protected void gvContainer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int RowIndex = row.RowIndex;

            if (e.CommandName == "EditGrid")
            {
                HiddenField gvhdnLeaseContainerID = (HiddenField)gvContainer.Rows[RowIndex].FindControl("gvhdnLeaseContainerID");
                HiddenField gvhdnContainerTypeId = (HiddenField)gvContainer.Rows[RowIndex].FindControl("gvhdnContainerTypeId");
                Label lblContainerSize = (Label)gvContainer.Rows[RowIndex].FindControl("lblContainerSize");
                Label lblUnit = (Label)gvContainer.Rows[RowIndex].FindControl("lblUnit");
                //Label lblwtPerCont = (Label)gvContainer.Rows[RowIndex].FindControl("lblwtPerCont");

                hdnIndex.Value = RowIndex.ToString();
                hdnLeaseContainerID.Value = gvhdnLeaseContainerID.Value;
                ddlCntrType.SelectedValue = gvhdnContainerTypeId.Value;
                ddlSize.SelectedValue = lblContainerSize.Text;
                txtNos.Text = lblUnit.Text;
                //txtWtPerCntr.Text = lblwtPerCont.Text;
            }
            else if (e.CommandName == "Remove")
            {
                if (ViewState["BookingCntr"] != null)
                    Containers = (List<IBookingContainer>)ViewState["BookingCntr"];

                if (Containers.Count > 0)
                {
                    //dt = (DataTable)otabSelItems;
                    Containers.RemoveAt(RowIndex);
                    //dt.Rows.RemoveAt(RowIndex);
                    //dt.AcceptChanges();
                    //foreach (BookingContainerEntity obj in Containers)
                    //{
                    //    _TotWeight = _TotWeight + obj.NoofContainers.ToDecimal() * obj.wtPerCntr.ToDecimal();
                    //}

                    //txtGrossWeight.Text = _TotWeight.ToString();

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
                foreach (BookingContainerEntity obj in Containers)
                {
                    oBookingBll = new LeaseBLL();
         
                    obj.BookingID = BookingId;
            
                    int res = oBookingBll.AddEditLeaseContainer(obj);
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
                        txtContainerDtls.Text = obj.NoofContainers.ToString() + " X " + obj.CntrSize + "'" + obj.ContainerType;
                    else
                        txtContainerDtls.Text += "," + obj.NoofContainers.ToString() + " X " + obj.CntrSize + "'" + obj.ContainerType;
                }
            }
        }

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem Li = null;
            ddlEmptyYard.Items.Clear();
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            GeneralFunctions.PopulateDropDownList(ddlEmptyYard, dbinteract.PopulateDDLDS("dbo.mstAddress", "fk_AddressID", "AddrName", "Where (AddrActive=1 and AddrType='3' and fk_LocationID=" + ddlLocation.SelectedValue, true));

            //Li = new ListItem("SELECT", "0");
            //ddlBookingParty.Items.Insert(0, Li);
        }

        protected void ddlEmptyYard_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}