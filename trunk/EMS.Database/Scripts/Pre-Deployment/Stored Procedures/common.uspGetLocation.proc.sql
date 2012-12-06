/****** Object:  StoredProcedure [common].[uspGetLocation]    Script Date: 12/06/2012 23:36:34 ******/

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 03-Dec-2012 
-- Description  :
-- =============================================
CREATE PROCEDURE [common].[uspGetLocation]
(
	-- Add the parameters for the stored procedure here
	@LocId				INT = NULL,
	@IsActiveOnly		CHAR(1),
	@SchAbbr			VARCHAR(3) = NULL,
	@SchLocName			VARCHAR(50) = NULL,
	@SortExpression		VARCHAR(50),
	@SortDirection		VARCHAR(4)	
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
					
	SELECT	 loc.pk_LocID AS 'Id'
			,loc.LocName AS 'Name'
			,loc.LocAddress AS 'Address'
			,loc.LocCity AS 'City'
			,loc.LocPin AS 'Pin'
			,loc.LocAbbr
			,loc.LocPhone
			,loc.PGRFreeDays
			,loc.CANFooter
			,loc.CartingFooter
			,loc.SlotFooter
			,loc.PickUpFooter
			,loc.CustomHouseCode
			,loc.GatewayPort
			,loc.ICEGateLoginD
			,loc.PCSLoginID
			,loc.ISO20
			,loc.ISO40
			,loc.Active
	FROM	[DSR].[dbo].[mstLocation] loc
	WHERE	((ISNULL(@LocId, 0) = 0) OR (loc.pk_LocID = @LocId))
	AND		((ISNULL(@SchAbbr, '') = '') OR (loc.LocAbbr LIKE '%'+ @SchAbbr + '%'))
	AND		((ISNULL(@SchLocName, '') = '') OR (loc.LocName LIKE '%'+ @SchLocName + '%'))
	AND		((@IsActiveOnly = 'N') OR (loc.Active = @IsActiveOnly))
	AND		loc.IsDeleted = 0
	ORDER BY 
			CASE @SortDirection
				WHEN 'ASC' THEN
					CASE @SortExpression
						WHEN 'Abbr' THEN loc.LocAbbr
						WHEN 'Location' THEN loc.LocName
						ELSE '1'
					END 
			END ASC,
			CASE @SortDirection
				WHEN 'DESC' THEN
					CASE @SortExpression
						WHEN 'Abbr' THEN loc.LocAbbr
						WHEN 'Location' THEN loc.LocName
						ELSE '1'
					END 
			END DESC
END

GO