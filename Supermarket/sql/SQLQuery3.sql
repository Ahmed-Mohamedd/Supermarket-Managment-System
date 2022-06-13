

create table [production].categories
(
category_id int identity(1,1) primary key,
category_name varchar(40) not null,
description varchar(60) 
)

create view [production].cat_view(catId , catName ,catDescription )
as
select * from [production].categories;


-- create stored procedure to insert into categories_view

create proc production.sp_ins_categ 
@name varchar(40), @desc varchar(60)
as
insert into [production].[cat_view](catName , catDescription)
values (@name , @desc);


create proc production.sp_up_categ 
@id int ,@name varchar(40), @desc varchar(60)
as
update  [production].[cat_view]
set catName = @name , catDescription = @desc 
where catId  =@id;


create proc production.sp_del_categ 
@id int ,@name varchar(40), @desc varchar(60)
as
delete from  [production].[cat_view] 
where catId  =@id;
---------------------------
--------------------------
-----------------------

-- create new table for sellers
 create table production.sellers
 (
 seller_id int identity(1,1) primary key ,
 name varchar(30) not null,
 age int  ,
 phone varchar(20) not null ,
 address varchar(50),
 password varchar(20) not null
 )


 create view production.sellers_view(sellerId , sellerName , sellerAge , sellerPhone , sellerAddress , sellerPassword)
 as
 select * from production.sellers;

 alter table production.products
 add seller_id int references production.sellers(seller_id);

 -- insert proc
 create proc production.sp_seller_ins
 @name varchar(30), @age int , @phone varchar(20),@address varchar(50),@password varchar(20)
 as
 insert into production.sellers_view (sellerName,sellerAge,sellerPhone,sellerAddress,sellerPassword)
 values(@name,@age,@phone,@address,@password);

 -- update proc
 create proc production.sp_seller_up
 @id int,@name varchar(30), @age int , @phone varchar(20),@address varchar(50),@password varchar(20)
 as
 update  production.sellers_view set sellerName = @name, sellerAge = @age,sellerPhone = @phone,sellerAddress = @address,sellerPassword = @password
 where sellerId = @id;
  
  -- delete proc
  create proc production.sp_seller_del
 @id int,@name varchar(30), @age int , @phone varchar(20),@address varchar(50),@password varchar(20)
 as
 delete from   production.sellers_view 
 where sellerId = @id;

