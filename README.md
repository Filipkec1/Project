
# Project

A small ASP .NET 5 API.
For its architecture it uses repository pattern with unit of work.

It only uses one table person.

```sh
CREATE TABLE [Person] (
	[Id] uniqueidentifier PRIMARY KEY DEFAULT (NEWID()),
	[Name] varchar(250) NOT NULL,
	[Surname] varchar(250) NOT NULL,
	[City] varchar(250) NOT NULL,
	[Address] varchar(250) NOT NULL,
	[TelephoneNumber] varchar(20) NOT NULL,
	[Email] varchar(100) NOT NULL,
	[OIB] char(11) NOT NULL
)
```
OIB is set to unique.
___

Person controller has 7 possilbe calls .

 1. [GET] /api/PersonApi/{id} - gets person by its Id
 2. [GET] /api/PersonApi - get all people in database
 3. [POST] /api/PersonApi - create new person in database
 4. [PUT] /api/PersonApi - update person in database
 5. [DELETE] /api/PersonApi/{id} - delete person from database
 6. [GET] /api/PersonApi/CheckOib/{oib}
 7. [POST] ​/api​/PersonApi​/Upload


The CheckOib call is used to check if an Oib is unique. It is ment to be used for validation.

The Upload is used to upload a text file that contains a JSON file. 
The JSON file contains a list of new People to add or update. 
Depening if the person is not in the database then it will be add or if it it exist in the database it will be updated. 
An example is present in the [examples](https://github.com/Filipkec1/Project/tree/main/examples "examples") folder