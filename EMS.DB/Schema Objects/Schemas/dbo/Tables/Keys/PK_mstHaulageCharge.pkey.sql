﻿ALTER TABLE [dbo].[mstHaulageCharge]
    ADD CONSTRAINT [PK_mstHaulageCharge] PRIMARY KEY CLUSTERED ([pk_HaulageChgID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
