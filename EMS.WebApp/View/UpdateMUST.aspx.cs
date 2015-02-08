using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EMS.Utilities;
using EMS.BLL;
using EMS.Common;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.WebApp.View
{
    public partial class UpdateMUST : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int currentMonth = DateTime.Now.Month.ToInt();
            ddlMonth.SelectedValue = currentMonth.ToString();
            string currentYear = DateTime.Now.Year.ToString();
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {

            txtYear.Text = txtYear.Text.Substring(0,4);

            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            dbinteract.UpdateMUSTRecord(ddlMonth.SelectedValue, txtYear.Text);
            lblmsg.Text = "MUST Updated";
        }

    }
}