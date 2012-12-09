ALTER TABLE [dbo].[trnVoyage]
    ADD CONSTRAINT [DF_trn_Voyage_PassengerList] DEFAULT ((0)) FOR [PassengerList];

