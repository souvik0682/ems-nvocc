using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;

namespace DSR.Entity
{
    public class CustomerEntity : ICustomer
    {
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

        #region ICommon Members

        public int CreatedBy
        {
            get;
            set;
        }

        public DateTime CreatedOn
        {
            get;
            set;
        }

        public int ModifiedBy
        {
            get;
            set;
        }

        public DateTime ModifiedOn
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public CustomerEntity()
        {

        }

        public CustomerEntity(DataTableReader reader)
        {
 
        }

        #endregion

        #region Public Functions

         #endregion
    }
}
