ALTER TABLE [dbo].[mstAddress]
    ADD CONSTRAINT [DF_mstAddress_AddrType] DEFAULT ((0)) FOR [AddrType];

