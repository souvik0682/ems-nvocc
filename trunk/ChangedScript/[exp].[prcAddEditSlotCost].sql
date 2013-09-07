USE [Liner]
GO

/****** Object:  StoredProcedure [exp].[prcAddEditSlotCost]    Script Date: 05/01/2013 18:18:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 
CREATE proc [exp].[prcAddEditSlotCost]    
@pk_SlotCostID int,      
@fk_SlotID int,
@CargoType char(1),
@fk_ContainerType int,
@CntrSize char(2),
@SpecialType char(1),
@RatePerTon Decimal(12,2),
@RatePerCBM Decimal(12,2),
@ContainerRate Decimal(12,2),
@SlotCostStatus bit

as    
  begin    
  
	if @pk_SlotCostID <= 0     
		begin    
		  Insert into exp.mstSlotCost
			(cargotype,
			CntrSize,
			fk_SlotID,
			fk_ContainerType,
			SpecialType,
			ContainerRate,
			RatePerTon,
			RatePerCBM,
			SlotCostStatus)
		  Values
		  (
			@cargoType,
			@cntrsize,
			@fk_SlotID,
			@fk_ContainerType,
			@SpecialType,
			@ContainerRate,
			@RatePerTon,
			@RatePerCBM,
			@SlotCostStatus
			)
		end    
	else    
		update exp.mstSlotCost set    
			cargotype		= @cargotype,
			cntrsize		= @cntrsize,
			fk_ContainerType= @fk_ContainerType,
			SpecialType		= @SpecialType,
			ContainerRate	= @ContainerRate,
			Rateperton		= @RatePerTon,
			RatePerCBM		= @RatePerCBM
		where pk_SlotCostID = @pk_SlotCostID
		
    
  end    

GO


