CREATE TABLE [dbo].[Fruit]
(
	[FruitID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FruitName] NVARCHAR(50) NOT NULL, 
    [FruitGrowsOnID] INT NOT NULL, 
    CONSTRAINT [FK_Fruit_FruitGrowsOn] FOREIGN KEY ([FruitGrowsOnID]) 
	REFERENCES [FruitGrowsOn]([FruitGrowsOnID])
)
