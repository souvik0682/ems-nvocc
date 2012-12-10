/****** Object:  StoredProcedure [common].[uspGetGroupCompany]    Script Date: 12/10/2012 21:01:25 ******/

--exec [common].[uspGetArea] NULL,'N'

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 10-Dec-2012 
-- Description  :
-- =============================================
CREATE PROCEDURE [common].[uspGetGroupCompany]
(
	-- Add the parameters for the stored procedure here
	@GroupId			INT = NULL,
	@IsActiveOnly		CHAR(1),
	@SchGroupName		VARCHAR(50) = NULL,
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
						
	SELECT	 pk_GroupID AS 'Id'
			,GroupName AS 'Name'
			,[Address]
			,City
			,Pin
			,Phone
			,Active
	FROM	[DSR].[dbo].[mstGroupCompany]
	WHERE	((ISNULL(@GroupId, 0) = 0) OR (pk_GroupID = @GroupId))
	AND		((ISNULL(@SchGroupName, '') = '') OR (GroupName LIKE '%'+ @SchGroupName + '%'))
	AND		((@IsActiveOnly = 'N') OR (Active = @IsActiveOnly))
	AND		IsDeleted = 0
	ORDER BY 
			CASE @SortDirection
				WHEN 'ASC' THEN
					CASE @SortExpression
						WHEN 'GroupName' THEN GroupName
						ELSE '1'
					END 
			END ASC,
			CASE @SortDirection
				WHEN 'DESC' THEN
					CASE @SortExpression
						WHEN 'GroupName' THEN GroupName
						ELSE '1'
					END 
			END DESC
END

GO


