# Caso 2 - Gestión de Proyectos de Consultoría

API REST desarrollada para la gestión de proyectos de una empresa de servicios de consultoría. El proyecto utiliza PostgreSQL como base de datos y .NET 10 con C#, aplicando el enfoque **Database First** mediante **Entity Framework Core Scaffolding**.

## Tecnologías

| Tecnología | Uso |
|---|---|
| .NET 10 | Framework principal del proyecto |
| C# | Lenguaje de programación |
| ASP.NET Core Web API | Desarrollo de la API REST |
| Entity Framework Core | ORM para el acceso a datos |
| Npgsql | Proveedor de PostgreSQL para Entity Framework Core |
| PostgreSQL | Motor de base de datos |
| Swagger / OpenAPI | Documentación y prueba de los endpoints |

## Arquitectura

El proyecto utiliza los patrones **Repository** y **Unit of Work** para organizar y gestionar el acceso a datos.

```text
Controller
    │
    ▼
  Service
    │
    ▼
Unit of Work
    │
    ├── Repository Cliente
    ├── Repository Empleado
    ├── Repository Proyecto
    ├── Repository Tarea
    └── ...
    │
    ▼
Entity Framework Core
    │
    ▼
PostgreSQL
