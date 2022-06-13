create schema production;

create table production.products
(
 product_id int  identity(1,1) primary key,
name varchar(30) not null,
quantity int not null,
price int not null,
category_id int 
);

create view production.products_view(productId , productName , prodQuantity , prodPrice , categoryId)
as
select * from production.products;

use supermarket
go

alter table production.products
alter column category_id int not null;


SELECT *
FROM production.products p
WHERE NOT EXISTS
(
    SELECT 1 
    FROM production.categories c
    WHERE c.category_id = p.category_id
);

alter table production.products with check
add constraint fk_prod_cat foreign key (category_id) references production.categories(category_id) ;

alter view production.products_view(productId , productName , prodQuantity , prodPrice , categoryId , sellerId)
as
select * from production.products;

create view production.comboboxCateg_view (categoryName)
as
select catName from production.cat_view ;

select catName from production.cat_view;

select catName , count(catId) as numberOfCat
from [production].[cat_view]
group by catName


select count(catName)
from production.cat_view