ALTER TABLE [dbo].[mstAddress]
    ADD CONSTRAINT [FK_mstAddress_mstCompany] FOREIGN KEY ([fk_CompanyID]) REFERENCES [dbo].[mstCompany] ([pk_CompanyID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

