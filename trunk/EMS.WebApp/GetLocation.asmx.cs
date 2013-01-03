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

            string s = string.Empty;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //PortNames[i] = dt.Rows[i]["PortName"].ToString();
                s = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i]["PortName"].ToString(), dt.Rows[i]["PortId"].ToString());
                PortNames[i] = s;
            }
            return PortNames;
        }

        //[WebMethod]
        //public string[] GetVoyageList(string prefixText, int count)
        //{
        //    DataTable dt = CommonBLL.GetVoyageList(prefixText);
        //    string[] voyageNos = new string[dt.Rows.Count];

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        voyageNos[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Convert.ToString(dt.Rows[i]["VoyageNo"]), Convert.ToString(dt.Rows[i]["VoyageID"]));
        //    }

        //    return voyageNos;
        //}

        //[WebMethod]
        //public string[] GetVesselList(string prefixText, int count)
        //{
        //    DataTable dt = CommonBLL.GetVesselList(prefixText);
        //    string[] vesselNames = new string[dt.Rows.Count];

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        vesselNames[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Convert.ToString(dt.Rows[i]["VesselName"]), Convert.ToString(dt.Rows[i]["VesselID"]));
        //    }

        //    return vesselNames;
        //}
    }
}
