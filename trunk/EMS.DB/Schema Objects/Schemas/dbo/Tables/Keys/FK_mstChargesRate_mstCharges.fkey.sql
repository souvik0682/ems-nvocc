ALTER TABLE [dbo].[mstChargesRate]
    ADD CONSTRAINT [FK_mstChargesRate_mstCharges] FOREIGN KEY ([fk_ChargesID]) REFERENCES [dbo].[mstCharges] ([pk_ChargesID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

