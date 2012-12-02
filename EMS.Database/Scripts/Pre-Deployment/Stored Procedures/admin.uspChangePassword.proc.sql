/****** Object:  StoredProcedure [admin].[uspChangePassword]    Script Date: 12/02/2012 18:13:27 ******/

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 02-Dec-2012 
-- Description  :
-- =============================================
CREATE PROCEDURE [admin].[uspChangePassword]
(
	-- Add the parameters for the stored procedure here
	@UserId		INT,
	@OldPwd		VARCHAR(50),
	@NewPwd		VARCHAR(50),
	@Result		BIT OUT
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS (SELECT 1 FROM [dbo].[mstUsers] WHERE pk_UserID = @UserId AND [Password] = @OldPwd)
	BEGIN
		UPDATE	[dbo].[mstUsers]
		SET		[Password]= @NewPwd
		WHERE	pk_UserID = @UserID
		
		SET @Result = 1
	END
	ELSE
		SET @Result = 0
END


GO


