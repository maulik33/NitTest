IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspAssignQuestion]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspAssignQuestion]
GO

/****** Object:  StoredProcedure [dbo].[uspAssignQuestion]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[uspAssignQuestion]    Script Date: 05/23/2011  ******/
CREATE PROCEDURE [dbo].[uspAssignQuestion]
@TestId INT,
@QuestionId INT ,
@QuestionNumber INT ,
@Active INT
AS
	BEGIN
	SET NOCOUNT ON
		DECLARE @v_TestCount INT
		SELECT @v_TestCount = 0
		SELECT @v_TestCount = Count(TestId)
		FROM TestQuestions
		WHERE QID = @QuestionId
		AND TestID = @TestId		
			IF(@Active = 1)				
				BEGIN
					
					IF @v_TestCount = 0
						BEGIN					
							INSERT INTO TestQuestions
							(
								TestID,
								QID,
								QuestionNumber
							 )
							Values
							(
								@TestId,
								@QuestionId,
								@QuestionNumber
							)
						END
					ELSE
						BEGIN
							
							UPDATE  TestQuestions
							SET QuestionNumber = @QuestionNumber
							WHERE TestID = @TestId
							AND QID = @QuestionId
						END
					END
				ELSE
					BEGIN
						IF @v_TestCount > 0
						BEGIN	
								DELETE FROM  TestQuestions
								WHERE TestID = @TestId
								AND QID = @QuestionId
						END
					END					
	SET NOCOUNT OFF
	END
GO
