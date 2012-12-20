/****** Object:  StoredProcedure [admin].[uspGetMenuAccessByUser]    Script Date: 12/21/2012 00:34:03 ******/

--exec [admin].[uspGetMenuAccessByUser] 1, 28

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 13-Dec-2012 
-- Description	:
-- =============================================
CREATE PROCEDURE [admin].[uspGetMenuAccessByUser]
(
	-- Add the parameters for the stored procedure here
	@UserId INT,
	@MenuId	INT
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @RoleId INT
	
	SELECT	@RoleId = fk_RoleID
	FROM	dbo.mstUsers
	WHERE	pk_UserID = @UserId
	
	SELECT	pk_MenuAccessID AS 'MenuAccessID',
			fk_CompanyID AS 'CompanyID',
			fk_RoleID AS 'RoleID',
			MEN.pk_MenuID AS 'MenuID',
			MEN.MainID,
			MEN.SubID,
			MEN.SubsubID,
			RMA.OptionAdd AS 'CanAdd',
			RMA.OptionEdit AS 'CanEdit',
			RMA.OptionDelete AS 'CanDelete',
			RMA.OptionView AS 'CanView',
			MEN.MenuName
	FROM	dbo.mstRoleMenuAccess RMA
			INNER JOIN dbo.mstMenu MEN
				ON RMA.fk_MenuID = MEN.pk_MenuID
			INNER JOIN dbo.mstRoles ROL
				ON RMA.fk_RoleID = ROL.pk_RoleID
	WHERE	fk_RoleID = @RoleId
	AND		RMA.fk_MenuID = @MenuId
	AND		ROL.RoleStatus = 1
END

GO


