/****** Object:  StoredProcedure [admin].[uspGetRole]    Script Date: 12/22/2012 10:35:04 ******/

--exec [common].[uspGetRole]

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 06-Dec-2012 
-- Description  :
-- =============================================
CREATE PROCEDURE [admin].[uspGetRole]
(
	-- Add the parameters for the stored procedure here
	@RoleId			INT = NULL,
	@IsActiveOnly	BIT,
	@SchRole		VARCHAR(50) = NULL,
	@SortExpression VARCHAR(20),
	@SortDirection  VARCHAR(4)
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
			,RoleStatus AS 'Active'
	FROM	dbo.mstRoles
	WHERE	((ISNULL(@RoleId, 0) = 0) OR (pk_RoleID = @RoleId))
	AND		Display = 1
	--AND		((@IsActiveOnly = 0) OR (Active = @IsActiveOnly))
	AND		((ISNULL(@SchRole, '') = '') OR (RoleName LIKE '%'+ @SchRole + '%'))
	ORDER BY RoleName
END

GO


