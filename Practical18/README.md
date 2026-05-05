## Task
* Implement APIs for CRUD operations on the student model using EF Core.
* Create views for the CRUD operations with the following functionality: (Index, Create, Edit, Details, and Delete with confirmation popup).
* Implement ViewModels and configure AutoMapper (Mapper.CreateMap<>) for mapping domain models to view models.
* Understand Model Binding, HTTP Status Codes, Input/Output Formatters and Content Navigation for API responses.


## Note

* Task is implemented inside AuthController, StudentsController and UserController.
* AutoMapper is used as for mapping each entity model to view model and vice versa per given in practical.
* Logger is used inside global exception middleware.
* Dependencies are wired correctly using Extention methods (place inside Practical18.MVC/Ententions directory) and those methods are called inside Program.cs file.
* JWT Token is used for authorization.
* Inside Student Controller only User with Admin Role can create, update and delete a Student entry and Normal user can only view students data that too after logging in.
* Also Normal User can not access User Controller API endpoints, only Admin User can access them.