Welcome to documentation
# Ride Share Api Documentation


This project is sample for Event.


> Table of Contents
> - [Summary](#summary)
> - [Constants](#constants)
> - [Authorization](#authorization) 
> - [Authentication](#Authentication) 
> - [Controllers](#controllers) 
> - [Api Responses](#api-responses) 
> - [Services](#ipaymentservice) 
> - [Repositories](#repositories) 

<hr />

# Summary

| Directories   | Description |
| ---           | ---         |
| Contexts      | Database contexts are here. Currently MongoDB Context is here. |
| Controllers   | Default folder of MVC for Controllers. Controller Versions are seperated by folders like V1, V2 etc.   |
| Extensions    | Includes Classes with Extension Methods.    |
| Exceptions    | Includes Custom Exceptions to allow throwing exceptions handled situations in anywhere in app.    |
| Helpers       | Includes helper class to make things easier. |
| Middleware    | Includes stuffs which handles and works for all requests. Like Auth., Exceptions etc.
| Models        | Default folder of MVC for Models. Includes database models and service models. |
| Repositories  | Includes queries of database. It's an access layer to database.
| Services      | Includes connections of services. It's an access layer to other services. Like Payment, Tenant etc.


<hr />


### For Server;

```csharp
[Authorize] // < --- This endpoint requires authentication
public async Task<IActionResul> MyMethod()
{
   //do something...
}
```

or

```csharp
[Authorize] // < --- Endpoints in this controller require authentication
public class MyController : BaseController
{
    [AllowAnonymous] // < --- But this endpoint doesn't require any authentication
    public async Task<IActionResul> MyMethod()
    {
       //do something...
    }
}
```


<hr />


## Api Responses

Api responses all request with same sheme. You can see sheme with following block:

```
    Message                 : Message about response.
    IsUserFriendlyMessage   : The message comes from resource or not
    Key                     : key of Warning / Error.
    Data                    : Result of request
    Meta                    : Metadatat about Data. (Ex.: PaginationData for lists etc.)
    Errors                  : Handled errors during request.
        
```

<hr />


# Services

Here includes communication layers to other Services. 

# Repositories

Here includes access layers to database. 
There are 3 folders in here. 

1. Base
    - Includes `Generic` **BaseRepository** and **IBaseRepository** which provides main CRUD process independent of model. Just inherit from this and *CRUD process are ready to use*.
2. Abstraction
    - Here is for `Interfaces` of repositories. Interfaces are extracted to here from repositories.
3. Concrete
    - Includes concrete Repository `classes`.


<hr />
