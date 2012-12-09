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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'For ICEGate EDI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mstNVOCC', @level2type = N'COLUMN', @level2name = N'ContAgentCode';

