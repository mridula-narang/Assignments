create database dbEmployee07Oct2024

use dbEmployee07Oct2024

CREATE TABLE Employee
(emp_no INT IDENTITY(1,1) constraint pk_employee_no primary key,
emp_name varchar(20),
salary INT,
boss_no INT constraint fk_boss_id foreign key references Employee(emp_no) null
)

insert into Employee(emp_name,salary,boss_no) values ('Alice',75000,null)

CREATE TABLE Department
(deptname varchar(20) constraint pk_department_name primary key,
floor_no INT,
phone INT,
emp_id INT constraint fk_employee_id foreign key references Employee 
)

insert into Department(deptname,floor_no,phone,emp_id) values ('Management',5,34,1)
insert into Department(deptname,floor_no,phone,emp_id) values ('Marketing',5,38,2)

ALTER TABLE Employee
ADD dept_name varchar(20)


ALTER TABLE Employee
ADD constraint fk_deptname foreign key (dept_name) references Department(deptname)

update Employee set dept_name = 'Management' where emp_no = 1

insert into Employee(emp_name,salary,boss_no,dept_name) values 
('Ned',45000,1,null)

insert into Employee(emp_name,salary,boss_no) values 
('Andrew',25000,2)

insert into Employee(emp_name,salary,boss_no) values 
('Clare',22000,2),
('Todd',38000,1),
('Nancy',22000,5)

insert into Employee(emp_name,salary,boss_no) values 
('Brier',43000,1),
('Sarah',56000,7),
('Sophile',35000,1),
('Sanjay',15000,3),
('Rita',15000,4),
('Gigi',16000,4),
('Maggie',11000,4),
('Paul',15000,3),
('James',15000,3),
('Pat',15000,3),
('Mark',15000,3)

insert into Department(deptname,floor_no,phone,emp_id) values 
('Books',1,81,4),
('Clothes',2,24,4),
('Equipment',3,57,3),
('Furniture',4,14,3),
('Navigation',1,41,3),
('Recreation',2,29,4),
('Accounting',5,35,5),
('Purchasing',5,36,7),
('Personnel',5,37,9)

update Employee set dept_name = 'Marketing' where emp_no IN (2,3,4)
update Employee set dept_name = 'Accounting' where emp_no IN (5,6)
update Employee set dept_name = 'Purchasing' where emp_no IN (7,8)
update Employee set dept_name = 'Personnel' where emp_no = 9
update Employee set dept_name = 'Navigation' where emp_no = 10
update Employee set dept_name = 'Books' where emp_no = 11
update Employee set dept_name = 'Clothes' where emp_no IN (12,13)
update Employee set dept_name = 'Equipment' where emp_no IN (14,15)
update Employee set dept_name = 'Furniture' where emp_no = 16
update Employee set dept_name = 'Recreation' where emp_no = 17

select * from Employee
select * from Department

sp_help Department
sp_help Employee

CREATE TABLE Item
(item_name varchar(100) constraint pk_item_name primary key,
item_type varchar(5),
item_color varchar(15)
)

CREATE TABLE Sales
(
    sales_no INT IDENTITY(101,1) constraint pk_sales_id primary key,
    sales_qty INT,
    itemname varchar(100) constraint fk_item_name foreign key references Item(item_name) not null,
    department_name varchar(20) constraint fk_department_name foreign key references Department(deptname) not null
)

insert into Item(item_name,item_color,item_type) values
('Pocket Knife-Nile','Brown','E'),
('Pocket Knife-Avon','Brown','E'),
('Elephant Polo Stick','Bamboo','R'),
('Camel Saddle','Brown','R'),
('Boots-snake proof','Green','C'),
('Pith helmet','Kakhi','C'),
('Hat-polar explorer','White','C'),
('Hammock','Kakhi','F'),
('Map Case','Brown','E'),
('Safari chair','Kakhi','F'),
('Safari cooling kit','Kakhi','F'),
('Tent 2-person','Kakhi','F'),
('Tent 8-person','Kakhi','F'),
('Stetson','Black','C')

insert into Item(item_name,item_type) values
('Compass','N'),
('Geo positioning system','N'),
('Sextant','N'),
('Map measure','N'),
('Exploring 10 easy lessons','B'),
('How to win foreign friends','B')

select * from Item

insert into Sales(sales_qty,itemname,department_name) values
(2,'Boots-snake proof','Clothes'),
(1,'Pith helmet','Clothes'),
(1,'Sextant','Navigation'),
(3,'Hat-polar explorer','Clothes'),
(5,'Pith helmet','Equipment'),
(2,'Pocket Knife-Nile','Clothes'),
(3,'Pocket Knife-Nile','Recreation'),
(1,'Compass','Navigation'),
(2,'Geo positioning system','Navigation'),
(5,'Map measure','Navigation'),
(1,'Geo positioning system','Books'),
(1,'Sextant','Books'),
(3,'Pocket Knife-Nile','Books'),
(1,'Pocket Knife-Nile','Navigation'),
(1,'Pocket Knife-Nile','Equipment'),
(1,'Sextant','Clothes'),
(1,'Exploring 10 easy lessons','Books'),
(1,'Elephant Polo Stick','Recreation'),
(1,'Camel Saddle','Recreation')

select * from Sales
