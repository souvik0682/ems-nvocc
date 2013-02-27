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

namespace EMS.WebApp.Transaction
{
    public partial class BL_Query : System.Web.UI.Page
    {
        DataSet BLDataSet = new DataSet();
        ImportBLL oImportBLL = new ImportBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            string[] Val = new string[] { "A", "A", "A", "A", "A" };
            gvwVendor.DataSource = Val;
            gvwVendor.DataBind();
        }

        protected void txtBlNo_TextChanged(object sender, EventArgs e)
        {
            BLDataSet = oImportBLL.GetBLQuery(txtBlNo.Text.Trim());
            txtBlNo.Text = string.Empty;
            if (BLDataSet.Tables[0].Rows.Count > 0)
            {
                fillBLDetail(BLDataSet.Tables[0]);
                fillServiceRequest(BLDataSet.Tables[0]);
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
                oImportBLL.SaveBLQActivity(GenerateBLQActivityXMLString(), Convert.ToInt32(hdnBLId.Value));
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

    }
}