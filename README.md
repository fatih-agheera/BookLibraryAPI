# BookLibraryAPI project for Case Study
This project demonstrates my skills in developing a CRUD Web Api application using the following technologies:  

 - .NET 9.0
 - Three-layer architecture
 - Entity Framework Core
 - Mongo DB
 - Authentication via JwtBearer
 - Swagger UI
 - AutoMapper
 - FluentValidation

# Description
The application has 2 controllers: CRUD (Create, read, update, delete) operations for Books and Register/Login actions. Book library is 'Read only' for Anonymous users and 'Fully accessible' for Authenticated ones.

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
