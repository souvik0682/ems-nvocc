﻿ALTER TABLE [dbo].[mstExchangeRate]
    ADD CONSTRAINT [PK_mstExchangeRate] PRIMARY KEY CLUSTERED ([fk_ExchRateID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
