﻿ALTER TABLE [dbo].[ImpBLHeader]
    ADD CONSTRAINT [PK_trn_BLHeader_1] PRIMARY KEY CLUSTERED ([pk_BLID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
