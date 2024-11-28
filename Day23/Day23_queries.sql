select * from publishers

select * from publishers where country = 'USA'

select * from publishers where country != 'USA'

select * from publishers where country <> 'USA'

select * from publishers where country = 'USA' and city = 'Berkeley'

select * from publishers where country = 'USA' or city = 'Paris'

select * from titles

select * from titles where price<15

select title,price,notes from titles

select title,price,notes from titles where price<15

select title from titles where price>8 and price<15

select title from titles where price between 8 and 15

select * from titles where type = 'business' or type = 'psychology'

select * from titles where type in ('psychology','business')

select * from titles where price is null

select * from titles where title like '%Anger%'
go
select * from titles where title like '_he%'

--select all the employee whose first name has 'e'
select * from employee where fname like '%e%'
--select all the employees who are hired before 1991-10-26
select * from employee where hire_date < '1991-10-26'
--select employees who work for the publisher 0877 and is having middle name
select * from employee where pub_id = 0877 and minit <> ''


select * from employee order by fname 
select * from employee order by fname desc

select * from employee order by pub_id

select * from employee order by pub_id, fname
select * from employee order by pub_id desc, fname
select * from employee order by pub_id, fname desc

select * from employee where fname like '%e%' order by pub_id

---how many people work for the publisher 0736
select count(emp_id)  from employee
select count(emp_id) as Number_Of_Employees from employee
select count(emp_id)  Number_Of_Employees  from employee
select count(emp_id) as Number_Of_Employees from employee where pub_id = 0736


select min(job_id) Least_Job_Id  from employee

select min(hire_date) First_Hired_Date  from employee

select * from titles
--Find the least priced book
select min(price) Min_price from titles
--Find the average cost of books published by 1389
select AVG(price) avg_price from titles where pub_id = 1389
--Find the total price of all the books of type business   
select sum(price) total_price from titles where type = 'business'
--Find the most expensive book
select max(price) max_price from titles
--Find teh number of books inteh type popular_comp
select count(title) from titles where type = 'popular_comp'

Select pub_id, avg(price) average_price from titles group by pub_id

select type, count(title_id) Number_of_books from titles group by type

--number of employees for every publisher
select pub_id, count(emp_id) Number_Of_Employees from employee group by pub_id

--select the number of publishers in every country
select country,count(pub_id) Number_Of_Publishers from publishers group by country