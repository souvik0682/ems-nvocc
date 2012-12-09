-- =============================================
-- Author:		RAJEN SAHA
-- Create date: 01/12/2012
-- Description:	VENDOR ADD/EDIT
-- =============================================
CREATE PROCEDURE [dbo].[spAddEditVendor]
	-- Add the parameters for the stored procedure here
	@VendorId INT = 0,
	@VendorType VARCHAR(5) = NULL,
	@LocationID INT = 0,
	@VendorSalutation INT = 0,
	@VendorName VARCHAR(250) = NULL,
	@VendorAddress VARCHAR(500) = NULL,
	@CFSCode VARCHAR(50) = NULL,
	@Terminalid int,
	@CompanyID INT=0,
	@VendorActive BIT = 1,
	@CreatedBy INT=0,
    @CreatedOn DATETIME = NULL,
    @ModifiedBy INT = 0,
    @ModifiedOn DATETIME = NULL,
    @RESULT INT OUT       
        
AS
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF(@VendorId <= 0)
	BEGIN
		INSERT INTO dbo.mstAddress(
									fk_CompanyID,
									fk_LocationID,
									AddrName,
									AddrAddress,
									AddrSalutation,
									AddrType,
									CFSCode,
									fk_terminalid,
									AddrActive,
									fk_UserAdded,
									fk_UserLastEdited,
									AddedOn,
									EditedOn
									)
							VALUES(	@CompanyID,
									@LocationID,
									@VendorName,
									@VendorAddress,
									@VendorSalutation,
									@VendorType,
									@CFSCode,
									@Terminalid,
									@VendorActive,		
									@CreatedBy,		
									@ModifiedBy,		
									@CreatedOn,
									@ModifiedOn)	
		IF(@@ERROR = 0)
		BEGIN
			SET @RESULT = 1
		END
		ELSE
		BEGIN
			SET @RESULT = 0
		END													
	END
	ELSE
	BEGIN
		UPDATE dbo.mstAddress SET
				fk_CompanyID=@CompanyID,
				fk_LocationID=@LocationID,
				AddrName=@VendorName,
				AddrAddress=@VendorAddress,
				AddrSalutation=@VendorSalutation,
				AddrType=@VendorType,
				CFSCode=@CFSCode,
				fk_terminalid = @Terminalid,
				AddrActive=@VendorActive,				
				fk_UserLastEdited=@ModifiedBy,				
				EditedOn=@ModifiedOn
				where fk_AddressID = @VendorId
		
		IF(@@ERROR = 0)
		BEGIN
			SET @RESULT = 1
		END
		ELSE
		BEGIN
			SET @RESULT = 0
		END				
	END
END TRY
BEGIN CATCH
		SET @RESULT = 0
END CATCH
