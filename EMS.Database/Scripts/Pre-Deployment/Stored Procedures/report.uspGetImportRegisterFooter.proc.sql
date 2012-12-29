USE [NVOCC]
GO

/****** Object:  StoredProcedure [report].[uspGetImportRegisterFooter]    Script Date: 12/29/2012 23:55:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 26-Dec-2012
-- Description	:
-- =============================================
CREATE PROCEDURE [report].[uspGetImportRegisterFooter] 
(
	-- Add the parameters for the stored procedure here
	@NVOCCId	INT,
	@LocId		INT,
	@VoyageId	BIGINT,
	@VesselId	BIGINT
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT	LOC.LocAbbr AS 'Location',
			NVOCC.ProspectName AS 'Line',
			IBH.ImpLineBLNo,
			IBF.CntrNo,
			IBF.CntrSize,
			CT.ContainerAbbr AS 'ContainerType',
			IBF.SealNo,
			IBF.CargoWtTon AS 'PayLoad',
			IBH.NumberPackage,
			NULL AS 'PackageUnit',
			RsStatus,
			IBH.GoodDescription,
			PL.PortName AS 'PortLoading',
			PD.PortName AS 'PortDischarge',
			PL.PortName AS 'FinalDestination'
	FROM	[dbo].[ImpBLHeader] IBH
			INNER JOIN [dbo].[ImpBLFooter] IBF
				ON IBH.pk_BLID = IBF.fk_BLID
			INNER JOIN [DSR].[dbo].[mstLocation] LOC 
				ON IBH.fk_LocationID = LOC.pk_LocID
			INNER JOIN [DSR].[dbo].[mstProspectFor] NVOCC
				ON IBH.fk_NVOCCID = NVOCC.pk_ProspectID
			INNER JOIN dbo.mstContainerType CT
				ON IBF.fk_ContainerTypeID = CT.pk_ContainerTypeID
			INNER JOIN [DSR].[dbo].[mstPort] PL
				ON IBH.fk_PortLoading = PL.pk_PortID
			INNER JOIN [DSR].[dbo].[mstPort] PD
				ON IBH.fk_PortDischarge = PD.pk_PortID
			INNER JOIN [DSR].[dbo].[mstPort] FD
				ON IBH.fk_FinalDestination = FD.pk_PortID
END

GO


