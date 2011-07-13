CREATE VIEW [readmodel].[UserForTrainingsDetails]
AS
    SELECT u.Id, u.Email, u.FirstName, u.LastName,
        ut.AssignmentDate, ut.TrainingId
    FROM dbo.Users u
    JOIN 
        dbo.UserTrainings ut ON u.Id = ut.UserId