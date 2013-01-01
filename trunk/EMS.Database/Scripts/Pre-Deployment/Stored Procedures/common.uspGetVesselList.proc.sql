/****** Object:  StoredProcedure [common].[uspGetVesselList]    Script Date: 01/01/2013 20:49:23 ******/

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 01-Jan-2013
-- Description	:
-- =============================================
CREATE PROCEDURE [common].[uspGetVesselList] 
(
	-- Add the parameters for the stored procedure here
	@InitialChar VARCHAR(100)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT	pk_VesselID AS 'VesselID',
			VesselName + '(' + CAST(pk_VesselID AS VARCHAR) + ')' AS 'VesselName'
	FROM	dbo.trnVessel
	WHERE	VesselName LIKE '' + @InitialChar + '%'
	ORDER BY	VesselName
END

GO


