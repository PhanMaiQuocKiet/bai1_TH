Create Database DbTh1
USE DbTh1;
--Create Table
create Table AdminUser
(
	UserName char(24) primary key,
	Passwords VARCHAR(64)
)

create Table Users
(
	UserName char(24) primary key,
	Passwords VARCHAR(64),
	FName nvarchar(35),
	LName nvarchar(15),
	DoB DATE,
	Phone char(10),
	Address nvarchar(100),
	email varchar(50),
	gender smallint

)
create Table HoaDon
(
	idlnvoice int IDENTITY(1,1) primary key,
	UserName char(24),
	Datelnvoice DATETIME ,
	FOREIGN key (UserName) references Users(UserName)
)
create Table LoaiSanPham
(
	idCate int IDENTITY(1,1) primary key,
	NameCate nvarchar(25)
)
create Table SanPham
(
	idPro int IDENTITY(1,1) primary key,
	idCate int,
	NamePro nvarchar(40),
	Qty int,
	Price decimal(10,8),
	Descriptions varchar(255),
	imagePath varchar(255),

	FOREIGN key(idCate) references LoaiSanPham(idCate)
)
CREATE TABLE ChiTietHoaDon
(
    idlnvoice INT,
    idPro INT,
    ItemQty INT,
    PRIMARY KEY (idlnvoice, idPro),
    FOREIGN KEY (idlnvoice) REFERENCES HoaDon(idlnvoice),
    FOREIGN KEY (idPro) REFERENCES SanPham(idPro)
);