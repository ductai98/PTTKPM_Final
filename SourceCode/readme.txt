Mở file .sln bằng visual studio
Mở file QLTS.sql bằng MS SQL Server Management Studio, chạy dòng 
	use master
	IF EXISTS(select * from sys.databases where name='QuanLyQuanTraSua')
	DROP DATABASE QuanLyQuanTraSua
	go

sau đó chạy các dòng còn lại