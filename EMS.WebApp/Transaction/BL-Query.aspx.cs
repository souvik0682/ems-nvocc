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

namespace EMS.WebApp.Transaction
{
    public partial class BL_Query : System.Web.UI.Page
    {
        DataSet BLDataSet = new DataSet();
        ImportBLL oImportBLL = new ImportBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            //string[] Val = new string[] { "A", "A", "A", "A", "A" };
            //gvwVendor.DataSource = Val;
            //gvwVendor.DataBind();

            if (!Page.IsPostBack)
            {
                FillDropDown();
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
        }

        protected void txtBlNo_TextChanged(object sender, EventArgs e)
        {
            PopulateAllData();
        }

        private void PopulateAllData()
        {
            BLDataSet = oImportBLL.GetBLQuery(txtBlNo.Text.Trim(), (int)EMS.Utilities.Enums.BLActivity.DOE);
            txtBlNo.Text = string.Empty;
            if (BLDataSet.Tables[0].Rows.Count > 0)
            {
                fillBLDetail(BLDataSet.Tables[0]);
                fillServiceRequest(BLDataSet.Tables[0]);
                FillUploadedDocument(BLDataSet.Tables[1]);
                FillSubmittedDocument(BLDataSet.Tables[2]);
                FillInvoiceStatus(Convert.ToInt64(hdnBLId.Value));
                FillDoExtension(BLDataSet.Tables[3]);
            }
        }

        void fillBLDetail(DataTable dtDetail)
        {
            hdnBLId.Value = dtDetail.Rows[0]["BLID"].ToString();
            txtBlNo.Text = dtDetail.Rows[0]["BLNO"].ToString();
            txtCha.Text = dtDetail.Rows[0]["CHA"].ToString();
            txtHouseBlNo.Text = dtDetail.Rows[0]["HBLNO"].ToString();
            txtDetentionFee.Text = dtDetail.Rows[0]["DTNFEE"].ToString();
            txtDoValidTill.Text = dtDetail.Rows[0]["DOVALIDUPTO"].ToString();
            txtLandingDate.Text = Convert.ToDateTime(dtDetail.Rows[0]["LANDINGDT"].ToString()).ToString("dd/MM/yyyy");
            txtVessel.Text = dtDetail.Rows[0]["VESSEL"].ToString();
            txtVoyage.Text = dtDetail.Rows[0]["VOYAGE"].ToString();
            txtDetentionFreeDays.Text = dtDetail.Rows[0]["DTNFREEDAYS"].ToString();
            txtDetentionFee.Text = dtDetail.Rows[0]["DTNFEE"].ToString();
            txtPGRFreedays.Text = dtDetail.Rows[0]["PGRFREEDAYS"].ToString();
            txtPGRTill.Text = dtDetail.Rows[0]["PGRTILL"].ToString();

        }

        void fillServiceRequest(DataTable dtDetail)
        {
            txtDoValidTill.Text = Convert.ToDateTime(txtLandingDate.Text).AddDays(Convert.ToDouble(txtDetentionFreeDays.Text) - 1).ToShortDateString();

            if (dtDetail.Rows[0]["FREIGHTTYPE"].ToString().ToLower() == "pp")
                chkFreightPaidstatus.Checked = true;
            else
                chkFreightPaidstatus.Checked = false;


            if (dtDetail.Rows[0]["DESTUFFING"].ToString() == "0")
                ddlDestuffing.SelectedIndex = 0;
            else
                ddlDestuffing.SelectedIndex = 1;

            chkBankGuarantee.Checked = Convert.ToBoolean(dtDetail.Rows[0]["BANKGUARANTEE"].ToString());

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

            gvwVendor.DataSource = dtInvoices;
            gvwVendor.DataBind();
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
            }
        }

        void FillDoExtension(DataTable dtDOE)
        {
            StringBuilder sbr = new StringBuilder();
            if (dtDOE.Rows.Count > 0)
            {
                sbr.Append("<table style='width:80%;' cellspacing='0' align='center'>");
                sbr.Append("<tr style='height:30px;background-color:#328DC4;color:White; font-weight:bold;'><td>Date</td><td>Print</td></tr>");

                for (int rowCount = 0; rowCount < dtDOE.Rows.Count; rowCount++)
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

                sbr.Append("</table>");
                dvDoExtension.InnerHtml = sbr.ToString();
            }
            else
            {
                sbr.Append("<table style='width:80%;' cellspacing='0' align='center'>");
                sbr.Append("<tr><td>No records found</td></tr>");
                sbr.Append("</table>");
                dvDoExtension.InnerHtml = sbr.ToString();
            }
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

            if (chkDo.Checked == true)
            {
                ddlDestuffing.Enabled = true;
                txtDoValidTill.Enabled = true;
                chkFreightPaidstatus.Enabled = true;
                chkBankGuarantee.Enabled = true;
                lnkGenerateInvoiceDo.Enabled = true;
                lnkGenerateInvoiceDo.Enabled = true;
            }
            else
            {
                ddlDestuffing.Enabled = false;
                txtDoValidTill.Enabled = false;
                chkFreightPaidstatus.Enabled = false;
                chkBankGuarantee.Enabled = false;
                lnkGenerateInvoiceDo.Enabled = false;
                lnkGenerateInvoiceDo.Enabled = false;
            }
        }

        protected void chkDoExtension_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDoExtension.Checked == true)
            {
                txtVAlidityDate.Enabled = true;
                lnkGenerateInvoiceDOE.Enabled = true;
            }
            else
            {
                txtVAlidityDate.Enabled = false;
                lnkGenerateInvoiceDOE.Enabled = false;
            }
        }

        protected void chkSlotExtension_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSlotExtension.Checked == true)
            {
                txtExtensionForDetention.Enabled = true;
                txtExtensionForPGR.Enabled = true;
                lnkGenerateInvoiceSlotExtension.Enabled = true;

            }
            else
            {
                txtExtensionForDetention.Enabled = false;
                txtExtensionForPGR.Enabled = false;
                lnkGenerateInvoiceSlotExtension.Enabled = false;
            }
        }

        protected void chkAmendment_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAmendment.Checked == true)
            {
                ddlAmendmentFor.Enabled = true;
            }
            else
            {
                ddlAmendmentFor.Enabled = false;
            }
        }

        protected void chkBondCancel_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBondCancel.Checked == true)
            {
                txtBondCancellation.Enabled = true;
            }
            else
            {
                txtBondCancellation.Enabled = false;
            }
        }

        protected void imgBtnExaminationDo_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("/#?bl");
        }

        protected void lnkGenerateInvoiceDo_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtBlNo.Text))
                Response.Redirect("~/Reports/InvDO.aspx?BL=" + txtBlNo.Text.Trim());
            else
                lblMessageServiceReq.Text = ResourceManager.GetStringWithoutName("ERR00080");
        }

        protected void lnkGenerateInvoiceDOE_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtBlNo.Text))
                Response.Redirect("~/Reports/InvDOExt.aspx?BL=" + txtBlNo.Text.Trim());
            else
                lblMessageServiceReq.Text = ResourceManager.GetStringWithoutName("ERR00080");
        }

        protected void lnkGenerateInvoiceSlotExtension_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtBlNo.Text))
                Response.Redirect("~/Reports/InvSlotExt.aspx?BL=" + txtBlNo.Text.Trim());
            else
                lblMessageServiceReq.Text = ResourceManager.GetStringWithoutName("ERR00080");
        }

        protected void btnSave2_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                switch (oImportBLL.SaveBLQActivity(GenerateBLQActivityXMLString(), Convert.ToInt32(hdnBLId.Value)))
                {
                    case 1: PopulateAllData();
                        lblMessageServiceReq.Text= ResourceManager.GetStringWithoutName("ERR00009");
                        UpdatePanel2.Update();
                        break;
                }

            }
        }


        string GenerateBLQActivityXMLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<Activity>");

            if (chkDo.Checked)
            {
                sb.Append("<Item>");

                sb.Append("<BLID>" + hdnBLId.Value + "</BLID>");
                sb.Append("<AT>" + (int)EMS.Utilities.Enums.BLActivity.DO + "</AT>");
                sb.Append("<AD>" + DateTime.Today.Date.ToString("MM/dd/yyyy") + "</AD>");
                sb.Append("<VD>" + Convert.ToDateTime(txtDoValidTill.Text).ToString("MM/dd/yyyy") + "</VD>");
                sb.Append("<BG>" + (chkBankGuarantee.Checked ? "1" : "0") + "</BG>");
                sb.Append("<IG>" + 0 + "</IG>");
                sb.Append("<NOP>" + 0 + "</NOP>");

                sb.Append("</Item>");
            }

            if (chkDoExtension.Checked)
            {
                sb.Append("<Item>");

                sb.Append("<BLID>" + hdnBLId.Value + "</BLID>");
                sb.Append("<AT>" + (int)EMS.Utilities.Enums.BLActivity.DOE + "</AT>");
                sb.Append("<AD>" + DateTime.Today.Date.ToString("MM/dd/yyyy") + "</AD>");
                sb.Append("<VD>" + Convert.ToDateTime(txtVAlidityDate.Text).ToString("MM/dd/yyyy") + "</VD>");
                sb.Append("<BG>" + "0" + "</BG>");
                sb.Append("<IG>" + 0 + "</IG>");
                sb.Append("<NOP>" + 0 + "</NOP>");

                sb.Append("</Item>");
            }


            if (chkSlotExtension.Checked)
            {
                sb.Append("<Item>");

                sb.Append("<BLID>" + hdnBLId.Value + "</BLID>");
                sb.Append("<AT>" + (int)EMS.Utilities.Enums.BLActivity.SE + "</AT>");
                if (!String.IsNullOrEmpty(txtExtensionForDetention.Text))
                    sb.Append("<AD>" + Convert.ToDateTime(txtExtensionForDetention.Text).ToString("MM/dd/yyyy") + "</AD>");
                else
                    sb.Append("<AD></AD>");

                if (!String.IsNullOrEmpty(txtExtensionForPGR.Text))
                    sb.Append("<VD>" + Convert.ToDateTime(txtExtensionForPGR.Text).ToString("MM/dd/yyyy") + "</VD>");
                else
                    sb.Append("<VD></VD>");

                sb.Append("<BG>" + "0" + "</BG>");
                sb.Append("<IG>" + 0 + "</IG>");
                sb.Append("<NOP>" + 0 + "</NOP>");

                sb.Append("</Item>");
            }

            if (chkBondCancel.Checked)
            {
                sb.Append("<Item>");

                sb.Append("<BLID>" + hdnBLId.Value + "</BLID>");
                sb.Append("<AT>" + (int)EMS.Utilities.Enums.BLActivity.BC + "</AT>");
                sb.Append("<AD>" + DateTime.Today.Date.ToString("MM/dd/yyyy") + "</AD>");
                sb.Append("<VD>" + Convert.ToDateTime(txtBondCancellation.Text).ToString("MM/dd/yyyy") + "</VD>");
                sb.Append("<BG>" + "0" + "</BG>");
                sb.Append("<IG>" + 0 + "</IG>");
                sb.Append("<NOP>" + 0 + "</NOP>");

                sb.Append("</Item>");
            }

            sb.Append("</Activity>");

            /*
            foreach (GridViewRow gvRow in gvSelectedContainer.Rows)
            {
                HiddenField hdnOldTransactionId = (HiddenField)gvRow.FindControl("hdnOldTransactionId");
                HiddenField hdnCurrentTransactionId = (HiddenField)gvRow.FindControl("hdnCurrentTransactionId");
                CheckBox chkItem = (CheckBox)gvRow.FindControl("chkItem");

                sb.Append("<Cont>");

                sb.Append("<Oid>" + hdnOldTransactionId.Value + "</Oid>");
                sb.Append("<Nid>" + hdnCurrentTransactionId.Value + "</Nid>");
                sb.Append("<Stats>" + chkItem.Checked.ToString() + "</Stats>");

                sb.Append("</Cont>");

            }*/



            return sb.ToString();
        }

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
            sbr.Append(chkCHSSA.Checked == true ? "1" : "0");

            oImportBLL.SaveSubmittedDocument(Convert.ToInt64(hdnBLId.Value), sbr.ToString());
        }
    }
}