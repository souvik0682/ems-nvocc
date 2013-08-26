using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.WebApp.MasterModule
{
    public partial class Export_charge_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] Names = new string[5] { "A", "A", "A", "A", "A" };

            gvwCharge.DataSource = Names;
            gvwCharge.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MasterModule/Export-charge-add-edit.aspx");
        }
    }
}