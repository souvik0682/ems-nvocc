/****** Object:  StoredProcedure [admin].[uspSaveRole]    Script Date: 12/12/2012 22:06:38 ******/

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 12-Dec-2012
-- Description	:
-- =============================================
CREATE PROCEDURE [admin].[uspSaveRole] 
(
	-- Add the parameters for the stored procedure here	
	@RoleID		INT,
	@RoleName	VARCHAR(50),
	@CompanyID	INT,
	@ModifiedBy	INT,
	@XMLDoc		NTEXT,
	@Result		INT = NULL OUT
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @hDoc			INT
	DECLARE @ErrMsg			NVARCHAR(4000)
	DECLARE @ErrSeverity	INT
	DECLARE	@NewRoleID		INT
	
	DECLARE @TempRoleMenu TABLE
	(
		MenuAccessID	INT,
		RoleID			INT,
		MenuID			INT,
		OptionAdd		BIT,
		OptionEdit		BIT,
		OptionDelete	BIT,
		OptionView		BIT
	)

	EXEC sp_xml_preparedocument @hDoc OUTPUT, @XMLDoc
	
	INSERT INTO @TempRoleMenu
	(
		MenuAccessID,
		MenuID,
		OptionAdd,
		OptionEdit,
		OptionDelete,
		OptionView			
	)
	SELECT	MenuAccessID,
			MenuID,
			CanAdd,
			CanEdit,
			CanDelete,
			CanView
	FROM	OPENXML(@hDoc, '/ArrayOfRoleMenuEntity/RoleMenuEntity')
	WITH (		
			MenuAccessID	INT	'MenuAccessID',
			MenuID			INT	'MenuID',
			CanAdd			BIT	'CanAdd',
			CanEdit			BIT 'CanEdit',
			CanDelete		BIT	'CanDelete',
			CanView			BIT	'CanView'
		 )			
	
	EXEC sp_xml_removedocument @hDoc
	
	IF EXISTS(SELECT 1 FROM dbo.mstRoles WHERE pk_RoleID <> @RoleID AND RoleName = @RoleName)
	BEGIN
		SET @Result = 1
	END
	ELSE
	BEGIN
		SET @Result = 0	
	END
	
	IF @Result = 0
	BEGIN
		BEGIN TRY
			BEGIN TRAN
			
			IF @RoleID > 0
			BEGIN
				UPDATE	dbo.mstRoles
				SET		RoleName = @RoleName
				WHERE	pk_RoleID = @RoleID
				
				--Update existing record
				UPDATE	RMA
				SET		RMA.OptionAdd = TMP.OptionAdd,
						RMA.OptionEdit = TMP.OptionEdit,
						RMA.OptionDelete = TMP.OptionDelete,
						RMA.OptionView = TMP.OptionView
				FROM	dbo.mstRoleMenuAccess RMA
						INNER JOIN @TempRoleMenu TMP
							ON RMA.pk_MenuAccessID = TMP.MenuAccessID
				
				INSERT INTO dbo.mstRoleMenuAccess
				(
					fk_CompanyID,
					fk_RoleID,
					fk_MenuID,
					OptionAdd,
					OptionEdit,
					OptionDelete,
					OptionView
				)	
				SELECT	@CompanyID,
						@RoleID,
						MenuID,
						OptionAdd,
						OptionEdit,
						OptionDelete,
						OptionView
				FROM	@TempRoleMenu
				WHERE	MenuAccessID = 0
			END
			ELSE
			BEGIN
				INSERT INTO dbo.mstRoles
				(
					RoleName,
					LocationSpecific,
					RoleStatus				
				)
				VALUES
				(
					@RoleName,
					1,
					1
				)
				
				SET @NewRoleID = SCOPE_IDENTITY()
				
				INSERT INTO dbo.mstRoleMenuAccess
				(
					fk_CompanyID,
					fk_RoleID,
					fk_MenuID,
					OptionAdd,
					OptionEdit,
					OptionDelete,
					OptionView
				)	
				SELECT	@CompanyID,
						@NewRoleID,
						MenuID,
						OptionAdd,
						OptionEdit,
						OptionDelete,
						OptionView
				FROM	@TempRoleMenu
			END
			
			COMMIT TRAN
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
		
			SELECT	@ErrMsg	= ERROR_MESSAGE(),@ErrSeverity = ERROR_SEVERITY()
		
			RAISERROR(@ErrMsg, @ErrSeverity, 1)
			RETURN -1
		END CATCH
		
		RETURN 0			
	END	
	
	RETURN 0
END

GO


