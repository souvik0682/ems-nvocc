using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Common;
using EMS.Utilities;
using EMS.Utilities.ResourceManager;

namespace EMS.WebApp.MasterModule
{
    public partial class AddEditLine : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private bool _hasEditAccess = true;
        private string NVOCCId = "";
        #endregion


        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            IUser user = (IUser)Session[Constants.SESSION_USER_INFO];
            _userId = user == null ? 0 : user.Id;

            if (!IsPostBack)
            {
                NVOCCId = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);
                ClearText();
                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageLine.aspx','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
                txtLineName.Attributes["onkeypress"] = "javascript:return SetMaxLength(this, 50)";
                if (NVOCCId != "-1")
                    LoadData(NVOCCId);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            NVOCCId = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);
            SaveData(NVOCCId);

        }

        #endregion

        #region Private Methods

        private void LoadData(string NVOCCId)
        {
            ClearText();

            int intNVOCCId = 0;
            if (NVOCCId == "" || !Int32.TryParse(NVOCCId, out intNVOCCId))
                return;
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            System.Data.DataSet ds = dbinteract.GetNVOCCLine(Convert.ToInt32(NVOCCId), "");

            if (!ReferenceEquals(ds, null) && ds.Tables[0].Rows.Count > 0)
            {
                txtLineName.Text = ds.Tables[0].Rows[0]["NVOCCName"].ToString();
                txtFreeDays.Text = Convert.ToString(ds.Tables[0].Rows[0]["DefaultFreeDays"]);
                txtContact.Text = Convert.ToString(ds.Tables[0].Rows[0]["ContAgentCode"]);

                txtImpCommsn.Text = Convert.ToString(ds.Tables[0].Rows[0]["ImportCommission"]);
                txtExportCommission.Text = Convert.ToString(ds.Tables[0].Rows[0]["ExportCommission"]);
                string exp = Convert.ToString(ds.Tables[0].Rows[0]["ExportBooking"]);
                ddlExpBook.SelectedValue = exp == "1" ? "y" : "n";

                imgLogo.ImageUrl = string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["LogoPath"])) ? "~/MasterModule/Image/Defaultlogo.png" : ds.Tables[0].Rows[0]["LogoPath"].ToString();
                hdnLogo.Value = imgLogo.ImageUrl;

            }
        }
        private void ClearText()
        {
            txtLineName.Text = "";
            txtFreeDays.Text = "";
            txtContact.Text = "";

        }
        private void SaveData(string NVOCCId)
        {
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            bool isedit = NVOCCId != "-1" ? true : false;
          
           
            if (!isedit)
                if (!dbinteract.IsUnique("mstNVOCC","NVOCCName",txtLineName.Text.Trim()))
                {
                    GeneralFunctions.RegisterAlertScript(this, "Line Name must be unique. The given name has already been used for another Line. Please try with another one.");
                    return;
                }

            if (fuLogo.HasFile)
            {
                hdnLogo.Value = "~/MasterModule/Image/" + fuLogo.GetImageFileName();
                fuLogo.PostedFile.SaveAs( Server.MapPath(hdnLogo.Value));
            }
            else
                hdnLogo.Value = "";
            decimal impComm = 0;
            decimal.TryParse(txtImpCommsn.Text,out impComm);
            decimal expCommsn = 0;
            decimal.TryParse(txtExportCommission.Text,out expCommsn);
            char ExpBook =  ddlExpBook.SelectedValue=="y"?'1':'0';
            int result = dbinteract.AddEditLine(_userId, Convert.ToInt32(NVOCCId), txtLineName.Text.Trim(), string.IsNullOrEmpty(txtFreeDays.Text) ? -1 :  Convert.ToInt32(txtFreeDays.Text), txtContact.Text,impComm,expCommsn,ExpBook, hdnLogo.Value, isedit);


            if (result > 0)
            {
                Response.Redirect("~/MasterModule/ManageLine.aspx");
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