# The People Search Application
## Features
The system provides a text box that accepts name search for people based on their first and last name. It will do a contains search on both First Last name. Return persons record if name contains in First or Last name. Input to the text box will send automatically after a 300ms of last input.
Currenlty there is a 1000ms delay is added to the request to showcase the graceful handling of UI if search request takes time. 
The system can add a new person to the Database.

## Tech stack
### Backend
The system using SQLite as Database. Used ASP.NET Web API for building REST service endpoints for CRUD operations. Repoistory Pattern and Dependency Injection is used build the backend. MOQ framework is using for Unit and Integration tests.

