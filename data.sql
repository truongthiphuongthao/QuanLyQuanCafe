CREATE DATABASE QuanLyQuanCafe

USE QuanLyQuanCafe

CREATE TABLE TableFood
(
  id INT IDENTITY PRIMARY KEY,
  name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
  status NVARCHAR(100) NOT NULL DEFAULT N'Trống' -- Trống || Có người
)


CREATE TABLE Account 
(
  UserName NVARCHAR(100) PRIMARY KEY,
  DisplayName NVARCHAR(100) NOT NULL DEFAULT N'User',
  Password NVARCHAR(100) NOT NULL DEFAULT 0,
  Type INT NOT NULL DEFAULT 0 ---1: admin - 0: staff
)

CREATE TABLE FoodCategory
(
 id INT IDENTITY PRIMARY KEY,
 name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên'
)


CREATE TABLE Food
(
 id INT IDENTITY PRIMARY KEY,
 name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
 idCategory INT NOT NULL,
 price FLOAT NOT NULL DEFAULT 0,
 FOREIGN KEY (idCategory) REFERENCES dbo.FoodCategory(id)
)


CREATE TABLE Bill 
(
 id INT IDENTITY PRIMARY KEY,
 DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
 DateCheckOut DATE,
 idTable INT NOT NULL,
 status INT NOT NULL DEFAULT 0 --1: đã thanh toán -- 0: chưa thanh toán
 FOREIGN KEY (idTable) REFERENCES dbo.TableFood(id)
)



CREATE TABLE BillInfo
(
 id INT IDENTITY PRIMARY KEY,
 idBill INT NOT NULL,
 idFood INT NOT NULL,
 quantity INT NOT NULL DEFAULT 0, 
 FOREIGN KEY (idBill) REFERENCES dbo.Bill(id),
 FOREIGN KEY (idFood) REFERENCES dbo.Food(id)
)

INSERT INTO dbo.Account(UserName, DisplayName, Password, Type) 
	   VALUES (N'admin', N'Admin', N'admin', 1)
INSERT INTO dbo.Account(UserName, DisplayName, Password, Type)
	   VALUES (N'nhanvien', N'Nhân viên', N'nhanvien', 0) 
	   
	   
-- SELECT * FROM dbo.Account

CREATE PROC USP_GetAccountByUserName
@userName nvarchar(100)
AS
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName
END

EXEC dbo.USP_GetAccountByUserName @userName = N'Admin'
	

CREATE PROC USP_Login
@userName nvarchar(100),
@passWord nvarchar(100)
AS
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName and Password = @passWord
END
----------- thêm bàn------
DECLARE @i INT = 0
WHILE @i <= 10
BEGIN 
	INSERT dbo.TableFood (name) VALUES (N'Bàn ' + CAST(@i AS nvarchar(100)))
	SET @i = @i + 1
END


CREATE PROC USP_GetTableList
AS 
SELECT * FROM dbo.TableFood

UPDATE dbo.TableFood SET STATUS = N'Có người' WHERE id = 9

EXEC dbo.USP_GetTableList

--- thêm category --------
INSERT dbo.FoodCategory (name) VALUES (N'Hải sản')
INSERT dbo.FoodCategory (name) VALUES (N'Nông sản')
INSERT dbo.FoodCategory (name) VALUES (N'Lâm sản')
INSERT dbo.FoodCategory (name) VALUES (N'Sản sản')
INSERT dbo.FoodCategory (name) VALUES (N'Nước')
----- thêm món ăn ----------
INSERT dbo.Food(name,idCategory, price) VALUES (N'Mực một nắng nướng sa tế',1, 120000)
INSERT dbo.Food(name,idCategory, price) VALUES (N'Nghêu hấp xả',1, 50000)
INSERT dbo.Food(name,idCategory, price) VALUES (N'Sò huyết rang me',1, 70000)
INSERT dbo.Food(name,idCategory, price) VALUES (N'Cơm rang',2, 40000)
INSERT dbo.Food(name,idCategory, price) VALUES (N'Thịt kho',3, 20000)
INSERT dbo.Food(name,idCategory, price) VALUES (N'Cơm chiên mushi',4, 50000)
INSERT dbo.Food(name,idCategory, price) VALUES (N'Đá me',5, 10000)
INSERT dbo.Food(name,idCategory, price) VALUES (N'Cafe',5, 14000)
----- thêm bill -------------
INSERT dbo.Bill(DateCheckIn, DateCheckOut, idTable, status) VALUES (GETDATE(), NULL, 1, 0)
INSERT dbo.Bill(DateCheckIn, DateCheckOut, idTable, status) VALUES (GETDATE(), NULL, 4, 0)
INSERT dbo.Bill(DateCheckIn, DateCheckOut, idTable, status) VALUES (GETDATE(), GETDATE(), 2, 1)

-------- thêm billinfo -------
INSERT dbo.BillInfo (idBill, idFood, quantity) VALUES (1, 1, 2)
INSERT dbo.BillInfo (idBill, idFood, quantity) VALUES (1, 3, 4)
INSERT dbo.BillInfo (idBill, idFood, quantity) VALUES (1, 5, 1)
INSERT dbo.BillInfo (idBill, idFood, quantity) VALUES (2, 1, 2)
INSERT dbo.BillInfo (idBill, idFood, quantity) VALUES (2, 6, 2)
INSERT dbo.BillInfo (idBill, idFood, quantity) VALUES (3, 5, 2)

SELECT * FROM dbo.Bill
SELECT * FROM dbo.BillInfo
SELECT * FROM dbo.Food
SELECT * FROM dbo.FoodCategory
select * from dbo.Bill where idTable = 1 and status = 1
select f.name, bi.quantity, f.price, f.price*bi.quantity as totalPrice from dbo.BillInfo as bi, dbo.Bill as b, dbo.Food as f 
where bi.idBill = b.id 
and bi.idFood = f.id
and b.status = 0
and b.idTable = 3