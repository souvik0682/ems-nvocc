using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities;
using EMS.Utilities.ResourceManager;
using EMS.Common;

namespace EMS.WebApp.MasterModule
{
    public partial class AddEditPort : System.Web.UI.Page
    {

        #region Private Member Variables

        private int _userId = 0;
        private bool _hasEditAccess = true;
        private string portId = "";
        #endregion


        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            _userId = EMS.BLL.UserBLL.GetLoggedInUserId();

            if (!IsPostBack)
            {
                portId = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);
                ClearText();
                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManagePort.aspx','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
                txtPortName.Attributes["onkeypress"] = "javascript:return SetMaxLength(this, 200)";
                if (portId != "-1")
                    LoadData(portId);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            portId = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);
            SaveData(portId);

        }

        #endregion

        #region Private Methods

        private void LoadData(string portId)
        {
            ClearText();

            int intportId = 0;
            if (portId == "" || !Int32.TryParse(portId, out intportId))
                return;
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            System.Data.DataSet ds = dbinteract.GetPort(Convert.ToInt32(portId), "", "");

            if (!ReferenceEquals(ds, null) && ds.Tables[0].Rows.Count > 0)
            {
                txtPortName.Text = ds.Tables[0].Rows[0]["PortName"].ToString();
                txtPortCode.Text = ds.Tables[0].Rows[0]["PortCode"].ToString();
                ddlICD.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["ICDIndicator"]);
                txtPortAddressee.Text = ds.Tables[0].Rows[0]["PortAddressee"].ToString();
                txtAdd1.Text = ds.Tables[0].Rows[0]["Address2"].ToString();
                txtAdd2.Text = ds.Tables[0].Rows[0]["Address3"].ToString();
                txtExportPort.Text = ds.Tables[0].Rows[0]["ExportPort"].ToString();

            }
        }
        private void ClearText()
        {
            txtPortCode.Text = "";
            txtPortName.Text = "";
            txtPortAddressee.Text = "";
            txtAdd1.Text = "";
            txtAdd2.Text = "";
        }
        private void SaveData(string portId)
        {
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            bool isedit = portId != "-1" ? true : false;
            if (!isedit)
                // if (dbinteract.GetPort(-1, txtPortCode.Text.Trim(), "").Tables[0].Rows.Count > 0)
                if (!dbinteract.IsUnique("mstPort", "PortCode", txtPortCode.Text.Trim()))
                {
                    GeneralFunctions.RegisterAlertScript(this, "Port Code must be unique. The given code has already been used for another port. Please try with another one.");
                    return;
                }
            int result = dbinteract.AddEditPort(_userId, Convert.ToInt32(portId), txtPortName.Text.Trim(), txtPortCode.Text, ddlICD.SelectedIndex == 0 ? false : true, txtPortAddressee.Text, txtAdd1.Text, txtAdd2.Text,txtExportPort.Text, isedit);


            if (result > 0)
            {
                Response.Redirect("~/MasterModule/ManagePort.aspx");
            }
            else
            {
                GeneralFunctions.RegisterAlertScript(this, "Error Occured");
            }
        }


        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }
    }
}