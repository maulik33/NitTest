IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCustomizedFRTests]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetCustomizedFRTests]
GO
/****** Object:  StoredProcedure [dbo].[uspGetCustomizedFRTests]    Script Date: 10/19/2011 19:59:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetCustomizedFRTests]   
@UserId int,
@TimeOffset int 
AS  
BEGIN  
SELECT   UserTestID,UserId,dbo.UserTests.TestID,dbo.UserTests.TestNumber,
         DATEADD(hour, @TimeOffset, TestStarted) as TestStarted,dbo.UserTests.ProductID,dbo.Tests.TestName  
  FROM  UserTests   
  INNER JOIN Tests ON Tests.TestId = UserTests.TestId  
  WHERE UserID= @UserId AND IsCustomizedFRTest= 1 AND UserTests.ProductID= 3 AND Tests.ReleaseStatus ='F'  
  ORDER BY Teststarted Desc  
END  
GO


  

