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

namespace EMS.WebApp.Farwarding.Transaction
{
    public partial class ManageJobBL : System.Web.UI.Page
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
            //CheckUserAccess();


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
                    btnClose.Visible = false;
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
                txtBLDate.Text = txtCBLDate.Text;
            }
        }

        protected void txtBLNo_TextChanged(object sender, EventArgs e)
        {
            txtCBLNo.Text = txtBLNo.Text;
            txtOBLNo.Text = txtBLNo.Text;
        }

        void txtBLDate_TextChanged(object sender, EventArgs e)
        {
            //txtCBLDate.Text = txtBLDate.Text;
        }

        void txtBookingNo_TextChanged(object sender, EventArgs e)
        {
            int Status;
            if (!string.IsNullOrEmpty(txtBookingNo.Text))
            {
                if (JobBLBLL.CheckBookingLocation(txtBookingNo.Text, _userLocation) == true)
                {
                    Status = JobBLBLL.CheckExpBLExistance(txtBookingNo.Text);
                    if (Status != 0)
                    {
                        LoadExportBLHeaderForAdd(txtBookingNo.Text, Status);
                        lblErr.Text = "";
                    }
                    else
                    {
                        lblErr.Text = "Error!!! Container(s) Missing or B/L Exists against Job";
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

                IJobBLContainer cntr = e.Row.DataItem as IJobBLContainer;
                ddlPart.SelectedValue = cntr.Part.ToString();

                if (cntr.Part)
                    ddlPart.Enabled = false;
                else
                    ddlPart.Enabled = true;

                //Unit
                List<Unit> lstUnit = new List<Unit>();
                DataTable dtUnits = JobBLBLL.GetUnitsForExportBlContainer();

                foreach (DataRow dr in dtUnits.Rows)
                {
                    lstUnit.Add(new Unit { Value = dr[0].ToString(), Text = dr[1].ToString() });
                }

                DropDownList ddlUnit = (DropDownList)e.Row.FindControl("ddlUnit");
                ddlUnit.DataTextField = "Text";
                ddlUnit.DataValueField = "Value";
                ddlUnit.DataSource = lstUnit;
                ddlUnit.DataBind();
                if (!string.IsNullOrEmpty(cntr.Unit))
                    ddlUnit.SelectedValue = cntr.Unit.ToString();

                //Container Size
                List<ContainerSize> lstSize = new List<ContainerSize>();
                lstSize.Add(new ContainerSize { Text = "20", Value = "20" });
                lstSize.Add(new ContainerSize { Text = "40", Value = "40" });

                // Bind drop down to Part
                DropDownList ddlContainerSize = (DropDownList)e.Row.FindControl("ddlContainerSize");
                ddlContainerSize.DataTextField = "Text";
                ddlContainerSize.DataValueField = "Value";
                ddlContainerSize.DataSource = lstSize;
                ddlContainerSize.DataBind();
                if (!string.IsNullOrEmpty(cntr.ContainerSize))
                    ddlContainerSize.SelectedValue = cntr.ContainerSize.ToString();
                else
                    ddlContainerSize.SelectedValue = "20";

                //ContainerType
                List<ContainerType> lstContainerType = new List<ContainerType>();
                DataTable dtContainerType = JobBLBLL.GetContainerType();

                foreach (DataRow dr in dtContainerType.Rows)
                {
                    lstContainerType.Add(new ContainerType { Value = dr[0].ToString(), Text = dr[1].ToString() });
                }

                DropDownList ddlContainerType = (DropDownList)e.Row.FindControl("ddlContainerType");
                ddlContainerType.DataTextField = "Text";
                ddlContainerType.DataValueField = "Value";
                ddlContainerType.DataSource = lstContainerType;
                ddlContainerType.DataBind();
                if (cntr.ContainerTypeId.ToString() != null)
                    ddlContainerType.SelectedValue = cntr.ContainerTypeId.ToString();

                CustomControls.CustomTextBox txtTareWeight = (CustomControls.CustomTextBox)e.Row.FindControl("txtTareWeight");
                string TareWeight = JobBLBLL.GetTareWeight(Convert.ToInt32(ddlContainerType.SelectedValue), Convert.ToDecimal(ddlContainerSize.SelectedValue));
                txtTareWeight.Text = TareWeight;

                ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");

                //if ((e.Row.DataItem as IJobBLContainer).IsDeleted)
                //{
                //    btnRemove.ImageUrl = "~/Images/undelete.png";
                //    btnRemove.Attributes.Add("onclick", "javascript:return confirm('Are you sure about undelete?');");
                //    btnRemove.CommandName = "UnDelete";
                //    btnRemove.ToolTip = "Undelete";
                //}
                //else
                //{
                btnRemove.ImageUrl = "~/Images/remove.png";
                btnRemove.Attributes.Add("onclick", "javascript:return confirm('Are you sure about delete?');");
                btnRemove.CommandName = "Delete";
                btnRemove.ToolTip = "Delete";
                //}
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            List<IJobBLContainer> lstData = ViewState["DataSource"] as List<IJobBLContainer>;
            int totalRows = gvwContainers.Rows.Count;

            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvwContainers.Rows[r];
                HiddenField hdnContainerId = (HiddenField)thisGridViewRow.FindControl("hdnContainerId");

                TextBox txtHireContainerNumber = (TextBox)thisGridViewRow.FindControl("txtHireContainerNumber");
                DropDownList ddlContainerSize = (DropDownList)thisGridViewRow.FindControl("ddlContainerSize");
                DropDownList ddlContainerType = (DropDownList)thisGridViewRow.FindControl("ddlContainerType");
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
                        d.HireContainerNumber = txtHireContainerNumber.Text.Trim();
                        d.ContainerSize = ddlContainerSize.SelectedValue;
                        d.ContainerTypeId = Convert.ToInt32(ddlContainerType.SelectedValue);
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
            //Souvik
            //else
            //{
            //    lstData.Where(d => d.ContainerId == Convert.ToInt64(hdnContainerId2.Value))
            //            .Select(d =>
            //            {
            //                d.IsDeleted = false;
            //                return d;
            //            }).ToList();

            //    btnRemove.Enabled = true;
            //}

            ViewState["DataSource"] = lstData;
            LoadExportBLContainers(0);
        }

        private void LoadExportBLContainers(long BLId)
        {
            try
            {
                bool isEdit = Convert.ToBoolean(ViewState["ISEDIT"]);

                List<IJobBLContainer> objData = new List<IJobBLContainer>();

                if (!ReferenceEquals(ViewState["DataSource"], null) && ((List<IJobBLContainer>)(ViewState["DataSource"])).Count > 0)
                {
                    objData = ViewState["DataSource"] as List<IJobBLContainer>;
                }
                else
                {
                    //Souvik
                    //if (!isEdit)
                    //{
                    //    objData = JobBLBLL.GetExportBLContainersForAdd(BookingId, Status);
                    //    if (JobBLBLL.CheckBookingBLContainer(BookingId, Status) == false && objData.Count() > 0)
                    //        btnSave.Visible = false;
                    //    else
                    //        if (_canEdit == true)
                    //            btnSave.Visible = true;
                    //        else
                    //            btnSave.Visible = false;
                    //}
                    //if (!isEdit)
                    //    objData.Add(new JobBLContainerEntity { ContainerId = -1, ContainerSize = "20", IsDeleted = false });
                    //else

                    if (isEdit)
                        objData = JobBLBLL.GetExportBLContainersForEdit(BLId);
                }

                List<IJobBLContainer> objDataGrid = new List<IJobBLContainer>();
                objDataGrid = objData.Where(d => d.IsDeleted == false).ToList();

                ViewState["DataSource"] = objData;

                gvwContainers.DataSource = objDataGrid;
                gvwContainers.DataBind();

                List<Unit> lstUnit = new List<Unit>();
                DataTable dtUnits = JobBLBLL.GetUnitsForExportBlContainer();

                foreach (DataRow dr in dtUnits.Rows)
                {
                    lstUnit.Add(new Unit { Value = dr[0].ToString(), Text = dr[1].ToString() });
                }
                if (hdnShipmentType.Value.ToString() == "0")
                    txtNetWt.Text = objData.Where(o => o.IsDeleted == false).Sum(o => o.GrossWeight).ToString(); // + " " + lstUnit.Where(u => u.Value == objData[0].Unit).ToList()[0].Text;

                txtContainers.Text = objData.Where(o => o.IsDeleted == false).Where(o => o.ContainerSize == "20").Count().ToString() + " x 20 - " + objData.Where(o => o.IsDeleted == false).Where(o => o.ContainerSize == "40").Count().ToString() + " x 40";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            List<IJobBLContainer> lstData = new List<IJobBLContainer>();

            if (!ReferenceEquals(ViewState["DataSource"], null))
            {
                lstData = ViewState["DataSource"] as List<IJobBLContainer>;

                int totalRows = gvwContainers.Rows.Count;

                for (int r = 0; r < totalRows; r++)
                {
                    GridViewRow thisGridViewRow = gvwContainers.Rows[r];
                    HiddenField hdnContainerId = (HiddenField)thisGridViewRow.FindControl("hdnContainerId");

                    TextBox txtHireContainerNumber = (TextBox)thisGridViewRow.FindControl("txtHireContainerNumber");
                    DropDownList ddlContainerSize = (DropDownList)thisGridViewRow.FindControl("ddlContainerSize");
                    DropDownList ddlContainerType = (DropDownList)thisGridViewRow.FindControl("ddlContainerType");
                    DropDownList ddlPart = (DropDownList)thisGridViewRow.FindControl("ddlPart");
                    DropDownList ddlUnit = (DropDownList)thisGridViewRow.FindControl("ddlUnit");
                    TextBox txtSealNo = (TextBox)thisGridViewRow.FindControl("txtSealNo");
                    TextBox txtPackage = (TextBox)thisGridViewRow.FindControl("txtPackage");
                    TextBox txtGrossWeight = (TextBox)thisGridViewRow.FindControl("txtGrossWeight");
                    TextBox txtShippingBillNumber = (TextBox)thisGridViewRow.FindControl("txtShippingBillNumber");
                    TextBox txtShippingBillDate = (TextBox)thisGridViewRow.FindControl("txtShippingBillDate");

                    string TareWeight = JobBLBLL.GetTareWeight(Convert.ToInt32(ddlContainerType.SelectedValue), Convert.ToDecimal(ddlContainerSize.SelectedValue));

                    lstData.Where(d => d.ContainerId == Convert.ToInt64(hdnContainerId.Value))
                        .Select(d =>
                        {
                            d.TareWeight = Convert.ToDecimal(TareWeight);
                            d.HireContainerNumber = txtHireContainerNumber.Text.Trim();
                            d.ContainerSize = ddlContainerSize.SelectedValue;
                            d.ContainerTypeId = Convert.ToInt32(ddlContainerType.SelectedValue);
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

                if (lstData.Count > 0)
                {
                    var min = lstData.Min(i => i.ContainerId);
                    lstData.Add(new JobBLContainerEntity { ContainerId = (min.ToInt() - 1), ContainerSize = "20", IsDeleted = false });
                }
                else
                {
                    lstData.Add(new JobBLContainerEntity { ContainerId = -1, ContainerSize = "20", IsDeleted = false });
                }

                List<IJobBLContainer> objDataGrid = new List<IJobBLContainer>();
                objDataGrid = lstData.Where(d => d.IsDeleted == false).ToList();

                ViewState["DataSource"] = lstData;

                gvwContainers.DataSource = objDataGrid;
                gvwContainers.DataBind();
            }
        }
        protected void ddlContainerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<IJobBLContainer> lstData = ViewState["DataSource"] as List<IJobBLContainer>;
            int totalRows = gvwContainers.Rows.Count;

            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvwContainers.Rows[r];
                HiddenField hdnContainerId = (HiddenField)thisGridViewRow.FindControl("hdnContainerId");

                TextBox txtHireContainerNumber = (TextBox)thisGridViewRow.FindControl("txtHireContainerNumber");
                DropDownList ddlContainerSize = (DropDownList)thisGridViewRow.FindControl("ddlContainerSize");
                DropDownList ddlContainerType = (DropDownList)thisGridViewRow.FindControl("ddlContainerType");
                DropDownList ddlPart = (DropDownList)thisGridViewRow.FindControl("ddlPart");
                DropDownList ddlUnit = (DropDownList)thisGridViewRow.FindControl("ddlUnit");
                TextBox txtSealNo = (TextBox)thisGridViewRow.FindControl("txtSealNo");
                TextBox txtPackage = (TextBox)thisGridViewRow.FindControl("txtPackage");
                TextBox txtGrossWeight = (TextBox)thisGridViewRow.FindControl("txtGrossWeight");
                TextBox txtShippingBillNumber = (TextBox)thisGridViewRow.FindControl("txtShippingBillNumber");
                TextBox txtShippingBillDate = (TextBox)thisGridViewRow.FindControl("txtShippingBillDate");

                string TareWeight = JobBLBLL.GetTareWeight(Convert.ToInt32(ddlContainerType.SelectedValue), Convert.ToDecimal(ddlContainerSize.SelectedValue));

                lstData.Where(d => d.ContainerId == Convert.ToInt64(hdnContainerId.Value))
                    .Select(d =>
                    {
                        d.TareWeight = Convert.ToDecimal(TareWeight);
                        d.HireContainerNumber = txtHireContainerNumber.Text.Trim();
                        d.ContainerSize = ddlContainerSize.SelectedValue;
                        d.ContainerTypeId = Convert.ToInt32(ddlContainerType.SelectedValue);
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
            LoadExportBLContainers(0);
        }

        protected void ddlContainerSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<IJobBLContainer> lstData = ViewState["DataSource"] as List<IJobBLContainer>;
            int totalRows = gvwContainers.Rows.Count;

            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvwContainers.Rows[r];
                HiddenField hdnContainerId = (HiddenField)thisGridViewRow.FindControl("hdnContainerId");

                TextBox txtHireContainerNumber = (TextBox)thisGridViewRow.FindControl("txtHireContainerNumber");
                DropDownList ddlContainerSize = (DropDownList)thisGridViewRow.FindControl("ddlContainerSize");
                DropDownList ddlContainerType = (DropDownList)thisGridViewRow.FindControl("ddlContainerType");
                DropDownList ddlPart = (DropDownList)thisGridViewRow.FindControl("ddlPart");
                DropDownList ddlUnit = (DropDownList)thisGridViewRow.FindControl("ddlUnit");
                TextBox txtSealNo = (TextBox)thisGridViewRow.FindControl("txtSealNo");
                TextBox txtPackage = (TextBox)thisGridViewRow.FindControl("txtPackage");
                TextBox txtGrossWeight = (TextBox)thisGridViewRow.FindControl("txtGrossWeight");
                TextBox txtShippingBillNumber = (TextBox)thisGridViewRow.FindControl("txtShippingBillNumber");
                TextBox txtShippingBillDate = (TextBox)thisGridViewRow.FindControl("txtShippingBillDate");

                string TareWeight = JobBLBLL.GetTareWeight(Convert.ToInt32(ddlContainerType.SelectedValue), Convert.ToDecimal(ddlContainerSize.SelectedValue));

                lstData.Where(d => d.ContainerId == Convert.ToInt64(hdnContainerId.Value))
                    .Select(d =>
                    {
                        d.TareWeight = Convert.ToDecimal(TareWeight);
                        d.HireContainerNumber = txtHireContainerNumber.Text.Trim();
                        d.ContainerSize = ddlContainerSize.SelectedValue;
                        d.ContainerTypeId = Convert.ToInt32(ddlContainerType.SelectedValue);
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
            LoadExportBLContainers(0);
        }

        private void LoadExportBLHeaderForAdd(string BookingNumber, int Status)
        {
            try
            {
                IJobBL exportBL = JobBLBLL.GetExportBLHeaderInfoForAdd(BookingNumber);

                if (!ReferenceEquals(exportBL, null))
                    if (exportBL.BookingId != 0)
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
                        hdnShipmentType.Value = exportBL.ShipmentType.ToString();
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
                        txtOJobNo.Text = exportBL.BookingNumber;
                        txtOJobDate.Text = exportBL.BookingDate.ToString("dd-MM-yyyy");
                        txtOBLDate.Text = txtBLDate.Text;

                        if (exportBL.ShipmentType != 0)
                        {
                            txtNetWt.Enabled = true;
                            txtNetWt.Text = exportBL.GrossWeight.ToString();
                        }
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

                        LoadExportBLContainers(0);
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
                IJobBL exportBL = JobBLBLL.GetExportBLHeaderInfoForEdit(BLNumber);
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
                    hdnShipmentType.Value = exportBL.ShipmentType.ToString();
                    ddlBLClause.SelectedValue = exportBL.BLClause;

                    if (exportBL.ShipmentType != 0)
                    {
                        txtNetWt.Enabled = true;
                        txtNetWt.Text = exportBL.GrossWeight.ToString();
                    }
                    if (ddlBLClause.SelectedValue == "R")
                    {
                        if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
                        {
                            IUser user = (IUser)Session[Constants.SESSION_USER_INFO];
                            if (user.UserRole.Id == (int)UserRole.Admin)
                            {
                                btnClose.Visible = true;
                                if (exportBL.CloseBL == true)
                                    btnClose.Text = "Open";
                                else
                                    btnClose.Text = "Close";
                            }
                            else
                            {
                                btnClose.Visible = false;
                            }
                        }
                        txtBLReleaseDate.Enabled = true;
                    }
                    else
                    {
                        txtBLReleaseDate.Enabled = false;
                        btnClose.Visible = false;
                    }

                    rdoBLType.SelectedValue = exportBL.BLType;
                    ddlShipmentMode.SelectedValue = exportBL.ShipmentMode.ToString();
                    ((TextBox)txtIssuePlace.FindControl("txtPort")).Text = exportBL.BLIssuePlace;
                    ViewState["BLISSUEID"] = exportBL.BLIssuePlaceId;
                    TxtNtWt.Text = exportBL.NetWeight.ToString();
                    txtBLReleaseDate.Text = exportBL.BLReleaseDate.ToString();
                    if (hdnBLThruEdge.Value == "" || hdnBLThruEdge.Value == "0")
                    {
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

                    if (exportBL.CloseVoyage == true)
                        btnSave.Visible = false;

                    if (exportBL.CloseBL == true)
                        btnSave.Visible = false;

                    LoadExportBLContainers(exportBL.BLId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadDeliveryAgentDDL(int FPOD)
        {
            DataTable dt = JobBLBLL.GetDeliveryAgents(FPOD);

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
            DataTable dt = JobBLBLL.GetShipmentModes();

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

                if (user.UserRole.Id != (int)UserRole.Admin && user.UserRole.Id != (int)UserRole.Manager)
                {

                    //ddlLocation.Enabled = false;
                }
                else
                {
                    _userLocation = 0;
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
            IJobBL objBL = new JobBLEntity();

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
            objBL.GrossWeight = Convert.ToDecimal(txtNetWt.Text.Trim());
            objBL.Voyage = txtVessel.Text.Trim();
            objBL.Vessel = txtVessel.Text.Trim();

            //objBL.NetWeight = Convert.ToDecimal(txtNetWt.Text.Trim());
            if (ddlBLClause.SelectedValue == "R")
                objBL.BLReleaseDate = Convert.ToDateTime(txtBLReleaseDate.Text.Trim());
            else
                objBL.BLReleaseDate = null;

            exportBLId = JobBLBLL.SaveExportBLHeader(objBL);

            return exportBLId;
        }

        private void SaveExportBLContainers(long exportBLId)
        {
            List<IJobBLContainer> lstData = new List<IJobBLContainer>();

            int totalRows = gvwContainers.Rows.Count;

            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvwContainers.Rows[r];
                HiddenField hdnContainerId = (HiddenField)thisGridViewRow.FindControl("hdnContainerId");

                TextBox txtHireContainerNumber = (TextBox)thisGridViewRow.FindControl("txtHireContainerNumber");
                DropDownList ddlContainerSize = (DropDownList)thisGridViewRow.FindControl("ddlContainerSize");
                DropDownList ddlContainerType = (DropDownList)thisGridViewRow.FindControl("ddlContainerType");
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
                        d.HireContainerNumber = txtHireContainerNumber.Text.Trim();
                        d.ContainerSize = ddlContainerSize.SelectedValue;
                        d.ContainerTypeId = Convert.ToInt32(ddlContainerType.SelectedValue);
                        d.Part = Convert.ToBoolean(ddlPart.SelectedValue);
                        d.Unit = ddlUnit.SelectedValue;
                        d.SealNumber = txtSealNo.Text.Trim();
                        d.Package = Convert.ToInt32(txtPackage.Text);
                        d.GrossWeight = Convert.ToDecimal(txtGrossWeight.Text);
                        d.ShippingBillNumber = txtShippingBillNumber.Text.Trim();
                        if (txtShippingBillDate.Text.Trim() != string.Empty)
                            d.ShippingBillDate = Convert.ToDateTime(txtShippingBillDate.Text.Trim());
                        else
                            d.ShippingBillDate = null;
                        return d;
                    }).ToList();
            }

            if (!Convert.ToBoolean(ViewState["ISEDIT"]))
                JobBLBLL.InsertBLContainers(lstData);
            else
                JobBLBLL.UpdateBLContainers(lstData);

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
                    Response.Redirect("~/Forwarding/Transaction/JobBL.aspx");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forwarding/Transaction/JobBL.aspx");
        }

        protected void txtGrossWeight_TextChanged(object sender, EventArgs e)
        {
            //TextBox thisTextBox = (TextBox)sender;
            //GridViewRow thisGridViewRow = (GridViewRow)thisTextBox.Parent.Parent;

            List<IJobBLContainer> lstData = ViewState["DataSource"] as List<IJobBLContainer>;
            int totalRows = gvwContainers.Rows.Count;

            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvwContainers.Rows[r];
                HiddenField hdnContainerId = (HiddenField)thisGridViewRow.FindControl("hdnContainerId");

                TextBox txtHireContainerNumber = (TextBox)thisGridViewRow.FindControl("txtHireContainerNumber");
                DropDownList ddlContainerSize = (DropDownList)thisGridViewRow.FindControl("ddlContainerSize");
                DropDownList ddlContainerType = (DropDownList)thisGridViewRow.FindControl("ddlContainerType");
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
                        d.HireContainerNumber = txtHireContainerNumber.Text.Trim();
                        d.ContainerSize = ddlContainerSize.SelectedValue;
                        d.ContainerTypeId = Convert.ToInt32(ddlContainerType.SelectedValue);
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
            LoadExportBLContainers(0);
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            long exportBLId = 0;

            exportBLId = Convert.ToInt64(ViewState["BLID"]);

            JobBLBLL.CloseOpenBL(exportBLId, _userId, btnClose.Text);
            Response.Redirect("~/Forwarding/Transaction/JobBL.aspx");
        }

        protected void txtBLReleaseDate_TextChanged(object sender, EventArgs e)
        {
            if (ddlBLClause.SelectedValue == "R")
                txtBLDate.Text = txtBLReleaseDate.Text;
            else
                txtBLDate.Text = txtCBLDate.Text;
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

    public class ContainerType
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }

    public class ContainerSize
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
}