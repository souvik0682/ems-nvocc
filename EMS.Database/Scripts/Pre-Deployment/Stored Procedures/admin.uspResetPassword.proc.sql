/****** Object:  StoredProcedure [admin].[uspResetPassword]    Script Date: 12/02/2012 18:14:53 ******/
-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 02-Dec-2012 
-- Description  :
-- =============================================
CREATE PROCEDURE [admin].[uspResetPassword]
(
	-- Add the parameters for the stored procedure here
	@UserId				INT,
	@Pwd				VARCHAR(50),
	@ModifiedBy			INT
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
   UPDATE	dbo.mstUsers
   SET		[Password] = @Pwd
   WHERE	pk_UserID = @UserId
END

GO


