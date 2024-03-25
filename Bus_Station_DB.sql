select @@SERVERNAME as [server name],SUSER_SNAME() as [user name];

-- Creating Passenger table
CREATE TABLE Passenger (
    id INT PRIMARY KEY,
    name VARCHAR(255),
    phone VARCHAR(20)
);
go
-- Creating Bus table
CREATE TABLE Bus (
    number INT PRIMARY KEY,
    class VARCHAR(50)
);
go
-- Creating Driver table
CREATE TABLE Driver (
    id INT PRIMARY KEY,
    name VARCHAR(255),
    address VARCHAR(255),
    bus_id INT FOREIGN KEY REFERENCES Bus(number)
);
go
-- Creating Trip table
CREATE TABLE Trip (
    id INT PRIMARY KEY,
    start_location VARCHAR(255),
    end_location VARCHAR(255)
);
go
-- Creating Travel table
CREATE TABLE Ticket (
    id INT PRIMARY KEY,
    passenger_id INT FOREIGN KEY REFERENCES Passenger(id),
    bus_id INT FOREIGN KEY REFERENCES Bus(number),
    trip_id INT FOREIGN KEY REFERENCES Trip(id),
    seat_number INT,
    date DATE,
    time TIME
);
go
--insertion stored procedures 
CREATE PROCEDURE InsertPassenger
    @id INT,
    @name VARCHAR(255),
    @phone VARCHAR(20)
AS
BEGIN
    INSERT INTO Passenger (id, name, phone)
    VALUES (@id, @name, @phone);
END;

go
CREATE PROCEDURE InsertBus
    @number INT,
    @class VARCHAR(50)
AS
BEGIN
    INSERT INTO Bus (number, class)
    VALUES (@number, @class);
END;

go
CREATE PROCEDURE InsertDriver
    @id INT,
    @name VARCHAR(255),
    @address VARCHAR(255),
    @bus_id INT
AS
BEGIN
    INSERT INTO Driver (id, name, address, bus_id)
    VALUES (@id, @name, @address, @bus_id);
END;

go
CREATE PROCEDURE InsertTrip
    @id INT,
    @start_location VARCHAR(255),
    @end_location VARCHAR(255)
AS
BEGIN
    INSERT INTO Trip (id, start_location, end_location)
    VALUES (@id, @start_location, @end_location);
END;

go
CREATE PROCEDURE InsertTicket
    @id INT,
    @passenger_id INT,
    @bus_id INT,
    @trip_id INT,
    @seat_number INT,
    @date DATE,
    @time TIME
AS
BEGIN
    INSERT INTO Ticket(id, passenger_id, bus_id, trip_id, seat_number, date, time)
    VALUES (@id, @passenger_id, @bus_id, @trip_id, @seat_number, @date, @time);
END;


-- data insertion
-- Inserting data into Passenger table
EXEC InsertPassenger 1, 'Mohamad Ali', '010-1234-5678';
EXEC InsertPassenger 2, 'Sameh Mohamed', '011-9876-5432';
EXEC InsertPassenger 3, 'Sara Ahmad', '012-1111-2222';

-- Inserting data into Bus table
EXEC InsertBus 1, 'Economy';
EXEC InsertBus 2, 'Business Class';
EXEC InsertBus 3, 'Elite Business Class';

-- Inserting data into Driver table
EXEC InsertDriver 1, 'Hassan Ibrahim', '123 Main St', 1;
EXEC InsertDriver 2, 'Ali Ahmad', '456 Oak St', 2;
EXEC InsertDriver 3, 'Mohamed Ali', '789 Elm St', 3;

-- Inserting data into Trip table
EXEC InsertTrip 1, 'Cairo', 'Alexandria';
EXEC InsertTrip 2, 'Luxor', 'Aswan';
EXEC InsertTrip 3, 'Hurghada', 'Sharm El Sheikh';

-- Inserting data into Travel table
EXEC InsertTicket 1, 1, 1, 1, 1, '2024-01-13', '08:00:00';
EXEC InsertTicket 2, 2, 2, 2, 2, '2024-01-14', '10:30:00';
EXEC InsertTicket 3, 3, 3, 3, 3, '2024-01-15', '12:45:00';

go
-- select all data in table stored procedures
CREATE PROCEDURE SelectAllPassenger
AS
BEGIN
    SELECT * FROM Passenger;
END;

go
CREATE PROCEDURE SelectAllBus
AS
BEGIN
    SELECT * FROM Bus;
END;

go
CREATE PROCEDURE SelectAllDriver
AS
BEGIN
    SELECT * FROM Driver;
END;

go
CREATE PROCEDURE SelectAllTrip
AS
BEGIN
    SELECT * FROM Trip;
END;

go
CREATE PROCEDURE SelectAllTicket
AS
BEGIN
    SELECT * FROM Ticket;
END;

-- Execute the procedures to select all data
EXEC SelectAllPassenger;
EXEC SelectAllBus;
EXEC SelectAllDriver;
EXEC SelectAllTrip;
EXEC SelectAllTicket;

go
-- select by primary key stored procedures
CREATE PROCEDURE SelectPassengerById
    @id INT
AS
BEGIN
    SELECT * FROM Passenger WHERE id = @id;
END;

go
CREATE PROCEDURE SelectBusByNumber
    @number INT
AS
BEGIN
    SELECT * FROM Bus WHERE number = @number;
END;

go
CREATE PROCEDURE SelectDriverById
    @id INT
AS
BEGIN
    SELECT * FROM Driver WHERE id = @id;
END;

go
CREATE PROCEDURE SelectTripById
    @id INT
AS
BEGIN
    SELECT * FROM Trip WHERE id = @id;
END;

go
CREATE PROCEDURE SelectTicketById
    @id INT
AS
BEGIN
    SELECT * FROM Ticket WHERE id = @id;
END;

-- Execute the procedures to select one row by primary key
EXEC SelectPassengerById 1;
EXEC SelectBusByNumber 1;
EXEC SelectDriverById 1;
EXEC SelectTripById 1;
EXEC SelectTicketById 1;

go
CREATE OR ALTER PROCEDURE createUser
    @username NVARCHAR(255),
    @password NVARCHAR(255)
AS
BEGIN
DECLARE @q NVARCHAR(MAX);
SET @q = '
CREATE LOGIN ' + QUOTENAME(@username) + ' WITH PASSWORD = ' + QUOTENAME(@password, '''') + ', DEFAULT_DATABASE=Bus_Station_DB, DEFAULT_LANGUAGE=English;
CREATE USER ' + QUOTENAME(@username) + ' FOR LOGIN ' + QUOTENAME(@username) + ';
ALTER ROLE db_owner ADD MEMBER ' + QUOTENAME(@username) + ';';
EXEC sp_executesql @q;
END
go

EXEC createUser 'test3', 'test3';






go
CREATE PROCEDURE validateUser
    @username VARCHAR(255),
	@password VARCHAR(255)
AS
BEGIN
SELECT pwdcompare(@password,password_hash) from sys.sql_logins where name = @username;
END


go
CREATE OR ALTER PROCEDURE whois
    @hostname VARCHAR(255) = 'next',
	@dbname VARCHAR(255) = 'Bus_Station_DB'
AS
BEGIN
DECLARE @t table(spid INT,ecid INT,status varchar(255),loginname VARCHAR(255),hostname VARCHAR(255),blk INT,dbname VARCHAR(255),cmd VARCHAR(255),request_id INT);
INSERT INTO @t execute sp_who
SELECT status,loginname,hostname,dbname,cmd FROM @t WHERE status <> 'sleeping' and hostname = @hostname and dbname = @dbname;
END


execute whois;


