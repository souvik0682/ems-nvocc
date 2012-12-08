using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EMS.Common;

namespace EMS.Entity
{
    public class MenuEntity : IMenu
    {
        #region IMenu Members

        public int MainID
        {
            get;
            set;
        }

        public int SubID
        {
            get;
            set;
        }

        public int SubSubID
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

        #endregion
    }
}
