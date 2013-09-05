USE [Liner]
GO

/****** Object:  StoredProcedure [exp].[prcGetSlotModel]    Script Date: 05/01/2013 21:17:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [exp].[prcGetSlotModel]
	-- Add the parameters for the stored procedure here
	@SlotID	BIGINT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   
	SELECT	 [pk_SlotID] SLOTID
      ,[fk_CompanyID]
      ,[fk_SlotOperatorID] OPERATOR
      ,[fk_LineID] LINE
      ,[fk_POL] PORTLOADING
      ,[fk_POD] PORTDISCHARGE
      ,[fk_MovOrigin] MOVORIGIN
      ,[fk_MovDest] MOVDESTINATION
      ,[PODTerminal] PODTERMINAL
      ,[EffDate] EFFECTIVEDATE
      
	FROM	exp.mstSlot s
	WHERE	pk_SlotID = @SlotID 
	
	
	SELECT	 [pk_SlotCostID] SLOTCOSTID
      ,[CargoType] CARGO
      ,[CntrSize] SIZE
      ,[fk_ContainerType] CONTAINERTYPE
      ,[ContainerRate]  AMOUNT
      ,[RatePerTon] REVTON
      ,SpecialType TYPE
	FROM	exp.mstSlotCost sc
	WHERE	SC.fk_SlotID = @SlotID and slotCostStatus=1
END

GO


