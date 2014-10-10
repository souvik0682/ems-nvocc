using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;

namespace EMS.Entity
{
     public class fwLineEntity : IFwLine
    {
        public int LineID { get; set; }

        public string LineName { get; set; }

        public bool LineActive { get; set; }

        public string LineType { get; set; }

        public string Prefix { get; set; }

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

        public fwLineEntity()
        {
        }

        public fwLineEntity(DataTableReader reader)
        {

            this.LineID = Convert.ToInt32(reader["pk_FLineID"]);
            this.LineName = Convert.ToString(reader["LineName"]);
            this.LineType = Convert.ToString(reader["LineType"]);
            this.Prefix = Convert.ToString(reader["Prefix"]);
        }
    }
}
