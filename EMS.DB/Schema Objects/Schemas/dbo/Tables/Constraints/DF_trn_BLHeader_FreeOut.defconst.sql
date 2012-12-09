ALTER TABLE [dbo].[ImpBLHeader]
    ADD CONSTRAINT [DF_trn_BLHeader_FreeOut] DEFAULT ((0)) FOR [FreeOut];

