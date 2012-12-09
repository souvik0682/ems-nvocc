ALTER TABLE [dbo].[ImpBLHeader]
    ADD CONSTRAINT [DF_trn_BLHeader_HazFlag] DEFAULT ((0)) FOR [HazFlag];

