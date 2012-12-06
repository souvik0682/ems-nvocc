/****** Object:  StoredProcedure [admin].[uspGetRole]    Script Date: 12/06/2012 23:38:41 ******/

--exec [common].[uspGetRole]

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 06-Dec-2012 
-- Description  :
-- =============================================
CREATE PROCEDURE [admin].[uspGetRole]
(
	-- Add the parameters for the stored procedure here
	@RoleId INT = NULL
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT	 pk_RoleID AS 'RoleId'
			,RoleName
			,LocationSpecific
			,RoleStatus
	FROM	dbo.mstRoles
	WHERE	((ISNULL(@RoleId, 0) = 0) OR (pk_RoleID = @RoleId))
END

GO