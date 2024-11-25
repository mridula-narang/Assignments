select * from publishers
select * from titles
--Print all the books published by Binnet & Hardley
select pub_id from publishers where pub_name = 'Binnet & Hardley'
select * from titles where pub_id = '0877'

--alternatively using a sub query
select * from titles where pub_id = 
(select pub_id from publishers where pub_name = 'Binnet & Hardley')

--all employess working for Binnet & Hardley
select * from Employee where pub_id = 
(select pub_id from publishers where pub_name = 'Binnet & Hardley')

select * from Employee where pub_id in 
(select pub_id from publishers where country = 'USA')	

--Books that are priced more than teh average of all  books
select * from titles where price >=
(select round(avg(price),2) from titles)

select round(14.8734,2) -- 14.8700
select round(14.8764,2) -- 14.8800

select round(14.8764,0) -- 15.0000
select round(17.8764,-1) -- 20.0000
select round(14.8764,-1) --10.0000
select round(14.8764,-2) -- 0.0000	

select * from authors

select * from titles where title_id in
(select title_id from titleauthor where au_id =
(select au_id from authors where au_fname = 'Johnson' and au_lname = 'White'))

select * from titles where title_id in
(select title_id from titleauthor where au_id =
(select au_id from authors where au_fname = 'Stearns' and au_lname = 'MacFeather'))

--print the details of authors who authored the books that sell for more than 15 bucks
select * from authors where au_id in
(select au_id from titleauthor where title_id in
(select title_id from titles where price > 15))

--get all the book sale details by the publisher Binnet & Hardley
select * from sales where title_id in
(select title_id from titles where pub_id =
(select pub_id from publishers where  pub_name = 'Binnet & Hardley'))

select * from publishers
select * from sales

select concat(au_fname,' ',au_lname) author_name from authors
select concat(SUBSTRING(title,1,5),'...') from titles

select title_id, title, price , pub_id,
RANK() over (order by pub_id) Publisher_Rank
from titles

select title_id, title, price , pub_id,
DENSE_RANK() over (order by pub_id) Publisher_Rank
from titles

select
pub_name Publisher_Name, title Book_name
from publishers join titles
on
publishers.pub_id = titles.pub_id

select
pub_name Publisher_Name, fname Employee_Name
from publishers p inner join employee e
on
p.pub_id = e.pub_id

select * from sales
--print the book name and the quantity billed for all the bills
select ord_num Order_Number,
t.title_id,title Book_name, qty Quantity
from titles t join sales s
on t.title_id = s.title_id

--print the employee full name and his job_id and job description
select e.job_id, concat(e.fname,' ',e.minit,' ',e.lname) employee_name, job_desc
from employee e join jobs j
on e.job_id = j.job_id
order by e.job_id

--print the job description and number of employees in the job
select j.job_desc Job_Description, COUNT(e.emp_id) Number_of_employees
from jobs j 
join employee e on j.job_id = e.job_id
group by j.job_desc

--print author name and book name
select concat(au_fname,' ',au_lname) 'Author name', title 'Book Name'
from authors a join titleauthor ta
on a.au_id = ta.au_id
join titles t
on t.title_id = ta.title_id
order by a.au_id

--print author name, publisher name and book name
select concat(au_fname,' ',au_lname) 'Author name', title 'Book Name',pub_name 'Publisher Name'
from authors a join titleauthor ta
on a.au_id = ta.au_id
join titles t
on t.title_id = ta.title_id
join publishers p
on p.pub_id = t.pub_id
order by a.au_id

select
pub_name Publisher_Name, title Book_Name
from publishers  left outer join titles
on
publishers.pub_id = titles.pub_id

select concat(au_fname,' ',au_lname) 'Author name', title 'Book Name',pub_name 'Publisher Name'
from authors a left outer join titleauthor ta
on a.au_id = ta.au_id
left outer join titles t
on t.title_id = ta.title_id
full outer join publishers p
on p.pub_id = t.pub_id