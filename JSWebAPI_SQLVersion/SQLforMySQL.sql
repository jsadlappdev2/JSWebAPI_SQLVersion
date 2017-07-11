--1.need to instal MySQL Reference DLLs for the solutions.
   --if you don't have MySQL Dlls. need to download: 
   -- https://dev.mysql.com/downloads/connector/net/
   --you will find the files under: C:\Program Files (x86)\MySQL\MySQL Connector Net 6.9.9\Assemblies\v4.5

--2. create MySQL databse and named as apidb
  -- username: root , password: root

--3. craete tables:

--test table
create table test
(name varchar(50),
 id int(11),
 sex varchar(2),
 address varchar(50));

 insert into test values('test',2,'F','test222');
 insert into test values('jerry',2,'M','test222');

 --tables for todolist
 
CREATE TABLE TodoItem(
   id INT AUTO_INCREMENT PRIMARY KEY,
   Description VARCHAR(500),
   DueDate VARCHAR(255),
    isDone TINYINT(1)
);

insert into TodoItem(Description,DueDate,isDone)  values('test1','2016-08-09',0)
insert into TodoItem(Description,DueDate,isDone)  values('test2','2016-09-09',0)

select * from TodoItem
