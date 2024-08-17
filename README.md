# Demo
Because this app doesn't provide a visual interface, i will describe the api's functionalities below, and explain how things are supposed to work.

###### The application is a simple team management app designed for coaches and footballers, where one can create an account, make a request to be either a coach / footballer, with their details. Later on, a footballer's request can be resolved by a coach (!only if that coach is managing the team from the footballer's request!) , or by an admin. A coach's request can only be resolved by an admin. This functionality works on the RBAC principle, implemented with JWT's. In their token, each user has a main role, and based on that role, they can access specific endpoints / not.

###### After an user has a role assigned, they have access to the team's channel. The coach can post announcements (delete and update them as well), and the footballers from that team can read the announcements. A footballer cannot see announcements from channels other than their team's, and a coach cannot post announcements to channels other than their team's.

###### Along with other endpoints that aren't really worth mentioning, this is the main functionality that the app provides. It is a simple API, but I have used a lot new technologies and learnt a lot, even if the complexity is not a high one.



## Things i've learnt while doing this
- **Working with JWT's and implementing RBAC. Although i used JWT's before, this was my first time using this technology in an ASP.NET Web API, and I learnt a lot about it.**

- **Working with the controller-service-repository architecture and understanding the responsibilities of the latter. I made some mistakes in the beginning, like including business logic in the repository, but later understood how things are supposed to work and refactored the code.**
- **Unit testing: This was the first project I wrote unit tests for, using Xunit and NSubstitute for mocking. I learnt some key concepts such as differences between unit/integration testing, what mocking is, what code coverage is, and others.**
- **Sanitizing input data with Fluent Validation. Before this project, I have only used the default method provided in ASP.NET Core for data validation (defining custom validators), but in this project I learnt how to validate input data with Fluent Validation and implemented this for all the incoming data from the client.**
- **Working with DTO's and their importance. Even though i had an idea about what problem they solve, i never used DTO's in an application before, and this project was perfect for me to learn how and when to use them.**


