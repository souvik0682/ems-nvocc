/****** Object:  StoredProcedure [common].[uspGetSalesExecutiveNew]    Script Date: 12/10/2012 21:02:38 ******/

--exec [common].[uspGetSalesExecutiveNew] 34

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 10-Dec-2012 
-- Description	:
-- =============================================
CREATE PROCEDURE [common].[uspGetSalesExecutiveNew]
(
	-- Add the parameters for the stored procedure here
	@UserId INT = NULL
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @LocId	INT
	DECLARE @RoleId	INT
	DECLARE @ExistingUserId INT
	
	SET @ExistingUserId = 0 
	
	SELECT	 @LocId = fk_LocID
			,@RoleId = fk_RoleID
	FROM	[DSR].[dbo].[mstUser]
	WHERE	pk_UserID = @UserId
	
	--Check whether customer is temporarily assigned to another user or not.
	SELECT	@ExistingUserId = [fk_ExistingUserD]
	FROM	[DSR].[dbo].[mstCustAssign] 
	WHERE	[fk_NewUserID] = @UserId 
	AND		[StartDate] <= GETDATE()
	AND		[EndDate] >= GETDATE() 
	AND		ParmanentorTemporary = 'T'
	
	IF @RoleId = 1 OR @RoleId = 3 --For ADMIN and MANAGEMENT
	BEGIN
		SELECT	 us.pk_UserID AS 'UserId'
				,us.UserName
				,us.FirstName
				,us.LastName
				,us.fk_RoleID AS 'RoleId'
				,ro.RoleName
				,ro.SalesRole
				,us.fk_LocID AS 'LocId'
				,lo.LocName
				,us.SalesPersonType
				,us.emailID
				,us.Active
		FROM	[DSR].[dbo].[mstUser] us
				INNER JOIN [DSR].[dbo].[mstRoles] ro
					ON us.fk_RoleID = ro.pk_RoleID
				INNER JOIN [DSR].[dbo].[mstLocation] lo
					ON us.fk_LocID = lo.pk_LocID
		WHERE	us.SalesPersonType IN ('L','M')
		AND		us.Active = 'Y'
		AND		us.IsDeleted = 0
		ORDER BY us.FirstName			
	END
	ELSE IF @RoleId = 2 --For MANAGERS
	BEGIN
		SELECT	 us.pk_UserID AS 'UserId'
				,us.UserName
				,us.FirstName
				,us.LastName
				,us.fk_RoleID AS 'RoleId'
				,ro.RoleName
				,ro.SalesRole
				,us.fk_LocID AS 'LocId'
				,lo.LocName
				,us.SalesPersonType
				,us.emailID
				,us.Active
		FROM	[DSR].[dbo].[mstUser] us
				INNER JOIN [DSR].[dbo].[mstRoles] ro
					ON us.fk_RoleID = ro.pk_RoleID
				INNER JOIN [DSR].[dbo].[mstLocation] lo
					ON us.fk_LocID = lo.pk_LocID
		WHERE	us.fk_LocID = @LocId
		AND		us.SalesPersonType IN ('L','M')
		AND		us.Active = 'Y'
		AND		us.IsDeleted = 0
		ORDER BY us.FirstName	
	END
	ELSE IF @RoleId = 4 --For SALES EXECUTIVE
	BEGIN
		SELECT	 us.pk_UserID AS 'UserId'
				,us.UserName
				,us.FirstName
				,us.LastName
				,us.fk_RoleID AS 'RoleId'
				,ro.RoleName
				,ro.SalesRole
				,us.fk_LocID AS 'LocId'
				,lo.LocName
				,us.SalesPersonType
				,us.emailID
				,us.Active
		FROM	[DSR].[dbo].[mstUser] us
				INNER JOIN [DSR].[dbo].[mstRoles] ro
					ON us.fk_RoleID = ro.pk_RoleID
				INNER JOIN [DSR].[dbo].[mstLocation] lo
					ON us.fk_LocID = lo.pk_LocID
		WHERE	((us.pk_UserID = @UserId) OR (us.pk_UserID = @ExistingUserId))
		AND		us.SalesPersonType IN ('L','M')
		AND		us.Active = 'Y'
		AND		us.IsDeleted = 0
		ORDER BY us.FirstName	
	END
END

GO


