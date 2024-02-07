CREATE PROCEDURE GetorCreatePriceId
	@Price money,
	@PriceId int OUTPUT

AS
BEGIN
	SELECT @PriceId = Id FROM Prices WHERE Price =@Price

	IF @PriceId IS NULL
	BEGIN
		INSERT INTO Prices VALUES(@Price)
		SELECT @PriceId= SCOPE_IDENTITY()

	END
END