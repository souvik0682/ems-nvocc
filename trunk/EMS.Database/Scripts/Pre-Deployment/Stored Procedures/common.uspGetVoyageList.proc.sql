/****** Object:  StoredProcedure [common].[uspGetVoyageList]    Script Date: 01/01/2013 20:50:14 ******/

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 01-Jan-2013
-- Description	:
-- =============================================
CREATE PROCEDURE [common].[uspGetVoyageList] 
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
	SELECT	pk_VoyageID AS 'VoyageID',
			VoyageNo + '(' + CAST(pk_VoyageID AS VARCHAR) + ')' AS 'VoyageNo'
	FROM	dbo.trnVoyage
	WHERE	VoyageNo LIKE '' + @InitialChar + '%'
	ORDER BY	VoyageNo
END

GO


