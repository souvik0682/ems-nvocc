-- =============================================
-- Author:		Rajen Saha
-- Create date: 5/12/2012
-- Description:	Deletion of Vendor
-- =============================================
CREATE PROCEDURE [dbo].[spDeleteImportHaulageChrg]
	-- Add the parameters for the stored procedure here
	@HaulageChgID Int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update mstHaulageCharge Set HaulageStatus = 0 where pk_HaulageChgID = @HaulageChgID
END
