using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities;
using EMS.BLL;

namespace EMS.WebApp.MasterModule
{
    public partial class Export_charge_add_edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] Name = new string[5];
            dgChargeRates.DataSource = Name;
            dgChargeRates.DataBind();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MasterModule/Export-charge-list.aspx");
        }

        protected void rdbDestinationCharge_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdbDestinationCharge.SelectedValue == "1")
                rdbDestinationCharge.Enabled = true;
            else
                rdbDestinationCharge.Enabled = false;
        }

        protected void rdbTerminalRequired_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddlFTerminal
            RadioButtonList rdl = (RadioButtonList)sender;
            TerminalSelection(rdl);
            //if (rdbTerminalRequired.SelectedValue == "1")
            //    TerminalSelection();
        }

        void PopulateDropDown(int Number, DropDownList ddl, int? Filter)
        {
            CommonBLL.PopulateDropdown(Number, ddl, Filter, 0);
        }

        private void TerminalSelection(RadioButtonList rdl)
        {
            GridViewRow Row = dgChargeRates.HeaderRow;
            DropDownList ddlFTerminal = (DropDownList)Row.FindControl("ddlFTerminal");
            //DropDownList ddlFLocation = (DropDownList)Row.FindControl("ddlFLocation");

            if (rdl.SelectedItem.Value == "0")
            {
                if (ddlFTerminal.Items.Count > 0)
                {
                    //ddlFTerminal.SelectedIndex = 0;
                    ddlFTerminal.Items.Clear();
                }
                ddlFTerminal.Enabled = false;
            }
            else
            {
                ddlFTerminal.Enabled = true;
                ddlFTerminal.Items.Clear();
                //if (Convert.ToInt32(ddlHeaderLocation.SelectedValue) > 0)
                //    PopulateDropDown((int)Enums.DropDownPopulationFor.TerminalCode, ddlFTerminal, Convert.ToInt32(ddlHeaderLocation.SelectedValue));

                //if (ddlHeaderLocation.SelectedValue == "-1")
                //{
                //    ListItem Li = new ListItem("ALL", "-1");
                //    ddlFTerminal.Items.Insert(0, Li);
                //}
            }
        }
    }
}