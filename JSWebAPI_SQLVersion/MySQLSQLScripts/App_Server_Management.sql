-- this SQL Scripts are used to maintain SQLs for APP Server.


-------------------------------manage Azure,Google,Amazon API keys------------------------------------------------------------------
drop table sys_public_api_keys
CREATE TABLE sys_public_api_keys(
   id INT AUTO_INCREMENT PRIMARY KEY,
   app_name varchar(100),
   apikey_provider VARCHAR(100),
   apikey_name VARCHAR(100),
   apikey_value1 VARCHAR(100),
   apikey_value2 VARCHAR(100),
   endpoint_1 VARCHAR(200),
   endpoint_2 VARCHAR(200),
   ref_url VARCHAR(200),
   is_free varchar(10),
   apply_email  VARCHAR(100),
   apply_password  VARCHAR(100),
   apply_info varchar(200),
   apply_date datetime,
   expire_date datetime,
   valid_flag varchar(10)
);

insert into sys_public_api_keys(app_name,apikey_provider,apikey_name,apikey_value1,apikey_value2,endpoint_1,endpoint_2,ref_url,is_free,apply_email,apply_password,apply_info,apply_date,expire_date,valid_flag)
values(
'DailyLifeHelper',
'MS_Azure',
'Computer_Vision_API',
'c514e88fbb2a47f382ae9c62581b2cd9',
'dabf4e6831934613a1999f6af7de79e1',
'https://westcentralus.api.cognitive.microsoft.com/vision/v1.0',
null,
'https://azure.microsoft.com/en-us/services/cognitive-services/computer-vision/',
'Y',
'jerryshenhz@gmail.com',
null,
null,
,
(CURDATE()+INTERVAL 30 DAY),
'Y')

select * from sys_public_api_keys


select apikey_provider,apikey_name,apikey_value1,apikey_value2,endpoint_1,endpoint_2,ref_url,is_free,apply_email,apply_password,apply_info,apply_date,expire_date,valid_flag  from sys_api_keys where valid_flag='Y'  order by apikey_provider, apikey_name, id