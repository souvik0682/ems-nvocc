using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;

namespace EMS.Entity
{
    public class ServiceEntity : IService
    {
           public int ServiceID
        {
            get;
            set;
        }

        public bool Action
        {
            get;
            set;
        }

        public string NVOCC
        {
            get;
            set;
        }

        public string FPOD
        {
            get;
            set;
        }

        public int LinerID
        {
            get;
            set;
        }
        public string FPODID
        {
            get;
            set;
        }

        public string ServiceName
        {
            get;
            set;
        }

        public bool ServiceStatus
        {
            get;
            set;
        }

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

        public ServiceEntity()
        {

        }


        public ServiceEntity(DataTableReader reader)
        {
            this.ServiceID = Convert.ToInt32(reader["pk_ServiceID"]);
            this.ServiceName = Convert.ToString(reader["ServiceName"]);
            this.LinerID = Convert.ToInt32(reader["fk_LIneID"]);
            this.ServiceStatus = Convert.ToBoolean(reader["ServiceStatus"]);
            this.NVOCC = Convert.ToString(reader["NVOCC"]);
            this.FPOD = Convert.ToString(reader["PortName"]);
            this.FPODID = Convert.ToString(reader["FK_FPOD"]);
        }
    }
}
