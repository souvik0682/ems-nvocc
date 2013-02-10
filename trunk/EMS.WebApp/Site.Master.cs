using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities;
using EMS.Common;
using EMS.BLL;

namespace EMS.WebApp
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //Clears the application cache.
            GeneralFunctions.ClearApplicationCache();

            SetUserAccess();

            if (!Request.Path.Contains("ChangePassword.aspx"))
            {
                if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
                {
                    IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                    if (ReferenceEquals(user, null) || user.Id == 0)
                    {
                        //Response.Redirect("~/Login.aspx");
                    }

                    SetAttributes(user);
                    ShowMenu(user);
                }
                else
                {
                    //Response.Redirect("~/Login.aspx");
                }
            }
            else
            {
                if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
                {
                    IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                    if (!ReferenceEquals(user, null) && user.Id > 0)
                    {
                        SetAttributes(user);
                        ShowMenu(user);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.DataBind();
            this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "Common", this.ResolveClientUrl("~/Scripts/Common.js"));
            this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "CustomTextBox", this.ResolveClientUrl("~/Scripts/CustomTextBox.js"));
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }

        protected void lnkPwd_Click(object sender, EventArgs e)
        {

        }

        private void SetAttributes(IUser user)
        {
            lblUserName.Text = "Welcome " + user.UserFullName;
        }

        private void ShowMenu(IUser user)
        {

        }

        private void SetUserAccess()
        {
            int menuId = 0;
            int userId = 0;

            userId = UserBLL.GetLoggedInUserId();
            menuId = GetMenuIdByPath();

            IUserPermission userPermission = UserBLL.GetMenuAccessByUser(userId, menuId);
            Session[Constants.SESSION_USER_PERMISSION] = userPermission;
        }

        private int GetMenuIdByPath()
        {
            int menuId = 0;

            if (Request.Path.Contains("/View/ManageUser.aspx") || Request.Path.Contains("/View/AddEditUser.aspx"))
            {
                menuId = (int)PageName.UserMaster;
            }
            else if (Request.Path.Contains("/View/ManageRole.aspx") || Request.Path.Contains("/View/AddEditRole.aspx"))
            {
                menuId = (int)PageName.RoleMaster;
            }
            else if (Request.Path.Contains("/View/ManageLocation.aspx") || Request.Path.Contains("/View/AddEditLocation.aspx"))
            {
                menuId = (int)PageName.LocationMaster;
            }
            else if (Request.Path.Contains("/View/ManageCustomer.aspx") || Request.Path.Contains("/View/AddEditCustomer.aspx"))
            {
                menuId = (int)PageName.CustomerMaster;
            }
            else if (Request.Path.Contains("/MasterModule/ManageLine.aspx") || Request.Path.Contains("/MasterModule/AddEditLine.aspx"))
            {
                menuId = (int)PageName.LineMSOMaster;
            }
            else if (Request.Path.Contains("/MasterModule/vendor-list.aspx") || Request.Path.Contains("/MasterModule/vendor-add-edit.aspx"))
            {
                menuId = (int)PageName.AddressMaster;
            }
            else if (Request.Path.Contains("/View/ManageServTax.aspx") || Request.Path.Contains("/View/AddEditSTax.aspx"))
            {
                menuId = (int)PageName.ServiceTaxMaster;
            }
            else if (Request.Path.Contains("/View/charge-list.aspx") || Request.Path.Contains("/View/charge-add-edit.aspx"))
            {
                menuId = (int)PageName.ChargeMaster;
            }
            else if (Request.Path.Contains("/View/ManageExchRate.aspx") || Request.Path.Contains("/View/AddEditExchRate.aspx"))
            {
                menuId = (int)PageName.ExchangeRateMaster;
            }
            else if (Request.Path.Contains("/MasterModule/ManageCountry.aspx") || Request.Path.Contains("/MasterModule/AddEditCountry.aspx"))
            {
                menuId = (int)PageName.CountryMaster;
            }
            else if (Request.Path.Contains("/MasterModule/ManagePort.aspx") || Request.Path.Contains("/MasterModule/AddEditPort.aspx"))
            {
                menuId = (int)PageName.PortMaster;
            }
            else if (Request.Path.Contains("/MasterModule/ImportHaulage-list.aspx") || Request.Path.Contains("/MasterModule/import-haulage-chrg-add-edit.aspx"))
            {
                menuId = (int)PageName.ImportHaulageChargeMaster;
            }
            else if (Request.Path.Contains("/MasterModule/ManageVessel.aspx"))
            {
                menuId = (int)PageName.VesselMaster;
            }
            else if (Request.Path.Contains("/MasterModule/MangeVoyage.aspx"))
            {
                menuId = (int)PageName.Voyage;
            }
            else if (Request.Path.Contains("/Reports/ImpBLChkLst.aspx"))
            {
                menuId = (int)PageName.ImportBLChklst;
            }
            else if (Request.Path.Contains("/Reports/IGMForm2.aspx"))
            {
                menuId = (int)PageName.IGMFormII;
            }
            else if (Request.Path.Contains("/Reports/IGMFormC.aspx"))
            {
                menuId = (int)PageName.IGMFormC;
            }
            else if (Request.Path.Contains("/Reports/FileLandingGurantee.aspx"))
            {
                menuId = (int)PageName.FileLandingGuranteeLetter;
            }
            else if (Request.Path.Contains("/Reports/ImportRegister.aspx"))
            {
                menuId = (int)PageName.ImportRegister;
            }
            else if (Request.Path.Contains("/Reports/CargoArrivalNotice.aspx"))
            {
                menuId = (int)PageName.CargoArrivalNotice;
            }
            else if (Request.Path.Contains("/Transaction/ImportBL.aspx") || Request.Path.Contains("/Transaction/ManageImportBL.aspx"))
            {
                menuId = (int)PageName.ImportBL;
            }

            //switch (Request.Path)
            //{
            //    // Master
            //    case "/View/ManageUser.aspx":
            //    case "/View/AddEditUser.aspx":
            //        menuId = (int)PageName.UserMaster;
            //        break;
            //    case "/View/ManageRole.aspx":
            //    case "/View/AddEditRole.aspx":
            //        menuId = (int)PageName.RoleMaster;
            //        break;
            //    case "/View/ManageLocation.aspx":
            //    case "/View/AddEditLocation.aspx":
            //        menuId = (int)PageName.LocationMaster;
            //        break;
            //    case "/View/ManageCustomer.aspx":
            //    case "/View/AddEditCustomer.aspx":
            //        menuId = (int)PageName.CustomerMaster;
            //        break;
            //    case "/MasterModule/ManageLine.aspx":
            //    case "/MasterModule/AddEditLine.aspx":
            //        menuId = (int)PageName.LineMSOMaster;
            //        break;
            //    case "/MasterModule/vendor-list.aspx":
            //    case "/MasterModule/vendor-add-edit.aspx":
            //        menuId = (int)PageName.AddressMaster;
            //        break;
            //    case "/View/ManageServTax.aspx":
            //    case "/View/AddEditSTax.aspx":
            //        menuId = (int)PageName.ServiceTaxMaster;
            //        break;
            //    case "/View/charge-list.aspx":
            //    case "/View/charge-add-edit.aspx":
            //        menuId = (int)PageName.ChargeMaster;
            //        break;
            //    //Container type
            //    case "/View/ManageExchRate.aspx":
            //    case "/View/AddEditExchRate.aspx":
            //        menuId = (int)PageName.ExchangeRateMaster;
            //        break;
            //    //Terminal
            //    case "/MasterModule/ManageCountry.aspx":
            //    case "/MasterModule/AddEditCountry.aspx":
            //        menuId = (int)PageName.CountryMaster;
            //        break;
            //    case "/MasterModule/ManagePort.aspx":
            //    case "/MasterModule/AddEditPort.aspx":
            //        menuId = (int)PageName.PortMaster;
            //        break;
            //    case "/MasterModule/ImportHaulage-list.aspx":
            //    case "/MasterModule/import-haulage-chrg-add-edit.aspx":
            //        menuId = (int)PageName.ImportHaulageChargeMaster;
            //        break;
            //    //Vessel master
            //}

            return menuId;
        }
    }
}