USE [NVOCC]
GO

/****** Object:  StoredProcedure [chg].[uspGetExchangeRate]    Script Date: 12/22/2012 10:48:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--exec [chg].[uspGetExchangeRate] 0,'2012-03-04','DATE','ASC'

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 08-Dec-2012 
-- Description  :
-- =============================================
CREATE PROCEDURE [chg].[uspGetExchangeRate]
(
	-- Add the parameters for the stored procedure here
	@ExchangeRateID		INT = NULL,
	@SchExchangeDate	DATE = NULL,
	@SortExpression		VARCHAR(20),
	@SortDirection		VARCHAR(4)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT	 fk_ExchRateID AS 'ExchRateID'
			,fk_CompanyID AS 'CompanyID'
			,ExchDate
			,USDXchRate
			--,FreeDays
	FROM	dbo.mstExchangeRate
	WHERE	((ISNULL(@ExchangeRateID, 0) = 0) OR (fk_ExchRateID = @ExchangeRateID))
	--AND		((ISNULL(@SchExchangeDate, '') = '') OR (ExchDate =  + @SchExchangeDate))
	ORDER BY fk_ExchRateID DESC
END

GO


