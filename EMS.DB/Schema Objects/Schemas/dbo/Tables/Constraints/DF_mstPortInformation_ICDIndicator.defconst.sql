ALTER TABLE [dbo].[mstPort]
    ADD CONSTRAINT [DF_mstPortInformation_ICDIndicator] DEFAULT ((0)) FOR [ICDIndicator];

