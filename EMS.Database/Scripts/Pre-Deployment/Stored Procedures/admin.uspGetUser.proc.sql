/****** Object:  StoredProcedure [admin].[uspGetUser]    Script Date: 12/21/2012 00:36:09 ******/

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 06-Dec-2012
-- Description	:
-- =============================================
CREATE PROCEDURE [admin].[uspGetUser]
(
	-- Add the parameters for the stored procedure here
	@UserId			INT = NULL,
	@IsActiveOnly	BIT,
	@SchUserName	VARCHAR(10) = NULL,
	@SchFirstName	VARCHAR(30) = NULL,
	@SortExpression VARCHAR(20),
	@SortDirection  VARCHAR(4)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @Sort VARCHAR(4)
	
	SELECT @Sort = CASE @SortDirection
						WHEN '' THEN 'ASC'
						ELSE @SortDirection
					END
					
	SELECT	 us.pk_UserID AS 'UserId'
			,us.UserName
			,us.FirstName
			,us.LastName
			,us.fk_RoleID AS 'RoleId'
			,ro.RoleName
			,ISNULL(us.fk_LocID, 0) AS 'LocId'
			,lo.LocName
			,us.emailID
			,us.UserActive
			,us.AllowMutipleLocation
	FROM	dbo.mstUsers us
			INNER JOIN dbo.mstRoles ro
				ON us.fk_RoleID = ro.pk_RoleID
			LEFT OUTER JOIN [DSR].[dbo].[mstLocation] lo
				ON us.fk_LocID = lo.pk_LocID
	WHERE	((ISNULL(@UserId, 0) = 0) OR (us.pk_UserID = @UserId))
	--AND		us.Active = 'Y'
	AND		((@IsActiveOnly = 0) OR (us.UserActive = @IsActiveOnly))
	AND		((ISNULL(@SchUserName, '') = '') OR (us.UserName LIKE '%'+ @SchUserName + '%'))
	AND		((ISNULL(@SchFirstName, '') = '') OR (us.FirstName LIKE '%'+ @SchFirstName + '%'))
	AND		us.IsDeleted = 0
	ORDER BY 
			CASE @SortDirection
				WHEN 'ASC' THEN
					CASE @SortExpression
						WHEN 'UserName' THEN UserName
						WHEN 'RoleName' THEN FirstName
						WHEN 'FirstName' THEN FirstName
						WHEN 'LastName' THEN LastName
						WHEN 'LocName' THEN FirstName
						ELSE '1'
					END 
			END ASC,
			CASE @SortDirection
				WHEN 'DESC' THEN
					CASE @SortExpression
						WHEN 'UserName' THEN UserName
						WHEN 'RoleName' THEN FirstName
						WHEN 'FirstName' THEN FirstName
						WHEN 'LastName' THEN LastName
						WHEN 'LocName' THEN FirstName
						ELSE '1'
					END 
			END DESC	
END

GO


