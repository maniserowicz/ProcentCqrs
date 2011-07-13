CREATE VIEW [readmodel].[UsersForUsersIndex]
	AS SELECT u.Id, u.FirstName, u.LastName, u.Email FROM [dbo].[Users] u