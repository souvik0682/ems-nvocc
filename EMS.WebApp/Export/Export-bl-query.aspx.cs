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
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data.SqlTypes;
using EMS.Common;
using EMS.Entity;

namespace EMS.WebApp.Transaction
{
    public partial class Export_bl_query : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            string[] Names = new string[5] { "A", "A", "A", "A", "A"};
            gvwInvoice.DataSource = Names;
            gvwInvoice.DataBind();
        }
    }
}