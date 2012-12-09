ALTER TABLE [dbo].[mstAddress]
    ADD CONSTRAINT [DF_mstAddress_AddrActive] DEFAULT ((1)) FOR [AddrActive];

