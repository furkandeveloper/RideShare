# Architecture

## Contexts
All database contexes are placed here. Mongo and EntityFramework contexes are separed with namespaces and folders as **Mongo** and **EntityFramework**


***
## Controllers
All AspNet Controllers are placed here. They are seperated by version. Versionning rule is `v{ver}`, as sample `v1`, `v2` or `v3`...

### v1
This namespace contains Version 1.0 Controllers and works with `MongoDb` at same time.

***
## Dtos

This folder includes all Data Transfer Objects and they're seperated by **Request**, **Response** and **Service** folders.

***
## Exceptions
This folder contains custom exceptions which is inherited from BaseException. BaseException is handled by Middleware and returns a status code from exception. Exceptions can be thrwon in anywhere like Repository.

***
## Helpers
This folder includes helper codes and extensions which is undependent from any context. They are generic helper codes.


***
## Mappings
This folder contains AutoMapper **Profile** objects. They define mappings between Dtos and Models. Also  this mappings is used for Projection to database queries.

***
## Middleware
This folder contains middleware processes like, Authentication, Authorization and Exception Handling. Also ActionFilters are here too.

***
## Models
All the database models are here. Also They're foldered by database provider like **EntityFramework**.

***
## Repositories
All repositories with database query are here. Only this repository objects can reach database data! You can not use DbContext except repository. If you need reach database for custom reason, just create a repository to do that and resolve that repository from Dependency Injection Container.

***
## Services
Normally this layer must be a logic layer. But in this project and this version, it has only services which don't touch database and external api integrations.
***
## Validations
This folder includes model validations. You mustn't use if for check any of property in codebase! **All validations must be placed here instead of inline `if`**. These validations are provided by FluentValidation.

***
# Dependencies

| Library | Description | Version |
| --- | --- | :---: |
| [AspNetCoreRateLimit](https://github.com/stefanprodan/AspNetCoreRateLimit) | rate limiting solution designed to control the rate of requests that clients can make to a Web API or MVC app based on IP address or client ID.  | v3.0.5 |
| [AspNetCore.MarkdownDocumenting](https://github.com/enisn/MarkdownDocumenting) | This library publishes local markdown documentations from Docs folder in project on Web Application.  | v1.1.3 |
| [AutoFilterer](https://github.com/enisn/AutoFilterer) | This library provides QueryString to LINQ conversion to automate filterings and queries. | v1.0.2 |
| [AutoMapper](https://github.com/AutoMapper/AutoMapper) | This is a object mapping and projection library. Projection can be applied directly to Entity Framework Database queries instead of using long Select statements. | v8.1.1 |
| [AutoMapper.Extensions.Microsoft.DependencyInjection](https://github.com/AutoMapper/AutoMapper) | AutoMapper extension to use AutoMapper over all abstractions with Dependency Injection | v8.1.1 |
| [FluentValidation.AspNetCore](https://github.com/JeremySkinner/FluentValidation) | A small validation library for .NET that uses a fluent interface and lambda expressions for building validation rules. | v8.4.0 |
| [Microsoft.AspNetCore.App](https://github.com/aspnet/AspNetCore) | Main SDK of AspNetCore which aims .Net Core Framework | v2.2.0 |
| [Microsoft.AspNetCore.Mvc.Versioning](https://github.com/Microsoft/aspnet-api-versioning/wiki) | A Library to apply versionning to REST API Controllers by Microsoft | v3.1.3 |
| [Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer](https://github.com/Microsoft/aspnet-api-versioning/wiki) | A Library to explore versionned api metas and it used to provide datas to swagger. | v3.2.0 |
| [EntityFrameworkCore](https://github.com/aspnet/EntityFrameworkCore) | Base Entity Framework Library on .Net core  | v2.2.4 |
| [Microsoft.NETCore.App](https://github.com/dotnet/core) | Main SDK of .NET CORE  | v2.2.0 |
| [Microsoft.VisualStudio.Azure.Containers.Tools.Targets](https://github.com/microsoft/DockerTools) | Docker Tools for Visual Studio  | v1.7.12 |
| [Microsoft.VisualStudio.Web.CodeGeneration.Design](https://github.com/aspnet/Tooling) | Controller, View, Model, etc... scaff-holding libray.  | v2.2.3 |
| [MongoDB.Driver](https://docs.mongodb.com/ecosystem/drivers/csharp/) | An Offical ORM Provider to communicate with a MongoDb | v2.8.1 |
| [MongoDB.Driver.Core](https://docs.mongodb.com/ecosystem/drivers/csharp/) | An Offical ORM Provider to communicate with a MongoDb | v2.8.1 |
| [StyleCop.Analyzers](https://github.com/DotNetAnalyzers/StyleCopAnalyzers) | Implementation of the StyleCop rules using the .NET Compiler Platform. Where possible, code fixes are also provided to simplify the process of correcting violations. | v1.1.118 |
| [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) | Swagger tooling for API's built with ASP.NET Core. Generate beautiful API documentation, including a UI to explore and test operations, directly from your routes, controllers and models. | v4.0.1 |


