ALTER TABLE [dbo].[mstUOM]
    ADD CONSTRAINT [DF_mstUOM_UnitStatus] DEFAULT ((1)) FOR [UnitStatus];

