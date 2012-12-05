/****** Object:  StoredProcedure [admin].[uspValidateUser]    Script Date: 12/05/2012 22:08:28 ******/

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date  : 02-Dec-2012
-- Description	: 
-- =============================================
CREATE PROCEDURE [admin].[uspValidateUser]
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
				ur.AllowMutipleLocation,
				ur.fk_RoleID AS 'RoleId',
				ur.fk_LocID AS 'LocId'
		FROM	dbo.mstUsers ur
				INNER JOIN DSR.dbo.mstRoles ro
					ON ur.fk_RoleID = ro.pk_RoleID
				INNER JOIN 	DSR.dbo.mstLocation lo
					ON ur.fk_LocID = lo.pk_LocID
		WHERE	ur.[UserName] = @UserName 
		AND		ur.[Password] = @Password 
		AND 	ur.[UserActive] = 1
END

GO


