USE [Liner]
GO

/****** Object:  StoredProcedure [exp].[prcGetBLPrinting]    Script Date: 09/16/2013 22:16:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [exp].[prcGetBLPrinting]   
 @BLID  varchar(50)  
AS  
BEGIN  
 SELECT   
  bh.fk_LocationID  
  ,[exp].[GetLocationName](bh.fk_LocationID,'N') LocationName  
 , bh.fk_NVOCCID  
 , bh.ExpBLNo  
 , bh.ExpBLDate  
 , bh.fk_BLIssuePort  
 , bh.ShipperName  
 , bh.Shipper  
 , bh.ConsigneeName  
 , bh.Consignee  
 , bh.NotifyName  
 , bh.Notify  
 , bh.PORDesc PlaceOfReceipt  
 , bh.POLDesc PlaceofLoading  
 , bh.PODDesc PlaceofDischarge  
 , bh.FinalDesc FinalDelivery  
 , bh.MarksNumbers  
 , bh.GoodsDescription  
 , bk.ppcc FreightPrePayToPay  
 , ag.AgentName  
 , Ag.AgentAddress  
 , exp.GetVesselName(bk.fk_VesselID) VesselName  
 , exp.GetVoyageNo(bk.fk_VoyageID) VoyageNo  
 , exp.GetPortName(bk.fk_FreightPayable,'N') FreightPayableAt  
 , case bh.BLClause when 'S' THEN 'SHIPPED ON BOARD' ELSE 'RECEIPT OF SHIPMENT' END BLClause  
 , case bk.ShipmentType when 'F' THEN 'FCL/FCL' WHEN 'L' THEN 'LCL/LCL' WHEN 'B' THEN 'BULK' END ShipmentType  
 , sm.MovTypeName ShipmentMode  
 , Case bh.NoofBLs   
 when 1 then 'ONE'  
 when 2 then 'TWO'  
 when 3 then 'THREE'  
 ELSE 'NO VALUE'  
 end NoofBLs  
 , (SELECT sum(grossWeight) GrWt FROM exp.BLContainers where fk_ExpBLID=bh.pk_ExpBLID) GRWT  
 , bh.NetWt  
 FROM exp.BLHeader bh  
INNER JOIN exp.Booking bk ON bh.fk_BookingID = bk.pk_BookingID
INNER JOIN exp.mstAgent Ag on bh.fk_AgentID = ag.pk_AgentID  
INNER JOIN exp.mstShipmentMode sm on sm.pk_MovTypeID=bh.fk_ShipmentMode  
where bh.ExpBLNo=@BLID  
  
SELECT   
CASE WHEN bf.cntrno IS NULL THEN oh.ContainerNo ELSE bf.CntrNo END ContainerNo  
, ebc.Size  
, ct.ContainerAbbr CntrType  
, ebc.SealNo  
, ebc.GrossWeight  
, ebc.Package  
FROM exp.BLContainers ebc  
join exp.BLHeader bh  on bh.pk_ExpBLID=ebc.fk_ExpBLID  
left outer join dbo.impblfooter bf on bf.pk_blfooterid=ebc.fk_ImpBLFooterID  
left outer join dbo.eqpOnHireContainers oh on oh.pk_HireContainerID = ebc.fk_HireContainerID  
inner join dbo.mstContainerType ct on ebc.fk_ContainerTypeID=ct.pk_ContainerTypeID  
where bh.ExpBLNo=@BLID  
  
END  
GO


