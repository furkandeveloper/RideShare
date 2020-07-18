# Response Scheme
This API returns a standardized response objects for all situations. None of endpoint returns directly main object of result. All of responses are placed into **ApiResult** object.

> **WARNING:** This scheme is used at `v2.1` or higher Api Versions.

# Parameters of ApiResult

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| Success | boolean | ✅ | Information about if proccess resulted successfully or not.  |
| Key | string | ✅ | Type of response. Also each error has their own key. It's more like error code. |
| Data | object /  Generic&lt;T&gt; | ❕ | Contains main data of response. Data can be null with some unsuccessful requests.
| Meta | object | ❌ | Metadata about response, like pagination page counts or depricated elements etc.|
| Message | string | ❕ | Message about request. Mostly unseuccessful requests have message about it. |
| IsUserFriendlyMessage | boolean | ❌ | Indicates if Message comes from resources and readable by directly users or not. |

***

# Examples

- A Successful listing result:

```json
{
    "success" : true,
    "key" : "OK",
    "message" : "Completed operation successfully.",
    "data" : [
        {
            "id" : "1234",
            "title" : "Hello World!"
        },
        {
            "id" : "1235",
            "title" : "Hello World 2!"
        }
    ],
    "meta" :{
        "total" : 2,
        "page" : 1,
        "perPage": 10,
        "totalPages" : 1 
    }
}
```

- A badrequest result:

```json
{
    "success" : false,
    "key" : "ModelState",
    "message" : "Kullanıcı adı alanı gereklidir.",
    "isUserFriendlyMessage": true,
    "data" : [
        {
            "UserName" : "Kullanıcı adı alanı gereklidir."
        }
    ]
}
```

- An internal server error result:

```json
{
    "success" : false,
    "key" : "SystemError",
    "message" : "object reference not set to an instance of an object"
}
```
