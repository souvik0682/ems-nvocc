using System;
namespace EMS.Common
{
   public interface IEqpContainerTransaction
    {
        DateTime AddedOn { get; set; }
        int? AddressID { get; set; }
        int? BLFooterID { get; set; }
        int? BookingID { get; set; }
        int CompanyID { get; set; }
        long ContainerTranID { get; set; }
        DateTime? EditedOn { get; set; }
        int? HireContainerID { get; set; }
        int LocationID { get; set; }
        DateTime MovementDate { get; set; }
        int MovementOptID { get; set; }
        string Narration { get; set; }
        int NVOCCID { get; set; }
        int StockLocationID { get; set; }
        int? TotalFEU { get; set; }
        int? TotalTEU { get; set; }
        string TransactionCode { get; set; }
        int? TransferLocationID { get; set; }
        int UserAdded { get; set; }
        int? UserLastEdited { get; set; }
    }
}
