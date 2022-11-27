--PROCEDIMIENTO PARA REGISTRAR UN ROL
CREATE PROC usp_RegistrarRol(
@Nombre varchar(300),
@Descripcion varchar(300),
@Resultado bit output
)as
begin
	SET @Resultado = 1 --existe
	IF NOT EXISTS (SELECT * FROM TROLES WHERE Nombre = @Nombre) --si no existe el correo de la variable 

		insert into TROLES(Nombre,Descripcion,Activo) values (
		@Nombre,@Descripcion,1) --1 es activo
	ELSE
		SET @Resultado = 0
	
end
go

--PROCEDIMIENTO PARA MODIFICAR ROL
create procedure usp_ModificarRol(
@IdRol int,
@Nombre varchar(300),
@Descripcion varchar(300),
@Activo bit,
@Resultado bit output
)
as
begin
	SET @Resultado = 1 --existe
	IF NOT EXISTS (SELECT * FROM TROLES WHERE Nombre = @Nombre and IdRol != @IdRol) --hago select y si no existe inserto
		
		update TROLES set 
		Nombre = @Nombre,
		Descripcion = @Descripcion,
		Activo = @Activo
		where IdRol = @IdRol

	ELSE
		SET @Resultado = 0 --si eciste no hagas insert

end

go


--PROCEDIMIENTO PARA ELIMINAR ROL
create procedure usp_EliminarRol(
@IdRol int,
@Resultado bit output
)
as
begin

	delete from TROLES where IdRol = @IdRol
	SET @Resultado = 1
end

go