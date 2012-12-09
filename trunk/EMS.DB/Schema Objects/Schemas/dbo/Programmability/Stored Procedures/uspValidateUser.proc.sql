-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date  : 08-Jul-2012
-- Description	: 
-- =============================================
CREATE PROCEDURE [dbo].[uspValidateUser]
	-- Add the parameters for the stored procedure here
(
	@UserName NVARCHAR(100),
	@Password NVARCHAR(100)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    DECLARE @IsActive BIT
    
   
    --IF @IsActive = 1
    --BEGIN
		SELECT	ur.pk_UserID AS 'UserId',
				ur.FirstName,
				ur.LastName,
				ur.emailID,
				ur.fk_RoleID AS 'RoleId',
				ur.fk_LocID AS 'LocId'
				--ur.SalesPersonType,
				--ro.SalesRole
		FROM	dbo.mstUsers ur
				INNER JOIN dbo.mstRoles ro
					ON ur.fk_RoleID = ro.pk_RoleID
				INNER JOIN 	DSR.dbo.mstLocation lo
					ON ur.fk_LocID = lo.pk_LocID
		WHERE	ur.[UserName] = @UserName 
		AND		ur.[Password] = @Password 
		AND 	ur.[UserActive] = 1
		AND		ur.IsDeleted = 0
		
	--END
	--ELSE
	--BEGIN
	--	SELECT	 0 AS 'UserId'
	--			,NULL AS 'UserFirstName'
	--			,NULL AS 'UserMiddleName'
	--			,NULL AS 'UserLastName'
	--			,0 AS 'IsAdmin'
	--			,'NA' AS 'Level'
	--			,0 AS 'ZoneID'
	--END
END
