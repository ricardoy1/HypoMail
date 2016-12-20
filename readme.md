*Type of Application*
MVC 4 
Web API
Class Libraries
.NET 4.5
Visual Studio 2013

*Solution structure*
1. Open the solution: "HypoMail.sln" with Microsoft Visual Studio 2013
2. Build the solution, this action will update the Nuget packages installed in this solution.
3. There are 2 different projects in the solution:
* HypoMail.Ui: MVC and Web Api controllers to support the front-end client.
* HypoMail.Ui.Tests: Unit test for HypoMail.Ui

*Architecture and Design*

*Application*
This is a basic MVC web application that loads an Angular JS SPA (Single Page Application) on the client side. 

*Front-End*
The front-end Angular JS application communicates with the server (GETs, POSTs) through Angular JS services that consume a Web Api controller.

*Service Layer*
The Web Api controller performs some exception handling by validating the model and returning a proper HTTP response. This controller gets injected, what I decided to call, a Business Service (MailsService) that is in charge of the business validations and interacting with the email server.
In order to keep a ligh-weight client and reduce the exposure of unnecessary information to the it, I decided to use DTOs that will contain the necessary information the user interface requires.

*Business Service Mails*
This MailsService takes care of the business concerns of persisting, getting and deleting Mails rather than the infrastructure related features like HTTP response code, etc.
The MailsService takes the DTOs from the client and transform them back to the business model and the other way around through the use of mappers. (http://en.wikipedia.org/wiki/Data_mapper_pattern).


*Validation*

*Dependency Injection*
I have taken a TDD approach during the development of this application and I used Unity Of Work to inject and mock dependencies.

*Unit Testing*
The appliation counts with many unit tests that cover the main features from back and fron end. 
I used Jasmine to test each of the controls and mocking its dependencies using Spy and $q among others. The Javascript tests can be run from the web app by adding /specs/testrunner.html to the URL.
C# unit testing used Fluent Assertions as I consider this makes unit tests quite readble.

*Unit Testing Technologies*
NUnit
MOQ Mocking Framework
Fluent Assertions 
Jasmine

*How to run the tests?*
C# tests: You can use a NUNIT runner or Resharper if you have it installed in your Visual Studio.
Jasmine: You can run all the Jasmine tests by going to http://{app-url}/specs/testrunner.html
