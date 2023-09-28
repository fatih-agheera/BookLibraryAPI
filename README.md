# BookLibraryAPI project for Modsen Company
This project demonstrates my skills in developing a CRUD Web Api application using the following technologies:  

 - .NET 6.0
 - Three-layer architecture
 - Entity Framework Core
 - Microsoft SQL Server
 - Authentication via JwtBearer
 - Swagger UI
 - AutoMapper
 - FluentValidation

# Description
The application has 2 controllers: CRUD (Create, read, update, delete) operations for Books and Register/Login actions. Book library is 'Read only' for Anonymous users and 'Fully accessible' for Authenticated ones.

# Run Locally
Clone the project

```bash
  git clone https://github.com/HaidukEvgen/BookLibraryAPI
```

Go to the project directory

```bash
  cd BookLibraryAPI
```

Set your connection string to MS SQL Server in "ConnectionStrings/ConnectionStringLibraryApiDbSql" inside appsettings.json    

`"ConnectionStrings": {
    "ConnectionStringLibraryApiDbSql": "Data Source=<ServerName>;Initial Catalog=<CatalogName>"
  }`

Create Database using CLI 

```bash
dotnet ef database update --project DataLayer\DataLayer.csproj --startup-project BookLibraryAPI\BookLibraryAPI.csproj --context DataLayer.Data.LibraryDbContext --configuration Debug 20230928120140_InitDB
```
If you are getting an error, it is possible that you don't have Entity Framework Core .NET Command-line Tools installed. In this case run:
```bash
dotnet tool install -g dotnet-ef --ignore-failed-sources
```
Start the server

```bash
  dotnet run --project BookLibraryAPI\BookLibraryAPI.csproj --launch-profile BookLibraryAPI
```

Go to swagger page 

    https://localhost:7098/swagger/index.html

You can see 5 request for events and 2 for register/login.

![image](https://github.com/HaidukEvgen/BookLibraryAPI/assets/92396956/95713a0a-033e-433f-8e08-8dfb442009da)


### Book Controller

 - GET - Getting list with all book from database
 - GET with {id} - Getting a certain book by its Id
 - GET with {string} - Getting a book by its ISBN
 - POST - Create a new book. Request body must contain a book info in JSON. JWT token required in headers for authentication
 - PUT with {id} - Update info about existing book by its Id. Request body must contain a book info in JSON. JWT token required in headers for authentication
 - DELETE with {id} - Delete book by its Id. JWT token required in headers for authentication

### User Controller

 - POST in User/Resiter with Body (login-password pair) - For registration.
 - POST in User/Login with Body (login-password pair) - For authentication.

  `{
      "username": "User1",
      "password": "123"
  }`

## JWT Authentication

After a successful login, the server will return a Jwt Token. Copy the token.

### Authentication

1. Find the green "Authorize" button at the top of the page.
2. Click on it and insert your Jwt Token into the "Value" field.
3. Click "Authorize".

If your token is valid, you will be able to perform Creation, Update, and Deletion operations.

### Token Regeneration

To regenerate the token, log in again.
