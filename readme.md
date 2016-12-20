*Type of Application*
MVC 4 
Web API
Class Libraries
.NET 4.5
Visual Studio 2013

*Visual Studio Project*
Following the instructions of not using MVC scaffolding, I started by creating an Empty Web Api project and then started to add the features on top of it.

*Solution structure*
1. Open the solution: "HypoMail.sln" with Microsoft Visual Studio 2013
2. Build the solution, this action will update the Nuget packages installed in this solution.
3. There are 4 different projects in the solution:
* HypoMail.Ui: MVC and Web Api controllers to support the front-end client.
* HypoMail.Ui.Tests: Unit test for HypoMail.Ui
* HypoMail.Business: Back-end business services, validators, exception handling and repository.
* HypoMail.Business.Tests: Unit tests for HypoMail.Business 

*Architecture and Design*

*Application*
This is a basic MVC web application that loads an Angular JS SPA (Single Page Application) on the client side. 

*Front-End*
The front-end Angular JS application communicates with the server (GETs, POSTs) through Angular JS services that consume a Web Api controller.

*Service Layer*
The Web Api controller performs some exception handling by validating the model and returning a proper HTTP response. This controller gets injected, what I decided to call, a Business Service (MailsService) that is in charge of the business validations and interacting with the data respository.
In order to keep a ligh-weight client and reduce the exposure of unnecessary information to the it, I decided to use DTOs that will contain the necessary information the user interface requires.

*Business Service Mails*
This MailsService takes care of the business concerns of persisting, getting and deleting Mails rather than the infrastructure related features like HTTP response code, etc.
The MailsService takes the DTOs from the client and transform them back to the business model and the other way around through the use of mappers. (http://en.wikipedia.org/wiki/Data_mapper_pattern).


*Validation*
The validator returns a list with the validation errors and then they are returned back to the client. Even though the UI does not allow the user to enter a Mail without modules, for instance, the backend validates everything again to make sure that we can rely on the service. In addition, this is a web-app and a person with a minimum knowledge of web technologies would be able to remove the modules from the HTTP request before it reaches the Ajax call and then our model would end-up having an wrong Mail.


*Dependency Injection*
I have taken a TDD approach during the development of this application and I used Unity Of Work to inject and mock dependencies.

*Unit Testing*
The appliation counts with many unit tests that cover the main features from back and fron end. 
I used Jasmine to test each of the controls and mocking its dependencies using Spy and $q among others. The Javascript tests can be run from the web app by adding /specs/testrunner.html to the URL.
C# unit testing used Fluent Assertions as I consider this makes unit tests quite readble.

*Assumptions*

Assignement: The list should show the Mail name, price, duration and “new” column. Modules can be loaded via a link to Mail “Details” which will list out Mail details and Modules. If the item was created in the past 7 days the new column should state that this Mail is “new”." 

*Dates*
I  assume we don't need to display the created and modified dates at all. They will be used for internal statistics, logs and finding out if the Mail is "New" or not.

Modifying any module or Mail attribute will change the modified date of the Mail.
Modifying the module will change the modified date of the module.
The dates will be handled on the server side.

*Duration*
The duration of the Mail is given by the total of hours of its modules. Therefore, it will be redundant to keep record in the database of the total of hours of a Mail (http://en.wikipedia.org/wiki/Second_normal_form).
The duration of the Mail and modules will be given in hours and each module cannot be longer than 24 hours. 
The hours duration will allow only an interger number.

*Clarifications*
Even though I didn't have enough time to finish the sorting and search, I tried to leave the application as ready as I could in order to address that task in the future.

*Peding Features due to the lack of time*
Sorting. 
Search.
Logging.
Security.

*Testing*
I have created around 50 unit tests:
26 Back end tests.
24 Javascript tests.

*Unit Testing Technologies*
NUnit
MOQ Mocking Framework
Fluent Assertions 
Jasmine


*How to run the tests?*
C# tests: You can use a NUNIT runner or Resharper if you have it installed in your Visual Studio.
Jasmine: You can run all the Jasmine tests by going to http://{app-url}/specs/testrunner.html

*Live Demo*
You will be able to find a live sample of the exact version of this application in: http://HypoMail.apphb.com/
