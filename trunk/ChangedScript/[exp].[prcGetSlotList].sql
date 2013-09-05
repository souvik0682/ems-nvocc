USE [Liner]
GO

/****** Object:  StoredProcedure [exp].[prcGetSlotList]    Script Date: 05/01/2013 21:17:22 ******/
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
CREATE PROCEDURE [exp].[prcGetSlotList]
(
	-- Add the parameters for the stored procedure here
	@CompanyID		INT = NULL,
	@SlotID			BIGINT =  NULL,
	@SchOperatorName VARCHAR(50) = NULL,
	@SchPOLName		VARCHAR(30) = NULL,
	@SchPODName		VARCHAR(30) = NULL,
	@SchLine		VARCHAR(30) = NULL,
	--@SchPODTerminal	VARCHAR(30) = NULL,
	@SortExpression	VARCHAR(30),
	@SortDirection  VARCHAR(4) 
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @Sort VARCHAR(4)
	
	SELECT @Sort = CASE @SortDirection
						WHEN '' THEN 'ASC'
						ELSE @SortDirection
					END
				
	SELECT	 slot.pk_SlotID
			,op.SlotOperatorName
			,exp.getLineName(slot.fk_LineID) LineName
			,exp.GetPortName(slot.fk_POL,'B') LoadPort
			,exp.GetPortName(slot.fk_POD,'B') DischargePort
			,slot.effDate
			,mo1.MovTypeName + mo2.movtypename Terms
			,slot.SlotStatus
	FROM	exp.mstSlot slot
			INNER JOIN exp.mstSlotOperator op  
				ON slot.fk_SlotOperatorID = op.pk_SlotOperatorID
			INNER JOIN exp.mstMovementType mo1 on slot.fk_MovDest=mo1.pk_MovTypeID
			INNER JOIN exp.mstMovementType mo2 on slot.fk_MovDest=mo2.pk_MovTypeID
	WHERE	((ISNULL(@SlotId, 0) = 0) OR (slot.pk_SlotID = @SlotID))
	AND		((ISNULL(@SchOperatorName, '') = '') OR (op.SlotOperatorName LIKE '%'+ @SchOperatorName + '%'))
	AND		((ISNULL(@SchLine, '') = '') OR (exp.getLineName(slot.fk_LineID) LIKE '%'+ @SchLine + '%'))
	AND		((ISNULL(@SchPODName, '') ='') OR (exp.GetPortName(slot.fk_POD,'N') LIKE '%'+@SchPODName + '%'))
	AND		((ISNULL(@SchPOLName, '') ='') OR (exp.GetPortName(slot.fk_POL,'N') LIKE '%'+@SchPOLName + '%'))
	AND		slot.SlotStatus = 1 AND slot.fk_CompanyID=@CompanyID
	ORDER BY 
			CASE @SortDirection
				WHEN 'ASC' THEN
					CASE @SortExpression
						WHEN 'SlotOperatorName' THEN SlotOperatorName
						WHEN 'LINE' THEN exp.getLineName(slot.fk_LineID)
						WHEN 'POD' THEN exp.GetPortName(slot.fk_POD,'N')
						WHEN 'POL' THEN exp.GetPortName(slot.fk_POL,'N')
						ELSE '1'
					END 
			END ASC,
			CASE @SortDirection
				WHEN 'DESC' THEN
					CASE @SortExpression
						WHEN 'SlotOperatorName' THEN SlotOperatorName
						WHEN 'LINE' THEN exp.getLineName(slot.fk_LineID)
						WHEN 'POD' THEN exp.GetPortName(slot.fk_POD,'N')
						WHEN 'POL' THEN exp.GetPortName(slot.fk_POL,'N')
						ELSE '1'
					END 
			END DESC	
END

GO


