USE [NVOCC]
GO

/****** Object:  StoredProcedure [chg].[uspSaveExchangeRate]    Script Date: 12/22/2012 10:50:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 14-Dec-2012 
-- Description  :
-- =============================================
CREATE PROCEDURE [chg].[uspSaveExchangeRate]
(
	-- Add the parameters for the stored procedure here
	@ExchangeRateID		INT,
	@CompanyID			INT,
	@ExchangeDate		DATE,
	@USDExchangeRate	DECIMAL(12,2),
	--@FreeDays			INT,
	@ModifiedBy			INT,
	@Result				INT = NULL OUT
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
   
	IF EXISTS(SELECT 1 FROM dbo.mstExchangeRate WHERE fk_ExchRateID <> @ExchangeRateID AND ExchDate = @ExchangeDate)
	BEGIN
		SET @Result = 1
	END
	ELSE
	BEGIN
		SET @Result = 0	
	END
	
	IF @Result = 0
	BEGIN
		IF(@ExchangeRateID>0)
			BEGIN
				UPDATE	 dbo.mstExchangeRate
				SET		 fk_companyid = @CompanyId
						,ExchDate = @ExchangeDate
						,USDXchRate = @USDExchangeRate
						--,FreeDays = @FreeDays
						,fk_UserLastEdited = @ModifiedBy
						,EditedOn = GETDATE()
				WHERE	 fk_ExchRateID = @ExchangeRateID
			END
		ELSE
			BEGIN
				DECLARE @NewRateID INT
				
				SELECT	@NewRateID = ISNULL(MAX(fk_ExchRateID), 0) + 1
				FROM	dbo.mstExchangeRate
				
				INSERT INTO dbo.mstExchangeRate
				(
					 fk_ExchRateID
					,fk_companyid
					,ExchDate
					,USDXchRate
					--,FreeDays
					,fk_UserAdded
					,AddedOn
				)
				VALUES
				(	
					 @NewRateID
					,@CompanyId
					,@ExchangeDate
					,@USDExchangeRate
					--,@FreeDays
					,@ModifiedBy
					,GETDATE()
				)
			END
	END
END

GO


