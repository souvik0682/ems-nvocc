using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EMS.Entity.Report
{
    public class DOPrintEntity
    {
        #region Public Properties

        public string AgentCode { get; set; }
        public string LineCode { get; set; }
        public string LoadPort { get; set; }
        public string PODForm1 { get; set; }
        public string FinalPort { get; set; }
        public string Cargo { get; set; }
        public string HazClass { get; set; }
        public string Vessel { get; set; }
        public string ETATerminal { get; set; }
        public string ETDTerminal { get; set; }
        public DateTime VesselCutoffDate { get; set; }
        public DateTime SICutoffDate { get; set; }
        public DateTime DocumentCutoffDate { get; set; }
        public string Dimensions { get; set; }
        public string VIANo { get; set; }
        public string ROTNo { get; set; }
        public int CntrNos { get; set; }
        public int CntrSize { get; set; }
        public string CntrType { get; set; }

        #endregion

        #region Constructors

        public DOPrintEntity()
        {
            
        }

        public DOPrintEntity(DataTableReader reader)
        {

        }

        #endregion
    }
}
