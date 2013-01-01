/****** Object:  StoredProcedure [report].[uspGetImportSummary]    Script Date: 01/01/2013 20:53:10 ******/

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 01-Jan-2013 
-- Description	:
-- =============================================
CREATE PROCEDURE [report].[uspGetImportSummary]
(
	-- Add the parameters for the stored procedure here
	@NVOCCId		INT,
	@LocId			INT,
	@DischargeFrom	DATETIME,
	@DischargeTo	DATETIME
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 1
END

GO


