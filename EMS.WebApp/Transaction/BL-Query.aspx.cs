using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EMS.BLL;

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


    }
}