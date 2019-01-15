# Sala de Conferencias
Una empresa de eventos desea centralizar la gestión de las reservas de salones que ofrece a sus clientes para el dictado de conferencias y la realización de eventos. El departamento de tecnología ha decidido realizar la implementación de una aplicación web, para ello se requiere desarrollar un Web Api Restful en C#.

Una Sala de conferencias se compone de la siguiente información:
-	Nombre del salón
-	Descripción del salón
-	Tipo espacio (Aire libre, Sala vacía, Sala amoblada). 
-	Cualidades de las que disponle el salón (Pantallas, Equipo de sonido, Aire acondicionado, Video Beam, Butacas de lujo).
-	Precio por hora del Alquiler.

Una Reservación contiene los siguientes datos:
-	Salón a reservar
-	Cliente que reserva el salón
-	Hora de inicio de la actividad
-	Hora de culminación
-	Monto total de la reserva
-	Si la misma ya fue pagada por el cliente
-	Confirmada

Un cliente contiene los siguientes datos:
-	Nombre del cliente
-	Número de identificación (Entre 9 y 10 dígitos)
-	Saldo actual que debe por reservaciones
-	Número telefónico (11 dígitos)

El proyecto desarrollará la gestión de Salas, clientes y reservaciones, donde se requiere:
- [x] Administración de un Salón o Sala (CRUD).  
- [x] Administración de un cliente (CRUD).
- [x] Administración de una Reservación (CRUD).

(Se implementaron los endpoint básicos correspondientes)


Tomando en cuenta que se deben respetar las siguientes reglas de negocio:

- [x]	No se puede eliminar clientes que tengan reservaciones sin confirmar. 
- [x]	Al momento de eliminar un cliente se debe eliminar el historial de reservaciones.
- [x]	Un cliente no puede reservar un salón en un periodo determinado de tiempo si el mismo ya se encuentra reservado para ese periodo.
- [x]	Una reserva no puede empezar luego de las 10 PM ni antes de las 7 AM.
- [x]	No se puede eliminar directamente un reservación que ya fue confirmada o pagada.

Adicionalmente se requiere lo siguiente:

- [x]	Poder obtener un listado de las ultimas 10 reservas de un cliente.
- [x]	Obtener un reporte de las reservas pendientes por confirmar.
- [x]	Obtener un reporte de las reservas pendientes por pagar.
- [x]	Confirmar una reservación
- [x]	Pagar una reservación

## Aspectos Técnicos

Cada uno de los siguientes aspectos es Obligatorio:

- [x]	Realizar los Servicios REST y el Backend con ASP.Net Web Api C#.

> Se implementaron los servicios sobre un proyecto ASP.Net Core 2.1

- [x]	El componente de acceso a los datos se debe implementar con ORM, utilizar Entity Framework CodeFirst, se debe tener definido los Migrations y Seeding de los datos.

> Se implementaron las clases correspondientes utilizando la arquitectura de N-Capas.

Cada uno de los siguientes aspectos es opcional, pero entre más de estos aspectossean implementados, mejor será la evaluación final de la prueba:
- [x]	Uso de convención de nombres en Camel Case.

> Se utilizó la convención de nombres adecuada en el desarrollo de todo el código.

- [x]	En el componente de acceso a datos debe implementar Patrón Repositorio.

> Se implementaron repositorios para gestionar el acceso a la capa de dominio correspondiente.

- [x] Implementar algún mecanismo de autenticación y de autorización.

> Se utilizó Identity para gestionar lo pertinente a los aspectos de autorización y autenticación mediante el uso de JWT.

- [x] Utilizar inyección de dependencias mínimo en los límites del sistema.
> El sistema está configurado para utilizar inyección de dependencias en los endpoint y en los servicios.

- [ ]	Tener un proyecto de pruebas unitarias que cubra por lo menos los escenarios de las reglas de negocio.