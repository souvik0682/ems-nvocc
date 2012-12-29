USE [NVOCC]
GO

/****** Object:  StoredProcedure [report].[uspGetImportRegisterHeader]    Script Date: 12/29/2012 23:54:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date	: 26-Dec-2012
-- Description	:
-- =============================================
CREATE PROCEDURE [report].[uspGetImportRegisterHeader] 
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
			IBH.ItemLineNo,
			IBH.ImpLineBLNo,
			IBH.ImpLineBLDate,
			IBH.NoofTEU,
			IBH.NoofFEU,
			PL.PortName AS 'PortLoading',
			PD.PortName AS 'PortDischarge',
			PL.PortName AS 'FinalDestination',
			NULL AS 'DischargeDate', --not found
			IBH.IGMBLNumber,
			IBH.GrossWeight,
			IBH.GoodDescription,
			IBH.NumberPackage,
			NULL AS 'PackageUnit',
			RsStatus,
			PGR_FreeDays,
			NULL AS 'AddressCFS',
			NULL AS 'ICD',--not found
			TPBondNo,
			IBH.CACode,
			IBH.CargoMovement,
			IBH.ConsigneeInformation,
			IBH.NotifyPartyInformation,
			IBH.MarksNumbers
	FROM	[dbo].[ImpBLHeader] IBH
			INNER JOIN [DSR].[dbo].[mstLocation] LOC 
				ON IBH.fk_LocationID = LOC.pk_LocID
			INNER JOIN [DSR].[dbo].[mstProspectFor] NVOCC
				ON IBH.fk_NVOCCID = NVOCC.pk_ProspectID	
			INNER JOIN [DSR].[dbo].[mstPort] PL
				ON IBH.fk_PortLoading = PL.pk_PortID
			INNER JOIN [DSR].[dbo].[mstPort] PD
				ON IBH.fk_PortDischarge = PD.pk_PortID
			INNER JOIN [DSR].[dbo].[mstPort] FD
				ON IBH.fk_FinalDestination = FD.pk_PortID				
END

GO


