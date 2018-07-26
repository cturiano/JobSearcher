using System;
using JobSearcher.Utils;
using NUnit.Framework;

namespace JobSearcherTest.UtilsTests
{
    [TestFixture]
    internal class RelayCommandTests
    {
        [Test]
        public void ExecuteCanExecuteTest()
        {
            var counter = 0;
            var canExecute = true;

            try
            {
                Action<object> a = null;
                var c = new RelayCommand(a);
                Assert.Fail("Should have thrown ArgumentNullException.", c);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOf<ArgumentNullException>(e);
            }

            var command = new RelayCommand(param => counter++);
            Assert.IsTrue(command.CanExecute(new object()));
            command.Execute(new object());
            Assert.IsTrue(counter == 1);

            command = new RelayCommand(param => counter++, param => canExecute);
            Assert.IsTrue(command.CanExecute(new object()));
            command.Execute(new object());
            Assert.IsTrue(counter == 2);

            canExecute = false;
            Assert.IsFalse(command.CanExecute(new object()));
            command.Execute(new object());
            Assert.IsTrue(counter == 2);
        }
    }
}