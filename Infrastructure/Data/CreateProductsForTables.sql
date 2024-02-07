DECLARE @ResultMessage nvarchar(max)

EXEC AddProduct
	@Title = 'Jordan Retro 2',
	@Price = 2249,
	@Manufacturer = 'Jordan',
	@ImageURL = 'https://images.footlocker.com/is/image/FLEU/314103225604_04?wid=581&hei=581&fmt=png-alpha',
	@Result = @ResultMessage OUTPUT

SELECT @ResultMessage as Result




EXEC AddProduct
	@Title = 'Jordan 6 Rings',
	@Price = 2049,
	@Manufacturer = 'Jordan',
	@ImageURL = 'https://images.footlocker.com/is/image/FLEU/314102800704_02?wid=581&hei=581&fmt=png-alpha',
	@Result = @ResultMessage OUTPUT

SELECT @ResultMessage as Result





EXEC AddProduct
	@Title = 'Nike Air Max Penny',
	@Price = 2149,
	@Manufacturer = 'Nike',
	@ImageURL = 'https://images.footlocker.com/is/image/FLEU/314103482304_02?wid=581&hei=581&fmt=png-alpha',
	@Result = @ResultMessage OUTPUT

SELECT @ResultMessage as Result





EXEC AddProduct
	@Title = 'Nike Air More Uptempo 96',
	@Price = 2199,
	@Manufacturer = 'Nike',
	@ImageURL = 'https://images.footlocker.com/is/image/FLEU/314106168504_02?wid=581&hei=581&fmt=png-alpha',
	@Result = @ResultMessage OUTPUT

SELECT @ResultMessage as Result




EXEC AddProduct
	@Title = 'Reebok Shaq Victory',
	@Price = 2199,
	@Manufacturer = 'Reebok',
	@ImageURL = 'https://images.footlocker.com/is/image/FLEU/314104895504_02?wid=581&hei=581&fmt=png-alpha',
	@Result = @ResultMessage OUTPUT

SELECT @ResultMessage as Result
