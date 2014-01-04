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
        ImportBLL oImportBLL = new ImportBLL();
        ExportBLQueryBLL oExportBLL = new ExportBLQueryBLL();
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

                FillDropDown();
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

        private void FillDropDown()
        {
            ListItem Li = null;
           
            Li = new ListItem("SELECT", "0");
            ddlLocation.Items.Insert(0, Li);
        }

        private void PopulateAllData()
        {
            ClearAll();
            //DisableAllServiceControls();
            BLDataSet = oExportBLL.GetBLQuery(txtBlNo.Text.Trim(), (int)EMS.Utilities.Enums.BLActivity.DOE);
            txtBlNo.Text = string.Empty;
            if (BLDataSet.Tables[0].Rows.Count > 0)
            {
                fillBLDetail(BLDataSet.Tables[0]);
            }
            FillInvoiceStatus(Convert.ToInt64(hdnBLId.Value));
        }
        void FillInvoiceStatus(Int64 BLId)
        {
            DataTable dtInvoices = oExportBLL.GetAllInvoice(BLId);
            gvwInvoice.DataSource = dtInvoices;
            gvwInvoice.DataBind();
        }
        void fillBLDetail(DataTable dtDetail)
        {
            bool FrtInvButton = true;
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
                    FrtInvButton = false;
                if (dtDetail.Rows[0]["ppcc"].ToString() == "T")
                    FrtInvButton = false;
                if (dtDetail.Rows[0]["ShipmentType"].ToInt()==0 && dtDetail.Rows[0]["BILABLE"].ToInt() == 0)
                    FrtInvButton = false;

                if (FrtInvButton == false)
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

                ListItem Li = null;

                Li = new ListItem(dtDetail.Rows[0]["fk_LocationID"].ToString(), dtDetail.Rows[0]["fk_LocationID"].ToString());
                ddlLocation.Items.Insert(0, Li);

                Li = null;

                Li = new ListItem(dtDetail.Rows[0]["fk_NVOCCID"].ToString(), dtDetail.Rows[0]["fk_NVOCCID"].ToString());
                ddlLine.Items.Insert(0, Li);

                lnkDownload.CommandArgument = dtDetail.Rows[0]["DocUploaded"].ToString();
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnCharge = (ImageButton)e.Row.FindControl("btnCharges");
               
                //long Status_Invoice_ID = Convert.ToInt64(e.CommandArgument);
                ImageButton btnStatus = (ImageButton)e.Row.FindControl("btnStatus");
                //LinkButton moneyReceipt = (LinkButton)e.Row.FindControl("aMoneyRecpt");
                HiddenField hdnInvID = (HiddenField)e.Row.FindControl("hdnInvID");
                btnStatus.OnClientClick = "javascript:return confirm('" + ResourceManager.GetStringWithoutName("ERR00014") + "');";

                System.Web.UI.HtmlControls.HtmlAnchor aPrint = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("aPrint");
                System.Web.UI.HtmlControls.HtmlAnchor aAddCrdtNote = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("aAddCrdtNote");
                System.Web.UI.HtmlControls.HtmlAnchor aMoneyRecpt = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("aMoneyRecpt");
                System.Web.UI.HtmlControls.HtmlAnchor RcvdLnk = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("RcvdLnk");
                System.Web.UI.HtmlControls.HtmlAnchor CrnLnk = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("CrnLnk");
                
                var data = (DataRowView)e.Row.DataItem;
                bool Actv = Convert.ToBoolean(data["invoiceActive"]);

                if (Actv == false)
                {
                    btnStatus.Visible = false;
                    aPrint.Visible = false;
                    aAddCrdtNote.Visible = false;
                    aMoneyRecpt.Visible = false;
                    CrnLnk.Visible = false;
                    RcvdLnk.Visible = false;
                    e.Row.Cells[0].ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    btnStatus.Visible = true;
                    aPrint.Visible = true;
                    aAddCrdtNote.Visible = true;
                    aMoneyRecpt.Visible = true;
                    CrnLnk.Visible = true;
                    RcvdLnk.Visible = true;
                    e.Row.Cells[0].ForeColor = System.Drawing.Color.Black;
                }

            }

         
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                IUser user = (IUser)Session[Constants.SESSION_USER_INFO];
                HiddenField hdnInvID = (HiddenField)e.Row.FindControl("hdnInvID");
                System.Web.UI.HtmlControls.HtmlAnchor aPrint = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("aPrint");
                //aPrint.Visible = false;
                aPrint.Attributes.Add("onclick", string.Format("return ReportPrint1('{0}','{1}','{2}','{3}','{4}');",
                    "reportName=" + EMS.Utilities.GeneralFunctions.EncryptQueryString("ExportInvoice"),
                    "&LineBLNo=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(txtBlNo.Text),
                    "&Location=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(ddlLocation.SelectedValue),
                    "&LoginUserName=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(user.FirstName + " " + user.LastName),
                    "&InvoiceId=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(hdnInvID.Value)));
            }

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

        protected void ShowReceivedAmt(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlAnchor a = (System.Web.UI.HtmlControls.HtmlAnchor)sender;
            headerTest.InnerText = "Received Amount Details";

            GridViewRow Row = (GridViewRow)a.NamingContainer;
            HiddenField hdnInvID = (HiddenField)Row.FindControl("hdnInvID");

            DataTable dtDoc = oImportBLL.GetReceivedAmtBreakup(Convert.ToInt64(hdnInvID.Value));

            StringBuilder sbr = new StringBuilder();
            sbr.Append("<table style='width: 100%; border: none;' cellpadding='0' cellspacing='0'>");
            sbr.Append("<tr style='background-color:#328DC4;color:White; font-weight:bold;'>");
            sbr.Append("<td style='width: 70px;padding-left:2px;'>MRNo.</td>");
            sbr.Append("<td style='width: 80px;'>Date</td>");
            sbr.Append("<td style='width: 80px;text-align:right;'>Cash</td>");
            sbr.Append("<td style='width: 80px;text-align:right;'>Cheque</td>");
            sbr.Append("<td style='width: 80px;text-align:right;'>TDS</td>");
            sbr.Append("<td style='width: 50px;text-align:center;'>Print</td>");
            sbr.Append("</tr>");

            for (int rowCount = 0; rowCount < dtDoc.Rows.Count; rowCount++)
            {
                string MRID = dtDoc.Rows[rowCount]["MRID"].ToString();
                string CASH = dtDoc.Rows[rowCount]["CASH"].ToString();
                string CHEQUE = dtDoc.Rows[rowCount]["CHEQUE"].ToString();
                string DATE = Convert.ToDateTime(dtDoc.Rows[rowCount]["DATE"].ToString()).ToString("dd/MM/yyyy");
                string MRNO = dtDoc.Rows[rowCount]["MRNO"].ToString();
                string TDS = dtDoc.Rows[rowCount]["TDS"].ToString();


                if (rowCount % 2 == 0) //For ODD row
                {
                    sbr.Append("<tr>");
                    sbr.Append("<td>" + MRNO + "</td>");
                    sbr.Append("<td>" + DATE + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + CASH + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + CHEQUE + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + TDS + "</td>");
                    //sbr.Append("<td><a href='AddEditMoneyReceipts.aspx?mrid=" + GeneralFunctions.EncryptQueryString(MRID) + "'><img src='../Images/edit.png' /></a></td>");
                    sbr.Append("<td><a target='_blank' href='../Reports/MoneyRcpt.aspx?mrid=" + GeneralFunctions.EncryptQueryString(MRID) + "'><img src='../Images/Print.png' /></a></td>");

                    sbr.Append("</tr>");
                }
                else // For Even Row
                {
                    sbr.Append("<tr style='background-color:#99CCFF;'>");
                    sbr.Append("<td>" + MRNO + "</td>");
                    sbr.Append("<td>" + DATE + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + CASH + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + CHEQUE + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + TDS + "</td>");
                    //sbr.Append("<td><a href='AddEditMoneyReceipts.aspx?mrid=" + GeneralFunctions.EncryptQueryString(MRID) + "'><img src='../Images/edit.png' /></a></td>");
                    sbr.Append("<td><a target='_blank' href='../Reports/MoneyRcpt.aspx?mrid=" + GeneralFunctions.EncryptQueryString(MRID) + "'><img src='../Images/Print.png' /></a></td>");

                    sbr.Append("</tr>");
                }
            }

            sbr.Append("</table>");

            dvMoneyReceived.InnerHtml = sbr.ToString();

            mpeMoneyReceivedDetail.Show();

        }

        protected void ShowCreditNoteAmt(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlAnchor a = (System.Web.UI.HtmlControls.HtmlAnchor)sender;
            headerTest.InnerText = "Credit Note Amount Details";

            GridViewRow Row = (GridViewRow)a.NamingContainer;
            HiddenField hdnInvID = (HiddenField)Row.FindControl("hdnInvID");

            DataTable dtDoc = oImportBLL.GetCNAmtBreakup(Convert.ToInt64(hdnInvID.Value));

            StringBuilder sbr = new StringBuilder();
            sbr.Append("<table style='width: 100%; border: none;' cellpadding='0' cellspacing='0'>");
            sbr.Append("<tr style='background-color:#328DC4;color:White; font-weight:bold;'>");
            sbr.Append("<td style='width: 170px;padding-left:2px;'>CRNNo.</td>");
            sbr.Append("<td style='width: 60px;'>Date</td>");
            sbr.Append("<td style='width: 70px;text-align:right;'>Gross</td>");
            sbr.Append("<td style='width: 70px;text-align:right;'>STax</td>");
            sbr.Append("<td style='width: 70px;text-align:right;'>Cess</td>");
            sbr.Append("<td style='width: 70px;text-align:right;'>ACess</td>");
            sbr.Append("<td style='width: 50px;text-align:center;'>Print</td>");
            sbr.Append("</tr>");

            for (int rowCount = 0; rowCount < dtDoc.Rows.Count; rowCount++)
            {
                string CRNID = dtDoc.Rows[rowCount]["CRNID"].ToString();
                string GROSS = dtDoc.Rows[rowCount]["GROSS"].ToString();
                string STAX = dtDoc.Rows[rowCount]["STAX"].ToString();
                string DATE = Convert.ToDateTime(dtDoc.Rows[rowCount]["DATE"].ToString()).ToString("dd/MM/yyyy");
                string CRNNO = dtDoc.Rows[rowCount]["CRNNO"].ToString();
                string CESS = dtDoc.Rows[rowCount]["CESS"].ToString();
                string ACESS = dtDoc.Rows[rowCount]["ACESS"].ToString();

                IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                string ss = string.Format("ReportPrint2('{0}','{1}','{2}','{3}','{4}','{5}');",
                "reportName=" + EMS.Utilities.GeneralFunctions.EncryptQueryString("CreditNoteExport"),
                "&LineBLNo=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(txtBlNo.Text),
                "&Location=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(ddlLocation.SelectedValue),
                "&LoginUserName=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(user.FirstName + " " + user.LastName),
                "&InvoiceId=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(hdnInvID.Value),
                "&CreditNoteNo=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(CRNID));

                if (rowCount % 2 == 0) //For ODD row
                {
                    sbr.Append("<tr>");
                    sbr.Append("<td>" + CRNNO + "</td>");
                    sbr.Append("<td>" + DATE + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + GROSS + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + STAX + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + CESS + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + ACESS + "</td>");
                    //sbr.Append("<td><a href='AddEditMoneyReceipts.aspx?mrid=" + GeneralFunctions.EncryptQueryString(MRID) + "'><img src='../Images/edit.png' /></a></td>");
                    //sbr.Append("<td><a target='_blank' href='../Reports/MoneyRcpt.aspx?mrid=" + GeneralFunctions.EncryptQueryString(MRID) + "'><img src='../Images/Print.png' /></a></td>");
                    sbr.Append("<td><a target='_blank' onclick=" + ss + "><img src='../Images/Print.png' /></a></td>");

                    sbr.Append("</tr>");
                }
                else // For Even Row
                {
                    sbr.Append("<tr style='background-color:#99CCFF;'>");
                    sbr.Append("<td>" + CRNNO + "</td>");
                    sbr.Append("<td>" + DATE + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + GROSS + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + STAX + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + CESS + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + ACESS + "</td>");
                    //sbr.Append("<td><a href='AddEditMoneyReceipts.aspx?mrid=" + GeneralFunctions.EncryptQueryString(MRID) + "'><img src='../Images/edit.png' /></a></td>");
                    //sbr.Append("<td><a target='_blank' href='../Reports/MoneyRcpt.aspx?mrid=" + GeneralFunctions.EncryptQueryString(MRID) + "'><img src='../Images/Print.png' /></a></td>");
                    sbr.Append("<td><a target='_blank' onclick=" + ss + "><img src='../Images/Print.png' /></a></td>");

                    sbr.Append("</tr>");
                }
            }

            sbr.Append("</table>");

            dvMoneyReceived.InnerHtml = sbr.ToString();

            mpeMoneyReceivedDetail.Show();

        }

        protected void gvwInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Status")
            {
                DeleteInvoice(Convert.ToInt32(e.CommandArgument));
            }
        }

        private void DeleteInvoice(Int32 InvId)
        {
            ExportBLQueryBLL.DeleteInvoice(InvId);
            FillInvoiceStatus(Convert.ToInt64(hdnBLId.Value));
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00010") + "');</script>", false);
            foreach (GridViewRow itemrow in gvwInvoice.Rows)
            {
                if (itemrow.Cells[6].Text == "False")
                {
                    itemrow.Cells[6].Visible = false;
                }
            }
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lnkDownload.CommandArgument))
            {
                string fileName = Server.MapPath("~/Export/ChargesFile/") + lnkDownload.CommandArgument;
                DownloadFile(fileName, true);
            }
            else
                lblMessageBLQuery.Text = "Nothing to Download";
            
        }

        private void DownloadFile(string fname, bool forceDownload)
        {
            string path = fname;
            string name = Path.GetFileName(path);
            string ext = Path.GetExtension(path);
            string type = "";
            // set known types based on file extension  
            if (ext != null)
            {
                switch (ext.ToLower())
                {
                    case ".htm":
                    case ".html":
                        type = "text/HTML";
                        break;

                    case ".txt":
                        type = "text/plain";
                        break;

                    case ".doc":
                    case ".docx":
                    case ".rtf":
                        type = "Application/msword";
                        break;
                }
            }
            if (forceDownload)
            {
                Response.AppendHeader("content-disposition",
                    "attachment; filename=" + name);
            }
            if (type != "")
                Response.ContentType = type;
            Response.WriteFile(path);
            Response.End();
        }
    }
}