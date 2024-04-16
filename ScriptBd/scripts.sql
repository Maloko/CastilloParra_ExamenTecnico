
create database EmpresaOeschle;
GO


use  EmpresaOeschle;
GO


create table cliente(
IdCliente int primary key identity,
Nombres varchar(50),
Apellidos varchar(100),
FechaNac datetime
)

GO

alter procedure sp_listarClientes
as
begin
	select IdCliente,Nombres,Apellidos,FechaNac,
		  DATEDIFF(YEAR, FechaNac, GETDATE()) - 
            CASE
                WHEN (DATEADD(YEAR, DATEDIFF(YEAR, FechaNac, GETDATE()), FechaNac) > GETDATE()) 
                THEN 1
                ELSE 0
            END AS Edad
	from cliente

end

GO

create procedure sp_buscarCliente
(
@idcliente int
)
as
begin
	select IdCliente,Nombres,Apellidos,FechaNac,
		  DATEDIFF(YEAR, FechaNac, GETDATE()) - 
            CASE
                WHEN (DATEADD(YEAR, DATEDIFF(YEAR, FechaNac, GETDATE()), FechaNac) > GETDATE()) 
                THEN 1
                ELSE 0
            END AS Edad
	from cliente
	where IdCliente=@idcliente

end

GO

create procedure sp_crearCliente
(
@nombres varchar (50),
@apellidos varchar(100),
@fechanac datetime
)
as
begin



insert into cliente(Nombres,Apellidos,FechaNac)
values (@nombres,@apellidos,@fechanac)


end

	 