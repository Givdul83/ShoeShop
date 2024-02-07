CREATE PROCEDURE GetorCreateManufacturerId
	@Manufacturer nvarchar(50),
	@ManufacturerId int OUTPUT

AS
BEGIN
	SELECT @ManufacturerId = Id FROM Manufacturers WHERE Manufacturer =@Manufacturer

	IF @ManufacturerId IS NULL
	BEGIN
		INSERT INTO Manufacturer VALUES(@Manufacturer)
		SELECT @ManufacturerId= SCOPE_IDENTITY()

	END
END