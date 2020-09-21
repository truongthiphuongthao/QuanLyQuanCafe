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

UPDATE dbo.TableFood SET STATUS = N'Trống' WHERE id = 9

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
INSERT dbo.BillInfo (idBill, idFood, quantity) VALUES (12, 1, 2)
INSERT dbo.BillInfo (idBill, idFood, quantity) VALUES (12, 3, 4)
INSERT dbo.BillInfo (idBill, idFood, quantity) VALUES (12, 5, 1)
INSERT dbo.BillInfo (idBill, idFood, quantity) VALUES (13, 1, 2)
INSERT dbo.BillInfo (idBill, idFood, quantity) VALUES (14,6, 2)
INSERT dbo.BillInfo (idBill, idFood, quantity) VALUES (15, 5, 2)

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

CREATE PROC USP_InsertBill
@idTable INT
AS
BEGIN
	INSERT dbo.Bill(DateCheckIn, DateCheckOut, idTable, status, discount) VALUES (GETDATE(), NULL, @idTable, 0, 0)
END

ALTER PROC USP_InsertBillInfo
@idBill INT, @idFood INT, @quantity INT
AS 
BEGIN
	DECLARE @isExistBillInfo INT
	DECLARE @foodCount INT = 1
	
	SELECT  @isExistBillInfo = id, @foodCount = b.quantity FROM dbo.BillInfo as b
	WHERE 
		idBill = @idBill
		AND idFood = @idFood
	
	IF(@isExistBillInfo > 0)
	BEGIN
		DECLARE @newCount INT = @foodCount + @quantity
		IF(@newCount > 0)
			UPDATE dbo.BillInfo SET quantity = @foodCount + @quantity WHERE idFood = @idFood
		ELSE
			DELETE dbo.BillInfo WHERE idBill = @idBill AND idFood = @idFood
	END
	ELSE
	BEGIN
		INSERT dbo.BillInfo (idBill, idFood, quantity) VALUES (@idBill, @idFood , @quantity )	
	END
END 
EXEC USP_InsertBillInfo @idBill =12, @idFood = 1, @quantity = 1
select * from dbo.BillInfo

select MAX(id) from dbo.Bill

delete from dbo.BillInfo
delete  from dbo.Bill


alter trigger UTG_UpdateBillInfo on dbo.BillInfo for insert, update
as 
begin
	declare @idBill int
	
	select @idBill = idBill from Inserted
	
	declare @idTable int
	
	select @idTable = idTable from dbo.Bill where id = @idBill and status = 0
	
	declare @count int 
	select @count = count(*) from dbo.BillInfo where idBill = @idBill
	if(@count > 0 )
	update dbo.TableFood set status = N'Có người' where id = @idTable
	else 
	update dbo.TableFood set status = N'Trống' where id = @idTable
end


create trigger UTG_UpdateBill on dbo.Bill for update
as
begin
	declare @idBill int
	select @idBill = id from Inserted
	declare @idTable int
	select @idTable = idTable from dbo.Bill where id = @idBill
	declare @count int = 0
	select @count = count(*) from dbo.Bill where idTable = @idTable and status = 0
	if(@count = 0)
		update dbo.TableFood set status = N'Trống' where id = @idTable		
end

-- them discount
alter table dbo.Bill
add discount int

update dbo.Bill set discount = 0

---- Chuyển bàn

alter proc USP_SwitchTable
@idTable1 int, @idTable2 int
as
begin
	declare @idFirstBill int
	declare @idSecondBill int
	
	declare @isFirstTableEmpty int = 1
	declare @isSecondTableEmpty int = 1
	
	
	select @idSecondBill = id from dbo.Bill where idTable = @idTable2 and status = 0
	select @idFirstBill  = id from dbo.Bill where idTable = @idTable1 and status = 0
	
	if(@idFirstBill is null)
		begin
			insert dbo.Bill(DateCheckIn, DateCheckOut, idTable, status) values (GETDATE(), NULL, @idTable1, 0)
			select @idFirstBill = max(id) from dbo.Bill where idTable = @idTable1 and status = 0
		end
		select @isFirstTableEmpty = count(*) from dbo.BillInfo where idBill = @idFirstBill
	if(@idSecondBill is null)
		begin
			insert dbo.Bill(DateCheckIn, DateCheckOut, idTable, status) values (GETDATE(), NULL, @idTable2, 0)
			select @idSecondBill = max(id) from dbo.Bill where idTable = @idTable2 and status = 0
			set @isSecondTableEmpty = 1
		end	
		select @isSecondTableEmpty = count(*) from dbo.BillInfo where idBill = @idSecondBill
	
	select id into IDBillInfoTable from dbo.BillInfo where idBill = @idSecondBill
	
	update dbo.BillInfo set idBill = @idSecondBill where idBill = @idFirstBill
	
	update dbo.BillInfo set idBill = @idFirstBill where id in (select * from IDBillInfoTable)
	
	drop table IDBillInfoTable
	
	if(@isFirstTableEmpty = 0)
		update dbo.TableFood set status = N'Trống' where id = @idTable2
	if(@isSecondTableEmpty = 0)
		update dbo.TableFood set status = N'Trống' where id = @idTable1
end

-- update dbo.TableFood set status = N'Trống'
select * from dbo.Bill
select * from dbo.BillInfo
select * from dbo.TableFood

alter table dbo.Bill add totalPrice float

delete from dbo.BillInfo
delete from dbo.Bill

alter proc USP_GetListBillByDate
@checkIn date, @checkOut date
as
begin
	select t.name as [Tên bàn], b.totalPrice as [Tổng tiền], DateCheckIn as [Ngày vào], DateCheckOut as [Ngày ra] , discount as [Giảm giá]
	from dbo.Bill as b, dbo.TableFood as t
	where DateCheckIn >= @checkIn 
	and DateCheckOut <=  @checkOut and b.status = 1
	and t.id = b.idTable
end



