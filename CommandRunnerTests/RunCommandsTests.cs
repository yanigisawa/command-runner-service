using CommandRunner;
using NUnit.Framework;
using System;
using System.IO;

namespace CommandRunnerTests
{
    public class RunCommandsTests
    {
        private string _testFile = @"..\..\..\testScriptFile\printDate.bat";

        public RunCommandsTests()
        {
            //_testFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _testFile);
        }

        [Test]
        public void RunCommands_IfCommandFileDoesNotExists_ExceptionThrown()
        {
            Assert.Throws<FileNotFoundException>(() => { var rc = new RunCommands("someNonExistentFile.bat", 1); });
        }

        [Test]
        public void RunCommands_IfInvalidSecondsEntered_ExceptionThrown()
        {
            Assert.Throws<ArgumentException>(() => { new RunCommands(_testFile, -1); });
        }
    }
}
