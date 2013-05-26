
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 01/19/2013 16:06:59
-- Generated from EDMX file: d:\my documents\visual studio 2012\Projects\WorldMessages\WorldMessages.Database\WorldMessagesDatabase.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [WorldMessages];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ConversationUser_Conversation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConversationUser] DROP CONSTRAINT [FK_ConversationUser_Conversation];
GO
IF OBJECT_ID(N'[dbo].[FK_ConversationUser_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConversationUser] DROP CONSTRAINT [FK_ConversationUser_User];
GO
IF OBJECT_ID(N'[dbo].[FK_ConversationMessage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MessageSet] DROP CONSTRAINT [FK_ConversationMessage];
GO
IF OBJECT_ID(N'[dbo].[FK_MessageUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MessageSet] DROP CONSTRAINT [FK_MessageUser];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[ConversationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConversationSet];
GO
IF OBJECT_ID(N'[dbo].[MessageSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MessageSet];
GO
IF OBJECT_ID(N'[dbo].[ConversationUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConversationUser];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ConversationSet'
CREATE TABLE [dbo].[ConversationSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MessageSet'
CREATE TABLE [dbo].[MessageSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] nvarchar(max)  NOT NULL,
    [Text] nvarchar(max)  NOT NULL,
    [Conversation_Id] int  NOT NULL,
    [User_Id] int  NOT NULL
);
GO

-- Creating table 'ConversationUser'
CREATE TABLE [dbo].[ConversationUser] (
    [Conversation_Id] int  NOT NULL,
    [User_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ConversationSet'
ALTER TABLE [dbo].[ConversationSet]
ADD CONSTRAINT [PK_ConversationSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MessageSet'
ALTER TABLE [dbo].[MessageSet]
ADD CONSTRAINT [PK_MessageSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Conversation_Id], [User_Id] in table 'ConversationUser'
ALTER TABLE [dbo].[ConversationUser]
ADD CONSTRAINT [PK_ConversationUser]
    PRIMARY KEY NONCLUSTERED ([Conversation_Id], [User_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Conversation_Id] in table 'ConversationUser'
ALTER TABLE [dbo].[ConversationUser]
ADD CONSTRAINT [FK_ConversationUser_Conversation]
    FOREIGN KEY ([Conversation_Id])
    REFERENCES [dbo].[ConversationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [User_Id] in table 'ConversationUser'
ALTER TABLE [dbo].[ConversationUser]
ADD CONSTRAINT [FK_ConversationUser_User]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ConversationUser_User'
CREATE INDEX [IX_FK_ConversationUser_User]
ON [dbo].[ConversationUser]
    ([User_Id]);
GO

-- Creating foreign key on [Conversation_Id] in table 'MessageSet'
ALTER TABLE [dbo].[MessageSet]
ADD CONSTRAINT [FK_ConversationMessage]
    FOREIGN KEY ([Conversation_Id])
    REFERENCES [dbo].[ConversationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ConversationMessage'
CREATE INDEX [IX_FK_ConversationMessage]
ON [dbo].[MessageSet]
    ([Conversation_Id]);
GO

-- Creating foreign key on [User_Id] in table 'MessageSet'
ALTER TABLE [dbo].[MessageSet]
ADD CONSTRAINT [FK_MessageUser]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MessageUser'
CREATE INDEX [IX_FK_MessageUser]
ON [dbo].[MessageSet]
    ([User_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------