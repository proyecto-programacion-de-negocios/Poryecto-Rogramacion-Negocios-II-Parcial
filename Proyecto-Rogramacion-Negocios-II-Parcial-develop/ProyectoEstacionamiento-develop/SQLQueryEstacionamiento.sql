/*
	Objetivo: Crear la base de datos Zoologico
	para utilizarla en la conectividad
	con C# WPF.
	Autor: Heydi Lemus, German Antonelli
	Fecha: 03/Julio/2019
*/

-- Seleccionar la base de datos por defecto
USE tempdb
GO


-- Crear la base de datos
CREATE DATABASE Estacionamiento
go	


-- Seleccionar la base de datos recién creada 
USE Estacionamiento
GO

-- Crear el esquema a utilizar
CREATE SCHEMA Est
GO

CREATE SCHEMA Co
GO

--DROP DATABASE ESTACIONAMIENTO
--GO

-- Crear la tabla automovil
CREATE TABLE Est.Automovil (
	IdAutomovil INT IDENTITY(1,1) NOT NULL
		CONSTRAINT PK_Id_Automovil PRIMARY KEY CLUSTERED,
	Placa NVARCHAR (8) NOT NULL,
    Tipo INT NOT NULL
)
GO


-- Crear la tabla Tipo
CREATE TABLE Est.Tipo (
	IdTipo INT IDENTITY (1,1) NOT NULL 
		CONSTRAINT PK_Id_Tipo PRIMARY KEY CLUSTERED,
	Nombretipo NVARCHAR (50) NOT NULL
	
)
GO

--Creacion de la tabla Monto
CREATE TABLE Co.Monto(
	IdMonto INT IDENTITY (1,1) NOT NULL
		CONSTRAINT PK_Id_Monto PRIMARY KEY CLUSTERED,
	IdAutomovil INT NOT NULL,
	HoraIngreso TIME NOT NULL,
	HoraSalida TIME NOT NULL
	
)
GO


-- Crear la tabla Registro
CREATE TABLE Est.Registro (
	IdTicket INT IDENTITY (1,1) NOT NULL
		CONSTRAINT PK_Est_Registro PRIMARY KEY CLUSTERED,
	placaAutomovil NVARCHAR(8) NOT NULL,
	IdAutomovil INT NOT NULL, 
	TipoAutomovil INT NOT NULL,
	HoraEntrada DATETIME NOT NULL,
	HoraSalida DATETIME NOT NULL,
	TotalHoras INT NOT NULL,
	Monto DECIMAL NOT NULL,
)
GO



--CREACION DE LAS LLAVES FORANEAS
ALTER TABLE Co.Monto
	ADD CONSTRAINT
	Fk_Un_AutomovilTiene_Muchos_Montos
	FOREIGN KEY (IdAutomovil) REFERENCES Est.Automovil(IdAutomovil)
	ON UPDATE CASCADE
	ON DELETE NO ACTION
GO

ALTER TABLE Est.Automovil
	ADD CONSTRAINT
		FK_Un_TipoTiene_Varios_Automoviles
		FOREIGN KEY (Tipo) REFERENCES Est.Tipo(IdTipo)
		ON UPDATE CASCADE
		ON DELETE NO ACTION
GO


INSERT INTO Est.Automovil(Placa, Tipo)
VALUES	('UPN3452', 1 ),
		('PBT9834', 2 ),
		('REN3432', 3),
		('ARJ2122', 4),
		('JRR8998', 5),
		('AJS1234', 6),
		('ESE4565', 7),
		('ESE4568', 8),
		('ESE4545', 1),
		('GND5678', 2),
	    ('HND1111', 3),
		('JND2222', 4),
		('JPG0933', 5),
		('LND2344', 6),
		('WER5555', 7),
		('WGD6389', 8)
Go


INSERT INTO Est.Tipo(nombretipo)
VALUES	('camioneta'),
		('Pick up'),
		('Turismo'),
		('Camión'),
		('Rastra'),
		('Moto'),
		('Bicicleta'),
		('Bus')
Go

ALTER TABLE Est.Automovil
ADD UNIQUE (Placa);
GO

CREATE TRIGGER TR_Registro
ON Co.Monto FOR INSERT
AS
BEGIN 
DECLARE @idAutomovil NVARCHAR (8)
SELECT @idAutomovil = IdAutomovil FROM Co.Monto
DECLARE @Placa NVARCHAR (8)
SELECT @Placa = Placa FROM Est.Automovil where IdAutomovil = @idAutomovil
DECLARE @TIPO VARCHAR (20) 
SELECT @TIPO = Tipo FROM Est.Automovil WHERE Placa = @Placa
DECLARE @HoraEntrada DATETIME 
SELECT @HoraEntrada = HoraIngreso FROM Co.Monto 
WHERE IdAutomovil = @idAutomovil
DECLARE @HoraSalida DATETIME 
SELECT @HoraSalida = HoraSalida FROM Co.Monto 
DECLARE @TiempoTotal INT 
SELECT @TiempoTotal = TotalHoras FROM Est.Registro

---Creacion de trigger para llenar la tabla de reporte---

INSERT INTO Est.Registro 
VALUES(@Placa, @idAutomovil,  @Tipo, @HoraEntrada, @HoraSalida, DATEDIFF(hh, @HoraEntrada, @HoraSalida),0)
-- Crear la tabla Registro


if(@TiempoTotal) = 1 or (@TiempoTotal)=0 BEGIN
	UPDATE Est.Registro 
	SET Monto = 20 where placaAutomovil = @Placa AND HoraSalida=@HoraSalida
END
if(@TiempoTotal) = 2  BEGIN
	UPDATE Est.Registro
	SET Monto = 30 where placaAutomovil = @Placa AND HoraSalida=@HoraSalida
END
if(@TiempoTotal) = 3 or (@TiempoTotal) =4  BEGIN
	UPDATE Est.Registro
	SET Monto = 70 where placaAutomovil = @Placa AND HoraSalida=@HoraSalida
END
if(@TiempoTotal) >= 4  BEGIN
	UPDATE Est.Registro
	SET Monto = 15*TotalHoras where placaAutomovil = @Placa AND HoraSalida=@HoraSalida
END
if(@Tipo) = 4 or (@Tipo) = 8 or (@Tipo) = 5 BEGIN
	UPDATE Est.Registro
	SET Monto = Monto*2 where placaAutomovil = @Placa AND HoraSalida=@HoraSalida
	END
	
	if(@Tipo) = 6 or (@Tipo) = 9 BEGIN
	UPDATE Est.Registro
	SET Monto = Monto*0.5 where placaAutomovil = @Placa AND HoraSalida=@HoraSalida
	END
end
GO

INSERT INTO Co.Monto VALUES (7,GETDATE(),GETDATE())
go

--select * FROM Est.Automovil
--GO

--SELECT * FROM Est.Tipo
--GO

--SELECT* FROM Co.Monto
--GO

SELECT* FROM Est.Registro
GO
