using System.Net;
using Core.Middleware;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Tests.Middleware
{
    [TestClass]
    public class ExceptionHandlingMiddlewareTests
    {
        [TestMethod]
        public async Task InvokeAsync_ShouldHandleException()
        {
            var context = new DefaultHttpContext();
            var responseStream = new MemoryStream();
            context.Response.Body = responseStream;

            var capturedLogMessages = new List<string>();
            var serviceProvider = new ServiceCollection()
                .AddLogging(builder =>
                {
                    builder.AddProvider(new TestLoggerProvider(capturedLogMessages));
                })
                .BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger<ExceptionHandlingMiddleware>>();

            var next = new RequestDelegate(context => throw new Exception("Test Exception"));

            var middleware = new ExceptionHandlingMiddleware(next, logger);

            await middleware.InvokeAsync(context);
            responseStream.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(responseStream);
            var responseText = await reader.ReadToEndAsync();
            var responseObject = JsonConvert.DeserializeAnonymousType(responseText, new { error = "" });

            Assert.AreEqual((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);
            Assert.AreEqual("An error occurred while processing your request.", responseObject?.error);

            Assert.AreEqual(1, capturedLogMessages.Count);
            Assert.IsTrue(capturedLogMessages[0].Contains("Unhandled exception occurred"));
        }
    }

    public class TestLoggerProvider : ILoggerProvider
    {
        private readonly List<string> capturedLogMessages;

        public TestLoggerProvider(List<string> capturedLogMessages)
        {
            this.capturedLogMessages = capturedLogMessages;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new TestLogger(capturedLogMessages);
        }

        public void Dispose()
        {
        }
    }

    public class TestLogger : ILogger
    {
        private readonly List<string> capturedLogMessages;
    

        public TestLogger(List<string> capturedLogMessages)
        {
            this.capturedLogMessages = capturedLogMessages;
        }

        IDisposable? ILogger.BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            capturedLogMessages.Add(formatter(state, exception));
        }

    }
}
