# WebApp

Features:
    - MVC architecture with very little dependencies between layers
    - DOTNET EF used as ORM
    - XUnit Test project to tests classes, using Moq to mock dependencies DI'ed in class
    - Used InMemoryDb class that implements IRepository, which is the only class that interacts with DB (only 4 calls), and calls the tested Repository class while inserting a new DbContext unique to the Test
    - Clean, easily extendable, Filters; which use the IFilter interface which only exposes method "ApplyFilter"
    - Same for IValidator<T>  which take a generic type in order to DI multiple validators which validate different classes
    - Fully async app all the way through
    - Validation for new orders in front end and back end
    - Basic frontend that makes used of Dotnet Views and PartialViews
    
TODO:
    - Finish UI Admin page (pagination, filters, popup modal)
    - Add login for admins
