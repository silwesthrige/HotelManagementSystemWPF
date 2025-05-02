CREATE DATABASE MyHotel;

USE MyHotel;

CREATE TABLE rooms(
	roomID INT IDENTITY(1,1) PRIMARY KEY,
	roomNo VARCHAR(250) NOT NULL UNIQUE,
	roomType VARCHAR(250) NOT NULL,
	bed VARCHAR(250) NOT NULL,
	price DECIMAL(12,2) NOT NULL,
	booked VARCHAR(50) DEFAULT 'NO'

);
UPDATE employee SET pass = 'vish' WHERE LOWER(email) = 'vishwa@gmail.com';
SELECT * FROM employee;

CREATE TABLE Customer(
	customerID INT IDENTITY(1,1) PRIMARY KEY,
	customerName VARCHAR(250) NOT NULL,
	mobile BIGINT  NOT NULL,
	nationality VARCHAR(250) NOT NULL,
	gender VARCHAR(50) NOT NULL,
	dob VARCHAR(50) NOT NULL,
	idProof VARCHAR(50) NOT NULL,
	email VARCHAR(350) NOT NULL,
	checkIn VARCHAR(250) NOT NULL,
	checkOut VARCHAR(250) ,
	chekOut VARCHAR(250) NOT NULL DEFAULT 'NO',
	roomID INT NOT NULL,
	FOREIGN KEY (roomID) REFERENCES rooms(roomID)
);

CREATE TABLE employee(
	employeeID INT IDENTITY(1,1) PRIMARY KEY,
	employeeName VARCHAR(250) NOT NULL,
	mobile BIGINT NOT NULL,
	gender VARCHAR(250) NOT NULL,
	email VARCHAR(250) NOT NULL,
	username VARCHAR(250) NOT NULL,
	pass VARCHAR(250) NOT NULL
);

SELECT * FROM Customer;
SELECT * FROM employee WHERE username = 'sadad';

UPDATE  rooms SET booked = 'NO';
DELETE  FROM Customer;
SELECT COUNT(*) FROM rooms;
DELETE Customer;

DROP TABLE Customer;
DROP TABLE rooms;

SELECT * FROM Customer;
SELECT * FROM rooms WHERE booked = 'NO';
SELECT * FROM rooms WHERE roomNo = '1001';


INSERT INTO Customer(customerName,mobile,nationality,gender,dob,idProof,addres,checkIn,roomID) VALUES (@v1,@v2,@v3,@v4,@v5,@v6,@v7,@v8,@v9);

UPDATE rooms SET booked = 'YES' WHERE roomID = '1';
SELECT * FROM rooms;

SELECT Customer.customerID,Customer.customerName,Customer.mobile,Customer.nationality,Customer.gender, Customer.dob,Customer.idProof,Customer.addres,Customer.checkIn,rooms.roomNo,rooms.roomType,rooms.bed,rooms.price FROM Customer inner join rooms on Customer.roomID = rooms.roomID WHERE chekOut = 'NO';

SELECT 
    Customer.customerID,
    Customer.customerName,
    Customer.mobile,
    Customer.nationality,
    Customer.gender,
    Customer.dob,
    Customer.idProof,
    Customer.addres,
    Customer.checkIn,
    rooms.roomNo,
    rooms.roomType,
    rooms.bed,
    CONCAT('Rs.', FORMAT(rooms.price,2)) AS price
FROM 
    Customer
INNER JOIN 
    rooms ON Customer.roomID = rooms.roomID
WHERE 
    checkOut = 'NO';

	SELECT Customer.customerID, Customer.customerName, Customer.mobile, Customer.nationality, Customer.gender, Customer.dob, Customer.idProof, Customer.addres, Customer.checkIn, rooms.roomNo, rooms.roomType, rooms.bed, rooms.price FROM Customer INNER JOIN rooms ON Customer.roomID = rooms.roomID WHERE LOWER(Customer.checkOut) = 'NO' AND LOWER(Customer.customerName) LIKE '%shan%';

SELECT Customer.customerID, Customer.customerName, Customer.mobile, Customer.nationality, Customer.gender, Customer.dob, Customer.idProof, Customer.addres, Customer.checkIn, rooms.roomNo, rooms.roomType, rooms.bed, rooms.price FROM Customer INNER JOIN rooms ON Customer.roomID = rooms.roomID WHERE Customer.chekOut = 'NO' AND LOWER(Customer.customerName) LIKE '%s%';

UPDATE Customer SET chekOut='YES' , checkOut ='' WHERE customerID = '' UPDATE rooms SET booked = 'NO' WHERE roomNo = '';

UPDATE Customer SET chekOut='YES' , checkOut ='4/28/2025' WHERE LOWER(customerName) = 'shan' UPDATE rooms SET booked = 'NO' WHERE roomNo = '1003';"

DELETE FROM employee WHERE LOWER(employeeName) ='charuka';
SELECT * FROM employee;

SELECT Customer.customerName,Customer.mobile,Customer.nationality,Customer.idProof,Customer.email,Customer.checkIn,rooms.roomNo,rooms.roomType,rooms.bed,rooms.price FROM Customer inner join rooms on Customer.roomID = rooms.roomID WHERE chekOut = 'NO' AND LOWER(Customer.customerName) = '';
SELECT Customer.customerName,Customer.mobile,Customer.nationality,Customer.idProof,Customer.email,Customer.checkIn,rooms.roomNo,rooms.roomType,rooms.bed,rooms.price FROM Customer inner join rooms on Customer.roomID = rooms.roomID WHERE chekOut = 'NO' ;
SELECT Customer.customerName,Customer.mobile,Customer.nationality,Customer.idProof,Customer.email,Customer.checkIn,rooms.roomNo,rooms.roomType,rooms.bed,rooms.price FROM Customer inner join rooms on Customer.roomID = rooms.roomID;

SELECT 
                       Customer.customerID,
                        Customer.customerName,
                        Customer.mobile,
                        Customer.nationality,
                        Customer.gender,
                        Customer.dob,
                        Customer.idProof,
                        Customer.email,
                        Customer.checkIn,
                        Customer.checkOut,
                        rooms.roomNo,
                        rooms.roomType,
                        rooms.bed,
                        rooms.price
                    FROM Customer 
                    INNER JOIN rooms ON Customer.roomID = rooms.roomID 
                    WHERE Customer.checkOut = 'NO';

SELECT Customer.customerName, Customer.mobile, Customer.nationality, Customer.idProof, Customer.email, Customer.checkIn,rooms.roomNo, rooms.roomType, rooms.bed, rooms.priceFROM CustomerINNER JOIN rooms ON Customer.roomID = rooms.roomID;

SELECT Customer.checkIn, Customer.checkOut, rooms.price FROM Customer INNER JOIN rooms ON Customer.roomID = rooms.roomID WHERE LOWER(customerName) = 'jeram';