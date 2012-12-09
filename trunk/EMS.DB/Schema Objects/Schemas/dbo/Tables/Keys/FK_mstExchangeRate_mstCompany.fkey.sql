ALTER TABLE [dbo].[mstExchangeRate]
    ADD CONSTRAINT [FK_mstExchangeRate_mstCompany] FOREIGN KEY ([fk_CompanyID]) REFERENCES [dbo].[mstCompany] ([pk_CompanyID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

