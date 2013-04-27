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
            if (!IsPostBack)
            {
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
            string fileName = string.Empty;
            FileUtil t = null;

            switch (CommonBLL.GetTerminalType(Convert.ToInt32(ddlVoyage.SelectedValue)))
            {
                case "NSICT":
                    fileName = Server.MapPath("~/Download/" + DateTime.Now.Ticks.ToString() + ".xlsx");
                    t = new FileUtil(Server.MapPath("~/FileTemplate/Template.xlsx"), fileName);
                    if (CommonBLL.GenerateExcel(fileName, ddlLine.SelectedValue, ddlVessel.SelectedValue, hdnReturn.Value, ddlLocation.SelectedValue, ddlVoyage.SelectedValue, txtVIANo.Text))
                    {
                        t.Download(Response);
                    }
                    break;

                case "JNPT":
                case "GTI":
                    fileName = Server.MapPath(@"d:\Containers.txt");
                    //t = new FileUtil(Server.MapPath("~/FileTemplate/Template.xlsx"), fileName);
                    t = new FileUtil();
                    if (CommonBLL.GenerateText(fileName, Convert.ToInt32(ddlLine.SelectedValue), Convert.ToInt32(ddlVessel.SelectedValue), Convert.ToInt32(hdnReturn.Value), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlVoyage.SelectedValue), Convert.ToInt32(txtVIANo.Text)))
                    {
                        t.Download(Response);
                    }
                    break;
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