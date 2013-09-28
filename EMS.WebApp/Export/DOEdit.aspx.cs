using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.WebApp.Export
{
    public partial class DOEdit : System.Web.UI.Page
    {
        #region Private Member Variables

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add(new System.Data.DataColumn("ID"));
                System.Data.DataRow row = dt.NewRow();
                dt.Rows.Add(row);

                System.Data.DataRow row1 = dt.NewRow();
                dt.Rows.Add(row1);

                gvwList.DataSource = dt;
                gvwList.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Export/DOList.aspx");
        }

        protected void gvwList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        #endregion

        #region Private Methods

        private void CheckUserAccess()
        {

        }

        private void RetriveParameters()
        {

        }

        private void SetAttributes()
        {

        }

        #endregion
    }
}