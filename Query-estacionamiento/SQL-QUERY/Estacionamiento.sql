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
IF NOT EXISTS(SELECT * FROM sys.databases WHERE [name] = 'Estacionamiento')
	BEGIN
		CREATE DATABASE Estacionamiento
	END
GO

DROP DATABASE Estacionamiento
GO 

-- Seleccionar la base de datos recién creada 
USE Estacionamiento
GO

-- Crear el esquema a utilizar
CREATE SCHEMA Est
GO

CREATE SCHEMA Co
GO


/*-- Crear la tabla Placa
CREATE TABLE Est.Placa (
	idplaca INT IDENTITY (1,1) NOT NULL
		CONSTRAINT PK_Placa_id PRIMARY KEY CLUSTERED
       Nombre NVARCHAR (30) NOT NULL
)
GO
*/

-- Crear la tabla automovil
CREATE TABLE Est.Automovil (
	IdAutomovil INT IDENTITY(1,1) NOT NULL
		CONSTRAINT PK_Id_Automovil PRIMARY KEY CLUSTERED,
	Placa NVARCHAR (8) NOT NULL,
    IdTipo INT NOT NULL,
	NumeroTicket INT NOT NULL
)
GO


-- Crear la tabla Tipo
CREATE TABLE Est.Tipo (
	IdPlaca INT IDENTITY (1,1) NOT NULL 
		CONSTRAINT PK_Id_Tipo PRIMARY KEY CLUSTERED,
	Nombretipo NVARCHAR (50) NOT NULL
	--Color NVARCHAR (15) NOT NULL,
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

/*-- Crear la tabla Registro con relacion de las tablas placa, agencia, tipo
CREATE TABLE Est.Registro (
	id INT NOT NULL IDENTITY (1,1)
		CONSTRAINT PK_Registro_id PRIMARY KEY CLUSTERED,
	numeroticket INT NOT NULL,
	IdPlaca INT NOT NULL,
	IdAutomovil INT NOT NULL, 
	IdTipo INT NOT NULL, 
	--fecha DATETIME NOT NULL,
	--hora DATETIME NOT NULL
)
GO
*/


--CREACION DE LAS LLAVES FORANEAS
ALTER TABLE Est.Automovil
	ADD CONSTRAINT
	FK_Un_TipoTiene_VariosAutomoviles
	FOREIGN KEY (IdTipo) REFERENCES Est.Tipo_Automovil(IdTipo)
	ON UPDATE CASCADE
	ON DELETE NO ACTION
GO


ALTER TABLE Co.Monto
	ADD CONSTRAINT
	Fk_Un_AutomovilTiene_Muchos_Montos
	FOREIGN KEY (IdAutomovil) REFERENCES Est.Automovil(IdAutomovil)
	ON UPDATE CASCADE
	ON DELETE NO ACTION
GO

-- Relaciones
/*ALTER TABLE Est.Automovil
	ADD CONSTRAINT
		FK_Un_TipoTiene_Varios_Automoviles
		FOREIGN KEY (IdAutomovil) REFERENCES Est.Automovil(id)
		ON UPDATE CASCADE
		ON DELETE NO ACTION
GO

ALTER TABLE Est.Registro
	ADD CONSTRAINT
		FK_Est_Registro$Pertenece$Est_Id_Automovil
		FOREIGN KEY (idAutomovil) REFERENCES Est.Automovil(id)
		ON UPDATE CASCADE
		ON DELETE NO ACTION
GO

ALTER TABLE Est.Registro
	ADD CONSTRAINT
		FK_Est_Registro$Pertenece$Est_Tipo_id
		FOREIGN KEY (idTipo) REFERENCES Est.Tipo(IdTipo)
		ON UPDATE CASCADE
		ON DELETE NO ACTION
GO

ALTER TABLE Co.Monto
	ADD CONSTRAINT
	Fk_Un_AutomovilTiene_Muchos_Montoss
	FOREIGN KEY (IdAutomovil) REFERENCES Est.Automovil(IdAutomovil)
	ON UPDATE CASCADE
	ON DELETE NO ACTION
GO
*/

	
INSERT INTO Est.Automovil(Placa, IdTipo, numeroticket)
VALUES	('UPN3452', 1 , 01),
		('PBT9834', 2, 02),
		('REN3432', 3, 03),
		('ARJ2122', 4, 04),
		('JRR8998', 5, 05),
		('AJS1234', 6, 06),
		('ESE4565', 7, 07),
		('JPC3443', 8, 08),
		('HTP0991', 1, 09),
		('PBT9403', 2, 10),
		('AAJ3421', 3, 11),
        ('GND5678', 4, 12),
	    ('HND1111', 5, 13),
		('JND2222', 6, 14),
		('JPG0933', 7, 15),
		('LND2344', 8, 16),
		('WER5555', 1, 17),
		('WGD6389', 2, 18),
		('RRD7372', 3, 19),
        ('RRC3456', 4, 20),
		('AAJ0908', 5, 21),
        ('AJD2343', 6, 22),
        ('RCP8786', 7, 23),
        ('ABC5678', 8, 24)
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

INSERT INTO Co.Monto
VALUES
	(1,GETDATE(),GETDATE())
GO


select * FROM Est.Automovil
GO

SELECT * FROM Est.Tipo
GO

SELECT* FROM Co.Monto
GO