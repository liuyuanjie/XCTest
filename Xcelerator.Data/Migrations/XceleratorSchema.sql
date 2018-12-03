USE [Xcelerator]

CREATE TABLE [dbo].[Organization](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	CONSTRAINT [PK_Organization] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
)
GO

CREATE NONCLUSTERED INDEX [IX_Organization_Name]
	ON [dbo].[Organization]([Name])
GO

CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[NormalizedUserName] [nvarchar](50) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](256) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[JobTitle] [nvarchar](50) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[IsEnabled] [bit] NOT NULL,
	[OrganizationId] [int] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	),
	CONSTRAINT UX_User_Email UNIQUE
	(Email),
	CONSTRAINT UX_User_UserName UNIQUE
	(UserName),
	CONSTRAINT FK_User_Organization_OrganizationId_Id FOREIGN KEY 
	(OrganizationId) REFERENCES [dbo].[Organization](Id) ON DELETE NO ACTION
)
GO

CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Description] [nvarchar](1024) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
)
GO

CREATE NONCLUSTERED INDEX [IX_Role_Name]
	ON [dbo].[Role]([Name])
GO

CREATE TABLE [dbo].[Template](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[OrganizationId] [int] NULL,
	[Description] [nvarchar](1024) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	CONSTRAINT [PK_Template] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	),
	CONSTRAINT FK_Template_Organization_OrganizationId_Id FOREIGN KEY 
	(OrganizationId) REFERENCES [dbo].[Organization](Id) ON DELETE NO ACTION
)
GO

CREATE TABLE [dbo].[Audit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[TemplateId] [int] NOT NULL,
	[OrganizationId] [int] NULL,
	[Status] [TINYINT] NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	CONSTRAINT [PK_Audits] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	),
	CONSTRAINT FK_Audit_Template_TemplateId_Id FOREIGN KEY 
	(TemplateId) REFERENCES [dbo].[Template](Id) ON DELETE NO ACTION,
	CONSTRAINT FK_Audit_Organization_OrganizationId_Id FOREIGN KEY 
	(OrganizationId) REFERENCES [dbo].[Organization](Id) ON DELETE NO ACTION
)
GO

CREATE TABLE [dbo].[RoleClaim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[ClaimType] [nvarchar](256) NULL,
	[ClaimValue] [nvarchar](256) NULL,
	CONSTRAINT PK_RoleClaim PRIMARY KEY CLUSTERED
		([Id]),
	CONSTRAINT FK_RoleClaim_Role_RoleId_Id FOREIGN KEY 
		(RoleId) REFERENCES [dbo].[Role](Id) ON DELETE CASCADE
 )
GO

CREATE NONCLUSTERED INDEX [IX_RoleClaims_RoleId]
	ON [dbo].[RoleClaim]([RoleId])
GO

CREATE TABLE [dbo].[UserClaim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ClaimType] [nvarchar](256) NULL,
	[ClaimValue] [nvarchar](256) NULL,
	CONSTRAINT PK_UserClaim PRIMARY KEY CLUSTERED
		([Id]),
	CONSTRAINT FK_UserClaim_User_UserId_Id FOREIGN KEY 
		(UserId) REFERENCES [dbo].[User](Id) ON DELETE CASCADE
 )
GO

CREATE NONCLUSTERED INDEX [IX_UserClaims_UserId]
	ON [dbo].[UserClaim]([UserId])
GO

CREATE TABLE [dbo].[UserLogin](
	[LoginProvider] [nvarchar](256) NOT NULL,
	[ProviderKey] [nvarchar](256) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [int] NOT NULL,
	 CONSTRAINT [PK_AspNetUserLogin] PRIMARY KEY CLUSTERED 
	(
		[LoginProvider] ASC,
		[ProviderKey] ASC
	),
	CONSTRAINT FK_UserLogin_User_UserId_Id FOREIGN KEY 
		(UserId) REFERENCES [dbo].[User](Id) ON DELETE CASCADE
)
GO

CREATE NONCLUSTERED INDEX [IX_AspNetUserLogin_UserId]
	ON [dbo].[UserLogin]([UserId])
GO

CREATE TABLE [dbo].[UserToken](
	[UserId] [int] NOT NULL,
	[LoginProvider] [nvarchar](256) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Value] [nvarchar](max) NULL,
	CONSTRAINT [PK_UserToken] PRIMARY KEY CLUSTERED 
	(
		[UserId] ASC,
		[LoginProvider] ASC,
		[Name] ASC
	),
	CONSTRAINT FK_UserToken_User_UserId_Id FOREIGN KEY 
	(UserId) REFERENCES [dbo].[User](Id) ON DELETE CASCADE
)
GO

CREATE TABLE [dbo].[AuditQuestion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AuditId] [int] NOT NULL,
	[QuestionId] [int] NOT NULL,
	[AssignedUserId] [int] NOT NULL,
	[Result] [nvarchar](256) NULL,
	[Description] [nvarchar](1024) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	CONSTRAINT [PK_AuditQuestion] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	),
	CONSTRAINT FK_AuditQuestion_User_AssignedUserId_Id FOREIGN KEY 
	(AssignedUserId) REFERENCES [dbo].[User](Id) ON DELETE NO ACTION,
	CONSTRAINT FK_AuditQuestion_Audit_AuditId_Id FOREIGN KEY 
	(AuditId) REFERENCES [dbo].[Audit](Id) ON DELETE NO ACTION
)
GO

CREATE TABLE [dbo].[AuditUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AuditId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	CONSTRAINT [PK_AuditUser] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	),
	CONSTRAINT UX_AuditUser_AuditId_UserId UNIQUE
	(AuditId, UserId),
	CONSTRAINT FK_AuditUser_Audit_AuditId_Id FOREIGN KEY 
	(AuditId) REFERENCES [dbo].[Audit](Id) ON DELETE CASCADE,
	CONSTRAINT FK_AuditUser_User_UserId_Id FOREIGN KEY 
	(UserId) REFERENCES [dbo].[User](Id) ON DELETE NO ACTION
)
GO

CREATE TABLE [dbo].[UserRole](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
	(
		[UserId] ASC,
		[RoleId] ASC
	),
	CONSTRAINT FK_UserRole_User_UserId_Id FOREIGN KEY 
	(UserId) REFERENCES [dbo].[User](Id) ON DELETE CASCADE,
	CONSTRAINT FK_UserRole_Role_RoleId_Id FOREIGN KEY 
	(RoleId) REFERENCES [dbo].[Role](Id) ON DELETE CASCADE
)
GO