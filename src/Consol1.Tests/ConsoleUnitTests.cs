using System;
using System.IO;
using NUnit.Framework;

namespace Consol1.Tests
{
    [TestFixture]
    public class ConsoleUnitTests
    {
        private StringWriter _consoleOutput;
        private TextWriter _originalOutput;

        [SetUp]
        public void SetUp()
        {
            // Redirect console output so we can assert on it
            _originalOutput = Console.Out;
            _consoleOutput = new StringWriter();
            Console.SetOut(_consoleOutput);
        }

        [TearDown]
        public void TearDown()
        {
            // Restore original console output and release resources
            Console.SetOut(_originalOutput);
            _consoleOutput.Dispose();
        }

        [Test]
        public void Console_WriteLine_WritesExpectedText()
        {
            Console.WriteLine("App started");

            string output = _consoleOutput.ToString().Trim();

            Assert.That(output, Is.EqualTo("App started"));
        }

        [Test]
        public void Console_WriteLine_OutputIsNotEmpty()
        {
            Console.WriteLine("Hello from test");

            string output = _consoleOutput.ToString();

            Assert.That(output, Is.Not.Empty);
        }

        [Test]
        public void Console_MultipleWrites_CapturesAllLines()
        {
            Console.WriteLine("Line 1");
            Console.WriteLine("Line 2");
            Console.WriteLine("Line 3");

            string output = _consoleOutput.ToString();

            Assert.That(output, Does.Contain("Line 1"));
            Assert.That(output, Does.Contain("Line 2"));
            Assert.That(output, Does.Contain("Line 3"));
        }

        [Test]
        public void Console_WriteLine_EmptyString_WritesEmptyLine()
        {
            Console.WriteLine(string.Empty);

            string output = _consoleOutput.ToString();

            Assert.That(output, Is.EqualTo(Environment.NewLine));
        }
    }
}
