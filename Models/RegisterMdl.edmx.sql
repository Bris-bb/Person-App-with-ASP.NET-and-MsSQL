
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/22/2018 11:05:13
-- Generated from EDMX file: D:\7star\asp.net mvc\PersonApp\PersonApp\Models\RegisterMdl.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [RegisterDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_custom_setting_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[custom_setting] DROP CONSTRAINT [FK_custom_setting_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_Event_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_Notify_Personal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notify] DROP CONSTRAINT [FK_Notify_Personal];
GO
IF OBJECT_ID(N'[dbo].[FK_Personal_Personal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Personal] DROP CONSTRAINT [FK_Personal_Personal];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[custom_setting]', 'U') IS NOT NULL
    DROP TABLE [dbo].[custom_setting];
GO
IF OBJECT_ID(N'[dbo].[Event]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Event];
GO
IF OBJECT_ID(N'[dbo].[Notify]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notify];
GO
IF OBJECT_ID(N'[dbo].[Personal]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Personal];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'Notifies'
CREATE TABLE [dbo].[Notifies] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Notify1] nvarchar(2000)  NOT NULL,
    [Personal_Id] int  NULL
);
GO

-- Creating table 'Personals'
CREATE TABLE [dbo].[Personals] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Insider_Name] nvarchar(200)  NOT NULL,
    [Insider_Name_arabic] nvarchar(200)  NULL,
    [Company_Name] nvarchar(200)  NOT NULL,
    [Company_Name_arabic] nvarchar(200)  NULL,
    [Position] nvarchar(200)  NOT NULL,
    [Position_arabic] nvarchar(200)  NULL,
    [UAE_Resident] nvarchar(200)  NOT NULL,
    [UAE_Resident_arabic] nvarchar(200)  NULL,
    [Emirates_ID] nvarchar(200)  NOT NULL,
    [Passport_Number] nvarchar(200)  NOT NULL,
    [Mobile_Number] nvarchar(200)  NOT NULL,
    [Office_Number] nvarchar(200)  NULL,
    [User_Id] nvarchar(128)  NOT NULL,
    [Email_Address_2] nvarchar(200)  NULL,
    [Trade_License] nvarchar(200)  NOT NULL,
    [Security_Code] nvarchar(200)  NULL,
    [Other_information] nvarchar(200)  NULL,
    [ID_Expire_Date] datetime  NULL,
    [ID_Upload] nvarchar(200)  NULL,
    [Passport_Expire_Date] datetime  NULL,
    [Passport_Upload] nvarchar(200)  NULL,
    [Company_TL_Expire_Date] datetime  NULL,
    [Comany_TL_Upload] nvarchar(200)  NULL,
    [status] nvarchar(200)  NULL,
    [Comany_Image_Upload] nvarchar(200)  NULL
);
GO

-- Creating table 'Events'
CREATE TABLE [dbo].[Events] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [User_ID] nvarchar(128)  NULL,
    [title] nvarchar(100)  NULL,
    [description] nvarchar(max)  NULL,
    [date] datetime  NULL,
    [end_date] datetime  NULL,
    [url] nvarchar(200)  NULL
);
GO

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [RoleId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'custom_setting'
CREATE TABLE [dbo].[custom_setting] (
    [ID] nvarchar(128)  NOT NULL,
    [admin_email] nvarchar(128)  NOT NULL,
    [smtp_domain] nvarchar(50)  NULL,
    [port] int  NULL,
    [notify_email] nvarchar(50)  NULL,
    [theme] nvarchar(100)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [ID] in table 'Notifies'
ALTER TABLE [dbo].[Notifies]
ADD CONSTRAINT [PK_Notifies]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Personals'
ALTER TABLE [dbo].[Personals]
ADD CONSTRAINT [PK_Personals]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [PK_Events]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ID] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ID] in table 'custom_setting'
ALTER TABLE [dbo].[custom_setting]
ADD CONSTRAINT [PK_custom_setting]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Personal_Id] in table 'Notifies'
ALTER TABLE [dbo].[Notifies]
ADD CONSTRAINT [FK_Notify_Personal]
    FOREIGN KEY ([Personal_Id])
    REFERENCES [dbo].[Personals]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Notify_Personal'
CREATE INDEX [IX_FK_Notify_Personal]
ON [dbo].[Notifies]
    ([Personal_Id]);
GO

-- Creating foreign key on [RoleId] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId'
CREATE INDEX [IX_FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId]
ON [dbo].[AspNetUserRoles]
    ([RoleId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserRoles]
    ([UserId]);
GO

-- Creating foreign key on [User_ID] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [FK_Event_AspNetUsers]
    FOREIGN KEY ([User_ID])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Event_AspNetUsers'
CREATE INDEX [IX_FK_Event_AspNetUsers]
ON [dbo].[Events]
    ([User_ID]);
GO

-- Creating foreign key on [User_Id] in table 'Personals'
ALTER TABLE [dbo].[Personals]
ADD CONSTRAINT [FK_Personal_Personal]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Personal_Personal'
CREATE INDEX [IX_FK_Personal_Personal]
ON [dbo].[Personals]
    ([User_Id]);
GO

-- Creating foreign key on [ID] in table 'custom_setting'
ALTER TABLE [dbo].[custom_setting]
ADD CONSTRAINT [FK_custom_setting_AspNetUsers]
    FOREIGN KEY ([ID])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------