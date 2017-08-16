/*--------------------------------------------------------------------------------------------------------------------------------
               Purpose: create database tables for Ying app.
			   Creator: Jerry Shen
			   Verison:
			          2017.08.17 JS created

--------------------------------------------------------------------------------------------------------------------------------*/

--user table
CREATE TABLE ying_users(
   userid INT AUTO_INCREMENT PRIMARY KEY,
   username VARCHAR(100),
   email varchar(100),
   password varchar(100),
   create_date datetime,
   unvalid_date datetime,
   valid_flag TINYINT(1)
);

select * from ying_users


 --send email his table

 create table ying_email_send_history
 (
 id INT AUTO_INCREMENT PRIMARY KEY,
 userid int,
 email_to varchar(100),
 content varchar(500),
 send_date datetime,
 send_from varchar(100)
 
 )
 
 -- app log table

select * from ying_logs
 create table ying_logs
 (
 id INT AUTO_INCREMENT PRIMARY KEY,
 username varchar(50),
 appname varchar(50),
 modulename varchar(50),
 usestarttime datetime 
 ) 
 
 -- url tables
 drop table  ying_urls
 create table ying_urls
 (
 id INT AUTO_INCREMENT PRIMARY KEY,
 type varchar(50),
 url varchar(1000),
 description varchar(1000),
 entrytime datetime,
 valid_flag varchar(1)
 )  
 
 

 

