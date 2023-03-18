# API Documentation

This API allows user registration and retrieval of user data. It provides three methods:

1. Method for user registration
2. Method to retrieve a list of registered users
3. Method to retrieve user data based on email and password

## Database
By default, the SQL Server localdb that comes with Visual Studio is used. Can be changed in appsettings.Development.json as ConnectionStrings: RegistrationConnectionString value.
If there is no existing database, the solution initializes it at the start of the application.

## Swagger
The solution comes equipped with Swagger for ease of use and documentation.
