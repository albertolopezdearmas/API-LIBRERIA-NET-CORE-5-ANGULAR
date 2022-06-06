# API-LIBRERIA-NET-CORE-5-ANGULAR
backend (API-LIBRERIA-NET-CORE-5 + BD SQLServer 2012)  frontend (ANGULAR13)
# backend
/LibraryApi/ Web API desarrollado con .Net Core 5 
/LibraryApi/Library.Api/Data/Data.sql  es el scritp de la base de datos para SQL Server se generó con compatibilidad de la versión 2012
/LibraryApi/Library.Api/appsettings.json contiene la cadena de conección a la base de datos "Library": "Server=(local);Initial Catalog=LibraryBD;Integrated Security=True"

# frontend
/LibraryFrontend/Library.Frontend/ frontend desarrollada con Angular 13 
/LibraryFrontend/Library.Frontend/src/app/servicios/api/api.service.ts url de consumo de la Web API url: string = 'http://localhost:43697';
