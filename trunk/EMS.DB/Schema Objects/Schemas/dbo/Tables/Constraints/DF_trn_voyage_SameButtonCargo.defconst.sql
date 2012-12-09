ALTER TABLE [dbo].[trnVoyage]
    ADD CONSTRAINT [DF_trn_voyage_SameButtonCargo] DEFAULT ((0)) FOR [SameButtonCargo];

