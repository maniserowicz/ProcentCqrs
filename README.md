This is my playground for experiments with CQRS in web app.

Main ideas:

 * write-only domain model with no getters/setters, only public methods
 * read model created via DB views
 * all interaction with domain via commands/events

Git tags mark "milestones" in this project's development - use them to navigate though project lifecycle.

For now I am _not_ going to implement event sourcing, as I probably won't be able to use it in a real project in foreseeable future.

Testing the domain (it has no public getters/setters)

I solved this (however I'm not sure if it's the right way) by testing with DB - tests call command handlers instead of domain objects themselves.
Domain command handlers get NH session, and tests use Simple.Data to verify that operation has the expected result. I currently don't have any idea of how to do it better.

Further development ideas:

 * add authentication and authorization
 * create command wrappers for handling authorization and transactions
 * ...

Tools user so far:

 * ASP MVC 3
 * SQL Server
 * Autofac
 * NHibernate (for write model)
 * Simple.Data (for read model)
 * NLog
 * SQL Server CE (for testing)
 * xUnit (will probably be converted to MSpec ofter runner for R#6 release)
 * FakeItEasy