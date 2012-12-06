/****** Object:  StoredProcedure [common].[uspDeleteLocation]    Script Date: 12/06/2012 23:41:00 ******/
-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 03-Dec-2012 
-- Description  :
-- =============================================
CREATE PROCEDURE [common].[uspDeleteLocation]
(
	-- Add the parameters for the stored procedure here
	@LocId		INT,
	@ModifiedBy INT
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE	[DSR].[dbo].[mstLocation]
	SET		 IsDeleted = 1
			,fk_UserLastEdited = @ModifiedBy
			,EditedOn = GETUTCDATE()
	WHERE	pk_LocID = @LocId
END

GO