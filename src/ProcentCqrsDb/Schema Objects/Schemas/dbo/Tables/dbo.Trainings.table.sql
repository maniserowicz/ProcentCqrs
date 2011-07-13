CREATE TABLE [dbo].[Trainings]
(
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Trainings] PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    )
)	