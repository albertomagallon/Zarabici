USE [Master]
GO
if exists(select * from sys.databases where name = 'Zarabizi')
	drop database Zarabizi
GO
create database Zarabizi
-- Para dar tiempo a que ejecute el comando que precede
GO	
use [Zarabizi]
GO
-- -$$$-- CREACION DE TABLAS --$$$-
-- Crear tabla Empleado
create table Empleado(
	idEmpleado				int identity(1,1) PRIMARY KEY	NOT NULL,
	DNIEmpleado				NVarChar(10)					NOT NULL,
	nombreEmpleado			NVarChar(15)					NOT NULL,
	apellidosEmpleado		NVarChar(30)					NOT NULL,
	direccionEmpleado		NVarChar(30)					NOT NULL,
	poblacionEmpleado		NVarChar(20)					NOT NULL,
	codigopostalEmpleado	NVarChar(5)						NOT NULL,
	tlfFijoEmpleado			NVarChar(9)						NOT NULL,
	tlfMovilEmpleado		NVarChar(9)							NULL,
	emailEmpleado			NVarChar(50)						NULL,
	--Calcular la antiguedad del empleado Datetime Default
	fechaDeAltaEmpleado		DateTime Default					NULL, 
	fechaNacimientoEmpleado DateTime Default					NULL,
	cuentabancariaEmpleado  NVarchar(16)					 NOT NULL);	
GO
--Crear tabla Bicileta
create table Bicicleta(
	idBicicleta				int identity(1,1) PRIMARY KEY	NOT NULL, 	
	distanciaTotal			Decimal(10,3)					NOT NULL,	
	distanciaMantenimiento	Decimal(10,3)					NOT NULL); 
																		-- Almacena la distancia recorrida hasta el próximo mantenimiento, 
																	   -- una vez que llega por ejemplo cada 100 kilometros el campo se reinicia o 
																	   -- es reinicidado por la tabla RepararBicileta la cual registra una reparacion*/
GO
--Crear tabla intermedia surgida entre Empleado y Bicicleta: EmpleadoBicileta
create table RepararBicicleta(
	idRepararBicicleta			int identity(1,1) PRIMARY KEY  NOT NULL,
	idEmpleado					int								   NULL, 
	idBicicleta					int								   NULL, 
	descripcionRepararBicicleta NVarChar(50)					   NOT NULL,
	fechaRepararBicicleta		DateTime						   NOT NULL);
GO
--Crear tabla Estacion
create table Estacion(
	idEstacion			int identity(1,1) PRIMARY KEY	NOT NULL,
	nombreEstacion		NVarChar(20)					NOT NULL,
	ubicacionEstacion	NVarChar(50)					NOT NULL,
	anclajesEstacion	int								NOT NULL);
GO
--Crear tabla intermedia surgida de la relacion Empleado y Estacion: EmpleadoEstacion
create table RepararEstacion(
	idRepararEstacion			int identity(1,1) PRIMARY KEY NOT NULL,
	idEmpleado					int								  NULL, 
	idEstacion					int								  NULL,
	descripcionRepararEstacion	NVarChar(50)				  NOT NULL,
	fechaRepararEstacion		DateTime					  NOT NULL);
GO
--Crear tabla Anclaje
create table Anclaje(
	idAnclaje			int identity(1,1) PRIMARY KEY NOT NULL,
	--Estado anclaje: ¿Estropeado: 1 o no estropeado: 0?
	estadoAnclaje		Bit			 NOT NULL, 
	--Ocupado: Si/no
	ocupadoAnclaje		Bit			 NOT NULL,
	idEstacion			int			 NOT NULL,
	idBicicleta			int				 NULL);
GO
--Crear tabla Oficina
create table Oficina(
	idOficina		 int identity(1,1) PRIMARY KEY	NOT NULL,
	nombreOficina	 NVarChar(20)					NOT NULL,		
	ubicacionOficina NVarChar(50)					NOT NULL,
	ciudadOficina	 NVarChar(20)					NOT NULL,
	provinciaOficina NVarChar(20)					NOT NULL);
GO
--Crear tabla Tarifa
create table Tarifa(
	idTarifa			int identity(1,1) PRIMARY KEY	NOT NULL,	
	-- días 30, 60, 90
	diasTarifa			int								NOT NULL, 
	nombreTarifa		VARCHAR(50)						NOT NULL,
	fechaInicioTarifa	datetime						NOT NULL,
	fechaFinalTarifa	datetime						NOT NULL,
	precioTarifa		decimal(10,2)					NOT NULL,
	idOficina			int								NOT NULL);
GO
-- Crear tabla bono
create table Bono(
	idBono			int identity(1,1) PRIMARY KEY NOT NULL,
	fechaCompraBono datetime						  NULL, 
	fechaInicioBono	datetime					  NOT NULL,
	fechaFinalBono	datetime					  NOT NULL,
	idSocio			int							  NOT NULL,
	idTarifa		int							  NOT NULL);
GO
-- Crear tabla Socio
create table Socio(
	idSocio				 int identity(1,1) PRIMARY KEY	NOT NULL,
	nombreSocio			 NVarChar(15)					NOT NULL,
	apellidosSocio		 NVarChar(30)					NOT NULL,
	direccionSocio		 NVarChar(30)					NOT NULL,
	poblacionSocio		 NVarChar(20)					NOT NULL,
	codigopostalSocio    NVarChar(5)					NOT NULL,
	tlfFijoSocio		 NVarChar(9)					NOT NULL,
	tlfMovilSocio		 NVarChar(9)					    NULL,
	fechaNacimientoSocio Datetime						NOT NULL,
	fechaAltaSocio		 Datetime						NOT NULL,
	CuentaBancariaSocio	 NVarChar(16)					NOT NULL,
	idOficina			 int							   NULL);	
GO
--Crear tabla Recorrido
create table Recorrido(
	idRecorrido	int identity(1,1) PRIMARY KEY NOT NULL,
	distanciaRecorrido		float			      NULL,	
	fechaSalidaRecorrido	datetime		  NOT NULL,
	fechaLlegadaRecorrido	datetime			  NULL,
	idEstacionInicio        int				  NOT NULL,	
	idEstacionFinal 	    int					  NULL, 	
	idBicicleta				int				      NULL,
	idSocio					int				      NULL);
GO
--Crear tabla Incidencia
create table Incidencia(
	idIncidencia	int identity(1,1) PRIMARY KEY	NOT NULL,
	textoIncidencia NVarChar(50)					NOT NULL,		
	fechaIncidencia  datetime						NOT NULL, 
	idSocio			 int							    NULL);
GO
-- -$$$-- CREACION DE RELACIONES --$$$-


--Relaciones entre Empleado y Bicicleta
ALTER TABLE RepararBicicleta
	Add constraint [fk_RepararBicicleta_idEmpleado] Foreign key ([idEmpleado]) References [Empleado]([idEmpleado])
	On update cascade
	On delete set null;
GO
ALTER TABLE RepararBicicleta
	Add constraint [fk_RepararBicicleta_idBicicleta] Foreign key ([idBicicleta]) References [Bicicleta]([idBicicleta])
	On update cascade
	On delete cascade;
GO
--Relaciones entre Empleado y Estacion
ALTER TABLE RepararEstacion
	Add constraint [fk_RepararEstacion_idEmpleado] Foreign key ([idEmpleado]) References [Empleado]([idEmpleado])
	On update cascade
	On delete set null; -- Conservamos la informacion de las reparaciones aunque el empleado ya no este
GO
ALTER TABLE RepararEstacion
	Add constraint [fk_RepararEstacion_idEstacion] Foreign key ([idEstacion]) References [Estacion]([idEstacion])
	On update cascade
	On delete cascade;  
GO
-- Relacion entre Anclaje y Estacion
ALTER TABLE Anclaje
	Add constraint [fk_Anclaje_idEstacion] Foreign key ([idEstacion]) References [Estacion]([idEstacion])
	On update cascade
	On delete cascade;
GO
-- Relacion entre Recorrido y Estacion
alter table Recorrido
	add constraint [fk_Recorrido_idEstacionInicio] Foreign key (idEstacionInicio) References [Estacion]([idEstacion])	
	on update no action; -- *!
GO
alter table Recorrido
	add constraint [fk_Recorrido_idEstacionFinal] Foreign key (idEstacionFinal) References [Estacion]([idEstacion])	
	on update cascade; -- *!
-- Relacion entre Socio y Recorrido
GO
alter table Recorrido
	add constraint [fk_Recorrido_idSocio] Foreign key (idSocio) References [Socio]([idSocio])	
	on update cascade; 
GO
-- Relacion entre Recorrido y Bicicleta
alter table Recorrido
	add constraint [fk_Recorrido_idBicicleta] Foreign key (idBicicleta) References [Bicicleta]([idBicicleta])
	on update cascade
	on delete set null; -- *! Conservamos la informacion del recorrido aunque la bicicleta ya no este para realizar estadísticas.
-- Relacion entre Bono y Socio
alter table Bono add constraint [fk_Bono_idSocio] Foreign key ([idSocio]) References [Socio]([idSocio])
	on update cascade
	on delete cascade;
GO
-- Relacion entre Bono y Tarifa
alter table Bono add constraint [fk_Bono_idTarifa] Foreign key ([idTarifa]) References [Tarifa]([idTarifa])
	on update cascade;
GO
-- Relacion entre Incidencia y Socio
alter table Incidencia add constraint [fk_Incidencia_idSocio] Foreign key([idSocio]) References [Socio]([idSocio])
	on update cascade
	on delete set null; -- Conservamos la informacion de la incidencia aunque el socio ya no este para realizar estadísticas sobre problemas frecuetnes
GO
-- Relacion entre Socio y Oficina
alter table Socio add constraint [fk_Socio_idOficina] Foreign key ([idOficina]) References [Oficina]([idOficina])
	on update cascade; -- *!
-- Relacion entre Tarifa y Oficina
alter table Tarifa add constraint [fk_Tarifa_idOficina] Foreign key ([idOficina]) References [Oficina]([idOficina])
	on update no action -- *!
	on delete no action;
-- Relacion entre Anclaje y Bicicleta
alter table Anclaje
	add constraint [fk_anclaje_idBicicleta] Foreign key([idBicicleta]) References [Bicicleta]([idBicicleta])
	on update cascade
	on delete set null; -- *!

-- -$$$-- INSERCCIONES DE DATOS --$$$-	
-- -- Tabla Empleado
insert into Empleado (DNIEmpleado, nombreEmpleado, apellidosEmpleado, direccionEmpleado, poblacionEmpleado, codigopostalEmpleado, tlfFijoEmpleado, tlfMovilEmpleado, emailEmpleado, fechaDeAltaEmpleado, fechaNacimientoEmpleado, cuentabancariaEmpleado)
values ('73091222T','Alberto','Simón Trallero','C\ Gaspar Sanz 3','Zaragoza', '50700','978123547', '675123548','albertosimon@gmail.com','1/2/2003','15/9/1986','3622399815023639');
insert into Empleado (DNIEmpleado, nombreEmpleado, apellidosEmpleado, direccionEmpleado, poblacionEmpleado, codigopostalEmpleado, tlfFijoEmpleado, tlfMovilEmpleado, emailEmpleado, fechaDeAltaEmpleado, fechaNacimientoEmpleado, cuentabancariaEmpleado)
values ('77576333J','Alberto','Magallón Sábado','C\ Mayor 34','Zuera', '60420','978246249', '675246249','albertomagallon@gmail.com','1/7/1995','21/3/1986','9439239391943923');
insert into Empleado (DNIEmpleado, nombreEmpleado, apellidosEmpleado, direccionEmpleado, poblacionEmpleado, codigopostalEmpleado, tlfFijoEmpleado, tlfMovilEmpleado, emailEmpleado, fechaDeAltaEmpleado, fechaNacimientoEmpleado, cuentabancariaEmpleado)
values ('75631444K','Amelia','Benito Bondía','C\ Esconjurador','Zaragoza', '50600','978124958', '675124958','ameliabenito@gmail.com','1/1/2007','30/1/1991','8733135623873313');
insert into Empleado (DNIEmpleado, nombreEmpleado, apellidosEmpleado, direccionEmpleado, poblacionEmpleado, codigopostalEmpleado, tlfFijoEmpleado, tlfMovilEmpleado, emailEmpleado, fechaDeAltaEmpleado, fechaNacimientoEmpleado, cuentabancariaEmpleado)
values ('73192555T','Celia','Hernandez Grao','Avd. América 23 2ºD','Zaragoza', '50700','978123661', '675123661','celiahernandez@gmail.com','15/9/2008','24/6/1991','5424429201542442');
insert into Empleado (DNIEmpleado, nombreEmpleado, apellidosEmpleado, direccionEmpleado, poblacionEmpleado, codigopostalEmpleado, tlfFijoEmpleado, tlfMovilEmpleado, emailEmpleado, fechaDeAltaEmpleado, fechaNacimientoEmpleado, cuentabancariaEmpleado)
values ('73121666T','Carlos','Cueto Prades','Paseo Calanda 12 6ºC','Zaragoza', '50700','978341123', '675341123','carloscueto@gmail.com','14/9/2010','17/8/1985','9711371222971137');

-- -- Tabla Bicicleta
insert into Bicicleta (distanciaTotal, distanciaMantenimiento) 
values (145.23,45.23);
-- 2
insert into Bicicleta (distanciaTotal, distanciaMantenimiento) 
values (77.56,77.56);
-- 3
insert into Bicicleta (distanciaTotal, distanciaMantenimiento) 
values (231.14,70.14);
-- 4
insert into Bicicleta (distanciaTotal, distanciaMantenimiento) 
values (88.0,0.9);
-- 5
insert into Bicicleta (distanciaTotal, distanciaMantenimiento) 
values (334.25,56.9);
-- 6
insert into Bicicleta (distanciaTotal, distanciaMantenimiento) 
values (423.34,93.9);
-- 7
insert into Bicicleta (distanciaTotal, distanciaMantenimiento) 
values (67.84,10.9);

-- -- Tabla RepararBicicleta
insert into RepararBicicleta (idEmpleado, idBicicleta, descripcionRepararBicicleta, fechaRepararBicicleta)
values (1,1,'Mantenimiento rutinario','16/8/2005');
insert into RepararBicicleta (idEmpleado, idBicicleta, descripcionRepararBicicleta, fechaRepararBicicleta)
values (1,3,'Mantenimiento rutinario','19/8/2005');
insert into RepararBicicleta (idEmpleado, idBicicleta, descripcionRepararBicicleta, fechaRepararBicicleta)
values (2,3,'Manillar partido','5/10/2005');
insert into RepararBicicleta (idEmpleado, idBicicleta, descripcionRepararBicicleta, fechaRepararBicicleta)
values (3,4,'Chasis doblado','13/11/2005');
insert into RepararBicicleta (idEmpleado, idBicicleta, descripcionRepararBicicleta, fechaRepararBicicleta)
values (1,5,'Mantenimiento rutinario','20/11/2005');
insert into RepararBicicleta (idEmpleado, idBicicleta, descripcionRepararBicicleta, fechaRepararBicicleta)
values (1,5,'Mantenimiento rutinario','23/12/2005');
insert into RepararBicicleta (idEmpleado, idBicicleta, descripcionRepararBicicleta, fechaRepararBicicleta)
values (2,5,'Pinchazo','24/2/2006');
insert into RepararBicicleta (idEmpleado, idBicicleta, descripcionRepararBicicleta, fechaRepararBicicleta)
values (1,6,'Mantenimiento rutinario','27/2/2006');
insert into RepararBicicleta (idEmpleado, idBicicleta, descripcionRepararBicicleta, fechaRepararBicicleta)
values (4,6,'Mantenimiento rutinario','10/3/2006');
insert into RepararBicicleta (idEmpleado, idBicicleta, descripcionRepararBicicleta, fechaRepararBicicleta)
values (1,6,'Mantenimiento rutinario','12/4/2006');
insert into RepararBicicleta (idEmpleado, idBicicleta, descripcionRepararBicicleta, fechaRepararBicicleta)
values (2,6,'Pinchazo','16/3/2009');
insert into RepararBicicleta (idEmpleado, idBicicleta, descripcionRepararBicicleta, fechaRepararBicicleta)
values (5,7,'Pinchazo','16/5/2009');

-- -- Tabla Estacion
insert into Estacion (nombreEstacion, ubicacionEstacion, anclajesEstacion)
values ('Parque Delicias','C\ Universitas 25', 15)
insert into Estacion (nombreEstacion, ubicacionEstacion, anclajesEstacion)
values ('Independencia I','Paseo Independencia 1', 15)
insert into Estacion (nombreEstacion, ubicacionEstacion, anclajesEstacion)
values ('Independencia II','Paseo Independencia 53', 15)
insert into Estacion (nombreEstacion, ubicacionEstacion, anclajesEstacion)
values ('Plaza San Francisco','Plza. San Francisco 3', 30)
insert into Estacion (nombreEstacion, ubicacionEstacion, anclajesEstacion)
values ('Actur','Avda. Pintor Ruiz Picasso 29', 30)

-- -- Tabla RepararEstacion
insert into RepararEstacion (idEmpleado, idEstacion, descripcionRepararEstacion, fechaRepararEstacion)
values (2,1, 'Daños de autobús urbano', '12/2/2002')
insert into RepararEstacion (idEmpleado, idEstacion, descripcionRepararEstacion, fechaRepararEstacion)
values (1,1, 'Daños de autobús urbano', '19/2/2002')
insert into RepararEstacion (idEmpleado, idEstacion, descripcionRepararEstacion, fechaRepararEstacion)
values (1,1, 'Han quemado el TPV', '23/2/2002')
insert into RepararEstacion (idEmpleado, idEstacion, descripcionRepararEstacion, fechaRepararEstacion)
values (3,2, 'Daños de autobús urbano', '13/3/2005')
insert into RepararEstacion (idEmpleado, idEstacion, descripcionRepararEstacion, fechaRepararEstacion)
values (2,2, 'Daños de autobús urbano', '18/3/2005')
insert into RepararEstacion (idEmpleado, idEstacion, descripcionRepararEstacion, fechaRepararEstacion)
values (5,3, 'Anclajes doblados', '19/1/2008')

-- -- Tabla Anclaje
insert into Anclaje (estadoAnclaje, ocupadoAnclaje, idEstacion, idBicicleta)
values (0, 1, 1, 3)																																								
insert into Anclaje (estadoAnclaje, ocupadoAnclaje, idEstacion)
values (0, 0, 1)
insert into Anclaje (estadoAnclaje, ocupadoAnclaje, idEstacion)
values (0, 0, 1)
insert into Anclaje (estadoAnclaje, ocupadoAnclaje, idEstacion)
values (1, 0, 1)
insert into Anclaje (estadoAnclaje, ocupadoAnclaje, idEstacion)
values (0, 0, 1)
insert into Anclaje (estadoAnclaje, ocupadoAnclaje, idEstacion, idBicicleta)
values (0, 1, 2, 1)
insert into Anclaje (estadoAnclaje, ocupadoAnclaje, idEstacion)
values (0, 0, 2)
insert into Anclaje (estadoAnclaje, ocupadoAnclaje, idEstacion)
values (0, 0, 2)
insert into Anclaje (estadoAnclaje, ocupadoAnclaje, idEstacion)
values (1, 0, 2)
insert into Anclaje (estadoAnclaje, ocupadoAnclaje, idEstacion, idBicicleta)
values (0, 1, 2, 5)
insert into Anclaje (estadoAnclaje, ocupadoAnclaje, idEstacion, idBicicleta)
values (0, 1, 3, 6)
insert into Anclaje (estadoAnclaje, ocupadoAnclaje, idEstacion, idBicicleta)
values (0, 1, 3, 2)
insert into Anclaje (estadoAnclaje, ocupadoAnclaje, idEstacion)
values (0, 0, 3)
insert into Anclaje (estadoAnclaje, ocupadoAnclaje, idEstacion)
values (0, 0, 3)
insert into Anclaje (estadoAnclaje, ocupadoAnclaje, idEstacion)
values (1, 0, 3)
insert into Anclaje (estadoAnclaje, ocupadoAnclaje, idEstacion, idBicicleta)
values (0, 1, 4, 4)
insert into Anclaje (estadoAnclaje, ocupadoAnclaje, idEstacion)
values (0, 0, 4)
insert into Anclaje (estadoAnclaje, ocupadoAnclaje, idEstacion)
values (0, 0, 4)
insert into Anclaje (estadoAnclaje, ocupadoAnclaje, idEstacion)
values (1, 0, 4)
insert into Anclaje (estadoAnclaje, ocupadoAnclaje, idEstacion)
values (1, 0, 4)
insert into Anclaje (estadoAnclaje, ocupadoAnclaje, idEstacion, idBicicleta)
values (0, 1, 4, 7)
insert into Anclaje (estadoAnclaje, ocupadoAnclaje, idEstacion)
values (1, 0, 5)
insert into Anclaje (estadoAnclaje, ocupadoAnclaje, idEstacion)
values (1, 0, 5)
insert into Anclaje (estadoAnclaje, ocupadoAnclaje, idEstacion)
values (0, 0, 5)

-- -- Tabla Oficina
insert into Oficina (nombreOficina, ubicacionOficina, ciudadOficina, provinciaOficina)
values ('Independencia', 'Paseo Independencia Nº23', 'Zaragoza','Zaragoza')
insert into Oficina (nombreOficina, ubicacionOficina, ciudadOficina, provinciaOficina)
values ('El retiro', 'Avda. América Nº102', 'Madrid','Madrid')
insert into Oficina (nombreOficina, ubicacionOficina, ciudadOficina, provinciaOficina)
values ('Las Ramblas', 'Avda. Las Ramblas Nº142', 'Barcelona','Barcelona')
GO
-- -- Tabla Tarifa
insert into Tarifa (diasTarifa, nombreTarifa, fechaInicioTarifa, fechaFinalTarifa, precioTarifa, idOficina)
values (30, 'Joven', '1/1/2010', '31/12/2010', 24.95, 1)
insert into Tarifa (diasTarifa, nombreTarifa, fechaInicioTarifa, fechaFinalTarifa, precioTarifa, idOficina)
values (30, 'Jubilado', '1/1/2010', '31/12/2010', 19.95, 1)
insert into Tarifa (diasTarifa, nombreTarifa, fechaInicioTarifa, fechaFinalTarifa, precioTarifa, idOficina)
values (30, 'Normal', '1/6/2010', '31/12/2010', 49.95, 1)
insert into Tarifa (diasTarifa, nombreTarifa, fechaInicioTarifa, fechaFinalTarifa, precioTarifa, idOficina)
values (90, 'Normal Plus', '1/1/2010', '31/12/2010', 99.95, 1)
insert into Tarifa (diasTarifa, nombreTarifa, fechaInicioTarifa, fechaFinalTarifa, precioTarifa, idOficina)
values (60, 'Normal Intermediate', '1/1/2011', '31/12/2011', 79.95, 1)
insert into Tarifa (diasTarifa, nombreTarifa, fechaInicioTarifa, fechaFinalTarifa, precioTarifa, idOficina)
values (30, 'Joven', '1/1/2010', '31/12/2010', 34.95, 2)
insert into Tarifa (diasTarifa, nombreTarifa, fechaInicioTarifa, fechaFinalTarifa, precioTarifa, idOficina)
values (30, 'Jubilado', '1/1/2010', '31/12/2010', 29.95, 2)
insert into Tarifa (diasTarifa, nombreTarifa, fechaInicioTarifa, fechaFinalTarifa, precioTarifa, idOficina)
values (30, 'Normal', '1/1/2011', '31/12/2011', 59.95, 2)
insert into Tarifa (diasTarifa, nombreTarifa, fechaInicioTarifa, fechaFinalTarifa, precioTarifa, idOficina)
values (90, 'Normal Plus', '1/1/2010', '31/12/2010', 109.95, 2)
insert into Tarifa (diasTarifa, nombreTarifa, fechaInicioTarifa, fechaFinalTarifa, precioTarifa, idOficina)
values (60, 'Normal Intermediate', '1/1/2010', '31/12/2010', 89.95, 2)
insert into Tarifa (diasTarifa, nombreTarifa, fechaInicioTarifa, fechaFinalTarifa, precioTarifa, idOficina)
values (30, 'Indigente', '1/1/2011', '31/12/2011', 34.95, 3)
insert into Tarifa (diasTarifa, nombreTarifa, fechaInicioTarifa, fechaFinalTarifa, precioTarifa, idOficina)
values (30, 'Jubilado', '1/1/2010', '31/12/2010', 29.95, 3)
insert into Tarifa (diasTarifa, nombreTarifa, fechaInicioTarifa, fechaFinalTarifa, precioTarifa, idOficina)
values (30, 'Normal', '1/1/2010', '31/12/2010', 59.95, 3)
insert into Tarifa (diasTarifa, nombreTarifa, fechaInicioTarifa, fechaFinalTarifa, precioTarifa, idOficina)
values (90, 'Normal Plus', '1/1/2010', '31/12/2010', 109.95, 3)
insert into Tarifa (diasTarifa, nombreTarifa, fechaInicioTarifa, fechaFinalTarifa, precioTarifa, idOficina)
values (60, 'Normal Intermediate', '1/1/2010', '31/12/2010', 89.95, 3)
GO
-- -- Tabla Socio
insert into Socio (nombreSocio, apellidosSocio, direccionSocio, poblacionSocio, codigopostalSocio, tlfFijoSocio, tlfMovilSocio, fechaNacimientoSocio, fechaAltaSocio, CuentaBancariaSocio, idOficina)
values ('Jorge', 'Colomer Garcia', 'C\ San Jorge Nº3 1ºA', 'Calanda', 44570, 978887001, 666888001,'30/12/1987','23/1/2000','1234567891234567',1)
insert into Socio (nombreSocio, apellidosSocio, direccionSocio, poblacionSocio, codigopostalSocio, tlfFijoSocio, tlfMovilSocio, fechaNacimientoSocio, fechaAltaSocio,CuentaBancariaSocio, idOficina)
values ('Ignacio', 'Ariño Borraz', 'C\ Gaspar Sanz Nº1 5ºC', 'Zaragoza', 57000, 979997002, 666741301,'15/2/1986','13/6/1999','1234567891234567',1)
insert into Socio (nombreSocio, apellidosSocio, direccionSocio, poblacionSocio, codigopostalSocio, tlfFijoSocio, tlfMovilSocio, fechaNacimientoSocio,fechaAltaSocio, CuentaBancariaSocio, idOficina)
values ('David', 'Asensio Val', 'C\ Mayor Nº5 1ºC', 'Zaragoza', 57000, 979996001, 676888101,'17/4/1984','26/4/2001','1234567891234567',1)
insert into Socio (nombreSocio, apellidosSocio, direccionSocio, poblacionSocio, codigopostalSocio, tlfFijoSocio, tlfMovilSocio, fechaNacimientoSocio,fechaAltaSocio, CuentaBancariaSocio, idOficina)
values ('Javier', 'Martinez Borruel', 'C\ Caspe Nº35 6ºC', 'Zaragoza', 57000, 979995103, 676183151,'3/7/1987','12/9/2001','1234567891234567',2)
insert into Socio (nombreSocio, apellidosSocio, direccionSocio, poblacionSocio, codigopostalSocio, tlfFijoSocio, tlfMovilSocio, fechaNacimientoSocio,fechaAltaSocio, CuentaBancariaSocio, idOficina)
values ('Manuel', 'Piro Rito', 'C\ Ramon y Cajal Nº7 3ºC', 'Zaragoza', 57000, 979935143, 626187151,'5/10/1987','7/12/2001','1234567891234567',2)
insert into Socio (nombreSocio, apellidosSocio, direccionSocio, poblacionSocio, codigopostalSocio, tlfFijoSocio, tlfMovilSocio, fechaNacimientoSocio,fechaAltaSocio, CuentaBancariaSocio, idOficina)
values ('Joaquin', 'Moreno Abadía', 'C\ Mayor Nº10 3ºB', 'Zaragoza', 57000, 979395203, 621185101,'10/11/1986','29/12/2000','1234567891234567',2)
insert into Socio (nombreSocio, apellidosSocio, direccionSocio, poblacionSocio, codigopostalSocio, tlfFijoSocio, tlfMovilSocio, fechaNacimientoSocio,fechaAltaSocio, CuentaBancariaSocio, idOficina)
values ('Raquel', 'Palomo Gracia', 'C\ Cristobal Colon Nº1', 'Zaragoza', 57000, 979315203, 621185101,'19/3/1986','16/11/1999','1234567891234567',3)
insert into Socio (nombreSocio, apellidosSocio, direccionSocio, poblacionSocio, codigopostalSocio, tlfFijoSocio, tlfMovilSocio, fechaNacimientoSocio,fechaAltaSocio, CuentaBancariaSocio, idOficina)
values ('Alba', 'Gimeno Royo', 'C\ Pintor Blasco Nº3', 'Zaragoza', 57000, 978314213, 620175101,'15/5/1986','15/6/2002','1234567891234567',3)
GO
-- -- Tabla Bono
insert into Bono (fechaCompraBono, fechaInicioBono, fechaFinalBono, idSocio, idTarifa)
values('1/2/2010','1/2/2010','1/2/2010',1,1)
insert into Bono (fechaCompraBono, fechaInicioBono, fechaFinalBono, idSocio, idTarifa)
values('1/2/2010','1/2/2010','1/2/2010',2,1)
insert into Bono (fechaCompraBono, fechaInicioBono, fechaFinalBono, idSocio, idTarifa)
values('5/2/2010','5/2/2010','5/2/2010',3,1)
insert into Bono (fechaCompraBono, fechaInicioBono, fechaFinalBono, idSocio, idTarifa)
values('17/2/2010','17/2/2010','17/2/2010',4,2)
insert into Bono (fechaCompraBono, fechaInicioBono, fechaFinalBono, idSocio, idTarifa)
values('1/5/2010','1/5/2010','1/5/2010',5,3)
insert into Bono (fechaCompraBono, fechaInicioBono, fechaFinalBono, idSocio, idTarifa)
values('13/6/2010','13/6/2010','13/6/2010',6,4)
insert into Bono (fechaCompraBono, fechaInicioBono, fechaFinalBono, idSocio, idTarifa)
values('1/7/2010','1/7/2010','1/7/2010',7,5)
insert into Bono (fechaCompraBono, fechaInicioBono, fechaFinalBono, idSocio, idTarifa)
values('1/7/2010','1/12/2010','20/1/2011',8,6)
GO
-- -- Tabla Recorrido
insert into Recorrido (distanciaRecorrido, fechaSalidaRecorrido, fechaLlegadaRecorrido, idEstacionInicio,
	idEstacionFinal, idBicicleta, idSocio)
values(1.3, '3/5/2010 7:30:00','3/5/2010 7:37:10',1,2,1,1)		
insert into Recorrido (distanciaRecorrido, fechaSalidaRecorrido, fechaLlegadaRecorrido, idEstacionInicio,
	idEstacionFinal, idBicicleta, idSocio)
values(3.5, '3/5/2010 13:10:10','3/5/2010 13:47:10',1,3,2,3)		
insert into Recorrido (distanciaRecorrido, fechaSalidaRecorrido, fechaLlegadaRecorrido, idEstacionInicio,
	idEstacionFinal, idBicicleta, idSocio)
values(2.7, '10/6/2010 17:20:04','10/6/2010 17:50:10',2,1,3,5)		
insert into Recorrido (distanciaRecorrido, fechaSalidaRecorrido, fechaLlegadaRecorrido, idEstacionInicio,
	idEstacionFinal, idBicicleta, idSocio)
values(2.5, '10/6/2010 18:12:29','10/6/2010 19:00:10',2,4,4,4)		
insert into Recorrido (distanciaRecorrido, fechaSalidaRecorrido, fechaLlegadaRecorrido, idEstacionInicio,
	idEstacionFinal, idBicicleta, idSocio)
values(1.1, '10/6/2010 20:02:20','10/6/2010 20:10:58',3,2,5,6)		
insert into Recorrido (distanciaRecorrido, fechaSalidaRecorrido, fechaLlegadaRecorrido, idEstacionInicio,
	idEstacionFinal, idBicicleta, idSocio)
values(1.1, '10/6/2010 20:02:20','10/6/2010 20:10:58',4,3,6,7)	
insert into Recorrido (distanciaRecorrido, fechaSalidaRecorrido, fechaLlegadaRecorrido, idEstacionInicio,
	idEstacionFinal, idBicicleta, idSocio)
values(1.1, '10/6/2010 20:02:20','10/6/2010 20:10:58',5,4,7,7)	
GO
-- -- Incidencia
insert into Incidencia (textoIncidencia, fechaIncidencia, idSocio)
	values('La bici 4 en el anclaje 1 no existe','12/10/2010',1)
insert into Incidencia (textoIncidencia, fechaIncidencia, idSocio)
	values('Me dice que vaya al anclaje -100','3/11/2010',2)
insert into Incidencia (textoIncidencia, fechaIncidencia, idSocio)
	values('Dice que coja la bici 5 pero no está','14/11/2010',3)	
insert into Incidencia (textoIncidencia, fechaIncidencia, idSocio)
	values('Esta ardiendo el contenedor de enfrente','27/11/2010',4)	
GO

-- -$$$-- CONSULTAS --$$$-	

/*¿Cuantos km ha hecho una bicicleta pasada por parámetro?*/
Declare @idBici int;
set @idBici = 1;
SELECT distanciaTotal as "Distancia Total" FROM Bicicleta WHERE idBicicleta=@idBici;
GO

/*Cantidad facturada (real) en un año. Parámetro de una sucursal y el año*/
Declare @idOficina int, @anno int;
set @idOficina = 1
set @anno = 2010;
SELECT SUM(Tarifa.precioTarifa) as Euros FROM Bono INNER JOIN Tarifa ON Bono.idTarifa = Tarifa.idTarifa
WHERE Tarifa.idOficina = @idOficina and
		YEAR(fechaCompraBono) = @anno
GO
/*Bonos a punto de caducarse (15 días)*/
SELECT idSocio, max(fechaInicioBono) as "Inicio del bono", max(fechaFinalBono)as "Final del bono"
FROM bono
GROUP BY idSocio
Having (max(fechaFinalBono) between 
		SYSDATETIME() and dateadd(d,15,SYSDATETIME()))
GO
/*Dada una parada cuantas bicis hay*/
DECLARE @idEstacion int
SET @idEstacion=2
SELECT COUNT(idAnclaje) as Bicicletas FROM Estacion INNER JOIN Anclaje ON estacion.idEstacion = anclaje.idEstacion
WHERE Estacion.idEstacion = @idEstacion and anclaje.idBicicleta is not null
GO
/*Dada una parada cuantos anclajes libre hay*/
Declare @idEstacion int
SET @idEstacion = 1
SELECT COUNT(idAnclaje) as Anclajes FROM Estacion INNER JOIN Anclaje ON estacion.idEstacion = anclaje.idEstacion
WHERE Estacion.idEstacion = @idEstacion and ocupadoAnclaje = 0 and estadoAnclaje = 0
GO
/*Top 10 de los recorridos mas usados*/
SELECT  TOP(10) idEstacionInicio, idEstacionFinal,  COUNT(CAST(idEstacionInicio AS CHAR)+ CAST(idEstacionFinal AS CHAR)) as Recorridos  
FROM Recorrido
GROUP BY idEstacionInicio, idEstacionFinal
ORDER BY Recorridos desc
GO
/*Top 10 de las paradas más utilizadas*/
SELECT TOP(10) idEstacion, COUNT(idEstacion) as Frecuencia 
FROM Estacion inner join Recorrido
on idEstacion=idEstacionInicio or idEstacion=idEstacionFinal
GROUP by idEstacion
ORDER by COUNT(idEstacion) desc
GO
/*Top 10 de las paradas menos utilizadas*/
SELECT TOP(10) idEstacion, COUNT(idEstacion) as Frecuencia 
FROM Estacion inner join Recorrido
on idEstacion=idEstacionInicio or idEstacion=idEstacionFinal
GROUP by idEstacion
ORDER by COUNT(idEstacion) asc
GO

-- -$$$-- PROCEDIMIENTOS ALMACENADOS --$$$-	

-- -$$- Procedimiento almacenado: Iniciar recorrido -$$-
/* 
   Este procedimiento almacenado recupera un identificador de bicicleta(idBicicleta)
   de la tabla Anclaje de un registro que tenga un anclaje sin estropear(estadoAnclaje),
   que esté ocupado(ocupadoAnclaje) y que pertenezca a la estacion pasada por
   parámetro @estacion(idEstacion).  Luego actualiza dicho registro en la tabla
   desocupando el anclaje(ocupadoAnclaje) y dejando nulo el campo idBicicleta.
   A continuación inserta en la tabla Recorrido un nuevo registro con el idEstacionInicio
   que corresponde al parametro @estacion(idEstacion), la fecha actual del sistema, el 
   identificador de bicicleta recupearado anteriormente y el usuario pasado como
   parámetro @usuario(idSocio).  Por último devuelve el identificador de bicicleta recuperado.

*/
CREATE PROCEDURE cp_iniciarRecorrido(@usuario int, @estacion int)
AS
BEGIN TRY
	BEGIN TRAN
		-- Busca idAnclaje
		DECLARE @idBicicleta int
							--El anclaje no debe de estar estropeado y debe de estar
							-- ocupado por una bici
		SET @idBicicleta = (SELECT TOP(1) idBicicleta FROM Anclaje 
							WHERE estadoAnclaje = 0 and ocupadoAnclaje = 1 and
								  idEstacion = @estacion)  							  
		-- Encuentra un anclaje libre
		IF (@idBicicleta>0)
		BEGIN
			-- Actualiza tabla anclaje
			UPDATE Anclaje
			SET idBicicleta = NULL, ocupadoAnclaje=0
			WHERE idBicicleta = @idBicicleta	
		END
		ELSE
		BEGIN
			-- Excepcion
			  RAISERROR ('Error idBicicleta.', -- Message text.
				   16, -- Severity.
				   1 -- State.
				   );

		END
		-- Crea registro en la tabla Recorrido	
		insert into Recorrido (idestacionInicio, fechaSalidaRecorrido, idBicicleta, idSocio)
		values (@Estacion, SYSDATETIME(),@idBicicleta, @usuario);
		-- Devuelve el id_bici
		--PRINT @idbicicleta 
	COMMIT TRAN
	RETURN @idbicicleta	
END TRY
BEGIN CATCH
 ROLLBACK TRAN
 DECLARE @ErrNum int, @ErrMsg NVARCHAR(4000);
 SET @ErrNum = (SELECT ERROR_NUMBER() AS ErrorNumber);
 SET @ErrMsg = (SELECT ERROR_MESSAGE() AS ErrorMessage); 
 SET @idBicicleta=0
 PRINT @ErrNum 
 PRINT @ErrMsg 
 RETURN @idBicicleta
END CATCH
GO

-- -$$- Procedimiento almacenado: Finalizar recorrido -$$-
/* 
   Este procedimiento almacenado recupera el idEstacion de un registro 
   en la tabla Anclaje dado el parámetro @anclaje(idAnclaje).  Luego 
   actualiza la tabla indicando que en dicho registro el anclaje se 
   encuentra ocupado por la bicicleta pasada por el parámetro @idbici(idBicicleta).
   A continuación, se actualiza el idEstacionFinal con el idEstacion recuperado
   anteriormente de la tabla Recorrido donde su registro corresponda con el
   idBicicleta pasado por parámetro @idbici(idBicicleta) y su idEstacionFinal
   sea nulo (el recorrido no se haya completado)   
*/
CREATE PROCEDURE cp_finalizarRecorrido(@idbici int, @anclaje int)
AS
BEGIN TRY
	BEGIN TRAN
	-- 1º Saber que estacion es con el anclaje
		DECLARE @idEstacion int
		SET @idEstacion = (SELECT idEstacion FROM Anclaje 
							WHERE idAnclaje = @anclaje)  
		-- Encuentra la estacion
		IF (@idEstacion>0)
		BEGIN
			-- Actualiza tabla anclaje
			UPDATE Anclaje
			SET ocupadoAnclaje = 1, idBicicleta = @idbici
			WHERE idAnclaje = @anclaje						
		END
		ELSE
		BEGIN
			-- Excepcion
			  RAISERROR ('Error idEstacion.', -- Message text.
				   16, -- Severity.
				   1 -- State.
				   );

		END
	-- 2º Actualizar en la tabla recorrido el idEstacionfinal con el idBici
		-- Actualiza tabla recorrido
		UPDATE Recorrido 
		SET idEstacionFinal = @idEstacion
		WHERE idBicicleta = @idbici and idEstacionFinal is null
	COMMIT
END TRY
BEGIN CATCH
 ROLLBACK TRAN
 DECLARE @ErrNum int, @ErrMsg NVARCHAR(4000);
 SET @ErrNum = (SELECT ERROR_NUMBER() AS ErrorNumber);
 SET @ErrMsg = (SELECT ERROR_MESSAGE() AS ErrorMessage);
 PRINT @ErrNum 
 PRINT @ErrMsg  + ' CP Finaliza idBicicleta: ' + convert(NVARCHAR(100), @idbici) + 'idAnclaje: ' + convert(NVARCHAR(100),  @anclaje)
END CATCH
GO

-- -$$- Procedimiento almacenado: Insertar Datos -$$-
/* 
	Este procedimiento almacenado inserta datos aleatorios en las tablas de Socios,
	Bicicletas y Reparaciones de bicicletas, y recorridos.   
*/
CREATE PROCEDURE cp_insertarDatos(@numSocios int, @numBicis int, @numRecorridos int) AS
BEGIN
-- Numeros aleatorios
DECLARE @Semilla float
DECLARE @Entero int
DECLARE @MaxValue int
DECLARE @MinValue int
-- Tabla Socio
DECLARE @NumeroDNI Nvarchar(9)
DECLARE @LetraDNI char
DECLARE @Nombre Nvarchar(20)
DECLARE @Apellido Nvarchar(50)
DECLARE @Direccion Nvarchar(50)
DECLARE @DireccionNum NVarChar(2)
DECLARE @Telefono NVarchar(8)
DECLARE @Movil NVarchar(8)
DECLARE @Ciudad NVarchar(20)
DECLARE @CP Nvarchar(5)
DECLARE @dia Nvarchar(2)
DECLARE @mes Nvarchar(2)
DECLARE @anno Nvarchar(4)
DECLARE @FechaNacimiento NVarchar(10)
DECLARE @FechaAltaSocio NVarchar(10)
DECLARE @Banco1 NVarchar(8)
DECLARE @Banco2 NVarchar(8)
DECLARE @Banco4 NVarchar(16)
DECLARE @idOficina NVarchar(1)

-- Tabla Bicicleta y RepararBicicleta
DECLARE @kmsTotal int
DECLARE @kmsReparacion float
DECLARE @NumReparaciones int
DECLARE @IdBici int
DECLARE @IdEmpleado int

-- Tabla Recorridos
DECLARE @Socio int
DECLARE @Estacion int
DECLARE @EstadoAnclaje bit
DECLARE @Anclaje int

-- Recorrer bucles
DECLARE @Contador int
DECLARE @Contador2 int

SET @MaxValue = 9
SET @MinValue = 1

IF (@numSocios<0 and @numSocios>10000)
BEGIN
	PRINT '[-] El valor de número de socios debe de estar comprendido entre 0 y 100000 - ' + @numSocios 
	RETURN
END
IF (@numBicis<0 and @numBicis>10000)
BEGIN
	PRINT '[-] El valor de número de bicis debe de estar comprendido entre 0 y 100000 - ' + @numBicis  
	RETURN
END
IF (@numRecorridos<0 and @numRecorridos>10000)
BEGIN
	PRINT '[-] El valor de número de bicis debe de estar comprendido entre 0 y 100000 - ' + @numRecorridos  
	RETURN
END

   
    -- Variables: Del 1 al 15 nombres, del 16 al 40 son apellidos, del 41 al 45 letras DNI
    -- del 46 al 50 ciudades, del 51 al 75 Calles
    DECLARE @Variables XML;
    SET @Variables = '
	    <ROOT>
		    <Customer pos="1" id="Alberto"></Customer>
		    <Customer pos="2" id="Jorge"></Customer>
		    <Customer pos="3" id="Javier"></Customer>
		    <Customer pos="4" id="Juan"></Customer>
		    <Customer pos="5" id="Alfredo"></Customer>
		    <Customer pos="6" id="Pedro"></Customer>
		    <Customer pos="7" id="Amelia"></Customer>
		    <Customer pos="8" id="Casandra"></Customer>
		    <Customer pos="9" id="Celia"></Customer>
		    <Customer pos="10" id="Ana"></Customer>
		    <Customer pos="11" id="David"></Customer>
		    <Customer pos="12" id="Sandra"></Customer>
		    <Customer pos="13" id="Eduardo"></Customer>
		    <Customer pos="14" id="Victor"></Customer>
		    <Customer pos="15" id="Pablo"></Customer>
		    
		    <Customer pos="16" id="Magallon"></Customer>
		    <Customer pos="17" id="Sabado"></Customer>
		    <Customer pos="18" id="Colomer"></Customer>
		    <Customer pos="19" id="Garcia"></Customer>
		    <Customer pos="20" id="Benito"></Customer>
		    <Customer pos="21" id="Bondia"></Customer>
		    <Customer pos="22" id="Asensio"></Customer>
		    <Customer pos="23" id="Lahoz"></Customer>
		    <Customer pos="24" id="Martinez"></Customer>
		    <Customer pos="25" id="Lamiel"></Customer>
		    <Customer pos="26" id="Trigo"></Customer>
		    <Customer pos="27" id="Aznar"></Customer>
		    <Customer pos="28" id="Ariño"></Customer>
		    <Customer pos="29" id="Borraz"></Customer>
		    <Customer pos="30" id="Caldú"></Customer>
		    <Customer pos="31" id="Aguilar"></Customer>
		    <Customer pos="32" id="Rodriguez"></Customer>
		    <Customer pos="33" id="Escuin"></Customer>
		    <Customer pos="34" id="Latorre"></Customer>
		    <Customer pos="35" id="Moreno"></Customer>
		    <Customer pos="36" id="Abadía"></Customer>
		    <Customer pos="37" id="Hernandez"></Customer>
		    <Customer pos="38" id="Borruel"></Customer>
		    <Customer pos="39" id="Rebullida"></Customer>
		    <Customer pos="40" id="Barberan"></Customer>		    
		    
		    <Customer pos="41" id="K"></Customer>		    
		    <Customer pos="42" id="T"></Customer>		    
		    <Customer pos="43" id="B"></Customer>		    
		    <Customer pos="44" id="J"></Customer>		    
		    <Customer pos="45" id="N"></Customer>		    
		    
		    <Customer pos="46" id="Zaragoza"></Customer>		
		    <Customer pos="47" id="Barcelona"></Customer>		
		    <Customer pos="48" id="Madrid"></Customer>		
		    <Customer pos="49" id="Bilbao"></Customer>		
		    <Customer pos="50" id="Calanda"></Customer>		
		    
		    <Customer pos="51" id="C/Gaspar Sanz"></Customer>		
		    <Customer pos="52" id="C/Mayor"></Customer>		
		    <Customer pos="53" id="Paseo de Calanda"></Customer>		
		    <Customer pos="54" id="Paseo Cuellar"></Customer>		
		    <Customer pos="55" id="C/Compromiso de Caspe"></Customer>		
		    <Customer pos="56" id="Avda. Cesaraugusto"></Customer>		
		    <Customer pos="57" id="Paseo Independencia"></Customer>		
		    <Customer pos="58" id="Paseo Alcañiz"></Customer>		
		    <Customer pos="59" id="Plza. San Francisco"></Customer>		
		    <Customer pos="60" id="Avda. La Clocha"></Customer>		
		    <Customer pos="61" id="Plza. de España"></Customer>		
		    <Customer pos="62" id="C/Ramon y Cajal"></Customer>		
		    <Customer pos="63" id="Avda. Pamplona"></Customer>		
		    <Customer pos="64" id="Plza. La Hoya"></Customer>		
		    <Customer pos="65" id="C/Dr. Fleming"></Customer>		
		    <Customer pos="66" id="Avda. Universitas"></Customer>		
		    <Customer pos="67" id="Avda. Mozart"></Customer>		
		    <Customer pos="68" id="C/Violeta Parra"></Customer>		
		    <Customer pos="69" id="Avda. Pablo Picasso"></Customer>		
		    <Customer pos="70" id="C/J. Sender"></Customer>		
	    </ROOT>'		

-- TABLA SOCIO
SET @Contador=0
WHILE (@Contador<@numSocios)
BEGIN
SET @MaxValue = 15
SET @MinValue = 1

	-- NOMBRE	
		SET @Semilla = RAND()    
		SET @Entero = ((@MaxValue + 1) - @MinValue) * @Semilla + @MinValue   
		    	
		SET @Nombre = (SELECT D.element.value('@id', 'VARCHAR(20)')
						FROM @Variables.nodes('/ROOT/Customer') AS D ( element )
						WHERE D.element.value('@pos', 'INT')=@Entero)
SET @MaxValue = 40
SET @MinValue = 16						
	-- APELLIDO	1
		SET @Semilla = RAND()    
		SET @Entero = ((@MaxValue + 1) - @MinValue) * @Semilla + @MinValue
	    		    	
		SET @Apellido = (SELECT D.element.value('@id', 'VARCHAR(20)')
						FROM @Variables.nodes('/ROOT/Customer') AS D ( element )
						WHERE D.element.value('@pos', 'INT')=@Entero)
	-- APELLIDO 2						
		SET @Semilla = RAND()    
		SET @Entero = ((@MaxValue + 1) - @MinValue) * @Semilla + @MinValue    
		    	
		SET @Apellido = @Apellido + ' ' +(SELECT D.element.value('@id', 'VARCHAR(20)')		
						FROM @Variables.nodes('/ROOT/Customer') AS D ( element )
						WHERE D.element.value('@pos', 'INT')=@Entero)	
						
SET @MaxValue = 79999999
SET @MinValue = 70000000							
	-- NUMERO DNI
		SET @Semilla = RAND()    
		SET @Entero = ((@MaxValue + 1) - @MinValue) * @Semilla + @MinValue    
		SET @NumeroDNI = convert(NVARCHAR(9),@Entero)
		
SET @MaxValue = 41
SET @MinValue = 45							
	-- LETRA DNI
		SET @Semilla = RAND()    
		SET @Entero = ((@MaxValue + 1) - @MinValue) * @Semilla + @MinValue    
		    	
		SET @LetraDNI =(SELECT D.element.value('@id', 'CHAR')
						FROM @Variables.nodes('/ROOT/Customer') AS D ( element )
						WHERE D.element.value('@pos', 'INT')=@Entero)						
	-- COMPLETAR DNI
	SET @NumeroDNI =  @NumeroDNI + @LetraDNI		
									
SET @MaxValue = 70
SET @MinValue = 51		
		-- DIRECCION
			SET @Semilla = RAND()    
			SET @Entero = ((@MaxValue + 1) - @MinValue) * @Semilla + @MinValue    
			    	
			SET @Direccion =(SELECT D.element.value('@id', 'VARCHAR(50)')
							FROM @Variables.nodes('/ROOT/Customer') AS D ( element )
							WHERE D.element.value('@pos', 'INT')=@Entero)	

SET @MaxValue = 99
SET @MinValue = 1
	-- UN NÚMERO DONDE VIVIR
		SET @Semilla = RAND()    
		SET @Entero = ((@MaxValue + 1) - @MinValue) * @Semilla+ @MinValue    
		SET @DireccionNum = convert(NVARCHAR(2),@Entero)	
	SET @Direccion = @Direccion + ' Nº' + @DireccionNum	

SET @MaxValue = 50
SET @MinValue = 46		
	-- CIUDAD
		SET @Semilla = RAND()    
		SET @Entero = ((@MaxValue + 1) - @MinValue) * @Semilla + @MinValue    
		    	
		SET @Ciudad =(SELECT D.element.value('@id', 'VARCHAR(20)')
						FROM @Variables.nodes('/ROOT/Customer') AS D ( element )
						WHERE D.element.value('@pos', 'INT')=@Entero)	
												
SET @MaxValue = 99999
SET @MinValue = 10000
	-- CÓDIGO POSTAL
		SET @Semilla = RAND()    
		SET @Entero = ((@MaxValue + 1) - @MinValue) * @Semilla + @MinValue    
		SET @CP = convert(NVARCHAR(5),@Entero)					

SET @MaxValue = 99999999
SET @MinValue = 90000000
	-- TELÉFONO FIJO
		SET @Semilla = RAND()    
		SET @Entero= ((@MaxValue + 1) - @MinValue) * @Semilla + @MinValue    
		SET @Telefono = convert(NVARCHAR(8),@Entero)			
		
SET @MaxValue = 69999999
SET @MinValue = 60000000
	-- TELEFONO MOVIL
		SET @Semilla= RAND()    
		SET @Entero = ((@MaxValue + 1) - @MinValue) * @Semilla + @MinValue    
		SET @Movil = convert(NVARCHAR(8),@Entero)			
		
	-- FECHA NACIMIENTO
	SET @MaxValue = 30
	SET @MinValue = 1
		-- DIA
			SET @Semilla= RAND()    
			SET @Entero = ((@MaxValue + 1) - @MinValue) * @Semilla + @MinValue    
			SET @dia = convert(NVARCHAR(2),@Entero)	
	SET @MaxValue = 12
	SET @MinValue = 1
		-- MES
			SET @Semilla= RAND()    
			SET @Entero = ((@MaxValue + 1) - @MinValue) * @Semilla + @MinValue    
			SET @mes = convert(NVARCHAR(2),@Entero)				
	SET @MaxValue = 1999
	SET @MinValue = 1940
		-- AÑO
			SET @Semilla= RAND()    
			SET @Entero = ((@MaxValue + 1) - @MinValue) * @Semilla + @MinValue    
			SET @anno = convert(NVARCHAR(4),@Entero)		
			
	SET @FechaNacimiento = @dia + '/'+ @mes + '/' + @anno	
	
	-- FECHA ALTA SOCIO
	SET @MaxValue = 30
	SET @MinValue = 1
		-- DIA
			SET @Semilla= RAND()    
			SET @Entero = ((@MaxValue + 1) - @MinValue) * @Semilla + @MinValue    
			SET @dia = convert(NVARCHAR(2),@Entero)	
	SET @MaxValue = 12
	SET @MinValue = 1
		-- MES
			SET @Semilla= RAND()    
			SET @Entero = ((@MaxValue + 1) - @MinValue) * @Semilla + @MinValue    
			SET @mes = convert(NVARCHAR(2),@Entero)				
	SET @MaxValue = 2005
	SET @MinValue = 1998
		-- AÑO
			SET @Semilla= RAND()    
			SET @Entero = ((@MaxValue + 1) - @MinValue) * @Semilla + @MinValue    
			SET @anno = convert(NVARCHAR(4),@Entero)		
			
	SET @FechaAltaSocio = @dia + '/'+ @mes + '/' + @anno	
	
		-- Cuenta bancaria
	SET @MaxValue = 99999999
	SET @MinValue = 10000000
		-- Banco1
			SET @Semilla= RAND()    
			SET @Entero = ((@MaxValue + 1) - @MinValue) * @Semilla + @MinValue    
			SET @Banco1 = convert(NVARCHAR(8),@Entero)		
		-- Banco2
			SET @Semilla= RAND()    
			SET @Entero = ((@MaxValue + 1) - @MinValue) * @Semilla + @MinValue    
			SET @Banco2 = convert(NVARCHAR(8),@Entero)					
		SET @MaxValue = 9999
		SET @MinValue = 1000
	SET @Banco4 = @Banco1 + @Banco2
	
		-- Cuenta bancaria
	SET @MaxValue = 3
	SET @MinValue = 1
		-- Banco1
			SET @Semilla= RAND()    
			SET @Entero = ((@MaxValue + 1) - @MinValue) * @Semilla + @MinValue    
			SET @idOficina = convert(NVARCHAR(1),@Entero)		
	
   -- INSERT
   insert into Socio (nombreSocio, apellidosSocio, direccionSocio, poblacionSocio, codigopostalSocio, tlfFijoSocio, tlfMovilSocio, fechaNacimientoSocio, fechaAltaSocio, CuentaBancariaSocio, idOficina)
	values (@Nombre, @Apellido, @Direccion, @Ciudad, @CP, @Telefono, @Movil,@FechaNacimiento,@fechaAltaSocio,@Banco4,@idOficina)
  
   SET @Contador = @Contador+1
END

-- TABLA BICICLETA Y REPARAR BICICLETA

SET @Contador=0
WHILE (@Contador<@numBicis)
BEGIN
SET @MaxValue = 999
SET @MinValue = 100
	SET @Semilla= RAND()    
	SET @Entero = ((@MaxValue + 1) - @MinValue) * @Semilla + @MinValue    
	SET @kmsTotal = @Entero 
	SET @kmsReparacion = @kmsTotal % 100
	SET @NumReparaciones = convert(NVARCHAR(1),@kmsTotal/100)
	
	-- Insertar una nueva bicicleta
	insert into Bicicleta (distanciaTotal, distanciaMantenimiento) 
	values (@kmsTotal,@kmsReparacion);
	-- Seleccionar el idBicicleta insertada
	SET @idBici = (SELECT MAX(idbicicleta) FROM bicicleta)
	-- Insertar sus reparaciones
SET @MaxValue = 5
SET @MinValue = 1
SET @Contador2 = 0
	WHILE (@Contador2<@NumReparaciones)
	BEGIN
		SET @Semilla= RAND()    
		SET @Entero = ((@MaxValue + 1) - @MinValue) * @Semilla + @MinValue    
		SET @IdEmpleado = @Entero 
		
		insert into RepararBicicleta (idEmpleado, idBicicleta, descripcionRepararBicicleta, fechaRepararBicicleta)
		values (@IdEmpleado,@IdBici,'Mantenimiento rutinario',SYSDATETIME());

		SET @Contador2 = @Contador2+1
	END
	
	SET @Contador = @Contador+1
END

-- TABLA RECORRIDO
SET @Contador = 0
WHILE(@Contador<@numRecorridos)
BEGIN
-- DAME LE PRIMER ANCLAJE OCUPADO
-- DAME USUARIO ALEATORIA
-- INICIA RECORRIDO
-- DAME ANCLAJE LIBRE DESDE EL FINAL DE OTRA ESTACION

-- DAME USUARIO ALEATORIO
SET @Socio = (SELECT top 1 (idSocio) from socio order by newid())


-- DAME LE PRIMER ANCLAJE OCUPADO
SET @Anclaje = (SELECT MIN(idAnclaje) FROM Anclaje WHERE ocupadoAnclaje=1 and estadoAnclaje=0)
-- Recuepero la estacion
SET @Estacion = (SELECT idEstacion FROM Anclaje WHERE idAnclaje = @Anclaje)
		
exec @idbici = cp_iniciarRecorrido @Socio, @Estacion
-- DAME ANCLAJE LIBRE DESDE EL FINAL DE OTRA ESTACION
SET @Anclaje = (SELECT MAX(idAnclaje)FROM Anclaje WHERE ocupadoAnclaje=0 and estadoAnclaje=0)
exec cp_finalizarRecorrido @idbici, @Anclaje

	SET @Contador = @Contador+1		
END
SELECT COUNT(idSocio) as Socios FROM Socio
SELECT COUNT(idBicicleta) as Bicicletas FROM Bicicleta
SELECT COUNT(idRecorrido) as Recorridos FROM Recorrido
END
GO
--exec cp_insertarDatos 1000, 500, 4000

-- -$$- Procedimiento almacenado: Comprobar numero banco -$$-
/* 
	Este procedimiento almacenado comprueba dado un nombre de columna y una tabla si los números de tarjeta
	de crédito de dicha tabla son verdaderos o falsos, para ello utiliza un cursor para moverse por los registros
	de la tabla y se apoya en el procedimiento almacenado de Algoritmo de número de banco para deducir si el
	número es verdadero o falso.
*/
CREATE PROCEDURE cp_ComprobarNumeroBanco(@nombreColum NVarchar(50), @Tabla NVarchar(50))
AS
BEGIN TRY
		-- Declaracion de variables para el cursor
		DECLARE @numCuenta NVarchar(30),
				@id int,
				@idColum NVarchar(30),				
				@SQL sysname,
				@contador int,
				@idLetra int,
				@resultado int
				
		 SET @idColum = (SELECT TOP(1)COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @Tabla)		 												
		-- Declaración del cursor
		set @SQL = 'DECLARE cTabla CURSOR FOR Select ' + @idColum + ', ' +  @nombreColum + ' from ' + @Tabla
		exec (@SQL)								

		-- Apertura del cursor
		OPEN cTabla

		-- Lectura de la primera fila del cursor
		FETCH cTabla INTO @id, @numCuenta
		
		WHILE (@@FETCH_STATUS = 0 )
		BEGIN						
			exec @resultado = cp_algoritmoNumeroBanco @numCuenta
			IF(@resultado = 0)
			BEGIN				
				PRINT 'El número de tarjeta ' + @numCuenta + ' con identificador ' + CONVERT(varchar(255), @id) + ' es falso'
			END
			ELSE
			BEGIN
				PRINT 'El número de tarjeta ' + @numCuenta + ' con identificador ' + CONVERT(varchar(255), @id) + ' es verdadero'
			END						
			-- Lectura de la siguiente fila del cursor
			FETCH cTabla INTO @id, @numCuenta
		END
					 
		-- Cierre del cursor
		CLOSE cTabla
		-- Liberar los recursos
		DEALLOCATE cTabla
END TRY
BEGIN CATCH
 DECLARE @ErrNum int, @ErrMsg NVARCHAR(4000);
 SET @ErrNum = (SELECT ERROR_NUMBER() AS ErrorNumber);
 SET @ErrMsg = (SELECT ERROR_MESSAGE() AS ErrorMessage); 
 PRINT @ErrMsg
END CATCH
GO
--exec cp_ComprobarNumeroBanco 'cuentabancariaEmpleado', 'Empleado'

-- -$$- Procedimiento almacenado: Algoritmo numero banco -$$-
/* 
	Este procedimiento almacenado comprueba dado un número de tarjeta de crédito si es auténtico o falso.  Para
	ello recurre a un algoritmo para averiguarlo que consiste en:
	1º Seleccionar todos los dígitos impares y multiplicarlos por dos
	2º Sumar los dígitos de los resultados anteriores que sean mayores que uno
	3º Sumar el resultado anterior con los dígitos pares del número de tarjeta de crédito
	4º Si el resultado anterior es divisible por 10 el número de tarjeta es verdadero sino es falso.
*/
CREATE procedure cp_algoritmoNumeroBanco(@numCuenta NVarchar(16))
as
begin try
	DECLARE @contador int,			
			@letra NVarchar(2),
			@cadenaAux1 Nvarchar(8),
			@cadenaAux2 Nvarchar(8),
			@cadenaAux3 Nvarchar(8),
			@intAux1 int,
			@intletra int,
			@digito1 int,
			@digito2 int,
			@suma1 int,
			@suma2 int,
			@resultado int
			
			set @contador = 1			
			set @cadenaAux1=''
			set @cadenaAux2=''
			set @cadenaAux3=''
			set @suma1=0
			set @suma2=0
			set @resultado=0
	-- Clasifica los carácteres pares e impares			
	WHILE (@contador<(LEN(@numCuenta)+1))
	BEGIN
		IF(@contador%2<>0)		
		BEGIN
			SET @letra = (SELECT SUBSTRING(@numCuenta,@contador,1))
			SET @cadenaAux1 = @cadenaAux1 + @letra		
		END
		ELSE
		BEGIN
			SET @letra = (SELECT SUBSTRING(@numCuenta,@contador,1))
			SET @cadenaAux2 = @cadenaAux2 + @letra		
		END
	SET @contador = @contador+1
	END
	-- Multiplicar por dos cada digito y los resultados que sean dos digitos sumarlos entre si
	set @contador = 1	
	WHILE (@contador<(LEN(@cadenaAux1)+1))
	BEGIN	
		SET @intletra = convert(int,(SELECT SUBSTRING(@cadenaAux1,@contador,1)))
		SET @intletra = @intletra*2	
		SET @letra = CONVERT(Varchar(5),@intletra)
		IF(LEN(@letra)=2)
		BEGIN
			SET @digito1 = CONVERT(int,SUBSTRING(@letra,1,1))
			SET @digito2 = CONVERT(int,SUBSTRING(@letra,2,1))
			SET @intletra = @digito1 + @digito2		
		END 
		SET @cadenaAux3= @cadenaAux3 + convert(NVarchar(2),@intletra)
		SET @contador = @contador+1
	END
	-- Sumar todo de la primera cadena
	set @contador = 1	
	WHILE (@contador<(LEN(@cadenaAux3)+1))
	BEGIN	
		SET @digito1 = convert(int,(SELECT SUBSTRING(@cadenaAux3,@contador,1)))
		SET @suma1=@suma1+@digito1
		SET @contador = @contador+1
	END		
	-- Sumar todo de la segunda cadena
	set @contador = 1	
	WHILE (@contador<(LEN(@cadenaAux2)+1))
	BEGIN	
		SET @digito2 = convert(int,(SELECT SUBSTRING(@cadenaAux2,@contador,1)))
		SET @suma2=@suma2+@digito2
		SET @contador = @contador+1
	END
	SET @resultado = @suma1+@suma2
	IF(@resultado%10=0)
	BEGIN
		return 1
	END
	ELSE
	BEGIN
		return 0
	END
END TRY
BEGIN CATCH
 DECLARE @ErrNum int, @ErrMsg NVARCHAR(4000);
 SET @ErrNum = (SELECT ERROR_NUMBER() AS ErrorNumber);
 SET @ErrMsg = (SELECT ERROR_MESSAGE() AS ErrorMessage); 
 PRINT @ErrMsg
 PRINT @ErrNum
END CATCH	
-- -$$$-- TRIGGERS --$$$-	
-- -$$- Trigger: Actualizar recorrido -$$-
/* 
   Este trigger actualiza la fecha de llegada de un recorrido y calcula
   aproximadamente los kms del mismo.  Es disparado por el procedimiento
   almacenado "Finalizar recorrido" en el momento que se actualiza la tabla
   con el idEstacionFinal.  Los kms o distancia del recorrido los calculamos
   tomando como referencia unos 15 kms/hora aproximadamente en un minuto son 
   unos 0.26 kms/min, calculamos la diferencia de minutos entre la fecha de salida y la de 
   llegada y lo multiplicamos por ésta última cifra.   
*/
GO
CREATE trigger TR_actualizaRecorrido on Recorrido
for update
as
BEGIN TRY
	IF UPDATE(idEstacionFinal)
	BEGIN
		UPDATE Recorrido 
		SET fechaLlegadaRecorrido =  SYSDATETIME(), distanciaRecorrido=(DATEDIFF(minute, fechaSalidaRecorrido, SYSDATETIME()))* 0.26
		WHERE idBicicleta = (Select idBicicleta FROM inserted) and distanciaRecorrido is null
	END
END TRY	
BEGIN CATCH
 DECLARE @ErrNum int, @ErrMsg NVARCHAR(4000);
 SET @ErrNum = (SELECT ERROR_NUMBER() AS ErrorNumber);
 SET @ErrMsg = (SELECT ERROR_MESSAGE() AS ErrorMessage);
 PRINT @ErrNum 
 PRINT @ErrMsg
END CATCH
GO

-- -$$$-- INDICES --$$$-	
CREATE NONCLUSTERED INDEX [_dta_index_Recorrido_5_629577281__K6_K7_1] ON [dbo].[Recorrido] 
(
	[idEstacionFinal] ASC,
	[idBicicleta] ASC
)
INCLUDE ( [idRecorrido]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE STATISTICS [_dta_stat_629577281_7_5] ON [dbo].[Recorrido]([idBicicleta], [idEstacionInicio])
go

CREATE STATISTICS [_dta_stat_629577281_8_5_7] ON [dbo].[Recorrido]([idSocio], [idEstacionInicio], [idBicicleta])
go

CREATE NONCLUSTERED INDEX [_dta_index_Socio_5_565577053__K12] ON [dbo].[Socio] 
(
	[idOficina] ASC
)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE STATISTICS [_dta_stat_117575457_2] ON [dbo].[RepararBicicleta]([idEmpleado])
go

CREATE STATISTICS [_dta_stat_117575457_3_2] ON [dbo].[RepararBicicleta]([idBicicleta], [idEmpleado])
go

-- -$$$-- RESTRICCIONES --$$$-	
-- Las cuentas bancarias tengan 16 digitos
ALTER TABLE Empleado ADD CONSTRAINT cuentabancariaEmpleadoTam CHECK(LEN(cuentabancariaEmpleado)=16)
GO
ALTER TABLE Socio ADD CONSTRAINT cuentabancariaSocioTam CHECK(LEN(cuentabancariaSocio)=16)
GO
-- Las cuentas bancarias sean numéricas
ALTER TABLE Empleado ADD CONSTRAINT cuentabancariaEmpleadoNum CHECK(ISNUMERIC(cuentabancariaEmpleado)=1)
GO
ALTER TABLE Socio ADD CONSTRAINT cuentabancariaSocioNum CHECK(ISNUMERIC(cuentabancariaSocio)=1)
GO
-- Los codigos postal tengan 5 digitos y sean numéricos
ALTER TABLE Empleado ADD CONSTRAINT CPEmpleadoRestriccion CHECK(ISNUMERIC(codigopostalEmpleado)=1 AND LEN(codigopostalEmpleado)=5)
GO
ALTER TABLE Socio ADD CONSTRAINT CPSocioRestriccion CHECK(ISNUMERIC(codigopostalSocio)=1 AND LEN(codigopostalSocio)=5)
GO
-- Validar una direccion de email
ALTER TABLE Empleado ADD CONSTRAINT ValidarEmail 
CHECK
(
	-- No puede contener espacios
	CHARINDEX(' ',LTRIM(RTRIM(emailEmpleado))) = 0 
	-- '@' no puede ser el primer caracter de la direccion
AND 	LEFT(LTRIM(emailEmpleado),1) <> '@'  
	-- '.' no puede ser el ultimo carcater de la direccion
AND 	RIGHT(RTRIM(emailEmpleado),1) <> '.' 
	--  '.' debe de ir despues de '@'		
AND 	CHARINDEX('.',emailEmpleado,CHARINDEX('@',emailEmpleado)) - CHARINDEX('@',emailEmpleado) > 1 
	-- Solo una '@' esta permitida	
AND 	LEN(LTRIM(RTRIM(emailEmpleado))) - LEN(REPLACE(LTRIM(RTRIM(emailEmpleado)),'@','')) = 1 
	-- El nombre de dominio debe de tener al menos dos caracteres de extension
AND 	CHARINDEX('.',REVERSE(LTRIM(RTRIM(emailEmpleado)))) >= 3 
	-- No puede haber direcciones como '.@' and '..'
AND 	(CHARINDEX('.@',emailEmpleado) = 0 AND CHARINDEX('..',emailEmpleado) = 0)
)
