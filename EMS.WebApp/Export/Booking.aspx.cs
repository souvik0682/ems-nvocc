using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.WebApp.Export
{
    public partial class Booking : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private int _roleId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private bool _LocationSpecific = true;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Export/ManageBooking.aspx");
        }

        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void gvBooking_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void gvBooking_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void gvBooking_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
        }
    }
}