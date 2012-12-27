using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Utilities
{
    public static class Enums
    {
        public enum DropDownPopulationFor
        {
            VendorType = 1,
            Location = 2,
            TerminalCode = 3,
            Port = 4,
            ContainerSize = 5,
            Line = 6,
            ContainerMovementStatus = 7,
            VendorList = 8
        }


        /// <summary>
        /// Use "_" as a seperator for item having more than one word
        /// </summary>
        public enum ChargeType
        {
            Per_Unit = 1,
            Per_Document = 2,
            Detention = 3,
            Port_Ground = 4,
            Slab = 5

        }
        public enum Currency
        {
            DOLLAR = 1,
            INR = 2
        }

        public enum ImportExportGeneral
        {
            Import = 1,
            Export = 2,
            General = 3
        }

        public enum WashingType
        {
            General = 1,
            Heavy = 2,
            Chemical = 3
        }

    }
}
