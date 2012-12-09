ALTER TABLE [dbo].[mstDocumentType]
    ADD CONSTRAINT [FK_mstDocumentType_mstCompany] FOREIGN KEY ([fk_CompanyID]) REFERENCES [dbo].[mstCompany] ([pk_CompanyID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

