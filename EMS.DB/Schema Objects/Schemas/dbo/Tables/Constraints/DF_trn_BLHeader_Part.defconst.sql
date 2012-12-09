ALTER TABLE [dbo].[ImpBLHeader]
    ADD CONSTRAINT [DF_trn_BLHeader_Part] DEFAULT ((0)) FOR [PartBL];

