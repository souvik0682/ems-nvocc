ALTER TABLE [dbo].[ImpBLHeader]
    ADD CONSTRAINT [DF_trn_BLHeader_TaxExemption] DEFAULT ((0)) FOR [TaxExemption];

