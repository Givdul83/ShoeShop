CREATE PROCEDURE GetorCreateImageId
	@ImageURL nvarchar(450),
	@ImageId int OUTPUT

AS
BEGIN
	SELECT @ImageId = Id FROM Images WHERE ImageURL = @ImageURL

	IF @ImageId IS NULL
	BEGIN
		INSERT INTO Images VALUES(@ImageURL)
		SELECT @ImageId= SCOPE_IDENTITY()

	END
END