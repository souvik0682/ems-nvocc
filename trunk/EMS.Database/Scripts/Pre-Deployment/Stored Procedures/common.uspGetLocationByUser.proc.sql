/****** Object:  StoredProcedure [common].[uspGetLocationByUser]    Script Date: 12/10/2012 21:08:23 ******/

--exec [common].[uspGetLocationByUser] 2

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 30-Jun-2012 
-- Description  :
-- =============================================
CREATE PROCEDURE [common].[uspGetLocationByUser]
(
	-- Add the parameters for the stored procedure here
	@UserId				INT
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @LocId	INT
	DECLARE @RoleId	INT
	
	SELECT	 @LocId = fk_LocID
			,@RoleId = fk_RoleID
	FROM	[DSR].[dbo].[mstUser]
	WHERE	pk_UserID = @UserId
	
	IF @RoleId = 1 OR @RoleId = 3 --For ADMIN and MANAGEMENT
	BEGIN				
		SELECT	 loc.pk_LocID AS 'Id'
				,loc.LocName AS 'Name'
				,loc.LocAddress AS 'Address'
				,loc.LocCity AS 'City'
				,loc.LocPin AS 'Pin'
				,loc.LocAbbr
				,loc.LocPhone
				,loc.fk_Manager 'ManagerId'
				,NULL AS 'ManagerName'
				,loc.PGRFreeDays
				,loc.CANFooter
				,loc.CartingFooter
				,loc.SlotFooter
				,loc.PickUpFooter
				,loc.CustomHouseCode
				,loc.GatewayPort
				,loc.ICEGateLoginD
				,loc.PCSLoginID
				,loc.ISO20
				,loc.ISO40				
				,loc.Active
		FROM	[DSR].[dbo].[mstLocation] loc
		WHERE	loc.Active = 'Y'
		AND		loc.IsDeleted = 0
		ORDER BY loc.LocName
	END
	ELSE IF @RoleId = 2 --For MANAGERS
	BEGIN
		SELECT	 loc.pk_LocID AS 'Id'
				,loc.LocName AS 'Name'
				,loc.LocAddress AS 'Address'
				,loc.LocCity AS 'City'
				,loc.LocPin AS 'Pin'
				,loc.LocAbbr
				,loc.LocPhone
				,loc.fk_Manager 'ManagerId'
				,NULL AS 'ManagerName'
				,loc.PGRFreeDays
				,loc.CANFooter
				,loc.CartingFooter
				,loc.SlotFooter
				,loc.PickUpFooter
				,loc.CustomHouseCode
				,loc.GatewayPort
				,loc.ICEGateLoginD
				,loc.PCSLoginID
				,loc.ISO20
				,loc.ISO40				
				,loc.Active
		FROM	[DSR].[dbo].[mstLocation] loc
		WHERE	loc.pk_LocID = @LocId
		AND		loc.Active = 'Y'
		AND		loc.IsDeleted = 0
		ORDER BY loc.LocName	
	END
	ELSE IF @RoleId = 4 --For SALES EXECUTIVE
	BEGIN
		SELECT	 loc.pk_LocID AS 'Id'
				,loc.LocName AS 'Name'
				,loc.LocAddress AS 'Address'
				,loc.LocCity AS 'City'
				,loc.LocPin AS 'Pin'
				,loc.LocAbbr
				,loc.LocPhone
				,loc.fk_Manager 'ManagerId'
				,NULL AS 'ManagerName'
				,loc.PGRFreeDays
				,loc.CANFooter
				,loc.CartingFooter
				,loc.SlotFooter
				,loc.PickUpFooter
				,loc.CustomHouseCode
				,loc.GatewayPort
				,loc.ICEGateLoginD
				,loc.PCSLoginID
				,loc.ISO20
				,loc.ISO40				
				,loc.Active
		FROM	[DSR].[dbo].[mstLocation] loc
		WHERE	loc.pk_LocID = @LocId
		AND		loc.Active = 'Y'
		AND		loc.IsDeleted = 0
		ORDER BY loc.LocName	
	END
END

GO


