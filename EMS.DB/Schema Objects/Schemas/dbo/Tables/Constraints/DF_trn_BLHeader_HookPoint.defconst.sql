ALTER TABLE [dbo].[ImpBLHeader]
    ADD CONSTRAINT [DF_trn_BLHeader_HookPoint] DEFAULT ((0)) FOR [HookPoint];

