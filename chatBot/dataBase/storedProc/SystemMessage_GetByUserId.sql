USE [C27]
GO
/****** Object:  StoredProcedure [dbo].[SystemMessage_GetByUserId]    Script Date: 16-Mar-17 16:23:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER Procedure [dbo].[SystemMessage_GetByUserId]
@userId nvarchar(128)

As
Begin
SELECT [id]
      ,[humanId]
      ,[content]
      ,[createDate]
	  ,(SELECT firstName + ' ' + lastName FROM [UserProfile] WHERE [UserProfile].userId = [SystemMessage].humanId) AS humanFullName
	  ,(SELECT url FROM [Media] JOIN [UserProfile] ON [UserProfile].userId = [SystemMessage].humanId WHERE [Media].id = [UserProfile].mediaId) AS humanUrl
	  ,[isSender]
  FROM [dbo].[SystemMessage]
  where humanId = @userId
  End
