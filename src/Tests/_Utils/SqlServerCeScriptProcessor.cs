using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;
using System;

namespace ProcentCqrs.Tests._Utils
{
    public class SqlServerCeScriptProcessor
    {
        public class ScriptFileInfo
        {
            public virtual string Path { get; set; }
            public virtual string PreparedContent { get; set; }
        }

        /// <summary>
        /// Sql Server CE does not support views, index ordering etc,
        /// so scripts need to be simplified here.
        /// </summary>
        public IEnumerable<ScriptFileInfo> PrepareScripts(IEnumerable<string> files)
        {
            if (files == null)
            {
                yield break;
            }

            foreach (var scriptFile in files.Where(x => this.CanProcessFile(x)))
            {
                string script = File.ReadAllText(scriptFile);

                string preparedContent = Prepare(script);

                yield return new ScriptFileInfo()
                    {
                        Path = scriptFile,
                        PreparedContent = preparedContent,
                    };
            }
        }

        public bool CanProcessFile(string filePath)
        {
            return filePath.DoesNotEndWith(".schema.sql")
                && filePath.DoesNotEndWith(".view.sql");
        }

        public string Prepare(string script)
        {
            script = RemoveSchemaQualifications(script);
            script = RemoveIndexKind(script);
            script = RemoveIndexOrder(script);

            return script;
        }

        private string RemoveIndexOrder(string script)
        {
            return Regex.Replace(script, @" (ASC|DESC)(\s|,)", "$2");
        }

        private string RemoveIndexKind(string script)
        {
            script = Regex.Replace(script, @" CLUSTERED", string.Empty);
            script = Regex.Replace(script, @" NONCLUSTERED", string.Empty);
            return script;
        }

        private string RemoveSchemaQualifications(string script)
        {
            return Regex.Replace(script, @"\[.+\]\.", string.Empty);
        }
    }


    #region Tests

    public class SqlServerCeScriptProcessor_Tests
    {
        private string original = @"CREATE TABLE [dbo].[UserTrainings](
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
)";

        private string expected = @"CREATE TABLE [UserTrainings](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [UserId] [int] NOT NULL,
    [TrainingId] [int] NOT NULL,
    [AssignmentDate] [datetime] NOT NULL,
 CONSTRAINT [PK_UserTrainings] PRIMARY KEY 
(
    [Id]
),
 CONSTRAINT [UQ_UserTrainings] UNIQUE 
(
    [TrainingId],
    [UserId]
)
)";

        [Fact]
        public void Removes_what_needs_to_be_removed()
        {
            string result = new SqlServerCeScriptProcessor().Prepare(original);

            Assert.Equal(expected, result);
        }
    }

    #endregion
}