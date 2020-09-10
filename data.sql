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
 idCategory INT NOT NULL 
 
 FOREIGN KEY (idCategory) REFERENCES dbo.FoodCategory(id)
)


CREATE TABLE Bill 
(
 id INT IDENTITY PRIMARY KEY,
 DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
 DateCheckOut DATE NOT NULL,
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
	   VALUES (N'Admin', N'Admin', N'1', 1)
INSERT INTO dbo.Account(UserName, DisplayName, Password, Type)
	   VALUES (N'Nhân viên', N'Nhân viên', N'123', 0) 
	   
	   
-- SELECT * FROM dbo.Account

CREATE PROC USP_GetAccountByUserName
@userName nvarchar(100)
AS
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName
END


EXEC dbo.USP_GetAccountByUserName @userName = N'Admin'
	
drop proc USP_GetAccountByUserName

SELECT * FROM dbo.Account WHERE UserName = N'Admin' AND Password = N'1'