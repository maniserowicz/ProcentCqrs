This is my playgound for experiments with CQRS in web app.

Main ideas:

 * write-only domain model with no getters/setters, only public methods
 * read model created via DB views
 * all interaction with domain via commands/events

Git tags mark "milesones" in this project's development - use them to navigate though project lifecycle.

For now I am _not_ going to implement event sourcing, as I probably won't be able to use it in a real project in foreseeable future.

My main current issue:

 * how do I test domain if there are no public getters/setters? idea: run a command handler in test, then fetch appropriate read model and verify fields there... but that kinda sucks - I should test behavior, not state transition (?), especially in very simple instructions (like user.ChangeName()) that do not affect system logic

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