﻿using System;
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
            VendorList = 8,

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

        public enum ContainerMovement
        {
            ONBR = 1,
            DCHF = 2,
            CFSI = 3,
            CFSD = 4,
            SNTC = 5,
            DCHE = 6,
            ONH = 7,
            TRFI = 8,
            RCVE = 9,
            URPR = 10,
            SNTS = 11,
            RCVF = 12,
            LODF = 13,
            LODE = 14,
            RCEE = 15,
            TRFE = 16,
            OFFH = 17
        }

        public enum BLQ_Doc
        {
            FreightPrepaid = 1,
            CFS_Nomination = 2,
            Detention_Waiver = 3,
            BL_Surrender = 4,
            BL_Extension = 5
        }

        public enum BLActivity
        {
            DO = 1,
            DOE = 2,
            SE = 3,
            AMD = 4,
            BC = 5
        }
    }
}
