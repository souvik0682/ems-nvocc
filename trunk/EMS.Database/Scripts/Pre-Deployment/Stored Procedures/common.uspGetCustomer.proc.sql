/****** Object:  StoredProcedure [common].[uspGetCustomer]    Script Date: 12/10/2012 22:21:14 ******/

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 10-Dec-2012 
-- Description  :
-- =============================================
CREATE PROCEDURE [common].[uspGetCustomer]
(
	-- Add the parameters for the stored procedure here
	@CustId				INT = NULL,
	@IsActiveOnly		CHAR(1),
	@SchLocAbbr			VARCHAR(3) = NULL,
	@SchCustName		VARCHAR(60) = NULL,
	@SchGroupName		VARCHAR(50) = NULL,
	@SalesExecutiveId	INT = NULL,
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
						
	SELECT   t1.[pk_CustID] AS 'CustID'
			,t1.[fk_GroupID] AS 'GroupID'
			,t2.GroupName
			,t1.[fk_LocID] AS 'LocID'
			,t3.LocName + ' (' + t3.LocAbbr + ')' AS LocName
			,t1.[fk_AreaID] AS 'AreaID'
			,t4.AreaName
			,t1.[fk_CustTypeID] AS 'CustTypeID' 
			,t5.TypeName AS 'CustTypeName'
			,t1.[CorporateorLocal]
			,t1.[CustName]
			,t1.[CustAddress]
			,t1.[CustCity]
			,t1.[CustPin]
			,t1.[CustPhone1]
			,t1.[CustPhone2]
			,t1.[ContactPerson1]
			,t1.[ContactDesg1]
			,t1.[ContactMob1]
			,t1.[ContactEmailId1]
			,t1.[ContactPerson2]
			,t1.[ContactDesg2]
			,t1.[ContactMob2]
			,t1.[ContactEmailId2]
			,t1.[CustomerProfile]
			,t1.[PANNo]
			,t1.[TANNo]
			,t1.[BINNo]
			,t1.[IECNo]
			,t1.[fk_UserID] AS 'SalesExecutiveId'
			,t6.FirstName + ' ' + t6.LastName AS 'SalesExecutiveName'
			,t1.[Active]
	FROM	[DSR].[dbo].[mstCustomer] t1
			INNER JOIN [DSR].[dbo].[mstGroupCompany] t2 
				ON t1.fk_GroupID = t2.pk_GroupID
			INNER JOIN [DSR].[dbo].[mstLocation] t3
				ON t1.fk_LocID = t3.pk_LocID
			INNER JOIN [DSR].[dbo].[mstArea] t4
				ON t1.fk_AreaID = t4.pk_AreaID
			INNER JOIN [DSR].[dbo].[mstCustomerType] t5
				ON t1.fk_CustTypeID = t5.pk_CustTypeID
			LEFT OUTER JOIN [DSR].[dbo].[mstUser] t6
				ON t1.fk_UserID = t6.pk_UserID
	WHERE	((ISNULL(@CustId, 0) = 0) OR (t1.pk_CustID = @CustId))
	AND		((ISNULL(@SalesExecutiveId,0) = 0) OR (t1.fk_UserID = @SalesExecutiveId))
	AND		((ISNULL(@SchLocAbbr, '') = '') OR (t3.LocName LIKE '%'+ @SchLocAbbr + '%'))
	AND		((ISNULL(@SchCustName, '') = '') OR (t1.CustName LIKE '%'+ @SchCustName + '%'))
	AND		((ISNULL(@SchGroupName, '') = '') OR (t2.GroupName LIKE '%'+ @SchGroupName + '%'))	
	AND		((@IsActiveOnly = 'N') OR (t1.Active = @IsActiveOnly))
	AND		t1.IsDeleted = 0
	ORDER BY 
			CASE @SortDirection
				WHEN 'ASC' THEN
					CASE @SortExpression
						WHEN 'Location' THEN t3.LocName
						WHEN 'CustName' THEN t1.CustName
						ELSE '1'
					END 
			END ASC,
			CASE @SortDirection
				WHEN 'DESC' THEN
					CASE @SortExpression
						WHEN 'Location' THEN t3.LocName
						WHEN 'CustName' THEN t1.CustName
						ELSE '1'
					END 
			END DESC
END


GO


