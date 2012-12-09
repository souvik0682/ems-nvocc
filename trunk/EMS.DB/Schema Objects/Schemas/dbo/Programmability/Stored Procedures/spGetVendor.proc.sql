--exec [common].[uspGetLocation] NULL,'N'

-- =============================================
-- Author		: Rajen Saha
-- Create date	: 02-Dec-2012 
-- Description  :
-- =============================================
CREATE PROCEDURE [dbo].[spGetVendor]
(
	-- Add the parameters for the stored procedure here
	@VendorId				INT = 0,
	@SchName			VARCHAR(100) = '',
	@SchLoc 			VARCHAR(100) = '',
	@SortExpression		VARCHAR(10) = NULL,
	@SortDirection		VARCHAR(4) = NULL	
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @Sort VARCHAR(4)
	
	Create table #TempLocation
	(
		locId int,
		locname nvarchar(250)
	)
	
	Insert Into #TempLocation(locId,locname)
	Select pk_LocID,LocName from DSR.dbo.mstLocation
	
	If(@VendorId > 0)
	Begin
		SELECT	 vnd.fk_AddressID AS 'Id'
			,vnd.fk_CompanyID AS 'Company'
			,vnd.fk_LocationID AS 'Location'
			,vnd.AddrName AS 'Name'
			,vnd.AddrAddress AS 'Address'
			,vnd.AddrSalutation As 'Salutation'
			,vnd.AddrType As 'Type'
			,vnd.CFSCode
			,vnd.fk_terminalid As 'Terminal'
			
		FROM	dbo.mstAddress vnd	
				LEFT OUTER JOIN mstAddressType typ on vnd.AddrType = typ.pk_AddrTypeID
				LEFT OUTER JOIN #TempLocation loc on loc.locId = vnd.fk_LocationID
		Where vnd.fk_AddressID = @VendorId
	End
	Else
	Begin
			SELECT @Sort = CASE @SortDirection
						WHEN '' THEN 'ASC'
						ELSE @SortDirection
					END
					
			SELECT	 vnd.fk_AddressID AS 'Id'
					,vnd.fk_CompanyID AS 'Company'
					,loc.locname AS 'Location'
					,vnd.AddrName AS 'Name'
					,vnd.AddrAddress AS 'Address'
					,vnd.AddrSalutation As 'Salutation'
					,typ.AddrTypeDesc As 'Type'
					,vnd.CFSCode
					,vnd.fk_terminalid As 'Terminal'			
			FROM	dbo.mstAddress vnd	
					LEFT OUTER JOIN mstAddressType typ on vnd.AddrType = typ.pk_AddrTypeID
					LEFT OUTER JOIN #TempLocation loc on loc.locId = vnd.fk_LocationID
			
			WHERE	vnd.AddrActive = 1 
					AND			
					vnd.AddrName LIKE '%' + 
											CASE ISNULL(@SchName,'')
												WHEN '' THEN vnd.AddrName
												ELSE  @SchName	
											END + '%' 
					AND					
					loc.locname LIKE '%' + 
											CASE ISNULL(@SchLoc,'')
												WHEN '' THEN loc.locname
												ELSE  @SchLoc	
											END + '%'
					
			ORDER BY 
					CASE @SortDirection
						WHEN 'ASC' THEN
							CASE @SortExpression
								WHEN 'Type' THEN typ.AddrTypeDesc								
								WHEN 'Name' THEN vnd.AddrName
								WHEN 'Location' THEN loc.locname
								ELSE '1'
							END 
					END ASC,
					CASE @SortDirection
						WHEN 'DESC' THEN
							CASE @SortExpression
								WHEN 'Type' THEN typ.AddrTypeDesc								
								WHEN 'Name' THEN vnd.AddrName
								WHEN 'Location' THEN loc.locname
								ELSE '1'
							END 
					END DESC
	End
	
	drop table #TempLocation
END
