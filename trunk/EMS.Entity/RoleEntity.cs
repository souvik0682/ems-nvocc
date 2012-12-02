using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EMS.Common;

namespace EMS.Entity
{
    public class RoleEntity : IRole
    {
        #region IRole Members

        public string RoleType
        {
            get;
            set;
        }

        #endregion

        #region IBase<int> Members

        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public RoleEntity()
        {

        }

        public RoleEntity(DataTableReader reader)
        {
            this.Id = Convert.ToInt32(reader["RoleId"]);
            this.Name = Convert.ToString(reader["RoleName"]);

            if (reader["RoleType"] != DBNull.Value)
                this.RoleType = Convert.ToString(reader["RoleType"]);
        }

        #endregion
    }
}