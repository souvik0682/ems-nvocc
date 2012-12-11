/****** Object:  StoredProcedure [admin].[uspGetMenuByRole]    Script Date: 12/12/2012 00:15:43 ******/

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 11-Dec-2012
-- Description	: 
-- =============================================
CREATE PROCEDURE [admin].[uspGetMenuByRole]
(
	-- Add the parameters for the stored procedure here
	@RoleId	INT,
	@MainId INT
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT	 ISNULL(RM.pk_MenuAccessID, 0) AS 'MenuAccessID'
			,ISNULL(RM.fk_CompanyID, 0) AS 'CompanyID'
			,ISNULL(RM.fk_RoleID, 0) AS 'RoleID'
			,ME.pk_MenuID AS 'MenuID'
			,ME.MenuName
			,ME.MainID 
			,ME.SubID
			,ME.SubsubID
			,ISNULL(RM.OptionAdd, 0) AS 'CanAdd'
			,ISNULL(RM.OptionEdit, 0) AS 'CanEdit'
			,ISNULL(RM.OptionDelete, 0) AS 'CanDelete'
			,ISNULL(RM.OptionView, 0) AS 'CanView'
	FROM	dbo.mstRoleMenuAccess RM
			RIGHT OUTER JOIN dbo.mstMenu ME ON RM.fk_MenuID = ME.pk_MenuID AND RM.fk_RoleID = @RoleId
	WHERE	ME.MainID = @MainId
	AND		ME.SubsubID > 0
END

GO


