using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.WebApp.Export
{
    public partial class ManageBooking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkContainerDtls_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();
        }

        protected void lnkTransitRoute_Click(object sender, EventArgs e)
        {
            ModalPopupExtender2.Show();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Export/Booking.aspx");
        }
    }
}