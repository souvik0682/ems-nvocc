/*
Deployment script for EMS.Database
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "EMS.Database"
:setvar DefaultDataPath ""
:setvar DefaultLogPath ""

GO
USE [master]

GO
:on error exit
GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL
    AND DATABASEPROPERTYEX(N'$(DatabaseName)','Status') <> N'ONLINE')
BEGIN
    RAISERROR(N'The state of the target database, %s, is not set to ONLINE. To deploy to this database, its state must be set to ONLINE.', 16, 127,N'$(DatabaseName)') WITH NOWAIT
    RETURN
END

GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)] COLLATE SQL_Latin1_General_CP1_CI_AS
GO
EXECUTE sp_dbcmptlevel [$(DatabaseName)], 100;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
USE [$(DatabaseName)]

GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

GO
PRINT N'Creating [admin]...';


GO
CREATE SCHEMA [admin]
    AUTHORIZATION [dbo];


GO
PRINT N'Creating [common]...';


GO
CREATE SCHEMA [common]
    AUTHORIZATION [dbo];


GO
PRINT N'Creating [admin].[ErrorLog]...';


GO
CREATE TABLE [admin].[ErrorLog] (
    [Id]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [UserId]       INT           NULL,
    [ErrorMessage] VARCHAR (255) NULL,
    [StackTrace]   VARCHAR (MAX) NULL,
    [ErrorDate]    DATETIME      NULL,
    CONSTRAINT [PK_ErrorLog] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Creating [admin].[uspChangePassword]...';


GO
/****** Object:  StoredProcedure [admin].[uspChangePassword]    Script Date: 12/02/2012 18:13:27 ******/

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 02-Dec-2012 
-- Description  :
-- =============================================
CREATE PROCEDURE [admin].[uspChangePassword]
(
	-- Add the parameters for the stored procedure here
	@UserId		INT,
	@OldPwd		VARCHAR(50),
	@NewPwd		VARCHAR(50),
	@Result		BIT OUT
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS (SELECT 1 FROM [dbo].[mstUsers] WHERE pk_UserID = @UserId AND [Password] = @OldPwd)
	BEGIN
		UPDATE	[dbo].[mstUsers]
		SET		[Password]= @NewPwd
		WHERE	pk_UserID = @UserID
		
		SET @Result = 1
	END
	ELSE
		SET @Result = 0
END
GO
PRINT N'Creating [admin].[uspResetPassword]...';


GO
/****** Object:  StoredProcedure [admin].[uspResetPassword]    Script Date: 12/02/2012 18:14:53 ******/
-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 02-Dec-2012 
-- Description  :
-- =============================================
CREATE PROCEDURE [admin].[uspResetPassword]
(
	-- Add the parameters for the stored procedure here
	@UserId				INT,
	@Pwd				VARCHAR(50),
	@ModifiedBy			INT
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
   UPDATE	dbo.mstUsers
   SET		[Password] = @Pwd
   WHERE	pk_UserID = @UserId
END
GO
PRINT N'Creating [admin].[uspSaveError]...';


GO
/****** Object:  StoredProcedure [admin].[uspSaveError]    Script Date: 12/02/2012 18:15:36 ******/

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 02-Dec-2012
-- Description	:	
-- =============================================
CREATE PROCEDURE [admin].[uspSaveError]
(
	-- Add the parameters for the stored procedure here
	@UserId			INT,
	@ErrorMessage	VARCHAR(255),	
	@StackTrace		VARCHAR(MAX)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [admin].[ErrorLog](UserId, ErrorMessage, StackTrace, ErrorDate)
	VALUES(@UserId, @ErrorMessage, @StackTrace, GETUTCDATE())
END
GO
PRINT N'Creating [admin].[uspValidateUser]...';


GO
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
PRINT N'Creating [common].[uspGetLocation]...';


GO
/****** Object:  StoredProcedure [common].[uspGetLocation]    Script Date: 12/05/2012 22:09:58 ******/

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 03-Dec-2012 
-- Description  :
-- =============================================
CREATE PROCEDURE [common].[uspGetLocation]
(
	-- Add the parameters for the stored procedure here
	@LocId				INT = NULL,
	@IsActiveOnly		CHAR(1),
	@SchAbbr			VARCHAR(3) = NULL,
	@SchLocName			VARCHAR(50) = NULL,
	@SortExpression		VARCHAR(50),
	@SortDirection		VARCHAR(4)	
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
					
	SELECT	 loc.pk_LocID AS 'Id'
			,loc.LocName AS 'Name'
			,loc.LocAddress AS 'Address'
			,loc.LocCity AS 'City'
			,loc.LocPin AS 'Pin'
			,loc.LocAbbr
			,loc.LocPhone
			,loc.PGRFreeDays
			,loc.CANFooter
			,loc.CartingFooter
			,loc.SlotFooter
			,loc.PickUpFooter
			,loc.CustomHouseCode
			,loc.GatewayPort
			,loc.ICEGateLoginD
			,loc.PCSLoginID
			,loc.ISO20
			,loc.ISO40
			,loc.Active
	FROM	[DSR_MOD].[dbo].[mstLocation] loc
	WHERE	((ISNULL(@LocId, 0) = 0) OR (loc.pk_LocID = @LocId))
	AND		((ISNULL(@SchAbbr, '') = '') OR (loc.LocAbbr LIKE '%'+ @SchAbbr + '%'))
	AND		((ISNULL(@SchLocName, '') = '') OR (loc.LocName LIKE '%'+ @SchLocName + '%'))
	AND		((@IsActiveOnly = 'N') OR (loc.Active = @IsActiveOnly))
	AND		loc.IsDeleted = 0
	ORDER BY 
			CASE @SortDirection
				WHEN 'ASC' THEN
					CASE @SortExpression
						WHEN 'Abbr' THEN loc.LocAbbr
						WHEN 'Location' THEN loc.LocName
						ELSE '1'
					END 
			END ASC,
			CASE @SortDirection
				WHEN 'DESC' THEN
					CASE @SortExpression
						WHEN 'Abbr' THEN loc.LocAbbr
						WHEN 'Location' THEN loc.LocName
						ELSE '1'
					END 
			END DESC
END
GO
PRINT N'Creating [common].[uspSaveLocation]...';


GO
/****** Object:  StoredProcedure [common].[uspSaveLocation]    Script Date: 12/05/2012 22:10:02 ******/

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 03-Dec-2012 
-- Description  :
-- =============================================
CREATE PROCEDURE [common].[uspSaveLocation]
(
	-- Add the parameters for the stored procedure here
	@LocId				INT,
	@PGRFreeDays		INT,
	@CanFooter			VARCHAR(300),
	@SlotFooter			VARCHAR(300),
	@CartingFooter		VARCHAR(300),
	@PickUpFooter		VARCHAR(300),
	@CustomHouseCode	CHAR(6),
	@GatewayPort		CHAR(6),
	@ICEGateLoginD		VARCHAR(20),
	@PCSLoginID			VARCHAR(8),
	@ISO20				CHAR(4),
	@ISO40				CHAR(4),
	@ModifiedBy			INT
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
   
	UPDATE	[DSR_MOD].[dbo].[mstLocation]
	SET		 PGRFreeDays = @PGRFreeDays
			,CanFooter = @CanFooter
			,SlotFooter = @SlotFooter
			,CartingFooter = @CartingFooter
			,PickUpFooter = @PickUpFooter
			,CustomHouseCode = @CustomHouseCode
			,GatewayPort = @GatewayPort
			,ICEGateLoginD = @ICEGateLoginD
			,PCSLoginID = @PCSLoginID
			,ISO20 = @ISO20
			,ISO40 = @ISO40
			--,fk_UserLastEdited = @ModifiedBy
			,EditedOn = GETUTCDATE()
	WHERE	pk_LocID = @LocId
END
GO
-- Refactoring step to update target server with deployed transaction logs
CREATE TABLE  [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
GO
sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
GO

GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        DECLARE @VarDecimalSupported AS BIT;
        SELECT @VarDecimalSupported = 0;
        IF ((ServerProperty(N'EngineEdition') = 3)
            AND (((@@microsoftversion / power(2, 24) = 9)
                  AND (@@microsoftversion & 0xffff >= 3024))
                 OR ((@@microsoftversion / power(2, 24) = 10)
                     AND (@@microsoftversion & 0xffff >= 1600))))
            SELECT @VarDecimalSupported = 1;
        IF (@VarDecimalSupported > 0)
            BEGIN
                EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
            END
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET MULTI_USER 
    WITH ROLLBACK IMMEDIATE;


GO
