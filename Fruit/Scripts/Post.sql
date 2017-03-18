/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
DECLARE @growsonids TABLE (id int, name nvarchar(50));

INSERT INTO dbo.FruitGrowsOn ( FruitGrowsOnName) 
OUTPUT inserted.FruitGrowsOnID, inserted.FruitGrowsOnName INTO @growsonids
VALUES ('Vine'),('Bush'), ('Tree'),('flower');

INSERT INTO dbo.Fruit (FruitName, FruitGrowsOnID)
	VALUES ('Banana', (SELECT [id] FROM @growsonids WHERE [name] = 'Tree'));
INSERT INTO dbo.Fruit (FruitName, FruitGrowsOnID)
	VALUES ('Grape', (SELECT [id] FROM @growsonids WHERE [name] = 'Vine'));
INSERT INTO dbo.Fruit (FruitName, FruitGrowsOnID)
	VALUES ('Blueberry', (SELECT [id] FROM @growsonids WHERE [name] = 'Bush'));	