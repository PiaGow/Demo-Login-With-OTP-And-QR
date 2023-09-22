create database btnhomotp
go

create table DataAccount
(
	UID nchar(10) PRIMARY KEY,
	Email nvarchar(50) not null,
	MatKhau nchar(20) not null,
	TenNguoiDung nvarchar(50) not null
)

INSERT INTO DataAccount (UID, Email, MatKhau,TenNguoiDung) VALUES ('1', N'phat14072009@gmail.com','123', N'My Account');
INSERT INTO DataAccount (UID, Email, MatKhau,TenNguoiDung) VALUES ('2', N'2worldteamsayshi@gmail.com','111', N'My team');
INSERT INTO DataAccount (UID, Email, MatKhau,TenNguoiDung) VALUES ('3', N'phanngocduc818@gmail.com','12345', N'Your Account');

