/****** Object:  StoredProcedure [admin].[uspDeleteUser]    Script Date: 12/06/2012 23:37:38 ******/

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 06-Dec-2012 
-- Description  :
-- =============================================
CREATE PROCEDURE [admin].[uspDeleteUser]
(
	-- Add the parameters for the stored procedure here
	@UserId		INT,
	@ModifiedBy INT
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE	 dbo.mstUsers
	SET		 IsDeleted = 1
	WHERE	 pk_UserID = @UserId
END

GO