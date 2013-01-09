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
            PER_UNIT = 1,
            PER_DOCUMENT = 2,
            DETENTION = 3,
            PORT_GROUND_RENT = 4,
            SLAB = 5,
            LCL = 6
        }
        public enum Currency
        {
            INR = 1,
            DOLLAR = 2
            
        }

        public enum ImportExportGeneral
        {
            IMPORT = 1,
            EXPORT = 2,
            GENERAL = 3
        }

        public enum WashingType
        {
            GENERAL = 1,
            HEAVY = 2,
            CHEMICAL = 3
        }

        public enum Salutation
        {
            MR = 1,
            MS = 2,
            DR = 3,
            M_S = 4
        }

    }
}
