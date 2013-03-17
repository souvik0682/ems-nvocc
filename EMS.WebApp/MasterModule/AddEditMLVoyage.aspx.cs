using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities;
using EMS.Common;
using EMS.Utilities.ResourceManager;

namespace EMS.WebApp.MasterModule
{
    public partial class AddEditMLVoyage : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private bool _hasEditAccess = true;
        private string VoyageId = "";
        #endregion

        BLL.DBInteraction dbinteract = new BLL.DBInteraction();
        protected void Page_Load(object sender, EventArgs e)
        {
            _userId = EMS.BLL.UserBLL.GetLoggedInUserId();

            VoyageId = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);

            if (!IsPostBack)
            {
               
                GeneralFunctions.PopulateDropDownList(ddlVessel, dbinteract.PopulateDDLDS("trnVessel", "pk_VesselID", "VesselName"));
                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('MainLineVoyage.aspx','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
                if (VoyageId != "-1")
                    LoadData(VoyageId);
            }
           
            //
        }

   


        private void LoadData(string VoyageId)
        {
            int intVoyageId = 0;
            if (VoyageId == "" || !Int32.TryParse(VoyageId, out intVoyageId))
                return;
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            System.Data.DataSet ds = dbinteract.GetMLVoyage(Convert.ToInt32(VoyageId), "a", "", "");

            if (!ReferenceEquals(ds, null) && ds.Tables[0].Rows.Count > 0)
            {
                ddlVessel.SelectedValue = ds.Tables[0].Rows[0]["fk_VesselID"].ToString();
                txtVoyageNo.Text = ds.Tables[0].Rows[0]["MLVoyageNo"].ToString();
                txtdtActivity.Text = ds.Tables[0].Rows[0]["ActivityDate"].ToString();
                ((TextBox)AutoCompletepPort4.FindControl("txtPort")).Text = ds.Tables[0].Rows[0]["lastport"].ToString();

            }
        }

      
     

       

        protected void btnSave_Click(object sender, EventArgs e)
        {
            VoyageId = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);
            bool isedit = VoyageId != "-1" ? true : false;

            int fk_PortID = 0; 
            try
            {
                string port = ((TextBox)AutoCompletepPort4.FindControl("txtPort")).Text;
                port = port.Contains(',') && port.Split(',').Length >= 1 ? port.Split(',')[1].Trim() : "";
                 fk_PortID = dbinteract.GetId("Port", port);
            }
            catch
            {
                
               
            }


            int result = EMS.BLL.VoyageBLL.AddEditMLVoyage(VoyageId,ddlVessel.SelectedValue, txtVoyageNo.Text.ToUpper(), txtdtActivity.Text, fk_PortID, _userId, isedit);

            if (result > 0)
            {
                
                Response.Redirect("~/MasterModule/MainLineVoyage.aspx");
            
            }
            else
            {
                GeneralFunctions.RegisterAlertScript(this, "Error Occured");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }
    }
}