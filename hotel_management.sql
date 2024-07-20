--CREATE DATABASE HotelManagement;

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='rooms')
	CREATE TABLE dbo.rooms (
		room_id	INT IDENTITY(1,1) NOT NULL,
		room_number NVARCHAR(10) NOT NULL, 
		room_type NVARCHAR(50) NOT NULL,
		price_per_night DECIMAL(10, 2) NOT NULL,
		availability BIT NOT NULL -- 1 - номер зан€т, 0 - номер свободен

		CONSTRAINT PK_rooms_room_id PRIMARY KEY (room_id)
	);

IF NOT EXISTS (SELECT *FROM sysobjects WHERE name='customers')
	CREATE TABLE dbo.customers (
		customer_id INT IDENTITY(1,1) NOT NULL,
		first_name NVARCHAR(50) NOT NULL,
		last_name NVARCHAR(50) NOT NULL,
		email NVARCHAR(50) NOT NULL,
		phone_number NVARCHAR(50) NOT NULL,

		CONSTRAINT PK_customers_customer_id PRIMARY KEY (customer_id)
	);

IF NOT EXISTS (SELECT *FROM sysobjects WHERE name='bookings')
	CREATE TABLE dbo.bookings (
		booking_id INT IDENTITY(1,1) NOT NULL,
		customer_id INT NOT NULL,
		room_id INT NOT NULL,
		check_in_date DATE NOT NULL,
		check_out_date DATE NOT NULL,

		CONSTRAINT PK_bookings_booking_id PRIMARY KEY (booking_id),

		CONSTRAINT FK_bookings_customer_id FOREIGN KEY (customer_id) REFERENCES dbo.customers (customer_id),
		CONSTRAINT FK_bookings_room_id FOREIGN KEY (room_id) REFERENCES dbo.rooms (room_id)
	);

IF NOT EXISTS (SELECT *FROM sysobjects WHERE name='facilities')
	CREATE TABLE dbo.facilities (
		facility_id INT IDENTITY(1,1) NOT NULL,
		facility_name NVARCHAR(100) NOT NULL,

		CONSTRAINT PK_bookings_facility_id PRIMARY KEY (facility_id),
	);

IF NOT EXISTS (SELECT *FROM sysobjects WHERE name='rooms_to_facilities')
	CREATE TABLE dbo.rooms_to_facilities (
		room_id INT NOT NULL,
		facility_id INT NOT NULL,

		CONSTRAINT FK_rooms_to_facilities_room_id FOREIGN KEY (room_id) REFERENCES dbo.rooms (room_id),
		CONSTRAINT FK_rooms_to_facilities_facility_id FOREIGN KEY (facility_id) REFERENCES dbo.facilities (facility_id),
	);

INSERT INTO dbo.rooms VALUES 
	('101', 'double', '450', '1'),
	('102', 'double', '450', '1'),
	('103', 'double', '450', '0'),
	('104', 'double', '450', '1'),
	('105', 'single', '350', '1'),
	('106', 'single', '350', '0'),
	('107', 'single', '350', '1');

INSERT INTO dbo.customers VALUES 
	('Neo', 'Iluxa', 'neoiluxa2005@iii.com', '89323232323'),
	('Ilya', 'Tselischev', 'ilyatsal@gmail.com', '893262461'),
	('Dima', 'Belle', 'qweqeqe@yahoo.com', '8932390452'),
	('Elena', 'Orlova', 'elenaorlova@mail.ru.com', '89323232642');

INSERT INTO dbo.bookings VALUES 
	(1, 1, '2023-04-01', '2023-04-05'),
    (2, 3, '2023-04-10', '2023-04-15'),
    (3, 5, '2023-05-20', '2023-05-25'),
    (4, 7, '2023-06-30', '2023-06-02');

INSERT INTO dbo.facilities VALUES
	('WiFi'),
	('Minibar'),
	('Sewing Kit'),
	('Safe Deposit Box'),
	('Telephone'),
	('Ironing Board'),
	('Air conditioner'),
	('Cable TV');

INSERT INTO dbo.rooms_to_facilities VALUES 
	(1, 1), 
    (1, 4),
	(1, 2), 
    (2, 1),
    (3, 1), 
    (3, 2), 
	(4, 1), 
    (4, 3), 
    (5, 1),
    (5, 5), 
	(6, 1), 
    (6, 2), 
    (7, 1);

-- Ќайдите все доступные номера дл€ бронировани€ сегодн€.
SELECT * FROM dbo.rooms WHERE availability = 0;

-- Ќайдите всех клиентов, чьи фамилии начинаютс€ с буквы "S".
SELECT * FROM dbo.customers WHERE last_name LIKE 'S%';

-- Ќайдите все бронировани€ дл€ определенного клиента (по имени или электронному адресу).
SELECT * FROM dbo.bookings 
	JOIN dbo.customers 
	ON bookings.customer_id = customers.customer_id
	WHERE customers.first_name = 'Elena' OR customers.email = 'elenaorlova@mail.ru.com';

-- Ќайдите все бронировани€ дл€ определенного номера.
SELECT * FROM dbo.bookings 
	JOIN dbo.rooms
	ON bookings.room_id = rooms.room_id
	WHERE rooms.room_number = '101';

-- Ќайдите все номера, которые не забронированы на определенную дату.
SELECT * FROM dbo.rooms 
	JOIN dbo.bookings
	ON bookings.room_id = rooms.room_id
	WHERE rooms.availability = 0 AND '2023-07-03' BETWEEN check_in_date AND check_out_date
	
SELECT * FROM dbo.rooms
	LEFT JOIN dbo.bookings
    ON rooms.room_id = bookings.room_id
    AND '2023-04-04' BETWEEN bookings.check_in_date AND bookings.check_out_date
	WHERE bookings.room_id IS NULL;
