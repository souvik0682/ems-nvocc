using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.DAL;
using EMS.Common;
using EMS.Entity;
using EMS.Utilities;
using System.Web;
using EMS.Utilities.ResourceManager;
using EMS.Utilities.Cryptography;
using System.Data;

namespace EMS.BLL
{
    public class MoneyReceiptsBLL
    {
        #region User

        public static string GetDefaultPassword()
        {
            return Encryption.Encrypt(Constants.DEFAULT_PASSWORD);
        }

        public bool ValidateUser(IUser user)
        {
            UserDAL.ValidateUser(user);
            return (user.Id > 0) ? true : false;
        }

        public static int GetLoggedInUserId()
        {
            int userId = 0;

            if (!ReferenceEquals(System.Web.HttpContext.Current.Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)System.Web.HttpContext.Current.Session[Constants.SESSION_USER_INFO];

                if (!ReferenceEquals(user, null))
                {
                    userId = user.Id;
                }
            }

            return userId;
        }

        public static int GetLoggedInUserRoleId()
        {
            int roleId = 0;

            if (!ReferenceEquals(System.Web.HttpContext.Current.Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)System.Web.HttpContext.Current.Session[Constants.SESSION_USER_INFO];

                if (!ReferenceEquals(user, null) && user.Id > 0)
                {
                    if (!ReferenceEquals(user.UserRole, null))
                    {
                        roleId = user.UserRole.Id;
                    }
                }
            }

            return roleId;
        }

        private void SetDefaultSearchCriteriaForUser(SearchCriteria searchCriteria)
        {
            searchCriteria.SortExpression = "UserName";
            searchCriteria.SortDirection = "ASC";
        }

        public List<MoneyReceiptEntity> GetMoneyReceipts(SearchCriteria searchCriteria)
        {
            return MoneyReceiptDAL.GetMoneyReceipts(searchCriteria);
        }

        public BLInformations GetBLInformation(string blNo)
        {
            return MoneyReceiptDAL.GetBLInformation(blNo);
        }

        public List<InvoiceTypeEntity> GetInvoiceTypes()
        {
            return MoneyReceiptDAL.GetInvoiceTypes();
        }

        public List<InvoiceTypeEntity> GetExpInvoiceTypes()
        {
            return MoneyReceiptDAL.GetExpInvoiceTypes();
        }

        public List<InvoiceDetailsEntity> GetInvoiceDetails(int invoiceTypeId)
        {
            return MoneyReceiptDAL.GetInvoiceDetails(invoiceTypeId);
        }

        //public int SaveMoneyReceipts(List<MoneyReceiptEntity> moneyReceipts)
        //{
        //    return MoneyReceiptDAL.SaveMoneyReceipts(moneyReceipts);
        
        //}

        public int SaveMoneyReceipt(MoneyReceiptEntity moneyReceipt)
        {
            return MoneyReceiptDAL.SaveMoneyReceipt(moneyReceipt);

        }

        public void DeleteMoneyReceipts(string MRNo)
        {
            MoneyReceiptDAL.DeleteMoneyReceipts(MRNo);

        }
        #endregion

        #region Role

        private void SetDefaultSearchCriteriaForRole(SearchCriteria searchCriteria)
        {
            searchCriteria.SortExpression = "Role";
            searchCriteria.SortDirection = "ASC";
        }

        public List<IRole> GetAllRole(SearchCriteria searchCriteria)
        {
            return UserDAL.GetRole(false, searchCriteria);
        }

        public List<IRole> GetActiveRole()
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForRole(searchCriteria);
            return UserDAL.GetRole(true, searchCriteria);
        }

        public IRole GetRole(int roleId)
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForRole(searchCriteria);
            return UserDAL.GetRole(roleId, false, searchCriteria);
        }

        public string SaveRole(List<RoleMenuEntity> lstRoleMenu, IRole role, int modifiedBy)
        {
            int result = 0;
            string errMessage = string.Empty;
            string xmlDoc = GeneralFunctions.Serialize(lstRoleMenu);
            result = UserDAL.SaveRole(role, Constants.DEFAULT_COMPANY_ID, xmlDoc, modifiedBy);

            switch (result)
            {
                case 1:
                    errMessage = ResourceManager.GetStringWithoutName("ERR00072");
                    break;
                default:
                    break;
            }

            return errMessage;
        }

        public void DeleteRole(int roleId, int modifiedBy)
        {
            UserDAL.DeleteRole(roleId, modifiedBy);
        }

        public void ChangeRoleStatus(int roleId, bool status, int modifiedBy)
        {
            UserDAL.ChangeRoleStatus(roleId, status, modifiedBy);
        }

        public List<IRoleMenu> GetMenuByRole(int roleId, int mainId)
        {
            return UserDAL.GetMenuByRole(roleId, mainId);
        }

        //public static void GetMenuAccessByUser(int userId, int menuId, out bool canAdd, out bool canEdit, out bool canDelete, out bool canView)
        //{
        //    canAdd = false;
        //    canEdit = false;
        //    canDelete = false;
        //    canView = false;

        //    IRoleMenu roleMenuAccess = UserDAL.GetMenuAccessByUser(userId, menuId);

        //    if (!ReferenceEquals(roleMenuAccess, null))
        //    {
        //        canAdd = roleMenuAccess.CanAdd;
        //        canEdit = roleMenuAccess.CanEdit;
        //        canDelete = roleMenuAccess.CanDelete;
        //        canView = roleMenuAccess.CanView;
        //    }
        //}

        public static IUserPermission GetMenuAccessByUser(int userId, int menuId)
        {
            IUserPermission userPermission = new UserPermission();

            IRoleMenu roleMenuAccess = UserDAL.GetMenuAccessByUser(userId, menuId);

            if (!ReferenceEquals(roleMenuAccess, null))
            {
                userPermission.CanAdd = roleMenuAccess.CanAdd;
                userPermission.CanEdit = roleMenuAccess.CanEdit;
                userPermission.CanDelete = roleMenuAccess.CanDelete;
                userPermission.CanView = roleMenuAccess.CanView;
            }

            return userPermission;
        }

        public static void GetUserPermission(out bool canAdd, out bool canEdit, out bool canDelete, out bool canView)
        {
            canAdd = false;
            canEdit = false;
            canDelete = false;
            canView = false;

            if (!ReferenceEquals(System.Web.HttpContext.Current.Session[Constants.SESSION_USER_PERMISSION], null))
            {
                IUserPermission userPermission = (IUserPermission)System.Web.HttpContext.Current.Session[Constants.SESSION_USER_PERMISSION];

                if (!ReferenceEquals(userPermission, null))
                {
                    canAdd = userPermission.CanAdd;
                    canEdit = userPermission.CanEdit;
                    canDelete = userPermission.CanDelete;
                    canView = userPermission.CanView;
                }
            }
        }

        #endregion


        public DataTable GetInvoiceDetailForMoneyReceipt(Int64 InvoiceId)
        {
            return MoneyReceiptDAL.GetInvoiceDetailForMoneyReceipt(InvoiceId);
        }
    }
}
