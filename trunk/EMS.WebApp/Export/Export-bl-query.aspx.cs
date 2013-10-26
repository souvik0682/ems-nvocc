using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EMS.BLL;
using EMS.Utilities;
using EMS.Utilities.ResourceManager;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data.SqlTypes;
using EMS.Common;
using EMS.Entity;

namespace EMS.WebApp.Transaction
{
    public partial class Export_bl_query : System.Web.UI.Page
    {
        DataSet BLDataSet = new DataSet();
        ExportBLQueryBLL oImportBLL = new ExportBLQueryBLL();
        #region Private Member Variables

        private int _userId = 0;
        private int _locId = 0;
        private bool _canView = false;
        private bool _LocationSpecific = true;
        private int _userLocation = 0;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //CheckUserAccess();
            IUser user = (IUser)Session[Constants.SESSION_USER_INFO];
            _userId = user == null ? 0 : user.Id;
            //_userId = UserBLL.GetLoggedInUserId();
            _LocationSpecific = UserBLL.GetUserLocationSpecific();
            _userLocation = UserBLL.GetUserLocation();

            if (!Page.IsPostBack)
            {
                autoComplete1.ContextKey = "0|0";
                //FillDropDown();
                if (_LocationSpecific)
                {
                   // ddlLocation.SelectedValue = Convert.ToString(_userLocation);
                    //ddlLocation.Enabled = false;
                    //ddlLine.Enabled = true;
                }
                RetriveParameters();

            }

            //string[] Names = new string[5] { "A", "A", "A", "A", "A"};
            //gvwInvoice.DataSource = Names;
            //gvwInvoice.DataBind();
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

                if (user.UserRole.Id != (int)UserRole.Admin)
                {
                    Response.Redirect("~/Unauthorized.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        private void RetriveParameters()
        {
            if (!ReferenceEquals(Request.QueryString["BLNumber"], null))
            {
                txtBlNo.Text = GeneralFunctions.DecryptQueryString(Request.QueryString["BLNumber"].ToString());
                PopulateAllData();
            }
        }
        private void PopulateAllData()
        {
            ClearAll();
            //DisableAllServiceControls();
            BLDataSet = oImportBLL.GetBLQuery(txtBlNo.Text.Trim(), (int)EMS.Utilities.Enums.BLActivity.DOE);
            txtBlNo.Text = string.Empty;
            if (BLDataSet.Tables[0].Rows.Count > 0)
            {
                fillBLDetail(BLDataSet.Tables[0]);
            }
            FillInvoiceStatus(Convert.ToInt64(hdnBLId.Value));
        }
        void FillInvoiceStatus(Int64 BLId)
        {
            DataTable dtInvoices = oImportBLL.GetAllInvoice(BLId);
            gvwInvoice.DataSource = dtInvoices;
            gvwInvoice.DataBind();
        }
        void fillBLDetail(DataTable dtDetail)
        {
            try
            {
                hdnBLId.Value = dtDetail.Rows[0]["pk_ExpBLID"].ToString();
                txtBlNo.Text = dtDetail.Rows[0]["ExpBLno"].ToString();
                txtPOR.Text = dtDetail.Rows[0]["RcvdPort"].ToString();
                txtBookingParty.Text = dtDetail.Rows[0]["BookingParty"].ToString();
                txtVessel.Text = dtDetail.Rows[0]["VesselName"].ToString();
                txtVoyage.Text = dtDetail.Rows[0]["VoyageNo"].ToString();
                txtBookingDate.Text = Convert.ToDateTime(dtDetail.Rows[0]["BookingDate"].ToString()).ToString("dd/MM/yyyy");
                txtBookingNo.Text = dtDetail.Rows[0]["BOOKINGNO"].ToString();
                txtPOL.Text = dtDetail.Rows[0]["LoadPort"].ToString();
                txtShipper.Text = dtDetail.Rows[0]["SHIPPER"].ToString();
                txtPOD.Text = dtDetail.Rows[0]["DshgPort"].ToString();
                txtContainer.Text = dtDetail.Rows[0]["QTYSTRING"].ToString();
                txtRemarks.Text = dtDetail.Rows[0]["ExportRemarks"].ToString();
                txtBLDate.Text = Convert.ToDateTime(dtDetail.Rows[0]["ExpBLDate"].ToString()).ToString("dd/MM/yyyy");
                txtFPOD.Text = dtDetail.Rows[0]["FinalDest"].ToString();
                if (dtDetail.Rows[0]["FrtInvExist"].ToInt() == 1)
                    btnAddFreightInvoice.Visible = false;
                else
                    btnAddFreightInvoice.Visible = true;

                switch (dtDetail.Rows[0]["Shipmenttype"].ToString())
                {
                    case "0": // Per Unit TYPE & SIZE
                        txtShipmentType.Text = "FCL";
                        break;

                    case "1": // Per Document
                        txtShipmentType.Text = "LCL";
                        break;

                    case "2": // Per CBM
                        txtShipmentType.Text = "BULK";
                        break;

                    case "3": // Per TON
                        txtShipmentType.Text = "BREAK BULK";
                        break;

                }

                //txtShipmentType.Text = dtDetail.Rows[0]["SHIPMENTTYPE"].ToString();                
            }
            catch
            {
            }
           
        }
        void ClearAll()
        {
            txtPOR.Text = string.Empty;
            txtVessel.Text = string.Empty;
            txtBookingParty.Text = string.Empty;         
            txtBookingDate.Text = string.Empty;
            txtBookingNo.Text = string.Empty; 
            txtPOL.Text = string.Empty;
            txtVoyage.Text = string.Empty;
            txtShipper.Text = string.Empty;         
            txtPOD.Text = string.Empty;
            txtContainer.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtBLDate.Text = string.Empty;
            txtFPOD.Text = string.Empty;
            txtShipmentType.Text = string.Empty;
            txtBookingNo.Text = string.Empty;
            gvwInvoice.DataSource = null;
            gvwInvoice.DataBind();
        }
        protected void gvwInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    IUser user = (IUser)Session[Constants.SESSION_USER_INFO];
            //    HiddenField hdnInvID = (HiddenField)e.Row.FindControl("hdnInvID");
            //    System.Web.UI.HtmlControls.HtmlAnchor aPrint = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("aPrint");

            //    //aPrint.Attributes.Add("onclick", string.Format("return ReportPrint1('{0}','{1}','{2}','{3}','{4}');",
            //    //    "reportName=" + EMS.Utilities.GeneralFunctions.EncryptQueryString("InvoiceDeveloper"),
            //    //    "&LineBLNo=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(txtBlNo.Text),
            //    //    "&Location=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(ddlLocation.SelectedValue),
            //    //    "&LoginUserName=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(user.FirstName + " " + user.LastName),
            //    //    "&InvoiceId=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(hdnInvID.Value)));
            //}
        }
        protected void txtBlNo_TextChanged(object sender, EventArgs e)
        {
            if (txtBlNo.Text != string.Empty)
            {
                PopulateAllData();
                UpdatePanel2.Update();
            }
            else
            {
                ClearForm();
            }
        }
        void ClearForm()
        {
            ClearAll();
            // DisableAllServiceControls();
            // DisableAllCheckBoxes();
        }

        protected void btnAddFreightInvoice_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("~/Export/ExportInvoice.aspx?p1=" + GeneralFunctions.EncryptQueryString(txtBlNo.Text)
                + "&p3=" + GeneralFunctions.EncryptQueryString("1")
                + "&p4="+ GeneralFunctions.EncryptQueryString("1")
            +"&p5=" + GeneralFunctions.EncryptQueryString(txtBookingNo.Text)
                 + "&p6=" + GeneralFunctions.EncryptQueryString(txtBLDate.Text)
                   + "&p7=" + GeneralFunctions.EncryptQueryString(txtBookingDate.Text)
                     + "&p8=" + GeneralFunctions.EncryptQueryString(txtContainer.Text)
                );

        }
        protected void btnAddOtherInvoice_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("~/Export/ExportInvoice.aspx?p1=" + GeneralFunctions.EncryptQueryString(txtBlNo.Text)
                + "&p3=" + GeneralFunctions.EncryptQueryString("3")
                + "&p4="+ GeneralFunctions.EncryptQueryString("3")
            +"&p5=" + GeneralFunctions.EncryptQueryString(txtBookingNo.Text)
                 + "&p6=" + GeneralFunctions.EncryptQueryString(txtBLDate.Text)
                   + "&p7=" + GeneralFunctions.EncryptQueryString(txtBookingDate.Text)
                     + "&p8=" + GeneralFunctions.EncryptQueryString(txtContainer.Text)
                );

        }
        
        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Export/ExportBL.aspx");
        }

        protected void btnBack1_Click(object sender, EventArgs e)
        {

        }

    }
}