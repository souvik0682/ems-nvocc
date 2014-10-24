using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities;

namespace EMS.WebApp.CustomControls
{ 
    [System.Web.Script.Services.ScriptService]
    public partial class AutoCompleteExtender : System.Web.UI.UserControl
    {
        public string CountryName { get { return txtCountry.Text; } set { txtCountry.Text = value; } }
        public string CountryId { get { return hdnCountryId.Value; } set { hdnCountryId.Value = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

       

     



    }
}