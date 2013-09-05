USE [Liner]
GO

/****** Object:  StoredProcedure [exp].[prcDeleteSlot]    Script Date: 05/01/2013 21:17:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- exec [exp].[prcDeleteSlotOperator] 1
CREATE proc [exp].[prcDeleteSlot]
 @pk_SlotID int
  as
  --delete from trnVoyage where pk_VoyageID=@pk_VoyageID
  update exp.mstSlot
  set SlotStatus=0
  where pk_SlotID=@pk_SlotID
  
  update exp.mstSlotCost set slotCostStatus=0 where fk_slotid=@pk_SlotID
  
GO


