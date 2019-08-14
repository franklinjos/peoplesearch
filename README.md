# The People Search Application
## Features
The system provides a text box that accepts name search for people based on their first and last name. It will do a contains search on both First Last name. Return persons record if name contains in First or Last name. Input to the text box will send automatically after a 300ms of last input.

Currenlty there is a 1000ms delay is added to the request to showcase the graceful handling of UI if search request takes time. The system can add a new person using form with basic validation and via REST API to the Database.

Each person shows as a card with profile details, UI support four different screen sizes 360px , 768px , 992px and 1440px
In smallest screen system will stack all cards vertically, next three breakpoints cards will show in 2, 3 and 4 columns. System will skeleton loading while loading data in progress.

## Tech stack

### Back-end
The system using SQLite as Database. Used ASP.NET Web API for building REST service endpoints for CRUD operations. Repoistory Pattern and Dependency Injection is used build the backend. MOQ framework is using for Unit and Integration tests.

## Front-end
The system is using react for UI framework, styled-components for the styling needs, @blueprintjs for UI components. jest for unit testing.

## Screen shots

### Search 
#### Initial
![Alt text](snapshots/search.JPG?raw=true "Search")

#### Results
![Alt text](snapshots/search-res.JPG?raw=true "Search results")

### Add person form 
![Alt text](snapshots/add-person.JPG?raw=true "Add Person")

### Skeleton loading
![Alt text](snapshots/loading.JPG?raw=true "Skeleton loading")

### Screen break point
#### X-Small
![Alt text](snapshots/breakpoint-xsmall.JPG?raw=true "x-small")

### Small
![Alt text](snapshots/breakpoint-small.JPG?raw=true "Small")

### Medium
![Alt text](snapshots/breakpoint-med1.JPG?raw=true "Medium")

### Large
![Alt text](snapshots/breakpoint-lrg.JPG?raw=true "Skeleton loading")




