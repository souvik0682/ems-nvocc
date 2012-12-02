/****** Object:  StoredProcedure [admin].[uspSaveError]    Script Date: 12/02/2012 18:15:36 ******/

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 02-Dec-2012
-- Description	:	
-- =============================================
CREATE PROCEDURE [admin].[uspSaveError]
(
	-- Add the parameters for the stored procedure here
	@UserId			INT,
	@ErrorMessage	VARCHAR(255),	
	@StackTrace		VARCHAR(MAX)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [admin].[ErrorLog](UserId, ErrorMessage, StackTrace, ErrorDate)
	VALUES(@UserId, @ErrorMessage, @StackTrace, GETUTCDATE())
END

GO


