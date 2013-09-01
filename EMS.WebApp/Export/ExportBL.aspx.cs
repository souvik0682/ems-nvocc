using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities;

namespace EMS.WebApp.Export
{
    public partial class ExportBL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string encryptedId = GeneralFunctions.EncryptQueryString("-1");
            Response.Redirect("~/Export/ManageExportBL.aspx?BLId=" + encryptedId);
        }
    }
}