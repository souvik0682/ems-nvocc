/****** Object:  StoredProcedure [common].[uspSaveLocation]    Script Date: 12/05/2012 22:10:02 ******/

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 03-Dec-2012 
-- Description  :
-- =============================================
CREATE PROCEDURE [common].[uspSaveLocation]
(
	-- Add the parameters for the stored procedure here
	@LocId				INT,
	@PGRFreeDays		INT,
	@CanFooter			VARCHAR(300),
	@SlotFooter			VARCHAR(300),
	@CartingFooter		VARCHAR(300),
	@PickUpFooter		VARCHAR(300),
	@CustomHouseCode	CHAR(6),
	@GatewayPort		CHAR(6),
	@ICEGateLoginD		VARCHAR(20),
	@PCSLoginID			VARCHAR(8),
	@ISO20				CHAR(4),
	@ISO40				CHAR(4),
	@ModifiedBy			INT
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
   
	UPDATE	[DSR].[dbo].[mstLocation]
	SET		 PGRFreeDays = @PGRFreeDays
			,CanFooter = @CanFooter
			,SlotFooter = @SlotFooter
			,CartingFooter = @CartingFooter
			,PickUpFooter = @PickUpFooter
			,CustomHouseCode = @CustomHouseCode
			,GatewayPort = @GatewayPort
			,ICEGateLoginD = @ICEGateLoginD
			,PCSLoginID = @PCSLoginID
			,ISO20 = @ISO20
			,ISO40 = @ISO40
			--,fk_UserLastEdited = @ModifiedBy
			,EditedOn = GETUTCDATE()
	WHERE	pk_LocID = @LocId
END

GO


