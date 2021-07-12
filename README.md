This project was build using .Net Core 3.1 and VS 2019.

It is a web api built for receving and displaying orders sent by a point of sale in a restaurant.
It uses Swagger to display all the application endpoints and schemas.

The unit/integration tests are built using xUnit.
It uses dependency injection from .Net Core.

It implements an In-Memory Queue, using .Net native "ConcurrentQueue" class.
It has a stub for a "Database Queue", available for better understanding of how to expand on the original design.