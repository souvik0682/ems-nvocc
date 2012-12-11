/*
Deployment script for EMS.DB
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "EMS.DB"
:setvar DefaultDataPath ""
:setvar DefaultLogPath ""

GO
USE [master]

GO
:on error exit
GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL
    AND DATABASEPROPERTYEX(N'$(DatabaseName)','Status') <> N'ONLINE')
BEGIN
    RAISERROR(N'The state of the target database, %s, is not set to ONLINE. To deploy to this database, its state must be set to ONLINE.', 16, 127,N'$(DatabaseName)') WITH NOWAIT
    RETURN
END

GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [NVOCC], FILENAME = '$(DefaultDataPath)$(DatabaseName).mdf', FILEGROWTH = 1024 KB)
    LOG ON (NAME = [NVOCC_log], FILENAME = '$(DefaultLogPath)$(DatabaseName)_log.ldf', MAXSIZE = 2097152 MB, FILEGROWTH = 10 %) COLLATE SQL_Latin1_General_CP1_CI_AS
GO
EXECUTE sp_dbcmptlevel [$(DatabaseName)], 100;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
USE [$(DatabaseName)]

GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

GO
PRINT N'Creating [dbo].[eqpContainer]...';


GO
CREATE TABLE [dbo].[eqpContainer] (
    [pk_ContainerID]    BIGINT          IDENTITY (1, 1) NOT NULL,
    [fk_CompanyID]      INT             NULL,
    [ContainerNo]       VARCHAR (11)    NOT NULL,
    [fk_BLID]           BIGINT          NOT NULL,
    [fk_ImpVesselID]    BIGINT          NOT NULL,
    [fk_ImpVoyageID]    BIGINT          NOT NULL,
    [fk_LocationID]     INT             NULL,
    [GLDate]            DATETIME        NULL,
    [fk_ICDLocation]    INT             NULL,
    [ICDOutDate]        DATE            NULL,
    [ContainerSize]     CHAR (1)        NULL,
    [fk_ContainerType]  INT             NULL,
    [ImportBerth]       CHAR (3)        NULL,
    [Comodity]          VARCHAR (20)    NULL,
    [ContHTFT]          INT             NULL,
    [ContHTIN]          INT             NULL,
    [GrossWt]           DECIMAL (14, 4) NULL,
    [TareWt]            DECIMAL (14, 4) NULL,
    [SealNo]            VARCHAR (15)    NULL,
    [Package]           INT             NULL,
    [CagEnc]            VARCHAR (10)    NULL,
    [Status]            CHAR (3)        NULL,
    [ISO]               CHAR (1)        NULL,
    [SOC]               CHAR (1)        NULL,
    [DestuffDate]       DATETIME        NULL,
    [PassOutDate]       DATETIME        NULL,
    [PortDetention]     DECIMAL (12, 2) NULL,
    [Waiver]            DECIMAL (6, 2)  NULL,
    [YardReturnDate]    DATETIME        NULL,
    [Detention]         DECIMAL (12, 2) NULL,
    [DetentionWith]     DECIMAL (12, 2) NULL,
    [DetentionWO]       DECIMAL (12, 2) NULL,
    [DetentionPercent]  DECIMAL (6, 2)  NULL,
    [ISSShpr]           DATE            NULL,
    [RemDock]           DATE            NULL,
    [ExpectedDate]      DATE            NULL,
    [CFSDestuff]        CHAR (1)        NULL,
    [LeftBehind]        CHAR (1)        NULL,
    [OnHireDate]        DATE            NULL,
    [OnhireRef]         VARCHAR (20)    NULL,
    [OffHireDate]       DATE            NULL,
    [OffHireReference]  VARCHAR (30)    NULL,
    [ToSkip]            CHAR (1)        NULL,
    [EBLNo]             VARCHAR (20)    NULL,
    [Booking]           VARCHAR (10)    NULL,
    [ExpBerth]          CHAR (3)        NULL,
    [fk_ExpVesselID]    BIGINT          NULL,
    [fk_ExpVoyageID]    BIGINT          NULL,
    [SOB]               DATE            NULL,
    [Active]            CHAR (1)        NULL,
    [XchangeRate]       DECIMAL (12, 6) NULL,
    [fk_DeliveryPortID] BIGINT          NULL,
    [NOMVsl]            VARCHAR (20)    NULL,
    [fk_LoadPortID]     BIGINT          NULL,
    [RepCharges]        DECIMAL (12, 2) NULL,
    [RepTax]            DECIMAL (12, 2) NULL,
    [PRDetention]       DECIMAL (12, 2) NULL,
    [Damage]            CHAR (1)        NULL,
    [OnHold]            VARCHAR (5)     NULL,
    [PickNo]            VARCHAR (6)     NULL,
    [PickDate]          DATE            NULL,
    [PickValid]         DATE            NULL,
    [LGNo]              VARCHAR (20)    NULL,
    [PortBill]          VARCHAR (10)    NULL,
    [PortGLD]           DATE            NULL,
    [PortOutDate]       DATE            NULL,
    [PortXchangeRate]   DECIMAL (12, 6) NULL,
    [PortUSD]           DECIMAL (12, 2) NULL,
    [PortCharge1]       DECIMAL (12, 2) NULL,
    [PortCharge2]       DECIMAL (12, 2) NULL,
    [LockCCB]           BIT             NULL,
    [fk_PortLoadingID]  BIGINT          NULL,
    [fk_UserAdded]      INT             NOT NULL,
    [fk_UserLastEdited] INT             NULL,
    [AddedOn]           DATETIME        NOT NULL,
    [EditedOn]          DATETIME        NULL
);


GO
PRINT N'Creating PK_eqpContainer...';


GO
ALTER TABLE [dbo].[eqpContainer]
    ADD CONSTRAINT [PK_eqpContainer] PRIMARY KEY CLUSTERED ([pk_ContainerID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[eqpContainerOld]...';


GO
CREATE TABLE [dbo].[eqpContainerOld] (
    [fk_ContainerID]    BIGINT          IDENTITY (1, 1) NOT NULL,
    [ContainerNo]       VARCHAR (11)    NOT NULL,
    [fk_BLID]           BIGINT          NOT NULL,
    [fk_ImpVesselID]    BIGINT          NOT NULL,
    [fk_ImpVoyageID]    BIGINT          NOT NULL,
    [fk_LocationID]     INT             NULL,
    [GLDate]            DATETIME        NULL,
    [fk_ICDLocation]    INT             NULL,
    [ICDOutDate]        DATE            NULL,
    [ContainerSize]     CHAR (1)        NULL,
    [fk_ContainerType]  INT             NULL,
    [ImportBerth]       CHAR (3)        NULL,
    [Comodity]          VARCHAR (20)    NULL,
    [ContHTFT]          INT             NULL,
    [ContHTIN]          INT             NULL,
    [GrossWt]           DECIMAL (14, 4) NULL,
    [TareWt]            DECIMAL (14, 4) NULL,
    [SealNo]            VARCHAR (15)    NULL,
    [Package]           INT             NULL,
    [CagEnc]            VARCHAR (10)    NULL,
    [Status]            CHAR (3)        NULL,
    [ISO]               CHAR (1)        NULL,
    [SOC]               CHAR (1)        NULL,
    [DestuffDate]       DATETIME        NULL,
    [PassOutDate]       DATETIME        NULL,
    [PortDetention]     DECIMAL (12, 2) NULL,
    [Waiver]            DECIMAL (6, 2)  NULL,
    [YardReturnDate]    DATETIME        NULL,
    [Detention]         DECIMAL (12, 2) NULL,
    [DetentionWith]     DECIMAL (12, 2) NULL,
    [DetentionWO]       DECIMAL (12, 2) NULL,
    [DetentionPercent]  DECIMAL (6, 2)  NULL,
    [ISSShpr]           DATE            NULL,
    [RemDock]           DATE            NULL,
    [ExpectedDate]      DATE            NULL,
    [CFSDestuff]        CHAR (1)        NULL,
    [LeftBehind]        CHAR (1)        NULL,
    [OnHireDate]        DATE            NULL,
    [OnhireRef]         VARCHAR (20)    NULL,
    [OffHireDate]       DATE            NULL,
    [OffHireReference]  VARCHAR (30)    NULL,
    [ToSkip]            CHAR (1)        NULL,
    [EBLNo]             VARCHAR (20)    NULL,
    [Booking]           VARCHAR (10)    NULL,
    [ExpBerth]          CHAR (3)        NULL,
    [fk_ExpVesselID]    BIGINT          NULL,
    [fk_ExpVoyageID]    BIGINT          NULL,
    [SOB]               DATE            NULL,
    [Active]            CHAR (1)        NULL,
    [XchangeRate]       DECIMAL (12, 6) NULL,
    [fk_DeliveryPortID] BIGINT          NULL,
    [NOMVsl]            VARCHAR (20)    NULL,
    [fk_LoadPortID]     BIGINT          NULL,
    [RepCharges]        DECIMAL (12, 2) NULL,
    [RepTax]            DECIMAL (12, 2) NULL,
    [PRDetention]       DECIMAL (12, 2) NULL,
    [Damage]            CHAR (1)        NULL,
    [OnHold]            VARCHAR (5)     NULL,
    [PickNo]            VARCHAR (6)     NULL,
    [PickDate]          DATE            NULL,
    [PickValid]         DATE            NULL,
    [LGNo]              VARCHAR (20)    NULL,
    [PortBill]          VARCHAR (10)    NULL,
    [PortGLD]           DATE            NULL,
    [PortOutDate]       DATE            NULL,
    [PortXchangeRate]   DECIMAL (12, 6) NULL,
    [PortUSD]           DECIMAL (12, 2) NULL,
    [PortCharge1]       DECIMAL (12, 2) NULL,
    [PortCharge2]       DECIMAL (12, 2) NULL,
    [LockCCB]           BIT             NULL,
    [fk_PortLoadingID]  BIGINT          NULL
);


GO
PRINT N'Creating PK_trnContainer...';


GO
ALTER TABLE [dbo].[eqpContainerOld]
    ADD CONSTRAINT [PK_trnContainer] PRIMARY KEY CLUSTERED ([fk_ContainerID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[eqpContainerTransaction]...';


GO
CREATE TABLE [dbo].[eqpContainerTransaction] (
    [pk_ContainerID]        BIGINT     IDENTITY (1, 1) NOT NULL,
    [fk_CompanyID]          INT        NOT NULL,
    [fk_LocationID]         NCHAR (10) NOT NULL,
    [fk_MovementGroup]      BIGINT     NOT NULL,
    [fk_MovementID]         INT        NOT NULL,
    [MovementDate]          DATE       NOT NULL,
    [fk_StockLocationID]    INT        NOT NULL,
    [fk_TransferLocationID] INT        NULL,
    [fk_AddressID]          INT        NULL,
    [fk_UserAdded]          INT        NOT NULL,
    [fk_UserLastEdited]     INT        NULL,
    [AddedOn]               DATETIME   NOT NULL,
    [EditedOn]              DATETIME   NULL
);


GO
PRINT N'Creating PK_eqpTransaction...';


GO
ALTER TABLE [dbo].[eqpContainerTransaction]
    ADD CONSTRAINT [PK_eqpTransaction] PRIMARY KEY CLUSTERED ([pk_ContainerID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[eqpOnHire]...';


GO
CREATE TABLE [dbo].[eqpOnHire] (
    [pk_OnHireContID]   BIGINT       IDENTITY (1, 1) NOT NULL,
    [fk_CompanyID]      INT          NULL,
    [fk_LocationID]     INT          NOT NULL,
    [fk_OnHireID]       BIGINT       NULL,
    [fk_ContainerID]    BIGINT       NULL,
    [fk_VesselID]       BIGINT       NULL,
    [fk_VoyageID]       BIGINT       NULL,
    [LinePrefix]        VARCHAR (20) NULL,
    [LGNo]              VARCHAR (40) NULL,
    [IGMNo]             BIGINT       NULL,
    [IGMDate]           DATE         NULL,
    [ActualOnHireDate]  DATE         NULL,
    [fk_MovementID]     INT          NULL,
    [StatusDate]        DATE         NULL,
    [fk_UserAdded]      INT          NOT NULL,
    [fk_UserLastEdited] INT          NULL,
    [AddedOn]           DATETIME     NOT NULL,
    [EditedOn]          DATETIME     NULL
);


GO
PRINT N'Creating PK_EqpOnHireContainer...';


GO
ALTER TABLE [dbo].[eqpOnHire]
    ADD CONSTRAINT [PK_EqpOnHireContainer] PRIMARY KEY CLUSTERED ([pk_OnHireContID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[eqpOnHire].[IX_eqpOnHireContainer]...';


GO
CREATE NONCLUSTERED INDEX [IX_eqpOnHireContainer]
    ON [dbo].[eqpOnHire]([fk_ContainerID] ASC, [pk_OnHireContID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];


GO
PRINT N'Creating [dbo].[eqpOnHireContainers]...';


GO
CREATE TABLE [dbo].[eqpOnHireContainers] (
    [fk_NVOCCID]           INT          NULL,
    [pk_OnHireContainerID] BIGINT       IDENTITY (1, 1) NOT NULL,
    [fk_OnHireID]          BIGINT       NULL,
    [OnHireReference]      VARCHAR (20) NULL,
    [OnHireDate]           DATE         NULL,
    [ValidTill]            DATE         NULL,
    [fk_ReturnPortID]      BIGINT       NULL,
    [fk_ContainerTypeID]   INT          NULL,
    [Total20]              INT          NULL,
    [Total40]              INT          NULL,
    [fk_UserAdded]         INT          NOT NULL,
    [fk_UserLastEdited]    INT          NULL,
    [AddedOn]              DATETIME     NOT NULL,
    [EditedOn]             DATETIME     NULL
);


GO
PRINT N'Creating PK_eqpOnHireDetails...';


GO
ALTER TABLE [dbo].[eqpOnHireContainers]
    ADD CONSTRAINT [PK_eqpOnHireDetails] PRIMARY KEY CLUSTERED ([pk_OnHireContainerID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[eqpPickUp]...';


GO
CREATE TABLE [dbo].[eqpPickUp] (
    [fk_PickUpID]       BIGINT       NOT NULL,
    [fk_CompanyID]      INT          NOT NULL,
    [fk_LocationID]     INT          NOT NULL,
    [fk_NVOCCID]        INT          NOT NULL,
    [fk_FinYearID]      INT          NOT NULL,
    [PickUpNo]          VARCHAR (60) NULL,
    [IssueDate]         DATE         NULL,
    [ValidTill]         DATE         NULL,
    [fk_RelatedRefNo]   INT          NULL,
    [fk_BookingRef]     VARCHAR (50) NULL,
    [fk_EmptyYard]      INT          NULL,
    [Feeder]            VARCHAR (40) NULL,
    [Cargo]             VARCHAR (40) NULL,
    [PickActive]        BIT          NULL,
    [fk_UserAdded]      INT          NOT NULL,
    [fk_UserLastEdited] INT          NULL,
    [AddedOn]           DATETIME     NOT NULL,
    [EditedOn]          DATETIME     NULL
);


GO
PRINT N'Creating [dbo].[eqpRepairing]...';


GO
CREATE TABLE [dbo].[eqpRepairing] (
    [pk_RepairID]       BIGINT          NULL,
    [fk_ContainerID]    INT             NULL,
    [RepMaterialEst]    DECIMAL (12, 2) NULL,
    [RepLabourEst]      DECIMAL (12, 2) NULL,
    [RepMaterialAppr]   DECIMAL (12, 2) NULL,
    [RepLabourAppr]     DECIMAL (12, 2) NULL,
    [RepMaterialBilled] DECIMAL (12, 2) NULL,
    [RepLabourBilled]   DECIMAL (12, 2) NULL,
    [fk_UserAdded]      INT             NOT NULL,
    [fk_UserLastEdited] INT             NULL,
    [AddedOn]           DATETIME        NOT NULL,
    [EditedOn]          DATETIME        NULL
);


GO
PRINT N'Creating [dbo].[expBLCharges]...';


GO
CREATE TABLE [dbo].[expBLCharges] (
    [pk_ExpBLChgID] BIGINT          IDENTITY (1, 1) NOT NULL,
    [fk_ExpBLID]    BIGINT          NULL,
    [fk_ChargeID]   INT             NULL,
    [RatePerTEU]    DECIMAL (12, 2) NULL,
    [RatePerFEU]    DECIMAL (12, 2) NULL,
    [Currency]      CHAR (3)        NULL,
    [XChangeRate]   DECIMAL (12, 6) NULL,
    [ChgTotal]      DECIMAL (12, 2) NULL,
    [ToCalc]        CHAR (1)        NULL,
    [SortOrder]     INT             NULL,
    [CollPrep]      CHAR (1)        NULL
);


GO
PRINT N'Creating PK_empBLDetails...';


GO
ALTER TABLE [dbo].[expBLCharges]
    ADD CONSTRAINT [PK_empBLDetails] PRIMARY KEY CLUSTERED ([pk_ExpBLChgID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[expBLContainer]...';


GO
CREATE TABLE [dbo].[expBLContainer] (
    [pk_ExpBLContainerID] BIGINT          IDENTITY (1, 1) NOT NULL,
    [fk_ExpBLID]          BIGINT          NULL,
    [fj_ContainerID]      BIGINT          NULL,
    [SealNo]              VARCHAR (15)    NULL,
    [GrossWeight]         DECIMAL (12, 3) NULL,
    [TareWeight]          DECIMAL (12, 3) NULL,
    [Package]             INT             NULL
);


GO
PRINT N'Creating PK_expBLContainer...';


GO
ALTER TABLE [dbo].[expBLContainer]
    ADD CONSTRAINT [PK_expBLContainer] PRIMARY KEY CLUSTERED ([pk_ExpBLContainerID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[expBLHeader]...';


GO
CREATE TABLE [dbo].[expBLHeader] (
    [pk_ExpBLID]        BIGINT          NULL,
    [fk_LocationID]     INT             NULL,
    [fk_NVOCCID]        BIGINT          NULL,
    [fk_BookingID]      BIGINT          NULL,
    [ExpBLNo]           VARCHAR (60)    NULL,
    [ExpBLDate]         DATETIME        NULL,
    [fk_PortLoading]    BIGINT          NULL,
    [POLDesc]           VARCHAR (25)    NULL,
    [fk_PortDest]       BIGINT          NULL,
    [PODDesc]           VARCHAR (25)    NULL,
    [fk_FinalDest]      BIGINT          NULL,
    [FinalDesc]         VARCHAR (25)    NULL,
    [fk_PLRecp]         BIGINT          NULL,
    [PLRecpDesc]        VARCHAR (25)    NULL,
    [fk_BLIssue]        BIGINT          NULL,
    [BLIssueDesc]       VARCHAR (25)    NULL,
    [Shipper]           VARCHAR (300)   NULL,
    [Consignee]         VARCHAR (300)   NULL,
    [Notify]            VARCHAR (300)   NULL,
    [Forwarder]         VARCHAR (300)   NULL,
    [Broker]            VARCHAR (300)   NULL,
    [Brokerage]         CHAR (1)        NULL,
    [BrokeragePercent]  DECIMAL (5, 2)  NULL,
    [fk_VesselID]       BIGINT          NULL,
    [fk_VoyageID]       BIGINT          NULL,
    [FeederFreight]     DECIMAL (12, 2) NULL,
    [Slot]              VARCHAR (10)    NULL,
    [MoveOriginal]      VARCHAR (3)     NULL,
    [MoveDest]          VARCHAR (3)     NULL,
    [FinalML]           VARCHAR (3)     NULL,
    [FinalVoyage]       VARCHAR (10)    NULL,
    [FinalTSP]          DATE            NULL,
    [FinalPOD]          DATE            NULL,
    [FinalFNL]          DATE            NULL,
    [NumberofBL]        INT             NULL,
    [TEU]               INT             NULL,
    [FEU]               INT             NULL,
    [FreightPPorCC]     CHAR (1)        NULL,
    [CreditDays]        INT             NULL,
    [Marks]             VARCHAR (300)   NULL,
    [Package]           VARCHAR (300)   NULL,
    [Goods]             VARCHAR (300)   NULL,
    [GrossWeight]       DECIMAL (12, 2) NULL,
    [NetWeight]         DECIMAL (12, 2) NULL,
    [CBM]               DECIMAL (12, 2) NULL,
    [Agent]             VARCHAR (300)   NULL,
    [OriginalBLs]       INT             NULL,
    [Comodity]          VARCHAR (25)    NULL,
    [Hazardous]         BIT             NULL,
    [Technical]         VARCHAR (60)    NULL,
    [UNNo]              VARCHAR (5)     NULL,
    [IMDG]              VARCHAR (4)     NULL,
    [Flash]             VARCHAR (6)     NULL,
    [PackCode]          CHAR (1)        NULL,
    [ShippingBill]      VARCHAR (10)    NULL,
    [ShippingBillDate]  DATE            NULL,
    [PartBL]            CHAR (1)        NULL,
    [fk_UserAdded]      INT             NOT NULL,
    [fk_UserLastEdited] INT             NULL,
    [AddedOn]           DATETIME        NOT NULL,
    [EditedOn]          DATETIME        NULL
);


GO
PRINT N'Creating [dbo].[expBooking]...';


GO
CREATE TABLE [dbo].[expBooking] (
    [pk_BookingID]       BIGINT          NULL,
    [fk_LocationID]      INT             NULL,
    [fk_NVOCCID]         INT             NULL,
    [BookingNo]          VARCHAR (40)    NULL,
    [BookingDate]        DATE            NULL,
    [fk_ExpBLID]         BIGINT          NULL,
    [fk_FinyearID]       INT             NULL,
    [fk_LoadingPortID]   BIGINT          NULL,
    [fk_DestPortID]      BIGINT          NULL,
    [fk_FinalDestID]     BIGINT          NULL,
    [Shipper]            VARCHAR (300)   NULL,
    [Forwarder]          VARCHAR (300)   NULL,
    [Broker]             VARCHAR (300)   NULL,
    [Brokerage]          CHAR (1)        NULL,
    [BrokeragePercent]   DECIMAL (6, 2)  NULL,
    [fk_VesselID]        BIGINT          NULL,
    [fk_VoyageID]        BIGINT          NULL,
    [FeederFreight]      DECIMAL (12, 2) NULL,
    [Slot]               VARCHAR (10)    NULL,
    [fk_MoveOriginalID]  INT             NULL,
    [fk_MoveDestID]      INT             NULL,
    [MainLineVesselPlan] VARCHAR (60)    NULL,
    [MainLiveVoyagePlan] VARCHAR (20)    NULL,
    [ETDTSP]             DATE            NULL,
    [ETAPOD]             DATE            NULL,
    [ETAFinal]           DATE            NULL,
    [FinalML]            VARCHAR (24)    NULL,
    [FinalVoyage]        VARCHAR (10)    NULL,
    [FinalTSP]           DATE            NULL,
    [FinalPOD]           DATE            NULL,
    [FinalFNL]           DATE            NULL,
    [OriginalBLs]        INT             NULL,
    [TotalTEU]           INT             NULL,
    [TotalFEU]           INT             NULL,
    [FreightCorP]        CHAR (1)        NULL,
    [CreditDays]         INT             NULL,
    [Notes]              VARCHAR (300)   NULL,
    [PickNo]             VARCHAR (60)    NULL,
    [BrokeragePerTEU]    DECIMAL (12, 2) NULL,
    [BrokeragePerFEU]    DECIMAL (12, 2) NULL,
    [RefundPerTEU]       DECIMAL (12, 2) NULL,
    [RefundPerFEU]       DECIMAL (12, 2) NULL,
    [AcceptBooking]      BIT             NULL,
    [fk_UserAdded]       INT             NOT NULL,
    [fk_UserLastEdited]  INT             NULL,
    [AddedOn]            DATETIME        NOT NULL,
    [EditedOn]           DATETIME        NULL
);


GO
PRINT N'Creating [dbo].[expBookingCharges]...';


GO
CREATE TABLE [dbo].[expBookingCharges] (
    [pk_BookingChgID] BIGINT          NOT NULL,
    [fk_BookingID]    BIGINT          IDENTITY (1, 1) NOT NULL,
    [fk_ChargeID]     INT             NULL,
    [ActRatePerTEU]   DECIMAL (12, 2) NULL,
    [ActRatePerFEU]   DECIMAL (12, 2) NULL,
    [MnftRatePerTEU]  DECIMAL (12, 2) NULL,
    [MnftRatePerFEU]  DECIMAL (12, 2) NULL
);


GO
PRINT N'Creating PK_expBookingCharges...';


GO
ALTER TABLE [dbo].[expBookingCharges]
    ADD CONSTRAINT [PK_expBookingCharges] PRIMARY KEY CLUSTERED ([pk_BookingChgID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[finInvoice]...';


GO
CREATE TABLE [dbo].[finInvoice] (
    [pk_InvoiceID]      BIGINT        NULL,
    [fk_LocationID]     INT           NULL,
    [fk_NVOCCID]        INT           NULL,
    [fk_InvoiceTypeID]  INT           NULL,
    [InvoiceNo]         VARCHAR (20)  NULL,
    [InvoiceDate]       DATE          NULL,
    [fk_CHAID]          INT           NULL,
    [Account]           VARCHAR (300) NULL,
    [fk_UserAdded]      INT           NOT NULL,
    [fk_UserLastEdited] INT           NULL,
    [AddedOn]           DATETIME      NOT NULL,
    [EditedOn]          DATETIME      NULL
);


GO
PRINT N'Creating [dbo].[finMoneyRcpt]...';


GO
CREATE TABLE [dbo].[finMoneyRcpt] (
    [fk_InvoiceID]      BIGINT          NULL,
    [fk_LocationID]     INT             NULL,
    [fk_NVOCCID]        INT             NULL,
    [pk_MoneyRcptID]    BIGINT          NOT NULL,
    [MRNo]              VARCHAR (40)    NULL,
    [MRDate]            DATE            NULL,
    [CashPayment]       DECIMAL (14, 2) NULL,
    [ChequePayment]     DECIMAL (14, 2) NULL,
    [ChequeNo]          VARCHAR (6)     NULL,
    [ChequeDate]        DATE            NULL,
    [ChequeBank]        VARCHAR (50)    NULL,
    [TDSDeducted]       DECIMAL (14, 2) NULL,
    [fk_UserAdded]      INT             NOT NULL,
    [fk_UserLastEdited] INT             NULL,
    [AddedOn]           DATETIME        NOT NULL,
    [EditedOn]          DATETIME        NULL
);


GO
PRINT N'Creating PK_finMoneyRcpt...';


GO
ALTER TABLE [dbo].[finMoneyRcpt]
    ADD CONSTRAINT [PK_finMoneyRcpt] PRIMARY KEY CLUSTERED ([pk_MoneyRcptID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[ImpBLFooter]...';


GO
CREATE TABLE [dbo].[ImpBLFooter] (
    [fk_BLID]            BIGINT          NULL,
    [pk_BLFooterID]      BIGINT          IDENTITY (1, 1) NOT NULL,
    [CntrNo]             VARCHAR (11)    NOT NULL,
    [CntrSize]           CHAR (2)        NOT NULL,
    [fk_ContainerTypeID] INT             NOT NULL,
    [SealNo]             VARCHAR (15)    NOT NULL,
    [Comodity]           VARCHAR (20)    NOT NULL,
    [GrossWeight]        DECIMAL (13, 3) NOT NULL,
    [Package]            INT             NOT NULL,
    [CargoWtTon]         DECIMAL (12, 3) NOT NULL,
    [fk_ISOID]           INT             NOT NULL,
    [SOC]                BIT             NOT NULL,
    [Temperature]        DECIMAL (6, 2)  NULL,
    [TempUnit]           CHAR (1)        NULL,
    [CustomSeal]         VARCHAR (15)    NULL,
    [CAgent]             VARCHAR (150)   NULL,
    [TempMax]            DECIMAL (6, 2)  NULL,
    [TempMin]            DECIMAL (6, 2)  NULL,
    [PCSTemp]            CHAR (3)        NULL,
    [DIMCode]            VARCHAR (3)     NULL,
    [ODLength]           DECIMAL (12, 2) NULL,
    [ODWidth]            DECIMAL (12, 2) NULL,
    [ODHeight]           DECIMAL (12, 2) NULL,
    [Cargo]              CHAR (3)        NULL,
    [Stowage]            VARCHAR (6)     NULL,
    [CallNo]             CHAR (1)        NULL,
    [IMCO]               VARCHAR (4)     NULL,
    [fk_MovementID]      INT             NULL,
    [DateLastMoved]      DATE            NULL
);


GO
PRINT N'Creating PK_ImpBLFooter...';


GO
ALTER TABLE [dbo].[ImpBLFooter]
    ADD CONSTRAINT [PK_ImpBLFooter] PRIMARY KEY CLUSTERED ([pk_BLFooterID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[ImpBLHeader]...';


GO
CREATE TABLE [dbo].[ImpBLHeader] (
    [fk_CompanyID]           INT             NOT NULL,
    [fk_LocationID]          INT             NOT NULL,
    [pk_BLID]                BIGINT          IDENTITY (1, 1) NOT NULL,
    [fk_NVOCCID]             INT             NULL,
    [ImpBLNo]                VARCHAR (60)    NOT NULL,
    [ImpBLDate]              DATETIME        NOT NULL,
    [fk_ImpVesselID]         BIGINT          NOT NULL,
    [fk_ImpVoyageID]         BIGINT          NOT NULL,
    [ItemLinePrefix]         VARCHAR (8)     NULL,
    [ItemLineNo]             VARCHAR (6)     NULL,
    [MBLNumber]              VARCHAR (60)    NULL,
    [MBLDate]                DATE            NOT NULL,
    [MotherVessel]           VARCHAR (300)   NULL,
    [fk_BLIssuePortID]       BIGINT          NOT NULL,
    [fk_PlaceReceipt]        BIGINT          NOT NULL,
    [fk_PortLoading]         BIGINT          NOT NULL,
    [fk_PortDischarge]       BIGINT          NOT NULL,
    [fk_PlaceDelivery]       BIGINT          NOT NULL,
    [fk_FinalDestination]    BIGINT          NOT NULL,
    [fk_StockLocationID]     BIGINT          NULL,
    [TranShipment]           VARCHAR (500)   NULL,
    [CargoNature]            VARCHAR (2)     NOT NULL,
    [CargoMovement]          VARCHAR (2)     NOT NULL,
    [ItemType]               VARCHAR (2)     NOT NULL,
    [CargoType]              CHAR (1)        NOT NULL,
    [GrossWeight]            DECIMAL (16, 3) NULL,
    [fk_UnitOfWeight]        INT             NULL,
    [Volume]                 DECIMAL (16, 3) NULL,
    [fk_UnitOfVolume]        INT             NULL,
    [NumberPackage]          BIGINT          NOT NULL,
    [fk_UnitPackage]         INT             NOT NULL,
    [PackageDetail]          VARCHAR (30)    NULL,
    [HazFlag]                BIT             NOT NULL,
    [UNOCode]                VARCHAR (5)     NOT NULL,
    [IMOCode]                VARCHAR (3)     NOT NULL,
    [ShipperInformation]     VARCHAR (500)   NULL,
    [ConsigneeInformation]   VARCHAR (500)   NOT NULL,
    [NotifyPartyInformation] VARCHAR (500)   NOT NULL,
    [MarksNumbers]           VARCHAR (500)   NOT NULL,
    [GoodDescription]        VARCHAR (500)   NOT NULL,
    [fk_AddressCFSId]        INT             NOT NULL,
    [TPBondNo]               VARCHAR (10)    NULL,
    [CACode]                 VARCHAR (10)    NULL,
    [fk_AddressCHAID]        INT             NULL,
    [TransportMode]          VARCHAR (1)     NOT NULL,
    [MLOCode]                VARCHAR (16)    NULL,
    [DOLock]                 BIT             NOT NULL,
    [DOLockReason]           VARCHAR (100)   NULL,
    [DetentionFree]          INT             NOT NULL,
    [DetentionSlabType]      CHAR (1)        NULL,
    [PGR_FreeDays]           INT             NOT NULL,
    [FreightType]            CHAR (2)        NOT NULL,
    [FreigthToCollect]       DECIMAL (12, 2) NULL,
    [FreeOut]                BIT             NOT NULL,
    [PartBL]                 BIT             NOT NULL,
    [HookPoint]              BIT             NOT NULL,
    [TaxExemption]           BIT             NOT NULL,
    [Reefer]                 BIT             NOT NULL,
    [ONBR]                   BIT             NULL,
    [DocFact]                BIT             NOT NULL,
    [dtAdded]                DATETIME        NOT NULL,
    [dtEdited]               DATETIME        NULL,
    [fk_UserAdded]           BIGINT          NOT NULL,
    [fk_UserEdited]          BIGINT          NULL,
    [RsStatus]               BIT             NULL
);


GO
PRINT N'Creating PK_trn_BLHeader_1...';


GO
ALTER TABLE [dbo].[ImpBLHeader]
    ADD CONSTRAINT [PK_trn_BLHeader_1] PRIMARY KEY CLUSTERED ([pk_BLID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[mstAddress]...';


GO
CREATE TABLE [dbo].[mstAddress] (
    [fk_AddressID]      INT           IDENTITY (1, 1) NOT NULL,
    [fk_CompanyID]      INT           NOT NULL,
    [fk_LocationID]     INT           NOT NULL,
    [AddrName]          VARCHAR (60)  NOT NULL,
    [AddrAddress]       VARCHAR (500) NULL,
    [AddrSalutation]    VARCHAR (10)  NULL,
    [AddrType]          VARCHAR (5)   NOT NULL,
    [CFSCode]           VARCHAR (10)  NULL,
    [fk_terminalid]     INT           NULL,
    [AddrActive]        BIT           NOT NULL,
    [fk_UserAdded]      INT           NULL,
    [fk_UserLastEdited] INT           NULL,
    [AddedOn]           DATETIME      NULL,
    [EditedOn]          DATETIME      NULL
);


GO
PRINT N'Creating PK_mstVendors...';


GO
ALTER TABLE [dbo].[mstAddress]
    ADD CONSTRAINT [PK_mstVendors] PRIMARY KEY CLUSTERED ([fk_AddressID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[mstAddressType]...';


GO
CREATE TABLE [dbo].[mstAddressType] (
    [pk_AddrTypeID] INT          IDENTITY (1, 1) NOT NULL,
    [AddrType]      CHAR (2)     NOT NULL,
    [AddrTypeDesc]  VARCHAR (25) NOT NULL
);


GO
PRINT N'Creating PK_mstAddressType...';


GO
ALTER TABLE [dbo].[mstAddressType]
    ADD CONSTRAINT [PK_mstAddressType] PRIMARY KEY CLUSTERED ([pk_AddrTypeID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[mstCharges]...';


GO
CREATE TABLE [dbo].[mstCharges] (
    [pk_ChargesID]      INT          IDENTITY (1, 1) NOT NULL,
    [fk_CompanyID]      INT          NULL,
    [ChargeDescr]       VARCHAR (40) NULL,
    [ChargeType]        CHAR (1)     NULL,
    [IEC]               CHAR (1)     NULL,
    [fk_NVOCCID]        INT          NULL,
    [Sequence]          INT          NULL,
    [RateChangeable]    BIT          NULL,
    [ChargeActive]      BIT          NOT NULL,
    [fk_UserAdded]      INT          NULL,
    [fk_UserLastEdited] INT          NULL,
    [AddedOn]           DATETIME     NULL,
    [EditedOn]          DATETIME     NULL,
    [freightcomponent]  BIT          NULL
);


GO
PRINT N'Creating PK_mstCharges_1...';


GO
ALTER TABLE [dbo].[mstCharges]
    ADD CONSTRAINT [PK_mstCharges_1] PRIMARY KEY CLUSTERED ([pk_ChargesID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[mstChargesRate]...';


GO
CREATE TABLE [dbo].[mstChargesRate] (
    [fk_ChargesID]         INT             NOT NULL,
    [pk_ChargesRateID]     INT             IDENTITY (1, 1) NOT NULL,
    [fk_LocationID]        BIGINT          NOT NULL,
    [fk_TerminalID]        INT             NULL,
    [RatePerBL]            DECIMAL (12, 2) NULL,
    [SlabLow]              INT             NULL,
    [SlabHigh]             INT             NULL,
    [RatePerTEU]           DECIMAL (12, 2) NULL,
    [RatePerFEU]           DECIMAL (12, 2) NULL,
    [RatePerCBM]           DECIMAL (12, 2) NULL,
    [RatePerTon]           DECIMAL (12, 2) NULL,
    [ServiceTaxApplicable] BIT             NULL,
    [RateActive]           BIT             NOT NULL,
    [PRatePerTEU]          DECIMAL (12, 2) NULL,
    [PRatePerFEU]          DECIMAL (12, 2) NULL,
    [PRatePerBL]           DECIMAL (12, 2) NULL
);


GO
PRINT N'Creating PK_mstChargesRate...';


GO
ALTER TABLE [dbo].[mstChargesRate]
    ADD CONSTRAINT [PK_mstChargesRate] PRIMARY KEY CLUSTERED ([pk_ChargesRateID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[mstCompany]...';


GO
CREATE TABLE [dbo].[mstCompany] (
    [pk_CompanyID]     INT           IDENTITY (1, 1) NOT NULL,
    [CompName]         VARCHAR (60)  NOT NULL,
    [CompAbbr]         VARCHAR (5)   NOT NULL,
    [CompAddress]      VARCHAR (300) NOT NULL,
    [CompCity]         VARCHAR (20)  NULL,
    [CompPIN]          VARCHAR (10)  NULL,
    [ActivityNature]   CHAR (1)      NULL,
    [PANNo]            VARCHAR (20)  NULL,
    [ICEGateLogin]     VARCHAR (20)  NOT NULL,
    [InstType]         NCHAR (10)    NULL,
    [CustHouseCode]    VARCHAR (20)  NULL,
    [PCSLogin]         VARCHAR (20)  NULL,
    [ShippingLineCode] VARCHAR (10)  NULL,
    [GateWayPort]      VARCHAR (6)   NULL,
    [BankDetail]       VARCHAR (300) NULL,
    [CompStatus]       BIT           NOT NULL
);


GO
PRINT N'Creating PK_mstCompany...';


GO
ALTER TABLE [dbo].[mstCompany]
    ADD CONSTRAINT [PK_mstCompany] PRIMARY KEY CLUSTERED ([pk_CompanyID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[mstContainerMovement]...';


GO
CREATE TABLE [dbo].[mstContainerMovement] (
    [pk_MovementID]     INT          IDENTITY (1, 1) NOT NULL,
    [MoveAbbr]          VARCHAR (4)  NULL,
    [MoveDescr]         VARCHAR (25) NULL,
    [MoveSeq]           INT          NULL,
    [MoveStatus]        BIT          NULL,
    [fk_UserAdded]      INT          NULL,
    [fk_UserLastEdited] INT          NULL,
    [AddedOn]           DATETIME     NULL,
    [EditedOn]          DATETIME     NULL
);


GO
PRINT N'Creating PK_mstContainerMovement...';


GO
ALTER TABLE [dbo].[mstContainerMovement]
    ADD CONSTRAINT [PK_mstContainerMovement] PRIMARY KEY CLUSTERED ([pk_MovementID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[mstContainerMovOptions]...';


GO
CREATE TABLE [dbo].[mstContainerMovOptions] (
    [pk_MovmentOptID] INT IDENTITY (1, 1) NOT NULL,
    [fk_CurrMoveID]   INT NOT NULL,
    [fk_AvlMoveID]    INT NOT NULL
);


GO
PRINT N'Creating PK_mstContainerMovOptions...';


GO
ALTER TABLE [dbo].[mstContainerMovOptions]
    ADD CONSTRAINT [PK_mstContainerMovOptions] PRIMARY KEY CLUSTERED ([pk_MovmentOptID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[mstContainerSize]...';


GO
CREATE TABLE [dbo].[mstContainerSize] (
    [fk_SizeID] INT         NULL,
    [Size]      VARCHAR (2) NULL
);


GO
PRINT N'Creating [dbo].[mstContainerType]...';


GO
CREATE TABLE [dbo].[mstContainerType] (
    [pk_ContainerTypeID] INT          IDENTITY (1, 1) NOT NULL,
    [ContainerAbbr]      VARCHAR (2)  NULL,
    [CotainerDesc]       VARCHAR (20) NULL,
    [TareWeight20]       INT          NULL,
    [TareWeight40]       INT          NULL,
    [CntrHTFT]           INT          NULL,
    [CntrHTIN]           INT          NULL,
    [ISO20]              VARCHAR (4)  NULL,
    [ISO40]              VARCHAR (4)  NULL,
    [fk_UserAdded]       INT          NULL,
    [fk_UserLastEdited]  INT          NULL,
    [AddedOn]            DATETIME     NULL,
    [EditedOn]           DATETIME     NULL
);


GO
PRINT N'Creating PK_mstContainerType...';


GO
ALTER TABLE [dbo].[mstContainerType]
    ADD CONSTRAINT [PK_mstContainerType] PRIMARY KEY CLUSTERED ([pk_ContainerTypeID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[mstCountry]...';


GO
CREATE TABLE [dbo].[mstCountry] (
    [CountryName]       VARCHAR (100) NULL,
    [CountryAbbr]       CHAR (2)      NULL,
    [CountryStatus]     BIT           NULL,
    [pk_countryid]      INT           IDENTITY (1, 1) NOT NULL,
    [fk_UserAdded]      INT           NULL,
    [fk_UserLastEdited] INT           NULL,
    [AddedOn]           DATETIME      NULL,
    [EditedOn]          DATETIME      NULL
);


GO
PRINT N'Creating [dbo].[mstDocCharge]...';


GO
CREATE TABLE [dbo].[mstDocCharge] (
    [fk_DocTypeID]      INT      NOT NULL,
    [fk_ChargesRateID]  INT      NOT NULL,
    [pk_DocTypeChg]     INT      IDENTITY (1, 1) NOT NULL,
    [Serial]            INT      NOT NULL,
    [DCRStatus]         BIT      NOT NULL,
    [fk_UserAdded]      INT      NULL,
    [fk_UserLastEdited] INT      NULL,
    [AddedOn]           DATETIME NULL,
    [EditedOn]          DATETIME NULL
);


GO
PRINT N'Creating PK_mstDocCharge...';


GO
ALTER TABLE [dbo].[mstDocCharge]
    ADD CONSTRAINT [PK_mstDocCharge] PRIMARY KEY CLUSTERED ([pk_DocTypeChg] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[mstDocumentType]...';


GO
CREATE TABLE [dbo].[mstDocumentType] (
    [pk_DocTypeID]      INT          IDENTITY (1, 1) NOT NULL,
    [fk_CompanyID]      INT          NULL,
    [DocName]           VARCHAR (40) NULL,
    [DocHeading]        VARCHAR (40) NULL,
    [DocType]           CHAR (1)     NULL,
    [Calculation]       CHAR (1)     NULL,
    [SeparateonNVOCC]   BIT          NULL,
    [DocAbbr]           VARCHAR (4)  NULL,
    [ResetPeriod]       CHAR (1)     NULL,
    [DocStatus]         BIT          NULL,
    [fk_UserAdded]      INT          NULL,
    [fk_UserLastEdited] INT          NULL,
    [AddedOn]           DATETIME     NULL,
    [EditedOn]          DATETIME     NULL
);


GO
PRINT N'Creating PK_mstInvoiceType...';


GO
ALTER TABLE [dbo].[mstDocumentType]
    ADD CONSTRAINT [PK_mstInvoiceType] PRIMARY KEY CLUSTERED ([pk_DocTypeID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[mstExchangeRate]...';


GO
CREATE TABLE [dbo].[mstExchangeRate] (
    [fk_ExchRateID]     INT             NOT NULL,
    [fk_CompanyID]      INT             NULL,
    [ExchDate]          DATE            NULL,
    [USDXchRate]        DECIMAL (12, 2) NULL,
    [fk_UserAdded]      INT             NULL,
    [fk_UserLastEdited] INT             NULL,
    [AddedOn]           DATETIME        NULL,
    [EditedOn]          DATETIME        NULL
);


GO
PRINT N'Creating PK_mstExchangeRate...';


GO
ALTER TABLE [dbo].[mstExchangeRate]
    ADD CONSTRAINT [PK_mstExchangeRate] PRIMARY KEY CLUSTERED ([fk_ExchRateID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[mstExportMode]...';


GO
CREATE TABLE [dbo].[mstExportMode] (
    [fk_ModeID]         INT          IDENTITY (1, 1) NOT NULL,
    [ModeName]          VARCHAR (10) NULL,
    [ModeStatus]        BIT          NULL,
    [fk_UserAdded]      INT          NULL,
    [fk_UserLastEdited] INT          NULL,
    [AddedOn]           DATETIME     NULL,
    [EditedOn]          DATETIME     NULL
);


GO
PRINT N'Creating PK_mstMode...';


GO
ALTER TABLE [dbo].[mstExportMode]
    ADD CONSTRAINT [PK_mstMode] PRIMARY KEY CLUSTERED ([fk_ModeID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[mstFinYear]...';


GO
CREATE TABLE [dbo].[mstFinYear] (
    [pk_FinYearID] INT         IDENTITY (1, 1) NOT NULL,
    [FYStartDate]  DATE        NULL,
    [FYEndDate]    DATE        NULL,
    [FYAbbr]       VARCHAR (5) NULL
);


GO
PRINT N'Creating PK_mstFinYear...';


GO
ALTER TABLE [dbo].[mstFinYear]
    ADD CONSTRAINT [PK_mstFinYear] PRIMARY KEY CLUSTERED ([pk_FinYearID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[mstHaulageCharge]...';


GO
CREATE TABLE [dbo].[mstHaulageCharge] (
    [pk_HaulageChgID]   INT             IDENTITY (1, 1) NOT NULL,
    [fk_LocationFrom]   INT             NULL,
    [fk_LocationTo]     INT             NULL,
    [ContainerSize]     VARCHAR (2)     NULL,
    [WeightFrom]        DECIMAL (12, 3) NULL,
    [WeightTo]          DECIMAL (12, 3) NULL,
    [HaulageRate]       DECIMAL (12, 2) NULL,
    [HaulageStatus]     BIT             NULL,
    [fk_UserAdded]      INT             NULL,
    [fk_UserLastEdited] INT             NULL,
    [AddedOn]           DATETIME        NULL,
    [EditedOn]          DATETIME        NULL
);


GO
PRINT N'Creating PK_mstHaulageCharge...';


GO
ALTER TABLE [dbo].[mstHaulageCharge]
    ADD CONSTRAINT [PK_mstHaulageCharge] PRIMARY KEY CLUSTERED ([pk_HaulageChgID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[mstISO]...';


GO
CREATE TABLE [dbo].[mstISO] (
    [pk_ISOID]          INT          IDENTITY (1, 1) NOT NULL,
    [ISOAbbr]           CHAR (4)     NULL,
    [ISOName]           VARCHAR (20) NULL,
    [fk_UserAdded]      INT          NULL,
    [fk_UserLastEdited] INT          NULL,
    [AddedOn]           DATETIME     NULL,
    [EditedOn]          DATETIME     NULL
);


GO
PRINT N'Creating PK_mstISO...';


GO
ALTER TABLE [dbo].[mstISO]
    ADD CONSTRAINT [PK_mstISO] PRIMARY KEY CLUSTERED ([pk_ISOID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[mstLastNo]...';


GO
CREATE TABLE [dbo].[mstLastNo] (
    [pk_DocNoID]    INT    IDENTITY (1, 1) NOT NULL,
    [fk_LocationID] INT    NOT NULL,
    [fk_DocTypeID]  INT    NULL,
    [fk_NVOCCID]    INT    NULL,
    [fk_FinYearID]  INT    NULL,
    [LastNo]        BIGINT NULL
);


GO
PRINT N'Creating PK_mstLastNo...';


GO
ALTER TABLE [dbo].[mstLastNo]
    ADD CONSTRAINT [PK_mstLastNo] PRIMARY KEY CLUSTERED ([pk_DocNoID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[mstLinkRoleModule]...';


GO
CREATE TABLE [dbo].[mstLinkRoleModule] (
    [pk_LinkID]   INT NULL,
    [fk_ModuleID] INT NULL,
    [fk_RoleID]   INT NULL,
    [InsRec]      BIT NULL,
    [EditRec]     BIT NULL,
    [DelRec]      BIT NULL,
    [ViewRec]     BIT NULL
);


GO
PRINT N'Creating [dbo].[mstModules]...';


GO
CREATE TABLE [dbo].[mstModules] (
    [pk_Modules]   INT          IDENTITY (1, 1) NOT NULL,
    [ModuleName]   VARCHAR (50) NULL,
    [ModuleType]   CHAR (1)     NULL,
    [ModuleStatus] BIT          NULL
);


GO
PRINT N'Creating PK_mstModules...';


GO
ALTER TABLE [dbo].[mstModules]
    ADD CONSTRAINT [PK_mstModules] PRIMARY KEY CLUSTERED ([pk_Modules] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[mstNVOCC]...';


GO
CREATE TABLE [dbo].[mstNVOCC] (
    [pk_NVOCCID]        INT          NOT NULL,
    [NVOCCName]         VARCHAR (50) NULL,
    [NVOCCActive]       BIT          NULL,
    [DefaultFreeDays]   INT          NULL,
    [ContAgentCode]     VARCHAR (10) NULL,
    [fk_UserAdded]      INT          NULL,
    [fk_UserLastEdited] INT          NULL,
    [AddedOn]           DATETIME     NULL,
    [EditedOn]          DATETIME     NULL
);


GO
PRINT N'Creating [dbo].[mstPort]...';


GO
CREATE TABLE [dbo].[mstPort] (
    [pk_PortID]     BIGINT       IDENTITY (1, 1) NOT NULL,
    [PortCode]      VARCHAR (6)  NOT NULL,
    [PortName]      VARCHAR (30) NOT NULL,
    [ICDIndicator]  BIT          NOT NULL,
    [dtAdded]       DATE         NOT NULL,
    [dtEdited]      DATE         NULL,
    [fk_UserAdded]  BIGINT       NOT NULL,
    [fk_UserEdited] BIGINT       NULL,
    [RStatus]       BIT          NULL,
    [PortAddressee] VARCHAR (50) NULL,
    [Address2]      VARCHAR (50) NULL,
    [Address3]      VARCHAR (50) NULL
);


GO
PRINT N'Creating PK_mstPortInformation...';


GO
ALTER TABLE [dbo].[mstPort]
    ADD CONSTRAINT [PK_mstPortInformation] PRIMARY KEY CLUSTERED ([pk_PortID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[mstRoles]...';


GO
CREATE TABLE [dbo].[mstRoles] (
    [pk_RoleID] INT          IDENTITY (1, 1) NOT NULL,
    [RoleName]  VARCHAR (50) NULL,
    [RollType]  CHAR (10)    NULL
);


GO
PRINT N'Creating PK_mstRoles...';


GO
ALTER TABLE [dbo].[mstRoles]
    ADD CONSTRAINT [PK_mstRoles] PRIMARY KEY CLUSTERED ([pk_RoleID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[mstServTaxStructure]...';


GO
CREATE TABLE [dbo].[mstServTaxStructure] (
    [pk_StaxID]         INT            NULL,
    [StartDate]         DATE           NULL,
    [TaxPer]            DECIMAL (6, 2) NULL,
    [TaxCess]           DECIMAL (6, 2) NULL,
    [TaxAddCess]        DECIMAL (6, 2) NULL,
    [TaxStatus]         BIT            NULL,
    [fk_UserAdded]      INT            NULL,
    [fk_UserLastEdited] INT            NULL,
    [AddedOn]           DATETIME       NULL,
    [EditedOn]          DATETIME       NULL
);


GO
PRINT N'Creating [dbo].[mstTerminal]...';


GO
CREATE TABLE [dbo].[mstTerminal] (
    [fk_TerminalID]     INT          IDENTITY (1, 1) NOT NULL,
    [fk_LocationID]     INT          NOT NULL,
    [TerminalName]      VARCHAR (20) NOT NULL,
    [TerminalStatus]    BIT          NOT NULL,
    [fk_UserAdded]      INT          NULL,
    [fk_UserLastEdited] INT          NULL,
    [AddedOn]           DATETIME     NULL,
    [EditedOn]          DATETIME     NULL
);


GO
PRINT N'Creating PK_mstTerminal...';


GO
ALTER TABLE [dbo].[mstTerminal]
    ADD CONSTRAINT [PK_mstTerminal] PRIMARY KEY CLUSTERED ([fk_TerminalID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[mstUOM]...';


GO
CREATE TABLE [dbo].[mstUOM] (
    [pk_UOMId]      INT          IDENTITY (1, 1) NOT NULL,
    [UnitCode]      VARCHAR (3)  NOT NULL,
    [UnitName]      VARCHAR (30) NOT NULL,
    [UnitType]      VARCHAR (1)  NOT NULL,
    [dtAdded]       DATE         NOT NULL,
    [dtEdited]      DATE         NULL,
    [fk_UserAdded]  BIGINT       NOT NULL,
    [fk_UserEdited] BIGINT       NULL,
    [UnitStatus]    BIT          NULL,
    [DefaultValue]  BIT          NULL
);


GO
PRINT N'Creating PK_mstUOM...';


GO
ALTER TABLE [dbo].[mstUOM]
    ADD CONSTRAINT [PK_mstUOM] PRIMARY KEY CLUSTERED ([pk_UOMId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[mstUsers]...';


GO
CREATE TABLE [dbo].[mstUsers] (
    [pk_UserID]        INT          IDENTITY (1, 1) NOT NULL,
    [UserName]         VARCHAR (10) NOT NULL,
    [Password]         VARCHAR (50) NOT NULL,
    [FirstName]        VARCHAR (30) NOT NULL,
    [LastName]         VARCHAR (30) NOT NULL,
    [fk_RoleID]        INT          NOT NULL,
    [fk_LocID]         INT          NOT NULL,
    [emailID]          VARCHAR (50) NULL,
    [UserActive]       BIT          NOT NULL,
    [AllowMultipleLoc] BIT          NOT NULL,
    [IsDeleted]        BIT          NOT NULL
);


GO
PRINT N'Creating PK_mstUser...';


GO
ALTER TABLE [dbo].[mstUsers]
    ADD CONSTRAINT [PK_mstUser] PRIMARY KEY CLUSTERED ([pk_UserID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[mstVendorMovementAccess]...';


GO
CREATE TABLE [dbo].[mstVendorMovementAccess] (
    [VENDORCODE]       VARCHAR (10) NULL,
    [fk_AllowedMoveID] INT          NULL
);


GO
PRINT N'Creating [dbo].[mstVesselPrefix]...';


GO
CREATE TABLE [dbo].[mstVesselPrefix] (
    [fk_VesselPrefixID] INT          IDENTITY (1, 1) NOT NULL,
    [VesselPrefix]      VARCHAR (10) NULL,
    [fk_UserAdded]      INT          NULL,
    [fk_UserLastEdited] INT          NULL,
    [AddedOn]           DATETIME     NULL,
    [EditedOn]          DATETIME     NULL
);


GO
PRINT N'Creating PK_mstVesselPrefix...';


GO
ALTER TABLE [dbo].[mstVesselPrefix]
    ADD CONSTRAINT [PK_mstVesselPrefix] PRIMARY KEY CLUSTERED ([fk_VesselPrefixID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[trnRepairing]...';


GO
CREATE TABLE [dbo].[trnRepairing] (
    [fk_ContainerID]    BIGINT          NULL,
    [fk_BLID]           BIGINT          NULL,
    [RepairingEstimate] VARCHAR (20)    NULL,
    [LabourEstimate]    DECIMAL (12, 2) NULL,
    [MaterialEstimate]  DECIMAL (12, 2) NULL,
    [WashingEstimate]   DECIMAL (12, 2) NULL,
    [LabourToCharge]    DECIMAL (12, 2) NULL,
    [MaterialToCharge]  DECIMAL (12, 2) NULL,
    [WashingToCharge]   DECIMAL (12, 2) NULL,
    [LabourApproved]    DECIMAL (12, 2) NULL,
    [MaterialApproved]  DECIMAL (12, 2) NULL,
    [WashingApproved]   DECIMAL (12, 2) NULL,
    [fk_ApprovedUser]   INT             NULL,
    [RepairingTax]      DECIMAL (12, 2) NULL,
    [RepairingBillNo]   VARCHAR (20)    NULL,
    [RepairingBillDate] DATE            NULL,
    [PRDetention]       DECIMAL (12, 2) NULL,
    [fk_UserAdded]      INT             NOT NULL,
    [fk_UserLastEdited] INT             NULL,
    [AddedOn]           DATETIME        NOT NULL,
    [EditedOn]          DATETIME        NULL
);


GO
PRINT N'Creating [dbo].[trnVessel]...';


GO
CREATE TABLE [dbo].[trnVessel] (
    [fk_CompanyID]      INT          NOT NULL,
    [pk_VesselID]       BIGINT       IDENTITY (1, 1) NOT NULL,
    [fk_VesselPrefixID] INT          NOT NULL,
    [VesselName]        VARCHAR (60) NOT NULL,
    [CallSign]          VARCHAR (14) NULL,
    [IMONumber]         VARCHAR (14) NULL,
    [fk_CountryId]      INT          NULL,
    [ShippingLineCode]  VARCHAR (10) NULL,
    [AgentCode]         VARCHAR (10) NULL,
    [MasterName]        VARCHAR (50) NULL,
    [fk_PortID]         BIGINT       NULL,
    [dtAdded]           DATE         NOT NULL,
    [dtEdited]          DATE         NULL,
    [fk_UserAdded]      BIGINT       NOT NULL,
    [fk_UserEdited]     BIGINT       NULL,
    [VesselStatus]      BIT          NULL,
    [fk_locationid]     INT          NOT NULL
);


GO
PRINT N'Creating PK_trn_vessel...';


GO
ALTER TABLE [dbo].[trnVessel]
    ADD CONSTRAINT [PK_trn_vessel] PRIMARY KEY CLUSTERED ([fk_CompanyID] ASC, [pk_VesselID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[trnVoyage]...';


GO
CREATE TABLE [dbo].[trnVoyage] (
    [pk_VoyageID]        BIGINT          IDENTITY (1, 1) NOT NULL,
    [fk_CompanyID]       INT             NOT NULL,
    [fk_VesselID]        BIGINT          NOT NULL,
    [fl_TerminalID]      INT             NULL,
    [VoyageNo]           VARCHAR (10)    NOT NULL,
    [IGMNo]              VARCHAR (10)    NULL,
    [IGMDate]            DATE            NULL,
    [LandingDate]        DATE            NULL,
    [AddLandingDate]     DATE            NULL,
    [VoyageType]         CHAR (1)        NOT NULL,
    [LGNo]               VARCHAR (40)    NULL,
    [AltLGNo]            VARCHAR (40)    NULL,
    [fk_LPortID]         BIGINT          NULL,
    [fk_LPortID1]        BIGINT          NULL,
    [fk_LPortID2]        BIGINT          NULL,
    [VesselType]         VARCHAR (1)     NULL,
    [MotherDaughterDtl]  VARCHAR (500)   NULL,
    [TotalLines]         VARCHAR (5)     NULL,
    [CargoDesc]          VARCHAR (50)    NOT NULL,
    [ETADate]            DATETIME        NULL,
    [ETATime]            TIME (7)        NULL,
    [LightHouseDue]      INT             NULL,
    [SameButtonCargo]    BIT             NOT NULL,
    [ShipStoreSubmitted] BIT             NOT NULL,
    [CrewList]           BIT             NOT NULL,
    [PassengerList]      BIT             NOT NULL,
    [CrewEffectList]     BIT             NOT NULL,
    [MaritimeList]       BIT             NOT NULL,
    [CallSign]           VARCHAR (7)     NULL,
    [ImpXChangeRate]     DECIMAL (12, 7) NULL,
    [PCCNo]              VARCHAR (10)    NULL,
    [PCCDate]            DATE            NULL,
    [VIANo]              VARCHAR (10)    NULL,
    [VCN]                VARCHAR (14)    NULL,
    [SailDate]           DATE            NULL,
    [ETD]                DATE            NULL,
    [ExpXchangeRate]     DECIMAL (12, 7) NULL,
    [CutOffDate]         DATE            NULL,
    [EGMNo]              BIGINT          NULL,
    [EGMDate]            DATE            NULL,
    [BondNo]             VARCHAR (10)    NULL,
    [VesselApplNo]       VARCHAR (7)     NULL,
    [VesselAppDate]      DATE            NULL,
    [BondAmount]         DECIMAL (10, 2) NULL,
    [BondBalance]        DECIMAL (10, 2) NULL,
    [VesselSerial]       VARCHAR (6)     NULL,
    [dtAdded]            DATE            NULL,
    [dtEdited]           DATE            NULL,
    [fk_UserAdded]       BIGINT          NULL,
    [fk_UserEdited]      BIGINT          NULL,
    [VoyageStatus]       BIT             NULL
);


GO
PRINT N'Creating PK_trn_Voyage...';


GO
ALTER TABLE [dbo].[trnVoyage]
    ADD CONSTRAINT [PK_trn_Voyage] PRIMARY KEY CLUSTERED ([pk_VoyageID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating IX_trn_Voyage...';


GO
ALTER TABLE [dbo].[trnVoyage]
    ADD CONSTRAINT [IX_trn_Voyage] UNIQUE NONCLUSTERED ([pk_VoyageID] ASC, [fk_VesselID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY];


GO
PRINT N'Creating DF_trnBLFooter_SOC...';


GO
ALTER TABLE [dbo].[ImpBLFooter]
    ADD CONSTRAINT [DF_trnBLFooter_SOC] DEFAULT ((0)) FOR [SOC];


GO
PRINT N'Creating DF_trn_BLHeader_FreeOut...';


GO
ALTER TABLE [dbo].[ImpBLHeader]
    ADD CONSTRAINT [DF_trn_BLHeader_FreeOut] DEFAULT ((0)) FOR [FreeOut];


GO
PRINT N'Creating DF_trn_BLHeader_HazFlag...';


GO
ALTER TABLE [dbo].[ImpBLHeader]
    ADD CONSTRAINT [DF_trn_BLHeader_HazFlag] DEFAULT ((0)) FOR [HazFlag];


GO
PRINT N'Creating DF_trn_BLHeader_HookPoint...';


GO
ALTER TABLE [dbo].[ImpBLHeader]
    ADD CONSTRAINT [DF_trn_BLHeader_HookPoint] DEFAULT ((0)) FOR [HookPoint];


GO
PRINT N'Creating DF_trn_BLHeader_Part...';


GO
ALTER TABLE [dbo].[ImpBLHeader]
    ADD CONSTRAINT [DF_trn_BLHeader_Part] DEFAULT ((0)) FOR [PartBL];


GO
PRINT N'Creating DF_trn_BLHeader_Reefer...';


GO
ALTER TABLE [dbo].[ImpBLHeader]
    ADD CONSTRAINT [DF_trn_BLHeader_Reefer] DEFAULT ((0)) FOR [Reefer];


GO
PRINT N'Creating DF_trn_BLHeader_TaxExemption...';


GO
ALTER TABLE [dbo].[ImpBLHeader]
    ADD CONSTRAINT [DF_trn_BLHeader_TaxExemption] DEFAULT ((0)) FOR [TaxExemption];


GO
PRINT N'Creating DF_mstAddress_AddrActive...';


GO
ALTER TABLE [dbo].[mstAddress]
    ADD CONSTRAINT [DF_mstAddress_AddrActive] DEFAULT ((1)) FOR [AddrActive];


GO
PRINT N'Creating DF_mstAddress_AddrType...';


GO
ALTER TABLE [dbo].[mstAddress]
    ADD CONSTRAINT [DF_mstAddress_AddrType] DEFAULT ((0)) FOR [AddrType];


GO
PRINT N'Creating DF_mstAddress_fk_terminalid...';


GO
ALTER TABLE [dbo].[mstAddress]
    ADD CONSTRAINT [DF_mstAddress_fk_terminalid] DEFAULT ((-1)) FOR [fk_terminalid];


GO
PRINT N'Creating DF_mstPortInformation_ICDIndicator...';


GO
ALTER TABLE [dbo].[mstPort]
    ADD CONSTRAINT [DF_mstPortInformation_ICDIndicator] DEFAULT ((0)) FOR [ICDIndicator];


GO
PRINT N'Creating DF_mstUOM_UnitStatus...';


GO
ALTER TABLE [dbo].[mstUOM]
    ADD CONSTRAINT [DF_mstUOM_UnitStatus] DEFAULT ((1)) FOR [UnitStatus];


GO
PRINT N'Creating DF_mstUsers_IsDeleted...';


GO
ALTER TABLE [dbo].[mstUsers]
    ADD CONSTRAINT [DF_mstUsers_IsDeleted] DEFAULT ((0)) FOR [IsDeleted];


GO
PRINT N'Creating DF_trn_Voyage_CrewEffectList...';


GO
ALTER TABLE [dbo].[trnVoyage]
    ADD CONSTRAINT [DF_trn_Voyage_CrewEffectList] DEFAULT ((1)) FOR [CrewEffectList];


GO
PRINT N'Creating DF_trn_Voyage_CrewList...';


GO
ALTER TABLE [dbo].[trnVoyage]
    ADD CONSTRAINT [DF_trn_Voyage_CrewList] DEFAULT ((1)) FOR [CrewList];


GO
PRINT N'Creating DF_trn_Voyage_LightHouseDue...';


GO
ALTER TABLE [dbo].[trnVoyage]
    ADD CONSTRAINT [DF_trn_Voyage_LightHouseDue] DEFAULT ((0)) FOR [LightHouseDue];


GO
PRINT N'Creating DF_trn_Voyage_MaritimeList...';


GO
ALTER TABLE [dbo].[trnVoyage]
    ADD CONSTRAINT [DF_trn_Voyage_MaritimeList] DEFAULT ((1)) FOR [MaritimeList];


GO
PRINT N'Creating DF_trn_Voyage_PassengerList...';


GO
ALTER TABLE [dbo].[trnVoyage]
    ADD CONSTRAINT [DF_trn_Voyage_PassengerList] DEFAULT ((0)) FOR [PassengerList];


GO
PRINT N'Creating DF_trn_voyage_SameButtonCargo...';


GO
ALTER TABLE [dbo].[trnVoyage]
    ADD CONSTRAINT [DF_trn_voyage_SameButtonCargo] DEFAULT ((0)) FOR [SameButtonCargo];


GO
PRINT N'Creating DF_trn_Voyage_ShipStoreSubmitted...';


GO
ALTER TABLE [dbo].[trnVoyage]
    ADD CONSTRAINT [DF_trn_Voyage_ShipStoreSubmitted] DEFAULT ((1)) FOR [ShipStoreSubmitted];


GO
PRINT N'Creating FK_mstAddress_mstCompany...';


GO
ALTER TABLE [dbo].[mstAddress] WITH NOCHECK
    ADD CONSTRAINT [FK_mstAddress_mstCompany] FOREIGN KEY ([fk_CompanyID]) REFERENCES [dbo].[mstCompany] ([pk_CompanyID]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_mstCharges_mstCompany...';


GO
ALTER TABLE [dbo].[mstCharges] WITH NOCHECK
    ADD CONSTRAINT [FK_mstCharges_mstCompany] FOREIGN KEY ([fk_CompanyID]) REFERENCES [dbo].[mstCompany] ([pk_CompanyID]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_mstChargesRate_mstCharges...';


GO
ALTER TABLE [dbo].[mstChargesRate] WITH NOCHECK
    ADD CONSTRAINT [FK_mstChargesRate_mstCharges] FOREIGN KEY ([fk_ChargesID]) REFERENCES [dbo].[mstCharges] ([pk_ChargesID]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_mstDocumentType_mstCompany...';


GO
ALTER TABLE [dbo].[mstDocumentType] WITH NOCHECK
    ADD CONSTRAINT [FK_mstDocumentType_mstCompany] FOREIGN KEY ([fk_CompanyID]) REFERENCES [dbo].[mstCompany] ([pk_CompanyID]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_mstExchangeRate_mstCompany...';


GO
ALTER TABLE [dbo].[mstExchangeRate] WITH NOCHECK
    ADD CONSTRAINT [FK_mstExchangeRate_mstCompany] FOREIGN KEY ([fk_CompanyID]) REFERENCES [dbo].[mstCompany] ([pk_CompanyID]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating [dbo].[spAddEditImportHaulageChrg]...';


GO
-- =============================================
-- Author:		RAJEN SAHA
-- Create date: 01/12/2012
-- Description:	VENDOR ADD/EDIT
-- =============================================
CREATE PROCEDURE [dbo].[spAddEditImportHaulageChrg]
	-- Add the parameters for the stored procedure here
	@HaulageChgID INT = 0,
	@LocationFrom int = 0,
	@LocationTo INT = 0,
	@ContainerSize varchar(2) = NULL,
	@WeightFrom decimal(12,2) = 0.0,
	@WeightTo decimal(12,2) = 0.0,
	@HaulageRate decimal(12,2) = 0.0,
	@HaulageStatus bit,
	
	@CreatedBy INT=0,
    @CreatedOn DATETIME = NULL,
    @ModifiedBy INT = 0,
    @ModifiedOn DATETIME = NULL,
    @RESULT INT OUT       
        
AS
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF(@HaulageChgID <= 0)
	BEGIN
		INSERT INTO dbo.mstHaulageCharge(
										pk_HaulageChgID,
										fk_LocationFrom,
										fk_LocationTo,
										ContainerSize,
										WeightFrom,
										WeightTo,
										HaulageRate,
										HaulageStatus,
										fk_UserAdded,										
										AddedOn,
										fk_UserLastEdited,
										EditedOn)
								VALUES(
										@HaulageChgID,
										@LocationFrom,
										@LocationTo,
										@ContainerSize,
										@WeightFrom,
										@WeightTo,
										@HaulageRate,
										@HaulageStatus,										
										@CreatedBy,
										@CreatedOn,
										@ModifiedBy,
										@ModifiedOn
										)	
		IF(@@ERROR = 0)
		BEGIN
			SET @RESULT = 1
		END
		ELSE
		BEGIN
			SET @RESULT = 0
		END													
	END
	ELSE
	BEGIN
		UPDATE dbo.mstHaulageCharge SET
				
				fk_LocationFrom = @LocationFrom ,
				fk_LocationTo = @LocationTo,
				ContainerSize = @ContainerSize,
				WeightFrom = @WeightFrom,
				WeightTo = @WeightTo,
				HaulageRate = @HaulageRate,
				HaulageStatus = @HaulageStatus,
				fk_UserLastEdited = @ModifiedBy,
				EditedOn = @ModifiedOn
				
				where pk_HaulageChgID = @HaulageChgID
		
		IF(@@ERROR = 0)
		BEGIN
			SET @RESULT = 1
		END
		ELSE
		BEGIN
			SET @RESULT = 0
		END				
	END
END TRY
BEGIN CATCH
		SET @RESULT = 0
END CATCH
GO
PRINT N'Creating [dbo].[spAddEditVendor]...';


GO
-- =============================================
-- Author:		RAJEN SAHA
-- Create date: 01/12/2012
-- Description:	VENDOR ADD/EDIT
-- =============================================
CREATE PROCEDURE [dbo].[spAddEditVendor]
	-- Add the parameters for the stored procedure here
	@VendorId INT = 0,
	@VendorType VARCHAR(5) = NULL,
	@LocationID INT = 0,
	@VendorSalutation INT = 0,
	@VendorName VARCHAR(250) = NULL,
	@VendorAddress VARCHAR(500) = NULL,
	@CFSCode VARCHAR(50) = NULL,
	@Terminalid int,
	@CompanyID INT=0,
	@VendorActive BIT = 1,
	@CreatedBy INT=0,
    @CreatedOn DATETIME = NULL,
    @ModifiedBy INT = 0,
    @ModifiedOn DATETIME = NULL,
    @RESULT INT OUT       
        
AS
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF(@VendorId <= 0)
	BEGIN
		INSERT INTO dbo.mstAddress(
									fk_CompanyID,
									fk_LocationID,
									AddrName,
									AddrAddress,
									AddrSalutation,
									AddrType,
									CFSCode,
									fk_terminalid,
									AddrActive,
									fk_UserAdded,
									fk_UserLastEdited,
									AddedOn,
									EditedOn
									)
							VALUES(	@CompanyID,
									@LocationID,
									@VendorName,
									@VendorAddress,
									@VendorSalutation,
									@VendorType,
									@CFSCode,
									@Terminalid,
									@VendorActive,		
									@CreatedBy,		
									@ModifiedBy,		
									@CreatedOn,
									@ModifiedOn)	
		IF(@@ERROR = 0)
		BEGIN
			SET @RESULT = 1
		END
		ELSE
		BEGIN
			SET @RESULT = 0
		END													
	END
	ELSE
	BEGIN
		UPDATE dbo.mstAddress SET
				fk_CompanyID=@CompanyID,
				fk_LocationID=@LocationID,
				AddrName=@VendorName,
				AddrAddress=@VendorAddress,
				AddrSalutation=@VendorSalutation,
				AddrType=@VendorType,
				CFSCode=@CFSCode,
				fk_terminalid = @Terminalid,
				AddrActive=@VendorActive,				
				fk_UserLastEdited=@ModifiedBy,				
				EditedOn=@ModifiedOn
				where fk_AddressID = @VendorId
		
		IF(@@ERROR = 0)
		BEGIN
			SET @RESULT = 1
		END
		ELSE
		BEGIN
			SET @RESULT = 0
		END				
	END
END TRY
BEGIN CATCH
		SET @RESULT = 0
END CATCH
GO
PRINT N'Creating [dbo].[spDeleteImportHaulageChrg]...';


GO
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
GO
PRINT N'Creating [dbo].[spDeleteVendor]...';


GO
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
GO
PRINT N'Creating [dbo].[spGetImportHaulageCharge]...';


GO
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
GO
PRINT N'Creating [dbo].[spGetVendor]...';


GO
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
GO
PRINT N'Creating [dbo].[spPopulateDropDownList]...';


GO
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
GO
PRINT N'Creating [dbo].[uspValidateUser]...';


GO
-- =============================================
-- Author		: Amit Kumar Chandra
-- Create date  : 08-Jul-2012
-- Description	: 
-- =============================================
CREATE PROCEDURE [dbo].[uspValidateUser]
	-- Add the parameters for the stored procedure here
(
	@UserName NVARCHAR(100),
	@Password NVARCHAR(100)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    DECLARE @IsActive BIT
    
   
    --IF @IsActive = 1
    --BEGIN
		SELECT	ur.pk_UserID AS 'UserId',
				ur.FirstName,
				ur.LastName,
				ur.emailID,
				ur.fk_RoleID AS 'RoleId',
				ur.fk_LocID AS 'LocId'
				--ur.SalesPersonType,
				--ro.SalesRole
		FROM	dbo.mstUsers ur
				INNER JOIN dbo.mstRoles ro
					ON ur.fk_RoleID = ro.pk_RoleID
				INNER JOIN 	DSR.dbo.mstLocation lo
					ON ur.fk_LocID = lo.pk_LocID
		WHERE	ur.[UserName] = @UserName 
		AND		ur.[Password] = @Password 
		AND 	ur.[UserActive] = 1
		AND		ur.IsDeleted = 0
		
	--END
	--ELSE
	--BEGIN
	--	SELECT	 0 AS 'UserId'
	--			,NULL AS 'UserFirstName'
	--			,NULL AS 'UserMiddleName'
	--			,NULL AS 'UserLastName'
	--			,0 AS 'IsAdmin'
	--			,'NA' AS 'Level'
	--			,0 AS 'ZoneID'
	--END
END
GO
PRINT N'Creating [dbo].[ImpBLFooter].[Temperature].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'for reefer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLFooter', @level2type = N'COLUMN', @level2name = N'Temperature';


GO
PRINT N'Creating [dbo].[ImpBLFooter].[TempUnit].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'C-Centegrate / F-farenhight', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLFooter', @level2type = N'COLUMN', @level2name = N'TempUnit';


GO
PRINT N'Creating [dbo].[ImpBLHeader].[MBLNumber].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default will be BLNo with user option to change', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'MBLNumber';


GO
PRINT N'Creating [dbo].[ImpBLHeader].[MBLDate].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default will be BL Date with user option to change', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'MBLDate';


GO
PRINT N'Creating [dbo].[ImpBLHeader].[fk_StockLocationID].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DEFAULT CURRENT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'fk_StockLocationID';


GO
PRINT N'Creating [dbo].[ImpBLHeader].[CargoNature].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Generally C but can be DB or LB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'CargoNature';


GO
PRINT N'Creating [dbo].[ImpBLHeader].[CargoMovement].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'LC /  TC / IT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'CargoMovement';


GO
PRINT N'Creating [dbo].[ImpBLHeader].[ItemType].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Generally OT but can be GC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'ItemType';


GO
PRINT N'Creating [dbo].[ImpBLHeader].[CargoType].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'L-LCL / F-FCL / E-ETY', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'CargoType';


GO
PRINT N'Creating [dbo].[ImpBLHeader].[UNOCode].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'When Haz. Cargo=0, it will be ZZZZZ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'UNOCode';


GO
PRINT N'Creating [dbo].[ImpBLHeader].[IMOCode].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'When Haz. Cargo="N", it will be ZZZ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'IMOCode';


GO
PRINT N'Creating [dbo].[ImpBLHeader].[MarksNumbers].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default will be N/M (No Mark)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'MarksNumbers';


GO
PRINT N'Creating [dbo].[ImpBLHeader].[fk_AddressCFSId].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'For CFS from address master', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'fk_AddressCFSId';


GO
PRINT N'Creating [dbo].[ImpBLHeader].[DetentionSlabType].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'R-Regular / O-Override', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'DetentionSlabType';


GO
PRINT N'Creating [dbo].[ImpBLHeader].[FreightType].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PP - Pre Paid / CC - To collect', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'FreightType';


GO
PRINT N'Creating [dbo].[ImpBLHeader].[FreigthToCollect].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'For CC this should be in USD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'FreigthToCollect';


GO
PRINT N'Creating [dbo].[mstAddress].[AddrType].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IC-Imp. Consignee / IN - Import Notify / OS -Overseas Agent / EY - Empty Yard / CF - CFS / IC - ICD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mstAddress', @level2type = N'COLUMN', @level2name = N'AddrType';


GO
PRINT N'Creating [dbo].[mstAddress].[fk_terminalid].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mstAddress', @level2type = N'COLUMN', @level2name = N'fk_terminalid';


GO
PRINT N'Creating [dbo].[mstCharges].[IEC].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'I-Import / E-Export / C-COMMON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mstCharges', @level2type = N'COLUMN', @level2name = N'IEC';


GO
PRINT N'Creating [dbo].[mstCompany].[ActivityNature].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'B-Bulk / C-Container', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mstCompany', @level2type = N'COLUMN', @level2name = N'ActivityNature';


GO
PRINT N'Creating [dbo].[mstCompany].[InstType].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'I-Import / E-Export / E-Equipment / A-All', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mstCompany', @level2type = N'COLUMN', @level2name = N'InstType';


GO
PRINT N'Creating [dbo].[mstDocumentType].[DocType].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'I - Import / E-Export', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mstDocumentType', @level2type = N'COLUMN', @level2name = N'DocType';


GO
PRINT N'Creating [dbo].[mstDocumentType].[Calculation].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'P-Posiitive / N-Negative', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mstDocumentType', @level2type = N'COLUMN', @level2name = N'Calculation';


GO
PRINT N'Creating [dbo].[mstDocumentType].[ResetPeriod].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'M-Monthly / Y-Yearly / D-Daily', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mstDocumentType', @level2type = N'COLUMN', @level2name = N'ResetPeriod';


GO
PRINT N'Creating [dbo].[mstModules].[ModuleType].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'E-Entry/P-Process/R-Report', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mstModules', @level2type = N'COLUMN', @level2name = N'ModuleType';


GO
PRINT N'Creating [dbo].[mstNVOCC].[ContAgentCode].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'For ICEGate EDI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mstNVOCC', @level2type = N'COLUMN', @level2name = N'ContAgentCode';


GO
PRINT N'Creating [dbo].[mstPort].[ICDIndicator].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0-No / 1- Yes  (For India port only where Left(IGCode,2)=''IN''', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mstPort', @level2type = N'COLUMN', @level2name = N'ICDIndicator';


GO
PRINT N'Creating [dbo].[mstRoles].[RollType].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A-Admin / U-User/C-CFS/E-Empty Yard/Surveyors', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mstRoles', @level2type = N'COLUMN', @level2name = N'RollType';


GO
PRINT N'Creating [dbo].[trnVoyage].[VesselType].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'C - With Cargo / E - Empty', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'trnVoyage', @level2type = N'COLUMN', @level2name = N'VesselType';


GO
PRINT N'Creating [dbo].[trnVoyage].[LightHouseDue].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0-No (Default) / 1-Yes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'trnVoyage', @level2type = N'COLUMN', @level2name = N'LightHouseDue';


GO
PRINT N'Creating [dbo].[trnVoyage].[SameButtonCargo].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0 (Default) - No / 1 - Yes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'trnVoyage', @level2type = N'COLUMN', @level2name = N'SameButtonCargo';


GO
PRINT N'Creating [dbo].[trnVoyage].[ShipStoreSubmitted].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0 - No / 1 - Yes (Default)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'trnVoyage', @level2type = N'COLUMN', @level2name = N'ShipStoreSubmitted';


GO
PRINT N'Creating [dbo].[trnVoyage].[CrewList].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0-No / 1-Yes (Default)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'trnVoyage', @level2type = N'COLUMN', @level2name = N'CrewList';


GO
PRINT N'Creating [dbo].[trnVoyage].[PassengerList].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0 - No (Default) / 1 - Yes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'trnVoyage', @level2type = N'COLUMN', @level2name = N'PassengerList';


GO
PRINT N'Creating [dbo].[trnVoyage].[CrewEffectList].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0-No / Y-Yes (Default)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'trnVoyage', @level2type = N'COLUMN', @level2name = N'CrewEffectList';


GO
PRINT N'Creating [dbo].[trnVoyage].[MaritimeList].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0-No / 1-Yes (Default)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'trnVoyage', @level2type = N'COLUMN', @level2name = N'MaritimeList';


GO
-- Refactoring step to update target server with deployed transaction logs
CREATE TABLE  [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
GO
sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
GO

GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[mstAddress] WITH CHECK CHECK CONSTRAINT [FK_mstAddress_mstCompany];

ALTER TABLE [dbo].[mstCharges] WITH CHECK CHECK CONSTRAINT [FK_mstCharges_mstCompany];

ALTER TABLE [dbo].[mstChargesRate] WITH CHECK CHECK CONSTRAINT [FK_mstChargesRate_mstCharges];

ALTER TABLE [dbo].[mstDocumentType] WITH CHECK CHECK CONSTRAINT [FK_mstDocumentType_mstCompany];

ALTER TABLE [dbo].[mstExchangeRate] WITH CHECK CHECK CONSTRAINT [FK_mstExchangeRate_mstCompany];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        DECLARE @VarDecimalSupported AS BIT;
        SELECT @VarDecimalSupported = 0;
        IF ((ServerProperty(N'EngineEdition') = 3)
            AND (((@@microsoftversion / power(2, 24) = 9)
                  AND (@@microsoftversion & 0xffff >= 3024))
                 OR ((@@microsoftversion / power(2, 24) = 10)
                     AND (@@microsoftversion & 0xffff >= 1600))))
            SELECT @VarDecimalSupported = 1;
        IF (@VarDecimalSupported > 0)
            BEGIN
                EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
            END
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET MULTI_USER 
    WITH ROLLBACK IMMEDIATE;


GO
