using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EMS.BLL;

namespace EMS.WebApp.Import
{
    public partial class bl_query : System.Web.UI.Page
    {
        DataSet BLDataSet=new DataSet();
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
            fillBLDetail(BLDataSet.Tables[0]);
        }

        void fillBLDetail(DataTable dtDetail)
        {
            if (dtDetail.Rows.Count > 0)
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
        }
    }
}