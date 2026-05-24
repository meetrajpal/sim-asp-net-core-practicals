## Task
* Implement global exception handling using middleware.
* Implement auditing and application logging into the database and then into a file as a backup.
* Implement a generic repository for database operations using the Unit of Work pattern.
* Implement asynchronous functions.
* Debug the entire application flow to get a grasp over breakpoints, watches, code stepping, inspecting, etc.

## Note
* Global exception middleware is placed in Practical20.API/Middlewares directory.
* Auditing table (AuditLogs) is created in DB using EF, Auditing entity is placed inside Practical20.Domain/Entities directory and auditing is handled inside SaveChanges method of ApplicationDbContext class centrally.
* Application logging table (Logs) is created in DB using Serilog and Serilog configuration is placed inside Practical20.API/Extensions directory.
* Generic repository is impletmented and Named as BaseRepository placed inside Practical20.Infrastructure/Repositories directory.
* Unit of work is also implemented and placed inside Practical20.Infrastructure/UnitOfWork directory.
* All functions are implemented as Asyncronous.
* 
