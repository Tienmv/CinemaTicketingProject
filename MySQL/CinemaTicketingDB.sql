-- kiểm tra database có tồn tại không mới xoá
drop database if exists CinemaTicketingDB;
-- kiểm tra database có tồn tại không mới tạo
create database if not exists CinemaTicketingDB char set 'utf8mb4';
-- sử dụng database CinemaTicketingDB
use CinemaTicketingDB;

-- Tạo Và Nhập Cơ Sở Rạp Chiếu
create table if not exists Cinemas(
	cine_id int primary key auto_increment,
    cine_address char(255) not null,
    cine_name char(50) not null unique,
    cine_phone char(11) not null
);
insert into Cinemas(cine_address, cine_name, cine_phone) values
	('Tầng 1, TTTM Vincom Mega Mall Royal City, 72A Nguyễn Trãi, Thanh Xuân, Hà Nội','CGV Royal City','0867955181'),
	('Tầng 5, MIPEC Tower, 299 P.Tây Sơn, Đống Đa, Hà Nội', 'CGV MIPEC Tower','0971322130'),
	('Tầng 8, TTTM Vincom, Số 2 P.Phạm Ngọc Thạch, Đống Đa, Hà Nội','BHD Phạm Ngọc Thạch','0262316743'),
	('Tâng 6, Vincom Center, 191 P.Bà Triệu, Hai Bà Trưng, Hà Nội','CGV Bà Triệu','0593125438'),
	('87 P.Láng Hạ, Chợ Dừa, Ba Đình, Hà Nội','Trung Tâm Chiếu Phim Quốc Gia','0978143313');
select * from Cinemas;

-- Tạo Và Nhập Phim
create table if not exists Movies(
	movie_id int primary key auto_increment,
    movie_name char(100),
    movie_description text,
    movie_author char(50),
    movie_actor char(255),
    movie_category char(50),
    movie_time int default 0,
    movie_dateStart date not null,
    movie_dateEnd date not null
);
insert into Movies( movie_name, movie_description, movie_author, movie_actor, movie_category, movie_time, movie_dateStart, movie_dateEnd) values
	('AVENGERS: ENDGAME', 'Cú búng tay của Thanos đã khiến toàn bộ dân số biến mất một nửa. Các siêu anh hùng đánh mất bạn bè, người thân và đánh mất cả chính mình.
    Bộ sáu Avengers đầu tiên tứ tán. Iron Man kẹt lại ngoài không gian, Hawkeye mất tích. Thor, Captain America, Hulk và Black Widow đều chìm trong nỗi đau vô tận vì mất đi những người thân yêu.
    Họ phải làm gì để cứu vãn mọi chuyện ở Avengers: Câu trả lời sẽ có trong Avengers: Endgame'
    ,'Anthony Russo, Joe Russo','Robert Downey Jr., Chris Hemsworth, Chris Evans, Scarlett Johansson, Mark Ruffalo, Jeremy Renner','Hành Động, Giả Tưởng',182,'2022-08-20','2022-12-20'),
    ('Minions: Sự trỗi dậy của Gru','Minions: Sự trỗi dậy của Gru là một bộ phim hoạt hình máy tính hài của Mỹ năm 2022 do Illumination sản xuất và được phân phối bởi Universal Pictures.
    Đây là phần tiếp theo của phần phim ngoại truyện Minions và là phần thứ năm nói chung trong loạt phim Kẻ trộm mặt trăng.','Kyle Balda',
    'Steve Carell, Jean-Claude Van Damme, Taraji P. Henson','Hài Hước, Phiêu Lưu',88,'2022-07-15','2022-09-15')
    ,('Deadpool 2','Deadpool 2 là một bộ phim siêu anh hùng của Mỹ dựa trên nhân vật Deadpool của Marvel Comics, được phát hành bởi 20th Century Fox. 
    Đây là phần thứ mười trong loạt phim X-Men, và là phần tiếp theo của bộ phim Deadpool năm 2016.
    Trong Deadpool 2, Deadpool thành lập nhóm X-Force để bảo vệ một đứa trẻ đột biến tên là Russel khỏi sự tấn công của Cable – một người nửa người nửa máy đến từ tương lai.','David Leitch'
    ,'Reynolds, Chris Pratt, Josh Brolin, Morena Baccarin','Hành Động, Siêu Anh Hùng',134,'2022-06-08','2022-12-08')
    ,('Bố Già', 'Ông bố cộc cằn làm nghề chạy xe ôm luôn cố gắng chăm lo cho vợ và hai đứa con, nhưng ba người họ lúc nào cũng khiến ông bất ngờ.'
    ,'Vũ Ngọc Đãng','Trấn Thành, Tuấn Trần , Ngân Chi','Gia đình, Hài',128,'2022-06-20','2022-11-20')
    ,('Iron Man 3 - Người Sắt 3','Khi thế giới của Tony Stark bị xé nát bởi tên khủng bố ghê gớm được gọi là Mandarin, anh bắt đầu một cuộc phiêu lưu xây dựng lại và trả thù.'
    ,'Shane Black', 'Ben KingsleyDon CheadleGuy PearceGwyneth PaltrowJames Badge DaleJon Favreau','Hành Động, Viễn Tưởng',130, '2022-02-25', '2022-12-25');
select * from Movies;

-- Tạo Show cho phim được chiếu trên rạp theo id
create table if not exists Shows(
	movie_id int not null,
    cine_id int not null,
    constraint pk_ShowTable primary key (movie_id, cine_id),
    constraint fk_Movies_ShowTable foreign key (movie_id) references Movies(movie_id),
    constraint fk_Cinemas_ShowTable foreign key (cine_id) references Cinemas(cine_id)
);
insert into Shows(movie_id, cine_id) values
(1,1), (1,2), (1,3), (1,4), (1,5),
(2,1), (2,2), (2,3), (2,4), (2,5),
(3,1), (3,2), (3,3), (3,4), (3,5),
(4,1), (4,2), (4,3), (4,4), (4,5),
(5,1), (5,2), (5,3), (5,4), (5,5);
select * from Shows inner join Movies on Shows.movie_id = Movies.movie_id;

create table if not exists RoomTypes(
	rt_name char(20) primary key,
    rt_description text
);
insert into RoomTypes(rt_name, rt_description) values
	('2D','Phụ đề việt'),
    ('IMAX2D','Phụ đề việt'),
    ('3D','Phụ đề việt'),
    ('4DX2D','Phụ đề việt'),
    ('Lamour','Thuyết minh');

create table if not exists Rooms(
	room_id int primary key auto_increment,
    cine_id int not null ,
    rt_name char(20) not null,
    room_name char(50) not null unique,
    room_seats text not null,
    constraint fk_Cinemas_Rooms foreign key (cine_id) references Cinemas(cine_id),
    constraint fk_RoomType_Rooms foreign key (rt_name) references RoomTypes(rt_name)
);
insert into Rooms(cine_id, rt_name, room_name, room_seats) values
    (1,'3D','Room 01','_________________________________________________________________ n n _________________________________________________________________ n n N:A1 N:A2 . N:A3 N:A4 N:A5 N:A6 N:A7 N:A8 . N:A9 N:A10 n N:B1 N:B2 . N:B3 N:B4 N:B5 N:B6 N:B7 N:B8 . N:B9 N:B10 n N:C1 N:C2 . V:C3 V:C4 V:C5 V:C6 V:C7 V:C8 . N:C9 N:C10 n N:D1 N:D2 . V:D3 V:D4 V:D5 V:D6 V:D7 V:D8 . N:D9 N:D10 n N:E1 N:E2 . V:E3 V:E4 V:E5 V:E6 V:E7 V:E8 . N:E9 N:E10 n N:F1 N:F2 . V:F3 V:F4 V:F5 V:F6 V:F7 V:F8 . N:F9 N:F10 n N:G1 N:G2 . V:G3 V:G4 V:G5 V:G6 V:G7 V:G8 . N:G9 N:G10 n N:H1 N:H2 . V:H3 V:H4 V:H5 V:H6 V:H7 V:H8 . N:H9 N:H10 n N:I1 N:I2 . N:I3 N:I4 N:I5 N:I6 N:I7 N:I8 . N:I9 N:I10 n N:J1 N:J2 . N:J3 N:J4 N:J5 N:J6 N:J7 N:J8 . N:J9 N:J10 n ____ ____ . D:K3 D:K4 D:K5 D:K6 D:K7 D:K8 . ____ _____ n n'),
    (1,'IMAX2D','Room 02','_________________________________________________________________ n n _________________________________________________________________ n n N:A1 N:A2 . N:A3 N:A4 N:A5 N:A6 N:A7 N:A8 . N:A9 N:A10 n N:B1 N:B2 . N:B3 N:B4 N:B5 N:B6 N:B7 N:B8 . N:B9 N:B10 n N:C1 N:C2 . V:C3 V:C4 V:C5 V:C6 V:C7 V:C8 . N:C9 N:C10 n N:D1 N:D2 . V:D3 V:D4 V:D5 V:D6 V:D7 V:D8 . N:D9 N:D10 n N:E1 N:E2 . V:E3 V:E4 V:E5 V:E6 V:E7 V:E8 . N:E9 N:E10 n N:F1 N:F2 . V:F3 V:F4 V:F5 V:F6 V:F7 V:F8 . N:F9 N:F10 n N:G1 N:G2 . V:G3 V:G4 V:G5 V:G6 V:G7 V:G8 . N:G9 N:G10 n N:H1 N:H2 . V:H3 V:H4 V:H5 V:H6 V:H7 V:H8 . N:H9 N:H10 n N:I1 N:I2 . N:I3 N:I4 N:I5 N:I6 N:I7 N:I8 . N:I9 N:I10 n N:J1 N:J2 . N:J3 N:J4 N:J5 N:J6 N:J7 N:J8 . N:J9 N:J10 n ____ ____ . D:K3 D:K4 D:K5 D:K6 D:K7 D:K8 . ____ _____ n n'),
    (1,'2D','Room 03','_________________________________________________________________ n n _________________________________________________________________ n n N:A1 N:A2 . N:A3 N:A4 N:A5 N:A6 N:A7 N:A8 . N:A9 N:A10 n N:B1 N:B2 . N:B3 N:B4 N:B5 N:B6 N:B7 N:B8 . N:B9 N:B10 n N:C1 N:C2 . V:C3 V:C4 V:C5 V:C6 V:C7 V:C8 . N:C9 N:C10 n N:D1 N:D2 . V:D3 V:D4 V:D5 V:D6 V:D7 V:D8 . N:D9 N:D10 n N:E1 N:E2 . V:E3 V:E4 V:E5 V:E6 V:E7 V:E8 . N:E9 N:E10 n N:F1 N:F2 . V:F3 V:F4 V:F5 V:F6 V:F7 V:F8 . N:F9 N:F10 n N:G1 N:G2 . V:G3 V:G4 V:G5 V:G6 V:G7 V:G8 . N:G9 N:G10 n N:H1 N:H2 . V:H3 V:H4 V:H5 V:H6 V:H7 V:H8 . N:H9 N:H10 n N:I1 N:I2 . N:I3 N:I4 N:I5 N:I6 N:I7 N:I8 . N:I9 N:I10 n N:J1 N:J2 . N:J3 N:J4 N:J5 N:J6 N:J7 N:J8 . N:J9 N:J10 n ____ ____ . D:K3 D:K4 D:K5 D:K6 D:K7 D:K8 . ____ _____ n n'),
    (1,'Lamour','Room 04','_________________________________________________________________ n n _________________________________________________________________ n n N:A1 N:A2 . N:A3 N:A4 N:A5 N:A6 N:A7 N:A8 . N:A9 N:A10 n N:B1 N:B2 . N:B3 N:B4 N:B5 N:B6 N:B7 N:B8 . N:B9 N:B10 n N:C1 N:C2 . V:C3 V:C4 V:C5 V:C6 V:C7 V:C8 . N:C9 N:C10 n N:D1 N:D2 . V:D3 V:D4 V:D5 V:D6 V:D7 V:D8 . N:D9 N:D10 n N:E1 N:E2 . V:E3 V:E4 V:E5 V:E6 V:E7 V:E8 . N:E9 N:E10 n N:F1 N:F2 . V:F3 V:F4 V:F5 V:F6 V:F7 V:F8 . N:F9 N:F10 n N:G1 N:G2 . V:G3 V:G4 V:G5 V:G6 V:G7 V:G8 . N:G9 N:G10 n N:H1 N:H2 . V:H3 V:H4 V:H5 V:H6 V:H7 V:H8 . N:H9 N:H10 n N:I1 N:I2 . N:I3 N:I4 N:I5 N:I6 N:I7 N:I8 . N:I9 N:I10 n N:J1 N:J2 . N:J3 N:J4 N:J5 N:J6 N:J7 N:J8 . N:J9 N:J10 n ____ ____ . D:K3 D:K4 D:K5 D:K6 D:K7 D:K8 . ____ _____ n n'),
    (1,'4DX2D','Room 05','_________________________________________________________________ n n _________________________________________________________________ n n N:A1 N:A2 . N:A3 N:A4 N:A5 N:A6 N:A7 N:A8 . N:A9 N:A10 n N:B1 N:B2 . N:B3 N:B4 N:B5 N:B6 N:B7 N:B8 . N:B9 N:B10 n N:C1 N:C2 . V:C3 V:C4 V:C5 V:C6 V:C7 V:C8 . N:C9 N:C10 n N:D1 N:D2 . V:D3 V:D4 V:D5 V:D6 V:D7 V:D8 . N:D9 N:D10 n N:E1 N:E2 . V:E3 V:E4 V:E5 V:E6 V:E7 V:E8 . N:E9 N:E10 n N:F1 N:F2 . V:F3 V:F4 V:F5 V:F6 V:F7 V:F8 . N:F9 N:F10 n N:G1 N:G2 . V:G3 V:G4 V:G5 V:G6 V:G7 V:G8 . N:G9 N:G10 n N:H1 N:H2 . V:H3 V:H4 V:H5 V:H6 V:H7 V:H8 . N:H9 N:H10 n N:I1 N:I2 . N:I3 N:I4 N:I5 N:I6 N:I7 N:I8 . N:I9 N:I10 n N:J1 N:J2 . N:J3 N:J4 N:J5 N:J6 N:J7 N:J8 . N:J9 N:J10 n ____ ____ . D:K3 D:K4 D:K5 D:K6 D:K7 D:K8 . ____ _____ n n');
select * from Rooms where cine_id = 1;

create table if not exists SeatTypes(
	st_type char(10) primary key,
    st_description text
);
insert into SeatTypes (st_type, st_description) values
	('N','Ghế thường'),
    ('V','Ghế Vip'),
    ('D','Ghế đôi');

-- Tạo Lịch Chiếu
create table if not exists Showtimes(
	showtime_id int primary key auto_increment,
    room_id int not null,
    movie_id int not null,
    showtime_status tinyint not null,
    showtime_weekdays text,
    showtime_timeline text,
    constraint fk_Rooms_Showtimes foreign key (room_id) references Rooms(room_id),
    constraint fk_Movies_Showtimes foreign key (movie_id) references Movies(movie_id), 	
    constraint uq_roomId_movieId_Showtimes unique(room_id, movie_id)
);
insert into Showtimes(room_id, movie_id, showtime_status, showtime_weekdays, showtime_timeline) values
	(1,1,0,'','07:00'),
    (2,2,0,'','09:00'),
    (3,3,0,'','11:00'),
    (4,4,0,'','14:00'),
    (5,5,0,'','16:00');
select * from Showtimes where movie_id = 1 and room_id = 1;

-- Cập nhật Giá Tiền
create table if not exists PriceSeatsOfRoomTypes(
	rt_name char(20),
    st_type char(20),
    price decimal(20, 0),
    constraint pk_PSORT primary key (rt_name, st_type),
    constraint fk_RoomTypes_PSORT foreign key (rt_name) references RoomTypes(rt_name),
    constraint fk_SeatTypes_PSORT foreign key (st_type) references SeatTypes(st_type)
);
insert into PriceSeatsOfRoomTypes(rt_name, st_type, price) values
	('3D','N',60000.00),
    ('3D','D',100000.00),
    ('3D','V',80000.00),
    ('2D','N',50000.00),
    ('2D','D',90000.00),
    ('2D','V',70000.00),
    ('Lamour','N',80000.00),
    ('Lamour','D',150000.00),
    ('Lamour','V',120000.00),
    ('4DX2D','N',100000.00),
    ('4DX2D','D',180000.00),
    ('4DX2D','V',150000.00),
    ('IMAX2D','N',70000.00),
    ('IMAX2D','D',120000.00),
    ('IMAX2D','V',100000.00);
select * from PriceSeatsOfRoomTypes where rt_name = '3D';

create table if not exists ShowtimesDetails(
	showtimed_id int primary key auto_increment ,
    showtime_id int,
    showtimed_timeStart datetime not null,
    showtimed_timeEnd datetime not null,
    showtimed_roomSeats text not null,
    constraint fk_Showtimes_ST foreign key (showtime_id) references Showtimes(showtime_id)
);
insert into ShowtimesDetails(showtime_id, showtimed_timeStart, showtimed_timeEnd, showtimed_roomSeats) values
	(1,'2022/08/04 15:00:00','2022/10/01 09:10:00','_________________________________________________________________ n n _________________________________________________________________ n n N:A1 N:A2 . V:A3 V:A4 V:A5 V:A6 V:A7 V:A8 . N:A9 N:A10 n N:B1 N:B2 . V:B3 V:B4 V:B5 V:B6 V:B7 V:B8 . N:B9 N:B10 n N:C1 N:C2 . V:C3 V:C4 V:C5 V:C6 V:C7 V:C8 . N:C9 N:C10 n N:D1 N:D2 . V:D3 V:D4 V:D5 V:D6 V:D7 V:D8 . N:D9 N:D10 n N:E1 N:E2 . V:E3 V:E4 V:E5 V:E6 V:E7 V:E8 . N:E9 N:E10 n N:F1 N:F2 . V:F3 V:F4 V:F5 V:F6 V:F7 V:F8 . N:F9 N:F10 n N:G1 N:G2 . V:G3 V:G4 V:G5 V:G6 V:G7 V:G8 . N:G9 N:G10 n N:H1 N:H2 . V:H3 V:H4 V:H5 V:H6 V:H7 V:H8 . N:H9 N:H10 n N:I1 N:I2 . V:I3 V:I4 V:I5 V:I6 V:I7 V:I8 . N:I9 N:I10 n N:J1 N:J2 . V:J3 V:J4 V:J5 V:J6 V:J7 V:J8 . N:J9 N:J10 n ____ ____ . D:K3 D:K4 D:K5 D:K6 D:K7 D:K8 . ____ _____ n n'),
    (2,'2022/08/03 09:00:00','2022/10/01 10:50:00','_________________________________________________________________ n n _________________________________________________________________ n n N:A1 N:A2 . V:A3 V:A4 V:A5 V:A6 V:A7 V:A8 . N:A9 N:A10 n N:B1 N:B2 . V:B3 V:B4 V:B5 V:B6 V:B7 V:B8 . N:B9 N:B10 n N:C1 N:C2 . V:C3 V:C4 V:C5 V:C6 V:C7 V:C8 . N:C9 N:C10 n N:D1 N:D2 . V:D3 V:D4 V:D5 V:D6 V:D7 V:D8 . N:D9 N:D10 n N:E1 N:E2 . V:E3 V:E4 V:E5 V:E6 V:E7 V:E8 . N:E9 N:E10 n N:F1 N:F2 . V:F3 V:F4 V:F5 V:F6 V:F7 V:F8 . N:F9 N:F10 n N:G1 N:G2 . V:G3 V:G4 V:G5 V:G6 V:G7 V:G8 . N:G9 N:G10 n N:H1 N:H2 . V:H3 V:H4 V:H5 V:H6 V:H7 V:H8 . N:H9 N:H10 n N:I1 N:I2 . V:I3 V:I4 V:I5 V:I6 V:I7 V:I8 . N:I9 N:I10 n N:J1 N:J2 . V:J3 V:J4 V:J5 V:J6 V:J7 V:J8 . N:J9 N:J10 n ____ ____ . D:K3 D:K4 D:K5 D:K6 D:K7 D:K8 . ____ _____ n n'),
    (3,'2022/08/05 11:00:00','2022/10/01 00:08:00','_________________________________________________________________ n n _________________________________________________________________ n n N:A1 N:A2 . V:A3 V:A4 V:A5 V:A6 V:A7 V:A8 . N:A9 N:A10 n N:B1 N:B2 . V:B3 V:B4 V:B5 V:B6 V:B7 V:B8 . N:B9 N:B10 n N:C1 N:C2 . V:C3 V:C4 V:C5 V:C6 V:C7 V:C8 . N:C9 N:C10 n N:D1 N:D2 . V:D3 V:D4 V:D5 V:D6 V:D7 V:D8 . N:D9 N:D10 n N:E1 N:E2 . V:E3 V:E4 V:E5 V:E6 V:E7 V:E8 . N:E9 N:E10 n N:F1 N:F2 . V:F3 V:F4 V:F5 V:F6 V:F7 V:F8 . N:F9 N:F10 n N:G1 N:G2 . V:G3 V:G4 V:G5 V:G6 V:G7 V:G8 . N:G9 N:G10 n N:H1 N:H2 . V:H3 V:H4 V:H5 V:H6 V:H7 V:H8 . N:H9 N:H10 n N:I1 N:I2 . V:I3 V:I4 V:I5 V:I6 V:I7 V:I8 . N:I9 N:I10 n N:J1 N:J2 . V:J3 V:J4 V:J5 V:J6 V:J7 V:J8 . N:J9 N:J10 n ____ ____ . D:K3 D:K4 D:K5 D:K6 D:K7 D:K8 . ____ _____ n n'),
    (4,'2022/09/08 22:00:00','2022/10/01 22:30:00','_________________________________________________________________ n n _________________________________________________________________ n n N:A1 N:A2 . V:A3 V:A4 V:A5 V:A6 V:A7 V:A8 . N:A9 N:A10 n N:B1 N:B2 . V:B3 V:B4 V:B5 V:B6 V:B7 V:B8 . N:B9 N:B10 n N:C1 N:C2 . V:C3 V:C4 V:C5 V:C6 V:C7 V:C8 . N:C9 N:C10 n N:D1 N:D2 . V:D3 V:D4 V:D5 V:D6 V:D7 V:D8 . N:D9 N:D10 n N:E1 N:E2 . V:E3 V:E4 V:E5 V:E6 V:E7 V:E8 . N:E9 N:E10 n N:F1 N:F2 . V:F3 V:F4 V:F5 V:F6 V:F7 V:F8 . N:F9 N:F10 n N:G1 N:G2 . V:G3 V:G4 V:G5 V:G6 V:G7 V:G8 . N:G9 N:G10 n N:H1 N:H2 . V:H3 V:H4 V:H5 V:H6 V:H7 V:H8 . N:H9 N:H10 n N:I1 N:I2 . V:I3 V:I4 V:I5 V:I6 V:I7 V:I8 . N:I9 N:I10 n N:J1 N:J2 . V:J3 V:J4 V:J5 V:J6 V:J7 V:J8 . N:J9 N:J10 n ____ ____ . D:K3 D:K4 D:K5 D:K6 D:K7 D:K8 . ____ _____ n n'),
    (5,'2022/09/08 23:59:00','2022/10/01 00:30:00','_________________________________________________________________ n n _________________________________________________________________ n n N:A1 N:A2 . V:A3 V:A4 V:A5 V:A6 V:A7 V:A8 . N:A9 N:A10 n N:B1 N:B2 . V:B3 V:B4 V:B5 V:B6 V:B7 V:B8 . N:B9 N:B10 n N:C1 N:C2 . V:C3 V:C4 V:C5 V:C6 V:C7 V:C8 . N:C9 N:C10 n N:D1 N:D2 . V:D3 V:D4 V:D5 V:D6 V:D7 V:D8 . N:D9 N:D10 n N:E1 N:E2 . V:E3 V:E4 V:E5 V:E6 V:E7 V:E8 . N:E9 N:E10 n N:F1 N:F2 . V:F3 V:F4 V:F5 V:F6 V:F7 V:F8 . N:F9 N:F10 n N:G1 N:G2 . V:G3 V:G4 V:G5 V:G6 V:G7 V:G8 . N:G9 N:G10 n N:H1 N:H2 . V:H3 V:H4 V:H5 V:H6 V:H7 V:H8 . N:H9 N:H10 n N:I1 N:I2 . V:I3 V:I4 V:I5 V:I6 V:I7 V:I8 . N:I9 N:I10 n N:J1 N:J2 . V:J3 V:J4 V:J5 V:J6 V:J7 V:J8 . N:J9 N:J10 n ____ ____ . D:K3 D:K4 D:K5 D:K6 D:K7 D:K8 . ____ _____ n n');
select * from Showtimesdetails;

-- Tạo Tài Khoản
create table if not exists Accounts (
	acc_username char(20) primary key,
    cine_id int not null,
    acc_password char(20) not null,
    acc_type char(20) not null,
    constraint fk_Cinemas_Accounts foreign key (cine_id) references Cinemas(cine_id)
);
insert into Accounts(acc_username, cine_id, acc_password, acc_type) values
	('Manager_01',1,'12345','m'),
    ('Staff_01',1,'12345','s');
select * from Accounts;

-- Cấp Quyền
drop user if exists 'tienmv'@'localhost';
create user if not exists 'tienmv'@'localhost' identified by '250220';
    grant all on Cinemas to 'tienmv'@'localhost';
    grant all on Movies to 'tienmv'@'localhost';
    grant all on Shows to 'tienmv'@'localhost';
    grant all on Rooms to 'tienmv'@'localhost';
    grant all on Showtimes to 'tienmv'@'localhost';
    grant all on Accounts to 'tienmv'@'localhost';
    grant all on RoomTypes to 'tienmv'@'localhost';
    grant all on SeatTypes to 'tienmv'@'localhost';
    grant all on PriceSeatsOfRoomTypes to 'tienmv'@'localhost';
    grant all on ShowtimesDetails to 'tienmv'@'localhost';
    grant lock tables on CinemaTicketingDB.* to 'tienmv'@'localhost';

-- lock tables Showtimes write;
-- unlock tables;