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

namespace EMS.WebApp.Transaction
{
    public partial class BL_Query : System.Web.UI.Page
    {
        DataSet BLDataSet = new DataSet();
        ImportBLL oImportBLL = new ImportBLL();

        #region Private Member Variables

        private int _userId = 0;
        private int _locId = 0;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserAccess();
            IUser user = (IUser)Session[Constants.SESSION_USER_INFO];
            _userId = user == null ? 0 : user.Id;

            if (!Page.IsPostBack)
            {
                autoComplete1.ContextKey = "0|0";
                FillDropDown();
                //chkFreightToCollect.Enabled = true;
                DisableAllServiceControls();
            }
            RetriveParameters();
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
            if (!ReferenceEquals(Request.QueryString["blno"], null))
            {
                txtBlNo.Text = Request.QueryString["blno"].ToString();
                PopulateAllData();
            }
        }

        void FillDropDown()
        {
            ListItem Li = null;
            foreach (Enums.UploadedDoc r in Enum.GetValues(typeof(Enums.UploadedDoc)))
            {
                Li = new ListItem("SELECT", "0");
                ListItem item = new ListItem(Enum.GetName(typeof(Enums.UploadedDoc), r).Replace('_', ' '), ((int)r).ToString());
                ddlUploadedDoc.Items.Add(item);
            }
            ddlUploadedDoc.Items.Insert(0, Li);

            foreach (Enums.AmendmentFor r in Enum.GetValues(typeof(Enums.AmendmentFor)))
            {
                Li = new ListItem("SELECT", "0");
                ListItem item = new ListItem(Enum.GetName(typeof(Enums.AmendmentFor), r).Replace('_', ' ').Replace("and", "&"), ((int)r).ToString());
                ddlAmendmentFor.Items.Add(item);
            }
            ddlAmendmentFor.Items.Insert(0, Li);


            Li = new ListItem("Select", "0");
            PopulateDropDown((int)Enums.DropDownPopulationFor.Location, ddlLocation, 0, 0);
            ddlLocation.Items.Insert(0, Li);

            Li = new ListItem("Select", "0");
            PopulateDropDown((int)Enums.DropDownPopulationFor.Line, ddlLine, 0, 0);
            ddlLine.Items.Insert(0, Li);
        }

        void PopulateDropDown(int Number, DropDownList ddl, int? Filter1, int? Filter2)
        {
            CommonBLL.PopulateDropdown(Number, ddl, Filter1, Filter2);
        }

        protected void txtBlNo_TextChanged(object sender, EventArgs e)
        {
            PopulateAllData();
        }

        private void PopulateAllData()
        {
            ClearAll();
            BLDataSet = oImportBLL.GetBLQuery(txtBlNo.Text.Trim(), (int)EMS.Utilities.Enums.BLActivity.DOE);
            txtBlNo.Text = string.Empty;
            if (BLDataSet.Tables[0].Rows.Count > 0)
            {
                fillBLDetail(BLDataSet.Tables[0]);

                //FREIGHTTYPE
                if (BLDataSet.Tables[0].Rows[0]["FREIGHTTOCOLLECT"].ToString() == "PP")
                {
                    chkFreightToCollect.Enabled = false;
                    chkDo.Enabled = true;
                }
                else
                {
                    if (Convert.ToBoolean(BLDataSet.Tables[0].Rows[0]["RECPTCHECK"].ToString()) == true)
                        chkFreightToCollect.Enabled = true;
                    else
                        chkFreightToCollect.Enabled = true;

                    txtFreightToCollect.Text = BLDataSet.Tables[0].Rows[0]["FREIGHTTOCOLLECT"].ToString();
                }

                if (Convert.ToBoolean(BLDataSet.Tables[0].Rows[0]["FREIGHTTOCOLLECT"]) == true)
                {
                    chkDo.Enabled = true;
                }

                if (Convert.ToBoolean(BLDataSet.Tables[0].Rows[0]["FSTINVGENERATED"]) == true)
                {
                    EnableDisableServiceRequestSection();
                }

                fillServiceRequest(BLDataSet.Tables[0]);
                FillUploadedDocument(BLDataSet.Tables[1]);
                FillSubmittedDocument(BLDataSet.Tables[2]);
                FillInvoiceStatus(Convert.ToInt64(hdnBLId.Value));
                FillDoExtension(BLDataSet.Tables[3]);
            }
        }

        void EnableDisableServiceRequestSection()
        {
            //chkDo.Enabled = true;
            chkDoExtension.Enabled = true;
            chkDetensionExtension.Enabled = true;
            chkPGRExtension.Enabled = true;
            chkSecurityInv.Enabled = true;
            chkFinalInvoice.Enabled = true;
            chkOtherInv.Enabled = true;
            chkAmendment.Enabled = true;
            chkBondCancel.Enabled = true;
        }

        void fillBLDetail(DataTable dtDetail)
        {
            hdnBLId.Value = dtDetail.Rows[0]["BLID"].ToString();
            txtBlNo.Text = dtDetail.Rows[0]["BLNO"].ToString();
            txtCha.Text = dtDetail.Rows[0]["CHA"].ToString();
            //txtHouseBlNo.Text = dtDetail.Rows[0]["HBLNO"].ToString();
            //txtDetentionFee.Text = dtDetail.Rows[0]["DTNFEE"].ToString();
            //txtDoValidTill.Text = dtDetail.Rows[0]["DOVALIDUPTO"].ToString();
            txtLandingDate.Text = Convert.ToDateTime(dtDetail.Rows[0]["LANDINGDT"].ToString()).ToString("dd/MM/yyyy");
            txtVessel.Text = dtDetail.Rows[0]["VESSEL"].ToString();
            txtVoyage.Text = dtDetail.Rows[0]["VOYAGE"].ToString();
            txtDetentionFreeDays.Text = dtDetail.Rows[0]["DTNFREEDAYS"].ToString();
            //txtDetentionFee.Text = dtDetail.Rows[0]["DTNFEE"].ToString();
            txtPGRFreedays.Text = dtDetail.Rows[0]["PGRFREEDAYS"].ToString();
            txtPGRTill.Text = dtDetail.Rows[0]["PGRTILL"].ToString();

            if (Convert.ToDateTime(dtDetail.Rows[0]["DOVALIDUPTO"].ToString()) > Convert.ToDateTime("01/01/1950"))
                txtDoValidUpto.Text = dtDetail.Rows[0]["DOVALIDUPTO"].ToString();
            else
                txtDoValidUpto.Text = Convert.ToDateTime(txtLandingDate.Text).AddDays(Convert.ToDouble(txtDetentionFreeDays.Text) - 1).ToShortDateString();

            //if (!string.IsNullOrEmpty(dtDetail.Rows[0]["DTNUPTO"].ToString()))
            if (Convert.ToDateTime(dtDetail.Rows[0]["DTNUPTO"].ToString()) > Convert.ToDateTime("01/01/1950"))
                tstDetentionTill.Text = dtDetail.Rows[0]["DTNUPTO"].ToString();
            else
                tstDetentionTill.Text = Convert.ToDateTime(txtLandingDate.Text).AddDays(Convert.ToDouble(txtDetentionFreeDays.Text) - 1).ToShortDateString();

            //if (!string.IsNullOrEmpty(dtDetail.Rows[0]["PGRTILL"].ToString()))
            if (Convert.ToDateTime(dtDetail.Rows[0]["PGRTILL"].ToString()) > Convert.ToDateTime("01/01/1950"))
                txtPGRTill.Text = dtDetail.Rows[0]["PGRTILL"].ToString();
            else
                txtPGRTill.Text = Convert.ToDateTime(txtLandingDate.Text).AddDays(Convert.ToDouble(txtPGRFreedays.Text) - 1).ToShortDateString();
        }

        void fillServiceRequest(DataTable dtDetail)
        {
            //txtDoValidTill.Text = Convert.ToDateTime(txtLandingDate.Text).AddDays(Convert.ToDouble(txtDetentionFreeDays.Text) - 1).ToShortDateString();

            //if (dtDetail.Rows[0]["FREIGHTTYPE"].ToString().ToLower() == "pp")
            //    chkFreightPaidstatus.Checked = true;
            //else
            //    chkFreightPaidstatus.Checked = false;


            if (dtDetail.Rows[0]["DESTUFFING"].ToString() == "0")
                ddlDestuffing.SelectedIndex = 0;
            else
                ddlDestuffing.SelectedIndex = 1;

            //chkBankGuarantee.Checked = Convert.ToBoolean(dtDetail.Rows[0]["BANKGUARANTEE"].ToString());

            if (dtDetail.Rows[0]["VALIDITYDATE"].ToString() != "")
                txtVAlidityDate.Text = Convert.ToDateTime(dtDetail.Rows[0]["VALIDITYDATE"].ToString()).ToString("dd/MM/yyyy");


            if (dtDetail.Rows[0]["SFD"].ToString() != "")
                txtExtensionForDetention.Text = Convert.ToDateTime(dtDetail.Rows[0]["SFD"].ToString()).ToString("dd/MM/yyyy");

            if (dtDetail.Rows[0]["SFPGR"].ToString() != "")
                txtExtensionForPGR.Text = Convert.ToDateTime(dtDetail.Rows[0]["SFPGR"].ToString()).ToString("dd/MM/yyyy");


            if (dtDetail.Rows[0]["BONDCANCEL"].ToString() != "")
                txtBondCancellation.Text = Convert.ToDateTime(dtDetail.Rows[0]["BONDCANCEL"].ToString()).ToString("dd/MM/yyyy");
        }

        void FillUploadedDocument(DataTable dtDoc)
        {

            StringBuilder sbr = new StringBuilder();
            sbr.Append("<table style='width: 100%; border: none;' cellpadding='0' cellspacing='0'>");
            sbr.Append("<tr style='background-color:#328DC4;color:White; font-weight:bold;'>");
            sbr.Append("<td style='width: 170px;padding-left:2px;'>Name</td>");
            sbr.Append("<td style='width: 80px;'>Uploaded On</td>");
            sbr.Append("<td style='width: 50px;text-align:center;'>Delete</td>");
            sbr.Append("</tr>");

            for (int rowCount = 0; rowCount < dtDoc.Rows.Count; rowCount++)
            {
                string Id = dtDoc.Rows[rowCount]["ID"].ToString();
                string Name = ddlUploadedDoc.Items.FindByValue(dtDoc.Rows[rowCount]["DOCNAME"].ToString()).Text.Replace(" ", "_");
                string Date = Convert.ToDateTime(dtDoc.Rows[rowCount]["UPLOADDATE"].ToString()).ToString("dd/MM/yyyy");

                if (rowCount % 2 == 0) //For ODD row
                {
                    sbr.Append("<tr><td padding-left:2px;><a href='DocDownload.ashx?Id=" + Id + "&n=" + Name + "'>" + Name + "</a></td>");
                    sbr.Append("<td>" + Date + "</td>");
                    sbr.Append("<td style='text-align:center;'>");
                    sbr.Append("<input type='image' onclick='DeleteUploadedDoc(this)' id='" + Id + "' src='../Images/remove_icon.gif' />");
                    sbr.Append("</td>");
                    sbr.Append("</tr>");
                }
                else // For Even Row
                {
                    //sbr.Append("<tr style='background-color:#99CCFF;'>");
                    sbr.Append("<tr style='background-color:#99CCFF;'><td padding-left:2px;><a href='DocDownload.ashx?Id=" + Id + "&n=" + Name + "'>" + Name + "</a></td>");
                    sbr.Append("<td>" + Date + "</td>");
                    sbr.Append("<td style='text-align:center;'>");
                    sbr.Append("<input type='image' onclick='DeleteUploadedDoc(this)' id='" + Id + "' src='../Images/remove_icon.gif' />");
                    sbr.Append("</td>");
                    sbr.Append("</tr>");
                }
            }


            sbr.Append("</table>");

            dvDoc.InnerHtml = sbr.ToString();

        }

        void FillInvoiceStatus(Int64 BLId)
        {
            DataTable dtInvoices = oImportBLL.GetAllInvoice(BLId);

            gvwInvoice.DataSource = dtInvoices;
            gvwInvoice.DataBind();
        }

        void FillSubmittedDocument(DataTable dtDoc)
        {
            if (dtDoc.Rows.Count > 0)
            {
                chkOriginalBL.Checked = Convert.ToBoolean(dtDoc.Rows[0]["OrgininalBL"].ToString());
                chkEndorseHBL.Checked = Convert.ToBoolean(dtDoc.Rows[0]["EndorseHBL"].ToString());
                chkContainerBond.Checked = Convert.ToBoolean(dtDoc.Rows[0]["ContainerBond"].ToString());
                chkInsuranceCopy.Checked = Convert.ToBoolean(dtDoc.Rows[0]["InsuranceCopy"].ToString());
                chkCopyOfMasterBL.Checked = Convert.ToBoolean(dtDoc.Rows[0]["MasterBLCopy"].ToString());
                chkSecurityCheque.Checked = Convert.ToBoolean(dtDoc.Rows[0]["SecurityCheque"].ToString());
                chkCopyOfBill.Checked = Convert.ToBoolean(dtDoc.Rows[0]["CopyBillOfEntry"].ToString());
                chkConsoldatorNOC.Checked = Convert.ToBoolean(dtDoc.Rows[0]["ConsolidatorsNOC"].ToString());
                chkCHSSA.Checked = Convert.ToBoolean(dtDoc.Rows[0]["HighSeaSales"].ToString());
                chkBankGuarantee.Checked = Convert.ToBoolean(dtDoc.Rows[0]["HighSeaSales"].ToString());
            }
        }

        //void FillDoExtension(DataTable dtDOE)
        //{
        //    StringBuilder sbr = new StringBuilder();
        //    if (dtDOE.Rows.Count > 0)
        //    {
        //        sbr.Append("<table style='width:80%;' cellspacing='0' align='center'>");
        //        sbr.Append("<tr style='height:30px;background-color:#328DC4;color:White; font-weight:bold;'><td>Date</td><td>Print</td></tr>");

        //        for (int rowCount = 0; rowCount < dtDOE.Rows.Count; rowCount++)
        //        {
        //            string Date = Convert.ToDateTime(dtDOE.Rows[rowCount]["Date"].ToString()).ToString("dd/MM/yyyy");

        //            if (rowCount % 2 == 0) //For ODD row
        //            {
        //                sbr.Append("<tr><td>" + Date + "</td><td><a href='#'><img src='../Images/Print.png' /></a></td></tr>");
        //            }
        //            else
        //            {
        //                sbr.Append("<tr style='background-color:#99CCFF;'><td>" + Date + "</td><td><a href='#'><img src='../Images/Print.png' /></a></td></tr>");
        //            }
        //        }

        //        sbr.Append("</table>");
        //        dvDoExtension.InnerHtml = sbr.ToString();
        //    }
        //    else
        //    {
        //        sbr.Append("<table style='width:80%;' cellspacing='0' align='center'>");
        //        sbr.Append("<tr><td>No records found</td></tr>");
        //        sbr.Append("</table>");
        //        dvDoExtension.InnerHtml = sbr.ToString();
        //    }
        //}

        void FillDoExtension(DataTable dtDOE)
        {
            StringBuilder sbr = null;
            if (dtDOE.Rows.Count > 0)
            {
                #region DO Extension Section

                sbr = new StringBuilder();
                sbr.Append("<table style='width:80%;' cellspacing='0' align='center'>");
                sbr.Append("<tr style='height:30px;background-color:#328DC4;color:White; font-weight:bold;'><td>Date</td><td>Print</td></tr>");

                for (int rowCount = 0; rowCount < dtDOE.Rows.Count; rowCount++)
                {
                    if (Convert.ToInt32(dtDOE.Rows[rowCount]["Type"].ToString()) == (int)EMS.Utilities.Enums.BLActivity.DOE)
                    {
                        string Date = Convert.ToDateTime(dtDOE.Rows[rowCount]["Date"].ToString()).ToString("dd/MM/yyyy");

                        if (rowCount % 2 == 0) //For ODD row
                        {
                            sbr.Append("<tr><td>" + Date + "</td><td><a href='#'><img src='../Images/Print.png' /></a></td></tr>");
                        }
                        else
                        {
                            sbr.Append("<tr style='background-color:#99CCFF;'><td>" + Date + "</td><td><a href='#'><img src='../Images/Print.png' /></a></td></tr>");
                        }
                    }
                }

                sbr.Append("</table>");
                dvDoExtension.InnerHtml = sbr.ToString();
                #endregion

                #region Detention Extention Section
                sbr = new StringBuilder();
                sbr.Append("<table style='width:80%;' cellspacing='0' align='center'>");
                sbr.Append("<tr style='height:30px;background-color:#328DC4;color:White; font-weight:bold;'><td>Date</td><td>Print</td></tr>");

                for (int rowCount = 0; rowCount < dtDOE.Rows.Count; rowCount++)
                {
                    if (Convert.ToInt32(dtDOE.Rows[rowCount]["Type"].ToString()) == (int)EMS.Utilities.Enums.BLActivity.DE)
                    {
                        string Date = Convert.ToDateTime(dtDOE.Rows[rowCount]["Date"].ToString()).ToString("dd/MM/yyyy");

                        if (rowCount % 2 == 0) //For ODD row
                        {
                            sbr.Append("<tr><td>" + Date + "</td><td><a href='#'><img src='../Images/Print.png' /></a></td></tr>");
                        }
                        else
                        {
                            sbr.Append("<tr style='background-color:#99CCFF;'><td>" + Date + "</td><td><a href='#'><img src='../Images/Print.png' /></a></td></tr>");
                        }
                    }
                }

                sbr.Append("</table>");
                dvEFD.InnerHtml = sbr.ToString();
                #endregion

                #region PGR Extention Section
                sbr = new StringBuilder();
                sbr.Append("<table style='width:80%;' cellspacing='0' align='center'>");
                sbr.Append("<tr style='height:30px;background-color:#328DC4;color:White; font-weight:bold;'><td>Date</td><td>Print</td></tr>");

                for (int rowCount = 0; rowCount < dtDOE.Rows.Count; rowCount++)
                {
                    if (Convert.ToInt32(dtDOE.Rows[rowCount]["Type"].ToString()) == (int)EMS.Utilities.Enums.BLActivity.PGRE)
                    {
                        string Date = Convert.ToDateTime(dtDOE.Rows[rowCount]["Date"].ToString()).ToString("dd/MM/yyyy");

                        if (rowCount % 2 == 0) //For ODD row
                        {
                            sbr.Append("<tr><td>" + Date + "</td><td><a href='#'><img src='../Images/Print.png' /></a></td></tr>");
                        }
                        else
                        {
                            sbr.Append("<tr style='background-color:#99CCFF;'><td>" + Date + "</td><td><a href='#'><img src='../Images/Print.png' /></a></td></tr>");
                        }
                    }
                }

                sbr.Append("</table>");
                dvPGR.InnerHtml = sbr.ToString();
                #endregion

                #region Security Invoice Section
                sbr = new StringBuilder();
                sbr.Append("<table style='width:80%;' cellspacing='0' align='center'>");
                sbr.Append("<tr style='height:30px;background-color:#328DC4;color:White; font-weight:bold;'><td>Date</td><td>Print</td></tr>");

                for (int rowCount = 0; rowCount < dtDOE.Rows.Count; rowCount++)
                {
                    if (Convert.ToInt32(dtDOE.Rows[rowCount]["Type"].ToString()) == (int)EMS.Utilities.Enums.BLActivity.SI)
                    {
                        string Date = Convert.ToDateTime(dtDOE.Rows[rowCount]["Date"].ToString()).ToString("dd/MM/yyyy");

                        if (rowCount % 2 == 0) //For ODD row
                        {
                            sbr.Append("<tr><td>" + Date + "</td><td><a href='#'><img src='../Images/Print.png' /></a></td></tr>");
                        }
                        else
                        {
                            sbr.Append("<tr style='background-color:#99CCFF;'><td>" + Date + "</td><td><a href='#'><img src='../Images/Print.png' /></a></td></tr>");
                        }
                    }
                }

                sbr.Append("</table>");
                dvSecurity.InnerHtml = sbr.ToString();
                #endregion

                #region Other Invoice Section
                sbr = new StringBuilder();
                sbr.Append("<table style='width:80%;' cellspacing='0' align='center'>");
                sbr.Append("<tr style='height:30px;background-color:#328DC4;color:White; font-weight:bold;'><td>Date</td><td>Print</td></tr>");

                for (int rowCount = 0; rowCount < dtDOE.Rows.Count; rowCount++)
                {
                    if (Convert.ToInt32(dtDOE.Rows[rowCount]["Type"].ToString()) == (int)EMS.Utilities.Enums.BLActivity.OI)
                    {
                        string Date = Convert.ToDateTime(dtDOE.Rows[rowCount]["Date"].ToString()).ToString("dd/MM/yyyy");

                        if (rowCount % 2 == 0) //For ODD row
                        {
                            sbr.Append("<tr><td>" + Date + "</td><td><a href='#'><img src='../Images/Print.png' /></a></td></tr>");
                        }
                        else
                        {
                            sbr.Append("<tr style='background-color:#99CCFF;'><td>" + Date + "</td><td><a href='#'><img src='../Images/Print.png' /></a></td></tr>");
                        }
                    }
                }

                sbr.Append("</table>");
                dvOtherInvoice.InnerHtml = sbr.ToString();
                #endregion
            }
            //else
            //{
            //    sbr.Append("<table style='width:80%;' cellspacing='0' align='center'>");
            //    sbr.Append("<tr><td>No records found</td></tr>");
            //    sbr.Append("</table>");
            //    dvDoExtension.InnerHtml = sbr.ToString();
            //}
        }

        protected void DeleteUploadedDoc(object sender, EventArgs e)
        {
            //System.Web.UI.HtmlControls.HtmlImage a = (System.Web.UI.HtmlControls.HtmlImage)sender;
            //Int64 docId = Convert.ToInt64(a.ID.Substring(a.ID.IndexOf('_') + 1));
            Int64 docId = Convert.ToInt64(hdnUploadedDocId.Value);
            oImportBLL.DeleteUploadedDocument(docId);
            PopulateAllData();
        }

        protected void chkDo_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllServiceControls();

            ddlDestuffing.Enabled = true;
            lnkGenerateInvoiceDo.Enabled = true;
            lnkGenerateInvoiceDo.Enabled = true;
            lnkDO.Enabled = true;

        }

        protected void chkDoExtension_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllServiceControls();

            txtVAlidityDate.Enabled = true;
            lnkGenerateInvoiceDOE.Enabled = true;
            lnkDoExtension.Enabled = true;

        }

        protected void chkSlotExtension_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllServiceControls();


            txtExtensionForDetention.Enabled = true;
            lnkGenerateInvoiceSlotExtension.Enabled = true;
            lnkSlotExtension.Enabled = true;


        }

        protected void chkAmendment_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllServiceControls();
            ddlAmendmentFor.Enabled = true;
            lnkPrintAmend.Enabled = true;
            //lnkAmendment.Enabled = true;

        }

        protected void chkBondCancel_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllServiceControls();

            txtBondCancellation.Enabled = true;
            btnBondSave.Enabled = true;
            lnkBondCancel.Enabled = true;

        }

        protected void imgBtnExaminationDo_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("/#?bl");
        }

        protected void btnSave2_Click(object sender, EventArgs e)
        {
            //if (Page.IsValid)
            //{
            //    switch (oImportBLL.SaveBLQActivity(GenerateBLQActivityXMLString(), Convert.ToInt32(hdnBLId.Value)))
            //    {
            //        case 1: PopulateAllData();
            //            lblMessageServiceReq.Text = ResourceManager.GetStringWithoutName("ERR00009");
            //            UpdatePanel2.Update();
            //            break;
            //    }

            //}
        }


        //string GenerateBLQActivityXMLString()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("<Activity>");

        //    if (chkDo.Checked)
        //    {
        //        sb.Append("<Item>");

        //        sb.Append("<BLID>" + hdnBLId.Value + "</BLID>");
        //        sb.Append("<AT>" + (int)EMS.Utilities.Enums.BLActivity.DO + "</AT>");
        //        sb.Append("<AD>" + DateTime.Today.Date.ToString("MM/dd/yyyy") + "</AD>");
        //        sb.Append("<VD>" + Convert.ToDateTime(txtDoValidTill.Text).ToString("MM/dd/yyyy") + "</VD>");
        //        sb.Append("<BG>" + (chkBankGuarantee.Checked ? "1" : "0") + "</BG>");
        //        sb.Append("<IG>" + 0 + "</IG>");
        //        sb.Append("<NOP>" + 0 + "</NOP>");

        //        sb.Append("</Item>");
        //    }

        //    if (chkDoExtension.Checked)
        //    {
        //        sb.Append("<Item>");

        //        sb.Append("<BLID>" + hdnBLId.Value + "</BLID>");
        //        sb.Append("<AT>" + (int)EMS.Utilities.Enums.BLActivity.DOE + "</AT>");
        //        sb.Append("<AD>" + DateTime.Today.Date.ToString("MM/dd/yyyy") + "</AD>");
        //        sb.Append("<VD>" + Convert.ToDateTime(txtVAlidityDate.Text).ToString("MM/dd/yyyy") + "</VD>");
        //        sb.Append("<BG>" + "0" + "</BG>");
        //        sb.Append("<IG>" + 0 + "</IG>");
        //        sb.Append("<NOP>" + 0 + "</NOP>");

        //        sb.Append("</Item>");
        //    }


        //    if (chkSlotExtension.Checked)
        //    {
        //        sb.Append("<Item>");

        //        sb.Append("<BLID>" + hdnBLId.Value + "</BLID>");
        //        sb.Append("<AT>" + (int)EMS.Utilities.Enums.BLActivity.SE + "</AT>");
        //        if (!String.IsNullOrEmpty(txtExtensionForDetention.Text))
        //            sb.Append("<AD>" + Convert.ToDateTime(txtExtensionForDetention.Text).ToString("MM/dd/yyyy") + "</AD>");
        //        else
        //            sb.Append("<AD></AD>");

        //        if (!String.IsNullOrEmpty(txtExtensionForPGR.Text))
        //            sb.Append("<VD>" + Convert.ToDateTime(txtExtensionForPGR.Text).ToString("MM/dd/yyyy") + "</VD>");
        //        else
        //            sb.Append("<VD></VD>");

        //        sb.Append("<BG>" + "0" + "</BG>");
        //        sb.Append("<IG>" + 0 + "</IG>");
        //        sb.Append("<NOP>" + 0 + "</NOP>");

        //        sb.Append("</Item>");
        //    }

        //    if (chkBondCancel.Checked)
        //    {
        //        sb.Append("<Item>");

        //        sb.Append("<BLID>" + hdnBLId.Value + "</BLID>");
        //        sb.Append("<AT>" + (int)EMS.Utilities.Enums.BLActivity.BC + "</AT>");
        //        sb.Append("<AD>" + DateTime.Today.Date.ToString("MM/dd/yyyy") + "</AD>");
        //        sb.Append("<VD>" + Convert.ToDateTime(txtBondCancellation.Text).ToString("MM/dd/yyyy") + "</VD>");
        //        sb.Append("<BG>" + "0" + "</BG>");
        //        sb.Append("<IG>" + 0 + "</IG>");
        //        sb.Append("<NOP>" + 0 + "</NOP>");

        //        sb.Append("</Item>");
        //    }

        //    sb.Append("</Activity>");

        //    /*
        //    foreach (GridViewRow gvRow in gvSelectedContainer.Rows)
        //    {
        //        HiddenField hdnOldTransactionId = (HiddenField)gvRow.FindControl("hdnOldTransactionId");
        //        HiddenField hdnCurrentTransactionId = (HiddenField)gvRow.FindControl("hdnCurrentTransactionId");
        //        CheckBox chkItem = (CheckBox)gvRow.FindControl("chkItem");

        //        sb.Append("<Cont>");

        //        sb.Append("<Oid>" + hdnOldTransactionId.Value + "</Oid>");
        //        sb.Append("<Nid>" + hdnCurrentTransactionId.Value + "</Nid>");
        //        sb.Append("<Stats>" + chkItem.Checked.ToString() + "</Stats>");

        //        sb.Append("</Cont>");

        //    }*/



        //    return sb.ToString();
        //}

        //string GenerateBLQActivityXMLString(int No)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("<Activity>");

        //    switch (No)
        //    {
        //        case 1:
        //            #region chkDo.Checked
        //            sb.Append("<Item>");

        //            sb.Append("<BLID>" + hdnBLId.Value + "</BLID>");
        //            sb.Append("<AT>" + (int)EMS.Utilities.Enums.BLActivity.DO + "</AT>");
        //            sb.Append("<AD>" + DateTime.Today.Date.ToString("MM/dd/yyyy") + "</AD>");
        //            //sb.Append("<VD>" + Convert.ToDateTime(txtDoValidTill.Text).ToString("MM/dd/yyyy") + "</VD>");
        //            //sb.Append("<BG>" + (chkBankGuarantee.Checked ? "1" : "0") + "</BG>");
        //            sb.Append("<IG>" + 0 + "</IG>");
        //            sb.Append("<NOP>" + 0 + "</NOP>");

        //            sb.Append("</Item>");
        //            #endregion
        //            break;

        //        case 2:
        //            #region chkDoExtension.Checked
        //            sb.Append("<Item>");

        //            sb.Append("<BLID>" + hdnBLId.Value + "</BLID>");
        //            sb.Append("<AT>" + (int)EMS.Utilities.Enums.BLActivity.DOE + "</AT>");
        //            sb.Append("<AD>" + DateTime.Today.Date.ToString("MM/dd/yyyy") + "</AD>");
        //            sb.Append("<VD>" + Convert.ToDateTime(txtVAlidityDate.Text).ToString("MM/dd/yyyy") + "</VD>");
        //            sb.Append("<BG>" + "0" + "</BG>");
        //            sb.Append("<IG>" + 0 + "</IG>");
        //            sb.Append("<NOP>" + 0 + "</NOP>");

        //            sb.Append("</Item>");
        //            #endregion
        //            break;

        //        case 3:
        //            #region chkSlotExtension.Checked
        //            sb.Append("<Item>");

        //            sb.Append("<BLID>" + hdnBLId.Value + "</BLID>");
        //            sb.Append("<AT>" + (int)EMS.Utilities.Enums.BLActivity.SE + "</AT>");
        //            if (!String.IsNullOrEmpty(txtExtensionForDetention.Text))
        //                sb.Append("<AD>" + Convert.ToDateTime(txtExtensionForDetention.Text).ToString("MM/dd/yyyy") + "</AD>");
        //            else
        //                sb.Append("<AD></AD>");

        //            if (!String.IsNullOrEmpty(txtExtensionForPGR.Text))
        //                sb.Append("<VD>" + Convert.ToDateTime(txtExtensionForPGR.Text).ToString("MM/dd/yyyy") + "</VD>");
        //            else
        //                sb.Append("<VD></VD>");

        //            sb.Append("<BG>" + "0" + "</BG>");
        //            sb.Append("<IG>" + 0 + "</IG>");
        //            sb.Append("<NOP>" + 0 + "</NOP>");

        //            sb.Append("</Item>");
        //            #endregion
        //            break;

        //        case 4:
        //            break;

        //        case 5:
        //            #region chkBondCancel.Checked
        //            sb.Append("<Item>");

        //            sb.Append("<BLID>" + hdnBLId.Value + "</BLID>");
        //            sb.Append("<AT>" + (int)EMS.Utilities.Enums.BLActivity.BC + "</AT>");
        //            sb.Append("<AD>" + DateTime.Today.Date.ToString("MM/dd/yyyy") + "</AD>");
        //            sb.Append("<VD>" + Convert.ToDateTime(txtBondCancellation.Text).ToString("MM/dd/yyyy") + "</VD>");
        //            sb.Append("<BG>" + "0" + "</BG>");
        //            sb.Append("<IG>" + 0 + "</IG>");
        //            sb.Append("<NOP>" + 0 + "</NOP>");

        //            sb.Append("</Item>");
        //            #endregion
        //            break;
        //    }

        //    sb.Append("</Activity>");

        //    return sb.ToString();
        //}

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt64(hdnBLId.Value) > 0)
            {
                byte[] DocImage = FileUpload1.FileBytes;
                string Type = FileUpload1.PostedFile.ContentType;
                switch (oImportBLL.SaveUploadedDocument(Convert.ToInt64(hdnBLId.Value), Convert.ToInt16(ddlUploadedDoc.SelectedValue), Type, DocImage, DateTime.Now.Date))
                {
                    case -1:
                    case 1: lblUploadMsg.Text = "Upload succeeded";
                        PopulateAllData();
                        break;
                    case 0: lblUploadMsg.Text = "Upload failed";
                        break;
                }
            }
            else
            {
                lblUploadMsg.Text = ResourceManager.GetStringWithoutName("ERR00080");
            }
        }

        protected void btnDocSubmit_Click(object sender, EventArgs e)
        {
            StringBuilder sbr = new StringBuilder();

            sbr.Append(chkOriginalBL.Checked == true ? "1," : "0,");
            sbr.Append(chkEndorseHBL.Checked == true ? "1," : "0,");
            sbr.Append(chkContainerBond.Checked == true ? "1," : "0,");
            sbr.Append(chkInsuranceCopy.Checked == true ? "1," : "0,");
            sbr.Append(chkCopyOfMasterBL.Checked == true ? "1," : "0,");
            sbr.Append(chkSecurityCheque.Checked == true ? "1," : "0,");
            sbr.Append(chkCopyOfBill.Checked == true ? "1," : "0,");
            sbr.Append(chkConsoldatorNOC.Checked == true ? "1," : "0,");
            sbr.Append(chkCHSSA.Checked == true ? "1," : "0,");
            sbr.Append(chkBankGuarantee.Checked == true ? "1" : "0");

            oImportBLL.SaveSubmittedDocument(Convert.ToInt64(hdnBLId.Value), sbr.ToString());
        }

        protected void lnkDO_Click(object sender, EventArgs e)
        {
            mpeDo.Show();
        }

        protected void lnkDoExtension_Click(object sender, EventArgs e)
        {
            mpeDOE.Show();
        }

        protected void lnkSlotExtension_Click(object sender, EventArgs e)
        {
            mpeSE.Show();
        }

        protected void lnkAmendment_Click(object sender, EventArgs e)
        {
            mpeAmend.Show();
        }

        protected void lnkBondCancel_Click(object sender, EventArgs e)
        {
            mpeBond.Show();
        }

        protected void LocationLine_Changed(object sender, EventArgs e)
        {
            autoComplete1.ContextKey = ddlLocation.SelectedValue + "|" + ddlLine.SelectedValue;
            if (ddlLocation.SelectedIndex > 0)
                ddlLine.Enabled = true;
            else
            {
                ddlLine.Enabled = false;
                ddlLine.SelectedIndex = 0;
            }

            if (ddlLine.SelectedIndex > 0)
                txtBlNo.Enabled = true;
            else
            {
                txtBlNo.Enabled = false;
                txtBlNo.Text = string.Empty;
                ClearAll();
            }
        }


        protected void chkFreightToCollect_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllServiceControls();

            lnkFreightToCollect.Enabled = true;
            txtFreightToCollect.Enabled = true;
            lnkGenInvFreightToCollect.Enabled = true;

        }

        protected void chkPGRExtension_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllServiceControls();
            lnkPGRExtension.Enabled = true;
            txtExtensionForPGR.Enabled = true;
            lnkGenInvPGR.Enabled = true;
        }

        protected void chkSecurityInv_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllServiceControls();
            lnkSecurityInv.Enabled = true;
            lnkGenInvSecirity.Enabled = true;
        }

        protected void chkFinalInvoice_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllServiceControls();
            lnkFinalInvoice.Enabled = true;
            lnkGenInvFinalDo.Enabled = true;
        }

        protected void chkOtherInv_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllServiceControls();
            lnkOtherInv.Enabled = true;
            lnkGenInvOtherInvoice.Enabled = true;
        }

        void Redirect(string BLNo, string InvoiceName, string InvTypID, string Misc)
        {
            if (String.IsNullOrEmpty(BLNo))
            {
                Label lblMsg = new Label();
                lblMessageBLQuery.Text = "<script type='text/javascript'>alert('Please enter BLNo');</script>";
                Page.Controls.Add(lblMessageBLQuery);
            }

            string p1 = GeneralFunctions.EncryptQueryString(BLNo);
            string p2 = GeneralFunctions.EncryptQueryString(InvoiceName);
            string p3 = GeneralFunctions.EncryptQueryString(InvTypID);
            string p4 = GeneralFunctions.EncryptQueryString(Misc);

            Response.Redirect("~/Transaction/ManageInvoice.aspx?p1=" + p1 + "&p2=" + p2 + "&p3=" + p3 + "&p4=" + p4 + "");
        }

        protected void lnkGenInvFreightToCollect_Click(object sender, EventArgs e)
        {
            Redirect(txtBlNo.Text.Trim(), "Freight Invoice", "7", txtFreightToCollect.Text.Trim());
        }

        protected void lnkGenerateInvoiceDo_Click(object sender, EventArgs e)
        {
            Redirect(txtBlNo.Text.Trim(), "First Invoice", "1", ddlDestuffing.SelectedItem.Text);
        }

        protected void lnkGenerateInvoiceDOE_Click(object sender, EventArgs e)
        {
            Redirect(txtBlNo.Text.Trim(), "DO Extension", "22", txtVAlidityDate.Text.Trim());
        }

        protected void lnkGenerateInvoiceSlotExtension_Click(object sender, EventArgs e)
        {
            Redirect(txtBlNo.Text.Trim(), "Proforma Invoice", "8", txtExtensionForDetention.Text.Trim());
        }

        protected void lnkGenInvPGR_Click(object sender, EventArgs e)
        {
            Redirect(txtBlNo.Text.Trim(), "Proforma Invoice", "21", txtExtensionForPGR.Text.Trim());
        }

        protected void lnkGenInvSecirity_Click(object sender, EventArgs e)
        {
            Redirect(txtBlNo.Text.Trim(), "Securty Deposit", "2", string.Empty);
        }

        protected void lnkGenInvFinalDo_Click(object sender, EventArgs e)
        {
            Redirect(txtBlNo.Text.Trim(), "Final Invoice", "3", string.Empty);
        }

        protected void lnkGenInvOtherInvoice_Click(object sender, EventArgs e)
        {
            Redirect(txtBlNo.Text.Trim(), "MISC. INVOICE", "0", string.Empty);
        }

        void DisableAllServiceControls()
        {
            lnkFreightToCollect.Enabled = false;
            lnkDO.Enabled = false;
            lnkDoExtension.Enabled = false;
            lnkSlotExtension.Enabled = false;
            lnkPGRExtension.Enabled = false;
            lnkSecurityInv.Enabled = false;
            lnkFinalInvoice.Enabled = false;
            lnkOtherInv.Enabled = false;
            lnkBondCancel.Enabled = false;

            txtFreightToCollect.Enabled = false;
            ddlDestuffing.Enabled = false;
            txtVAlidityDate.Enabled = false;
            txtExtensionForDetention.Enabled = false;
            txtExtensionForPGR.Enabled = false;
            ddlAmendmentFor.Enabled = false;
            txtBondCancellation.Enabled = false;

            lnkGenInvFreightToCollect.Enabled = false;
            lnkGenerateInvoiceDo.Enabled = false;
            lnkGenerateInvoiceDOE.Enabled = false;
            lnkGenerateInvoiceSlotExtension.Enabled = false;
            lnkGenInvPGR.Enabled = false;
            lnkGenInvSecirity.Enabled = false;
            lnkGenInvFinalDo.Enabled = false;
            lnkGenInvOtherInvoice.Enabled = false;
            lnkPrintAmend.Enabled = false;
            btnBondSave.Enabled = false;


        }

        protected void lnkSecurityInv_Click(object sender, EventArgs e)
        {
            mpeSecurity.Show();
        }

        protected void lnkPGRExtension_Click(object sender, EventArgs e)
        {
            mpePGR.Show();
        }

        protected void lnkOtherInv_Click(object sender, EventArgs e)
        {
            mpeOi.Show();
        }

        void ClearAll()
        {
            txtCha.Text = string.Empty;
            txtLandingDate.Text = string.Empty;
            txtDoValidUpto.Text = string.Empty;
            txtVessel.Text = string.Empty;
            txtVoyage.Text = string.Empty;
            txtDetentionFreeDays.Text = string.Empty;
            tstDetentionTill.Text = string.Empty;
            txtPGRFreedays.Text = string.Empty;
            txtPGRTill.Text = string.Empty;

            txtFreightToCollect.Text = string.Empty;
            ddlDestuffing.SelectedIndex = 0;
            txtVAlidityDate.Text = string.Empty;
            txtExtensionForDetention.Text = string.Empty;
            txtExtensionForPGR.Text = string.Empty;
            ddlAmendmentFor.SelectedIndex = 0;
            txtBondCancellation.Text = string.Empty;

            gvwInvoice.DataSource = null;
            gvwInvoice.DataBind();

            chkOriginalBL.Checked = false;
            chkEndorseHBL.Checked = false;
            chkContainerBond.Checked = false;
            chkBankGuarantee.Checked = false;
            chkInsuranceCopy.Checked = false;
            chkCopyOfMasterBL.Checked = false;
            chkSecurityCheque.Checked = false;
            chkCopyOfBill.Checked = false;
            chkConsoldatorNOC.Checked = false;
            chkCHSSA.Checked = false;

            dvDoc.InnerHtml = string.Empty;
            //gvwVendor
            /*
             * InvoiceType
             * InvoiceNo
             * Ammount
             * ReceivedAmt
             * 
             * BlId
             * */

        }

        protected void lnkFreightToCollect_Click(object sender, EventArgs e)
        {
            mpeFreight.Show();
        }

        protected void lnkFinalInvoice_Click(object sender, EventArgs e)
        {
            mpeFinalInv.Show();

        }

        protected void gvwVendor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //System.Web.UI.HtmlControls.HtmlAnchor aInvoice = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("aInvoice");
                //aInvoice.HRef = "ManageInvoice.aspx?invid=" + e.da;
                //href='<% "ManageInvoice.aspx?invid=" + GeneralFunctions.EncryptQueryString(Eval("InvoiceID")ToString()) %>'
            }
        }

        protected void ShowReceivedAmt(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlAnchor a = (System.Web.UI.HtmlControls.HtmlAnchor)sender;

            GridViewRow Row = (GridViewRow)a.NamingContainer;
            HiddenField hdnInvID = (HiddenField)Row.FindControl("hdnInvID");

            DataTable dtDoc = oImportBLL.GetReceivedAmtBreakup(Convert.ToInt64(hdnInvID.Value));

            StringBuilder sbr = new StringBuilder();
            sbr.Append("<table style='width: 100%; border: none;' cellpadding='0' cellspacing='0'>");
            sbr.Append("<tr style='background-color:#328DC4;color:White; font-weight:bold;'>");
            sbr.Append("<td style='width: 50px;padding-left:2px;'>MRNo.</td>");
            sbr.Append("<td style='width: 80px;'>Date</td>");
            sbr.Append("<td style='width: 80px;'>Cash</td>");
            sbr.Append("<td style='width: 80px;'>Cheque</td>");
            sbr.Append("<td style='width: 80px;'>TDS</td>");
            sbr.Append("<td style='width: 50px;text-align:center;'>Edit</td>");
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
                    sbr.Append("<td>" + CASH + "</td>");
                    sbr.Append("<td>" + CHEQUE + "</td>");
                    sbr.Append("<td>" + TDS + "</td>");
                    sbr.Append("<td><a href='AddEditMoneyReceipts.aspx?mrid=" + GeneralFunctions.EncryptQueryString(MRID) + "'><img src='../Images/edit.png' /></a></td>");
                    sbr.Append("</tr>");
                }
                else // For Even Row
                {
                    sbr.Append("<tr style='background-color:#99CCFF;'>");
                    sbr.Append("<td>" + MRNO + "</td>");
                    sbr.Append("<td>" + DATE + "</td>");
                    sbr.Append("<td>" + CASH + "</td>");
                    sbr.Append("<td>" + CHEQUE + "</td>");
                    sbr.Append("<td>" + TDS + "</td>");
                    sbr.Append("<td><a href='AddEditMoneyReceipts.aspx?mrid=" + GeneralFunctions.EncryptQueryString(MRID) + "'><img src='../Images/edit.png' /></a></td>");

                    sbr.Append("</tr>");
                }
            }

            sbr.Append("</table>");

            dvMoneyReceived.InnerHtml = sbr.ToString();

            mpeMoneyReceivedDetail.Show();

        }

    }
}