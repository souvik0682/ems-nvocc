/****** Object:  StoredProcedure [admin].[uspSaveUser]    Script Date: 12/06/2012 23:42:30 ******/

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 06-Dec-2012 
-- Description  :
-- =============================================
CREATE PROCEDURE [admin].[uspSaveUser]
(
	-- Add the parameters for the stored procedure here
	@UserId					INT,
	@UserName				VARCHAR(10),
	@Pwd					VARCHAR(50),
	@FirstName				VARCHAR(30),
	@LastName				VARCHAR(30),
	@RoleId					INT,
	@LocId					INT,
	@EmailId				VARCHAR(50) = NULL,
	@IsActive				BIT,
	@AllowMutipleLocation	BIT,
	@CompanyId				INT,
	@ModifiedBy				INT,
	@Result					INT = NULL OUT
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
   
	IF EXISTS(SELECT 1 FROM dbo.mstUsers WHERE pk_UserID <> @UserId AND UserName = @UserName)
	BEGIN
		SET @Result = 1
	END
	ELSE
	BEGIN
		SET @Result = 0	
	END
	
	IF @Result = 0
	BEGIN
		IF(@UserId>0)
			BEGIN
				UPDATE	 dbo.mstUsers
				SET		 UserName = @UserName
						,FirstName = @FirstName
						,LastName = @LastName
						,fk_RoleID = @RoleId
						,fk_LocID = @LocId
						,emailID = @EmailId
						,UserActive = @IsActive
						,AllowMutipleLocation = @AllowMutipleLocation
						,fk_companyid = @CompanyId
				WHERE	 pk_UserID = @UserId
			END
		ELSE
			BEGIN
				INSERT INTO dbo.mstUsers
				(
					 UserName
					,[Password]
					,FirstName
					,LastName
					,fk_RoleID
					,fk_LocID
					,emailID
					,UserActive
					,AllowMutipleLocation
					,fk_companyid
				)
				VALUES
				(
					 @UserName
					,@Pwd
					,@FirstName
					,@LastName
					,@RoleId
					,@LocId
					,@EmailId
					,1
					,@AllowMutipleLocation
					,@CompanyId
				)
			END
	END
END

GO