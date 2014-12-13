using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Common;
using EMS.Entity.Report;
using System.Data;
using EMS.Entity;

namespace EMS.WebApp.Forwarding.Report
{
    public partial class CreditorInvoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            rptViewer.LocalReport.Refresh();
        }
    }
}