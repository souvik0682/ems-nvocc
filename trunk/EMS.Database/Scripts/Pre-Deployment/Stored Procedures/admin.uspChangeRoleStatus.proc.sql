-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 11-Dec-2012
-- Description	:
-- =============================================
CREATE PROCEDURE [admin].[uspChangeRoleStatus] 
(
	-- Add the parameters for the stored procedure here
	@RoleId		INT,
	@RoleStatus BIT,
	@ModifiedBy	INT
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE	dbo.mstRoles
	SET		RoleStatus = @RoleStatus
	WHERE	pk_RoleID = @RoleId
END
GO
