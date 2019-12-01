using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace recipebook.functions.test.TestUtility.Fakes
{
    public class FakeLogger<T> : ILogger, ILogger<T>
    {
        private readonly TestContext _context;

        public FakeLogger(TestContext context)
        {
            _context = context;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = new LoggedMessage
            {
                Exception = exception,
                Level = logLevel,
                Message = formatter(state, exception)
            };

            _context.LoggedMessages.Add(message);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new DisposableObject();
        }

        private class DisposableObject : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }

    public class LoggedMessage
    {
        public Exception Exception { get; set; }
        public LogLevel Level { get; set; }
        public string Message { get; set; }
    }
}
