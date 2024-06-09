CREATE TABLE [dbo].[Users]
(
	[id] INT NOT NULL PRIMARY KEY,
	[name] VARCHAR(255) NOT NULL,
)

CREATE TABLE [dbo].[Products]
(
	[id] INT NOT NULL PRIMARY KEY,
	[name] VARCHAR(255) NOT NULL,
	[price] FLOAT NOT NULL,
)

CREATE TABLE [dbo].[WarehouseEntries]
(
	[id] INT NOT NULL PRIMARY KEY, 
    [productId] INT NOT NULL, 
    [quantity] INT NOT NULL,

    CONSTRAINT [FK_WarehouseEntries_Products] FOREIGN KEY ([productId]) REFERENCES [dbo].[Products] ([id])
)

CREATE TABLE [dbo].[Events]
(
	[id] INT NOT NULL PRIMARY KEY, 
    [stateId] INT NOT NULL, 
    [userId] INT NOT NULL, 
    [date] DATE NOT NULL,
    [quantity] INT NOT NULL

    CONSTRAINT [FK_Events_Users] FOREIGN KEY ([userId]) REFERENCES [dbo].[Users] ([id]),
    CONSTRAINT [FK_Events_WarehouseEntries] FOREIGN KEY ([stateId]) REFERENCES [dbo].[WarehouseEntries] ([id])
)