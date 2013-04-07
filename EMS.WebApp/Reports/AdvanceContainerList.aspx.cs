using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities;
using EMS.Common;
using EMS.BLL;

namespace EMS.WebApp.Reports
{
	public partial class AdvanceContainerList : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack) {
                Filler.FillData<ILocation>(ddlLine, new CommonBLL().GetActiveLocation(), "Name", "Id", "Location");
            }
		}
        protected void ddlLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLine.SelectedIndex > 0)
            {
                
                    Filler.FillData(ddlLocation, CommonBLL.GetLine(ddlLine.SelectedValue), "ProspectName", "ProspectID", "Line");
                
            }
        }


        protected void btnReport_Click(object sender, EventArgs e)
        {  
            var fileName=Server.MapPath("~/Download/" + DateTime.Now.Ticks.ToString() + ".xlsx");
            var t = new FileUtil(Server.MapPath("~/FileTemplate/Template.xlsx"),fileName );
            if (CommonBLL.GenerateExcel(fileName, ddlLine.SelectedValue, ddlVessel.SelectedValue, hdnReturn.Value, ddlLocation.SelectedValue, ddlVoyage.SelectedValue, txtVIANo.Text))
            {
                t.Download(Response);
            }
        }
        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLocation.SelectedIndex > 0)
            {
                Filler.FillData(ddlVessel, CommonBLL.GetVessels(ddlLocation.SelectedValue), "VesselName", "VesselID", "Vessel");
            }
        }

        protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlVessel.SelectedIndex > 0)
            {
                Filler.FillData(ddlVoyage, CommonBLL.GetVoyages(ddlVessel.SelectedValue, ddlLocation.SelectedValue), "VoyageNo", "VoyageID", "Voyage");
            }
        }

	}
}