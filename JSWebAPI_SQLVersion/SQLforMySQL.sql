--1.need to instal MySQL Reference DLLs for the solutions.
   --if you don't have MySQL Dlls. need to download: 
   -- https://dev.mysql.com/downloads/connector/net/
   --you will find the files under: C:\Program Files (x86)\MySQL\MySQL Connector Net 6.9.9\Assemblies\v4.5

--2. create MySQL databse and named as apidb
  -- username: root , password: root
  
  -- username: root password: frank110*****  -->home pc

--3. craete tables:
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


--user table
CREATE TABLE users(
   userid INT AUTO_INCREMENT PRIMARY KEY,
   username VARCHAR(100),
   email varchar(100),
   password varchar(100),
   create_date datetime,
   unvalid_date datetime,
   valid_flag TINYINT(1)
);

select * from users where valid_flag=1 and username ='jerryshen'

select concat( concat(concat('Hi ',' ', username),'! Your password is: ',password),'. Please try again!',' (Admin)') from users

--new todoitem with userid

drop table NewTodoItem
 
CREATE TABLE NewTodoItem(
   id INT AUTO_INCREMENT PRIMARY KEY,
   username varchar(100),
   Description VARCHAR(500),
   DueDate VARCHAR(255),
    isDone TINYINT(1)
);

 select * from NewTodoItem
 
 --table for send email
 
 drop table email_send_history
 create table email_send_history
 (
 id INT AUTO_INCREMENT PRIMARY KEY,
 userid int,
 email_to varchar(100),
 content varchar(500),
 send_date datetime,
 send_from varchar(100)
 
 )
 
 select * from email_send_history
 
 
 SELECT CHARACTER_SET_NAME, DESCRIPTION
       FROM INFORMATION_SCHEMA.CHARACTER_SETS
       WHERE DESCRIPTION LIKE '%Chin%'
       OR DESCRIPTION LIKE '%Japanese%'
       OR DESCRIPTION LIKE '%Korean%'
       ORDER BY CHARACTER_SET_NAME;
       
       
       
-------------table to store files in amazon s3 bucket
drop table file_on_awss3
 create table file_on_awss3
 (
 id INT AUTO_INCREMENT PRIMARY KEY,
 bucket_name varchar(100),
 sub_folder varchar(100),
 file_orginal_name varchar(100),
 file_path varchar(200),
 file_saved_name varchar(200),
 flle_type varchar(20),
 file_url varchar(200),
 upload_person varchar(100),
 upload_date datetime

 )
 
 select * from file_on_awss3
       

 
