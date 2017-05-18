delete from Product
delete from Store
go
DBCC CHECKIDENT ('[Store]', RESEED, 0);
DBCC CHECKIDENT ('[Product]', RESEED, 0);
go
insert into store (name) values ('ToysNStuff')
insert into store (name) values ('FoodBasket')
insert into store (name) values ('FurnaturePlus')

go
insert into product (Store,Name,price) values (1,'Doll',5.99)
insert into product (Store,Name,price) values (1,'Tonka Truck',18.99)
insert into product (Store,Name,price) values (1,'Nerf Gun',12.19)
insert into product (Store,Name,price) values (2,'Bread',2.39)
insert into product (Store,Name,price) values (1,'Cake Mix',4.59)
insert into product (Store,Name,price) values (3,'Couch',235.97)
insert into product (Store,Name,price) values (3,'Bed',340.99)
insert into product (Store,Name,price) values (3,'Table',87.99)
insert into product (Store,Name,price) values (3,'Chair',45.99)


select * from store
select * from product

