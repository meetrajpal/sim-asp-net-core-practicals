## Task
* Create below models:
* Students model (Fields & datatypes as per your discretion).
* Users model for Authentication purposes with properties for the First name, Last name, Email address, Mobile Number, and Password.
* Roles model for Authorization purposes with 2 roles (Admin user and Normal user).
* Connect to a SQL database and perform Code First migration using Entity Framework Core
* Configure connection strings for the database into the application.
* Implement Repository Pattern with Dependency Injection for performing CRUD operations with the database (Students table).
* Understand Scoped vs Singleton vs Transient services.


## Note

* Task is implemented inside AuthController, StudentsController and UserController.
* Logger is used inside global exception middleware.
* Dependencies are wired correctly using Extention methods (place inside Practical17.API/Ententions directory) and those methods are called inside Program.cs file.
* JWT Token is used for authorization.
* Inside Student Controller only User with Admin Role can create, update and delete a Student entry and Normal user can only view students data that too after logging in.
* Also Normal User can not access User Controller API endpoints, only Admin User can access them.