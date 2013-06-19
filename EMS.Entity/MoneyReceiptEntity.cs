using System;
using System.Data;
using System.Collections.Generic;

namespace EMS.Entity
{
    public class MoneyReceiptEntity
    {
        #region Identifiers
        private long moneyReceiptId;
        private long invoiceId;
        private string invoiceNo;
        private int invoiceTypeId;
        private DateTime? invoiceDate;
        private decimal invoiceAmount;
        private string invoiceTypeName;

        public string InvoiceTypeName
        {
            get { return invoiceTypeName; }
            set { invoiceTypeName = value; }
        }
        private long bLId;
        private decimal totReceipt;
        private string bLNo;
        private int locationId;
        private string locationName;
        private string locationAddress;
        private string rstoWord;
        private int nvoccId;
        private string nvoccName;
        private string mrNo;
        private DateTime? mrDate;
        private decimal cashPayment;
        private decimal chequePayment;
        private decimal tdsDeducted;
        private string chequeBank;
        private DateTime? chequeDate;
        private string chequeNo;
        private decimal mrAmount;
        private int userAddedId;
        private int userEditedId;
        private DateTime? userAddedOn;
        private DateTime? userEditedOn;
        private int isAdded;
        private int isEdited;
        private int isDeleted;
        private string chequeDetails;
        private string voyageNo;
        private string vesselName;
        private string rcvdFrom;
        private char exportImport;
        private bool status;
        private string cha;
     
        public string ChequeDetails
        {
            get { return chequeDetails; }
            set { chequeDetails = value; }
        }
        public char ExportImport
        {
            get { return exportImport; }
            set { exportImport = value; }
        }

        public bool Status
        {
            get { return status; }
            set { status = value; }
        }
        #endregion

        #region Constructor

        public MoneyReceiptEntity()
        {

        }

        public MoneyReceiptEntity(DataTableReader reader)
        {

            this.userAddedOn = Convert.ToDateTime(reader["AddedOn"]);
            this.userEditedOn = Convert.ToDateTime(reader["EditedOn"]);
            this.moneyReceiptId = long.Parse(reader["MRId"].ToString());
            this.invoiceId = long.Parse(reader["InvoiceId"].ToString());    
            this.invoiceNo = reader["InvoiceNo"].ToString();
            this.invoiceTypeId = Convert.ToInt32(reader["InvoiceType"]);
            this.invoiceDate = Convert.ToDateTime(reader["InvoiceDate"]);
            this.invoiceAmount = Convert.ToDecimal(reader["InvoiceAmount"]);
            this.bLId = long.Parse(reader["BLId"].ToString());
            this.bLNo = reader["BLNumber"].ToString();
            this.locationId = Convert.ToInt32(reader["LocationId"]);
            this.locationName = reader["MRLocation"].ToString();
            this.locationAddress = reader["LocationAddress"].ToString();
            this.totReceipt = Convert.ToDecimal(reader["TotReceipt"]);
            this.rstoWord = reader["RsInWord"].ToString();
            this.nvoccId = Convert.ToInt32(reader["NVOCCId"]);
            this.nvoccName = reader["NVOCCName"].ToString();
            this.mrNo = reader["MRNo"].ToString();
            this.mrDate = Convert.ToDateTime(reader["MRDate"]);
            this.cashPayment = Convert.ToDecimal(reader["CashPayment"]);
            this.chequePayment = Convert.ToDecimal(reader["ChequePayment"]);
            this.tdsDeducted = Convert.ToDecimal(reader["TDSDeducted"]);
            this.chequeBank = reader["ChequeBank"].ToString();
            if (!reader.IsDBNull(reader.GetOrdinal("ChequeDate")))
                this.chequeDate = Convert.ToDateTime(reader["ChequeDate"]);
            this.chequeNo = reader["ChequeNo"].ToString();
            this.MRAmount = Convert.ToDecimal(reader["MRAmount"]);
            this.userAddedId = Convert.ToInt32(reader["UserAdded"]);
            this.userEditedId = Convert.ToInt32(reader["UserLastEdited"]);
            this.userAddedOn = Convert.ToDateTime(reader["AddedOn"]);
            this.userEditedOn = Convert.ToDateTime(reader["EditedOn"]);
            this.voyageNo = reader["VoyageNo"].ToString();
            this.vesselName = reader["VesselName"].ToString();
            this.rcvdFrom = reader["RcvdFrom"].ToString();
            this.cha = reader["cha"].ToString();
            //this.totReceipt = Convert.ToDecimal(reader["TotReceipt"]);
           
        }

        #endregion

        #region Private Properties

        #endregion

        #region Public Properties
        public long MoneyReceiptId
        {
            get { return moneyReceiptId; }
            set { moneyReceiptId = value; }
        }

        public long InvoiceId
        {
            get { return invoiceId; }
            set { invoiceId = value; }
        }

        public string InvoiceNo
        {
            get { return invoiceNo; }
            set { invoiceNo = value; }
        }

        public int InvoiceTypeId
        {
            get { return invoiceTypeId; }
            set { invoiceTypeId = value; }
        }

        public DateTime? InvoiceDate
        {
            get { return invoiceDate; }
            set { invoiceDate = value; }
        }

        public decimal InvoiceAmount
        {
            get { return invoiceAmount; }
            set { invoiceAmount = value; }
        }

        public long BLId
        {
            get { return bLId; }
            set { bLId = value; }
        }

        public string BLNo
        {
            get { return bLNo; }
            set { bLNo = value; }
        }

        public int LocationId
        {
            get { return locationId; }
            set { locationId = value; }
        }

        public string LocationName
        {
            get { return locationName; }
        }

        public string LocationAddress
        {
            get { return locationAddress; }
        }

        public decimal TotReceipt
        {
            get { return totReceipt; }
        }

        public string RsToWord
        {
            get { return rstoWord; }
        }

        public int NvoccId
        {
            get { return nvoccId; }
            set { nvoccId = value; }
        }

        public string NvoccName
        {
            get { return nvoccName; }
        }

        public string MRNo
        {
            get { return mrNo; }
            set { mrNo = value; }
        }

        public DateTime? MRDate
        {
            get { return mrDate; }
            set { mrDate = value; }
        }

        public decimal CashPayment
        {
            get { return cashPayment; }
            set { cashPayment = value; }
        }

        public string ChequeBank
        {
            get { return chequeBank; }
            set { chequeBank = value; }
        }

        public decimal ChequePayment
        {
            get { return chequePayment; }
            set { chequePayment = value; }
        }

        public decimal TdsDeducted
        {
            get { return tdsDeducted; }
            set { tdsDeducted = value; }
        }

        public DateTime? ChequeDate
        {
            get { return chequeDate; }
            set { chequeDate = value; }
        }

        public string ChequeNo
        {
            get { return chequeNo; }
            set { chequeNo = value; }
        }

        public decimal MRAmount
        {
            get { return mrAmount; }
            set { mrAmount = value; }
        }

        public string VoyageNo
        {
            get { return voyageNo; }
            set { voyageNo = value; }
        }

        public string CHA
        {
            get { return cha; }
            set { cha = value; }
        }

        public string VesselName
        {
            get { return vesselName; }
            set { vesselName = value; }
        }

        public string RcvdFrom
        {
            get { return rcvdFrom; }
            set { rcvdFrom = value; }
        }

        public int UserAddedId
        {
            get { return userAddedId; }
            set { userAddedId = value; }
        }

        public int UserEditedId
        {
            get { return userEditedId; }
            set { userEditedId = value; }
        }

        public DateTime? UserAddedOn
        {
            get { return userAddedOn; }
            set { userAddedOn = value; }
        }

        public DateTime? UserEditedOn
        {
            get { return userEditedOn; }
            set { userEditedOn = value; }
        }

        public int IsAdded
        {
            get { return isAdded; }
            set { isAdded = value; }
        }

        public int IsEdited
        {
            get { return isEdited; }
            set { isEdited = value; }
        }

        public int IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }

        public List<ChargeDetails> ChargeDetails { get; set; }
        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        #endregion




    }

    public class ChargeDetails
    {
        public int ID { get; set; }
        public string MRNo { get; set; }
        public string BLNo { get; set; }
        public long InvoiceId { get; set; }
        public int InvoiceTypeId { get; set; }
        public string InvoiceTypeName { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal ReceivedAmount { get; set; }
        public decimal CurrentReceivedAmount { get; set; }
        public decimal TDS { get; set; }
        public decimal CashAmount { get; set; }
        public decimal ChequeAmount { get; set; }
        public decimal TotReceipt { get; set; }
        public string RsToWord { get; set; }
        public string ChequeDetails { get; set; }
        //public string CHA { get; set; }
    }

    public class InvoiceTypeEntity
    {
        public int InvoiceTypeId { get; set; }
        public string InvoiceTypeName { get; set; }
        public string InvoiceTypeHeader { get; set; }
        public string DocumentType { get; set; }
        public bool IsInvoiceType { get; set; }
    }

    public class InvoiceDetailsEntity
    {
        public int InvoiceTypeId { get; set; }
        public long InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public decimal InvoiceAmount { get; set; }

    }

    public class BLInformations
    {
        public long BLId { get; set; }
        public string BLNo { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string LocationAddress { get; set; }

        public int NVOCCId { get; set; }
        public string NVOCCName { get; set; }
        public int ExportImport { get; set; }

        public BLInformations(DataTableReader reader)
        {
            this.BLId = long.Parse(reader["BLId"].ToString());
            this.BLNo = reader["BLNumber"].ToString();
            this.LocationId = Convert.ToInt32(reader["LocationId"]);
            this.LocationName = reader["MRLocation"].ToString();
            this.LocationAddress = reader["LocationAddress"].ToString();
            this.NVOCCId = Convert.ToInt32(reader["NVOCCId"]);
            this.NVOCCName = reader["NVOCCName"].ToString();
            this.ExportImport = Convert.ToInt32(reader["ExportImport"]);
        }

    }
}
