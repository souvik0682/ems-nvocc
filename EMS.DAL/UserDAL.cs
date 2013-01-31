using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL.DbManager;
using EMS.Entity;

namespace EMS.DAL
{
    public sealed class UserDAL
    {
        private UserDAL()
        {
        }

        #region User

        public static bool ChangePassword(IUser user)
        {
            string strExecution = "[admin].[uspChangePassword]";
            bool result = false;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@UserId", user.Id);
                oDq.AddVarcharParam("@OldPwd", 50, user.Password);
                oDq.AddVarcharParam("@NewPwd", 50, user.NewPassword);
                oDq.AddBooleanParam("@Result", result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                result = Convert.ToBoolean(oDq.GetParaValue("@Result"));
            }

            return result;
        }

        public static void ValidateUser(IUser user)
        {
            string strExecution = "[admin].[uspValidateUser]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@UserName", 10, user.Name);
                oDq.AddVarcharParam("@Password", 50, user.Password);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    user.Id = Convert.ToInt32(reader["UserId"]);
                    user.FirstName = Convert.ToString(reader["FirstName"]);
                    user.LastName = Convert.ToString(reader["LastName"]);
                    user.UserRole.Id = Convert.ToInt32(reader["RoleId"]);
                    user.UserLocation.Id = Convert.ToInt32(reader["LocId"]);
                    user.EmailId = Convert.ToString(reader["emailID"]);
                    user.AllowMutipleLocation = Convert.ToBoolean(reader["AllowMutipleLocation"]);
                }

                reader.Close();
            }
        }

        public static List<IUser> GetUserList(bool isActiveOnly, SearchCriteria searchCriteria)
        {
            string strExecution = "[admin].[uspGetUser]";
            List<IUser> lstUser = new List<IUser>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBooleanParam("@IsActiveOnly", isActiveOnly);
                oDq.AddVarcharParam("@SchUserName", 10, searchCriteria.UserName);
                oDq.AddVarcharParam("@SchFirstName", 30, searchCriteria.FirstName);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IUser user = new UserEntity(reader);
                    lstUser.Add(user);
                }

                reader.Close();
            }

            return lstUser;
        }

        public static IUser GetUser(int userId, bool isActiveOnly, SearchCriteria searchCriteria)
        {
            string strExecution = "[admin].[uspGetUser]";
            IUser user = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@UserId", userId);
                oDq.AddBooleanParam("@IsActiveOnly", isActiveOnly);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    user = new UserEntity(reader);
                }

                reader.Close();
            }

            return user;
        }

        public static int SaveUser(IUser user, int companyId, int modifiedBy)
        {
            string strExecution = "[admin].[uspSaveUser]";
            int result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@UserId", user.Id);
                oDq.AddVarcharParam("@UserName", 10, user.Name);
                oDq.AddVarcharParam("@Pwd", 50, user.Password);
                oDq.AddVarcharParam("@FirstName", 30, user.FirstName);
                oDq.AddVarcharParam("@LastName", 30, user.LastName);
                oDq.AddIntegerParam("@RoleId", user.UserRole.Id);
                oDq.AddIntegerParam("@LocId", user.UserLocation.Id);
                oDq.AddVarcharParam("@EmailId", 50, user.EmailId);
                oDq.AddBooleanParam("@IsActive", user.IsActive);
                oDq.AddBooleanParam("@AllowMutipleLocation", user.AllowMutipleLocation);
                oDq.AddIntegerParam("@CompanyId", companyId);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.AddIntegerParam("@Result", result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                result = Convert.ToInt32(oDq.GetParaValue("@Result"));
            }

            return result;
        }

        public static void DeleteUser(int userId, int modifiedBy)
        {
            string strExecution = "[admin].[uspDeleteUser]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@UserId", userId);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.RunActionQuery();
            }
        }

        public static void ResetPassword(IUser user, int modifiedBy)
        {
            string strExecution = "[admin].[uspResetPassword]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@UserId", user.Id);
                oDq.AddVarcharParam("@Pwd", 50, user.Password);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.RunActionQuery();

            }
        }

        public static DataSet GetAdminUsers(int LoggedUserId, int locationId)
        {
            DataSet ds = new DataSet();
            using (DbQuery dq = new DbQuery("prcGetAdminUsers"))
            {
                dq.AddIntegerParam("@LoggedUserId", LoggedUserId);
                dq.AddIntegerParam("@locationId", locationId);
                ds = dq.GetTables();
            }
            return ds;
        }

        #endregion

        #region Role

        public static List<IRole> GetRole(bool isActiveOnly, SearchCriteria searchCriteria)
        {
            string strExecution = "[admin].[uspGetRole]";
            List<IRole> lstRole = new List<IRole>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBooleanParam("@IsActiveOnly", isActiveOnly);
                oDq.AddVarcharParam("@SchRole", 50, searchCriteria.RoleName);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IRole role = new RoleEntity(reader);
                    lstRole.Add(role);
                }

                reader.Close();
            }

            return lstRole;
        }

        public static IRole GetRole(int roleId, bool isActiveOnly, SearchCriteria searchCriteria)
        {
            string strExecution = "[admin].[uspGetRole]";
            IRole role = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@RoleId", roleId);
                oDq.AddBooleanParam("@IsActiveOnly", isActiveOnly);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    role = new RoleEntity(reader);
                }

                reader.Close();
            }

            return role;
        }

        public static int SaveRole(IRole role, int companyID, string xmlDoc, int modifiedBy)
        {
            string strExecution = "[admin].[uspSaveRole]";
            int result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@RoleID", role.Id);
                oDq.AddVarcharParam("@RoleName", 50, role.Name);
                oDq.AddIntegerParam("@CompanyId", companyID);
                oDq.AddNVarcharParam("@XMLDoc", xmlDoc);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.AddIntegerParam("@Result", result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                result = Convert.ToInt32(oDq.GetParaValue("@Result"));
            }

            return result;
        }

        public static void DeleteRole(int roleId, int modifiedBy)
        {
            string strExecution = "[admin].[uspDeleteRole]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@RoleId", roleId);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.RunActionQuery();
            }
        }

        public static void ChangeRoleStatus(int roleId, bool status, int modifiedBy)
        {
            string strExecution = "[admin].[uspChangeRoleStatus]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@RoleId", roleId);
                oDq.AddBooleanParam("@RoleStatus", status);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.RunActionQuery();
            }
        }

        public static List<IRoleMenu> GetMenuByRole(int roleId, int mainId)
        {
            string strExecution = "[admin].[uspGetMenuByRole]";
            List<IRoleMenu> lstMenu = new List<IRoleMenu>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@RoleId", roleId);
                oDq.AddIntegerParam("@MainId", mainId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IRoleMenu menu = new RoleMenuEntity(reader);
                    lstMenu.Add(menu);
                }

                reader.Close();
            }

            return lstMenu;
        }

        public static IRoleMenu GetMenuAccessByUser(int userId, int menuId)
        {
            string strExecution = "[admin].[uspGetMenuAccessByUser]";
            IRoleMenu roleMenu = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@UserId", userId);
                oDq.AddIntegerParam("@MenuId", menuId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    roleMenu = new RoleMenuEntity(reader);
                }

                reader.Close();
            }

            return roleMenu;
        }

        #endregion
    }
}
