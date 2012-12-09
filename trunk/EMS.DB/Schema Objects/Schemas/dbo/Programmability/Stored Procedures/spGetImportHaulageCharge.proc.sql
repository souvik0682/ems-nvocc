--exec [common].[uspGetLocation] NULL,'N'

-- =============================================
-- Author		: Rajen Saha
-- Create date	: 07-Dec-2012 
-- Description  :
-- =============================================
CREATE PROCEDURE [dbo].[spGetImportHaulageCharge]
(
	-- Add the parameters for the stored procedure here
	@HaulageChgID				INT = 0,
	@SchLocFrom			VARCHAR(100) = NULL,
	@SchLocTo 			VARCHAR(100) = NULL,
	@SchContSize 		VARCHAR(5) = NULL,
	@SortExpression		VARCHAR(50) = NULL,
	@SortDirection		VARCHAR(4) = NULL	
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @Sort VARCHAR(4), @Query Nvarchar(MAX)
	
	Create table #TempLocation
	(
		locId int,		
		locname nvarchar(250),
		PortCode varchar(6)
	)
	Create table #Temp
	(
		HaulageChgID int,
		ContainerSize nvarchar(2),
		HaulageRate decimal(10,2),
		HaulageStatus bit,
		WeightFrom decimal(10,2),
		WeightTo decimal(10,2),
		LocationFrom nvarchar(100),
		LFCode nvarchar(6),
		LocationTo nvarchar(100),
		LTCode nvarchar(6)
	)
	
	Insert Into #TempLocation(locId,locname,PortCode)
	--Select pk_LocID,LocName from DSR.dbo.mstLocation
	Select PM.pk_PortID,PM.PortName,PM.PortCode  From DSR.dbo.mstPort PM
	
	
	
	If(@HaulageChgID > 0)
	Begin		
		--Insert Into #Temp(ContainerSize,HaulageRate,HaulageStatus,HaulageChgID,WeightFrom,WeightTo,LocationFrom,LocationTo,LFCode,LTCode)
		SELECT	HCM.ContainerSize AS 'ContainerSize'
				,HCM.HaulageRate AS 'HaulageRate'
				,HCM.HaulageStatus AS 'HaulageStatus'
				,HCM.pk_HaulageChgID AS 'HaulageChgID'
				,HCM.WeightFrom AS 'WeightFrom'
				,HCM.WeightTo As 'WeightTo'
				,HCM.fk_LocationFrom AS 'LocationFrom'
				,HCM.fk_LocationTo AS 'LocationTo'
				,'' As LFCode
				,'' As LTCode
			
		FROM	dbo.mstHaulageCharge HCM					
		Where HCM.pk_HaulageChgID = @HaulageChgID
		
	End
	Else
	Begin			
			SELECT @Sort = CASE @SortDirection
						WHEN '' THEN 'ASC'
						ELSE @SortDirection
					END
					
			Insert Into #Temp(ContainerSize,HaulageRate,HaulageStatus,HaulageChgID,WeightFrom,WeightTo,LocationFrom,LFCode,LocationTo,LTCode)		
			SELECT	HCM.ContainerSize AS 'ContainerSize'
					,HCM.HaulageRate AS 'HaulageRate'
					,HCM.HaulageStatus AS 'HaulageStatus'
					,HCM.pk_HaulageChgID AS 'HaulageChgID'
					,HCM.WeightFrom AS 'WeightFrom'
					,HCM.WeightTo As 'WeightTo'
					,locF.locname AS 'LocationFrom'
					,locF.PortCode As 'FCode'
					,locT.locname AS 'LocationTo'
					,locT.PortCode As 'TCode'
						
			FROM	dbo.mstHaulageCharge HCM						
					LEFT OUTER JOIN #TempLocation locF on locF.locId = HCM.fk_LocationFrom
					LEFT OUTER JOIN #TempLocation locT on locT.locId = HCM.fk_LocationTo					
			WHERE	HCM.HaulageStatus = 1		
					
			Set @Query = 'Select * From #Temp where 1 = 1 '
			
			If(ISNULL(@SchLocFrom,'') <> '')
			BEGIN
				Set @Query = @Query + ' and LocationFrom LIKE ''%' + @SchLocFrom + '%'''
			END
			
			If(ISNULL(@SchLocTo,'') <> '')
			BEGIN
				Set @Query = @Query + ' and LocationTo LIKE ''%' + @SchLocTo + '%'''
			END
			
			IF(ISNULL(@SchContSize,'') <> '')
			BEGIN
				Set @Query = @Query + ' and ContainerSize =''' + @SchContSize + ''''
			END
			
			Set @Query = @Query + ' Order By ' + @SortExpression + ' ' + @SortDirection
			
			PRINT @Query
			EXEC sp_ExecuteSQL @Query
			
	End
	
	drop table #Temp
	drop table #TempLocation
END
