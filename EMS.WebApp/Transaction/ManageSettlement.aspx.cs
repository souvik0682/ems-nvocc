using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

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
using Microsoft.Win32;

namespace EMS.WebApp.Transaction
{
    public partial class ManageSettlement : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private int _locId = 0;
        private int _CompanyId = 1;
        private bool _LocationSpecific = true;
        private int _userLocation = 0;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            IUser user = (IUser)Session[Constants.SESSION_USER_INFO];
            _userId = user == null ? 0 : user.Id;
            //_userId = UserBLL.GetLoggedInUserId();
            _LocationSpecific = UserBLL.GetUserLocationSpecific();
            _userLocation = UserBLL.GetUserLocation();

            if (!Page.IsPostBack)
            {
                autoComplete1.ContextKey = "0|0";
               
                //chkFreightToCollect.Enabled = true;
                //DisableAllServiceControls();
                RetriveParameters();
            }
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
            if (!ReferenceEquals(Request.QueryString["id"], null))
            {
                hdnSettlementID.Value = GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString());
                txtBlNo.Enabled = false;
                DataSet BLDataSet = new DataSet();
                SettlementBLL oSettlementBLL = new SettlementBLL();
                BLDataSet = oSettlementBLL.GetSettlementWithSettlment(hdnSettlementID.Value.ToInt());
                hdnBLId.Value = BLDataSet.Tables[0].Rows[0]["BLID"].ToString();
                txtSettlementNo.Text = BLDataSet.Tables[0].Rows[0]["SettlementNo"].ToString();
                txtChequeDate.Text = BLDataSet.Tables[0].Rows[0]["ChequeDate"].ToString();
                txtPayToRcvdFrom.Text = BLDataSet.Tables[0].Rows[0]["PayRcv"].ToString();
                txtChequeDetail.Text = BLDataSet.Tables[0].Rows[0]["ChequeDetail"].ToString();
                txtBankName.Text = BLDataSet.Tables[0].Rows[0]["BankName"].ToString();
                txtPayToRcvdFrom.Text = BLDataSet.Tables[0].Rows[0]["PartyName"].ToString();
                hdnRRPath.Value = BLDataSet.Tables[0].Rows[0]["RefundRequestFile"].ToString();
                hdnCLPath.Value = BLDataSet.Tables[0].Rows[0]["ConsigneeLetterFile"].ToString();
                //hdnFilePath.Value = BLDataSet.Tables[0].Rows[0]["RefundRequestFile"].ToString();
                fillBLDetail(BLDataSet.Tables[0]);
                BLDataSet = oSettlementBLL.GetSettlementWithBL(hdnBLId.Value.ToInt());
                LoadBLStatus(BLDataSet.Tables[1]);
                txtOutstanding.Text = Convert.ToString(Math.Abs(BLDataSet.Tables[2].Rows[0]["TotInv"].ToDecimal() - BLDataSet.Tables[2].Rows[0]["TotMR"].ToDecimal() - BLDataSet.Tables[2].Rows[0]["TotCrn"].ToDecimal()));
                lnkCLUpload.Enabled = true;
                lnkRRUpload.Enabled = true;
                btnSave.Enabled = false;
                RRUpload.Enabled = false;
                CLUpload.Enabled = false;
 
                //PopulateAllData();
            }
            else
            {
                hdnSettlementID.Value = "0";
            }
        }

        protected void txtBlNo_TextChanged(object sender, EventArgs e)
        {
            if (txtBlNo.Text != string.Empty)
            {
                gvwInvoice.Visible = true;
                PopulateAllData();

                //UpdatePanel2.Update();
            }
            else
            {
                ClearAll();
            }
        }

        private void PopulateAllData()
        {
            ClearAll();
            DataSet BLDataSet = new DataSet();
            SettlementBLL oSettlementBLL = new SettlementBLL();
            BLDataSet = oSettlementBLL.GetSettlementWithBL(hdnBLId.Value.ToInt());
            txtBlNo.Text = string.Empty;
            lnkCLUpload.Enabled = false;
            lnkRRUpload.Enabled = false;

            if (BLDataSet.Tables[0].Rows.Count > 0)
            {
                fillBLDetail(BLDataSet.Tables[0]);
                LoadBLStatus(BLDataSet.Tables[1]);
                if (hdnSettlementID.Value == "0")
                    fillTotal(BLDataSet.Tables[2]);
                if (hdnOutstanding.Value.ToDecimal() > 0)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('Please Generate M/R before Settlement!');</script>", false);
                    Response.Redirect("~/Transaction/Settlement1.aspx");
                }
                if (BLDataSet.Tables[0].Rows[0]["BLClosed"].ToInt() == 1)
                {
                    btnSave.Visible = false;
                }
            }

           
        }

        void ClearAll()
        {
            txtBlDate.Text = string.Empty;
            txtBlNo.Text = string.Empty;
            txtLine.Text = string.Empty;
            txtLocation.Text = string.Empty;
            txtOutstanding.Text = string.Empty;
            //txtSettlementAmount.Text = string.Empty;
            txtSettlementDate.Text = string.Empty;
            //txtTransactionType.Text = string.Empty;

            DataSet BLDataSet = new DataSet();
            SettlementBLL oSettlementBLL = new SettlementBLL();
            BLDataSet = oSettlementBLL.GetSettlementWithBL(0);
            gvwInvoice.DataSource = BLDataSet.Tables[0];
            gvwInvoice.DataBind();
        }

        void fillBLDetail(DataTable dtDetail)
        {
            //hdnBLId.Value = dtDetail.Rows[0]["BLID"].ToString();
            txtBlNo.Text = dtDetail.Rows[0]["BLNO"].ToString();
            txtBlDate.Text = Convert.ToDateTime(dtDetail.Rows[0]["BLDate"]).ToString("dd/MM/yyyy");
          
            txtLocation.Text = dtDetail.Rows[0]["LocName"].ToString();
            txtLine.Text = dtDetail.Rows[0]["Line"].ToString();
            txtPayToRcvdFrom.Text = dtDetail.Rows[0]["PartyName"].ToString();
            hdnCustName.Value = dtDetail.Rows[0]["PartyName"].ToString();
            //txtPayToRcvdFrom.Text = dtDetail.Rows[0]["PayRcv"].ToString();
            //txtChequeDetail.Text = dtDetail.Rows[0]["ChequeDetail"].ToString();
            //txtBankName.Text = dtDetail.Rows[0]["BankName"].ToString();

            if (hdnSettlementID.Value == "0")
                txtSettlementDate.Text = DateTime.Now.ToShortDateString();
            else
            {
                txtSettlementDate.Text = Convert.ToDateTime(dtDetail.Rows[0]["SettlementDate"]).ToString("dd/MM/yyyy");
                //txtSettlementAmount.Text = dtDetail.Rows[0]["SettlementAmount"].ToString();
                //if (dtDetail.Rows[0]["PorR"].ToString() == "R")
                //{
                //    txtTransactionType.Text = "Rcvble";
                //    lblPaymentRcpt.Text = "Rcvble Amount";
                //    lblPayToRcvdFrom.Text = "Received From";
                //}
                //else
                //{
                //    txtTransactionType.Text = "Payable";
                //    lblPaymentRcpt.Text = "Payable Amount";
                //    lblPayToRcvdFrom.Text = "Paid To";
                //}
            }
        }

        private void LoadBLStatus(DataTable dtDetail)
        {
            if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            {
                SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];
                IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                if (!ReferenceEquals(searchCriteria, null))
                {
                    gvwInvoice.PageIndex = searchCriteria.PageIndex;

                    gvwInvoice.DataSource = dtDetail;
                    gvwInvoice.DataBind();
                }
            }
        }

        protected void fillTotal(DataTable dtTot)
        {
            txtOutstanding.Text = Convert.ToString(Math.Abs(dtTot.Rows[0]["TotInv"].ToDecimal() - dtTot.Rows[0]["TotMR"].ToDecimal() - dtTot.Rows[0]["TotCrn"].ToDecimal() + dtTot.Rows[0]["TotSet"].ToDecimal()));
            hdnOutstanding.Value = Convert.ToString(dtTot.Rows[0]["TotInv"].ToDecimal() - dtTot.Rows[0]["TotMR"].ToDecimal() - dtTot.Rows[0]["TotCrn"].ToDecimal() + dtTot.Rows[0]["TotSet"].ToDecimal());
            //txtSettlementAmount.Text = txtOutstanding.Text; // Convert.ToString(Math.Abs(dtTot.Rows[0]["TotCr"].ToDecimal() - dtTot.Rows[0]["TotDr"].ToDecimal()));
            //if (dtTot.Rows[0]["TotCr"].ToDecimal() - dtTot.Rows[0]["TotDr"].ToDecimal() > 0)
            //if (txtOutstanding.Text.ToDecimal() < 0)
            //{
            //    txtTransactionType.Text = "Rcvble";
            //    lblPaymentRcpt.Text = "Rcvble Amount";
            //}
            //else
            //{
            //    txtTransactionType.Text = "Payable";
            //    lblPaymentRcpt.Text = "Payable Amount";
            //}
        }

        protected void gvwInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 5);

                ScriptManager sManager = ScriptManager.GetCurrent(this);
                e.Row.Cells[0].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InvoiceNo"));
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InvoiceDate")).Split(' ')[0];
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DocType"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DocNo"));
                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DocDate")).Split(' ')[0];
                //e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DocDate"));
                e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InvAmount"));
                e.Row.Cells[6].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "OnAcAmount"));
                e.Row.Cells[7].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "MRAmount"));
                e.Row.Cells[8].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "CRNAmount"));
                e.Row.Cells[9].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "SetAmount"));
                //e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PorR"));
                //e.Row.Cells[6].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "SettlementAmount"));

                //ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");

                //btnRemove.OnClientClick = "javascript:return confirm('" + ResourceManager.GetStringWithoutName("ERR00014") + "');";
                //btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pk_SettlementID"));

            }
        }

   
        private void BuildSettlementEntity(ISettlement Settlement)
        {
            Settlement.CreatedBy = _userId;
            Settlement.CreatedOn = DateTime.Now;
            Settlement.CompanyID = _CompanyId;
            Settlement.BLID = hdnBLId.Value.ToInt();
            Settlement.SettlementDate = txtSettlementDate.Text.ToDateTime();
            Settlement.SettlementNo = txtSettlementNo.Text;
            Settlement.PorR = "P";
            Settlement.SettlementAmount = txtOutstanding.Text.ToDecimal();
            Settlement.OutstandingAmount = txtOutstanding.Text.ToDecimal();
            Settlement.BankName = txtBankName.Text;
            Settlement.ChequeDetail = txtChequeDetail.Text;
            if (txtChequeDate.Text == "")
                Settlement.ChequeDate = null;
            else
                Settlement.ChequeDate = txtChequeDate.Text.ToDateTime();
            //Settlement.ChequeDate = txtChequeDate.Text.ToDateTime();
            Settlement.PayRcvd = txtPayToRcvdFrom.Text;
            Settlement.pk_SettlementID = hdnSettlementID.Value.ToInt();
            Settlement.RRFileUploadPath = hdnRRPath.Value;
            Settlement.CLFileUploadPath = hdnCLPath.Value;
            //Settlement.RRFileUploadPath = hdnFilePath.Value.ToString();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //string misc = string.Empty;

                
               
                if (txtPayToRcvdFrom.Text != hdnCustName.Value && CLUpload.HasFile == false)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('Consignee Letter Upload is compulsory!');</script>", false);
                    return;
                }

                if (txtChequeDate.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('Cheque Date is compulsory!');</script>", false);
                    return;
                }

                if (txtChequeDetail.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('Cheque No is compulsory!');</script>", false);
                    return;
                }

                if (txtBankName.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('BankName is compulsory!');</script>", false);
                    return;
                }


                if (RRUpload.HasFile)
                {
                    var fileName = RRUpload.FileName;
                    var path = Server.MapPath("~/Transaction/SettlementDocs");
                    var newFileName = "RR" + txtBlNo;  //  Guid.NewGuid().ToString();

                    if (!string.IsNullOrEmpty(path))
                    {
                        path += @"\" + newFileName + System.IO.Path.GetExtension(fileName);
                        hdnRRPath.Value = path;
                        RRUpload.PostedFile.SaveAs(path);
                    }
                }

                if (CLUpload.HasFile)
                {
                    var fileName = CLUpload.FileName;
                    var path = Server.MapPath("~/Transaction/SettlementDocs");
                    var newFileName = "CL" + txtBlNo;  //  Guid.NewGuid().ToString();

                    if (!string.IsNullOrEmpty(path))
                    {
                        path += @"\" + newFileName + System.IO.Path.GetExtension(fileName);
                        hdnCLPath.Value = path;
                        CLUpload.PostedFile.SaveAs(path);
                    }
                }
                ISettlement Settlement = new SettlementEntity();
                BuildSettlementEntity(Settlement);
                long Settlementid = new SettlementBLL().SaveSettlement(Settlement);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('Record saved successfully!');</script>", false);
                if (!ReferenceEquals(Request.QueryString["id"], null))
                    Response.Redirect("~/Transaction/Settlement1.aspx");
                else
                    Response.Redirect("~/Transaction/Settlement1.aspx");
                    //ClearAll();
                
                //if (isedit == true)
                //    Response.Redirect("~/Export/Voyage.aspx");
                //else
                //    clearPage();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('Record could not be saved due to ! " + ex.Message.ToString() + "');</script>", false);
            }
        }

        private void clearPage()
        {

            txtLine.Text = string.Empty;
            txtLine.Text = string.Empty;
            txtLocation.Text = string.Empty;
            txtOutstanding.Text = string.Empty;
            //txtSettlementAmount.Text = string.Empty;
            txtSettlementDate.Text = string.Empty;
            txtSettlementNo.Text = string.Empty;
            //txtTransactionType.Text = string.Empty;
            txtBlNo.Text = string.Empty;
            txtBlDate.Text = string.Empty;
            txtBankName.Text = string.Empty;
            txtChequeDetail.Text = string.Empty;
            txtChequeDate.Text = string.Empty;
            txtPayToRcvdFrom.Text= string.Empty;
            gvwInvoice.DataSource = null;
            gvwInvoice.DataBind();
            gvwInvoice.Visible = false;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Transaction/Settlement1.aspx");
        }

        protected void lnkRRUpload_Click(object sender, EventArgs e)
        {
            //var filename = Convert.ToString(src.Tables[0].Rows[0]["LinkedFileName"]);
            var documentName = hdnRRPath.Value;
            var ext = System.IO.Path.GetExtension(hdnRRPath.Value);
            string filePath = string.Format(hdnRRPath.Value);
            System.IO.FileInfo file = new System.IO.FileInfo(filePath);

            if (file.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.Buffer = true;
                Response.ContentType = MimeType(ext);
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}.{1}", documentName, ext));
                Response.WriteFile(filePath);
                Response.End();
            }
        }

        protected void lnkCLUpload_Click(object sender, EventArgs e)
        {
            var documentName = hdnCLPath.Value;
            var ext = System.IO.Path.GetExtension(hdnCLPath.Value);
            string filePath = string.Format(hdnCLPath.Value);
            System.IO.FileInfo file = new System.IO.FileInfo(filePath);

            if (file.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.Buffer = true;
                Response.ContentType = MimeType(ext);
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}.{1}", documentName, ext));
                Response.WriteFile(filePath);
                Response.End();
            }
        }


        private static string MimeType(string Extension)
        {
            string mime = "application/octetstream";
            if (string.IsNullOrEmpty(Extension))
                return mime;

            string ext = Extension.ToLower();
            RegistryKey rk = Registry.ClassesRoot.OpenSubKey(ext);
            if (rk != null && rk.GetValue("Content Type") != null)
                mime = rk.GetValue("Content Type").ToString();
            return mime;
        }

        //protected void btnUpload_Click(object sender, EventArgs e)
        //{

        //}

   
    }
}