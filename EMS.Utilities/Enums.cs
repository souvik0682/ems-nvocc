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
            VendorList = 8,
            InvoiceLink = 9,
            Booking = 101,
            Vessel = 102
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
            INLAND_HAULAGE = 5,
            LCL = 6,
            SLAB_UNIT = 7,
            SLAB_DOCS = 8,
            //REPAIRING_MATERIAL = 9,
            REPAIRING =10

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
            FTC = 1,
            DO = 2,
            DOE = 3,
            DE = 4,
            PGRE = 5,
            SI = 6,
            FI = 7,
            OI = 8,
            AM = 9,
            BC = 10
        }

        public enum UploadedDoc
        {
            Freight_Prepaid = 1,
            CFS_Nomination = 2,
            Detention_Waiver = 3,
            BL_Surrender = 4,
            BL_Extension = 5
        }

        public enum AmendmentFor
        {
            Line_Number = 1,
            BL_Number_and_Date = 2,
            Marks_and_Numbers = 3,
            Package = 4,
            Weight = 5,
            Consignee = 6,
            Notify_Party = 7,
            Goods_Description = 8,
            Container_Details = 9

        }

        // "__" will be replace with "-" & "_" will be replace with space
        public enum DeliveryMode
        {
            None = 0,
            Factory_de__stuffing =1,
            CFS_de__stuffing = 2
        }
    }
}
