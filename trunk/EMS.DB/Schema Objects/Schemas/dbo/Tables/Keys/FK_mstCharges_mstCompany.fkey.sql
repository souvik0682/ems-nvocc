ALTER TABLE [dbo].[mstCharges]
    ADD CONSTRAINT [FK_mstCharges_mstCompany] FOREIGN KEY ([fk_CompanyID]) REFERENCES [dbo].[mstCompany] ([pk_CompanyID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

