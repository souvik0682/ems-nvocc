USE [Liner]
GO

/****** Object:  StoredProcedure [exp].[prcGetSlotCostList]    Script Date: 05/01/2013 18:19:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- EXEC [exp].[prcGetSlotList] @SortExpression='', @SortDirection=''
-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 06-Dec-2012
-- Description	:
-- =============================================
CREATE PROCEDURE [exp].[prcGetSlotCostList]
(
	-- Add the parameters for the stored procedure here
	@SlotID	BIGINT 

)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
				
	SELECT	 sc.fk_SlotID
			,sc.pk_SlotCostID
			,sc.CargoType
			,sc.CntrSize
			,sc.ContainerRate
			,sc.RatePerCBM
			,sc.RatePerTon
			,sc.SpecialType
			,sc.fk_ContainerType
	FROM	exp.mstSlotCost sc
	WHERE	SC.fk_SlotID = @SlotID
	
END

GO


