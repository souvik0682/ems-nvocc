using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.WebApp.CustomControls
{
    public partial class DatePIcker : System.Web.UI.UserControl
    {
        public  DateTime dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                for (int i = 1; i <= 31; i++)
                {
                    ddlDay.Items.Add(i.ToString());
                    ddlDay.Items[i-1].Value = i.ToString();
                }

                for (int i = 1975; i <= 2050; i++)
                {
                    ddlYear.Items.Add(i.ToString());
                    ddlYear.Items[i-1975].Value = i.ToString();
                }

                ddlDay.SelectedIndex = DateTime.Now.Day-1;
                ddlMonth.SelectedIndex = DateTime.Now.Month-1;
                ddlYear.SelectedIndex = DateTime.Now.Year - 1975;
            }

            string _dt = ddlDay.SelectedValue + "/" + ddlMonth.SelectedValue + "/" + ddlYear.SelectedValue;
            dt = Convert.ToDateTime(_dt);
        }

        protected void btnPick_Click(object sender, EventArgs e)
        {
           
            
        }
    }
}