ALTER TABLE [dbo].[mstUsers]
    ADD CONSTRAINT [DF_mstUsers_IsDeleted] DEFAULT ((0)) FOR [IsDeleted];

