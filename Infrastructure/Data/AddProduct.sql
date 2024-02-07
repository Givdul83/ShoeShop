ALTER PROCEDURE AddProduct
	@Title nvarchar(100),
	@Price money,
	@Manufacturer nvarchar(50),
	@ImageURL nvarchar(450),
	@Result nvarchar(max) OUTPUT

AS
BEGIN
	
	IF NOT EXISTS (SELECT 1 FROM Products WHERE Title = @Title )
	BEGIN

	BEGIN TRANSACTION

	BEGIN TRY
		DECLARE @ImageId int
		EXEC GetorCreateImageId @ImageURL, @ImageId OUTPUT

		DECLARE @ManufacturerId int
		EXEC GetorCreateManufacturerId @Manufacturer, @ManufacturerId OUTPUT

		DECLARE @PriceId int
		EXEC GetorCreatePriceId @Price , @PriceId OUTPUT

		INSERT INTO Products VALUES(@Title, @ManufacturerId, @PriceId)

		DECLARE @ProductId int
		SET @ProductId = SCOPE_IDENTITY()

		INSERT INTO ProductImages(ProductId, ImageId) VALUES(@ProductId,@ImageId)

		SET @Result= 'Produkt Skapades'
	COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SET @Result = 'Misslyckades'

	END CATCH
	END

	ELSE 
	SET @Result ='Misslyckades , Finns redan en produkt med denna titel'
	

END

