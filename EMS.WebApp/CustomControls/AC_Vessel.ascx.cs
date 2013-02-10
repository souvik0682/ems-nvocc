using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.WebApp.CustomControls
{
    public partial class AC_Vessel : System.Web.UI.UserControl
    {
        public string RandNo = Guid.NewGuid().ToString();

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        public event EventHandler TextChanged;
        protected void txtVessel_TextChanged(object sender, EventArgs e)
        {
            TextChanged(sender, e);
        }

    }
}