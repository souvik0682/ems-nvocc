using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EMS.Utilities;
using EMS.BLL;
using System.Configuration;
using EMS.Utilities.ResourceManager;
using EMS.Entity;
using EMS.Common;

namespace EMS.WebApp.Transaction
{
    public partial class ManageInvoice : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadInvoiceTypeDDL();
                LoadLocationDDL();
                LoadBLNoDDL();
            }
        }
        #endregion

        #region Invoice Type
        private void LoadInvoiceTypeDDL()
        {
            try
            {
                DataTable dt = new InvoiceBLL().GetInvoiceType();

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["pk_DocTypeID"] = "0";
                    dr["DocAbbr"] = "--Select--";
                    dt.Rows.InsertAt(dr, 0);
                    ddlInvoiceType.DataValueField = "pk_DocTypeID";
                    ddlInvoiceType.DataTextField = "DocAbbr";
                    ddlInvoiceType.DataSource = dt;
                    ddlInvoiceType.DataBind();
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion

        #region Location
        private void LoadLocationDDL()
        {

            try
            {
                DataTable dt = new InvoiceBLL().GetLocation();

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["pk_LocID"] = "0";
                    dr["LocAbbr"] = "--Select--";
                    dt.Rows.InsertAt(dr, 0);
                    ddlLocation.DataValueField = "pk_LocID";
                    ddlLocation.DataTextField = "LocAbbr";
                    ddlLocation.DataSource = dt;
                    ddlLocation.DataBind();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region BL No
        private void LoadBLNoDDL()
        {

            try
            {
                DataTable dt = new InvoiceBLL().GetBLno();
                DataRow dr = dt.NewRow();
                dr["pk_BLID"] = "0";
                dr["ImpLineBLNo"] = "--Select--";
                dt.Rows.InsertAt(dr, 0);
                ddlBLno.DataValueField = "pk_BLID";
                ddlBLno.DataTextField = "ImpLineBLNo";
                ddlBLno.DataSource = dt;
                ddlBLno.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Gross Weight
        private void GrossWeight()
        {
            string BLno = ddlBLno.SelectedItem.Text;
            DataTable dt = new InvoiceBLL().GrossWeight(BLno);
            txtGrossWeightTON.Text = dt.Rows[0]["GrossWeight"].ToString();

        }
        #endregion

        private void TEU()
        {
            string BLno = ddlBLno.SelectedItem.Text;
            DataTable dt = new InvoiceBLL().TEU(BLno);
            txtTEU.Text = dt.Rows[0]["NoofTEU"].ToString();

        }

        private void FEU()
        {
            string BLno = ddlBLno.SelectedItem.Text;
            DataTable dt = new InvoiceBLL().FEU(BLno);
            txtFFU.Text = dt.Rows[0]["NoofFEU"].ToString();

        }

        private void Volume()
        {
            string BLno = ddlBLno.SelectedItem.Text;
            DataTable dt = new InvoiceBLL().Volume(BLno);
            txtVolume.Text = dt.Rows[0]["Volume"].ToString();

        }

        private void BLdate()
        {
            string BLno = ddlBLno.SelectedItem.Text;
            DataTable dt = new InvoiceBLL().BLdate(BLno);
            txtBLdate.Text = dt.Rows[0]["ImpLineBLDate"].ToString();

        }

        protected void ddlBLno_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Response.Write(ddlBLno.SelectedItem.Text);
            GrossWeight();
            TEU();
            FEU();
            Volume();
            BLdate();

        }

        protected void ddlChargeName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dgChargeRates_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void dgChargeRates_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void dgInvoiceChargeRates_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }
    }
}