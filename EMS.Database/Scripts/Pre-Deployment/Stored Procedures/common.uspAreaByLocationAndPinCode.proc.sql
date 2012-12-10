/****** Object:  StoredProcedure [common].[uspAreaByLocationAndPinCode]    Script Date: 12/10/2012 21:43:52 ******/

--exec [common].[uspAreaByLocationAndPinCode] 1

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 17-Jul-2012 
-- Description  :
-- =============================================
CREATE PROCEDURE [common].[uspAreaByLocationAndPinCode]
(
	-- Add the parameters for the stored procedure here
	@LocId		INT,
	@PinCode	VARCHAR(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT	 ar.pk_AreaID AS 'Id'
			,ar.AreaName AS 'Name'
			,ar.PinCode
			,ar.fk_LocID AS 'LocId'
			,loc.LocName 
			,ar.Active
	FROM	[DSR].[dbo].[mstArea] ar
			INNER JOIN [DSR].[dbo].[mstLocation] loc
				ON loc.pk_LocID = ar.fk_LocID			
	WHERE	ar.fk_LocID = @LocId
	AND		ar.PinCode = @PinCode
	AND		ar.Active = 'Y'
	AND		ar.IsDeleted = 0
	ORDER BY ar.AreaName
END

GO


