/****** Object:  StoredProcedure [common].[uspGetCustomerType]    Script Date: 12/10/2012 21:02:07 ******/

--exec [common].[uspGetArea] NULL,'N'

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 10-Dec-2012 
-- Description  :
-- =============================================
CREATE PROCEDURE [common].[uspGetCustomerType]
(
	-- Add the parameters for the stored procedure here
	@CustTypeId			INT = NULL,
	@IsActiveOnly		CHAR(1)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT	 pk_CustTypeID AS 'CustTypeId'
			,TypeName AS 'CustTypeName'
			,Active
	FROM	[DSR].[dbo].[mstCustomerType]
	WHERE	((ISNULL(@CustTypeId, 0) = 0) OR (pk_CustTypeID = @CustTypeId))
	AND		((@IsActiveOnly = 'N') OR (Active = @IsActiveOnly))
	ORDER BY TypeName
END

GO


