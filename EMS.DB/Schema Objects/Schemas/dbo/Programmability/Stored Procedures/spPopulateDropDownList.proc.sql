-- =============================================
-- Author:		<Rajen Saha>
-- Create date: <02/12/2012>
-- Description:	<This sp will populated all the dropdown throughout the application. @Number is a general number, which is unique>
-- =============================================
CREATE PROCEDURE [dbo].[spPopulateDropDownList]
	-- Add the parameters for the stored procedure here
	@Number int,
	@Filter int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	

    
	IF(@Number = 1) -- For Vendor Type
	Begin
		Select pk_AddrTypeID as Value,AddrTypeDesc As [Text] from mstAddressType order by pk_AddrTypeID ASC
	End
	
	ELSE IF(@Number = 2 ) -- For Location
	Begin
		Select pk_LocID as Value, LocName As [Text] from DSR.dbo.mstLocation
			Where Active = 'Y' and IsDeleted=0
	End
	
	ELSE IF(@Number = 3 ) -- For Terminal Code
	Begin
		If(@Filter > 0)
		Begin
			Select fk_TerminalID as Value,TerminalName As [Text] 
				from mstTerminal 
				Where fk_LocationID = @Filter
		End
		Else
		Begin
			Select fk_TerminalID as Value,TerminalName As [Text] 
				from mstTerminal 
		End
	End
	
	ELSE IF(@Number = 4 ) -- For Port
	Begin
		Select pk_PortID as Value, PortName + ' (' + PortCode + ')'  As [Text] from DSR.dbo.mstPort
			Where Active = 'Y' Order by PortName ASC
	End
	
	ELSE IF(@Number = 5 ) -- For ContainerSize
	Begin
		Select fk_SizeID as Value, Size  As [Text] from dbo.mstContainerSize
			Order by Size ASC
	End
	
END
