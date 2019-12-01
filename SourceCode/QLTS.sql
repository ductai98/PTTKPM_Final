use master
IF EXISTS(select * from sys.databases where name='QuanLyQuanTraSua')
DROP DATABASE QuanLyQuanTraSua
go

create database QuanLyQuanTraSua
go

use QuanLyQuanTraSua
go

create table danhmuc(
	iddanhmuc INT IDENTITY(1,1) PRIMARY KEY,
	tendanhmuc nvarchar(100)
)

create table khohang(
	idkhohang int IDENTITY(1,1) PRIMARY KEY,
	tenkhoahang nvarchar(100),
	diachikho nvarchar(100)
)

create table khuyenmai(
	idkhuyenmai int IDENTITY(1,1) PRIMARY KEY,
	tenkhuyenmai nvarchar(100),
	ngaybatdau date,
	ngayketthuc date,
	mucgiamgia int
)

create table nhacungcap(
	idnhacungcap int IDENTITY(1,1) PRIMARY KEY,
	tennhacungcap nvarchar(100),
	diachinhacc nvarchar(100),
	sodienthoai varchar(20),
)

create table banan(
	idbanan int	IDENTITY(1,1) PRIMARY KEY,
	tenbanan nvarchar(100),
	trangthaibanan nvarchar(100) --có người hay chưa
)

create table taikhoan(
	idtaikhoan int IDENTITY(1,1) PRIMARY KEY,
	tendangnhap varchar(100),
	matkhau varchar(100),
	email varchar(100),
	hoten nvarchar(100),
	ngaysinh date,
	gioitinh nvarchar(50),
	chucvu nvarchar(50),
	sodienthoai varchar(20),
)

create table sanpham(
	idsanpham int IDENTITY(1,1) PRIMARY KEY,
	iddanhmuc int,
	idkhuyenmai int,
	idkhohang int,
	tensanpham nvarchar(100),
	giatien int,
	mota nvarchar(100),
	hinhanh varchar(100),
	banchay bit,
	conhang bit,

	foreign key (iddanhmuc) references danhmuc(iddanhmuc),
	foreign key (idkhuyenmai) references khuyenmai(idkhuyenmai),
	foreign key (idkhohang) references khohang(idkhohang)
)

create table hoadon(
	idhoadon int IDENTITY(1,1) PRIMARY KEY,
	idtaikhoan int,
	idbanan int,
	loaidonhang nvarchar(100), -- dùng tại cửa hàng/mang về
	ghichu nvarchar(100),
	ngaylaphoadon datetime,
	ngaythanhtoan datetime,
	trangthaithanhtoan int,
	giatien int,

	foreign key (idtaikhoan) references taikhoan(idtaikhoan),
	foreign key (idbanan) references banan(idbanan)
)

create table chitiethoadon(
	idhoadon int,
	idsanpham int,
	soluong int,
	dongia int,
	tongtien int,

	foreign key (idhoadon) references hoadon(idhoadon),
	foreign key (idsanpham) references sanpham(idsanpham),
	primary key (idhoadon, idsanpham)
)

create table thanhtoan(
	idthanhtoan int IDENTITY(1,1) PRIMARY KEY,
	idtaikhoan int,
	idhoadon int,
	ngaythanhtoan datetime,
	trangthaithanhtoan nvarchar(100),
	sotienthanhtoan int,

	foreign key (idtaikhoan) references taikhoan(idtaikhoan),
	foreign key (idhoadon) references hoadon(idhoadon)
)

create table phieunhap(
	idphieunhap int IDENTITY(1,1) PRIMARY KEY,
	idtaikhoan int,
	idnhacungcap int,
	ngaynhap datetime,

	foreign key (idtaikhoan) references taikhoan(idtaikhoan),
	foreign key (idnhacungcap) references nhacungcap(idnhacungcap)
)

create table chitietphieunhap(
	idphieunhap int,
	idsanpham int,
	soluong int,
	tongtien int,

	foreign key (idphieunhap) references phieunhap(idphieunhap),
	foreign key (idsanpham) references sanpham(idsanpham),
	primary key(idphieunhap, idsanpham)
)

insert into taikhoan(tendangnhap, matkhau, chucvu) values(N'tai', N'123', N'Admin')

insert into banan values(N'Bàn 1', N'Trống')
insert into banan values(N'Bàn 2', N'Trống')
insert into banan values(N'Bàn 3', N'Trống')
insert into banan values(N'Bàn 4', N'Trống')
insert into banan values(N'Bàn 5', N'Trống')

insert into danhmuc values(N'Trà sữa')
insert into danhmuc values(N'Trà đào')
insert into danhmuc values(N'Trà chanh')

insert into sanpham(iddanhmuc, tensanpham, giatien) values(1, N'Trà sữa trân châu', 10000)
insert into sanpham(iddanhmuc, tensanpham, giatien) values(1, N'Trà sữa đen', 15000)
insert into sanpham(iddanhmuc, tensanpham, giatien) values(1, N'Trà sữa trắng', 15000)
insert into sanpham(iddanhmuc, tensanpham, giatien) values(1, N'Trà sữa không sữa', 20000)
insert into sanpham(iddanhmuc, tensanpham, giatien) values(1, N'Trà sữa không trà', 10000)
insert into sanpham(iddanhmuc, tensanpham, giatien) values(2, N'Trà đào thường', 10000)
insert into sanpham(iddanhmuc, tensanpham, giatien) values(2, N'Trà đào chanh', 15000)
insert into sanpham(iddanhmuc, tensanpham, giatien) values(2, N'Trà đào muối', 22000)
insert into sanpham(iddanhmuc, tensanpham, giatien) values(3, N'Trà chanh thường', 12000)
insert into sanpham(iddanhmuc, tensanpham, giatien) values(3, N'Trà chanh muối', 13000)
insert into sanpham(iddanhmuc, tensanpham, giatien) values(3, N'Trà chanh đường', 14000)
go

create procedure sp_themhoadonchobanan @idbanan int
as
begin
	insert into hoadon(idbanan, ngaylaphoadon, ngaythanhtoan, trangthaithanhtoan, loaidonhang) values(@idbanan, GETDATE(), null, 0, N'Ăn tại bàn')
	update banan set trangthaibanan = N'Có người' where idbanan = @idbanan --Set lại trạng thái bàn ăn khi đã thêm hóa đơn
end
go

create procedure sp_themcthd @idhoadon int, @idsanpham int, @soluong int
as
begin
	declare @soLuongSanPham int
	select @soLuongSanPham = soluong from chitiethoadon where idhoadon = @idhoadon and idsanpham = @idsanpham
	if (@soLuongSanPham is not null) --Đã tồn tại món ăn trong hóa đơn
	begin
		declare @soluongtmp int = @soLuongSanPham + @soLuong
		if(@soluongtmp > 0) --Đảm bảo rằng số lượng sản phẩm luôn lớn hơn 0 sau khi update
		begin
			update chitiethoadon set soluong = @soLuongSanPham + @soluong where idsanpham = @idsanpham
		end
		else --Nếu số lượng nhỏ hơn  0 thì xóa luôn chitiethoadon
			delete chitiethoadon where idhoadon = @idhoadon and idsanpham = @idsanpham
	end
	else --Nếu chưa tồn tại thì thêm mới chi tiết hóa đơn
	begin
		insert into chitiethoadon(idhoadon, idsanpham, soluong) values(@idhoadon, @idsanpham, @soluong)
	end
	
end
go

create procedure sp_thanhtoanhoadon @idhoadon int, @tongTien int, @idbanan int = null
as
begin
	update hoadon set trangthaithanhtoan = 1, ngaythanhtoan = GETDATE(), giatien = @tongTien where idhoadon = @idhoadon
	if (@idbanan is not null)
	begin
		update banan set trangthaibanan = N'Trống' where idbanan = @idbanan
	end
end
go


create procedure sp_layhoadoncuthe @date date
as
begin
	declare @ngayketthuc date = DATEADD(day, 1, @date);
	select hd.ngaylaphoadon, hd.ngaythanhtoan, SUM(hd.giatien) as thanhtien from hoadon hd, banan ba 
			where (hd.idbanan = ba.idbanan) and 
				(ngaythanhtoan between @date and @ngayketthuc) and (trangthaithanhtoan = 1)
				group by hd.ngaylaphoadon, hd.ngaythanhtoan
end
go

create procedure sp_layhoadontheothang @date date
as
begin
	declare @ngayketthuc date = DATEADD(day, 30, @date);
	select hd.ngaylaphoadon, hd.ngaythanhtoan, SUM(hd.giatien) as thanhtien from hoadon hd, banan ba 
			where (ngaythanhtoan between @date and @ngayketthuc) and (trangthaithanhtoan = 1) 
			group by hd.ngaylaphoadon, hd.ngaythanhtoan
end
go

create procedure sp_layhoadontheongay @date date
as
begin
declare @ngayketthuc date = DATEADD(day, 1, @date);
	select hd.ngaylaphoadon, hd.ngaythanhtoan, SUM(hd.giatien) as thanhtien from hoadon hd, banan ba 
			where (ngaythanhtoan between @date and @ngayketthuc) and (trangthaithanhtoan = 1)
			group by hd.ngaylaphoadon, hd.ngaythanhtoan
end
go

create procedure sp_thongkesanphamtheongay @date date
as
begin
	declare @ngayketthuc date = DATEADD(day, 1, @date);
	select sp.tensanpham, sp.giatien, SUM(ct.soluong) as soluong from hoadon hd, sanpham sp, chitiethoadon ct
			where (ngaythanhtoan between @date and @ngayketthuc) and (trangthaithanhtoan = 1) and (sp.idsanpham = ct.idsanpham)
			and (ct.idhoadon = hd.idhoadon)
			group by sp.tensanpham, sp.giatien
end
go 

create procedure sp_thongkesanphamtheothang @date date
as
begin
	declare @ngayketthuc date = DATEADD(day, 30, @date);
	select sp.tensanpham, sp.giatien, SUM(ct.soluong) as soluong from hoadon hd, sanpham sp, chitiethoadon ct
			where (ngaythanhtoan between @date and @ngayketthuc) and (trangthaithanhtoan = 1) and (sp.idsanpham = ct.idsanpham)
			and (ct.idhoadon = hd.idhoadon)
			group by sp.tensanpham, sp.giatien
end
go

create procedure sp_thongkesanphamtuchon @date date
as
begin
	declare @ngayketthuc date = DATEADD(day, 1, @date);
	select sp.tensanpham, sp.giatien, SUM(ct.soluong) as soluong from hoadon hd, sanpham sp, chitiethoadon ct
			where (ngaythanhtoan between @date and @ngayketthuc) and (trangthaithanhtoan = 1) and (sp.idsanpham = ct.idsanpham)
			and (ct.idhoadon = hd.idhoadon)
			group by sp.tensanpham, sp.giatien
end
go 
