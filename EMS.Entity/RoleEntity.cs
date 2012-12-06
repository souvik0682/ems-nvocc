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

        public bool? LocationSpecific
        {
            get;
            set;
        }

        public bool? RoleStatus
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

            if (reader["LocationSpecific"] != DBNull.Value)
                this.LocationSpecific = Convert.ToBoolean(reader["LocationSpecific"]);

            if (reader["RoleStatus"] != DBNull.Value)
                this.RoleStatus = Convert.ToBoolean(reader["RoleStatus"]);
        }

        #endregion
    }
}