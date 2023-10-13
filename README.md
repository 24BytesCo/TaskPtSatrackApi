---

# API de Administración de Tareas

La **API de Administración de Tareas** es una aplicación de gestión de tareas que permite a los usuarios crear tareas, asignarles una categoría y establecer una fecha límite para completarlas. Este proyecto se ha desarrollado como parte de una prueba técnica para la empresa Satrack.

## Estructura de Capas

Este proyecto sigue una estructura de capas que separa claramente las responsabilidades:

- **Capa API**: La capa de API es la interfaz de comunicación con los clientes. Proporciona puntos finales para acceder a las funcionalidades de la aplicación.

- **Capa de Dominio (Domain)**: La capa de dominio contiene las entidades principales, lógica de negocio y reglas del dominio. Es el núcleo de la aplicación.

- **Capa de Aplicación (Application)**: La capa de aplicación contiene las características y casos de uso de la aplicación. Se encarga de la lógica de aplicación y utiliza la capa de dominio para realizar operaciones.

- **Capa de Infraestructura (Infrastructure)**: La capa de infraestructura proporciona implementaciones concretas para la persistencia de datos, acceso a la base de datos y otros aspectos técnicos.

## Características Clave

- **Arquitectura Limpia**: La aplicación sigue los principios de la arquitectura limpia para separar claramente las responsabilidades en capas de aplicación, dominio y presentación.

- **Mapping de Clases VM y Dto**: Se utilizan mapeos de clases para convertir entre modelos de vista y objetos de transferencia de datos.

- **Diseño de Patrón CQRS**: Se implementa el patrón CQRS (Command Query Responsibility Segregation) para separar comandos y consultas.

- **MediatR para Controladores**: Se utiliza MediatR para gestionar comandos y eventos en los controladores.

- **MediatR para Características de Aplicación**: MediatR se emplea para administrar características y casos de uso de la aplicación.

- **Middleware para Excepciones**: Se ha creado middleware para manejar excepciones y proporcionar respuestas de error claras a los clientes.

- **Validación de Transacciones**: La validación de transacciones se lleva a cabo utilizando FluentValidation para garantizar la integridad de los datos.

- **Patrón Repository Genérico**: Se utiliza un patrón de repositorio genérico para acceder y gestionar datos en la base de datos.

- **Validaciones Genéricas**: Se implementan validaciones genéricas para garantizar la calidad de los datos ingresados.

## Requisitos

Asegúrate de tener las siguientes herramientas instaladas:

- [.NET](https://dotnet.microsoft.com/download/dotnet/7.0) (7.0.401)
- [Entity Framework Core (EF)](https://docs.microsoft.com/en-us/ef/core/get-started/install) (7.0.12)

## Migraciones de Base de Datos

Para realizar las migraciones de base de datos, puedes ejecutar el siguiente comando:

```bash
dotnet ef migrations add InitMigration -p .\src\Infrastructure -s .\src\Api
```
