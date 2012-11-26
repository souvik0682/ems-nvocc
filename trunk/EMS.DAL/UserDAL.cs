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
                    user.EmailId = Convert.ToString(reader["emailID"]);
                }

                reader.Close();
            }
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
    }
}
