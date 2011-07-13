CREATE TABLE [dbo].[Users](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Email] [nvarchar](50) NOT NULL,
    [Password] [nvarchar](50) NOT NULL,
    [Bio] [nvarchar](250) NULL,
    [FirstName] [nvarchar](250) NULL,
    [LastName] [nvarchar](250) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    )
)