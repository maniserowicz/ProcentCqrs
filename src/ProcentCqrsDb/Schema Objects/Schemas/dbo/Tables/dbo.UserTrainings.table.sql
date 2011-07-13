CREATE TABLE [dbo].[UserTrainings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[TrainingId] [int] NOT NULL,
	[AssignmentDate] [datetime] NOT NULL,
 CONSTRAINT [PK_UserTrainings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
),
 CONSTRAINT [UQ_UserTrainings] UNIQUE NONCLUSTERED 
(
	[TrainingId] ASC,
	[UserId] ASC
)
)