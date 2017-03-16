USE [C27]
GO

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
