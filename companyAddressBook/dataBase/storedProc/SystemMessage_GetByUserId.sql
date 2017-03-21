USE [C27]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER Procedure [dbo].[Address_Select_ByCompanyAndType]
        @companyId int 
      , @addressType int 
As
Begin
SELECT [Id]
      ,[CompanyId]
      ,[Date]
      ,[Address1]
      ,[City]
      ,[State]
      ,[ZipCode]
      ,[Latitude]
      ,[Longitude]
      ,[Slug]
      ,[AddressType]
  FROM [dbo].[Addresses]
  where CompanyId = @companyId 
  AND AddressType = @addressType
  End