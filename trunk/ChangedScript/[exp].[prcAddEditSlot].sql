USE [Liner]
GO

/****** Object:  StoredProcedure [exp].[prcAddEditSlot]    Script Date: 05/01/2013 21:17:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 
CREATE proc [exp].[prcAddEditSlot]    
@userID int,      
@isedit bit,
@pk_SlotID int,      
@fk_CompanyID int,    
@fk_SlotOperatorID int,
@fk_LineID int,
@fk_POD bigint, 
@fk_POL bigint,
@EffDate Date,
@PODterminal Varchar(50),
@SlotStatus bit,
@fk_MovOrg int,
@fk_MovDst int,
@Result Int OutPut
as    
  begin    
  
	if @isedit=1     
		begin    
		  update exp.mstSlot    
		  set
			fk_CompanyID		=@fk_CompanyID, 
			fk_POD				= @fk_POD,
			fk_POL				= @fk_POL,
			PODTerminal			= @PODterminal,
			fk_SlotOperatorID	= @fk_SlotOperatorID,
			effDate				= @EffDate,
			fk_LineID			= @fk_LineID,
			fk_UserEdited		= @userID,
			fk_MovOrigin=@fk_MovOrg ,
		fk_MovDest=@fk_MovDst,
			dtEdited			= GETDATE()    
			where pk_Slotid=@pk_SlotID
			IF(@@ERROR = 0)
				BEGIN
					SET @RESULT = 1
				END
				ELSE
				BEGIN
					SET @RESULT = 0
				END				
		end    
	else    
		begin
		insert exp.mstSlot (    
		fk_CompanyID,    
		fk_POD,
		fk_POL,
		PODTerminal,
		fk_lineID,
		fk_SlotOperatorID,
		effdate,
		fk_UserAdded,
		dtAdded,
		fk_MovOrigin,
		fk_MovDest,
		SlotStatus
		)    
		values    
		(    
		@fk_CompanyID,    
		@fk_POD,
		@fk_POL,
		@PODterminal,
		@fk_LineID,
		@fk_SlotOperatorID,
		@EffDate,
		@userID,
		GETDATE(),
		@fk_MovOrg ,
@fk_MovDst ,
		1   
		)
		
		IF(@@ERROR = 0)
			BEGIN
				SET @RESULT = @@IDENTITY
			END
			ELSE
			BEGIN
				SET @RESULT = 0
			END		
		end			
  end    

GO


