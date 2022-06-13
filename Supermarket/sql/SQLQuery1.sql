create schema login;

create table login.users
(
id int identity(1,1) primary key,
user_name varchar(50) not null,
password varchar(20) not null,
user_type varchar(10) not null
);


create view login.users_view (userId ,userName , password , userType )
as
select * from users ;



