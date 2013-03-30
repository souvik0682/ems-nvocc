using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.WebApp.Reports.ReportViewer;
namespace EMS.WebApp.Popup
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ReportUtil rpt = new ReportUtil();
                    rpt.LoadReport(rptViewer, Request.QueryString);
                }
                catch
                {
                    Response.Clear();
                }
            }

        }
    }
}