using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logger;
using Logger.Enums;
using Moq;

namespace LoggerTest
{
    [TestClass]
    public class JobLoggerTest
    {
        private JobLogger jobLogger;

        [TestInitialize]
        public void Setup()
        {
            jobLogger = new JobLogger();
            
        }

        [TestMethod]
        public void AddLogger_1Logger_1LoggerAdded()
        {
            var mock = new Mock<ILogger>();

            jobLogger.AddLogger(mock.Object);

            Assert.AreEqual(1, jobLogger.CountLoggers);
        }

        [TestMethod]
        public void AddLogger_NullLogger_0LoggerAdded()
        {
            jobLogger.AddLogger(null);

            Assert.AreEqual(0, jobLogger.CountLoggers);
        }

        [TestMethod]
        public void AddLogCategory_1LogCategory_1LogCategoryAdded()
        {
            jobLogger.AddLogCategory(LogCategory.Message);

            Assert.AreEqual(1, jobLogger.CountLogCategories);
        }

        [TestMethod]
        public void AddLogCategory_2DifferentLogCategory_2LogCategoryAdded()
        {
            jobLogger.AddLogCategory(LogCategory.Message);
            jobLogger.AddLogCategory(LogCategory.Error);

            Assert.AreEqual(2, jobLogger.CountLogCategories);
        }

        [TestMethod]
        public void AddLogCategory_2SameLogCategory_1LogCategoryAdded()
        {
            jobLogger.AddLogCategory(LogCategory.Message);
            jobLogger.AddLogCategory(LogCategory.Message);

            Assert.AreEqual(1, jobLogger.CountLogCategories);
        }

        [TestMethod]
        public void ClearLoggers_RemoveAllLoggers()
        {
            var mock = new Mock<ILogger>();
            jobLogger.AddLogger(mock.Object);
            jobLogger.AddLogger(mock.Object);

            jobLogger.ClearLoggers();

            Assert.AreEqual(0, jobLogger.CountLoggers);
        }

        [TestMethod]
        public void ClearLogCategorys_RemoveAllLogCategorys()
        {
            jobLogger.AddLogCategory(LogCategory.Message);
            jobLogger.AddLogCategory(LogCategory.Error);

            jobLogger.ClearLogCategories();

            Assert.AreEqual(0, jobLogger.CountLogCategories);
        }

        [TestMethod]
        public void LogMessage_SameLoggerAndMessage_CallLogger()
        {
            var message = "test message";
            var mock = new Mock<ILogger>();
            jobLogger.AddLogger(mock.Object);
            jobLogger.AddLogCategory(LogCategory.Message);

            jobLogger.LogMessage(LogCategory.Message, message);

            mock.Verify(x => x.LogMessage(LogCategory.Message, message), Times.Once);
        }

        [TestMethod]
        public void LogMessage_DifferentLoggerAndMessage_NotCallLogger()
        {
            var message = "test message";
            var mock = new Mock<ILogger>();
            jobLogger.AddLogger(mock.Object);
            jobLogger.AddLogCategory(LogCategory.Message);

            jobLogger.LogMessage(LogCategory.Error, message);

            mock.Verify(x => x.LogMessage(LogCategory.Message, message), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void LogMessage_NoLoggers_ThrowException()
        {
            var message = "test message";
            jobLogger.AddLogCategory(LogCategory.Message);

            jobLogger.LogMessage(LogCategory.Error, message);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void LogMessage_NoLogCategorys_ThrowException()
        {
            var message = "test message";
            var mock = new Mock<ILogger>();
            jobLogger.AddLogger(mock.Object);

            jobLogger.LogMessage(LogCategory.Error, message);
        }
        
        [TestMethod]
        public void RemoveExistingLogCategory_1Removed()
        {
            jobLogger.AddLogCategory(LogCategory.Message);

            jobLogger.RemoveLogCategory(LogCategory.Message);

            Assert.AreEqual(0, jobLogger.CountLogCategories);
        }

        [TestMethod]
        public void RemoveInexistingLogCategory_0Removed()
        {
            jobLogger.AddLogCategory(LogCategory.Message);

            jobLogger.RemoveLogCategory(LogCategory.Error);

            Assert.AreEqual(1, jobLogger.CountLogCategories);
        }

    }
}
