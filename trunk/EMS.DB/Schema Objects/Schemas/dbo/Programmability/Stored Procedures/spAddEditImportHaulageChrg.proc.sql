-- =============================================
-- Author:		RAJEN SAHA
-- Create date: 01/12/2012
-- Description:	VENDOR ADD/EDIT
-- =============================================
CREATE PROCEDURE [dbo].[spAddEditImportHaulageChrg]
	-- Add the parameters for the stored procedure here
	@HaulageChgID INT = 0,
	@LocationFrom int = 0,
	@LocationTo INT = 0,
	@ContainerSize varchar(2) = NULL,
	@WeightFrom decimal(12,2) = 0.0,
	@WeightTo decimal(12,2) = 0.0,
	@HaulageRate decimal(12,2) = 0.0,
	@HaulageStatus bit,
	
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
	IF(@HaulageChgID <= 0)
	BEGIN
		INSERT INTO dbo.mstHaulageCharge(
										pk_HaulageChgID,
										fk_LocationFrom,
										fk_LocationTo,
										ContainerSize,
										WeightFrom,
										WeightTo,
										HaulageRate,
										HaulageStatus,
										fk_UserAdded,										
										AddedOn,
										fk_UserLastEdited,
										EditedOn)
								VALUES(
										@HaulageChgID,
										@LocationFrom,
										@LocationTo,
										@ContainerSize,
										@WeightFrom,
										@WeightTo,
										@HaulageRate,
										@HaulageStatus,										
										@CreatedBy,
										@CreatedOn,
										@ModifiedBy,
										@ModifiedOn
										)	
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
		UPDATE dbo.mstHaulageCharge SET
				
				fk_LocationFrom = @LocationFrom ,
				fk_LocationTo = @LocationTo,
				ContainerSize = @ContainerSize,
				WeightFrom = @WeightFrom,
				WeightTo = @WeightTo,
				HaulageRate = @HaulageRate,
				HaulageStatus = @HaulageStatus,
				fk_UserLastEdited = @ModifiedBy,
				EditedOn = @ModifiedOn
				
				where pk_HaulageChgID = @HaulageChgID
		
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
