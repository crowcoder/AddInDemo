CREATE TABLE [dbo].[FruitGrowsOn]
(
	[FruitGrowsOnID] INT NOT NULL PRIMARY KEY IDENTITY,
	[FruitGrowsOnName] nvarchar(50) NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'fruit grows on id',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FruitGrowsOn',
    @level2type = N'COLUMN',
    @level2name = N'FruitGrowsOnID'