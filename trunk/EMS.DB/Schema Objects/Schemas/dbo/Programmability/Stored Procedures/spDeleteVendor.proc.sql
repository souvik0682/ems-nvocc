-- =============================================
-- Author:		Rajen Saha
-- Create date: 5/12/2012
-- Description:	Deletion of Vendor
-- =============================================
CREATE PROCEDURE spDeleteVendor
	-- Add the parameters for the stored procedure here
	@VendorId Int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update mstAddress Set AddrActive = 0 where fk_AddressID = @VendorId
END
