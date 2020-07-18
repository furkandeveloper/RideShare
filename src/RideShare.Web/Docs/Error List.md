## ModelState Errors

| Inner Key |StatusCode | Description |
| --- | --- |--- |
| Required | 400 | Returns when a required field is null or empty. |
| InvalidField | 400 | Return if format of a field is invalid. Field can be `CreditCard`, `Currecny`, `Custom`, `Date`, `DateTime`, `Duration`, `EmailAddress`, `Html`, `ImageUrl`, `MultilineText`, `Password`, `PhoneNumber`, `PostalCode`, `Text`, `Time`, `Upload`_(File extension, size etc)_, `url` <br><br> Also this has a pattern like: `Invalid{field_name}`. For Example: `InvalidToken`, `InvalidPhoneNumber` etc... |
| DoesntMatch | 400 | If two field doesn't match, like Password & PasswordConfirm |
| MinLength| 400 | Returns if a field shorter than minimum length.  |
| MaxLength | 400 | Returns if a field longer than maximum length |
| OutOfRange | 400 | Returns if a numeric field less or greater than required range. |