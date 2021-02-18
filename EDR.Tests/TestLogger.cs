//using System;
//using System.Collections.Generic;
//using Microsoft.Extensions.Logging;

//namespace EDR.Tests
//{

//public class TestLogger<T> : ILogger<T>
//{
//    /// <inheritdoc />
//    public void Log<TState>(
//        LogLevel logLevel,
//        EventId eventId,
//        TState state,
//        Exception exception,
//        Func<TState, Exception, string> formatter)
//    {
//        if (state is IEnumerable<KeyValuePair<string, object>> flv)
//            foreach (var (_, value) in flv)
//                LoggedValues.Add(value);
//        else
//            throw new NotImplementedException();
//    }

//    public List<object> LoggedValues = new List<object>();

//    /// <inheritdoc />
//    public bool IsEnabled(LogLevel logLevel) => true;

//    /// <inheritdoc />
//    public IDisposable BeginScope<TState>(TState state) => throw new NotImplementedException();
//}

//}


