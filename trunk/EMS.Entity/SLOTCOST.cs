using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Entity
{
    public class Slot
    {
        public long SLOTID { get; set; }        
        public int OPERATOR { get; set; }
        public long PORTLOADING { get; set; }
        public int MOVORIGIN { get; set; }
        public long PORTDISCHARGE { get; set; }
        public string PODTERMINAL { get; set; }
        public DateTime? EFFECTIVEDATE { get; set; }
        public int MOVDESTINATION { get; set; }
        public long LINE { get; set; }

    }
    public class SlotCost
    {
        public long SLOTCOSTID { get; set; }
        public char TYPE { get; set; }
        public string SIZE { get; set; }
        public int CONTAINERTYPE { get; set; }
        public char CARGO { get; set; }
        public decimal AMOUNT { get; set; }
        public decimal REVTON { get; set; }
    }

    public class SlotCostModel
    {
        public Slot Slot { get; set; }
        public IList<SlotCost> SlotCostList { get; set; }
        public int UserId { get; set; }
        public int CompanyID { get; set; }
    }
}
