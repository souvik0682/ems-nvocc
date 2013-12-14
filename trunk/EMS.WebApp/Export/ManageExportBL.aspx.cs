using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.BLL;
using EMS.Common;
using EMS.Utilities;
using System.Data;
using EMS.Entity;
using System.Text;

namespace EMS.WebApp.Export
{
    public partial class ManageExportBL : System.Web.UI.Page
    {
        private int _userId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private int _userLocation = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            
           RetriveParameters();
           _userId = UserBLL.GetLoggedInUserId();
           _userLocation = UserBLL.GetUserLocation();
           CheckUserAccess();


            if (!IsPostBack)
            {
                LoadShipmentModeDDL();
                //LoadDeliveryAgentDDL();

                if (!ReferenceEquals(Request.QueryString["BLNumber"], null))
                {
                    ViewState["ISEDIT"] = true;
                    txtBookingNo.Enabled = false;

                    string BLNumber = string.Empty;
                    BLNumber = GeneralFunctions.DecryptQueryString(Request.QueryString["BLNumber"].ToString());
                    //LoadDeliveryAgentDDL(bl);
                    LoadExportBLHeaderForEdit(BLNumber);
                }
                else
                {
                    ViewState["ISEDIT"] = false;
                    
                }
            }

            txtIssuePlace.TextChanged += new EventHandler(txtIssuePlace_TextChanged);
            txtBookingNo.TextChanged += new EventHandler(txtBookingNo_TextChanged);
            txtBLDate.TextChanged += new EventHandler(txtBLDate_TextChanged);
        }

        protected void ddlBLClause_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBLClause.SelectedValue == "R")
            {
                txtBLReleaseDate.Enabled = true;
                //rfvBLReleaseDate.Enabled = true;
            }
            else
            {
                txtBLReleaseDate.Text = "";
                //rfvBLReleaseDate.Enabled = false;
                txtBLReleaseDate.Enabled = false;
            }
        }

        protected void txtBLNo_TextChanged(object sender, EventArgs e)
        {
            txtCBLNo.Text = txtBLNo.Text;
        }

        void txtBLDate_TextChanged(object sender, EventArgs e)
        {
            txtCBLDate.Text = txtBLDate.Text;
        }

        void txtBookingNo_TextChanged(object sender, EventArgs e)
        {
            int Status;
            if (!string.IsNullOrEmpty(txtBookingNo.Text))
            {
                if (ExportBLBLL.CheckBookingLocation(txtBookingNo.Text, _userLocation) == true)
                {
                    Status = ExportBLBLL.CheckExpBLExistance(txtBookingNo.Text);
                    if (Status != 0)
                    {
                        LoadExportBLHeaderForAdd(txtBookingNo.Text, Status);
                        lblErr.Text = "";
                    }
                    else
                    {
                        lblErr.Text = "Error!!! Container(s) Missing or B/L Exists against Booking";
                        return;
                    }
                }
                else
                    rfvBookingNo.Visible = true;
            }
        }

        void txtIssuePlace_TextChanged(object sender, EventArgs e)
        {
            string IssuePlace = ((TextBox)txtIssuePlace.FindControl("txtPort")).Text;

            if (IssuePlace != string.Empty)
            {
                if (IssuePlace.Split('|').Length > 1)
                {
                    string portCode = IssuePlace.Split('|')[1].Trim();
                    int portId = new ImportBLL().GetPortId(portCode);
                    ViewState["BLISSUEID"] = portId;
                }
                else
                {
                    ViewState["BLISSUEID"] = null;
                }
            }
            else
            {
                ViewState["BLISSUEID"] = null;
            }
        }


        protected void gvwContainers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Part
                List<Part> lstPart = new List<Part>();
                lstPart.Add(new Part { Text = "Yes", Value = "True" });
                lstPart.Add(new Part { Text = "No", Value = "False" });

                // Bind drop down to Part
                DropDownList ddlPart = (DropDownList)e.Row.FindControl("ddlPart");
                ddlPart.DataTextField = "Text";
                ddlPart.DataValueField = "Value";
                ddlPart.DataSource = lstPart;
                ddlPart.DataBind();

                IExportBLContainer cntr = e.Row.DataItem as IExportBLContainer;
                ddlPart.SelectedValue = cntr.Part.ToString();

                //Unit
                List<Unit> lstUnit = new List<Unit>();
                DataTable dtUnits = ExportBLBLL.GetUnitsForExportBlContainer();

                foreach (DataRow dr in dtUnits.Rows)
                {
                    lstUnit.Add(new Unit { Value = dr[0].ToString(), Text = dr[1].ToString() });
                }

                DropDownList ddlUnit = (DropDownList)e.Row.FindControl("ddlUnit");
                ddlUnit.DataTextField = "Text";
                ddlUnit.DataValueField = "Value";
                ddlUnit.DataSource = lstUnit;
                ddlUnit.DataBind();

                ddlUnit.SelectedValue = cntr.Unit.ToString();

                ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");

                if ((e.Row.DataItem as IExportBLContainer).IsDeleted)
                {
                    btnRemove.ImageUrl = "~/Images/undelete.png";
                    btnRemove.Attributes.Add("onclick", "javascript:return confirm('Are you sure about undelete?');");
                    btnRemove.CommandName = "UnDelete";
                    btnRemove.ToolTip = "Undelete";
                }
                else
                {
                    btnRemove.ImageUrl = "~/Images/remove.png";
                    btnRemove.Attributes.Add("onclick", "javascript:return confirm('Are you sure about delete?');");
                    btnRemove.CommandName = "Delete";
                    btnRemove.ToolTip = "Delete";
                }
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            List<IExportBLContainer> lstData = ViewState["DataSource"] as List<IExportBLContainer>;
            int totalRows = gvwContainers.Rows.Count;

            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvwContainers.Rows[r];
                HiddenField hdnContainerId = (HiddenField)thisGridViewRow.FindControl("hdnContainerId");

                DropDownList ddlPart = (DropDownList)thisGridViewRow.FindControl("ddlPart");
                DropDownList ddlUnit = (DropDownList)thisGridViewRow.FindControl("ddlUnit");
                TextBox txtSealNo = (TextBox)thisGridViewRow.FindControl("txtSealNo");
                TextBox txtPackage = (TextBox)thisGridViewRow.FindControl("txtPackage");
                TextBox txtGrossWeight = (TextBox)thisGridViewRow.FindControl("txtGrossWeight");
                TextBox txtShippingBillNumber = (TextBox)thisGridViewRow.FindControl("txtShippingBillNumber");
                TextBox txtShippingBillDate = (TextBox)thisGridViewRow.FindControl("txtShippingBillDate");

                lstData.Where(d => d.ContainerId == Convert.ToInt64(hdnContainerId.Value))
                    .Select(d =>
                    {
                        d.Part = Convert.ToBoolean(ddlPart.SelectedValue);
                        d.Unit = ddlUnit.SelectedValue;
                        d.SealNumber = txtSealNo.Text.Trim();
                        d.Package = Convert.ToInt32(txtPackage.Text);
                        d.GrossWeight = Convert.ToDecimal(txtGrossWeight.Text);
                        d.ShippingBillNumber = txtShippingBillNumber.Text.Trim();
                        if (txtShippingBillDate.Text.Trim() != string.Empty)
                            d.ShippingBillDate = Convert.ToDateTime(txtShippingBillDate.Text.Trim());
                        return d;
                    }).ToList();
            }

            ImageButton btnRemove = (ImageButton)sender;
            GridViewRow gvContainerRow = (GridViewRow)btnRemove.Parent.Parent;
            HiddenField hdnContainerId2 = gvContainerRow.FindControl("hdnContainerId") as HiddenField;

            if (btnRemove.CommandName == "Delete")
            {
                lstData.Where(d => d.ContainerId == Convert.ToInt64(hdnContainerId2.Value))
                        .Select(d =>
                        {
                            d.IsDeleted = true;
                            return d;
                        }).ToList();

                btnRemove.Enabled = false;
            }
            else
            {
                lstData.Where(d => d.ContainerId == Convert.ToInt64(hdnContainerId2.Value))
                        .Select(d =>
                        {
                            d.IsDeleted = false;
                            return d;
                        }).ToList();

                btnRemove.Enabled = true;
            }

            ViewState["DataSource"] = lstData;
            LoadExportBLContainers(0, 0,1);
        }

        private void LoadExportBLContainers(long BookingId, long BLId, int Status)
        {
            try
            {
                bool isEdit = Convert.ToBoolean(ViewState["ISEDIT"]);

                List<IExportBLContainer> objData = new List<IExportBLContainer>();

                if (!ReferenceEquals(ViewState["DataSource"], null) && ((List<IExportBLContainer>)(ViewState["DataSource"])).Count > 0)
                {
                    objData = ViewState["DataSource"] as List<IExportBLContainer>;
                }
                else
                {
                    if (!isEdit)
                        objData = ExportBLBLL.GetExportBLContainersForAdd(BookingId, Status);
                    else
                        objData = ExportBLBLL.GetExportBLContainersForEdit(BLId);
                }

                //objData = objData.Where(d => d.IsDeleted == false).ToList();
                ViewState["DataSource"] = objData;

                gvwContainers.DataSource = objData;
                gvwContainers.DataBind();

                List<Unit> lstUnit = new List<Unit>();
                DataTable dtUnits = ExportBLBLL.GetUnitsForExportBlContainer();

                foreach (DataRow dr in dtUnits.Rows)
                {
                    lstUnit.Add(new Unit { Value = dr[0].ToString(), Text = dr[1].ToString() });
                }

                txtNetWt.Text = objData.Where(o => o.IsDeleted == false).Sum(o => o.GrossWeight).ToString(); // + " " + lstUnit.Where(u => u.Value == objData[0].Unit).ToList()[0].Text;
                txtContainers.Text = objData.Where(o => o.IsDeleted == false).Where(o => o.ContainerSize == "20").Count().ToString() + " x 20 - " + objData.Where(o => o.IsDeleted == false).Where(o => o.ContainerSize == "40").Count().ToString() + " x 40";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        private void LoadExportBLHeaderForAdd(string BookingNumber, int Status)
        {
            try
            {
                IExportBL exportBL = ExportBLBLL.GetExportBLHeaderInfoForAdd(BookingNumber);

                if (!ReferenceEquals(exportBL, null))
                {
                    ViewState["BOOKINGID"] = exportBL.BookingId;
                    txtBookingDate.Text = exportBL.BookingDate.ToString("dd-MM-yyyy");
                    txtBLDate.Text = exportBL.BLDate.ToString("dd-MM-yyyy");
                    //txtBLDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                    txtBLDate.Enabled = false;
                    txtBookingParty.Text = exportBL.BookingParty;
                    txtRefBookingNo.Text = exportBL.RefBookingNumber;
                    txtLocation.Text = exportBL.Location;
                    ViewState["LOCATIONID"] = exportBL.LocationId;
                    txtLine.Text = exportBL.Nvocc;
                    ViewState["NVOCCID"] = exportBL.NvoccId;
                    txtVessel.Text = exportBL.Vessel;
                    ViewState["VESSELID"] = exportBL.VesselId;
                    txtVoyage.Text = exportBL.Voyage;
                    ViewState["VOYAGEID"] = exportBL.VoyageId;
                    txtPOR.Text = exportBL.POR;
                    txtPorDesc.Text = exportBL.PORDesc;
                    txtPOL.Text = exportBL.POL;
                    txtPolDesc.Text = exportBL.POLDesc;
                    txtPOD.Text = exportBL.POD;
                    txtPodDesc.Text = exportBL.PODDesc;
                    txtFPOD.Text = exportBL.FPOD;
                    txtFPodDesc.Text = exportBL.FPODDesc;
                    txtCommodity.Text = exportBL.Commodity;
                    LoadDeliveryAgentDDL(exportBL.fk_FPOD);
                    txtBLReleaseDate.Enabled = false;

                    if (exportBL.BLthruEdge == false)
                    {
                        hdnBLThruEdge.Value = "0";
                        txtBLNo.Enabled = true;
                        //rfvShipper.Enabled = false;
                        rfvShipperName.Enabled = false;
                        //rfvConsignee.Enabled = false;
                        rfvConsigneeName.Enabled = false;
                        rfvGoodsDescription.Enabled = false;
                        rfvMarks.Enabled = false;
                        rfvNotify.Enabled = false;
                        //rfvNotifyName.Enabled = false;
                        rfvAgent.Enabled = false;


                    }
                    else
                    {
                        txtBLNo.Enabled = false;
                        hdnBLThruEdge.Value = "1";
                        //rfvShipper.Enabled = true;
                        rfvShipperName.Enabled = true;
                        //rfvConsignee.Enabled = true;
                        rfvConsigneeName.Enabled = true;
                        rfvGoodsDescription.Enabled = true;
                        rfvMarks.Enabled = true;
                        rfvNotify.Enabled = true;
                        //rfvNotifyName.Enabled = true;
                        rfvAgent.Enabled = true;
                    }
                    txtCBookingNo.Text = exportBL.BookingNumber;
                    txtCBookingDate.Text = exportBL.BookingDate.ToString("dd-MM-yyyy");
                    txtCBLDate.Text = txtBLDate.Text;

                    if (exportBL.TotalTon != null)
                        txtTon.Text = exportBL.TotalTon;
                    else
                        txtTon.Text = "0";

                    if (exportBL.TotalCBM != null)
                        txtCbm.Text = exportBL.TotalCBM;
                    else
                        txtCbm.Text = "0";

                    if (exportBL.TotalTEU != null)
                        txtTeu.Text = exportBL.TotalTEU.ToString();
                    else
                        txtTeu.Text = "0";

                    if (exportBL.TotalFEU != null)
                        txtFeu.Text = exportBL.TotalFEU.ToString();
                    else
                        txtFeu.Text = "0";

                    LoadExportBLContainers(exportBL.BookingId, 0, Status);
                }
                else
                    rfvBookingNo.Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadExportBLHeaderForEdit(string BLNumber)
        {
            try
            {
                IExportBL exportBL = ExportBLBLL.GetExportBLHeaderInfoForEdit(BLNumber);
                LoadDeliveryAgentDDL(exportBL.fk_FPOD);
                if (!ReferenceEquals(exportBL, null))
                {
                    txtBookingNo.Text = exportBL.BookingNumber;
                    ViewState["BOOKINGID"] = exportBL.BookingId;
                    txtBookingDate.Text = exportBL.BookingDate.ToString("dd-MM-yyyy");
                    txtBLDate.Enabled = false;
                    txtBLNo.Text = exportBL.BLNumber;
                    ViewState["BLID"] = exportBL.BLId;
                    txtBLDate.Text = exportBL.BLDate.ToString("dd-MM-yyyy");
                    txtBookingParty.Text = exportBL.BookingParty;
                    txtRefBookingNo.Text = exportBL.RefBookingNumber;
                    txtLocation.Text = exportBL.Location;
                    ViewState["LOCATIONID"] = exportBL.LocationId;
                    txtLine.Text = exportBL.Nvocc;
                    ViewState["NVOCCID"] = exportBL.NvoccId;
                    txtVessel.Text = exportBL.Vessel;
                    ViewState["VESSELID"] = exportBL.VesselId;
                    txtVoyage.Text = exportBL.Voyage;
                    ViewState["VOYAGEID"] = exportBL.VoyageId;
                    txtPOR.Text = exportBL.POR;
                    txtPorDesc.Text = exportBL.PORDesc;
                    txtPOL.Text = exportBL.POL;
                    txtPolDesc.Text = exportBL.POLDesc;
                    txtPOD.Text = exportBL.POD;
                    txtPodDesc.Text = exportBL.PODDesc;
                    txtFPOD.Text = exportBL.FPOD;
                    txtFPodDesc.Text = exportBL.FPODDesc;
                    txtCommodity.Text = exportBL.Commodity;
                    txtContainers.Text = exportBL.Containers;
                    rdoOriginal.SelectedValue = exportBL.NoOfBL.ToString();

                    if (ddlBLClause.SelectedValue == "R")
                        txtBLReleaseDate.Enabled = true;
                    else
                        txtBLReleaseDate.Enabled = false;

                    //rdoOriginal.SelectedIndex = exportBL.NoOfBL.ToInt();
                    rdoBLType.SelectedValue = exportBL.BLType;
                    ddlShipmentMode.SelectedValue = exportBL.ShipmentMode.ToString();
                    ((TextBox)txtIssuePlace.FindControl("txtPort")).Text = exportBL.BLIssuePlace;
                    ViewState["BLISSUEID"] = exportBL.BLIssuePlaceId;
                    TxtNtWt.Text = exportBL.NetWeight.ToString();
                    //txtNetWt.Text = exportBL.NetWeight.ToString();
                    txtBLReleaseDate.Text = exportBL.BLReleaseDate.ToString();
                    //txtBLReleaseDate.Text = exportBL.BLReleaseDate.ToString("dd-MM-yyyy");
                    if (hdnBLThruEdge.Value == "" || hdnBLThruEdge.Value == "0")
                    {
                        //if (exportBL.BLthruEdge == false)
                        txtBLNo.Enabled = true;
                        rfvShipperName.Enabled = false;
                        rfvConsigneeName.Enabled = false;
                        rfvGoodsDescription.Enabled = false;
                        rfvMarks.Enabled = false;
                        rfvNotify.Enabled = false;
                        rfvAgent.Enabled = false;
                    }
                    else
                    {
                        txtBLNo.Enabled = false;
                        rfvShipperName.Enabled = true;
                        rfvConsigneeName.Enabled = true;
                        rfvGoodsDescription.Enabled = true;
                        rfvMarks.Enabled = true;
                        rfvNotify.Enabled = true;
                        rfvAgent.Enabled = true;
                    }
                    txtShipperName.Text = exportBL.ShipperName;
                    txtShipper.Text = exportBL.Shipper;
                    txtConsignee.Text = exportBL.Consignee;
                    txtConsigneeName.Text = exportBL.ConsigneeName;
                    txtNotify.Text = exportBL.NotifyParty;
                    txtNotifyName.Text = exportBL.NotifyPartyName;
                    ddlBLClause.SelectedValue = exportBL.BLClause;
                    txtGoodsDescription.Text = exportBL.GoodDesc;
                    txtMarks.Text = exportBL.MarksNumnbers;
                    ddlAgent.SelectedValue = exportBL.AgentId.ToString();

                    txtCBookingNo.Text = exportBL.BookingNumber;
                    txtCBookingDate.Text = exportBL.BookingDate.ToString("dd-MM-yyyy");
                    txtCBLNo.Text = exportBL.BLNumber;
                    txtCBLDate.Text = exportBL.BLDate.ToString("dd-MM-yyyy");
                    txtTon.Text = exportBL.TotalTon;
                    txtCbm.Text = exportBL.TotalCBM;

                    if (exportBL.TotalTEU != null)
                        txtTeu.Text = exportBL.TotalTEU.ToString();
                    else
                        txtTeu.Text = "0";

                    if (exportBL.TotalFEU != null)
                        txtFeu.Text = exportBL.TotalFEU.ToString();
                    else
                        txtFeu.Text = "0";

                    LoadExportBLContainers(0, exportBL.BLId,0);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadDeliveryAgentDDL(int FPOD)
        {
            DataTable dt = ExportBLBLL.GetDeliveryAgents(FPOD);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dr["AgentId"] = "0";
                dr["AgentName"] = "--Select--";
                dt.Rows.InsertAt(dr, 0);

                ddlAgent.DataValueField = "AgentId";
                ddlAgent.DataTextField = "AgentName";
                ddlAgent.DataSource = dt;
                ddlAgent.DataBind();
            }
        }

        private void LoadShipmentModeDDL()
        {
            DataTable dt = ExportBLBLL.GetShipmentModes();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dr["ModeId"] = "0";
                dr["ModeName"] = "--Select--";
                dt.Rows.InsertAt(dr, 0);

                ddlShipmentMode.DataValueField = "ModeId";
                ddlShipmentMode.DataTextField = "ModeName";
                ddlShipmentMode.DataSource = dt;
                ddlShipmentMode.DataBind();
            }
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

                if (_canView == false)
                {
                    Response.Redirect("~/Unauthorized.aspx");
                }

                if (user.UserRole.Id != (int)UserRole.Admin)
                {
                   
                    //ddlLocation.Enabled = false;
                }
                else
                {
                    _userLocation = 0;
                    //ddlLocation.Enabled = true;
                }
                
                if (!_canEdit)
                    btnSave.Visible = false;
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private long SaveExportBLHeaderDetails()
        {
            IExportBL objBL = new ExportBLEntity();
            long exportBLId = 0;
            if (ddlBLClause.SelectedValue == "R" && txtBLReleaseDate.Text == string.Empty)
            {
                lblErr.Text = "RFS date is compulsory";
                return 0;
            }

            if (!Convert.ToBoolean(ViewState["ISEDIT"]))
                objBL.BLId = 0;
            else
                objBL.BLId = Convert.ToInt64(ViewState["BLID"]);

            objBL.LocationId = Convert.ToInt32(ViewState["LOCATIONID"]);
            objBL.NvoccId = Convert.ToInt32(ViewState["NVOCCID"]);
            objBL.BookingId = Convert.ToInt64(ViewState["BOOKINGID"]);
            objBL.BLIssuePlaceId = Convert.ToInt32(ViewState["BLISSUEID"]);

            if (hdnBLThruEdge.Value == "1")
            {
                objBL.BLNumber = string.Empty;
                objBL.BLthruEdge = true;
            }
            else
            {
                objBL.BLNumber = txtBLNo.Text;
                objBL.BLthruEdge = false;
            }
            objBL.BLDate = Convert.ToDateTime(txtBLDate.Text.Trim());
            objBL.PORDesc = txtPorDesc.Text.Trim();
            objBL.POLDesc = txtPolDesc.Text.Trim();
            objBL.PODDesc = txtPodDesc.Text.Trim();
            objBL.FPODDesc = txtFPodDesc.Text.Trim();
            objBL.BLIssuePlace = ((TextBox)txtIssuePlace.FindControl("txtPort")).Text.Trim();
            objBL.ShipperName = txtShipperName.Text.Trim();
            objBL.Shipper = txtShipper.Text.Trim();
            objBL.ConsigneeName = txtConsigneeName.Text.Trim();
            objBL.Consignee = txtConsignee.Text.Trim();
            objBL.NotifyPartyName = txtNotifyName.Text.Trim();
            objBL.NotifyParty = txtNotify.Text.Trim();
            objBL.GoodDesc = txtGoodsDescription.Text.Trim();
            objBL.MarksNumnbers = txtMarks.Text.Trim();
            objBL.ShipmentMode = Convert.ToInt32(ddlShipmentMode.SelectedValue);
            objBL.AgentId = Convert.ToInt32(ddlAgent.SelectedValue);
            objBL.CreatedBy = _userId;
            objBL.CreatedOn = DateTime.Now;
            objBL.ModifiedBy = _userId;
            objBL.ModifiedOn = DateTime.Now;
            objBL.BLClause = ddlBLClause.SelectedValue;
            objBL.BLType = rdoBLType.SelectedValue;
            objBL.NoOfBL = Convert.ToInt32(rdoOriginal.SelectedValue);
            objBL.NetWeight = Convert.ToDecimal(TxtNtWt.Text.Trim());
            //objBL.NetWeight = Convert.ToDecimal(txtNetWt.Text.Trim());
            if (ddlBLClause.SelectedValue == "R")
                objBL.BLReleaseDate = Convert.ToDateTime(txtBLDate.Text.Trim());
            else
                objBL.BLReleaseDate = null;

            exportBLId = ExportBLBLL.SaveExportBLHeader(objBL);

            return exportBLId;
        }

        private void SaveExportBLContainers(long exportBLId)
        {
            List<IExportBLContainer> lstData = new List<IExportBLContainer>();

            int totalRows = gvwContainers.Rows.Count;

            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvwContainers.Rows[r];
                HiddenField hdnContainerId = (HiddenField)thisGridViewRow.FindControl("hdnContainerId");

                DropDownList ddlPart = (DropDownList)thisGridViewRow.FindControl("ddlPart");
                DropDownList ddlUnit = (DropDownList)thisGridViewRow.FindControl("ddlUnit");
                TextBox txtSealNo = (TextBox)thisGridViewRow.FindControl("txtSealNo");
                TextBox txtPackage = (TextBox)thisGridViewRow.FindControl("txtPackage");
                TextBox txtGrossWeight = (TextBox)thisGridViewRow.FindControl("txtGrossWeight");
                TextBox txtShippingBillNumber = (TextBox)thisGridViewRow.FindControl("txtShippingBillNumber");
                TextBox txtShippingBillDate = (TextBox)thisGridViewRow.FindControl("txtShippingBillDate");

                lstData = ViewState["DataSource"] as List<IExportBLContainer>;

                lstData.Where(d => d.ContainerId == Convert.ToInt64(hdnContainerId.Value))
                    .Select(d =>
                    {
                        d.BLId = exportBLId;
                        d.BookingId = Convert.ToInt64(ViewState["BOOKINGID"]);
                        d.Part = Convert.ToBoolean(ddlPart.SelectedValue);
                        d.Unit = ddlUnit.SelectedValue;
                        d.SealNumber = txtSealNo.Text.Trim();
                        d.Package = Convert.ToInt32(txtPackage.Text);
                        d.GrossWeight = Convert.ToDecimal(txtGrossWeight.Text);
                        d.ShippingBillNumber = txtShippingBillNumber.Text.Trim();
                        if (txtShippingBillDate.Text != string.Empty)
                            d.ShippingBillDate = Convert.ToDateTime(txtShippingBillDate.Text.Trim());
                        else
                            d.ShippingBillDate = null;
                        return d;
                    }).ToList();
            }

            if (!Convert.ToBoolean(ViewState["ISEDIT"]))
                ExportBLBLL.InsertBLContainers(lstData);
            else
                ExportBLBLL.UpdateBLContainers(lstData);

            ViewState["DataSource"] = lstData;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                
                long exportBLId = SaveExportBLHeaderDetails();
                if (exportBLId == 0)
                    lblErr.Text = "Error Saving B/L";
                else
                {
                    SaveExportBLContainers(exportBLId);
                    Response.Redirect("~/Export/ExportBL.aspx");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Export/ExportBL.aspx");
        }

        protected void txtGrossWeight_TextChanged(object sender, EventArgs e)
        {
            //TextBox thisTextBox = (TextBox)sender;
            //GridViewRow thisGridViewRow = (GridViewRow)thisTextBox.Parent.Parent;

            List<IExportBLContainer> lstData = ViewState["DataSource"] as List<IExportBLContainer>;
            int totalRows = gvwContainers.Rows.Count;

            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvwContainers.Rows[r];
                HiddenField hdnContainerId = (HiddenField)thisGridViewRow.FindControl("hdnContainerId");

                DropDownList ddlPart = (DropDownList)thisGridViewRow.FindControl("ddlPart");
                DropDownList ddlUnit = (DropDownList)thisGridViewRow.FindControl("ddlUnit");
                TextBox txtSealNo = (TextBox)thisGridViewRow.FindControl("txtSealNo");
                TextBox txtPackage = (TextBox)thisGridViewRow.FindControl("txtPackage");
                TextBox txtGrossWeight = (TextBox)thisGridViewRow.FindControl("txtGrossWeight");
                TextBox txtShippingBillNumber = (TextBox)thisGridViewRow.FindControl("txtShippingBillNumber");
                TextBox txtShippingBillDate = (TextBox)thisGridViewRow.FindControl("txtShippingBillDate");

                lstData.Where(d => d.ContainerId == Convert.ToInt64(hdnContainerId.Value))
                    .Select(d =>
                    {
                        d.Part = Convert.ToBoolean(ddlPart.SelectedValue);
                        d.Unit = ddlUnit.SelectedValue;
                        d.SealNumber = txtSealNo.Text.Trim();
                        d.Package = Convert.ToInt32(txtPackage.Text);
                        d.GrossWeight = Convert.ToDecimal(txtGrossWeight.Text);
                        d.ShippingBillNumber = txtShippingBillNumber.Text.Trim();
                        if (txtShippingBillDate.Text.Trim() != string.Empty)
                            d.ShippingBillDate = Convert.ToDateTime(txtShippingBillDate.Text.Trim());
                        return d;
                    }).ToList();
            }
            ViewState["DataSource"] = lstData;
            LoadExportBLContainers(0, 0, 0);
        }
    }

    public class Part
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
    
    public class Unit
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
}