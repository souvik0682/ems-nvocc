ALTER TABLE [dbo].[mstAddress]
    ADD CONSTRAINT [DF_mstAddress_fk_terminalid] DEFAULT ((-1)) FOR [fk_terminalid];

