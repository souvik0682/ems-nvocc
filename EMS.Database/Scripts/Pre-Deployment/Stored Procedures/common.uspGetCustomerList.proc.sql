﻿/****** Object:  StoredProcedure [common].[uspGetCustomerList]    Script Date: 12/10/2012 22:22:11 ******/

--exec [common].[uspGetCustomerList] 1,null,null,null,'Location','ASC'

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 10-Dec-2012 
-- Description  :
-- =============================================
CREATE PROCEDURE [common].[uspGetCustomerList]
(
	-- Add the parameters for the stored procedure here
	@UserId				INT,
	@IsActiveOnly		CHAR(1),
	@SchLocAbbr			VARCHAR(3) = NULL,
	@SchCustName		VARCHAR(60) = NULL,
	@SchGroupName		VARCHAR(50) = NULL,
	@SchExecutiveName	VARCHAR(50) = NULL,
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
	DECLARE @TEMP TABLE (CustId INT)
			
	--DECLARE @RoleId	INT
	--DECLARE @LocId	INT
	--DECLARE @SalesPersonType CHAR(1)
	
	SELECT @Sort = CASE @SortDirection
						WHEN '' THEN 'ASC'
						ELSE @SortDirection
					END
	
	INSERT INTO @TEMP(CustId)
	SELECT CustId FROM [DSR].[common].[fnGetCustomerByUser](@UserId)
	
	SELECT * FROM
	(
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
			INNER JOIN @TEMP t7
				ON t1.pk_CustID = t7.CustId
	WHERE	((ISNULL(@SchLocAbbr, '') = '') OR (t3.LocName LIKE '%'+ @SchLocAbbr + '%'))
	AND		((ISNULL(@SchCustName, '') = '') OR (t1.CustName LIKE '%'+ @SchCustName + '%'))
	AND		((ISNULL(@SchGroupName, '') = '') OR (t2.GroupName LIKE '%'+ @SchGroupName + '%'))	
	AND		((ISNULL(@SchExecutiveName, '') = '') OR (t6.FirstName LIKE '%' + @SchExecutiveName + '%') OR (t6.LastName LIKE '%' + @SchExecutiveName + '%'))
	AND		t1.IsDeleted = 0
	AND		((@IsActiveOnly = 'N') OR (t1.Active = @IsActiveOnly))
	UNION
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
	WHERE	t1.fk_UserAdded = @UserId
	AND		t1.fk_UserID <> @UserId
	AND		((ISNULL(@SchLocAbbr, '') = '') OR (t3.LocName LIKE '%'+ @SchLocAbbr + '%'))
	AND		((ISNULL(@SchCustName, '') = '') OR (t1.CustName LIKE '%'+ @SchCustName + '%'))
	AND		((ISNULL(@SchGroupName, '') = '') OR (t2.GroupName LIKE '%'+ @SchGroupName + '%'))	
	AND		((ISNULL(@SchExecutiveName, '') = '') OR (t6.FirstName LIKE '%' + @SchExecutiveName + '%') OR (t6.LastName LIKE '%' + @SchExecutiveName + '%'))
	AND		t1.IsDeleted = 0
	AND		((@IsActiveOnly = 'N') OR (t1.Active = @IsActiveOnly))	
	) RES
	ORDER BY 
			CASE @SortDirection
				WHEN 'ASC' THEN
					CASE @SortExpression
						WHEN 'Location' THEN RES.LocName
						WHEN 'CustName' THEN RES.CustName
						ELSE '1'
					END 
			END ASC,
			CASE @SortDirection
				WHEN 'DESC' THEN
					CASE @SortExpression
						WHEN 'Location' THEN RES.LocName
						WHEN 'CustName' THEN RES.CustName
						ELSE '1'
					END 
			END DESC
						
END


GO


