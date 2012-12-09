CREATE TABLE [dbo].[mstAddressType] (
    [pk_AddrTypeID] INT          IDENTITY (1, 1) NOT NULL,
    [AddrType]      CHAR (2)     NOT NULL,
    [AddrTypeDesc]  VARCHAR (25) NOT NULL
);

