using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using EMS.BLL;
using EMS.Utilities;
using System.Data;

namespace EMS.WebApp
{
    /// <summary>
    /// Summary description for GetLocation
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class GetLocation : System.Web.Services.WebService
    {
        public GetLocation()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod]
        public string[] GetCompletionList(string prefixText, int count)
        {
            DataTable dt = ImportHaulageBLL.GetAllPort(prefixText);
            string[] PortNames = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PortNames[i] = dt.Rows[i]["PortName"].ToString();
            }
            return PortNames;
        }
    }
}
