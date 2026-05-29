Use master
go
IF(EXISTS(SELECT * FROM SYSDATABASES WHERE NAME='IMua'))
	DROP DATABASE [IMua]
go
Create database [IMua]
go
use [IMua]
go
--------------------------------------------------------------------------------------------------------------------------------------
Create table [NguoiDung]
(
	[MaND] Integer Identity NOT NULL,
	[TenDangNhap] Nvarchar(50) NOT NULL,
	[MatKhau] Nvarchar(50) NOT NULL,
	[HoTen] Nvarchar(100) NOT NULL,
	[AnhDaiDien] Text NULL,
	[SoDT] Varchar(50) NOT NULL,
	[DiaChi] Ntext NOT NULL,
	[Email] Varchar(50) NOT NULL,
	[TrangThai] Bit NOT NULL, --- 0: Disable / 1: Active
	[GroupID] Nvarchar(50) NOT NULL,
Primary Key ([MaND])
) 
go

Create table [DanhMuc]
(
	[MaDM] Integer Identity NOT NULL,
	[TenDM] Nvarchar(100) NOT NULL,
	[AnhDM] Text NOT NULL,
	[BieuTuong] Text NOT NULL,
Primary Key ([MaDM])
) 
go

Create table [LoaiSP]
(
	[MaLoai] Integer Identity NOT NULL,
	[TenLoai] Nvarchar(100) NOT NULL,
	[MaDM] Integer NOT NULL,
Primary Key ([MaLoai])
) 
go

Create table [SanPham]
(
	[MaSP] Integer Identity NOT NULL,
	[TenSP] Nvarchar(100) NOT NULL,
	[AnhDaiDien] Text NOT NULL,
	[Gia] Money NOT NULL,
	[KhuyenMai] Integer NOT NULL,
	[MoTa] Nvarchar(4000) NOT NULL,
	[XuatXu] Nvarchar(100) NOT NULL,
	[TrongLuong] Text NOT NULL,
	[MaLoai] Integer NOT NULL,
Primary Key ([MaSP])
) 
go

Create table [HoaDon]
(
	[MaHD] Integer Identity NOT NULL,
	[PhiVanChuyen] Money NOT NULL,
	[ThanhTien] Money NOT NULL,
	[NgayMua] Datetime NOT NULL,
	[MaND] Integer NULL,
Primary Key ([MaHD])
) 
go

Create table [TinTuc]
(
	[MaTinTuc] Integer Identity NOT NULL,
	[TieuDe] Nvarchar(100) NOT NULL,
	[TomTat] Nvarchar(1000) NOT NULL,
	[NoiDung] Nvarchar(4000) NOT NULL,
	[AnhTinTuc] Text NOT NULL,
Primary Key ([MaTinTuc])
) 
go

Create table [ChiTietHoaDon]
(
	[MaHD] Integer NOT NULL,
	[MaSP] Integer NOT NULL,
	[SoLuong] Integer NOT NULL,
Primary Key ([MaHD],[MaSP])
) 
go

Create table [UserGroup]
(
	[GroupID] Nvarchar(50) NOT NULL,
	[Name] Nvarchar(200) NOT NULL,
Primary Key ([GroupID])
) 
go

Create table [Role]
(
	[RoleID] Nvarchar(50) NOT NULL,
	[Name] Nvarchar(200) NOT NULL,
Primary Key ([RoleID])
) 
go

Create table [Credential]
(
	[CredentialID] Bigint Identity(1,1) NOT NULL,
	[GroupID] Nvarchar(50) NOT NULL,
	[RoleID] Nvarchar(50) NOT NULL,
Primary Key ([CredentialID])
) 
go

Alter table [HoaDon] add  foreign key([MaND]) references [NguoiDung] ([MaND])  on update cascade on delete cascade 
go
Alter table [LoaiSP] add  foreign key([MaDM]) references [DanhMuc] ([MaDM])  on update cascade on delete cascade 
go
Alter table [SanPham] add  foreign key([MaLoai]) references [LoaiSP] ([MaLoai])  on update cascade on delete cascade 
go
Alter table [ChiTietHoaDon] add  foreign key([MaSP]) references [SanPham] ([MaSP])  on update cascade on delete cascade 
go
Alter table [ChiTietHoaDon] add  foreign key([MaHD]) references [HoaDon] ([MaHD])  on update cascade on delete cascade 
go
Alter table [NguoiDung] add  foreign key([GroupID]) references [UserGroup] ([GroupID])  on update cascade on delete cascade 
go
Alter table [Credential] add  foreign key([GroupID]) references [UserGroup] ([GroupID])  on update cascade on delete cascade 
go
Alter table [Credential] add  foreign key([RoleID]) references [Role] ([RoleID])  on update cascade on delete cascade 
go
--------------------------------------------------------------------------------------------------------------------------------------
Insert into [UserGroup] values ('ADMIN',N'Quản trị viên')
Insert into [UserGroup] values ('MEMBER',N'Người dùng')
go

--Role: NguoiDung
Insert into [Role] values('VIEW_USER', N'Xem danh sách người dùng')
Insert into [Role] values('ADD_USER', N'Thêm người dùng')
Insert into [Role] values('EDIT_USER', N'Sửa người dùng')
--Role: DanhMuc
Insert into [Role] values('VIEW_DANHMUC', N'Xem danh sách danh mục')
Insert into [Role] values('ADD_DANHMUC', N'Thêm danh mục')
Insert into [Role] values('EDIT_DANHMUC', N'Sửa danh mục')
Insert into [Role] values('DELETE_DANHMUC', N'Xoá danh mục')
--Role: LoaiSP
Insert into [Role] values('VIEW_LOAISP', N'Xem danh sách loại sản phẩm')
Insert into [Role] values('ADD_LOAISP', N'Thêm loại sản phẩm')
Insert into [Role] values('EDIT_LOAISP', N'Sửa loại sản phẩm')
Insert into [Role] values('DELETE_LOAISP', N'Xoá loại sản phẩm')
--Role: SanPham
Insert into [Role] values('VIEW_SANPHAM', N'Xem danh sách sản phẩm')
Insert into [Role] values('ADD_SANPHAM', N'Thêm sản phẩm')
Insert into [Role] values('EDIT_SANPHAM', N'Sửa sản phẩm')
Insert into [Role] values('DELETE_SANPHAM', N'Xoá sản phẩm')
--Role: HoaDon
Insert into [Role] values('VIEW_HOADON', N'Xem danh sách hoá đơn')
Insert into [Role] values('ADD_HOADON', N'Thêm hoá đơn')
Insert into [Role] values('EDIT_HOADON', N'Sửa hoá đơn')
Insert into [Role] values('DELETE_HOADON', N'Xoá hoá đơn')
--Role: TinTuc
Insert into [Role] values('VIEW_TINTUC', N'Xem danh sách tin tức')
Insert into [Role] values('ADD_TINTUC', N'Thêm tin tức')
Insert into [Role] values('EDIT_TINTUC', N'Sửa tin tức')
Insert into [Role] values('DELETE_TINTUC', N'Xoá tin tức')
--Role: UserGroup
Insert into [Role] values('VIEW_USERGROUP', N'Xem danh sách nhóm người dùng')
Insert into [Role] values('ADD_USERGROUP', N'Thêm nhóm người dùng')
Insert into [Role] values('EDIT_USERGROUP', N'Sửa nhóm người dùng')
Insert into [Role] values('DELETE_USERGROUP', N'Xoá nhóm người dùng')
--Role: Role
Insert into [Role] values('VIEW_ROLE', N'Xem danh sách vai trò')
Insert into [Role] values('ADD_ROLE', N'Thêm vai trò')
Insert into [Role] values('EDIT_ROLE', N'Sửa vai trò')
Insert into [Role] values('DELETE_ROLE', N'Xoá vai trò')
--Role: Credential
Insert into [Role] values('VIEW_CREDENTIAL', N'Xem danh sách uỷ quyền')
Insert into [Role] values('ADD_CREDENTIAL', N'Thêm nhóm uỷ quyền')
Insert into [Role] values('EDIT_CREDENTIAL', N'Sửa nhóm uỷ quyền')
Insert into [Role] values('DELETE_CREDENTIAL', N'Xoá nhóm uỷ quyền')
go

-- Mat khau mac dinh: 123456 (MD5)
Insert into [NguoiDung] values ('admin', 'e10adc3949ba59abbe56e057f20f883e', N'Quản trị viên', 'avatar1.png', '0123456789', N'Việt Nam', 'admin@gmail.com', 1, 'ADMIN')
Insert into [NguoiDung] values ('user', 'e10adc3949ba59abbe56e057f20f883e', N'Người dùng', 'avatar2.png', '0987654321', N'Việt Nam', 'user@gmail.com', 1, 'MEMBER')
go

Insert into [DanhMuc] values (N'Mỹ Phẩm Trang Điểm', 'dm1.png', 'icon-dm1.png')
Insert into [DanhMuc] values (N'Hỗ Trợ Điều Trị', 'dm2.png', 'icon-dm2.png')
Insert into [DanhMuc] values (N'Chăm Sóc Da Mặt', 'dm3.png', 'icon-dm3.png')
Insert into [DanhMuc] values (N'Chăm Sóc Toàn Thân', 'dm4.png', 'icon-dm4.png')
Insert into [DanhMuc] values (N'Chăm Sóc Tóc', 'dm5.png', 'icon-dm5.png')
Insert into [DanhMuc] values (N'Thương Hiệu Ưa Chuộng', 'dm6.png', 'icon-dm6.png')
Insert into [DanhMuc] values (N'Sức Khỏe Dinh Dưỡng', 'dm7.png', 'icon-dm7.png')
Insert into [DanhMuc] values (N'Các Sản Phẩm Khác', 'dm8.png', 'icon-dm8.png')
go

Insert into [LoaiSP] values (N'Son môi', 1)
Insert into [LoaiSP] values (N'Kem nền', 1)
Insert into [LoaiSP] values (N'Phấn nước', 1)
Insert into [LoaiSP] values (N'Che khuyết điểm', 1)
Insert into [LoaiSP] values (N'Điều trị mụn', 2)
Insert into [LoaiSP] values (N'Điều trị rụng tóc', 2)
Insert into [LoaiSP] values (N'Giảm cân tan mỡ bụng', 2)
Insert into [LoaiSP] values (N'Sữa rửa mặt', 3)
Insert into [LoaiSP] values (N'Nước tẩy trang', 3)
Insert into [LoaiSP] values (N'Kem tẩy lông', 4)
Insert into [LoaiSP] values (N'Kem chống nắng', 4)
Insert into [LoaiSP] values (N'Sữa tắm trắng da', 4)
Insert into [LoaiSP] values (N'Xịt dưỡng tóc', 5)
Insert into [LoaiSP] values (N'Kem ủ tóc', 5)
Insert into [LoaiSP] values (N'Thương hiệu Pháp', 6)
Insert into [LoaiSP] values (N'Thương hiệu Hàn Quốc', 6)
Insert into [LoaiSP] values (N'Dầu dừa nguyên chất', 7)
Insert into [LoaiSP] values (N'Mật ong thiên nhiên', 7)
Insert into [LoaiSP] values (N'Phụ kiện làm đẹp', 8)
Insert into [LoaiSP] values (N'Nước hoa chính hãng', 8)
go

--Loại sản phẩm: Son môi
Insert into [SanPham] values (N'Son Dior 999', 'sp-sonmoi1.png', '715000', '16', N'Son Dior 999 matte là một trong những màu son đỏ bán chạy nhất của thương hiệu Dior trên khắp thị trường thế giới. Đây là sự kết hợp hoàn hảo của dòng son lì và son dưỡng không chỉ giúp bạn sở hữu màu son cực quyến rũ, cuốn hút mà còn dưỡng môi mềm mịn, căng bóng hiệu quả.', N'Pháp', '3.5gr', 1)
Insert into [SanPham] values (N'Son Dưỡng Môi The Collagen', 'sp-sonmoi2.png', '270000', '26', N'Son Dưỡng Môi The Collagen được nhập khẩu chính hãng trực tiếp từ Hàn Quốc. Thuộc dòng son dưỡng cao cấp, Ecosy đem lại sự mềm mại, căng mọng và giúp cho bạn có một đôi môi ửng hồng tự nhiên. Được sự yêu thích và tin dùng của chị em phụ nữ khắp Châu Á.', N'Hàn Quốc', '3gr', 1)
Insert into [SanPham] values (N'Son Dưỡng Dior Addict', 'sp-sonmoi3.png', '920000', '20', N'Son dưỡng Dior Lip Glow được nhập khẩu chính hãng Pháp. Thỏi son với bảng thành phần nhiều dưỡng chất nhanh chóng giúp cấp nước trị nứt môi, dưỡng ẩm môi, trị thâm môi, tái tạo tế bào mới giữ cho môi luôn tươi trẻ, hồng hào tự nhiên chỉ sau 1-2 tuần sử dụng.', N'Pháp', '20gr', 1)
Insert into [SanPham] values (N'Son Lì 3CE Stylenanda', 'sp-sonmoi4.png', '520000', '14', N'Son lì 3CE – dòng son cực hot và được lựa chọn nhiều trên thị trường. Sở hữu bảng màu thời thượng, cực hot và dễ phối. Chất son mềm mịn, lì và khó phai, giúp bạn trở nên quyến rũ, tự tin hơn. Son lì 3CE Stylenanda chính là bí quyết giúp bạn trở nên sang trọng, quyến rũ.', N'Hàn Quốc', '3.5gr', 1)
Insert into [SanPham] values (N'Son Dưỡng Môi Bioderma', 'sp-sonmoi5.png', '350000', '15', N'Son dưỡng môi Bioderma là dòng sản phẩm chăm sóc môi tốt nhất đến từ Pháp. Son dưỡng Bioderma với thành phần chứa nhiều dưỡng chất giúp dưỡng môi mềm mịn, loại bỏ tế bào chết cho môi căng bóng, tươi trẻ và hồng hào. Bioderma chính là bí quyết dưỡng môi hoàn hảo cho bạn.', N'Pháp', '15ml', 1)
Insert into [SanPham] values (N'Son Dưỡng Môi DHC Nhật Bản', 'sp-sonmoi6.png', '250000', '22', N'Son dưỡng môi DHC được nhập khẩu chính hãng Nhật Bản. Son dưỡng DHC với thành phần chứa nhiều dưỡng chất giúp mềm môi, trị thâm môi, loại bỏ tế bào chết giúp môi căng mọng, hồng hào. Son môi DHC Lip cream có thành phần được chiết xuất 100% từ thiên nhiên nên an toàn khi sử dụng.', N'Nhật Bản', '10gr', 1)
Insert into [SanPham] values (N'Son Dưỡng Môi Vaseline', 'sp-sonmoi7.png', '85000', '24', N'Son dưỡng môi Vaseline Lip Therapy được sản xuất bởi mỹ phẩm Vaseline. Son dưỡng Vaseline với thành phần tự nhiên ngăn tình trạng nứt nẻ môi, nuôi dưỡng môi căng mọng, mềm mại và quyến rũ. Mua ngay son dưỡng Vaseline Lip Therapy hôm nay để nhận nhiều ưu đãi.', N'Trung Quốc', '21gr', 1)
Insert into [SanPham] values (N'Son Collagen The Face Shop', 'sp-sonmoi8.png', '320000', '16', N'Son Hàn Quốc Collagen nhập khẩu chính hãng từ Hàn Quốc là dòng son đang cực HOT trong năm nay, với chất son mềm mịn, mướt như nhung, màu sắc lên chuẩn, bám môi tốt cùng hương thơm nhẹ nhàng quyến rũ, vỏ son ánh kim đầy sang trọng, bạn sẽ bị cuốn hút ngay từ cái nhìn đầu tiên.', N'Hàn Quốc', '5gr', 1)
Insert into [SanPham] values (N'Son Lì Dạng Thỏi Pencil & Smudger', 'sp-sonmoi9.png', '280000', '14', N'Son thỏi lì dạng thỏi kèm đầu tán son Color Blur By Lipstudio Cream Matte Pencil Smudger được sản xuất bởi mỹ phẩm Maybelline. Son thỏi lì Maybelline với chất son mềm mịn, tạo màu cực chuẩn cho đôi môi thêm quyến rũ. Mua ngay để nhận nhiều ưu đãi.', N'Trung Quốc', '10gr', 1)
Insert into [SanPham] values (N'Son Môi AmOk Lovefit Mint Best Color', 'sp-sonmoi10.png', '180000', '12', N'Son Môi AmOk Lovefit Mint Best Color được nhập khẩu chính hãng từ AmOk hàn Quốc. Son Môi AmOk Lovefit Mint Best Color với chất son siêu mềm mịn, lên màu chuẩn, bảng màu phong phú, vỏ ngoài xin xắn, cầm rất chắc tay. Mua ngay để nhận nhiều ưu đãi.', N'Hàn Quốc', '20gr', 1)
--Loại sản phẩm: Kem nền
Insert into [SanPham] values (N'Kem Che Khuyết Điểm Dermacol', 'sp-kemnen1.png', '310000', '20', N'Kem che khuyết điểm Dermacol dòng sản phẩm trang điểm được tin dùng hàng đầu hiện nay. Che khuyết điểm Dermacol nhờ lớp kem mịn, siêu nhỏ giúp che phủ mọi khuyết điểm như sẹo, thâm, mụn, vết xăm,..cho da đều màu, dưỡng da mềm mịn.', N'Cộng Hòa Séc', '30gr', 2)
Insert into [SanPham] values (N'Kem Nền NEUTROGENA Skin Clearing Oil', 'sp-kemnen2.png', '250000', '20', N'Kem nền Neutrogena Skin Clearing Oil được nhập khẩu chính hãng tại Mỹ. Kem nền ngăn ngừa mụn Neutrogena, kiểm soát chất nhờn, tạo lớp nền tự nhiên cho da mềm mịn. Mua hàng ngay để nhận nhiều ưu đãi.', N'Mỹ', '35gr', 2)
Insert into [SanPham] values (N'Kem Nền Maybelline Rewind', 'sp-kemnen3.png', '210000', '20', N'Kem Nền Maybelline Instant Age Rewind được nhập khẩu chính hãng từ Mỹ. Kem che khuyết điểm Maybelline chống lão hóa, làm sáng các vùng da tối màu, sáng gương mặt, tạo nên vẻ tươi trẻ, mạnh khỏe và đầy sức sống.', N'Mỹ', '30gr', 2)
Insert into [SanPham] values (N'Kem Che Khuyết Điểm Instant Age', 'sp-kemnen4.png', '220000', '18', N'Kem nền Maybelline Instant Age Rewind được nhập khẩu chính hãng từ Mỹ. Che khuyết điểm Maybelline Instant Age Rewind giúp che phủ hoàn toàn các vết nám, mụn, sẹo, vết chân chim trên gương mặt, đồng thời làm sáng và chống lão hóa da.', N'Mỹ', '20gr', 2)
Insert into [SanPham] values (N'Kem Nền Revlon Colorstay 24h', 'sp-kemnen5.png', '245000', '12', N'Kem nền Revlon Colorstay được nhập khẩu chính hãng từ Mỹ. Kem nền Revlon Colostay che khuyết điểm hoàn hảo, được giới makeup đánh giá rất cao, chỉ vài lần dặm nhẹ là các vết mụn, thâm, nếp nhăn, chân chim sẽ hoàn toàn biến mất, khả năng dưỡng ẩm, chống nắng và bám màu siêu bền.', N'Mỹ', '35gr', 2)
Insert into [SanPham] values (N'Kem Nền The Face Shop Gold Collagen', 'sp-kemnen6.png', '380000', '8', N'Kem nền The Face Shop Gold Collagen được nhập khẩu chính hãng từ Hàn Quốc, chứa hàng triệu tinh thể vàng nguyên chất và Collagen, giúp bạn thúc đẩy tái tạo và tăng độ đàn hồi cho da, hạn chế hình thành nếp nhăn, vết chân chim, làm chậm quá trình lão hóa. Cho bạn làn da căng mịn và tươi trẻ.', N'Hàn Quốc', '40ml', 2)
Insert into [SanPham] values (N'Kem Nền The Face Shop Power Perfection', 'sp-kemnen7.png', '420000', '24', N'Kem nền The Face Shop Power Perfection được nhập khẩu chính hãng từ Hàn Quốc, sở hữu 3 công dụng trong 1 sản phẩm: Chống nắng với chỉ số chống nắng SPF 37+, làm trắng và che phủ khuyết điểm với chất Cream cực mịn, dưỡng da với chiết xuất Omega 3 và Vita skin.', N'Hàn Quốc', '40gr', 2)
--Loại sản phẩm: Phấn nước
Insert into [SanPham] values (N'Phấn Nước Sulwhasoo Perfecting', 'sp-phannuoc1.png', '950000', '15', N'Phấn nước Sulwhasoo Perfecting được nhập khẩu chính hãng từ Amore Pacific Hàn Quốc, là loại phấn nền cao cấp đa chức năng, vừa có khả năng chống nắng SPF 50, vừa che phủ khuyết điểm và cung cấp độ ẩm hoàn hảo, cho bạn làn da sáng, tươi tắn và mịn màng không tì vết.', N'Hàn Quốc', '40gr', 3)
Insert into [SanPham] values (N'Phấn Nước Laneige Cushion', 'sp-phannuoc2.png', '780000', '6', N'Phấn nước Laneige Cushion được nhập khẩu chính hãng từ Laneige Hàn Quốc, che khuyết điểm khá tốt, khả năng kiềm dầu lên đến 12 giờ, chống nước và chống nắng SPF 50+, tinh chất dưỡng da phân giải melanin giúp da trắng hồng hơn.', N'Hàn Quốc', '30gr', 3)
Insert into [SanPham] values (N'Phấn Tươi Ver 22 Bounce Up Pact SPF 50+/PA+++', 'sp-phannuoc3.png', '375000', '14', N'Phấn tươi Ver 22 Bounce Up Pact SPF 50+ PA+++ được nhập khẩu chính hãng từ Hàn Quốc. Phấn tươi trang điểm giúp kiềm dầu, dưỡng ẩm cao, tạo lớp nền trang điểm nhẹ nhàng, tự nhiên.', N'Hàn Quốc', '35gr', 3)
--Loại sản phẩm: Che khuyết điểm
Insert into [SanPham] values (N'Bảng Phấn Tạo Khối Color Effects', 'sp-chekhuyetdiem1.png', '210000', '20', N'Bảng Phấn Tạo Khối City Color Contour Effect được nhập khẩu chính hãng từ Hàn Quốc. Phấn tạo khối City Color 3 màu tạo cho khuôn mặt thanh thoát, hoàn hảo hơn khi makeup. Mua ngay Bảng Phấn Tạo Khối City Color Contour Effect hôm nay để được nhận nhiều ưu đãi.', N'Hàn Quốc', '50gr', 4)
Insert into [SanPham] values (N'Phấn Nén IOPE-Perfect Skin Twin Pact', 'sp-chekhuyetdiem2.png', '700000', '0', N'Phấn Nền Iope Perfect Skin Twin Pact được nhập khẩu chính hãng từ Hàn Quốc. Phấn Nền trang điểm Iope che phủ hoàn hảo, dưỡng ẩm cho da mịn màng, tươi tắn. Mua ngay Phấn Nền Iope Perfect Skin Twin Pact hôm nay để được nhận nhiều ưu đãi.', N'Hàn Quốc', '30gr', 4)
Insert into [SanPham] values (N'Kem Che Khuyết Điểm Và Tạo Khối NYX', 'sp-chekhuyetdiem3.png', '290000', '14', N'Kem che khuyết điểm và tạo khối NYX là sản phẩm nhập khẩu chính hãng từ Mỹ. Sản phẩm che khuyết điểm NYX 6 ô nhỏ gọn tiện dụng, chất kem cực mịn, màu sắc trung thực, Mỗi ô màu mang một công dụng khác nhau, che khuyết điểm, tạo khối, nhấn sáng...', N'Mỹ', '40gr', 4)
--Loại sản phẩm: Điều trị mụn
Insert into [SanPham] values (N'Serum Trị Mụn Peel Acnes Blanc', 'sp-dieutrimun1.png', '395000', '10', N'Serum trị mụn Peel Acnes BlanC điều trị tận gốc đến 90% các vấn đề về mụn nặng như: mụn bọc, mụn trứng cá, mụn sưng viêm,... Kháng viêm, điều trị dứt điểm và ngăn ngừa mụn quay trở lại. Kích thích sửa chữa, tăng cường tái sinh da. Xóa mờ vết thâm sẹo do mụn để lại.', N'Pháp', '40ml', 5)
Insert into [SanPham] values (N'Bộ Trị Mụn Yanhee', 'sp-dieutrimun2.png', '300000', '0', N'Bộ Trị Mụn Yanhee bao gồm: Xà phòng rửa mặt giúp da sạch sẽ, loại bỏ bụi bẩn và chất nhờn, làm mờ đốm đen, tàn nhang, kem dưỡng ban ngày với chỉ số chống nắng cao bảo vệ da khỏi tác hại của tia UV và kem dưỡng ban đêm chống lão hóa, làm trắng da.', N'Thái Lan', '35gr', 5)
Insert into [SanPham] values (N'Serum Trị Mụn The Ordinary Niacinamide', 'sp-dieutrimun3.png', '340000', '14', N'Serum Trị Mụn The Ordinary Niacinamide 10% + Zinc 1% giúp duy trì độ ẩm cho da, cải thiện hàng rào bảo vệ và ngăn ngừa tình trạng mất nước trên da. Cân bằng và kiểm soát hoạt động của tuyến bã nhờn, kiểm soát vùng dầu chữ T và thu nhỏ lỗ chân lông.', N'Việt Nam', '45ml', 5)
--Loại sản phẩm: Điều trị rụng tóc
Insert into [SanPham] values (N'Bộ Dầu Gội Weilaiya', 'sp-dieutrirungtoc1.png', '650000', '10', N'Bộ Dầu Gội Weilaiya mang đến giải pháp ngăn rụng và kích thích mọc tóc, chăm sóc cho mái tóc dài óng mượt của chị em phụ nữ. Với thành phần chiết xuất từ Hà Thủ Ô, Gừng, Lanvender cùng 8 loại thảo dược thuốc bắc quý hiếm, mang lại công thức hoàn hảo, giải quyết mọi vấn đề về tóc như tóc rụng, tóc hư tổn, chẻ ngọn, khô xơ,...', N'Hồng Kông', '650ml', 6)
Insert into [SanPham] values (N'Kẹo Gấu Mọc Tóc Hair Vitamins', 'sp-dieutrirungtoc2.png', '950000', '16', N'Kẹo gấu mọc tóc là một trong những dòng thực phẩm chăm sóc tóc được tin dùng hàng đầu tại Mỹ. Kẹo mọc tóc Sugar Bear Hair bổ sung nguồn dưỡng chất, vitamin giúp tóc mọc nhanh, chắc khỏe, tăng độ đàn hồi và bóng mượt hơn. Kẹo gấu mọc tóc Mỹ chính là lựa chọn số 1 cho bạn sở hữu mái tóc khỏe đẹp.', N'Mỹ', '80gr', 6)
Insert into [SanPham] values (N'Thảo Dược Mọc Tóc Mộc Hương Nhu', 'sp-dieutrirungtoc3.png', '290000', '0', N'Thảo Dược Mọc Tóc Mộc Hương Nhu tự hào là sản phẩm hàng Việt Nam chất lượng cao, với thành phần 100% từ thiên nhiên. Mộc Hương Nhu - dòng thảo dược chăm sóc tóc giúp giảm ngay các triệu chứng rụng tóc, phục hồi và kích thích tóc mọc nhanh.', N'Việt Nam', '100ml', 6)
--Loại sản phẩm: Giảm cân tan mỡ bụng
Insert into [SanPham] values (N'Thuốc Giảm Cân Đông Y Mộc Linh', 'sp-giamcan1.png', '540000', '20', N'Thuốc Giảm Cân Đông Y Mộc Linh là sản phẩm hỗ trợ giảm cân dành cho người bị lờn với thuốc lâu năm, khó giảm cân, những người cân nặng quá khổ muốn giảm số cân lớn. Thành phần chiết xuất 100% từ thảo mộc quý thiên nhiên cho hiệu quả mạnh gấp 3 lần so với các loại giảm cân thông thường khác.', N'Việt Nam', '100gr', 7)
Insert into [SanPham] values (N'Kem Tan Mỡ Eveline Slim', 'sp-giamcan2.png', '289000', '24', N'Kem Tan Mỡ Eveline Slim đánh tan các vùng mỡ thừa khó tan ở bụng, eo, mông, đùi,… và ngăn ngừa sự tổng hợp mỡ trở lại. Giảm đến 81% tế bào mỡ dư thừa. Loại bỏ 90% các tích tụ mỡ cứng đầu. Giúp làn da mền mại, mịn màng, săn chắc. Kích thích quá trình tuần hoàn tái tạo da và ngăn ngừa lão hóa hiệu quả.', N'Nga', '250ml', 7)
Insert into [SanPham] values (N'Cà Phê Giảm Mỡ Idol Slim', 'sp-giamcan3.png', '330000', '12', N'Cà phê giảm cân Slim được biết tới là dòng sản phẩm chăm sóc body hoàn hảo hàng đầu Thái Lan. Cà phê giảm cân Thái Lan với thành phần chứa nhiều dược liệu thiên nhiên giúp loại bỏ mỡ thừa, tái tạo tế bào da mới giúp bạn giảm cân dễ dàng, da trắng mịn hơn.', N'Thái Lan', '150gr', 7)
--Loại sản phẩm: Sữa rửa mặt
Insert into [SanPham] values (N'Sữa Rửa Mặt Body Vitamin E', 'sp-suaruamat1.png', '425000', '30', N'Sữa rửa mặt vitamin E là một trong mười dòng sữa rửa mặt đang cực hot trên thị trường hiện nay. Sữa rửa mặt vitamin E The Body Shop sở hữu bảng thành phần dịu nhẹ giúp da sạch sâu, nhanh chóng cấp nước bổ sung độ ẩm, dưỡng da bật tông và tươi tắn.', N'Hàn Quốc', '125ml', 8)
Insert into [SanPham] values (N'Sữa Rửa Mặt Foam Cleansing', 'sp-suaruamat2.png', '185000', '18', N'Sữa Rửa Mặt Foam Cleansing giúp loại bỏ các thành phần oxy hóa có hại cho da, làm sạch và dưỡng ẩm da một cách hiệu quả, tạo lớp bảo vệ da trước các biến đổi của môi trường. Cung cấp các dưỡng chất làm tăng sắc tố da, trẻ hóa các tế bào. Giữ ẩm hiệu quả, duy trì sự đàn hồi cho làn da mịn màng tươi sáng và trắng hồng tự nhiên.', N'Hàn Quốc', '120ml', 8)
Insert into [SanPham] values (N'CLEANSING MILK Sữa rửa mặt Shewhite', 'sp-suaruamat3.png', '320000', '0', N'Cleansing Milk Sữa rửa mặt Shewhite được sản xuất bởi mỹ phẩm Shewhite. Sữa rửa mặt Shewhite loại bỏ bụi bẩn trên da, tẩy tế bào chết, nuôi dưỡng da sáng mịn, trắng hồng.', N'Việt Nam', '125ml', 8)
--Loại sản phẩm: Nước tẩy trang
Insert into [SanPham] values (N'Nước Tẩy Trang Byphasse Solution', 'sp-nuoctaytrang1.png', '250000', '18', N'Nước Tẩy Trang Byphasse Solution Micellaire có xuất xứ từ đất nước Tây Ban Nha xinh đẹp. Tẩy trang Byphasse sử dụng công nghệ Micellar facial cleanser, micellar cleansing water loại bỏ mọi bụi bẩn, cả lớp makeup cứng đầu trên da, dưỡng da mềm mại.', N'Tây Ban Nha', '500ml', 9)
Insert into [SanPham] values (N'Nước Tẩy Trang Mắt Môi Lip Eye Remover', 'sp-nuoctaytrang2.png', '140000', '14', N'Nước Tẩy Trang Mắt Môi Lip Eye Remover được nhập khẩu chính hãng từ Hàn Quốc. Nước Tẩy Trang Mắt Môi Lip Eye Remover xoa dịu da, tẩy sạch lớp trang điểm trên mắt môi.', N'Hàn Quốc', '350ml', 9)
Insert into [SanPham] values (N'Nước Tẩy Trang Không Cồn BIODERMA SEBIUM', 'sp-nuoctaytrang3.png', '190000', '22', N'Nước tẩy trang không cồn Bioderma Sebium H2O được nhập khẩu chính hãng từ Pháp. Nước tẩy trang Bioderma loại bỏ bụi bẩn, make up trên da, kiềm chế lượng dầu, hồi phục vết nứt trên da, cho da mềm mịn.', N'Pháp', '100ml', 9)
--Loại sản phẩm: Kem tẩy lông
Insert into [SanPham] values (N'Gel Dịu Da Và Làm Chậm Mọc Lông CLEO', 'sp-kemtaylong1.png', '185000', '16', N'Sản phẩm dành riêng cho làn da sau tẩy lông, nhất là da nhạy cảm dễ bị kích ứng nhẹ như nóng rát, khô da,… Thành phần lá trà xanh giúp làm mềm và dịu da tức thì, giảm thiểu tối đa khả năng kích ứng. Hạn chế sự phát triển keratin, từ đó kéo dài thời gian lông mọc lại.', N'Mỹ', '50gr', 10)
Insert into [SanPham] values (N'Kem Tẩy Lông Missha In Shower Comfort', 'sp-kemtaylong2.png', '250000', '12', N'Kem tẩy lông Missha In Shower Comfor được nhập khẩu chính hãng từ mỹ phẩm Hàn Quốc. Kem diệt lông Missha tiêu diệt lông nhanh gọn, cung cấp tinh chất dưỡng da mềm mại.', N'Hàn Quốc', '100gr', 10)
Insert into [SanPham] values (N'Kem Tẩy Lông G9 Skin Shining Waxing Cream', 'sp-kemtaylong3.png', '250000', '20', N'Kem Tẩy Lông G9 Skin Shinning Waxing Cream được nhập khẩu chính hãng từ Hàn Quốc. Kem Tẩy Lông G9 Skin tẩy sạch lông, dưỡng ẩm cho da mềm mại.', N'Hàn Quốc', '120gr', 10)
--Loại sản phẩm: Kem chống nắng
Insert into [SanPham] values (N'Kem Chống Nắng Neutrogena Ultra Sheer', 'sp-kemchongnang1.png', '290000', '0', N'Kem chống nắng Neutrogena Ultra Sheer sử dụng công nghệ Helioplex độc quyền, có khả năng tạo những hoạt chất ổn định, bền vững giúp ngăn chặn tác hại của tia UVA và tia UVB. Tích hợp Butylene Glycol dưỡng ẩm và làm đẹp da, Vitamin A, C, E nuôi dưỡng làn da.', N'Mỹ', '88ml', 11)
Insert into [SanPham] values (N'Xịt Chống Nắng JM Solution Marine', 'sp-kemchongnang2.png', '300000', '24', N'JM Solution Marine Luminoso Pearl Sun Spray mang lại hiệu quả cao trong việc CHỐNG NẮNG – BẢO VỆ – DƯỠNG ẨM cho làn da của bạn. Với chỉ số chống nắng SPF50 + PA++++, sản phẩm giúp bảo vệ da khỏi các tác hại của tia cực tím, tia UVB, UVA, tia sáng xanh,… suốt một ngày dài.', N'Hàn Quốc', '310ml', 11)
Insert into [SanPham] values (N'Kem Chống Nắng Ice Sun Nature Republic SPF50+', 'sp-kemchongnang3.png', '280000', '14', N'Kem Chống Nắng Nature Republic Ice Sun Puff SPF50+ được nhập khẩu chính hãng từ Hàn Quốc. Kem chống nắng Ice Sun Nature bảo vệ da khỏi tác hại của ánh nắng, ngăn hình thành nám, vết thâm, chống lão hóa da.', N'Hàn Quốc', '100ml', 11)
--Loại sản phẩm: Sữa tắm trắng da
Insert into [SanPham] values (N'Sữa Tắm Trắng Da Cathy Choo', 'sp-suatrangda1.png', '260000', '24', N'Sữa tắm trắng da Cathy Choo được nhập khẩu chính hãng từ mỹ phẩm Thái Lan, giúp da săn chắc, làm đều màu da, cung cấp tinh chất cho da trắng sáng, mềm mịn. Sở hữu ngay làn da trắng hồng tự nhiên cùng Cathy Choo Vàng 24k chỉ sau vài tuần sử dụng.', N'Thái Lan', '750ml', 12)
Insert into [SanPham] values (N'Sữa Tắm Nước Hoa COCO Perfume', 'sp-suatrangda2.png', '220000', '18', N'Sữa tắm nước hoa siêu thơm Coco Perfume được nhập khẩu chính hãng từ Hàn Quốc. Sữa tắm Coco Perfume nước hoa giúp dưỡng ẩm cho da mềm mại, trắng sáng và hương thơm quyến rũ. Mua ngay sữa tắm siêu thơm Coco Perfume hôm nay để nhận được nhiều ưu đãi.', N'Hàn Quốc', '800ml', 12)
Insert into [SanPham] values (N'Sữa Tắm Organic Tảo Xoắn', 'sp-suatrangda3.png', '400000', '8', N'Sữa tắm trắng da Pizu chiết xuất từ ngọc trai đen, dãi yến, linh chi đỏ, cúc, tảo xoắn, men bia... giúp bạn cân bằng độ ẩm da, kiểm soát dầu nhờn, làm trắng da từ sâu bên trong. Đồng thời còn giúp bạn ngăn ngừa lão hóa, viêm da, dị ứng... và trị mụn hiệu quả.', N'Việt Nam', '300ml', 12)
--Loại sản phẩm: Xịt dưỡng tóc
Insert into [SanPham] values (N'Tinh Chất Hà Thủ Ô', 'sp-xitduongtoc1.png', '400000', '26', N'Tinh chất Hà Thủ Ô kích thích mọc móc giúp tóc mọc lại nhanh An Toàn và Hiệu Quả. Sản phẩm Hà Thủ Ô là dòng sản phẩm được ưa chuộng rất nhiều,ngăn rụng tóc, giúp tóc mọc nhanh và óng mượt.', N'Việt Nam', '200ml', 13)
Insert into [SanPham] values (N'Serum Tinh Dầu Bưởi', 'sp-xitduongtoc2.png', '150000', '0', N'Serum tinh dầu bười với chiết xuất thành phần từ 100% thiên nhiên. Tinh dầu bưởi được sản xuất ở dạng serum giúp da đầu hấp thu nhanh, ngăn rụng tóc dứt điểm, kích thích mọc tóc hiệu quả.', N'Việt Nam', '180ml', 13)
Insert into [SanPham] values (N'Serum Vỏ Cam Pizu', 'sp-xitduongtoc3.png', '210000', '0', N'Serum Vỏ Cam Pizu được sản xuất bởi mỹ phẩm thiên nhiên Pizu. Serum Vỏ Cam Pizu cung cấp chất dinh dưỡng kích thích mọc tóc, ngăn ngừa rụng tóc, cho mái tóc suôn mượt, bồng bềnh.', N'Việt Nam', '200ml', 13)
--Loại sản phẩm: Kem ủ tóc
Insert into [SanPham] values (N'Kem Ủ Tóc Phục Hồi Hư Tổn Bed Head Tigi', 'sp-kemutoc1.png', '395000', '26', N'Kem Ủ Tóc Phục Hồi Hư Tổn Bed Head Tigi cung cấp dưỡng chất để tóc vượt qua tình trạng hư tổn nặng, nuôi dưỡng tóc từ gốc đến ngọn, phục hồi cấu trúc sợi tóc. Cung cấp độ ẩm giúp tóc mềm mượt, giảm xơ rối và gãy rụng. Giúp tóc bóng hơn với độ bồng bềnh tự nhiên.', N'Mỹ', '200gr', 14)
Insert into [SanPham] values (N'Kem Ủ Tóc NutriCare Fanola', 'sp-kemutoc2.png', '450000', '26', N'Kem ủ tóc NutriCare Fanola chính là giải pháp hoàn hảo cho mái tóc được nhập khẩu chính hãng từ Ý, với thành phần chứa nhiều dưỡng chất giàu protein giúp cải thiện tối đa tình trạng rụng tóc, ngăn khô xơ, phục hồi tóc hư tổn, cho tóc suôn dày, mềm mượt và óng ả.', N'Ý', '1500ml', 14)
Insert into [SanPham] values (N'Dầu Gội Nhuộm Tóc Bubble Hair Coloring', 'sp-kemutoc3.png', '230000', '26', N'Dầu Gội Nhuộm Tóc Bubble Hair Coloring được nhập khẩu chính hãng từ Hàn Quốc. Dầu Gội Nhuộm Tóc Bubble Hair Coloring giúp màu bám đều lên tóc, màu tóc ưng ý, tự nhiên và không xơ rối.', N'Hàn Quốc', '800ml', 14)
--Loại sản phẩm: Thương hiệu Pháp
Insert into [SanPham] values (N'Dầu Gội Khô Evoluderm', 'sp-thPhap1.png', '200000', '10', N'Dầu gội khô Evoluderm được nhập khẩu chính hãng từ Evoluderm Pháp, giúp bạn làm sạch cặn bẩn, dầu nhờn trên tóc, giúp tóc tơi và suôn mượt, cực kì phù hợp cho những bạn mồ hôi dầu nhiều, người bệnh, mẹ bỉm sữa không gội đầu được...', N'Pháp', '300gr', 15)
Insert into [SanPham] values (N'Lăn Khử Mùi Etiaxil', 'sp-thPhap2.png', '320000', '14', N'Lăn khử mùi Etiaxil được mệnh danh là dòng sản phẩm điều trị mùi hôi cơ thể hàng đầu tại Pháp. Lăn nách Etiaxil sẽ ngay lập tức thẩm thấu giúp vùng da khô ráo, chặn đứng quá trình tiết mồ hôi, điều tiết tuyến bã nhờn, xóa mờ thâm sạm, dưỡng da trắng mịn và khỏe mạnh.', N'Pháp', '15ml', 15)
Insert into [SanPham] values (N'Nước Hoa Chanel Bleu De Chanel', 'sp-thPhap3.png', '980000', '7', N'Nước hoa Blue De Chanel được nhập khẩu chính hãng Pháp, là phiên bản được nâng cấp hoàn toàn từ phiên bản cũ đem lại cho bạn mùi hương đầy mạnh mẽ, quyến rũ của phái mạnh với hương thơm dài lâu.', N'Pháp', '100ml', 15)
--Loại sản phẩm: Thương hiệu Hàn Quốc
Insert into [SanPham] values (N'Kem Dưỡng Trắng Laneige Tone Up Cream', 'sp-thHQ1.png', '630000', '10', N'Kem dưỡng trắng Laneige – bí quyết giúp bạn sở hữu làn da tươi tắn, sáng mịn chỉ sau vài tuần, sở hữu bảng thành phần nhiều dưỡng chất nhanh chóng cấp ẩm giữ cho da luôn ẩm mịn, kích thích quá trình tái tạo collagen, tế bào da mới, dưỡng da sáng hồng, căng bóng từ sâu bên trong.', N'Hàn Quốc', '50ml', 16)
Insert into [SanPham] values (N'Kem Trị Sẹo Rỗ Đông Y Genie Non Fix Skin', 'sp-thHQ2.png', '990000', '18', N'Kem trị sẹo rỗ Đông Y Hàn Quốc được mệnh danh là “siêu phẩm” loại bỏ sẹo nhanh chóng trên thị trường. Kem Genie Non Fix Skin chứa bảng thành phần nhiều dưỡng chất ngay lập tức cấp ẩm, bổ sung collagen, kích thích tái tạo tế bào da mới giúp xóa mờ sẹo, dưỡng da đều màu và trắng sáng hơn.', N'Hàn Quốc', '30gr', 16)
Insert into [SanPham] values (N'Chì Kẻ Mày 2 Đầu Eco EyeBrown Pencil', 'sp-thHQ3.png', '120000', '16', N'Chì kẻ mày 2 đầu Eco EyeBrown Pencil được nhập khẩu chinh hãng từ Innisfree Hàn Quốc, được thiết kế một đầu kẻ cà một đầu chuốt giúp bạn tạo hình lông mày nhanh chóng hơn, đầu kẻ nhiều màu phù hợp với bất kì phong cách trang điểm nào của bạn, đầu chuốt mịn dễ dàng tán đều lông mày một cách rất tự nhiên.', N'Hàn Quốc', '0,3gr', 16)
--Loại sản phẩm: Dầu dừa nguyên chất
Insert into [SanPham] values (N'Dầu Dừa COCO Secret', 'sp-daudua1.png', '190000', '22', N'Dầu dừa Coco Secret 500ml được sản xuất bởi Công ty cổ phần Dầu Dừa Coco Secret Việt Nam. Dầu dừa Coco Secret giúp bạn dưỡng da, dưỡng tóc, tẩy trang... Ngoài ra còn dùng thay thế dầu ăn giúp các món ăn thêm ngon và an toàn cho sức khỏe.', N'Việt Nam', '500ml', 17)
Insert into [SanPham] values (N'Mặt Nạ Dừa Cửu Long', 'sp-daudua2.png', '135000', '0', N'Mặt nạ dừa Cửu Long 2 in 1 được sản xuất bởi mỹ phẩm thiên nhiên Coconut. Mặt nạ dừa giúp cân bằng Ph cho da, làm sạch da, ngăn ngừa mụn cho da trắng sáng, mềm mại.', N'Việt Nam', '10gr', 17)
--Loại sản phẩm: Mật ong thiên nhiên
Insert into [SanPham] values (N'Mật Ong Nguyên Chất QUEEN BEE', 'sp-matong1.png', '500000', '20', N'Mật Ong Nguyên Chất Queen Bee được sản xuất từ trang trại nuôi ong Ba Sáng, Đồng Nai. Mật Ong thiên nhiên Queen Bee không chỉ bổ dưỡng mà còn được xem như 1 vị thuốc cần thiết cho cơ thể, giúp bảo vệ sức khỏe của bạn.', N'Việt Nam', '2000ml', 18)
--Loại sản phẩm: Phụ kiện làm đẹp
Insert into [SanPham] values (N'Bộ Khuôn Tạo Dáng Chân Mày Mini Brow Class', 'sp-phukien1.png', '70000', '14', N'Bộ Khuôn Tạo Dáng Chân Mày Mini Brow Class được nhập khẩu chính hãng từ Hàn Quốc. Bộ Khuôn Tạo Dáng Chân Mày Mini Brow Class giúp định hình chân mày nhanh chóng, không gây lem.', N'Hàn Quốc', '10gr', 19)
Insert into [SanPham] values (N'Cọ Đánh Phấn Phủ Daily Beauty Tools Powder Brush', 'sp-phukien2.png', '160000', '12', N'Cọ Đánh Phấn Phủ Daily Beauty Tools được nhập khẩu chính hãng từ Hàn Quốc, được làm từ lông ngựa non, độ mềm mịn cực cao, chạm vào da cảm giác rất thích, đầu cọ to và tơi giúp thấm phấn được nhiều hơn, đồng thời trang điểm được hơn.', N'Hàn Quốc', '20gr', 19)
Insert into [SanPham] values (N'Cọ Rửa Mặt SkinFood Pore Brush', 'sp-phukien3.png', '150000', '14', N'Cọ Rửa Mặt Skinfood Pore Brush được nhập khẩu chính hãng từ Hàn Quốc. Cọ Rửa Mặt Skinfood Pore Brush làm sạch sâu da ngăn ngừa các loại mụn, tẩy tế bào chết dễ dàng cho da mềm mịn, trắng sáng.', N'Hàn Quốc', '10gr', 19)
--Loại sản phẩm: Nước hoa chính hãng
Insert into [SanPham] values (N'Nước Hoa COCO Mademoiselle Chanel', 'sp-nuochoa1.png', '950000', '10', N'Nước hoa Coco Mademoiselle Chanel cho phái nữ thêm quyến rũ, tự tin. Coco Mademoiselle Chanel sở hữu mùi hương ngọt ngào, đằm thắm mà vô cùng quyến rũ, nhẹ nhàng giúp phái nữ luôn cuốn hút. Sở hữu ngay Coco Mademoiselle để tạo cho mình mùi hương “riêng biệt” độc đáo.', N'Pháp', '100ml', 20)
Insert into [SanPham] values (N'Nước Hoa Versace Eros EDT', 'sp-nuochoa2.png', '900000', '0', N'Versace Eros thể hiện sự mạnh mẽ nam tính thông qua sự kết hợp tinh tế của hương lá bạc hà tươi, vỏ chanh và táo xanh. Hương giữa gây nghiện bởi sự pha trộn của các thành phần hương hoa phương Đông hấp dẫn như đậu tonka, hổ phách, hoa phong lữ, vani cùng với một chút thành phần hương đặc trưng của hương gỗ như gỗ tuyết tùng từ Atlas và Virginia.', N'Ý', '100ml', 20)
Insert into [SanPham] values (N'Bộ Set Nước Hoa Charme Mini 5 Chai', 'sp-nuochoa3.png', '500000', '24', N'Bộ Set Nước Hoa Charme Mini là phiên bản thu nhỏ các sản phẩm được yêu thích nhất của thương hiệu Charme Perfume, bao gồm 2 mùi nam và 3 mùi nữ: King, Giò, Queen , Ori, Mademoiselle và Good Girl.', N'Ý', '50ml', 20)
go

Insert into [TinTuc] values (N'Những cách trị sẹo rỗ từ thiên nhiên an toàn', N'Sẹo rỗ luôn là nỗi ám ảnh của hầu hết chúng ta, chúng không chỉ khiến da tổn thương đau đớn mà còn khiến mất tự tin, khó sử dụng mỹ phẩm. Vậy làm sao để tìm được cách trị sẹo rỗ an toàn, hiệu quả bạn hãy cùng theo dõi qua bài viết sau.', N'Một trong những cách trị sẹo rỗ hiệu quả nhất được nhiều người tin dùng nhất đó là sử dụng nha đam. Nha đam với thành phần chứa nhiều axit amin, vitamin, khoáng tố vi lượng nhất là axít gama linolenic có công dụng làm lành vết thương, kích thích tái tạo tế bào da nhanh chóng. Bạn chỉ cần dùng gel nha đam thoa  đều lên vùng sẹo sau khi rửa sạch trong khoảng 15 phút để dưỡng chất thẩm thấu tốt hơn. Rau má là một trong những cách trị sẹo rỗ lâu năm được lưu truyền trong dân gian và nhiều người tin dùng. Chúng có tác dụng chữa lành sẹo nhanh chóng, dễ kiếm nguyên liệu và dễ thực hiện. Với chiết xuất có chất triterpenoids kết hợp cùng các hợp chất khác sẽ ức chế sự sản sinh quá mức collagen trong các mô sẹo, ngăn sẹo phát triển.', 'tintuc1.png')
Insert into [TinTuc] values (N'Cách trị sẹo lõm trên mặt với vitamin E đơn giản', N'Cách trị sẹo lõm trên mặt từ vitamin E luôn là một trong những phương pháp trị sẹo được rất nhiều người tin dùng. Vậy dùng sao cho hiệu quả và an toàn, bạn hãy cùng theo dõi qua bài viết sau.', N'Hầu hết chúng ta đều biết rằng, vitamin E được xem như là một trong những nguyên liệu làm đẹp được rất nhiều chị em tin dùng. Bởi chúng có tác dụng trẻ hóa làn da, ngăn ngừa các triệu chứng lão hóa hiện hữu trên da một kẻ thù của sắc đẹp. Đặc biệt, nhờ cơ chế hoạt động bổ sung dưỡng chất và kích thích collagen nên vitamin E chính là cách trị sẹo lõm trên mặt tại nhà được nhiều người tin dùng. Dễ dàng nhận thấy vitamin E dạng viên nang dễ kiếm, giá rẻ và khá đơn giản và đem lại hiệu quả khá tốt trong việc loại bỏ sẹo lõm. Mật ong với thành phần chứa chất kháng viêm cùng các nguồn vitamin, dưỡng chất nên nó không chỉ trị thâm, dưỡng da, chống khô da mà còn là cách trị sẹo lõm trên mặt từ thiên nhiên đang để bạn dùng thử.', 'tintuc2.png')
Insert into [TinTuc] values (N'Cách trị sẹo lõm thủy đậu từ thiên nhiên an toàn, hiệu quả', N'Chúng ta đều biết thủy đậu nếu như không điều trị kịp thời đúng cách thì sẽ gây viêm nhiễm và dễ để lại sẹo lõm. Vậy đâu mới là cách trị sẹo lõm an toàn và hiệu quả, bạn hãy cùng theo dõi qua bài viết sau.', N'Sử dụng phương pháp tự nhiên để điều trị sẹo lõm rất tốt cho da, cung cấp các chất dinh dưỡng giúp tái tạo hồi phục da và lấp đầy sẹo lõm nhanh chóng. Trong rau má có chứa lượng lớn collagen đồng thời an toàn, lành tính nên không gây kích ứng da nên đây được xem là cách trị sẹo lõm do thủy đậu được rất nhiều người tin dùng. Cách làm đơn giản, bạn dùng rau má đã rửa sạch và xay nhuyễn, lọc lấy nước và thoa đều lên vùng da mụn đợi khô và rửa lại. Ngoài ra, bạn cũng có thể dùng bã rau má để đắp lên vùng bị sẹo để tăng hiệu quả hơn. Thành phần trong dầu dừa chứa axit béo chống oxy hóa và bảo vệ da, chúng sẽ kích thích quá trình sản xuất collagen giúp mềm da, làm bề mặt sẹo mềm mịn hơn.', 'tintuc3.png')
Insert into [TinTuc] values (N'Cách trị sẹo lõm và những sai lầm bạn chưa biết', N'Những vết sẹo lõm khiến chúng ta mất tự tin trong cuộc sống và luôn tìm phương pháp loại bỏ chúng. Vậy đâu mới là cách trị sẹo lõm an toàn, hiệu quả và nhanh chóng, bạn hãy cùng theo dõi qua bài viết sau.', N'Sẹo lõm hình thành từ những tổn thương do kết cấu các mô da và collagen bị đứt gãy, tế bào không hấp thu được dưỡng chất làm đầy vùng da bị lõm. Cách trị sẹo lõm lâu nămtại nhà từ các nguyên liệu dân gian được rất nhiều người tin dùng, bạn có thể sử dụng các nguyên liệu như: chanh, nghệ, mật ong, nha đam… Ưu điểm của các phương pháp tự nhiên sẽ có giá thành rẻ, dễ tìm kiếm, dễ áp dụng tại nhà. Tuy nhiên, dùng nguyên liệu tự nhiên sẽ khiến tốn thời gian để nhận thấy hiệu quả. ', 'tintuc4.png')
Insert into [TinTuc] values (N'Đi tìm cách trị sẹo lồi hiệu quả, nhanh chóng', N'Cách trị sẹo lồi nhanh chóng hiệu quả luôn là ước muốn của hầu hết người bị sẹo. Bởi sẹo lồi không chỉ khiến bạn đau đớn, gây tổn thương da mà còn khiến bạn mất tự tin trong cuộc sống. Vậy để loại bỏ sẹo lồi tốt nhất bạn hãy cùng theo dõi qua bài viết sau.', N'Trị sẹo bằng cách tiêm thuốc: Bằng cách tiêm trực tiếp chất Corticosteroid vào mô sẹo sẽ giúp phá hủy cấu trúc tổ chức của sẹo, giảm kích thích và giúp sẹo xẹp dần. Thế nhưng việc điều trị thường kéo dài từ 6 – 12 tháng, gây tốn kém và dễ khiến bị teo da tại vùng tiêm, rối loạn kinh nguyệt, mất sắc tố không hồi phục. Phẩu thuật cắt bỏ: Dành cho những vùng sẹo lớn, bác sĩ sẽ giúp bạn cắt bỏ vùng sẹo và ghép da nhằm giảm lực căng trên toàn bộ da được khâu. Nhược điểm của cách trị sẹo lồi công nghệ này là không làm dứt điểm sẹo, gây đau đớn, dễ bị tái phát. Phẩu thuật lạnh: Sử dụng ni tơ lỏng để phá hủy tổ chức của sẹo làm cho sẹo xẹp xuống, giúp bạn loại bỏ sẹo nhanh hơn.', 'tintuc5.png')
go
--------------------------------------------------------------------------------------------------------------------------------------
-- View SanPhamBanChay: bat buoc cho ung dung (HomeController, TrangChuController)
-- Neu thieu view nay, trang chu se loi: Invalid object name 'dbo.SanPhamBanChay'
--------------------------------------------------------------------------------------------------------------------------------------
if object_id(N'dbo.SanPhamBanChay', N'V') is not null
	drop view dbo.SanPhamBanChay
go

create view dbo.SanPhamBanChay
as
select top 10
	SanPham.MaSP,
	SanPham.TenSP,
	cast(SanPham.AnhDaiDien as nvarchar(100)) as AnhDaiDien,
	SanPham.Gia,
	SanPham.KhuyenMai,
	isnull(sum(ChiTietHoaDon.SoLuong), 0) as Tong
from SanPham
left join ChiTietHoaDon on SanPham.MaSP = ChiTietHoaDon.MaSP
group by
	SanPham.MaSP,
	SanPham.TenSP,
	cast(SanPham.AnhDaiDien as nvarchar(100)),
	SanPham.Gia,
	SanPham.KhuyenMai
order by isnull(sum(ChiTietHoaDon.SoLuong), 0) desc
go
--------------------------------------------------------------------------------------------------------------------------------------
-- Kiem tra du lieu sau khi tao database
--------------------------------------------------------------------------------------------------------------------------------------
select * from [NguoiDung]
select * from [DanhMuc]
select * from [LoaiSP]
select * from [SanPham]
select * from [TinTuc]
select * from [HoaDon]
select * from [ChiTietHoaDon]
select * from [UserGroup]
select * from [Role]
select * from [Credential]
select * from [SanPhamBanChay]
go