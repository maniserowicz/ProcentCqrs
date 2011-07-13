CREATE VIEW [readmodel].[TrainingForTrainingsDetails]
	AS SELECT t.Id, t.Name, (SELECT COUNT(*) FROM [dbo].[UserTrainings] ut WHERE ut.TrainingId=t.Id) AS UsersCount FROM [dbo].[Trainings] t