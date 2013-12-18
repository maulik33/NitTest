IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCheckSystem]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspCheckSystem]
GO

/****** Object:  StoredProcedure [dbo].[uspCheckSystem]    Script Date: 10/21/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspCheckSystem]
	@IsProductionApp bit,
	@ReturnValue varchar(200) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	SET @ReturnValue = ''

	--	Return Codes : Begin
	--	1: Updated Environment Id
	--	2: Inserted Default CFR Test

	--	Return Codes : End


	IF EXISTS(SELECT 1
		FROM dbo.AppSettings
		WHERE SettingsId = 5
		AND CONVERT(int, [Value]) != CONVERT(int, @IsProductionApp))
	BEGIN
		UPDATE dbo.AppSettings
		SET [Value] = CASE @IsProductionApp WHEN 1 THEN '1' ELSE '2' END
		WHERE SettingsId = 5

		SET @ReturnValue = '1|'
	END

	IF @IsProductionApp = 1
		AND NOT EXISTS(SELECT 1
		FROM dbo.Tests
		WHERE ReleaseStatus = 'F')
	BEGIN
		DECLARE @CFRTestIdOffset int
		SELECT @CFRTestIdOffset = CONVERT(int, [Value])
		FROM dbo.AppSettings
		WHERE SettingsId = 4

		SET @ReturnValue = @ReturnValue + '2|'

		SET IDENTITY_INSERT dbo.Tests ON

		INSERT INTO Tests(
			TestId,
			TestName,
			ProductID,
			DefaultGroup,
			ReleaseStatus,
			ActiveTest,
			TestSubGroup)
		VALUES(
			@CFRTestIdOffset + 1,
			'Cardiovascular.1',
			3,
			0,
			'F',
			1,
			1)

		SET IDENTITY_INSERT dbo.Tests OFF
	END

END
GO
